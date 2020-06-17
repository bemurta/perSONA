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
            this.Sound = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Next = new System.Windows.Forms.Button();
            this.calibrated = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.earphoneLabel = new System.Windows.Forms.Label();
            this.Help = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Fail
            // 
            this.Fail.Location = new System.Drawing.Point(626, 363);
            this.Fail.Name = "Fail";
            this.Fail.Size = new System.Drawing.Size(188, 35);
            this.Fail.TabIndex = 104;
            this.Fail.Text = "Falha";
            this.Fail.UseVisualStyleBackColor = true;
            this.Fail.Click += new System.EventHandler(this.Fail_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(230, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 358);
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
            this.pictureBox7.Size = new System.Drawing.Size(180, 349);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 83;
            this.pictureBox7.TabStop = false;
            // 
            // Sound
            // 
            this.Sound.Location = new System.Drawing.Point(426, 207);
            this.Sound.Margin = new System.Windows.Forms.Padding(4);
            this.Sound.Name = "Sound";
            this.Sound.Size = new System.Drawing.Size(388, 70);
            this.Sound.TabIndex = 103;
            this.Sound.Text = "Gerar Sinal Sonoro";
            this.Sound.UseVisualStyleBackColor = true;
            this.Sound.Click += new System.EventHandler(this.Sound_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(427, 146);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 20);
            this.label3.TabIndex = 102;
            this.label3.Text = "Status da calibração:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox7);
            this.panel2.Location = new System.Drawing.Point(12, 27);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(188, 358);
            this.panel2.TabIndex = 94;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(182, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 84;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(427, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 20);
            this.label2.TabIndex = 101;
            this.label2.Text = "Caixa sendo calibrada:";
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(426, 285);
            this.Next.Margin = new System.Windows.Forms.Padding(4);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(388, 70);
            this.Next.TabIndex = 96;
            this.Next.Text = "Próximo Fone";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // calibrated
            // 
            this.calibrated.AutoSize = true;
            this.calibrated.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.calibrated.Location = new System.Drawing.Point(655, 145);
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
            this.label1.Location = new System.Drawing.Point(426, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(324, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "Após calibrar o fone clique em \"Calibrado\"";
            // 
            // earphoneLabel
            // 
            this.earphoneLabel.AutoSize = true;
            this.earphoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.earphoneLabel.Location = new System.Drawing.Point(651, 90);
            this.earphoneLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.earphoneLabel.Name = "earphoneLabel";
            this.earphoneLabel.Size = new System.Drawing.Size(113, 20);
            this.earphoneLabel.TabIndex = 99;
            this.earphoneLabel.Text = "Lado Direito";
            // 
            // Help
            // 
            this.Help.Location = new System.Drawing.Point(426, 363);
            this.Help.Margin = new System.Windows.Forms.Padding(4);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(188, 35);
            this.Help.TabIndex = 97;
            this.Help.Text = "Ajuda";
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // earphoneCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 408);
            this.Controls.Add(this.Fail);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Sound);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.calibrated);
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
            this.Text = "Calibração com Fones de Ouvido";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
    }
}