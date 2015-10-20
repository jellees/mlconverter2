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
    public partial class Transpose : Form
    {
        public Transpose()
        {
            InitializeComponent();
        }

        public int transpose = 0;

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            transpose = (int)numericUpDown1.Value;
        }
    }
}
