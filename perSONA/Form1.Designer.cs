namespace perSONA
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.openServer = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.reset = new System.Windows.Forms.Button();
            this.createSource = new System.Windows.Forms.Button();
            this.createReceiver = new System.Windows.Forms.Button();
            this.play = new System.Windows.Forms.Button();
            this.createSource2 = new System.Windows.Forms.Button();
            this.play2 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(300, 185);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(142, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(300, 240);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(142, 23);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // openServer
            // 
            this.openServer.Location = new System.Drawing.Point(626, 93);
            this.openServer.Name = "openServer";
            this.openServer.Size = new System.Drawing.Size(75, 23);
            this.openServer.TabIndex = 2;
            this.openServer.Text = "StartServer";
            this.openServer.UseVisualStyleBackColor = true;
            this.openServer.Click += new System.EventHandler(this.openServer_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(22, 39);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(236, 365);
            this.textBox.TabIndex = 3;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(626, 132);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 4;
            this.reset.Text = "reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // createSource
            // 
            this.createSource.Location = new System.Drawing.Point(626, 215);
            this.createSource.Name = "createSource";
            this.createSource.Size = new System.Drawing.Size(112, 23);
            this.createSource.TabIndex = 5;
            this.createSource.Text = "create source";
            this.createSource.UseVisualStyleBackColor = true;
            this.createSource.Click += new System.EventHandler(this.createSource_Click);
            // 
            // createReceiver
            // 
            this.createReceiver.Location = new System.Drawing.Point(626, 185);
            this.createReceiver.Name = "createReceiver";
            this.createReceiver.Size = new System.Drawing.Size(112, 23);
            this.createReceiver.TabIndex = 6;
            this.createReceiver.Text = "create receiver";
            this.createReceiver.UseVisualStyleBackColor = true;
            this.createReceiver.Click += new System.EventHandler(this.createReceiver_Click);
            // 
            // play
            // 
            this.play.Location = new System.Drawing.Point(626, 244);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(112, 23);
            this.play.TabIndex = 7;
            this.play.Text = "play";
            this.play.UseVisualStyleBackColor = true;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // createSource2
            // 
            this.createSource2.Location = new System.Drawing.Point(626, 283);
            this.createSource2.Name = "createSource2";
            this.createSource2.Size = new System.Drawing.Size(112, 23);
            this.createSource2.TabIndex = 8;
            this.createSource2.Text = "create source 2";
            this.createSource2.UseVisualStyleBackColor = true;
            this.createSource2.Click += new System.EventHandler(this.createSource2_Click);
            // 
            // play2
            // 
            this.play2.Location = new System.Drawing.Point(626, 324);
            this.play2.Name = "play2";
            this.play2.Size = new System.Drawing.Size(112, 23);
            this.play2.TabIndex = 9;
            this.play2.Text = "play 2";
            this.play2.UseVisualStyleBackColor = true;
            this.play2.Click += new System.EventHandler(this.play2_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(626, 377);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Minimum = -40;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(666, 409);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "SNR: 0 dB";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.play2);
            this.Controls.Add(this.createSource2);
            this.Controls.Add(this.play);
            this.Controls.Add(this.createReceiver);
            this.Controls.Add(this.createSource);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.openServer);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button openServer;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button createSource;
        private System.Windows.Forms.Button createReceiver;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button createSource2;
        private System.Windows.Forms.Button play2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
    }
}

