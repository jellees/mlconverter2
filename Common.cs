using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter2
{
    class Common
    {
        // a class with usefull functions
        
        // reads a ushort bit formatted flag and returns the count
        static public int countFlag(ushort flag)
        {
            int count = 0;

            for (int i = 0; i < 16; i++)
            {
                if ((flag & (1 << i)) != 0) count++;
            }

            return count;
        }

        // returns a ushort bit formatted flag
        static public ushort getFlag(int count, int shift = 0)
        {
            ushort flag = 0;

            for (int i = 0; i < count; i++)
            {
                flag <<= 1;
                flag += 1;
            }

            return flag <<= shift;
        }

        // repairs a pointer
        static public int repairPointer(int pointer)
        {
            pointer <<= 8;
            pointer >>= 8;
            return Math.Abs(pointer);
        }

        // writes a VLV into a binary writer stream
        static public void toVLV(uint value, BinaryWriter file)
        {
            uint buffer;
            buffer = value & 0x7F;

            while (Convert.ToBoolean(value >>= 7))
            {
                buffer <<= 8;
                buffer |= ((value & 0x7F) | 0x80);
            }

            while (true)
            {
                file.Write(Convert.ToByte((buffer << 24) >> 24));
                if (Convert.ToBoolean(buffer & 0x80)) buffer >>= 8;
                else break;
            }
        }
    }
}
