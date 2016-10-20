using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace mlconverter2.soundfonts
{
    class Soundfont
    {
        // ---- data arrays ----

        public int[] instrumentPointers;
        public int[] samplePointers;

        int[][] instrumentDef;
        int[][] sampleDef;

        // ---- variables ----

        int activeSample;

        // ---- properties ----

        public int ActiveSample
        {
            get { return activeSample; }
            set { activeSample = value; }
        }

		public int[] SuperstarSample
		{
			get { return instrumentDef[activeSample]; }
		}

		public int[] KazooieSample
        {
            get { return sampleDef[activeSample]; }
        }

        // ---- functions ----

        public void PrepareSoundfont(BinaryReader file, int romFormat)
        {
			switch (romFormat)
			{
				case 0x00: //Superstar Saga
					int tableStart = 0x00A806B8;
					file.BaseStream.Position = tableStart; // Start of instrument offset table

					instrumentPointers = new int[0xEC];
					for (int i = 0; i < 0xEC; i++)
					{
						int offset = file.ReadInt32();
						if (offset == 0x40000000) break; // this means we have run into the start of instrument data, so we will stop

						if (offset == 0) // Set nonexistent offsets to zero, so they are easier to notice
						{
							instrumentPointers[i] = 0;
						}
						else
						{
							instrumentPointers[i] = tableStart + offset;
						}
					}
					
					// Note: we don't need to store sample pointers, because they are always 0x10 bytes after their respective instrument headers, and we have pointers to those already
					instrumentDef = new int[0xEC][];
					sampleDef = new int[0xEC][];
					for (int i = 0; i < 0xEC; i++)
					{
						if (instrumentPointers[i] != 0)
						{
							instrumentDef[i] = new int[4]; // The instrument header contains 4 values

							//This is just in case there is empty space in-between instruments
							file.BaseStream.Position = instrumentPointers[i];

							for (int j = 0; j < 4; j++)
							{
								instrumentDef[i][j] = file.ReadInt32();
							}
						}
					}
					break;

				case 0x01: //Banjo-Kazooie
					file.BaseStream.Position = 0x006ADE44;
					instrumentPointers = new int[0x32];
					for (int i = 0; i < 0x32; i++) instrumentPointers[i] = Common.repairPointer(file.ReadInt32());

					file.BaseStream.Position = 0x006ADF0C;
					samplePointers = new int[0x90];
					for (int i = 0; i < 0x90; i++) samplePointers[i] = Common.repairPointer(file.ReadInt32());

					instrumentDef = new int[0x32][];

					for (int i = 0; i < 0x32; i++)
					{
						instrumentDef[i] = new int[0x44 / 4];
						file.BaseStream.Position = instrumentPointers[i];

						for (int j = 0; j < 0x44 / 4; j++) instrumentDef[i][j] = file.ReadInt32();
					}

					sampleDef = new int[0x90][];

					for (int i = 0; i < 0x90; i++)
					{
						sampleDef[i] = new int[0x44 / 4];
						file.BaseStream.Position = samplePointers[i];

						for (int j = 0; j < 0x44 / 4; j++) sampleDef[i][j] = file.ReadInt32();
					}
					break;
			}

            file.Close();
        }

        public void KazooieSampleToWave(BinaryReader gba, BinaryWriter file)
        {
            int sampleLength = sampleDef[activeSample][6] - sampleDef[activeSample][4];
            gba.BaseStream.Position = Common.repairPointer(sampleDef[activeSample][4]);
            byte[] sample = gba.ReadBytes(sampleLength);

            file.Write(new byte[] { 0x52, 0x49, 0x46, 0x46 });
            file.Write(sampleLength + 0x24);
            file.Write(new byte[] { 0x57, 0x41, 0x56, 0x45, 0x66, 0x6D, 0x74, 0x20, 0x10, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00 });
            file.Write(sampleDef[activeSample][2]);
            file.Write(sampleDef[activeSample][2]);
            file.Write(new byte[] { 0x01, 0x00, 0x08, 0x00, 0x64, 0x61, 0x74, 0x61 });
            file.Write(sampleLength);
			
			for (int i = 0; i < sample.Length; i++) file.Write(Convert.ToSByte(sample[i] - 0x80));

            gba.Close();
            file.Close();
        }

		public void SuperstarSampleToWave(BinaryReader gba, BinaryWriter file)
		{
			int sampleLength = instrumentDef[activeSample][3];
			gba.BaseStream.Position = instrumentPointers[activeSample] + 0x10;
			byte[] sample = gba.ReadBytes(sampleLength);

			file.Write(new byte[] { 0x52, 0x49, 0x46, 0x46 });
			file.Write(sampleLength + 0x24);
			file.Write(new byte[] { 0x57, 0x41, 0x56, 0x45, 0x66, 0x6D, 0x74, 0x20, 0x10, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00 });
			file.Write(instrumentDef[activeSample][1] / 1000);
			file.Write(instrumentDef[activeSample][1] / 1000); // (Samplerate) * (1 channel) * ((8 bits per sample) / 8)
			file.Write(new byte[] { 0x01, 0x00, 0x08, 0x00, 0x64, 0x61, 0x74, 0x61 });
			file.Write(sampleLength);

			for (int i = 0; i < sample.Length; i++) file.Write(Convert.ToByte(sample[i]));

			gba.Close();
			file.Close();
		}
	}
}
