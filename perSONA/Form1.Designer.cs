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
            this.createReceiver = new System.Windows.Forms.Button();
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.testSetup = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vASettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioDatabaseEditorAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.applicatorBox = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.patientBox = new System.Windows.Forms.ListBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(13, 36);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(112, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Conectar ao VA";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(131, 36);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(112, 23);
            this.buttonDisconnect.TabIndex = 1;
            this.buttonDisconnect.Text = "Disconectar o VA";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // openServer
            // 
            this.openServer.Location = new System.Drawing.Point(13, 9);
            this.openServer.Name = "openServer";
            this.openServer.Size = new System.Drawing.Size(230, 23);
            this.openServer.TabIndex = 2;
            this.openServer.Text = "Iniciar VA";
            this.openServer.UseVisualStyleBackColor = true;
            this.openServer.Click += new System.EventHandler(this.openServer_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 174);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(260, 394);
            this.textBox.TabIndex = 3;
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(133, 191);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(112, 23);
            this.reset.TabIndex = 4;
            this.reset.Text = "Reset Scene";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // createReceiver
            // 
            this.createReceiver.Location = new System.Drawing.Point(133, 163);
            this.createReceiver.Name = "createReceiver";
            this.createReceiver.Size = new System.Drawing.Size(112, 23);
            this.createReceiver.TabIndex = 6;
            this.createReceiver.Text = "create receiver";
            this.createReceiver.UseVisualStyleBackColor = true;
            this.createReceiver.Click += new System.EventHandler(this.createReceiver_Click);
            // 
            // createSource2
            // 
            this.createSource2.Location = new System.Drawing.Point(133, 17);
            this.createSource2.Name = "createSource2";
            this.createSource2.Size = new System.Drawing.Size(112, 23);
            this.createSource2.TabIndex = 8;
            this.createSource2.Text = "Random signal";
            this.createSource2.UseVisualStyleBackColor = true;
            this.createSource2.Click += new System.EventHandler(this.createSource2_Click);
            // 
            // play2
            // 
            this.play2.Location = new System.Drawing.Point(133, 134);
            this.play2.Name = "play2";
            this.play2.Size = new System.Drawing.Size(112, 23);
            this.play2.TabIndex = 9;
            this.play2.Text = "Random angle";
            this.play2.UseVisualStyleBackColor = true;
            this.play2.Click += new System.EventHandler(this.play2_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(133, 219);
            this.trackBar1.Maximum = 40;
            this.trackBar1.Minimum = -40;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "SNR: 0 dB";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(15, 220);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 12;
            this.trackBar2.Value = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Volume:1";
            // 
            // getFolder
            // 
            this.getFolder.Location = new System.Drawing.Point(13, 17);
            this.getFolder.Name = "getFolder";
            this.getFolder.Size = new System.Drawing.Size(233, 23);
            this.getFolder.TabIndex = 15;
            this.getFolder.Text = "Load test list";
            this.getFolder.UseVisualStyleBackColor = true;
            this.getFolder.Click += new System.EventHandler(this.getFolder_Click);
            // 
            // speechRight
            // 
            this.speechRight.Location = new System.Drawing.Point(133, 76);
            this.speechRight.Name = "speechRight";
            this.speechRight.Size = new System.Drawing.Size(112, 23);
            this.speechRight.TabIndex = 16;
            this.speechRight.Text = "SR,NF";
            this.speechRight.UseVisualStyleBackColor = true;
            this.speechRight.Click += new System.EventHandler(this.speechRight_Click);
            // 
            // speechFront
            // 
            this.speechFront.Location = new System.Drawing.Point(133, 105);
            this.speechFront.Name = "speechFront";
            this.speechFront.Size = new System.Drawing.Size(112, 23);
            this.speechFront.TabIndex = 17;
            this.speechFront.Text = "SF,NF";
            this.speechFront.UseVisualStyleBackColor = true;
            this.speechFront.Click += new System.EventHandler(this.speechFront_Click);
            // 
            // speechLeft
            // 
            this.speechLeft.Location = new System.Drawing.Point(133, 47);
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
            this.listBox1.Location = new System.Drawing.Point(15, 47);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(112, 134);
            this.listBox1.TabIndex = 19;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(15, 194);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(112, 20);
            this.textBox2.TabIndex = 20;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(16, 47);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(230, 134);
            this.listBox2.TabIndex = 21;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Select signal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(15, 265);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(224, 236);
            this.zedGraphControl1.TabIndex = 23;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 85);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(230, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "Configuração de arquivos de audio";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(16, 475);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(230, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "Start test";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(16, 199);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(230, 21);
            this.comboBox3.TabIndex = 29;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 241);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(230, 20);
            this.textBox1.TabIndex = 30;
            this.textBox1.Text = "Test one";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Noise signal";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Speech test label";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.reset);
            this.panel1.Controls.Add(this.createReceiver);
            this.panel1.Controls.Add(this.createSource2);
            this.panel1.Controls.Add(this.play2);
            this.panel1.Controls.Add(this.zedGraphControl1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.speechRight);
            this.panel1.Controls.Add(this.speechFront);
            this.panel1.Controls.Add(this.speechLeft);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Location = new System.Drawing.Point(264, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 513);
            this.panel1.TabIndex = 39;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panel2.Location = new System.Drawing.Point(2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 513);
            this.panel2.TabIndex = 40;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numericUpDown3);
            this.groupBox5.Location = new System.Drawing.Point(16, 425);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(230, 44);
            this.groupBox5.TabIndex = 43;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Starting Signal to Noise Ratio";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(6, 18);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            -2147483648});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(218, 20);
            this.numericUpDown3.TabIndex = 0;
            this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numericUpDown2);
            this.groupBox4.Location = new System.Drawing.Point(134, 323);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(112, 44);
            this.groupBox4.TabIndex = 42;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Noise distance";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 1;
            this.numericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown2.Location = new System.Drawing.Point(9, 18);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown2.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Location = new System.Drawing.Point(16, 322);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(109, 44);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Speech distance";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown1.Location = new System.Drawing.Point(6, 18);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(97, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            15,
            0,
            0,
            65536});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton6);
            this.groupBox2.Location = new System.Drawing.Point(16, 373);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 44);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Noise souce direction";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(43, 17);
            this.radioButton4.TabIndex = 2;
            this.radioButton4.Text = "Left";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(174, 19);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(50, 17);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.Text = "Right";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Checked = true;
            this.radioButton6.Location = new System.Drawing.Point(87, 19);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(49, 17);
            this.radioButton6.TabIndex = 0;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "Front";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(16, 271);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 44);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Speech souce direction";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(43, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Text = "Left";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(174, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(50, 17);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.Text = "Right";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(87, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(49, 17);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Front";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.buttonConnect);
            this.panel3.Controls.Add(this.buttonDisconnect);
            this.panel3.Controls.Add(this.openServer);
            this.panel3.Location = new System.Drawing.Point(12, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(260, 121);
            this.panel3.TabIndex = 41;
            // 
            // testSetup
            // 
            this.testSetup.Location = new System.Drawing.Point(6, 141);
            this.testSetup.Name = "testSetup";
            this.testSetup.Size = new System.Drawing.Size(486, 35);
            this.testSetup.TabIndex = 42;
            this.testSetup.Text = "Avaliação customizada";
            this.testSetup.UseVisualStyleBackColor = true;
            this.testSetup.Click += new System.EventHandler(this.testSetup_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 60);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(486, 35);
            this.button2.TabIndex = 43;
            this.button2.Text = "Fala a frente, Ruído a frente";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(486, 35);
            this.button5.TabIndex = 44;
            this.button5.Text = "Fala a esquerda Ruído a frente";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(6, 100);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(486, 35);
            this.button6.TabIndex = 45;
            this.button6.Text = "Fala a direita, Ruído a direita";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.patientAreaToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(837, 24);
            this.menuStrip1.TabIndex = 46;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.audioDatabaseEditorAreaToolStripMenuItem,
            this.resultsFolderToolStripMenuItem,
            this.vASettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.settingsToolStripMenuItem.Text = "Configurações";
            // 
            // resultsFolderToolStripMenuItem
            // 
            this.resultsFolderToolStripMenuItem.Name = "resultsFolderToolStripMenuItem";
            this.resultsFolderToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.resultsFolderToolStripMenuItem.Text = "Pasta destino dos resultados";
            this.resultsFolderToolStripMenuItem.Click += new System.EventHandler(this.resultsFolderToolStripMenuItem_Click);
            // 
            // vASettingsToolStripMenuItem
            // 
            this.vASettingsToolStripMenuItem.Name = "vASettingsToolStripMenuItem";
            this.vASettingsToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.vASettingsToolStripMenuItem.Text = "Configurações do VA";
            // 
            // audioDatabaseEditorAreaToolStripMenuItem
            // 
            this.audioDatabaseEditorAreaToolStripMenuItem.Name = "audioDatabaseEditorAreaToolStripMenuItem";
            this.audioDatabaseEditorAreaToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.audioDatabaseEditorAreaToolStripMenuItem.Text = "Área de edição de arquivos de áudio";
            this.audioDatabaseEditorAreaToolStripMenuItem.Click += new System.EventHandler(this.audioDatabaseEditorAreaToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.helpToolStripMenuItem.Text = "Sobre";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // patientAreaToolStripMenuItem
            // 
            this.patientAreaToolStripMenuItem.Name = "patientAreaToolStripMenuItem";
            this.patientAreaToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.patientAreaToolStripMenuItem.Text = "Novo paciente";
            this.patientAreaToolStripMenuItem.Click += new System.EventHandler(this.patientAreaToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(289, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(536, 541);
            this.tabControl1.TabIndex = 47;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(528, 515);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Área Clínica";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.applicatorBox);
            this.groupBox8.Location = new System.Drawing.Point(12, 208);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(501, 64);
            this.groupBox8.TabIndex = 47;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Aplicador";
            // 
            // applicatorBox
            // 
            this.applicatorBox.Location = new System.Drawing.Point(6, 28);
            this.applicatorBox.Name = "applicatorBox";
            this.applicatorBox.Size = new System.Drawing.Size(488, 20);
            this.applicatorBox.TabIndex = 0;
            this.applicatorBox.Text = "Aplicador padrão";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.patientBox);
            this.groupBox7.Controls.Add(this.button7);
            this.groupBox7.Controls.Add(this.button8);
            this.groupBox7.Controls.Add(this.button9);
            this.groupBox7.Controls.Add(this.button10);
            this.groupBox7.Location = new System.Drawing.Point(6, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(507, 196);
            this.groupBox7.TabIndex = 47;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Paciente";
            // 
            // patientBox
            // 
            this.patientBox.FormattingEnabled = true;
            this.patientBox.Location = new System.Drawing.Point(12, 19);
            this.patientBox.Name = "patientBox";
            this.patientBox.Size = new System.Drawing.Size(239, 160);
            this.patientBox.TabIndex = 46;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(268, 18);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(230, 35);
            this.button7.TabIndex = 44;
            this.button7.Text = "Criar Paciente";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(268, 60);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(230, 35);
            this.button8.TabIndex = 43;
            this.button8.Text = "Ver dados do Paciente";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(268, 102);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(230, 35);
            this.button9.TabIndex = 45;
            this.button9.Text = "Adicionar audiometria";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(268, 144);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(230, 35);
            this.button10.TabIndex = 42;
            this.button10.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button5);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.button6);
            this.groupBox6.Controls.Add(this.testSetup);
            this.groupBox6.Location = new System.Drawing.Point(12, 278);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(501, 231);
            this.groupBox6.TabIndex = 46;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Avaliaçao de percepção de fala";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(528, 515);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Área de teste";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem
            // 
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem.Name = "áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem";
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 589);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "perSONA 1.2.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button openServer;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button createReceiver;
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button testSetup;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultsFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vASettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioDatabaseEditorAreaToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox applicatorBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.ListBox patientBox;
        private System.Windows.Forms.ToolStripMenuItem áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem;
    }
}

