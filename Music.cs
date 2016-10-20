using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace mlconverter2
{
    public class Music
    {
        // ---- data arrays ----

        private List<List<List<int>>> events;
        private int[] pointers;

        // ---- variables ----

        private int activeTrack = 0;
        private int activeEvent = 0;
        private int trackCount = 0;
        private byte format = 0xff;
        private string path = null;
        private string name = null;

        // ---- properties ----

        public int ActiveTrack
        {
            get { return activeTrack; }
            set { activeTrack = value; }
        }

        public int ActiveEvent
        {
            get { return activeEvent; }
            set { activeEvent = value; }
        }

        public int TrackCount
        {
            get { return trackCount; }
        }

        public int ActiveTrackCount
        {
            get { return events[activeTrack].Count; }
        }

        public byte Format
        {
            get { return format; }
            set { format = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public List<int> Event
        {
            get { return events[activeTrack][activeEvent]; }
            set { events[activeTrack][activeEvent] = value; }
        }

        // ---- functions ----

        public void removeEvent()
        {
            events[this.activeTrack].RemoveAt(this.activeEvent);
        }

        public void addEvent(List<int> item)
        {
            events[this.activeTrack].Insert(this.activeEvent + 1, item);
        }

        public void removeTrack()
        {
            events.RemoveAt(this.activeTrack);
            trackCount = events.Count;
        }

        public void addTrack(List<List<int>> track)
        {
            events.Add(track);
            trackCount = events.Count;
        }

        #region open functions

        public void openFile(BinaryReader file)
        {
            if (format == 0xff) throw new ArgumentException("A format is not specified.", "format");

            switch (this.format)
            {
                case 0x00: openMls(file); break;
                case 0x01: openBkg(file); break;
                default: break;
            }

            file.Close();
        }

        private void openMls(BinaryReader file)
        {
            // offset value
            int offset = (int)file.BaseStream.Position;
            
            // read header
            trackCount = Common.countFlag(file.ReadUInt16());
            pointers = new int[trackCount];
            for (int i = 0; i < trackCount; i++) pointers[i] = file.ReadUInt16();

            events = new List<List<List<int>>>(); //! this is very important to include

            // read events
            for (int i = 0; i < trackCount; i++ )
            {
                file.BaseStream.Position = offset + pointers[i];
                events.Add(new List<List<int>>());
                bool endOfTrack = false;

                while (!endOfTrack)
                {
                    byte status = file.ReadByte();

                    switch (status)
                    {
                        case 0x00: events[i].Add(new List<int> { 0x00, file.ReadByte(), file.ReadByte() }); break; // note extension
                        case 0xF0: events[i].Add(new List<int> { 0xF0, file.ReadByte() }); break; // instrument
                        case 0xF1: events[i].Add(new List<int> { 0xF1, file.ReadByte() }); break; // volume
                        case 0xF2: events[i].Add(new List<int> { 0xF2, file.ReadByte() }); break; // panning
                        case 0xF6: events[i].Add(new List<int> { 0xF6, file.ReadByte() }); break; // rest
                        case 0xF8: events[i].Add(new List<int> { 0xF8, file.ReadUInt16() }); endOfTrack = true; break; // loop
                        case 0xF9: events[i].Add(new List<int> { 0xF9, file.ReadByte() }); break; // tempo
                        default:
                            if (status > 0xFA) { events[i].Add(new List<int> { status }); endOfTrack = true; } // end of track
                            else { events[i].Add(new List<int> { status, file.ReadByte() }); } // note
                            break;
                    }
                }
            }
        }

        private void openBkg(BinaryReader file)
        {
            // read header
            trackCount = file.ReadInt32();
            int unknown1 = file.ReadInt32();
            int trackPointers = Common.repairPointer(file.ReadInt32());
            int unknown2 = file.ReadInt32();
            int unknown3 = file.ReadInt32();

            // read trackpointers
            file.BaseStream.Position = trackPointers;
            pointers = new int[trackCount];
            for (int i = 0; i < trackCount; i++) pointers[i] = Common.repairPointer(file.ReadInt32());

            events = new List<List<List<int>>>();

            // read events
            for (int i = 0; i < trackCount; i++)
            {
                file.BaseStream.Position = pointers[i];
                events.Add(new List<List<int>>());
                bool endOfTrack = false;

                while (!endOfTrack)
                {
                    byte status = file.ReadByte();

                    switch(status & 0x0F)
                    {
                        case 0x00: events[i].Add(new List<int> { status, file.ReadUInt16() + (file.ReadByte() << 16) }); break; // tempo change
                        case 0x01: events[i].Add(new List<int> { status, file.ReadByte() }); break; // 8-bit rest
                        case 0x02: events[i].Add(new List<int> { status, file.ReadUInt16() }); break; // 16-bit rest
                        case 0x03: events[i].Add(new List<int> { status, file.ReadUInt16() + (file.ReadByte() << 16) }); break; // 24-bit rest
                        case 0x05: events[i].Add(new List<int> { status, file.ReadByte(), file.ReadByte() }); break; // note on 
                        case 0x06: events[i].Add(new List<int> { status, file.ReadByte(), file.ReadByte() }); break; // note off
                        case 0x07: events[i].Add(new List<int> { status, file.ReadByte(), file.ReadByte() }); break; // track volume
                        case 0x08: events[i].Add(new List<int> { status, file.ReadByte() }); break; // instrument
                        case 0x0A: events[i].Add(new List<int> { status, file.ReadUInt16() }); break; // pitch change
                        case 0x0B: events[i].Add(new List<int> { status }); endOfTrack = true; break; // end of track
                        default: events[i].Add(new List<int> { status }); MessageBox.Show("unknown command found: 0x" + status.ToString("X2") 
                            + "at 0x" + file.BaseStream.Position.ToString("X8")); break; // unknown
                    }
                }
            }
        }

        #endregion

        #region save functions

        public void saveFile(BinaryWriter file)
        {
            switch (this.format)
            {
                case 0x00: saveMls(file); break;
                case 0x01: saveBkg(file); break;
                default: MessageBox.Show("Cannot save this format"); break;
            }

            file.Close();
        }

        private void saveMls(BinaryWriter file)
        {
            // write header
            file.Write(Common.getFlag(this.trackCount, 2));
            for (int i = 0; i < this.trackCount; i++) file.Write(Convert.ToUInt16(0xFFFF));
            ushort[] pointers = new ushort[this.trackCount];

            // write events
            for (int i = 0; i < this.trackCount; i++)
            {
                pointers[i] = (ushort)file.BaseStream.Position;

                for (int j = 0; j < events[i].Count; j++)
                {
                    // write the status byte
                    file.Write(Convert.ToByte(events[i][j][0]));
                    // write the parameters
                    switch (events[i][j][0])
                    {
                        case 0x00: file.Write(Convert.ToByte(events[i][j][1])); file.Write(Convert.ToByte(events[i][j][2])); break;
                        case 0xF0: file.Write(Convert.ToByte(events[i][j][1])); break;
                        case 0xF1: file.Write(Convert.ToByte(events[i][j][1])); break;
                        case 0xF2: file.Write(Convert.ToByte(events[i][j][1])); break;
                        case 0xF6: file.Write(Convert.ToByte(events[i][j][1])); break;
                        case 0xF8: file.Write(Convert.ToUInt16(events[i][j][1])); break;
                        case 0xF9: file.Write(Convert.ToByte(events[i][j][1])); break;
                        default:
                            if (events[i][j][0] < 0xFA) file.Write(Convert.ToByte(events[i][j][1])); break; // end of track event doesn't have parameters
                    }
                }
            }

            // rewrite pointers
            file.BaseStream.Position = 0x02;
            for (int i = 0; i < this.trackCount; i++) file.Write(pointers[i]);
        }

        private void saveBkg(BinaryWriter file)
        {
            // write header
            file.Write(this.trackCount);
            file.Write(0x000001E0);
            file.Write(0x00000014);
            file.Write(0x086AE300);
            file.Write(0x086ADE44);

            // write space for pointers
            for (int i = 0; i < this.trackCount; i++) file.Write(0xFFFFFFFF);
            uint[] pointers = new uint[this.trackCount];

            // write events
            for (int i = 0; i < this.trackCount; i++)
            {
                pointers[i] = (uint)file.BaseStream.Position;

                for (int j = 0; j < events[i].Count; j++)
                {
                    // write the status byte
                    file.Write(Convert.ToByte(events[i][j][0]));
                    // write the parameters
                    switch (events[i][j][0] & 0x0F)
                    {
                        case 0x00: file.Write(Convert.ToByte(events[i][j][1] & 0xFF)); file.Write(Convert.ToUInt16(events[i][j][1] >> 8)); break;
                        case 0x01: file.Write(Convert.ToByte(events[i][j][1])); break;
                        case 0x02: file.Write(Convert.ToUInt16(events[i][j][1])); break;
                        case 0x03: file.Write(Convert.ToByte(events[i][j][1] & 0xFF)); file.Write(Convert.ToUInt16(events[i][j][1] >> 8)); break;
                        case 0x05: file.Write(Convert.ToByte(events[i][j][1])); file.Write(Convert.ToByte(events[i][j][2])); break;
                        case 0x06: file.Write(Convert.ToByte(events[i][j][1])); file.Write(Convert.ToByte(events[i][j][2])); break;
                        case 0x07: file.Write(Convert.ToByte(events[i][j][1])); file.Write(Convert.ToByte(events[i][j][2])); break;
                        case 0x08: file.Write(Convert.ToByte(events[i][j][1])); break;
                        case 0x0A: file.Write(Convert.ToUInt16(events[i][j][1])); break;
                    }
                }
            }

            // rewrite pointers
            file.BaseStream.Position = 0x14;
            for (int i = 0; i < this.trackCount; i++) file.Write(pointers[i]);
        }

        #endregion

        #region export to midi functions

        public void toMidi(BinaryWriter file)
        {
            switch (this.format)
            {
                case 0x00: toMidiMls(file); break;
                case 0x01: toMidiBkg(file); break;
                default: MessageBox.Show("Cannot export this format to MIDI"); break;
            }

            file.Close();
        }

        private void toMidiMls(BinaryWriter file)
        {
            // contains a lot of old code. but I don't know if a better one is needed since this code covers
            // everything nicely I guess. it does contain some inconvinient ways of doing volume
            // so need to fix volume stuff later maybe
            
            // write midi header
            file.Write(0x6468544D);
            file.Write(0x06000000);
            file.Write(Convert.ToUInt16(0x0100));
            file.Write(Endian.SwapEndianU16(Convert.ToUInt16(this.trackCount + 1)));
            file.Write(Convert.ToUInt16(0xC003));

            // write first midi channel
            file.Write(0x6B72544D);
            file.Write(0x0B000000);
            file.Write(Convert.ToByte(0));
            file.Write(Convert.ToByte(0xFF)); file.Write(Convert.ToByte(0x51)); file.Write(Convert.ToByte(0x03));  // FF meta event
            file.Write(Convert.ToByte(0x05)); file.Write(Convert.ToByte(0x07)); file.Write(Convert.ToByte(0xC6));  // the parameters
            file.Write(0x002FFF00);

            bool noNoteExtend = true;
            int tempo = 0;

            // write each channel
            for (int i = 0; i < this.trackCount; i++)
            {
                // stuff to begin each channel with
                file.Write(0x6B72544D);
                int lengthPos = (int)file.BaseStream.Position;   // this variable is used to jump back later to write the length of the track
                file.Write(0xFFFFFFFF);

                int rest = 0;
                int length = 0;
                int volume = 0;

                for (int j = 0; j < events[i].Count; j++)
                {
                    switch (events[i][j][0])
                    {
                        case 0x00: length += events[i][j][2]; noNoteExtend = false; break;
                        case 0xF0: file.Write(Convert.ToByte(0));
                                file.Write(Convert.ToByte(0xC0 + i));
                                file.Write(Convert.ToByte(events[i][j][1])); break;
                        case 0xF1: if (noNoteExtend) { volume = events[i][j][1] / 2; }; break;
                        case 0xF2: file.Write(Convert.ToByte(0));
                                file.Write(Convert.ToUInt16(0x0AB0 + i));
                                file.Write(Convert.ToByte(events[i][j][1] / 2)); break;
                        case 0xF6: rest += events[i][j][1]; break;
                        case 0xF8: file.Write(0x002FFF00); j = events[i].Count; break; // read as end of track
                        case 0xF9: tempo = events[i][j][1]; break;
                        default:
                            if (events[i][j][0] > 0xFA) { file.Write(0x002FFF00); j = events[i].Count; }
                            else
                            {
                                length += events[i][j][0];

                                // write rest
                                Common.toVLV((uint)rest * 0x14, file);

                                // note on
                                file.Write(Convert.ToByte(0x90 + i));
                                file.Write(Convert.ToByte(events[i][j][1]));
                                file.Write(Convert.ToByte(volume));

                                // delta time
                                Common.toVLV((uint)length * 0x14, file);

                                // note off
                                file.Write(Convert.ToByte(0x80 + i));
                                file.Write(Convert.ToByte(events[i][j][1]));
                                file.Write(Convert.ToByte(0));

                                rest = 0;
                                length = 0;
                                noNoteExtend = true;
                            }
                            break;
                    }
                }

                // write position
                int originalPos = (int)file.BaseStream.Position;
                int trackLength = (int)file.BaseStream.Position - lengthPos;
                file.BaseStream.Position = lengthPos;
                file.Write(Endian.SwapEndian32(trackLength - 4));
                file.BaseStream.Position = originalPos;
            }

            // go back and write tempo
            file.BaseStream.Position = 0x1A; // the tempo is written on a fixed place anyway
            int time = 60000000 / tempo;
            file.Write(Convert.ToSByte(time >> 16));          // 
            file.Write(Convert.ToSByte((time << 16) >> 24));  // weird system to write out an int into 3 bytes
            file.Write(Convert.ToSByte((time << 24) >> 24));  //
        }

        private void toMidiBkg(BinaryWriter file)
        {
            // write midi header
            file.Write(0x6468544D);
            file.Write(0x06000000);
            file.Write(Convert.ToUInt16(0x0100));
            file.Write(Endian.SwapEndianU16(Convert.ToUInt16(this.trackCount)));
            file.Write(Convert.ToUInt16(0xC003));

            // write events
            for (int i = 0; i < this.trackCount; i++)
            {
                // stuff to begin each channel with
                file.Write(0x6B72544D);
                int lengthPos = (int)file.BaseStream.Position;   // this variable is used to jump back later to write the length of the track
                file.Write(0xFFFFFFFF);

                int rest = 0;

                for (int j = 0; j < events[i].Count; j++)
                {
                    switch (events[i][j][0] & 0x0F)
                    {
                        case 0x00: file.Write(Convert.ToByte(0x0));
                                file.Write(Convert.ToByte(0xFF)); file.Write(Convert.ToByte(0x51)); file.Write(Convert.ToByte(0x03));
                                file.Write(Convert.ToByte((events[i][j][1] & 0x00FF0000) >> 16));
                                file.Write(Convert.ToByte((events[i][j][1] & 0x0000FF00) >> 8));
                                file.Write(Convert.ToByte(events[i][j][1] & 0x000000FF)); break;
                        case 0x01: rest += events[i][j][1]; break;
                        case 0x02: rest += events[i][j][1]; break;
                        case 0x03: rest += events[i][j][1]; break;

                        case 0x05: Common.toVLV((uint)rest * 2, file);
                                file.Write(Convert.ToByte(0x90 + (i - 1)));
                                file.Write(Convert.ToByte(events[i][j][1]));
                                file.Write(Convert.ToByte(events[i][j][2]));
                                rest = 0; break;
                        case 0x06: Common.toVLV((uint)rest * 2, file);
                                file.Write(Convert.ToByte(0x80 + (i - 1)));
                                file.Write(Convert.ToByte(events[i][j][1]));
                                file.Write(Convert.ToByte(0));
                                rest = 0; break;
                        case 0x07: if (events[i][j][1] == 0x07)
                            {
                                Common.toVLV((uint)rest * 2, file);
                                file.Write(Convert.ToUInt16(0x07B0 + (i - 1)));
                                file.Write(Convert.ToByte(events[i][j][2]));
                                rest = 0; break;
                            } break;
                        case 0x08: 
                                Common.toVLV((uint)rest * 2, file);
                                file.Write(Convert.ToByte(0xC0 + (i - 1)));
                                file.Write(Convert.ToByte(events[i][j][1])); 
                                rest = 0; break;

                        case 0x0A: 
                                Common.toVLV((uint)rest * 2, file);
                                file.Write(Convert.ToByte(0xE0 + (i - 1)));
                                file.Write(Convert.ToByte(0x00));
                                file.Write(Convert.ToByte(events[i][j][1] / 0x80));
                                rest = 0; break; // pitch change, don't know how that works yet
                        case 0x0B: file.Write(Convert.ToByte(0x0));
                                file.Write(Convert.ToByte(0xFF));
                                file.Write(Convert.ToByte(0x2F));
                                file.Write(Convert.ToByte(0x00)); 
                                j = events[i].Count; break;
                    }
                }

                // write position
                int originalPos = (int)file.BaseStream.Position;
                int trackLength = (int)file.BaseStream.Position - lengthPos;
                file.BaseStream.Position = lengthPos;
                file.Write(Endian.SwapEndian32(trackLength - 4));
                file.BaseStream.Position = originalPos;
            }
        }

        #endregion

        #region import from midi functions

        public void fromMidi(BinaryReader file, int headerTracks, bool normalisation, bool loop, int loopformat)
        {
            if (format == 0xff) throw new ArgumentException("A format is not specified.", "format");

            switch (this.format)
            {
                case 0x00: fromMidiMls(file, headerTracks, normalisation, loop, loopformat); break;
                case 0x01: fromMidiBkg(file, headerTracks); break;
                default: break;
            }

            file.Close();
        }

        private void fromMidiMls(BinaryReader file, int headerTracks, bool normalisation, bool loop, int loopformat)
        {
            events = new List<List<List<int>>>();
            MIDI mid = new MIDI(file);
            int tempo = 0;

            trackCount = mid.TrackCount - headerTracks;

            // search in header tracks for tempo 
            for (int i = 0; i < headerTracks; i++ )
            {
                mid.TrackPosition = i;
                int[] status;

                while (true)
                {
                    status = mid.read();
                    status = mid.read();

                    if (status[0] == 0xFF)
                    {
                        // forgot why to divide by 2, maybe because it would go too fast otherwise
                        if (status[1] == 0x51) tempo = (60000000 / ((status[3] << 16) + (status[4] << 8) + (status[5])));
                        else if (status[1] == 0x2F) break;
                    }
                }
            }
            
            // read the other tracks
            for (int i = headerTracks; i < mid.TrackCount; i++)
            {
                int pos = i - headerTracks;
                mid.TrackPosition = i;

                events.Add(new List<List<int>>());

                bool endOfTrack = false;
                int[] status;

                int rest = 0;
                int note = 0;
                int noteLength = 0;
                decimal noteVolume = 0;
                decimal trackVolume = 0x7F; // this is the standard for each midi, if the volume isn't specified
                int volume = 0;

                int loopStart = 0;

                // always first the tempo to play safe
                if (tempo == 0) events[pos].Add(new List<int> { 0xF9, 0x78 });
                else events[pos].Add(new List<int> { 0xF9, tempo });

                while (!endOfTrack)
                {
                    // ---------- read rest ----------
                    rest = Convert.ToInt32(Convert.ToDecimal(mid.read()[0]) / (Convert.ToDecimal(mid.Devision) / 0x30));

                    if (note != 0) noteLength += rest;
                    if (rest != 0 && note == 0)
                        while (true)
                            if (rest > 0x90) { events[pos].Add(new List<int> { 0xF6, 0x90 }); rest -= 0x90; }
                            else { events[pos].Add(new List<int> { 0xF6, rest }); rest = 0; break; }

                    // ---------- read event ----------
                    status = mid.read();

                    switch (status[0] & 0xF0)
                    {
                        case 0x90:                                                                      // note on
                            if (status[1] == 0x7F) loopStart = events[pos].Count;//first look for a loop
                            else if (note == 0)
                            {
                                note = status[1];
                                noteVolume = status[2];
                                noteLength = 0;
                            }
                            // quick and dirty solution on note normalisation
                            // notice that you cannot normalize a note when the length = 0, thus more notes starting at the same time
                            // this will end up with 0 length notes and will eventually lead to a crash
                            // because 0 length notes are read as extended notes
                            else if (normalisation && noteLength != 0)
                            {
                                // write volume changes
                                int newVolume = Convert.ToInt32(trackVolume / 100 * (noteVolume / 0x7F * 100)) * 2;
                                if (volume != newVolume)
                                {
                                    volume = newVolume;
                                    events[pos].Add(new List<int> { 0xF1, volume });
                                }

                                // write note/notes
                                while (true)
                                    if (noteLength > 0x90) { events[pos].Add(new List<int> { 0x00, note + 0x80, 0x90 }); noteLength -= 0x90; }
                                    else { events[pos].Add(new List<int> { noteLength, note }); break; }

                                // setup new note
                                note = status[1];
                                noteVolume = status[2];
                                noteLength = 0;
                            }
                            break;

                        case 0x80:
                                if (note == status[1] && noteLength != 0)                                                      // note off
                                {
                                    // write volume changes
                                    int newVolume = Convert.ToInt32(trackVolume / 100 * (noteVolume / 0x7F * 100)) * 2;
                                    if (volume != newVolume)
                                    {
                                        volume = newVolume;
                                        events[pos].Add(new List<int> { 0xF1, volume });
                                    }

                                    // write note/notes
                                    while (true)
                                        if (noteLength > 0x90) { events[pos].Add(new List<int> { 0x00, note + 0x80, 0x90 }); noteLength -= 0x90; }
                                        else { events[pos].Add(new List<int> { noteLength, note }); break; }
                                    
                                    // this note has been done, clear note to read next note
                                    note = 0;
                                }
                                break;

                        case 0xB0: if (status[1] == 0x07) trackVolume = status[2]; break;                   // channel volume

                        case 0xC0: events[pos].Add(new List<int> { 0xF0, status[1] }); break;               // instrument

                        case 0xF0:
                                if (status[1] == 0x2F)                                                      // end of track
                                {
                                    if (loop)
                                    {
                                        if (loopformat == 0) loopStart = 0;
                                        events[pos].Add(new List<int> { 0xF8, (0xFFFF - Common.giveByteCount(events[pos], loopStart)) }); 
                                        endOfTrack = true;
                                    }
                                    else events[pos].Add(new List<int> { 0xFF }); endOfTrack = true;
                                }
                                else if (status[1] == 0x51)                                                 // volume
                                {
                                    events[pos].Add(new List<int> { 0xF9, (60000000 / ((status[3] << 16) + (status[4] << 8) + (status[5]))) });
                                }
                                break;
                    }
                }
            }
        }

        private void fromMidiBkg(BinaryReader file, int headerTracks)
        {
            events = new List<List<List<int>>>();
            MIDI mid = new MIDI(file);

            trackCount = mid.TrackCount;

            for (int i = 0; i < trackCount; i++)
            {
                int channelNumber;
                if (i > headerTracks) channelNumber = i - headerTracks;
                else channelNumber = 0;
                
                mid.TrackPosition = i;
                events.Add(new List<List<int>>());
                bool endOfTrack = false;

                while (!endOfTrack)
                {
                    int rest = 0;
                    if (mid.Devision != 0) rest = mid.read()[0] / mid.Devision;
                    else rest = mid.read()[0];

                    if (rest > 0x00 && rest <= 0xFF) events[i].Add(new List<int> { 0x01, rest }); // 8-bit rest
                    if (rest > 0xFF && rest <= 0xFFFF) events[i].Add(new List<int> { 0x02, rest }); // 16-bit rest
                    if (rest > 0xFFFF && rest <= 0xFFFFFF) events[i].Add(new List<int> { 0x03, rest }); // 16-bit rest
                    if (rest > 0xFFFFFF) MessageBox.Show("delta time larger than 0xFFFFFF, not used");

                    int[] status = mid.read();

                    switch(status[0] & 0xF0)
                    {
                        case 0x80: events[i].Add(new List<int> { (channelNumber << 4) + 0x06, status[1], status[2] }); break; // note off
                        case 0x90: events[i].Add(new List<int> { (channelNumber << 4) + 0x05, status[1], status[2] }); break; // note on
                        case 0xB0: if (status[1] == 0x07) events[i].Add(new List<int> { (channelNumber << 4) + 0x07, status[1], status[2] }); break; // channel volume
                        case 0xC0: events[i].Add(new List<int> { (channelNumber << 4) + 0x08, status[1] }); break; // instrument
                        case 0xE0: events[i].Add(new List<int> { (channelNumber << 4) + 0x0A, status[2] * 0x80 }); break; // pitch bend
                        case 0xF0: if (status[1] == 0x2F) { events[i].Add(new List<int> { 0x0B }); endOfTrack = true; } // end of track
                            else if (status[1] == 0x51) { events[i].Add(new List<int> { 0x00, (status[3] << 16) + (status[4] << 8) + (status[5]) }); } // tempo
                            break;
                    }
                }
            }

            mid.close();
        }

        #endregion

        #region transpose functions

        public void transpose(int trans)
        {
            switch (this.format)
            {
                case 0x00: transposeMls(trans); break;
                default: MessageBox.Show("Don't do transpose stuff on this format"); break;
            }
        }

        private void transposeMls(int trans)
        {
            bool testResult = true;

            // test it first
            for (int i = 0; i < events[activeTrack].Count; i++ )
            {
                if (events[activeTrack][i][0] > 0 && events[activeTrack][i][0] < 0xF0)
                {
                    int test = events[activeTrack][i][1];
                    test = test + trans;
                    if (test <= 0 || test >= 0xF0)
                    {
                        testResult = false;
                        break;
                    }
                }
            }

            if (testResult)
            {
                for (int i = 0; i < events[activeTrack].Count; i++)
                {
                    if (events[activeTrack][i][0] > 0 && events[activeTrack][i][0] < 0xF0)
                    {
                        int test = events[activeTrack][i][1];
                        events[activeTrack][i][1] = test + trans;
                    }
                }
            }
            else MessageBox.Show("Cannot transpose this high or this low");
        }

        #endregion
    }
}
