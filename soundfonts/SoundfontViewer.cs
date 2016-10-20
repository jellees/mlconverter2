using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace mlconverter2.soundfonts
{
    public partial class SoundfontViewer : Form
    {
        // ---- global variables ----

        Rom rom;
        Soundfont soundfont = new Soundfont();

		public SoundfontViewer()
		{
			InitializeComponent();
		}

		public SoundfontViewer(Rom _rom)
		{
			rom = _rom;
			InitializeComponent();
			setup();
		}

		// ---- control functions ----

		private void setup()
        {
            soundfont.PrepareSoundfont(new BinaryReader(new FileStream(rom.Path, FileMode.Open)), rom.RomFormat);

	        if (rom.RomFormat == 0x00)
	        {
		        for (int i = 0; i < soundfont.instrumentPointers.Length; i++)
		        {
			        if (soundfont.instrumentPointers[i] == 0) continue;
					instrumentlst.Items.Add("instrument 0x" + i.ToString("X2"));
					samplelst.Items.Add("sample 0x" + i.ToString("X2"));
			        samplelst2.Items.Add("sample 0x" + i.ToString("X2"));
		        }
		        instrumentlst.SetSelected(0, true);
	        }
	        else
	        {
				for (int i = 0; i < 0x32; i++)
				{
					instrumentlst.Items.Add("instrument 0x" + i.ToString("X2"));
				}
				instrumentlst.SetSelected(0, true);
				for (int i = 0; i < 0x90; i++)
				{
					samplelst.Items.Add("sample 0x" + i.ToString("X2"));
					samplelst2.Items.Add("sample 0x" + i.ToString("X2"));
				}
			}

			samplelst.SetSelected(0, true);
			samplelst2.SetSelected(0, true);

			setupSample(0);
		}

        private void setupSample(int index)
        {
            soundfont.ActiveSample = index;
	        int[] sample;

			if (rom.RomFormat == 0x00)
			{
				sample = soundfont.SuperstarSample;

				if (sample == null)
				{
					MessageBox.Show("Error, cannot load a null sample!");
					return;
				}

				SuperstarSamples.Enabled = true;
				KazooieSamples.Enabled = false;
				tabControl1.SelectTab(SuperstarSamples);

				checkBox1.Checked = (sample[0] == 0x40000000);
				sample17.Value = ((decimal)sample[1]) / 1000000;
				sample18.Value = (uint)sample[2];
				sample19.Value = (uint)sample[3];
			}
			else
			{
				sample = soundfont.KazooieSample;

				SuperstarSamples.Enabled = false;
				KazooieSamples.Enabled = true;
				tabControl1.SelectTab(KazooieSamples);

				sample0.Value = (uint)sample[0];
				sample1.Value = (uint)sample[1];
				sample2.Value = (uint)sample[2];
				sample3.Value = (uint)sample[3];
				sample4.Value = (uint)sample[4];
				sample5.Value = (uint)sample[5];
				sample6.Value = (uint)sample[6];
				sample7.Value = (uint)sample[7];
				sample8.Value = (uint)sample[8];
				sample9.Value = (uint)sample[9];
				sample10.Value = (uint)sample[10];
				sample11.Value = (uint)sample[11];
				sample12.Value = (uint)sample[12];
				sample13.Value = (uint)sample[13];
				sample14.Value = (uint)sample[14];
				sample15.Value = (uint)sample[15];
				sample16.Value = (uint)sample[16];
			}
        }

        // ---- misc functions ----

        private void exportAllToSample(string path)
		{
			switch (rom.RomFormat)
			{
				case 0x00:
					for (int i = 0; i < soundfont.instrumentPointers.Length; i++)
					{
						if (soundfont.instrumentPointers[i] == 0) continue;
						soundfont.ActiveSample = i;
						soundfont.SuperstarSampleToWave(new BinaryReader(new FileStream(rom.Path, FileMode.Open)), new BinaryWriter(File.OpenWrite(path + "\\sample" + i.ToString("D3") + ".wav")));
					}
					break;

				case 0x01:
					for (int i = 0; i < soundfont.samplePointers.Length; i++)
					{
						soundfont.ActiveSample = i;
						soundfont.KazooieSampleToWave(new BinaryReader(new FileStream(rom.Path, FileMode.Open)), new BinaryWriter(File.OpenWrite(path + "\\sample" + i.ToString("D3") + ".wav")));
					}
					break;

			}

			soundfont.ActiveSample = samplelst.SelectedIndex;
        }

        // ---- event triggered functions ----

        private void samplelst_SelectedIndexChanged(object sender, EventArgs e)
        {
            setupSample(samplelst.SelectedIndex);
        }

        private void instrumentlst_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exportSamplebtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Wave|*.wav";
            if (file.ShowDialog() == DialogResult.OK)
            {
				switch (rom.RomFormat)
				{
					case 0x00:
						soundfont.SuperstarSampleToWave(new BinaryReader(new FileStream(rom.Path, FileMode.Open)), new BinaryWriter(File.OpenWrite(file.FileName)));
						break;
					case 0x01:
						soundfont.KazooieSampleToWave(new BinaryReader(new FileStream(rom.Path, FileMode.Open)), new BinaryWriter(File.OpenWrite(file.FileName)));
						break;
				}
			}

            soundfont.ActiveSample = samplelst.SelectedIndex;
        }

        private void exportSamplesbtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                exportAllToSample(folder.SelectedPath);
                MessageBox.Show("Samples succesfully exported");
            }
        }

		private void button2_Click(object sender, EventArgs e)
		{
			exportSamplebtn_Click(sender, e);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			exportSamplesbtn_Click(sender, e);
		}

		private void samplelst2_SelectedIndexChanged(object sender, EventArgs e)
		{
			setupSample(samplelst2.SelectedIndex);
		}
	}
}
