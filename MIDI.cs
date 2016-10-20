using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mlconverter2
{
    class MIDI
    {
        // ---- data arrays ----

        private List<List<List<int>>> events;

        // ---- variables ----

        private int trackPosition;
        private int eventPosition;
        private int devision;

        // ---- properties ----

        public int TrackPosition
        {
            get { return trackPosition; }
            set { trackPosition = value; eventPosition = 0; }
        }

        public int EventPosition
        {
            get { return eventPosition; }
        }

        public int TrackCount
        {
            get { return events.Count; }
        }

        public int Devision
        {
            get { return devision; }
        }

        // ---- functions ----

        public MIDI(BinaryReader file)
        {
            events = new List<List<List<int>>>();

            // read header
            uint mthd = file.ReadUInt32();
            uint headLength = Endian.SwapEndianU32(file.ReadUInt32());
            int smf = Endian.SwapEndianU16(file.ReadUInt16());
            int trackCount = Endian.SwapEndianU16(file.ReadUInt16());
            devision = Endian.SwapEndianU16(file.ReadUInt16()); // don't know what the 30 means, but I used it in older code

            if (mthd == 0x6468544D && headLength == 6 && smf == 1 && trackCount > 0)
            {
                for (int i = 0; i < trackCount; i++)
                {
                    // skip check and track length
                    file.BaseStream.Position += 8;

                    // setup
                    events.Add(new List<List<int>>());
                    bool endOfTrack = false;
                    int pos = 0;
                    byte runningStatus = 0;

                    //bool notShown = true;

                    while(!endOfTrack)
                    {
                        // read delta time
                        events[i].Add(new List<int>());
                        events[i][pos].Add(Common.fromVLV(Common.readVLV(file)));
                        pos++;

                        // read event
                        events[i].Add(new List<int>());
                        events[i][pos].Add(file.ReadByte());

                        // do the running status byte
                        if (events[i][pos][0] >= 0x80 && events[i][pos][0] <= 0xEF) runningStatus = (byte)events[i][pos][0];
                        else if (events[i][pos][0] >= 0xF0 && events[i][pos][0] <= 0xF7) runningStatus = 0;

                        switch (events[i][pos][0] & 0xF0)
                        {
                            case 0x80:  // note off
                                events[i][pos].Add(file.ReadByte());
                                events[i][pos].Add(file.ReadByte());
                                break;
                            case 0x90:  // note on
                                events[i][pos].Add(file.ReadByte());
                                events[i][pos].Add(file.ReadByte());
                                if (events[i][pos][2] == 0) events[i][pos][0] -= 0x10; // its a note off
                                break;
                            case 0xA0:  // polyphonic aftertouch IGNORE
                                events[i][pos].Add(file.ReadByte());
                                events[i][pos].Add(file.ReadByte());
                                break;
                            case 0xB0:  // control data
                                events[i][pos].Add(file.ReadByte());
                                events[i][pos].Add(file.ReadByte());
                                break;
                            case 0xC0:  // instrument
                                events[i][pos].Add(file.ReadByte());
                                break;
                            case 0xD0:  // channel aftertouch IGNORE
                                events[i][pos].Add(file.ReadByte());
                                break;
                            case 0xE0:  // pitch bend
                                events[i][pos].Add(file.ReadByte());
                                events[i][pos].Add(file.ReadByte());
                                break;
                            case 0xF0:  // meta data
                                if (events[i][pos][0] == 0xFF)
                                {
                                    events[i][pos].Add(file.ReadByte());
                                    events[i][pos].Add(file.ReadByte());
                                    for (int j = 0; j < events[i][pos][2]; j++) events[i][pos].Add(file.ReadByte());

                                    if (events[i][pos][1] == 0x2F) endOfTrack = true;
                                }
                                break;
                            default:    // running status
                                if ((runningStatus & 0xF0) == 0x90) // running status is a note on
                                {
                                    int volume = file.ReadByte();
                                    events[i][pos].Add(events[i][pos][0]);
                                    events[i][pos].Add(volume);
                                    if (volume == 0) events[i][pos][0] = runningStatus - 0x10;
                                    else events[i][pos][0] = runningStatus;
                                }
                                else if ((runningStatus & 0xF0) == 0x80) // running status is a note off
                                {
                                    events[i][pos].Add(events[i][pos][0]);
                                    events[i][pos].Add(file.ReadByte());
                                    events[i][pos][0] = runningStatus;
                                }
                                else // everything else
                                {
                                    events[i][pos].Add(events[i][pos][0]);
                                    events[i][pos].Add(file.ReadByte());
                                    events[i][pos][0] = runningStatus;
                                }



                                /*
                                
                                if ((runningStatus & 0xF0) == 0xB0)
                                {
                                    events[i][pos].Add(events[i][pos][0]);
                                    events[i][pos].Add(file.ReadByte());
                                    events[i][pos][0] = runningStatus;
                                }
                                else
                                {
                                    if(notShown) System.Windows.Forms.MessageBox.Show("unknown byte found while reading\n\nat 0x" +
                                    file.BaseStream.Position.ToString("X8") + ": 0x" + events[i][pos][0].ToString("X2") + "\nwill be read as original data");

                                    events[i][pos].Add(file.ReadByte());

                                    notShown = false;
                                }*/
                                break;
                        }
                        pos++;
                    }
                }
            }
        }

        /// <summary>
        /// gives current event and advances it with one
        /// </summary>
        public int[] read()
        {
            int[] ret = events[trackPosition][eventPosition].ToArray();
            eventPosition++;
            return ret;
        }

        public void close()
        {
            events.Clear();
        }
    }
}
