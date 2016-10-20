using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter2
{
    public class Rom
    {
        // ---- data arrays ----
        
        int[] pointers;
        
        // ---- variables ----

        int romFormat = -1;
        int songCount;
        int pointersPointer;
        int activeSong;
        string path = null;

        // ---- properties ----

        public int ActiveSong
        {
            get { return activeSong; }
            set { activeSong = value; }
        }

        public int ActivePointer
        {
            get { return pointers[activeSong]; }
        }

        public int SongCount
        {
            get { return songCount; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public int RomFormat
        {
            get { return romFormat; }
        }

        // ---- functions ----

        public void openRom(BinaryReader file)
        {
            file.BaseStream.Position = 0xA0;
            string key = Encoding.ASCII.GetString(file.ReadBytes(0x12));
            
            switch(key)
            {
                case "MARIO&LUIGIUA88E01": this.romFormat = (int)Game.mlss; this.songCount = 0x33; this.pointersPointer = 0x21CB70; break;
                case "BANJOKAZOOIEBKZE78": this.romFormat = (int)Game.bkgr; this.songCount = 0x12; this.pointersPointer = 0x6AE150; break;
                case "BANJO PILOT\0BAJE78": this.romFormat = (int)Game.bapi; this.songCount = 0x22; this.pointersPointer = 0xB46E6C; break;
                case "ICE AGE 2 THBIAP7D": this.romFormat = (int)Game.icag; this.songCount = 0x1C; this.pointersPointer = 0xE633D4; break;
            }

            pointers = new int[songCount];

            file.BaseStream.Position = pointersPointer;
            for (int i = 0; i < songCount; i++)
            {
                pointers[i] = Common.repairPointer(file.ReadInt32());
            }

            file.Close();
        }
    }
}
