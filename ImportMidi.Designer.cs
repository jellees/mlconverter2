namespace mlconverter2
{
    partial class ImportMidi
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OKbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.normchk = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.loopchk = new System.Windows.Forms.CheckBox();
            this.loopForm = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Header Tracks";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Convert to Format";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(150, 13);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(137, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(150, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "(Usually 1)";
            // 
            // OKbtn
            // 
            this.OKbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbtn.Location = new System.Drawing.Point(12, 96);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(171, 23);
            this.OKbtn.TabIndex = 5;
            this.OKbtn.Text = "OK";
            this.OKbtn.UseVisualStyleBackColor = true;
            // 
            // cancelbtn
            // 
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(189, 96);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(160, 23);
            this.cancelbtn.TabIndex = 6;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // normchk
            // 
            this.normchk.AutoSize = true;
            this.normchk.Location = new System.Drawing.Point(15, 73);
            this.normchk.Name = "normchk";
            this.normchk.Size = new System.Drawing.Size(130, 17);
            this.normchk.TabIndex = 7;
            this.normchk.Text = "Normalisation of notes";
            this.toolTip1.SetToolTip(this.normchk, "Instead of ignoring less important notes,\r\nthis will cut off the overlapping note" +
                    "\r\nand start with the newer one.\r\n\r\n(This option may ruin everything)");
            this.normchk.UseVisualStyleBackColor = true;
            this.normchk.CheckedChanged += new System.EventHandler(this.normchk_CheckedChanged);
            // 
            // loopchk
            // 
            this.loopchk.AutoSize = true;
            this.loopchk.Location = new System.Drawing.Point(151, 73);
            this.loopchk.Name = "loopchk";
            this.loopchk.Size = new System.Drawing.Size(46, 17);
            this.loopchk.TabIndex = 8;
            this.loopchk.Text = "loop";
            this.loopchk.UseVisualStyleBackColor = true;
            this.loopchk.CheckedChanged += new System.EventHandler(this.loopchk_CheckedChanged);
            // 
            // loopForm
            // 
            this.loopForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loopForm.Enabled = false;
            this.loopForm.FormattingEnabled = true;
            this.loopForm.Items.AddRange(new object[] {
            "from start",
            "from loop point"});
            this.loopForm.Location = new System.Drawing.Point(203, 69);
            this.loopForm.Name = "loopForm";
            this.loopForm.Size = new System.Drawing.Size(146, 21);
            this.loopForm.TabIndex = 9;
            this.loopForm.SelectedIndexChanged += new System.EventHandler(this.loopForm_SelectedIndexChanged);
            // 
            // ImportMidi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(361, 128);
            this.Controls.Add(this.loopForm);
            this.Controls.Add(this.loopchk);
            this.Controls.Add(this.normchk);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.OKbtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ImportMidi";
            this.Text = "Import Midi";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button OKbtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.CheckBox normchk;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox loopchk;
        private System.Windows.Forms.ComboBox loopForm;
    }
}