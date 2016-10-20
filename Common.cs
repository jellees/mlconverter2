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

        /// <summary>
        /// reads a ushort bit formatted flag and returns the count
        /// </summary>
        static public int countFlag(ushort flag)
        {
            int count = 0;

            for (int i = 0; i < 16; i++)
            {
                if ((flag & (1 << i)) != 0) count++;
            }

            return count;
        }

        /// <summary>
        /// returns a ushort bit formatted flag
        /// </summary>
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

        /// <summary>
        /// repairs a pointer
        /// </summary>
        static public int repairPointer(int pointer)
        {
            pointer <<= 8;
            pointer >>= 8;
            return pointer & 0x00FFFFFF;
        }

        /// <summary>
        /// writes a VLV into a binary writer stream
        /// </summary>
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

        /// <summary>
        /// converts a VLV to normal
        /// </summary>
        static public int fromVLV(int vlv)
        {
            int number;

            number = (vlv & 0x7F000000) >> 24;
            number = (number << 7) + ((vlv & 0x7F0000) >> 16);
            number = (number << 7) + ((vlv & 0x7F00) >> 8);
            number = (number << 7) + (vlv & 0x7F);

            return number;
        }

        /// <summary>
        /// reads a VLV from a string
        /// </summary>
        static public int readVLV(BinaryReader file)
        {
            int delta = 0;

            while (true)
            {
                delta += file.ReadByte();
                if ((delta & 0x80) == 0x80)
                {
                    delta <<= 8;
                }
                else break;
            }

            return delta;
        }

        /// <summary>
        /// gives how much bytes are used
        /// </summary>
        static public int giveByteCount(List<List<int>> list, int start)
        {
            int ret = 0;

            for (int i = start; i < list.Count; i++)
            {
                if (list[i][0] == 0) ret += 3;
                else ret += 2;
            }

            return ret;
        }
    }
}
