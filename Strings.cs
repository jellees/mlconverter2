using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mlconverter2
{
    public enum Format : int
    {
        mls,
        bkg
    }

    public class StaticDataControl
    {
        static public string[] returnEventOptions(int format)
        {
            string[] ret = null;
            switch(format)
            {
                case 0x00: ret = new string[]
                    {
                        "extended note", 
                        "note", 
                        "instrument", 
                        "volume",
                        "panning",
                        "rest",
                        "loop",
                        "tempo",
                        "end of track"
                    }; break;
                case 0x01: ret = new string[]
                    {
                        "tempo change",
                        "rest 8-bit",
                        "rest 16-bit",
                        "rest 24-bit",
                        "note on",
                        "note off",
                        "track volume",
                        "instrument",
                        "pitch",
                        "end of track"
                    }; break;
            }
            return ret;
        }

        static public string returnEventListData(List<int> info, int format)
        {
            string ret = null;

            if (format == 0x00)
            {
                switch (info[0])
                {
                    case 0x00: ret = "0x00\textended note\t0x" + info[1].ToString("X2") + "\t0x" + info[2].ToString("X2"); break;
                    case 0xF0: ret = "0xF0\tinstrument\t0x" + info[1].ToString("X2"); break;
                    case 0xF1: ret = "0xF1\tvolume\t\t0x" + info[1].ToString("X2"); break;
                    case 0xF2: ret = "0xF2\tpanning\t\t0x" + info[1].ToString("X2"); break;
                    case 0xF6: ret = "0xF6\trest\t\t0x" + info[1].ToString("X2"); break;
                    case 0xF8: ret = "0xF8\tloop\t\t0x" + info[1].ToString("X2"); break;
                    case 0xF9: ret = "0xF9\ttempo\t\t0x" + info[1].ToString("X2"); break;
                    default:
                        if (info[0] > 0xFA) ret = "0x" + info[0].ToString("X2") + "\tend of track";
                        else ret = "0x" + info[0].ToString("X2") + "\tnote\t\t0x" + info[1].ToString("X2");
                        break;
                }
            }
            else if (format == 0x01)
            {
                switch (info[0] & 0x0F)
                {
                    case 0x00: ret = "0x" + info[0].ToString("X2") + "\ttempo change\t0x" + info[1].ToString("X6"); break;
                    case 0x01: ret = "0x" + info[0].ToString("X2") + "\trest 8-bit\t\t0x" + info[1].ToString("X2"); break;
                    case 0x02: ret = "0x" + info[0].ToString("X2") + "\trest 16-bit\t\t0x" + info[1].ToString("X4"); break;
                    case 0x03: ret = "0x" + info[0].ToString("X2") + "\trest 24-bit\t\t0x" + info[1].ToString("X6"); break;
                    case 0x05: ret = "0x" + info[0].ToString("X2") + "\tnote on\t\t0x" + info[1].ToString("X2") + "\t0x" + info[2].ToString("X2"); ; break;
                    case 0x06: ret = "0x" + info[0].ToString("X2") + "\tnote off\t\t0x" + info[1].ToString("X2") + "\t0x" + info[2].ToString("X2"); ; break;
                    case 0x07: ret = "0x" + info[0].ToString("X2") + "\ttrack volume\t0x" + info[1].ToString("X2") + "\t0x" + info[2].ToString("X2"); ; break;
                    case 0x08: ret = "0x" + info[0].ToString("X2") + "\tinstrument\t0x" + info[1].ToString("X2"); break;
                    case 0x0A: ret = "0x" + info[0].ToString("X2") + "\tpitch\t\t0x" + info[1].ToString("X2"); break;
                    case 0x0B: ret = "0x" + info[0].ToString("X2") + "\tend of track"; break;
                    default: ret = "0x" + info[0].ToString("X2") + "\tunknown\t0x"; break;
                }
            }
            else MessageBox.Show("returnEventListData missing for this format");

            return ret;
        }

        static public int[] returnControlData(List<int> info, int format)
        {
            int[] ret = null;
            
            if (format == 0x00)
            {
                switch(info[0])
                {   // cmdIndex, statusValue, enable, param1Value, enable, maxNum, param2Value, enable, maxNum
                    case 0x00: ret = new int[] { 0, info[0], 0, info[1], 1, 0xFF, info[2], 1, 0xFF }; break;
                    case 0xF0: ret = new int[] { 2, info[0], 0, info[1], 1, 0xFF, 0, 0, 0xFF }; break;
                    case 0xF1: ret = new int[] { 3, info[0], 0, info[1], 1, 0xFF, 0, 0, 0xFF }; break;
                    case 0xF2: ret = new int[] { 4, info[0], 0, info[1], 1, 0xFF, 0, 0, 0xFF }; break;
                    case 0xF6: ret = new int[] { 5, info[0], 0, info[1], 1, 0xFF, 0, 0, 0xFF }; break;
                    case 0xF8: ret = new int[] { 6, info[0], 0, info[1], 1, 0xFFFF, 0, 0, 0xFF }; break;
                    case 0xF9: ret = new int[] { 7, info[0], 0, info[1], 1, 0xFF, 0, 0, 0xFF }; break;
                    default:
                        if(info[0] > 0xFA) ret = new int[] { 8, info[0], 0, 0, 0, 0xFF, 0, 0, 0xFF };
                        else ret = new int[] { 1, info[0], 1, info[1], 1, 0xFF, 0, 0, 0xFF };
                        break;
                }
            }
            else if (format == 0x01)
            {
                switch(info[0] & 0x0F)
                {
                    case 0x00: ret = new int[] { 0, info[0], 0, info[1], 1, 0xFFFFFF, 0, 0, 0xFF }; break;
                    case 0x01: ret = new int[] { 1, info[0], 0, info[1], 1, 0xFF, 0, 0, 0xFF }; break;
                    case 0x02: ret = new int[] { 2, info[0], 0, info[1], 1, 0xFFFF, 0, 0, 0xFF }; break;
                    case 0x03: ret = new int[] { 3, info[0], 0, info[1], 1, 0xFFFFFF, 0, 0, 0xFF }; break;
                    case 0x05: ret = new int[] { 4, info[0], 0, info[1], 1, 0xFF, info[2], 1, 0xFF }; break;
                    case 0x06: ret = new int[] { 5, info[0], 0, info[1], 1, 0xFF, info[2], 1, 0xFF }; break;
                    case 0x07: ret = new int[] { 6, info[0], 0, info[1], 1, 0xFF, info[2], 1, 0xFF }; break;
                    case 0x08: ret = new int[] { 7, info[0], 0, info[1], 1, 0xFF, 0, 0, 0xFF }; break;
                    case 0x0A: ret = new int[] { 8, info[0], 0, info[1], 1, 0xFFFF, 0, 0, 0xFF }; break;
                    case 0x0B: ret = new int[] { 9, info[0], 0, 0, 0, 0xFF, 0, 0, 0xFF }; break;
                }
            }
            else MessageBox.Show("returnControlData missing for this format");

            return ret;
        }

        static public int returnStatusValues(int index, int format, int track)
        {
            int ret = 0;

            if (format == 0)
            {
                switch (index)
                {
                    case 0: ret = 0x00; break;
                    case 1: ret = 0x30; break;
                    case 2: ret = 0xF0; break;
                    case 3: ret = 0xF1; break;
                    case 4: ret = 0xF2; break;
                    case 5: ret = 0xF6; break;
                    case 6: ret = 0xF8; break;
                    case 7: ret = 0xF9; break;
                    case 8: ret = 0xFF; break;
                }
            }
            else if (format == 1)
            {
                if (track == 0) track++;
                switch (index)
                {
                    case 0: ret = 0x00; break;
                    case 1: ret = 0x01; break;
                    case 2: ret = 0x02; break;
                    case 3: ret = 0x03; break;
                    case 4: ret = 0x05 + ((track - 1) << 4); break;
                    case 5: ret = 0x06 + ((track - 1) << 4); break;
                    case 6: ret = 0x07 + ((track - 1) << 4); break;
                    case 7: ret = 0x08 + ((track - 1) << 4); break;
                    case 8: ret = 0x0A + ((track - 1) << 4); break;
                    case 9: ret = 0x0B; break;
                }
            }
            else MessageBox.Show("returnStatusValues missing for this format");

            int yi = Math.Abs(ret);

            return Math.Abs(ret);
        }
    }

    public enum Game : int
    {
        mlss,
        bkgr
    }
}
