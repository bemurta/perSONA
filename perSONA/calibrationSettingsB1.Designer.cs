namespace perSONA
{
    partial class calibrationSettingsB1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(calibrationSettingsB1));
            this.Next = new System.Windows.Forms.Button();
            this.ModeloOrelha = new System.Windows.Forms.GroupBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.MarcaOrelha = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ModeloOrelha.SuspendLayout();
            this.MarcaOrelha.SuspendLayout();
            this.SuspendLayout();
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(677, 79);
            this.Next.Margin = new System.Windows.Forms.Padding(4);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(300, 56);
            this.Next.TabIndex = 44;
            this.Next.Text = "Prosseguir";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // ModeloOrelha
            // 
            this.ModeloOrelha.Controls.Add(this.comboBox8);
            this.ModeloOrelha.Location = new System.Drawing.Point(342, 79);
            this.ModeloOrelha.Margin = new System.Windows.Forms.Padding(4);
            this.ModeloOrelha.Name = "ModeloOrelha";
            this.ModeloOrelha.Padding = new System.Windows.Forms.Padding(4);
            this.ModeloOrelha.Size = new System.Drawing.Size(300, 56);
            this.ModeloOrelha.TabIndex = 43;
            this.ModeloOrelha.TabStop = false;
            this.ModeloOrelha.Text = "Modelo da Orelha Artificial";
            // 
            // comboBox8
            // 
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Location = new System.Drawing.Point(9, 20);
            this.comboBox8.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(284, 28);
            this.comboBox8.TabIndex = 11;
            // 
            // MarcaOrelha
            // 
            this.MarcaOrelha.Controls.Add(this.comboBox2);
            this.MarcaOrelha.Location = new System.Drawing.Point(10, 79);
            this.MarcaOrelha.Margin = new System.Windows.Forms.Padding(4);
            this.MarcaOrelha.Name = "MarcaOrelha";
            this.MarcaOrelha.Padding = new System.Windows.Forms.Padding(4);
            this.MarcaOrelha.Size = new System.Drawing.Size(300, 56);
            this.MarcaOrelha.TabIndex = 42;
            this.MarcaOrelha.TabStop = false;
            this.MarcaOrelha.Text = "Marca da Orelha Artificial";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(9, 20);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(284, 28);
            this.comboBox2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(906, 40);
            this.label1.TabIndex = 41;
            this.label1.Text = "Você escolheu fazer a calibração com orelhas artificiais.\r\nPreencha as informaçõe" +
    "s a seguir para continuar, é possível escrever nas listas caso não encontre seus" +
    " equipamentos:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calibrationSettingsB1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 145);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.ModeloOrelha);
            this.Controls.Add(this.MarcaOrelha);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "calibrationSettingsB1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calibração com Orelha Artificial";
            this.TopMost = true;
            this.ModeloOrelha.ResumeLayout(false);
            this.MarcaOrelha.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.GroupBox ModeloOrelha;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.GroupBox MarcaOrelha;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
    }
}