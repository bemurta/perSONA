namespace perSONA
{
    partial class earphoneCalibration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(earphoneCalibration));
            this.Fail = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Sound = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Next = new System.Windows.Forms.Button();
            this.calibrated = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.earphoneLabel = new System.Windows.Forms.Label();
            this.Help = new System.Windows.Forms.Button();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.PhoneBalanceAdjusting = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Fail
            // 
            this.Fail.Location = new System.Drawing.Point(424, 282);
            this.Fail.Name = "Fail";
            this.Fail.Size = new System.Drawing.Size(188, 53);
            this.Fail.TabIndex = 104;
            this.Fail.Text = "Falha";
            this.Fail.UseVisualStyleBackColor = true;
            this.Fail.Click += new System.EventHandler(this.Fail_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox7);
            this.panel1.Location = new System.Drawing.Point(13, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 324);
            this.panel1.TabIndex = 95;
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
            this.pictureBox7.Size = new System.Drawing.Size(180, 315);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 83;
            this.pictureBox7.TabStop = false;
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
            this.pictureBox1.Size = new System.Drawing.Size(180, 315);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 84;
            this.pictureBox1.TabStop = false;
            // 
            // Sound
            // 
            this.Sound.Location = new System.Drawing.Point(424, 109);
            this.Sound.Margin = new System.Windows.Forms.Padding(4);
            this.Sound.Name = "Sound";
            this.Sound.Size = new System.Drawing.Size(388, 53);
            this.Sound.TabIndex = 103;
            this.Sound.Text = "Gerar sinal sonoro";
            this.Sound.UseVisualStyleBackColor = true;
            this.Sound.Click += new System.EventHandler(this.Sound_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 240);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 20);
            this.label3.TabIndex = 102;
            this.label3.Text = "Status da calibração:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(228, 42);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(188, 324);
            this.panel2.TabIndex = 94;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(424, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 20);
            this.label2.TabIndex = 101;
            this.label2.Text = "Reprodutor sonoro:";
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(624, 282);
            this.Next.Margin = new System.Windows.Forms.Padding(4);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(188, 53);
            this.Next.TabIndex = 96;
            this.Next.Text = "Próximo fone";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // calibrated
            // 
            this.calibrated.AutoSize = true;
            this.calibrated.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.calibrated.Location = new System.Drawing.Point(668, 239);
            this.calibrated.Margin = new System.Windows.Forms.Padding(4);
            this.calibrated.Name = "calibrated";
            this.calibrated.Size = new System.Drawing.Size(111, 24);
            this.calibrated.TabIndex = 100;
            this.calibrated.Text = "Calibrado";
            this.calibrated.UseVisualStyleBackColor = true;
            this.calibrated.CheckedChanged += new System.EventHandler(this.calibrated_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(424, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "Para calibrar ajuste o balanço do fone de ouvido";
            // 
            // earphoneLabel
            // 
            this.earphoneLabel.AutoSize = true;
            this.earphoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.earphoneLabel.Location = new System.Drawing.Point(666, 72);
            this.earphoneLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.earphoneLabel.Name = "earphoneLabel";
            this.earphoneLabel.Size = new System.Drawing.Size(109, 20);
            this.earphoneLabel.TabIndex = 99;
            this.earphoneLabel.Text = "Lado direito";
            // 
            // Help
            // 
            this.Help.Location = new System.Drawing.Point(424, 342);
            this.Help.Margin = new System.Windows.Forms.Padding(4);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(388, 53);
            this.Help.TabIndex = 97;
            this.Help.Text = "Ajuda";
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // volumeBar
            // 
            this.volumeBar.Location = new System.Drawing.Point(422, 180);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Minimum = 1;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.volumeBar.Size = new System.Drawing.Size(188, 56);
            this.volumeBar.TabIndex = 1;
            this.volumeBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.volumeBar.Value = 1;
            this.volumeBar.Scroll += new System.EventHandler(this.volumeBar_Scroll);
            // 
            // volumeLabel
            // 
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.Location = new System.Drawing.Point(666, 188);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(102, 20);
            this.volumeLabel.TabIndex = 105;
            this.volumeLabel.Text = "Volume: 100";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.InitialImage")));
            this.pictureBox2.Location = new System.Drawing.Point(101, 141);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(224, 304);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 106;
            this.pictureBox2.TabStop = false;
            // 
            // PhoneBalanceAdjusting
            // 
            this.PhoneBalanceAdjusting.Location = new System.Drawing.Point(424, 172);
            this.PhoneBalanceAdjusting.Name = "PhoneBalanceAdjusting";
            this.PhoneBalanceAdjusting.Size = new System.Drawing.Size(388, 53);
            this.PhoneBalanceAdjusting.TabIndex = 107;
            this.PhoneBalanceAdjusting.Text = "Como ajustar o balanço dos fones de ouvido?";
            this.PhoneBalanceAdjusting.UseVisualStyleBackColor = true;
            this.PhoneBalanceAdjusting.Click += new System.EventHandler(this.PhoneBalanceAdjusting_Click);
            // 
            // earphoneCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 408);
            this.Controls.Add(this.PhoneBalanceAdjusting);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.calibrated);
            this.Controls.Add(this.volumeLabel);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.Fail);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Sound);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.earphoneLabel);
            this.Controls.Add(this.Help);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(845, 455);
            this.MinimumSize = new System.Drawing.Size(845, 455);
            this.Name = "earphoneCalibration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calibração dos Fones de Ouvido";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Fail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Button Sound;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.CheckBox calibrated;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label earphoneLabel;
        private System.Windows.Forms.Button Help;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button PhoneBalanceAdjusting;
    }
}