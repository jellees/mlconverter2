using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mlconverter2.mlss
{
    public partial class Inserter : Form
    {
        public Inserter()
        {
            InitializeComponent();

            for (int i = 0; i < 0x33; i++) listBox1.Items.Add("Sequence 0x" + i.ToString("X2"));

            listBox1.SetSelected(0, true);
        }

        #region fixed pointer list
        int[] fixedPointers = { 
                                  0x00000000,
                                  0x0019BB34,
                                  0x0019C100,
                                  0x0019D01C,
                                  0x0019DB90,
                                  0x001A1D94,
                                  0x0019F0B4,
                                  0x0019FF38,
                                  0x001A347C,
                                  0x001A4788,
                                  0x001A51AC,
                                  0x001A829C,
                                  0x001A91A0,
                                  0x001ABD0C,
                                  0x001AD1E4,
                                  0x001AE224,
                                  0x001AF26C,
                                  0x001B1408,
                                  0x001B1F24,
                                  0x001B3004,
                                  0x001B37E4,
                                  0x001B4224,
                                  0x001B51B8,
                                  0x001B630C,
                                  0x001B75AC,
                                  0x001B8D18,
                                  0x001BA608,
                                  0x001BC33C,
                                  0x001BC7D8,
                                  0x001BEC30,
                                  0x001BFB74,
                                  0x001C0344,
                                  0x001C1A08,
                                  0x001C1C2C,
                                  0x001C2598,
                                  0x001C2A0C,
                                  0x001C341C,
                                  0x001C5A64,
                                  0x001C875C,
                                  0x001CA828,
                                  0x001CB8A0,
                                  0x001CEF80,
                                  0x001D17E0,
                                  0x001D2CB0,
                                  0x001D3074,
                                  0x001D5D2C,
                                  0x001D51A0,
                                  0x001D6E04,
                                  0x001D78D8,
                                  0x001D808C,
                                  0x001D93C0,
                                  0x001DA7E0
                              };
        #endregion

        public int pointer = 0;
        public int size = 0;
        public int index = 0;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            offset.Text = fixedPointers[listBox1.SelectedIndex].ToString("X8");
            if (radioButton2.Checked)
            {
                numericUpDown1.Enabled = true;
                pointer = (int)numericUpDown1.Value;
                size = 0x7FFFFFFF;
            }
            else
            {
                pointer = fixedPointers[listBox1.SelectedIndex];
                size = fixedPointers[listBox1.SelectedIndex + 1] - fixedPointers[listBox1.SelectedIndex];
            }
            index = listBox1.SelectedIndex;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                numericUpDown1.Enabled = true;
                pointer = (int)numericUpDown1.Value;
                size = 0x7FFFFFFF;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                numericUpDown1.Enabled = false;
                pointer = fixedPointers[listBox1.SelectedIndex];
                size = fixedPointers[listBox1.SelectedIndex + 1] - fixedPointers[listBox1.SelectedIndex];
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pointer = (int)numericUpDown1.Value;
            size = 0x7FFFFFFF;
        }
    }
}
