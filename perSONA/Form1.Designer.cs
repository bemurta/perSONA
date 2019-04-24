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
            this.components = new System.ComponentModel.Container();
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.getFolder = new System.Windows.Forms.Button();
            this.speechRight = new System.Windows.Forms.Button();
            this.speechFront = new System.Windows.Forms.Button();
            this.speechLeft = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(300, 37);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(142, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(300, 66);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(223, 23);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // openServer
            // 
            this.openServer.Location = new System.Drawing.Point(448, 39);
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
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(236, 161);
            this.textBox.TabIndex = 3;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(300, 95);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(223, 23);
            this.reset.TabIndex = 4;
            this.reset.Text = "reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // createSource
            // 
            this.createSource.Location = new System.Drawing.Point(410, 371);
            this.createSource.Name = "createSource";
            this.createSource.Size = new System.Drawing.Size(112, 23);
            this.createSource.TabIndex = 5;
            this.createSource.Text = "create source";
            this.createSource.UseVisualStyleBackColor = true;
            this.createSource.Click += new System.EventHandler(this.createSource_Click);
            // 
            // createReceiver
            // 
            this.createReceiver.Location = new System.Drawing.Point(411, 124);
            this.createReceiver.Name = "createReceiver";
            this.createReceiver.Size = new System.Drawing.Size(112, 23);
            this.createReceiver.TabIndex = 6;
            this.createReceiver.Text = "create receiver";
            this.createReceiver.UseVisualStyleBackColor = true;
            this.createReceiver.Click += new System.EventHandler(this.createReceiver_Click);
            // 
            // play
            // 
            this.play.Location = new System.Drawing.Point(410, 400);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(112, 23);
            this.play.TabIndex = 7;
            this.play.Text = "play";
            this.play.UseVisualStyleBackColor = true;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // createSource2
            // 
            this.createSource2.Location = new System.Drawing.Point(410, 297);
            this.createSource2.Name = "createSource2";
            this.createSource2.Size = new System.Drawing.Size(112, 21);
            this.createSource2.TabIndex = 8;
            this.createSource2.Text = "Random signal";
            this.createSource2.UseVisualStyleBackColor = true;
            this.createSource2.Click += new System.EventHandler(this.createSource2_Click);
            // 
            // play2
            // 
            this.play2.Location = new System.Drawing.Point(626, 252);
            this.play2.Name = "play2";
            this.play2.Size = new System.Drawing.Size(112, 23);
            this.play2.TabIndex = 9;
            this.play2.Text = "Random angle";
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
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(300, 377);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 12;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 409);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Volume";
            // 
            // getFolder
            // 
            this.getFolder.Location = new System.Drawing.Point(300, 124);
            this.getFolder.Name = "getFolder";
            this.getFolder.Size = new System.Drawing.Size(112, 23);
            this.getFolder.TabIndex = 15;
            this.getFolder.Text = "get folder";
            this.getFolder.UseVisualStyleBackColor = true;
            this.getFolder.Click += new System.EventHandler(this.getFolder_Click);
            // 
            // speechRight
            // 
            this.speechRight.Location = new System.Drawing.Point(626, 309);
            this.speechRight.Name = "speechRight";
            this.speechRight.Size = new System.Drawing.Size(112, 23);
            this.speechRight.TabIndex = 16;
            this.speechRight.Text = "SR,NF";
            this.speechRight.UseVisualStyleBackColor = true;
            this.speechRight.Click += new System.EventHandler(this.speechRight_Click);
            // 
            // speechFront
            // 
            this.speechFront.Location = new System.Drawing.Point(626, 338);
            this.speechFront.Name = "speechFront";
            this.speechFront.Size = new System.Drawing.Size(112, 23);
            this.speechFront.TabIndex = 17;
            this.speechFront.Text = "SF,NF";
            this.speechFront.UseVisualStyleBackColor = true;
            this.speechFront.Click += new System.EventHandler(this.speechFront_Click);
            // 
            // speechLeft
            // 
            this.speechLeft.Location = new System.Drawing.Point(626, 281);
            this.speechLeft.Name = "speechLeft";
            this.speechLeft.Size = new System.Drawing.Size(112, 23);
            this.speechLeft.TabIndex = 18;
            this.speechLeft.Text = "SL,NF";
            this.speechLeft.UseVisualStyleBackColor = true;
            this.speechLeft.Click += new System.EventHandler(this.speechLeft_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(626, 79);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(112, 121);
            this.listBox1.TabIndex = 19;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(626, 207);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(112, 20);
            this.textBox2.TabIndex = 20;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(300, 170);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(223, 121);
            this.listBox2.TabIndex = 21;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(300, 297);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 21);
            this.button1.TabIndex = 22;
            this.button1.Text = "Select signal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(22, 207);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(236, 236);
            this.zedGraphControl1.TabIndex = 23;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(300, 325);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.speechLeft);
            this.Controls.Add(this.speechFront);
            this.Controls.Add(this.speechRight);
            this.Controls.Add(this.getFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar2);
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
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button getFolder;
        private System.Windows.Forms.Button speechRight;
        private System.Windows.Forms.Button speechFront;
        private System.Windows.Forms.Button speechLeft;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button button2;
    }
}

