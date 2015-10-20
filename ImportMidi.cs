using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mlconverter2
{
    public partial class ImportMidi : Form
    {
        public ImportMidi()
        {
            InitializeComponent();

            comboBox1.Items.Add("mls (Mario & Luigi Superstar Saga)");
            comboBox1.Items.Add("bkg (Banjo Kazooie Grunty's Revenge)");
            comboBox1.SelectedIndex = 0;
        }

        public int headerTracksNum = 1;
        public int format = 0;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            headerTracksNum = (int)numericUpDown1.Value;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            format = comboBox1.SelectedIndex;
        }
    }
}
