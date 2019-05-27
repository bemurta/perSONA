namespace perSONA
{
    partial class testSetup
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.stepSnr = new System.Windows.Forms.NumericUpDown();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.numericRule = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.initialSnr = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.noiseDistance = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.speechDistance = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.noiseLeft = new System.Windows.Forms.RadioButton();
            this.noiseRight = new System.Windows.Forms.RadioButton();
            this.noiseFront = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.speechLeft = new System.Windows.Forms.RadioButton();
            this.speechRight = new System.Windows.Forms.RadioButton();
            this.speechFront = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.getFolder = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepSnr)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericRule)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.initialSnr)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.noiseDistance)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speechDistance)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.zedGraphControl1);
            this.panel2.Controls.Add(this.groupBox10);
            this.panel2.Controls.Add(this.groupBox8);
            this.panel2.Controls.Add(this.groupBox6);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.listBox2);
            this.panel2.Controls.Add(this.getFolder);
            this.panel2.Controls.Add(this.comboBox3);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(28, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(533, 529);
            this.panel2.TabIndex = 41;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(14, 275);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(230, 227);
            this.zedGraphControl1.TabIndex = 46;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.stepSnr);
            this.groupBox10.Location = new System.Drawing.Point(287, 414);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(230, 44);
            this.groupBox10.TabIndex = 45;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Signal to Noise Ratio step";
            // 
            // stepSnr
            // 
            this.stepSnr.Location = new System.Drawing.Point(6, 18);
            this.stepSnr.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.stepSnr.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.stepSnr.Name = "stepSnr";
            this.stepSnr.Size = new System.Drawing.Size(218, 20);
            this.stepSnr.TabIndex = 0;
            this.stepSnr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stepSnr.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.numericRule);
            this.groupBox8.Location = new System.Drawing.Point(287, 304);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(230, 44);
            this.groupBox8.TabIndex = 44;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Acceptance rule percentage";
            // 
            // numericRule
            // 
            this.numericRule.Location = new System.Drawing.Point(6, 18);
            this.numericRule.Name = "numericRule";
            this.numericRule.Size = new System.Drawing.Size(218, 20);
            this.numericRule.TabIndex = 0;
            this.numericRule.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericRule.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericRule.ValueChanged += new System.EventHandler(this.numericRule_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.comboBox1);
            this.groupBox6.Location = new System.Drawing.Point(287, 243);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(230, 44);
            this.groupBox6.TabIndex = 44;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Procedure";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "2-down-1-up"});
            this.comboBox1.Location = new System.Drawing.Point(6, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(218, 21);
            this.comboBox1.TabIndex = 46;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.initialSnr);
            this.groupBox5.Location = new System.Drawing.Point(287, 359);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(230, 44);
            this.groupBox5.TabIndex = 43;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Initial Signal to Noise Ratio (40 = no noise)";
            // 
            // initialSnr
            // 
            this.initialSnr.Location = new System.Drawing.Point(6, 18);
            this.initialSnr.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.initialSnr.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            -2147483648});
            this.initialSnr.Name = "initialSnr";
            this.initialSnr.Size = new System.Drawing.Size(218, 20);
            this.initialSnr.TabIndex = 0;
            this.initialSnr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.noiseDistance);
            this.groupBox4.Location = new System.Drawing.Point(287, 187);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(230, 44);
            this.groupBox4.TabIndex = 42;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Noise distance";
            // 
            // noiseDistance
            // 
            this.noiseDistance.DecimalPlaces = 1;
            this.noiseDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.noiseDistance.Location = new System.Drawing.Point(9, 18);
            this.noiseDistance.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.noiseDistance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.noiseDistance.Name = "noiseDistance";
            this.noiseDistance.Size = new System.Drawing.Size(215, 20);
            this.noiseDistance.TabIndex = 1;
            this.noiseDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.noiseDistance.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.noiseDistance.ValueChanged += new System.EventHandler(this.noiseDistance_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.speechDistance);
            this.groupBox3.Location = new System.Drawing.Point(287, 77);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 44);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Speech distance";
            // 
            // speechDistance
            // 
            this.speechDistance.DecimalPlaces = 1;
            this.speechDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.speechDistance.Location = new System.Drawing.Point(6, 18);
            this.speechDistance.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.speechDistance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speechDistance.Name = "speechDistance";
            this.speechDistance.Size = new System.Drawing.Size(218, 20);
            this.speechDistance.TabIndex = 0;
            this.speechDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.speechDistance.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.speechDistance.ValueChanged += new System.EventHandler(this.speechDistance_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.noiseLeft);
            this.groupBox2.Controls.Add(this.noiseRight);
            this.groupBox2.Controls.Add(this.noiseFront);
            this.groupBox2.Location = new System.Drawing.Point(287, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 44);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Noise souce direction";
            // 
            // noiseLeft
            // 
            this.noiseLeft.AutoSize = true;
            this.noiseLeft.Location = new System.Drawing.Point(6, 19);
            this.noiseLeft.Name = "noiseLeft";
            this.noiseLeft.Size = new System.Drawing.Size(43, 17);
            this.noiseLeft.TabIndex = 2;
            this.noiseLeft.Text = "Left";
            this.noiseLeft.UseVisualStyleBackColor = true;
            this.noiseLeft.CheckedChanged += new System.EventHandler(this.noiseLeft_CheckedChanged);
            // 
            // noiseRight
            // 
            this.noiseRight.AutoSize = true;
            this.noiseRight.Location = new System.Drawing.Point(174, 19);
            this.noiseRight.Name = "noiseRight";
            this.noiseRight.Size = new System.Drawing.Size(50, 17);
            this.noiseRight.TabIndex = 1;
            this.noiseRight.Text = "Right";
            this.noiseRight.UseVisualStyleBackColor = true;
            this.noiseRight.CheckedChanged += new System.EventHandler(this.noiseRight_CheckedChanged);
            // 
            // noiseFront
            // 
            this.noiseFront.AutoSize = true;
            this.noiseFront.Checked = true;
            this.noiseFront.Location = new System.Drawing.Point(87, 19);
            this.noiseFront.Name = "noiseFront";
            this.noiseFront.Size = new System.Drawing.Size(49, 17);
            this.noiseFront.TabIndex = 0;
            this.noiseFront.TabStop = true;
            this.noiseFront.Text = "Front";
            this.noiseFront.UseVisualStyleBackColor = true;
            this.noiseFront.CheckedChanged += new System.EventHandler(this.noiseFront_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.speechLeft);
            this.groupBox1.Controls.Add(this.speechRight);
            this.groupBox1.Controls.Add(this.speechFront);
            this.groupBox1.Location = new System.Drawing.Point(287, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 44);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Speech souce direction";
            // 
            // speechLeft
            // 
            this.speechLeft.AutoSize = true;
            this.speechLeft.Checked = true;
            this.speechLeft.Location = new System.Drawing.Point(6, 19);
            this.speechLeft.Name = "speechLeft";
            this.speechLeft.Size = new System.Drawing.Size(43, 17);
            this.speechLeft.TabIndex = 2;
            this.speechLeft.TabStop = true;
            this.speechLeft.Text = "Left";
            this.speechLeft.UseVisualStyleBackColor = true;
            this.speechLeft.CheckedChanged += new System.EventHandler(this.speechLeft_CheckedChanged);
            // 
            // speechRight
            // 
            this.speechRight.AutoSize = true;
            this.speechRight.Location = new System.Drawing.Point(174, 19);
            this.speechRight.Name = "speechRight";
            this.speechRight.Size = new System.Drawing.Size(50, 17);
            this.speechRight.TabIndex = 1;
            this.speechRight.Text = "Right";
            this.speechRight.UseVisualStyleBackColor = true;
            this.speechRight.CheckedChanged += new System.EventHandler(this.speechRight_CheckedChanged);
            // 
            // speechFront
            // 
            this.speechFront.AutoSize = true;
            this.speechFront.Location = new System.Drawing.Point(87, 19);
            this.speechFront.Name = "speechFront";
            this.speechFront.Size = new System.Drawing.Size(49, 17);
            this.speechFront.TabIndex = 0;
            this.speechFront.Text = "Front";
            this.speechFront.UseVisualStyleBackColor = true;
            this.speechFront.CheckedChanged += new System.EventHandler(this.speechFront_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(287, 469);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(230, 33);
            this.button4.TabIndex = 26;
            this.button4.Text = "Start test";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(14, 114);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(230, 108);
            this.listBox2.TabIndex = 21;
            // 
            // getFolder
            // 
            this.getFolder.Location = new System.Drawing.Point(14, 84);
            this.getFolder.Name = "getFolder";
            this.getFolder.Size = new System.Drawing.Size(230, 23);
            this.getFolder.TabIndex = 15;
            this.getFolder.Text = "Load test list";
            this.getFolder.UseVisualStyleBackColor = true;
            this.getFolder.Click += new System.EventHandler(this.getFolder_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(14, 245);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(230, 21);
            this.comboBox3.TabIndex = 29;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(230, 20);
            this.textBox1.TabIndex = 30;
            this.textBox1.Text = "Test one";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(85, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Speech test label";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(91, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Noise signal";
            // 
            // testSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 578);
            this.Controls.Add(this.panel2);
            this.Name = "testSetup";
            this.Text = "Form2";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stepSnr)).EndInit();
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericRule)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.initialSnr)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.noiseDistance)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.speechDistance)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown initialSnr;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown noiseDistance;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown speechDistance;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton noiseLeft;
        private System.Windows.Forms.RadioButton noiseRight;
        private System.Windows.Forms.RadioButton noiseFront;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton speechLeft;
        private System.Windows.Forms.RadioButton speechRight;
        private System.Windows.Forms.RadioButton speechFront;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button getFolder;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.NumericUpDown stepSnr;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.NumericUpDown numericRule;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox comboBox1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
    }
}