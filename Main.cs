using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace mlconverter2
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            enableControls(false);
        }

        // ---- global variables ----

        Music music = new Music();
        Rom rom = new Rom();
        bool updateEvent = true;
        bool updateCmb = true;

        RomViewer romViewer;
        soundfonts.SoundfontViewer soundfontViewer;
        
        // ---- control functions ----

        // enable/disable dangerous controls
        private void enableControls(bool value)
        {
            updateEventbtn.Enabled = value;
            addEventbtn.Enabled = value;
            removeEventbtn.Enabled = value;

            eventcmb.Enabled = value;
            valuenum.Enabled = value;

            parameter1num.Enabled = value;
            parameter2num.Enabled = value;
        }

        // open from a direct file
        private void openFromFile(string name)
        {
            switch (Path.GetExtension(name))
            {
                case ".mls": music.Format = (byte)Format.mls; break;
                case ".bkg": music.Format = (byte)Format.bkg; break;
                default: MessageBox.Show("Not a supported format"); break;
            }

            music.Path = name;
            music.Name = Path.GetFileName(name);
            music.openFile(new BinaryReader(new FileStream(name, FileMode.Open)));

            setupMusic();
        }

        // open from a rom
        public void openFromRom(int index)
        {
            switch (rom.RomFormat)
            {
                case (int)Game.mlss: music.Format = (byte)Format.mls; break;
                case (int)Game.bkgr: music.Format = (byte)Format.bkg; break;
                default: MessageBox.Show("Not a supported format"); break;
            }
            
            rom.ActiveSong = index;
            music.Path = null;
            music.Name = "0x" + rom.ActivePointer.ToString("X8");

            BinaryReader file = new BinaryReader(new FileStream(rom.Path, FileMode.Open));
            file.BaseStream.Position = rom.ActivePointer;
            music.openFile(file);

            setupMusic();
        }

        private void openFromMidi(string name, int format, int headerTracks)
        {
            switch (format)
            {
                case (int)Game.mlss: music.Format = (byte)Format.mls; break;
                case (int)Game.bkgr: music.Format = (byte)Format.bkg; break;
                default: MessageBox.Show("Not a supported format"); break;
            }

            music.Path = null;
            music.Name = Path.GetFileName(name);
            music.fromMidi(new BinaryReader(new FileStream(name, FileMode.Open)), headerTracks);

            setupMusic();
        }
        
        // setup everything after loading the music
        private void setupMusic()
        {
            enableControls(true);

            // setup event options
            eventcmb.Items.Clear();
            switch (music.Format)
            {
                case 0x00: eventcmb.Items.AddRange(StaticDataControl.returnEventOptions(0)); break;
                case 0x01: eventcmb.Items.AddRange(StaticDataControl.returnEventOptions(1)); break;
                default: MessageBox.Show("No event options for this format!\nThe process is likely unstable now, please restart the program."); break;
            }
            
            // start with track list
            setupTracks();

            // setup track
	        if (!setupEvents(0))
	        {
				enableControls(false);
		        return;
	        }

            // setup event 0
            setupEvent(0);

            // tell that we have loaded a sequence
            updateLabelStatus();

            // enable stuff
            prepareProgram();
        }

        // setup all tracks
        private void setupTracks()
        {
            trackListbox.Items.Clear();
            for (int i = 0; i < music.TrackCount; i++) trackListbox.Items.Add("track " + i.ToString());
            if (trackListbox.Items.Count > 0) trackListbox.SelectedIndex = 0;
            else eventListbox.Items.Clear();
        }

        // setup a track, Warning: activeEvent is random
        private bool setupEvents(int track)
        {
            eventListbox.Items.Clear();

            music.ActiveTrack = track;
	        if (music.ActiveTrackCount > 0)
	        {
		        for (int i = 0; i < music.ActiveTrackCount; i++)
		        {
			        music.ActiveEvent = i;
			        eventListbox.Items.Add(StaticDataControl.returnEventListData(music.Event, music.Format));
		        }
	        }
	        else
	        {
		        MessageBox.Show("The currently selected sequence has no events.");
		        return false;
	        }

	        return true;
        }

        // set an event
        private void setupEvent(int index)
        {
            if (updateEvent && index != -1)
            {
                music.ActiveEvent = index;
                int[] data = StaticDataControl.returnControlData(music.Event, music.Format);

                setupEventControllers(data);
                eventListbox.SelectedItem = index;
            }
        }

        // setup event controllers
        private void setupEventControllers(int[] data)
        {
            updateCmb = false;
            eventcmb.SelectedIndex = data[0];
            valuenum.Value = data[1];
            valuenum.Enabled = Convert.ToBoolean(data[2]);

            parameter1num.Maximum = data[5];
            parameter1num.Value = data[3];
            parameter1num.Enabled = Convert.ToBoolean(data[4]);

            parameter2num.Maximum = data[8];
            parameter2num.Value = data[6];
            parameter2num.Enabled = Convert.ToBoolean(data[7]);
            updateCmb = true;
        }

        private void updateLabelStatus()
        {
            if (rom.Path == null) romLoadedlbl.Text = "No ROM loaded";
            else romLoadedlbl.Text = "ROM loaded [" + Path.GetFileName(rom.Path) + "]";

            if (music.Name == null) seqLoadedlbl.Text = "No sequence loaded";
            else seqLoadedlbl.Text = "Sequence loaded [" + music.Name + "]";
        }

        // prepare the program depending on formats
        private void prepareProgram()
        {
            // prepare file tab
            switch(music.Format)
            {
                case 0x00:
                    saveFileToolStripMenuItem.Enabled = true;
                    saveFileAsToolStripMenuItem.Enabled = true;
                    exportToolStripMenuItem.Enabled = true;
                    trackMenu.Enabled = true;
                    break;
                case 0x01:
                    saveFileToolStripMenuItem.Enabled = true;
                    saveFileAsToolStripMenuItem.Enabled = true;
                    exportToolStripMenuItem.Enabled = true;
                    trackMenu.Enabled = true;
                    break;
                default:
                    saveFileToolStripMenuItem.Enabled = false;
                    saveFileAsToolStripMenuItem.Enabled = false;
                    exportToolStripMenuItem.Enabled = false;
                    trackMenu.Enabled = false;
                    break;
            }

            // prepare ROM tab
            switch (rom.RomFormat)
            {
                case 0x00:
                    romMenu.Enabled = true;
                    openSequencesListToolStripMenuItem.Enabled = true;
                    exportAllMidiToolStripMenuItem.Enabled = true;
                    soundFontToolStripMenuItem.Visible = true;
                    break;
                case 0x01:
                    romMenu.Enabled = true;
                    openSequencesListToolStripMenuItem.Enabled = true;
                    exportAllMidiToolStripMenuItem.Enabled = true;
                    soundFontToolStripMenuItem.Visible = true;
					//soundFontToolStripMenuItem.Visible = false;
					break;
                default:
                    romMenu.Enabled = false;
                    break;
            }

            // prepare other stuff
            if (rom.RomFormat == (int)Game.mlss && music.Format == (int)Format.mls) insertIntoRomToolStripMenuItem.Enabled = true;
            else insertIntoRomToolStripMenuItem.Enabled = false;
        }

        // ---- misc functions ----

        private void exportAllToMidi(string path)
        {
            switch (rom.RomFormat)
            {
                case (int)Game.mlss: music.Format = (byte)Format.mls; break;
                case (int)Game.bkgr: music.Format = (byte)Format.bkg; break;
                default: MessageBox.Show("Not a supported format"); break;
            }

            Music backup = music;

            for (int i = 0; i < rom.SongCount; i++)
            {
                rom.ActiveSong = i;
                BinaryReader file = new BinaryReader(new FileStream(rom.Path, FileMode.Open));
                file.BaseStream.Position = rom.ActivePointer;
                music.openFile(file);
                if(music.TrackCount != 0) // quick fix to fix the "no music" sequence from marioluigiss
                    music.toMidi(new BinaryWriter(File.OpenWrite(path + "\\sequence" + i.ToString("D3") + ".mid")));
            }

            music = backup;
        }

        public static void ReplaceData(string filename, int position, byte[] data)
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                stream.Position = position;
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
        }

        // ---- event triggered functions ----

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Mario & Luigi SS music format | *.mls | Banjo Kazooie GR music format | *.bkg";
            if (file.ShowDialog() == DialogResult.OK) { openFromFile(file.FileName); }
        }

        private void trackListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setupEvents(trackListbox.SelectedIndex);
            if (music.ActiveEvent > eventListbox.Items.Count) music.ActiveEvent = eventListbox.Items.Count -1;
        }

        private void eventListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setupEvent(eventListbox.SelectedIndex);
        }

        private void eventcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updateCmb)
            {
                List<int> pre = new List<int> { StaticDataControl.returnStatusValues(eventcmb.SelectedIndex, music.Format, music.ActiveTrack), 0, 0 };
                setupEventControllers(StaticDataControl.returnControlData(pre, music.Format));
            }
        }

        private void updateEventbtn_Click(object sender, EventArgs e)
        {
            if (music.ActiveEvent != -1)
            {
                int[] controlData = StaticDataControl.returnControlData(new List<int> { StaticDataControl.returnStatusValues(eventcmb.SelectedIndex, music.Format, music.ActiveTrack), 0, 0 }, music.Format);
                List<int> pre = new List<int>();

                pre.Add((int)valuenum.Value);
                if (Convert.ToBoolean(controlData[4]))
                {
                    pre.Add((int)parameter1num.Value);
                }
                if (Convert.ToBoolean(controlData[7]))
                {
                    pre.Add((int)parameter2num.Value);
                }

                music.Event = pre;

                int Y = eventListbox.TopIndex;
                int activeEvent = music.ActiveEvent;

                setupEvents(music.ActiveTrack);

                eventListbox.TopIndex = Y;
                eventListbox.SetSelected(activeEvent, true);
            }
        }

        private void addEventbtn_Click(object sender, EventArgs e)
        {
            music.addEvent(new List<int> { 0, 0, 0 });
            eventListbox.Items.Insert(music.ActiveEvent + 1, StaticDataControl.returnEventListData(new List<int> { 0, 0, 0 }, music.Format));
            eventListbox.SetSelected(music.ActiveEvent + 1, true);
        }

        private void removeEventbtn_Click(object sender, EventArgs e)
        {
            if (eventListbox.Items.Count > 0)
            {
                music.removeEvent();
                updateEvent = false;
                eventListbox.Items.RemoveAt(music.ActiveEvent);
                updateEvent = true;
                if (eventListbox.Items.Count > 0)
                    if (music.ActiveEvent < eventListbox.Items.Count) eventListbox.SetSelected(music.ActiveEvent, true);
                    else eventListbox.SetSelected(music.ActiveEvent - 1, true);
                else music.ActiveEvent = -1;
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (music.Path != null)
            {
                music.saveFile(new BinaryWriter(File.OpenWrite(music.Path)));
            }
            else saveAs();
        }

        private void saveFileAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void saveAs()
        {
            string extension = null;
            switch (music.Format)
            {
                case (byte)Format.mls: extension = ".mls"; break;
                case (byte)Format.bkg: extension = ".bkg"; break;
                default: extension = ".bin"; break;
            }

            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Music file | *" + extension;
            if (file.ShowDialog() == DialogResult.OK)
            {
                music.saveFile(new BinaryWriter(File.OpenWrite(file.FileName)));
                music.Path = file.FileName;
                music.Name = Path.GetFileName(file.FileName);
            }

            updateLabelStatus();
        }

        private void openRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "GBA rom | *.gba";
            if (file.ShowDialog() == DialogResult.OK)
            {
                rom.Path = file.FileName;
                rom.openRom(new BinaryReader(File.OpenRead(file.FileName)));

                updateLabelStatus();

                romViewer = new RomViewer(this);
                romViewer.Show();
                romViewer.writeSongList(rom);

                prepareProgram();
            }
        }

        private void openSequencesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (romViewer.IsDisposed)
            {
                romViewer = new RomViewer(this);
                romViewer.Show();
                romViewer.writeSongList(rom);
            }
        }

        private void mIDIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "MIDI sequence | *.mid";
            if (file.ShowDialog() == DialogResult.OK)
            {
                music.toMidi(new BinaryWriter(File.OpenWrite(file.FileName)));
                MessageBox.Show("MIDI succesfully exported!");
            }
        }

        private void mIDIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "MIDI sequence | *.mid";
            if (file.ShowDialog() == DialogResult.OK)
            {
                DialogResult dr = new DialogResult(); // this way of doing it makes the form a dialogresult?
                ImportMidi dia = new ImportMidi();
                dr = dia.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    openFromMidi(file.FileName, dia.format, dia.headerTracksNum);
                }
            }
        }

        private void exportAllMidiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                exportAllToMidi(folder.SelectedPath);
                MessageBox.Show("MIDIs succesfully exported!");
            }
        }

        private void soundFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            soundfontViewer = new soundfonts.SoundfontViewer(rom);
            soundfontViewer.ShowDialog();
        }

        private void addTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            music.addTrack(new List<List<int>>());
            setupTracks();
        }

        private void deleteTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            music.removeTrack();
            setupTracks();
        }

        private void insertIntoRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            mlss.Inserter dia = new mlss.Inserter();
            showdia:
            dr = dia.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (dia.pointer != 0)
                {
                    string temp = AppDomain.CurrentDomain.BaseDirectory + "//temp.bin";
                    music.saveFile(new BinaryWriter(File.OpenWrite(temp)));
                    if (File.Exists(temp))
                    {
                        BinaryReader file = new BinaryReader(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "//temp.bin", FileMode.Open));
                        byte[] bytes = file.ReadBytes((int)file.BaseStream.Length);
                        file.Close();
                        File.Delete(temp);
                        if (bytes.Length < dia.size)
                        {
                            ReplaceData(rom.Path, dia.pointer, bytes);
                            ReplaceData(rom.Path, 0x21CB70 + (4 * dia.index), BitConverter.GetBytes(dia.pointer + 0x08000000));
                            MessageBox.Show("Sequence succesfully inserted!");
                        }
                        else
                        {
                            MessageBox.Show("Sequence is too big.\nPlease use a custom offset for large sequences.");
                            goto showdia;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot import into the first slot, it needs to be empty.");
                    goto showdia;
                }
            }
        }

        private void transposeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            Transpose dia = new Transpose();
            dr = dia.ShowDialog();
            if (dr == DialogResult.OK)
            {
                music.transpose(dia.transpose);
            }
        }
    }
}
