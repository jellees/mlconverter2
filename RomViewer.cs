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
    public partial class RomViewer : Form
    {
        public RomViewer()
        {
            InitializeComponent();
        }

        private Main mainForm = null;
        public RomViewer(Form calledForm)
        {
            mainForm = calledForm as Main;
            InitializeComponent();
        }

        public void writeSongList(Rom rom)
        {
            for (int i = 0; i < rom.SongCount; i++)
            {
                rom.ActiveSong = i;
                songListBox.Items.Add("Sequence 0x" + i.ToString("X2") + "\t0x" + rom.ActivePointer.ToString("X8"));
            }
        }

        private void songListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.mainForm.openFromRom(songListBox.SelectedIndex);
        }
    }
}
