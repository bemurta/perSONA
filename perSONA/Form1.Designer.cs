using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VA;
using ZedGraph;

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
            this.reset = new System.Windows.Forms.Button();
            this.createReceiver = new System.Windows.Forms.Button();
            this.createSource2 = new System.Windows.Forms.Button();
            this.play2 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.speechRight = new System.Windows.Forms.Button();
            this.speechFront = new System.Windows.Forms.Button();
            this.speechLeft = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.testSetup = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioDatabaseEditorAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vASettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.applicatorBox = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.patientBox = new System.Windows.Forms.ListBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cond4 = new System.Windows.Forms.CheckBox();
            this.cond3 = new System.Windows.Forms.CheckBox();
            this.cond2 = new System.Windows.Forms.CheckBox();
            this.cond1 = new System.Windows.Forms.CheckBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.getFolder = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.openServer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(213, 593);
            this.reset.Margin = new System.Windows.Forms.Padding(5);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(177, 30);
            this.reset.TabIndex = 9;
            this.reset.Text = "Limpar cena";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // createReceiver
            // 
            this.createReceiver.Location = new System.Drawing.Point(12, 593);
            this.createReceiver.Margin = new System.Windows.Forms.Padding(5);
            this.createReceiver.Name = "createReceiver";
            this.createReceiver.Size = new System.Drawing.Size(191, 30);
            this.createReceiver.TabIndex = 8;
            this.createReceiver.Text = "Criar receptor";
            this.createReceiver.UseVisualStyleBackColor = true;
            this.createReceiver.Click += new System.EventHandler(this.createReceiver_Click);
            // 
            // createSource2
            // 
            this.createSource2.Location = new System.Drawing.Point(213, 219);
            this.createSource2.Margin = new System.Windows.Forms.Padding(5);
            this.createSource2.Name = "createSource2";
            this.createSource2.Size = new System.Drawing.Size(177, 30);
            this.createSource2.TabIndex = 3;
            this.createSource2.Text = "Sinal aleatório";
            this.createSource2.UseVisualStyleBackColor = true;
            this.createSource2.Click += new System.EventHandler(this.createSource2_Click);
            // 
            // play2
            // 
            this.play2.Location = new System.Drawing.Point(20, 235);
            this.play2.Margin = new System.Windows.Forms.Padding(5);
            this.play2.Name = "play2";
            this.play2.Size = new System.Drawing.Size(347, 30);
            this.play2.TabIndex = 7;
            this.play2.Text = "Direção aleatória";
            this.play2.UseVisualStyleBackColor = true;
            this.play2.Click += new System.EventHandler(this.play2_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Window;
            this.trackBar1.Location = new System.Drawing.Point(197, 288);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(5);
            this.trackBar1.Maximum = 40;
            this.trackBar1.Minimum = -40;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(170, 45);
            this.trackBar1.TabIndex = 11;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 316);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "SNR: 0 dB";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // trackBar2
            // 
            this.trackBar2.BackColor = System.Drawing.SystemColors.Window;
            this.trackBar2.Location = new System.Drawing.Point(20, 288);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(5);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(170, 45);
            this.trackBar2.TabIndex = 10;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar2.Value = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 316);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Volume:1";
            // 
            // speechRight
            // 
            this.speechRight.Location = new System.Drawing.Point(20, 115);
            this.speechRight.Margin = new System.Windows.Forms.Padding(5);
            this.speechRight.Name = "speechRight";
            this.speechRight.Size = new System.Drawing.Size(347, 30);
            this.speechRight.TabIndex = 5;
            this.speechRight.Text = "Sinal à direita Ruído à frente";
            this.speechRight.UseVisualStyleBackColor = true;
            this.speechRight.Click += new System.EventHandler(this.speechRight_Click);
            // 
            // speechFront
            // 
            this.speechFront.Location = new System.Drawing.Point(20, 175);
            this.speechFront.Margin = new System.Windows.Forms.Padding(5);
            this.speechFront.Name = "speechFront";
            this.speechFront.Size = new System.Drawing.Size(347, 30);
            this.speechFront.TabIndex = 6;
            this.speechFront.Text = "Sinal à frente Ruído à frente";
            this.speechFront.UseVisualStyleBackColor = true;
            this.speechFront.Click += new System.EventHandler(this.speechFront_Click);
            // 
            // speechLeft
            // 
            this.speechLeft.Location = new System.Drawing.Point(20, 55);
            this.speechLeft.Margin = new System.Windows.Forms.Padding(5);
            this.speechLeft.Name = "speechLeft";
            this.speechLeft.Size = new System.Drawing.Size(347, 30);
            this.speechLeft.TabIndex = 4;
            this.speechLeft.Text = "Sinal à esquerda Ruído à frente";
            this.speechLeft.UseVisualStyleBackColor = true;
            this.speechLeft.Click += new System.EventHandler(this.speechLeft_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(12, 273);
            this.listBox1.Margin = new System.Windows.Forms.Padding(5);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(378, 174);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 471);
            this.textBox2.Margin = new System.Windows.Forms.Padding(5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(378, 23);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 219);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(191, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "Usar sinal selecionado";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(11, 339);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(356, 340);
            this.zedGraphControl1.TabIndex = 14;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.play2);
            this.panel1.Controls.Add(this.zedGraphControl1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.speechRight);
            this.panel1.Controls.Add(this.speechFront);
            this.panel1.Controls.Add(this.speechLeft);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Location = new System.Drawing.Point(417, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 684);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(129, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 25);
            this.label3.TabIndex = 15;
            this.label3.Text = "Reprodução";
            // 
            // testSetup
            // 
            this.testSetup.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.testSetup.Location = new System.Drawing.Point(10, 160);
            this.testSetup.Margin = new System.Windows.Forms.Padding(5);
            this.testSetup.Name = "testSetup";
            this.testSetup.Size = new System.Drawing.Size(750, 35);
            this.testSetup.TabIndex = 3;
            this.testSetup.Text = "Avaliação customizada";
            this.testSetup.UseVisualStyleBackColor = false;
            this.testSetup.Click += new System.EventHandler(this.testSetup_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(10, 70);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(750, 35);
            this.button2.TabIndex = 1;
            this.button2.Text = "Fala a frente, Ruído a frente";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button5.Location = new System.Drawing.Point(10, 25);
            this.button5.Margin = new System.Windows.Forms.Padding(5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(750, 35);
            this.button5.TabIndex = 0;
            this.button5.Text = "Fala a esquerda Ruído a frente";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button6.Location = new System.Drawing.Point(10, 115);
            this.button6.Margin = new System.Windows.Forms.Padding(5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(750, 35);
            this.button6.TabIndex = 2;
            this.button6.Text = "Fala a direita, Ruído a direita";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.patientAreaToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.contactToolStripMenuItem,
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1210, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.audioDatabaseEditorAreaToolStripMenuItem,
            this.resultsFolderToolStripMenuItem,
            this.vASettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(111, 23);
            this.settingsToolStripMenuItem.Text = "Configurações";
            // 
            // audioDatabaseEditorAreaToolStripMenuItem
            // 
            this.audioDatabaseEditorAreaToolStripMenuItem.Name = "audioDatabaseEditorAreaToolStripMenuItem";
            this.audioDatabaseEditorAreaToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.audioDatabaseEditorAreaToolStripMenuItem.Text = "Área de edição de arquivos de áudio";
            this.audioDatabaseEditorAreaToolStripMenuItem.Click += new System.EventHandler(this.audioDatabaseEditorAreaToolStripMenuItem_Click);
            // 
            // resultsFolderToolStripMenuItem
            // 
            this.resultsFolderToolStripMenuItem.Name = "resultsFolderToolStripMenuItem";
            this.resultsFolderToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.resultsFolderToolStripMenuItem.Text = "Pasta destino dos resultados";
            this.resultsFolderToolStripMenuItem.Click += new System.EventHandler(this.resultsFolderToolStripMenuItem_Click);
            // 
            // vASettingsToolStripMenuItem
            // 
            this.vASettingsToolStripMenuItem.Name = "vASettingsToolStripMenuItem";
            this.vASettingsToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.vASettingsToolStripMenuItem.Text = "Configurações do VA";
            // 
            // patientAreaToolStripMenuItem
            // 
            this.patientAreaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientAreaToolStripMenuItem.Name = "patientAreaToolStripMenuItem";
            this.patientAreaToolStripMenuItem.Size = new System.Drawing.Size(109, 23);
            this.patientAreaToolStripMenuItem.Text = "Novo Paciente";
            this.patientAreaToolStripMenuItem.Click += new System.EventHandler(this.patientAreaToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(56, 23);
            this.helpToolStripMenuItem.Text = "Ajuda";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // contactToolStripMenuItem
            // 
            this.contactToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contactToolStripMenuItem.Name = "contactToolStripMenuItem";
            this.contactToolStripMenuItem.Size = new System.Drawing.Size(71, 23);
            this.contactToolStripMenuItem.Text = "Contato";
            this.contactToolStripMenuItem.Click += new System.EventHandler(this.contactToolStripMenuItem_Click);
            // 
            // áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem
            // 
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem.Name = "áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem";
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem.Size = new System.Drawing.Size(12, 23);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(400, 27);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(806, 740);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage2.Size = new System.Drawing.Size(798, 710);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Área Clínica";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox8.Controls.Add(this.applicatorBox);
            this.groupBox8.Location = new System.Drawing.Point(7, 281);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox8.Size = new System.Drawing.Size(781, 76);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Aplicador";
            // 
            // applicatorBox
            // 
            this.applicatorBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.applicatorBox.Location = new System.Drawing.Point(10, 30);
            this.applicatorBox.Margin = new System.Windows.Forms.Padding(5);
            this.applicatorBox.Name = "applicatorBox";
            this.applicatorBox.Size = new System.Drawing.Size(750, 23);
            this.applicatorBox.TabIndex = 0;
            this.applicatorBox.Text = "Aplicador padrão";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox7.Controls.Add(this.patientBox);
            this.groupBox7.Controls.Add(this.button7);
            this.groupBox7.Controls.Add(this.button8);
            this.groupBox7.Controls.Add(this.button10);
            this.groupBox7.Location = new System.Drawing.Point(7, 10);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox7.Size = new System.Drawing.Size(781, 229);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Paciente";
            // 
            // patientBox
            // 
            this.patientBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.patientBox.FormattingEnabled = true;
            this.patientBox.ItemHeight = 17;
            this.patientBox.Location = new System.Drawing.Point(20, 29);
            this.patientBox.Margin = new System.Windows.Forms.Padding(5);
            this.patientBox.Name = "patientBox";
            this.patientBox.Size = new System.Drawing.Size(348, 174);
            this.patientBox.TabIndex = 0;
            this.patientBox.SelectedIndexChanged += new System.EventHandler(this.patientBox_SelectedIndexChanged);
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button7.Location = new System.Drawing.Point(390, 29);
            this.button7.Margin = new System.Windows.Forms.Padding(5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(370, 35);
            this.button7.TabIndex = 1;
            this.button7.Text = "Criar Paciente";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button8.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button8.Location = new System.Drawing.Point(390, 74);
            this.button8.Margin = new System.Windows.Forms.Padding(5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(370, 35);
            this.button8.TabIndex = 2;
            this.button8.Text = "Ver Dados do Paciente";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button10
            // 
            this.button10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button10.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button10.Location = new System.Drawing.Point(390, 178);
            this.button10.Margin = new System.Windows.Forms.Padding(5);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(370, 35);
            this.button10.TabIndex = 3;
            this.button10.Text = "Deletar Paciente";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox6.Controls.Add(this.button5);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.button6);
            this.groupBox6.Controls.Add(this.testSetup);
            this.groupBox6.Location = new System.Drawing.Point(10, 494);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox6.Size = new System.Drawing.Size(781, 206);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Avaliaçao de percepção de fala";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage1.Size = new System.Drawing.Size(798, 710);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Área de teste";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.createReceiver);
            this.panel2.Controls.Add(this.reset);
            this.panel2.Controls.Add(this.cond4);
            this.panel2.Controls.Add(this.cond3);
            this.panel2.Controls.Add(this.cond2);
            this.panel2.Controls.Add(this.cond1);
            this.panel2.Controls.Add(this.listBox2);
            this.panel2.Controls.Add(this.getFolder);
            this.panel2.Controls.Add(this.createSource2);
            this.panel2.Controls.Add(this.comboBox3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Location = new System.Drawing.Point(4, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(403, 684);
            this.panel2.TabIndex = 0;
            // 
            // cond4
            // 
            this.cond4.AutoSize = true;
            this.cond4.Enabled = false;
            this.cond4.Location = new System.Drawing.Point(330, 642);
            this.cond4.Name = "cond4";
            this.cond4.Size = new System.Drawing.Size(60, 21);
            this.cond4.TabIndex = 9;
            this.cond4.Text = "Cena";
            this.cond4.UseVisualStyleBackColor = true;
            // 
            // cond3
            // 
            this.cond3.AutoSize = true;
            this.cond3.Enabled = false;
            this.cond3.Location = new System.Drawing.Point(208, 642);
            this.cond3.Name = "cond3";
            this.cond3.Size = new System.Drawing.Size(85, 21);
            this.cond3.TabIndex = 8;
            this.cond3.Text = "Receptor";
            this.cond3.UseVisualStyleBackColor = true;
            // 
            // cond2
            // 
            this.cond2.AutoSize = true;
            this.cond2.Checked = true;
            this.cond2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cond2.Enabled = false;
            this.cond2.Location = new System.Drawing.Point(107, 642);
            this.cond2.Name = "cond2";
            this.cond2.Size = new System.Drawing.Size(64, 21);
            this.cond2.TabIndex = 7;
            this.cond2.Text = "Ruído";
            this.cond2.UseVisualStyleBackColor = true;
            // 
            // cond1
            // 
            this.cond1.AutoSize = true;
            this.cond1.Enabled = false;
            this.cond1.Location = new System.Drawing.Point(12, 642);
            this.cond1.Name = "cond1";
            this.cond1.Size = new System.Drawing.Size(58, 21);
            this.cond1.TabIndex = 6;
            this.cond1.Text = "Sinal";
            this.cond1.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 17;
            this.listBox2.Location = new System.Drawing.Point(10, 55);
            this.listBox2.Margin = new System.Windows.Forms.Padding(5);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(380, 140);
            this.listBox2.TabIndex = 1;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // getFolder
            // 
            this.getFolder.Location = new System.Drawing.Point(10, 10);
            this.getFolder.Margin = new System.Windows.Forms.Padding(5);
            this.getFolder.Name = "getFolder";
            this.getFolder.Size = new System.Drawing.Size(380, 35);
            this.getFolder.TabIndex = 0;
            this.getFolder.Text = "Carregar pasta com sinais";
            this.getFolder.UseVisualStyleBackColor = true;
            this.getFolder.Click += new System.EventHandler(this.getFolder_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(16, 530);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(380, 25);
            this.comboBox3.TabIndex = 3;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 505);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ruído";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(5, 5);
            this.button9.Margin = new System.Windows.Forms.Padding(5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(350, 35);
            this.button9.TabIndex = 0;
            this.button9.Text = "Mostrar/Esconder painel de controle";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.BackgroundImage = global::perSONA.Properties.Resources.resized_help;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel4.Controls.Add(this.textBox);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.button9);
            this.panel4.Location = new System.Drawing.Point(20, 27);
            this.panel4.Margin = new System.Windows.Forms.Padding(5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(360, 736);
            this.panel4.TabIndex = 1;
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox.Location = new System.Drawing.Point(0, 200);
            this.textBox.Margin = new System.Windows.Forms.Padding(5);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(360, 536);
            this.textBox.TabIndex = 2;
            this.textBox.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.buttonConnect);
            this.panel3.Controls.Add(this.buttonDisconnect);
            this.panel3.Controls.Add(this.openServer);
            this.panel3.Location = new System.Drawing.Point(0, 50);
            this.panel3.Margin = new System.Windows.Forms.Padding(5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(360, 151);
            this.panel3.TabIndex = 1;
            this.panel3.Visible = false;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button3.Location = new System.Drawing.Point(5, 104);
            this.button3.Margin = new System.Windows.Forms.Padding(5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(348, 35);
            this.button3.TabIndex = 3;
            this.button3.Text = "Configuração de arquivos de audio";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(5, 59);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(5);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(170, 35);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Conectar ao VA";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonDisconnect.Location = new System.Drawing.Point(183, 59);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(5);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(170, 35);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Desconectar o VA";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // openServer
            // 
            this.openServer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.openServer.Location = new System.Drawing.Point(4, 14);
            this.openServer.Margin = new System.Windows.Forms.Padding(5);
            this.openServer.Name = "openServer";
            this.openServer.Size = new System.Drawing.Size(350, 35);
            this.openServer.TabIndex = 0;
            this.openServer.Text = "Iniciar VA";
            this.openServer.UseVisualStyleBackColor = true;
            this.openServer.Click += new System.EventHandler(this.openServer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 770);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "perSONA 1.5  BETA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Button createReceiver;
        private System.Windows.Forms.Button createSource2;
        private System.Windows.Forms.Button play2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button speechRight;
        private System.Windows.Forms.Button speechFront;
        private System.Windows.Forms.Button speechLeft;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.ListBox patientBox;
        private System.Windows.Forms.ToolStripMenuItem áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Panel panel4;
        private ToolStripMenuItem contactToolStripMenuItem;
        private Panel panel2;
        private ListBox listBox2;
        private Button getFolder;
        private ComboBox comboBox3;
        private System.Windows.Forms.Label label5;
        private CheckBox cond4;
        private CheckBox cond3;
        private CheckBox cond2;
        private CheckBox cond1;
        private System.Windows.Forms.Label label3;
        private TextBox textBox;
        private Panel panel3;
        private Button button3;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private Button openServer;
    }
}

