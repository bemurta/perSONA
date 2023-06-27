namespace perSONA
{
    partial class preliminaryTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(preliminaryTest));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.volume = new System.Windows.Forms.TrackBar();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.soundLabel = new System.Windows.Forms.Label();
            this.soundSignal = new System.Windows.Forms.Button();
            this.calibrate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volume)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.soundLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.soundSignal, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.calibrate, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(520, 198);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.Controls.Add(this.volume, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.volumeLabel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 101);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(514, 43);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // volume
            // 
            this.volume.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volume.Location = new System.Drawing.Point(131, 3);
            this.volume.Maximum = 100;
            this.volume.Minimum = 1;
            this.volume.Name = "volume";
            this.volume.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.volume.Size = new System.Drawing.Size(380, 37);
            this.volume.TabIndex = 1;
            this.volume.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.volume.Value = 1;
            this.volume.Scroll += new System.EventHandler(this.volume_Scroll);
            // 
            // volumeLabel
            // 
            this.volumeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.Location = new System.Drawing.Point(3, 0);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(122, 43);
            this.volumeLabel.TabIndex = 0;
            this.volumeLabel.Text = "Volume";
            this.volumeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // soundLabel
            // 
            this.soundLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundLabel.AutoSize = true;
            this.soundLabel.Location = new System.Drawing.Point(3, 49);
            this.soundLabel.Name = "soundLabel";
            this.soundLabel.Size = new System.Drawing.Size(514, 49);
            this.soundLabel.TabIndex = 3;
            this.soundLabel.Text = "Definir faixa dinâmica para sinais sonoros abaixo do limiar de desconforto.";
            this.soundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // soundSignal
            // 
            this.soundSignal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soundSignal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(64)))), ((int)(((byte)(137)))));
            this.soundSignal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.soundSignal.ForeColor = System.Drawing.Color.White;
            this.soundSignal.Location = new System.Drawing.Point(3, 3);
            this.soundSignal.Name = "soundSignal";
            this.soundSignal.Size = new System.Drawing.Size(514, 43);
            this.soundSignal.TabIndex = 1;
            this.soundSignal.Text = "Gerar sinal sonoro";
            this.soundSignal.UseVisualStyleBackColor = true;
            this.soundSignal.Click += new System.EventHandler(this.soundSignal_Click);
            // 
            // calibrate
            // 
            this.calibrate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calibrate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(64)))), ((int)(((byte)(137)))));
            this.calibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calibrate.ForeColor = System.Drawing.Color.White;
            this.calibrate.Location = new System.Drawing.Point(3, 150);
            this.calibrate.Name = "calibrate";
            this.calibrate.Size = new System.Drawing.Size(514, 45);
            this.calibrate.TabIndex = 2;
            this.calibrate.Text = "Calibrar";
            this.calibrate.UseVisualStyleBackColor = true;
            this.calibrate.Click += new System.EventHandler(this.calibrate_Click);
            // 
            // preliminaryTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 197);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "preliminaryTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pré-ensaio";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volume)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TrackBar volume;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.Label soundLabel;
        private System.Windows.Forms.Button soundSignal;
        private System.Windows.Forms.Button calibrate;
    }
}