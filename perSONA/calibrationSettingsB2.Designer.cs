namespace perSONA
{
    partial class calibrationSettingsB2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(calibrationSettingsB2));
            this.Next = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.mannequinPinnaeBox = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.mannequinModelBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mannequinBrandBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mannequinPicture = new System.Windows.Forms.PictureBox();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mannequinPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // Next
            // 
            this.Next.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Next.Location = new System.Drawing.Point(674, 189);
            this.Next.Margin = new System.Windows.Forms.Padding(4);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(300, 56);
            this.Next.TabIndex = 60;
            this.Next.Text = "Prosseguir";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.mannequinPinnaeBox);
            this.groupBox6.Location = new System.Drawing.Point(674, 80);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(300, 56);
            this.groupBox6.TabIndex = 56;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Pinna";
            // 
            // mannequinPinnaeBox
            // 
            this.mannequinPinnaeBox.FormattingEnabled = true;
            this.mannequinPinnaeBox.Items.AddRange(new object[] {
            "Sem Pinna"});
            this.mannequinPinnaeBox.Location = new System.Drawing.Point(7, 20);
            this.mannequinPinnaeBox.Name = "mannequinPinnaeBox";
            this.mannequinPinnaeBox.Size = new System.Drawing.Size(286, 28);
            this.mannequinPinnaeBox.TabIndex = 62;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.mannequinModelBox);
            this.groupBox8.Location = new System.Drawing.Point(13, 189);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(300, 56);
            this.groupBox8.TabIndex = 57;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Modelo do Manequim";
            // 
            // mannequinModelBox
            // 
            this.mannequinModelBox.FormattingEnabled = true;
            this.mannequinModelBox.Location = new System.Drawing.Point(9, 20);
            this.mannequinModelBox.Margin = new System.Windows.Forms.Padding(4);
            this.mannequinModelBox.Name = "mannequinModelBox";
            this.mannequinModelBox.Size = new System.Drawing.Size(284, 28);
            this.mannequinModelBox.TabIndex = 11;
            this.mannequinModelBox.SelectedIndexChanged += new System.EventHandler(this.mannequinModelBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mannequinBrandBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 80);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(300, 56);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Marca do Manequim";
            // 
            // mannequinBrandBox
            // 
            this.mannequinBrandBox.FormattingEnabled = true;
            this.mannequinBrandBox.Items.AddRange(new object[] {
            "01 dB",
            "Bruel and Kjaer",
            "Neumann"});
            this.mannequinBrandBox.Location = new System.Drawing.Point(9, 20);
            this.mannequinBrandBox.Margin = new System.Windows.Forms.Padding(4);
            this.mannequinBrandBox.Name = "mannequinBrandBox";
            this.mannequinBrandBox.Size = new System.Drawing.Size(284, 28);
            this.mannequinBrandBox.TabIndex = 11;
            this.mannequinBrandBox.SelectedIndexChanged += new System.EventHandler(this.mannequinBrandBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(46, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(906, 40);
            this.label1.TabIndex = 54;
            this.label1.Text = "Você escolheu fazer a calibração com o uso de um manequim.\r\nPreencha as informaçõ" +
    "es a seguir para continuar, é possível escrever nas listas caso não encontre seu" +
    "s equipamentos:\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mannequinPicture
            // 
            this.mannequinPicture.Location = new System.Drawing.Point(401, 80);
            this.mannequinPicture.Name = "mannequinPicture";
            this.mannequinPicture.Size = new System.Drawing.Size(165, 165);
            this.mannequinPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mannequinPicture.TabIndex = 61;
            this.mannequinPicture.TabStop = false;
            // 
            // calibrationSettingsB2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 258);
            this.Controls.Add(this.mannequinPicture);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "calibrationSettingsB2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calibração com Manequim";
            this.groupBox6.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mannequinPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox mannequinModelBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox mannequinBrandBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox mannequinPicture;
        private System.Windows.Forms.ComboBox mannequinPinnaeBox;
    }
}