namespace mlconverter2
{
    partial class RomViewer
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
			this.songListBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// songListBox
			// 
			this.songListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.songListBox.FormattingEnabled = true;
			this.songListBox.IntegralHeight = false;
			this.songListBox.Location = new System.Drawing.Point(0, 0);
			this.songListBox.Name = "songListBox";
			this.songListBox.Size = new System.Drawing.Size(264, 261);
			this.songListBox.TabIndex = 0;
			this.songListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.songListBox_MouseDoubleClick);
			// 
			// RomViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(264, 261);
			this.Controls.Add(this.songListBox);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(320, 4000);
			this.MinimumSize = new System.Drawing.Size(280, 300);
			this.Name = "RomViewer";
			this.ShowIcon = false;
			this.Text = "Select a Music Sequence";
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox songListBox;
    }
}