namespace perSONA
{
    partial class speakerCalibration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(speakerCalibration));
            this.SLM_Microphone = new System.Windows.Forms.PictureBox();
            this.Fail = new System.Windows.Forms.Button();
            this.Sound = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.calibrated = new System.Windows.Forms.CheckBox();
            this.speakerLabel = new System.Windows.Forms.Label();
            this.Help = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Next = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.SLM_Microphone)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SLM_Microphone
            // 
            this.SLM_Microphone.ErrorImage = ((System.Drawing.Image)(resources.GetObject("SLM_Microphone.ErrorImage")));
            this.SLM_Microphone.Image = ((System.Drawing.Image)(resources.GetObject("SLM_Microphone.Image")));
            this.SLM_Microphone.InitialImage = ((System.Drawing.Image)(resources.GetObject("SLM_Microphone.InitialImage")));
            this.SLM_Microphone.Location = new System.Drawing.Point(167, 149);
            this.SLM_Microphone.Margin = new System.Windows.Forms.Padding(4);
            this.SLM_Microphone.Name = "SLM_Microphone";
            this.SLM_Microphone.Size = new System.Drawing.Size(73, 178);
            this.SLM_Microphone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SLM_Microphone.TabIndex = 92;
            this.SLM_Microphone.TabStop = false;
            // 
            // Fail
            // 
            this.Fail.Location = new System.Drawing.Point(625, 319);
            this.Fail.Name = "Fail";
            this.Fail.Size = new System.Drawing.Size(188, 35);
            this.Fail.TabIndex = 109;
            this.Fail.Text = "Falha";
            this.Fail.UseVisualStyleBackColor = true;
            this.Fail.Click += new System.EventHandler(this.Fail_Click);
            // 
            // Sound
            // 
            this.Sound.Location = new System.Drawing.Point(425, 163);
            this.Sound.Margin = new System.Windows.Forms.Padding(4);
            this.Sound.Name = "Sound";
            this.Sound.Size = new System.Drawing.Size(388, 70);
            this.Sound.TabIndex = 108;
            this.Sound.Text = "Gerar Sinal Sonoro";
            this.Sound.UseVisualStyleBackColor = true;
            this.Sound.Click += new System.EventHandler(this.Sound_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 20);
            this.label3.TabIndex = 107;
            this.label3.Text = "Status da calibração:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(426, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 20);
            this.label2.TabIndex = 106;
            this.label2.Text = "Reprodutor sonoro:";
            // 
            // calibrated
            // 
            this.calibrated.AutoSize = true;
            this.calibrated.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.calibrated.Location = new System.Drawing.Point(654, 117);
            this.calibrated.Margin = new System.Windows.Forms.Padding(4);
            this.calibrated.Name = "calibrated";
            this.calibrated.Size = new System.Drawing.Size(111, 24);
            this.calibrated.TabIndex = 105;
            this.calibrated.Text = "Calibrada";
            this.calibrated.UseVisualStyleBackColor = true;
            this.calibrated.CheckedChanged += new System.EventHandler(this.calibrated_CheckedChanged);
            // 
            // speakerLabel
            // 
            this.speakerLabel.AutoSize = true;
            this.speakerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.speakerLabel.Location = new System.Drawing.Point(650, 73);
            this.speakerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.speakerLabel.Name = "speakerLabel";
            this.speakerLabel.Size = new System.Drawing.Size(110, 20);
            this.speakerLabel.TabIndex = 104;
            this.speakerLabel.Text = "Caixa 1 (0°)";
            // 
            // Help
            // 
            this.Help.Location = new System.Drawing.Point(425, 319);
            this.Help.Margin = new System.Windows.Forms.Padding(4);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(188, 35);
            this.Help.TabIndex = 102;
            this.Help.Text = "Ajuda";
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(426, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 40);
            this.label1.TabIndex = 103;
            this.label1.Text = "       Altere o volume dos repdutores sonoros        \r\nseparadamente através da p" +
    "laca de som";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(425, 241);
            this.Next.Margin = new System.Windows.Forms.Padding(4);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(388, 70);
            this.Next.TabIndex = 101;
            this.Next.Text = "Próxima Caixa";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.pictureBox7);
            this.panel8.Location = new System.Drawing.Point(68, 90);
            this.panel8.Margin = new System.Windows.Forms.Padding(4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(81, 75);
            this.panel8.TabIndex = 100;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox7.ErrorImage")));
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox7.InitialImage")));
            this.pictureBox7.Location = new System.Drawing.Point(4, 4);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(73, 67);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 2;
            this.pictureBox7.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pictureBox6);
            this.panel7.Location = new System.Drawing.Point(22, 171);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(81, 75);
            this.panel7.TabIndex = 99;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox6.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.ErrorImage")));
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.InitialImage")));
            this.pictureBox6.Location = new System.Drawing.Point(4, 4);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(73, 67);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 2;
            this.pictureBox6.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.pictureBox5);
            this.panel6.Location = new System.Drawing.Point(68, 252);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(81, 75);
            this.panel6.TabIndex = 98;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox5.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.ErrorImage")));
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.InitialImage")));
            this.pictureBox5.Location = new System.Drawing.Point(4, 4);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(73, 67);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 2;
            this.pictureBox5.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pictureBox4);
            this.panel5.Location = new System.Drawing.Point(163, 325);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(81, 75);
            this.panel5.TabIndex = 97;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.ErrorImage")));
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.InitialImage")));
            this.pictureBox4.Location = new System.Drawing.Point(4, 4);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(73, 67);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Location = new System.Drawing.Point(256, 252);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(81, 75);
            this.panel4.TabIndex = 94;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.InitialImage")));
            this.pictureBox2.Location = new System.Drawing.Point(4, 4);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(73, 67);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(299, 171);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(81, 75);
            this.panel3.TabIndex = 96;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox11);
            this.panel2.Location = new System.Drawing.Point(256, 90);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(81, 75);
            this.panel2.TabIndex = 95;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox11.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox11.ErrorImage")));
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox11.InitialImage")));
            this.pictureBox11.Location = new System.Drawing.Point(4, 4);
            this.pictureBox11.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(73, 67);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox11.TabIndex = 2;
            this.pictureBox11.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Location = new System.Drawing.Point(163, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(81, 75);
            this.panel1.TabIndex = 93;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.ErrorImage")));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
            this.pictureBox3.Location = new System.Drawing.Point(4, 4);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(73, 67);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // volumeLabel
            // 
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.Location = new System.Drawing.Point(668, 366);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(102, 20);
            this.volumeLabel.TabIndex = 111;
            this.volumeLabel.Text = "Volume: 100";
            // 
            // volumeBar
            // 
            this.volumeBar.Location = new System.Drawing.Point(425, 361);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Minimum = 1;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.volumeBar.Size = new System.Drawing.Size(188, 56);
            this.volumeBar.TabIndex = 110;
            this.volumeBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.volumeBar.Value = 1;
            this.volumeBar.Scroll += new System.EventHandler(this.volumeBar_Scroll);
            // 
            // speakerCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 408);
            this.Controls.Add(this.volumeLabel);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.SLM_Microphone);
            this.Controls.Add(this.Fail);
            this.Controls.Add(this.Sound);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.calibrated);
            this.Controls.Add(this.speakerLabel);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(845, 455);
            this.MinimumSize = new System.Drawing.Size(845, 455);
            this.Name = "speakerCalibration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calibração de Reprodutores Sonoros";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.SLM_Microphone)).EndInit();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox SLM_Microphone;
        private System.Windows.Forms.Button Fail;
        private System.Windows.Forms.Button Sound;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox calibrated;
        private System.Windows.Forms.Label speakerLabel;
        private System.Windows.Forms.Button Help;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.TrackBar volumeBar;
    }
}