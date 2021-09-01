namespace perSONA
{
    partial class twoSperakerCalibration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(twoSperakerCalibration));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.VolumeLabelAdjusting = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.Fail = new System.Windows.Forms.Button();
            this.Sound = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.calibrated = new System.Windows.Forms.CheckBox();
            this.speakerLabel = new System.Windows.Forms.Label();
            this.Help = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Next = new System.Windows.Forms.Button();
            this.SLM_Microphone = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SLM_Microphone)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
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
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // VolumeLabelAdjusting
            // 
            this.VolumeLabelAdjusting.AutoSize = true;
            this.VolumeLabelAdjusting.Location = new System.Drawing.Point(458, 188);
            this.VolumeLabelAdjusting.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VolumeLabelAdjusting.Name = "VolumeLabelAdjusting";
            this.VolumeLabelAdjusting.Size = new System.Drawing.Size(325, 20);
            this.VolumeLabelAdjusting.TabIndex = 124;
            this.VolumeLabelAdjusting.Text = "Altere o volume utilizando a placa de som ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 240);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 20);
            this.label3.TabIndex = 119;
            this.label3.Text = "Status da calibração:";
            // 
            // volumeLabel
            // 
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.Location = new System.Drawing.Point(668, 188);
            this.volumeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(102, 20);
            this.volumeLabel.TabIndex = 123;
            this.volumeLabel.Text = "Volume: 100";
            // 
            // volumeBar
            // 
            this.volumeBar.Location = new System.Drawing.Point(425, 180);
            this.volumeBar.Margin = new System.Windows.Forms.Padding(4);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Minimum = 1;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.volumeBar.Size = new System.Drawing.Size(235, 56);
            this.volumeBar.TabIndex = 122;
            this.volumeBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.volumeBar.Value = 1;
            this.volumeBar.Scroll += new System.EventHandler(this.volumeBar_Scroll);
            // 
            // Fail
            // 
            this.Fail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(64)))), ((int)(((byte)(137)))));
            this.Fail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Fail.ForeColor = System.Drawing.Color.White;
            this.Fail.Location = new System.Drawing.Point(425, 282);
            this.Fail.Margin = new System.Windows.Forms.Padding(4);
            this.Fail.Name = "Fail";
            this.Fail.Size = new System.Drawing.Size(188, 53);
            this.Fail.TabIndex = 121;
            this.Fail.Text = "Falha";
            this.Fail.UseVisualStyleBackColor = false;
            this.Fail.Click += new System.EventHandler(this.Fail_Click);
            // 
            // Sound
            // 
            this.Sound.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(64)))), ((int)(((byte)(137)))));
            this.Sound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Sound.ForeColor = System.Drawing.Color.White;
            this.Sound.Location = new System.Drawing.Point(425, 109);
            this.Sound.Margin = new System.Windows.Forms.Padding(5);
            this.Sound.Name = "Sound";
            this.Sound.Size = new System.Drawing.Size(388, 53);
            this.Sound.TabIndex = 120;
            this.Sound.Text = "Gerar sinal sonoro";
            this.Sound.UseVisualStyleBackColor = false;
            this.Sound.Click += new System.EventHandler(this.Sound_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(426, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 20);
            this.label2.TabIndex = 118;
            this.label2.Text = "Reprodutor sonoro:";
            // 
            // calibrated
            // 
            this.calibrated.AutoSize = true;
            this.calibrated.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.calibrated.Location = new System.Drawing.Point(672, 240);
            this.calibrated.Margin = new System.Windows.Forms.Padding(5);
            this.calibrated.Name = "calibrated";
            this.calibrated.Size = new System.Drawing.Size(111, 24);
            this.calibrated.TabIndex = 117;
            this.calibrated.Text = "Calibrada";
            this.calibrated.UseVisualStyleBackColor = true;
            this.calibrated.CheckedChanged += new System.EventHandler(this.calibrated_CheckedChanged);
            // 
            // speakerLabel
            // 
            this.speakerLabel.AutoSize = true;
            this.speakerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.speakerLabel.Location = new System.Drawing.Point(668, 72);
            this.speakerLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.speakerLabel.Name = "speakerLabel";
            this.speakerLabel.Size = new System.Drawing.Size(115, 20);
            this.speakerLabel.TabIndex = 116;
            this.speakerLabel.Text = "Caixa direita";
            // 
            // Help
            // 
            this.Help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(64)))), ((int)(((byte)(137)))));
            this.Help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Help.ForeColor = System.Drawing.Color.White;
            this.Help.Location = new System.Drawing.Point(425, 343);
            this.Help.Margin = new System.Windows.Forms.Padding(5);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(388, 53);
            this.Help.TabIndex = 114;
            this.Help.Text = "Ajuda";
            this.Help.UseVisualStyleBackColor = false;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(426, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(369, 40);
            this.label1.TabIndex = 115;
            this.label1.Text = "Para calibrar altere o volume das caixas de som\r\nseparadamente";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Next
            // 
            this.Next.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(64)))), ((int)(((byte)(137)))));
            this.Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Next.ForeColor = System.Drawing.Color.White;
            this.Next.Location = new System.Drawing.Point(625, 282);
            this.Next.Margin = new System.Windows.Forms.Padding(5);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(188, 53);
            this.Next.TabIndex = 113;
            this.Next.Text = "Próxima caixa";
            this.Next.UseVisualStyleBackColor = false;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // SLM_Microphone
            // 
            this.SLM_Microphone.ErrorImage = ((System.Drawing.Image)(resources.GetObject("SLM_Microphone.ErrorImage")));
            this.SLM_Microphone.Image = ((System.Drawing.Image)(resources.GetObject("SLM_Microphone.Image")));
            this.SLM_Microphone.InitialImage = ((System.Drawing.Image)(resources.GetObject("SLM_Microphone.InitialImage")));
            this.SLM_Microphone.Location = new System.Drawing.Point(167, 149);
            this.SLM_Microphone.Margin = new System.Windows.Forms.Padding(5);
            this.SLM_Microphone.Name = "SLM_Microphone";
            this.SLM_Microphone.Size = new System.Drawing.Size(73, 178);
            this.SLM_Microphone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SLM_Microphone.TabIndex = 125;
            this.SLM_Microphone.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Location = new System.Drawing.Point(299, 171);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(81, 75);
            this.panel1.TabIndex = 126;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(22, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(81, 75);
            this.panel2.TabIndex = 127;
            // 
            // twoSperakerCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(827, 408);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SLM_Microphone);
            this.Controls.Add(this.VolumeLabelAdjusting);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.volumeLabel);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.Fail);
            this.Controls.Add(this.Sound);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.calibrated);
            this.Controls.Add(this.speakerLabel);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Next);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "twoSperakerCalibration";
            this.Text = "twoSperakerCalibration";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SLM_Microphone)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label VolumeLabelAdjusting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Button Fail;
        private System.Windows.Forms.Button Sound;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox calibrated;
        private System.Windows.Forms.Label speakerLabel;
        private System.Windows.Forms.Button Help;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.PictureBox SLM_Microphone;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}