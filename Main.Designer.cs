namespace mlconverter2
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mIDIToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mIDIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSequencesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllMidiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.status = new System.Windows.Forms.StatusStrip();
            this.seqLoadedlbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.scheiding = new System.Windows.Forms.ToolStripStatusLabel();
            this.romLoadedlbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.trackslbl = new System.Windows.Forms.Label();
            this.trackListbox = new System.Windows.Forms.ListBox();
            this.trackgbx = new System.Windows.Forms.GroupBox();
            this.updateEventbtn = new System.Windows.Forms.Button();
            this.parametersgbx = new System.Windows.Forms.GroupBox();
            this.pr2lbl = new System.Windows.Forms.Label();
            this.parameter2num = new System.Windows.Forms.NumericUpDown();
            this.parameter1num = new System.Windows.Forms.NumericUpDown();
            this.pr1lbl = new System.Windows.Forms.Label();
            this.sortEventgbx = new System.Windows.Forms.GroupBox();
            this.valuenum = new System.Windows.Forms.NumericUpDown();
            this.Valuelbl = new System.Windows.Forms.Label();
            this.eventcmb = new System.Windows.Forms.ComboBox();
            this.removeEventbtn = new System.Windows.Forms.Button();
            this.addEventbtn = new System.Windows.Forms.Button();
            this.eventListbox = new System.Windows.Forms.ListBox();
            this.eventListlbl = new System.Windows.Forms.Label();
            this.soundFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpAllSamplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.status.SuspendLayout();
            this.trackgbx.SuspendLayout();
            this.parametersgbx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parameter2num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parameter1num)).BeginInit();
            this.sortEventgbx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valuenum)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.rOMToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(581, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.saveFileAsToolStripMenuItem,
            this.openRomToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Enabled = false;
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // saveFileAsToolStripMenuItem
            // 
            this.saveFileAsToolStripMenuItem.Enabled = false;
            this.saveFileAsToolStripMenuItem.Name = "saveFileAsToolStripMenuItem";
            this.saveFileAsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.saveFileAsToolStripMenuItem.Text = "Save File as...";
            this.saveFileAsToolStripMenuItem.Click += new System.EventHandler(this.saveFileAsToolStripMenuItem_Click);
            // 
            // openRomToolStripMenuItem
            // 
            this.openRomToolStripMenuItem.Name = "openRomToolStripMenuItem";
            this.openRomToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.openRomToolStripMenuItem.Text = "Open Rom";
            this.openRomToolStripMenuItem.Click += new System.EventHandler(this.openRomToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mIDIToolStripMenuItem1});
            this.importToolStripMenuItem.Enabled = false;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // mIDIToolStripMenuItem1
            // 
            this.mIDIToolStripMenuItem1.Name = "mIDIToolStripMenuItem1";
            this.mIDIToolStripMenuItem1.Size = new System.Drawing.Size(99, 22);
            this.mIDIToolStripMenuItem1.Text = "MIDI";
            this.mIDIToolStripMenuItem1.Click += new System.EventHandler(this.mIDIToolStripMenuItem1_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mIDIToolStripMenuItem});
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // mIDIToolStripMenuItem
            // 
            this.mIDIToolStripMenuItem.Name = "mIDIToolStripMenuItem";
            this.mIDIToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.mIDIToolStripMenuItem.Text = "MIDI";
            this.mIDIToolStripMenuItem.Click += new System.EventHandler(this.mIDIToolStripMenuItem_Click);
            // 
            // rOMToolStripMenuItem
            // 
            this.rOMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSequencesListToolStripMenuItem,
            this.exportAllMidiToolStripMenuItem,
            this.soundFontToolStripMenuItem});
            this.rOMToolStripMenuItem.Enabled = false;
            this.rOMToolStripMenuItem.Name = "rOMToolStripMenuItem";
            this.rOMToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.rOMToolStripMenuItem.Text = "ROM";
            // 
            // openSequencesListToolStripMenuItem
            // 
            this.openSequencesListToolStripMenuItem.Name = "openSequencesListToolStripMenuItem";
            this.openSequencesListToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.openSequencesListToolStripMenuItem.Text = "Open Seq List";
            this.openSequencesListToolStripMenuItem.Click += new System.EventHandler(this.openSequencesListToolStripMenuItem_Click);
            // 
            // exportAllMidiToolStripMenuItem
            // 
            this.exportAllMidiToolStripMenuItem.Name = "exportAllMidiToolStripMenuItem";
            this.exportAllMidiToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exportAllMidiToolStripMenuItem.Text = "Export All To Midi";
            this.exportAllMidiToolStripMenuItem.Click += new System.EventHandler(this.exportAllMidiToolStripMenuItem_Click);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seqLoadedlbl,
            this.scheiding,
            this.romLoadedlbl});
            this.status.Location = new System.Drawing.Point(0, 327);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(581, 22);
            this.status.TabIndex = 1;
            this.status.Text = "statusStrip1";
            // 
            // seqLoadedlbl
            // 
            this.seqLoadedlbl.Name = "seqLoadedlbl";
            this.seqLoadedlbl.Size = new System.Drawing.Size(113, 17);
            this.seqLoadedlbl.Text = "no sequence loaded";
            // 
            // scheiding
            // 
            this.scheiding.Name = "scheiding";
            this.scheiding.Size = new System.Drawing.Size(10, 17);
            this.scheiding.Text = "|";
            // 
            // romLoadedlbl
            // 
            this.romLoadedlbl.Name = "romLoadedlbl";
            this.romLoadedlbl.Size = new System.Drawing.Size(90, 17);
            this.romLoadedlbl.Text = "no ROM loaded";
            // 
            // trackslbl
            // 
            this.trackslbl.AutoSize = true;
            this.trackslbl.Location = new System.Drawing.Point(12, 27);
            this.trackslbl.Name = "trackslbl";
            this.trackslbl.Size = new System.Drawing.Size(43, 13);
            this.trackslbl.TabIndex = 2;
            this.trackslbl.Text = "Tracks:";
            // 
            // trackListbox
            // 
            this.trackListbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.trackListbox.FormattingEnabled = true;
            this.trackListbox.Location = new System.Drawing.Point(14, 44);
            this.trackListbox.Name = "trackListbox";
            this.trackListbox.Size = new System.Drawing.Size(120, 264);
            this.trackListbox.TabIndex = 3;
            this.trackListbox.SelectedIndexChanged += new System.EventHandler(this.trackListbox_SelectedIndexChanged);
            // 
            // trackgbx
            // 
            this.trackgbx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackgbx.Controls.Add(this.updateEventbtn);
            this.trackgbx.Controls.Add(this.parametersgbx);
            this.trackgbx.Controls.Add(this.sortEventgbx);
            this.trackgbx.Controls.Add(this.removeEventbtn);
            this.trackgbx.Controls.Add(this.addEventbtn);
            this.trackgbx.Controls.Add(this.eventListbox);
            this.trackgbx.Controls.Add(this.eventListlbl);
            this.trackgbx.Location = new System.Drawing.Point(140, 27);
            this.trackgbx.Name = "trackgbx";
            this.trackgbx.Size = new System.Drawing.Size(429, 295);
            this.trackgbx.TabIndex = 4;
            this.trackgbx.TabStop = false;
            this.trackgbx.Text = "track";
            // 
            // updateEventbtn
            // 
            this.updateEventbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateEventbtn.Location = new System.Drawing.Point(267, 33);
            this.updateEventbtn.Name = "updateEventbtn";
            this.updateEventbtn.Size = new System.Drawing.Size(153, 23);
            this.updateEventbtn.TabIndex = 6;
            this.updateEventbtn.Text = "Update Event";
            this.updateEventbtn.UseVisualStyleBackColor = true;
            this.updateEventbtn.Click += new System.EventHandler(this.updateEventbtn_Click);
            // 
            // parametersgbx
            // 
            this.parametersgbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.parametersgbx.Controls.Add(this.pr2lbl);
            this.parametersgbx.Controls.Add(this.parameter2num);
            this.parametersgbx.Controls.Add(this.parameter1num);
            this.parametersgbx.Controls.Add(this.pr1lbl);
            this.parametersgbx.Location = new System.Drawing.Point(266, 204);
            this.parametersgbx.Name = "parametersgbx";
            this.parametersgbx.Size = new System.Drawing.Size(154, 80);
            this.parametersgbx.TabIndex = 5;
            this.parametersgbx.TabStop = false;
            this.parametersgbx.Text = "Parameters";
            // 
            // pr2lbl
            // 
            this.pr2lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pr2lbl.AutoSize = true;
            this.pr2lbl.Location = new System.Drawing.Point(6, 47);
            this.pr2lbl.Name = "pr2lbl";
            this.pr2lbl.Size = new System.Drawing.Size(46, 13);
            this.pr2lbl.TabIndex = 3;
            this.pr2lbl.Text = "Param 2";
            // 
            // parameter2num
            // 
            this.parameter2num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.parameter2num.Hexadecimal = true;
            this.parameter2num.Location = new System.Drawing.Point(59, 45);
            this.parameter2num.Name = "parameter2num";
            this.parameter2num.Size = new System.Drawing.Size(89, 20);
            this.parameter2num.TabIndex = 2;
            // 
            // parameter1num
            // 
            this.parameter1num.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.parameter1num.Hexadecimal = true;
            this.parameter1num.Location = new System.Drawing.Point(59, 18);
            this.parameter1num.Name = "parameter1num";
            this.parameter1num.Size = new System.Drawing.Size(89, 20);
            this.parameter1num.TabIndex = 1;
            // 
            // pr1lbl
            // 
            this.pr1lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pr1lbl.AutoSize = true;
            this.pr1lbl.Location = new System.Drawing.Point(7, 20);
            this.pr1lbl.Name = "pr1lbl";
            this.pr1lbl.Size = new System.Drawing.Size(46, 13);
            this.pr1lbl.TabIndex = 0;
            this.pr1lbl.Text = "Param 1";
            // 
            // sortEventgbx
            // 
            this.sortEventgbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sortEventgbx.Controls.Add(this.valuenum);
            this.sortEventgbx.Controls.Add(this.Valuelbl);
            this.sortEventgbx.Controls.Add(this.eventcmb);
            this.sortEventgbx.Location = new System.Drawing.Point(266, 122);
            this.sortEventgbx.Name = "sortEventgbx";
            this.sortEventgbx.Size = new System.Drawing.Size(154, 76);
            this.sortEventgbx.TabIndex = 4;
            this.sortEventgbx.TabStop = false;
            this.sortEventgbx.Text = "Event";
            // 
            // valuenum
            // 
            this.valuenum.Hexadecimal = true;
            this.valuenum.Location = new System.Drawing.Point(59, 48);
            this.valuenum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.valuenum.Name = "valuenum";
            this.valuenum.Size = new System.Drawing.Size(89, 20);
            this.valuenum.TabIndex = 2;
            // 
            // Valuelbl
            // 
            this.Valuelbl.AutoSize = true;
            this.Valuelbl.Location = new System.Drawing.Point(7, 48);
            this.Valuelbl.Name = "Valuelbl";
            this.Valuelbl.Size = new System.Drawing.Size(34, 13);
            this.Valuelbl.TabIndex = 1;
            this.Valuelbl.Text = "Value";
            // 
            // eventcmb
            // 
            this.eventcmb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eventcmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventcmb.FormattingEnabled = true;
            this.eventcmb.Location = new System.Drawing.Point(7, 20);
            this.eventcmb.Name = "eventcmb";
            this.eventcmb.Size = new System.Drawing.Size(141, 21);
            this.eventcmb.TabIndex = 0;
            this.eventcmb.SelectedIndexChanged += new System.EventHandler(this.eventcmb_SelectedIndexChanged);
            // 
            // removeEventbtn
            // 
            this.removeEventbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeEventbtn.Location = new System.Drawing.Point(266, 92);
            this.removeEventbtn.Name = "removeEventbtn";
            this.removeEventbtn.Size = new System.Drawing.Size(154, 23);
            this.removeEventbtn.TabIndex = 3;
            this.removeEventbtn.Text = "Remove Event";
            this.removeEventbtn.UseVisualStyleBackColor = true;
            this.removeEventbtn.Click += new System.EventHandler(this.removeEventbtn_Click);
            // 
            // addEventbtn
            // 
            this.addEventbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addEventbtn.Location = new System.Drawing.Point(266, 62);
            this.addEventbtn.Name = "addEventbtn";
            this.addEventbtn.Size = new System.Drawing.Size(154, 23);
            this.addEventbtn.TabIndex = 2;
            this.addEventbtn.Text = "Add Event";
            this.addEventbtn.UseVisualStyleBackColor = true;
            this.addEventbtn.Click += new System.EventHandler(this.addEventbtn_Click);
            // 
            // eventListbox
            // 
            this.eventListbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eventListbox.FormattingEnabled = true;
            this.eventListbox.Location = new System.Drawing.Point(9, 33);
            this.eventListbox.Name = "eventListbox";
            this.eventListbox.Size = new System.Drawing.Size(251, 251);
            this.eventListbox.TabIndex = 1;
            this.eventListbox.SelectedIndexChanged += new System.EventHandler(this.eventListbox_SelectedIndexChanged);
            // 
            // eventListlbl
            // 
            this.eventListlbl.AutoSize = true;
            this.eventListlbl.Location = new System.Drawing.Point(6, 16);
            this.eventListlbl.Name = "eventListlbl";
            this.eventListlbl.Size = new System.Drawing.Size(57, 13);
            this.eventListlbl.TabIndex = 0;
            this.eventListlbl.Text = "Event List:";
            // 
            // soundFontToolStripMenuItem
            // 
            this.soundFontToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dumpAllSamplesToolStripMenuItem});
            this.soundFontToolStripMenuItem.Name = "soundFontToolStripMenuItem";
            this.soundFontToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.soundFontToolStripMenuItem.Text = "SoundFont";
            // 
            // dumpAllSamplesToolStripMenuItem
            // 
            this.dumpAllSamplesToolStripMenuItem.Name = "dumpAllSamplesToolStripMenuItem";
            this.dumpAllSamplesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.dumpAllSamplesToolStripMenuItem.Text = "Dump All Samples";
            this.dumpAllSamplesToolStripMenuItem.Click += new System.EventHandler(this.dumpAllSamplesToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 349);
            this.Controls.Add(this.trackgbx);
            this.Controls.Add(this.trackListbox);
            this.Controls.Add(this.trackslbl);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "Main";
            this.Text = "Form1";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.trackgbx.ResumeLayout(false);
            this.trackgbx.PerformLayout();
            this.parametersgbx.ResumeLayout(false);
            this.parametersgbx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parameter2num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parameter1num)).EndInit();
            this.sortEventgbx.ResumeLayout(false);
            this.sortEventgbx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valuenum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mIDIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mIDIToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel seqLoadedlbl;
        private System.Windows.Forms.ToolStripStatusLabel scheiding;
        private System.Windows.Forms.ToolStripStatusLabel romLoadedlbl;
        private System.Windows.Forms.Label trackslbl;
        private System.Windows.Forms.ListBox trackListbox;
        private System.Windows.Forms.GroupBox trackgbx;
        private System.Windows.Forms.ListBox eventListbox;
        private System.Windows.Forms.Label eventListlbl;
        private System.Windows.Forms.GroupBox parametersgbx;
        private System.Windows.Forms.Label pr2lbl;
        private System.Windows.Forms.NumericUpDown parameter2num;
        private System.Windows.Forms.NumericUpDown parameter1num;
        private System.Windows.Forms.Label pr1lbl;
        private System.Windows.Forms.GroupBox sortEventgbx;
        private System.Windows.Forms.ComboBox eventcmb;
        private System.Windows.Forms.Button removeEventbtn;
        private System.Windows.Forms.Button addEventbtn;
        private System.Windows.Forms.Button updateEventbtn;
        private System.Windows.Forms.NumericUpDown valuenum;
        private System.Windows.Forms.Label Valuelbl;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSequencesListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllMidiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpAllSamplesToolStripMenuItem;
    }
}

