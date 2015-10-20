using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace mlconverter2.bkgr
{
    public partial class SoundfontViewer : Form
    {
        public SoundfontViewer()
        {
            InitializeComponent();
        }

        public SoundfontViewer(Rom rome)
        {
            rom = rome;
            InitializeComponent();
            setup();
        }

        // ---- global variables ----

        Rom rom;
        SoundfontBKGR soundfont = new SoundfontBKGR();

        // ---- control functions ----

        private void setup()
        {
            soundfont.prepairSoundfont(new BinaryReader(new FileStream(rom.Path, FileMode.Open)));
            for (int i = 0; i < 0x32; i++) instrumentlst.Items.Add("instrument 0x" + i.ToString("X2"));
            instrumentlst.SetSelected(0, true);
            for (int i = 0; i < 0x90; i++) samplelst.Items.Add("sample 0x" + i.ToString("X2"));
            samplelst.SetSelected(0, true);

            setupSample(0);
        }

        private void setupSample(int index)
        {
            soundfont.ActiveSample = index;
            int[] sample = soundfont.Sample;

            sample0.Value = sample[0];
            sample1.Value = sample[1];
            sample2.Value = sample[2];
            sample3.Value = sample[3];
            sample4.Value = sample[4];
            sample5.Value = sample[5];
            sample6.Value = sample[6];
            sample7.Value = sample[7];
            sample8.Value = sample[8];
            sample9.Value = sample[9];
            sample10.Value = sample[10];
            sample11.Value = sample[11];
            sample12.Value = sample[12];
            sample13.Value = sample[13];
            sample14.Value = sample[14];
            sample15.Value = sample[15];
            sample16.Value = sample[16];
        }

        // ---- misc functions ----

        private void exportAllToSample(string path)
        {
            for (int i = 0; i < 0x90; i++)
            {
                soundfont.ActiveSample = i;
                soundfont.sampleToWave(new BinaryReader(new FileStream(rom.Path, FileMode.Open)), new BinaryWriter(File.OpenWrite(path + "\\sample" + i.ToString("D3") + ".wav")));
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
                soundfont.sampleToWave(new BinaryReader(new FileStream(rom.Path, FileMode.Open)), new BinaryWriter(File.OpenWrite(file.FileName)));
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
    }
}
