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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
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
            this.calibraçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.patientBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CreatePatient = new System.Windows.Forms.Button();
            this.ShowPatientData = new System.Windows.Forms.Button();
            this.DeletePatient = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.applicatorBox = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.getFolder = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.cond1 = new System.Windows.Forms.CheckBox();
            this.cond2 = new System.Windows.Forms.CheckBox();
            this.cond3 = new System.Windows.Forms.CheckBox();
            this.cond4 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.openServer = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // reset
            // 
            this.reset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reset.Location = new System.Drawing.Point(190, 5);
            this.reset.Margin = new System.Windows.Forms.Padding(5);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(176, 33);
            this.reset.TabIndex = 9;
            this.reset.Text = "Limpar cena";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // createReceiver
            // 
            this.createReceiver.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createReceiver.Location = new System.Drawing.Point(5, 5);
            this.createReceiver.Margin = new System.Windows.Forms.Padding(5);
            this.createReceiver.Name = "createReceiver";
            this.createReceiver.Size = new System.Drawing.Size(175, 33);
            this.createReceiver.TabIndex = 8;
            this.createReceiver.Text = "Criar receptor";
            this.createReceiver.UseVisualStyleBackColor = true;
            this.createReceiver.Click += new System.EventHandler(this.createReceiver_Click);
            // 
            // createSource2
            // 
            this.createSource2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createSource2.Location = new System.Drawing.Point(190, 5);
            this.createSource2.Margin = new System.Windows.Forms.Padding(5);
            this.createSource2.Name = "createSource2";
            this.createSource2.Size = new System.Drawing.Size(176, 37);
            this.createSource2.TabIndex = 3;
            this.createSource2.Text = "Sinal aleatório";
            this.createSource2.UseVisualStyleBackColor = true;
            this.createSource2.Click += new System.EventHandler(this.createSource2_Click);
            // 
            // play2
            // 
            this.play2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.play2.Location = new System.Drawing.Point(5, 217);
            this.play2.Margin = new System.Windows.Forms.Padding(5);
            this.play2.Name = "play2";
            this.play2.Size = new System.Drawing.Size(367, 36);
            this.play2.TabIndex = 7;
            this.play2.Text = "Direção aleatória";
            this.play2.UseVisualStyleBackColor = true;
            this.play2.Click += new System.EventHandler(this.play2_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.BackColor = System.Drawing.SystemColors.Window;
            this.trackBar1.Location = new System.Drawing.Point(190, 28);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(5);
            this.trackBar1.Maximum = 40;
            this.trackBar1.Minimum = -40;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(176, 50);
            this.trackBar1.TabIndex = 11;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "SNR: 0 dB";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.BackColor = System.Drawing.SystemColors.Window;
            this.trackBar2.Location = new System.Drawing.Point(5, 28);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(5);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(175, 50);
            this.trackBar2.TabIndex = 10;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar2.Value = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Volume:1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // speechRight
            // 
            this.speechRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.speechRight.Location = new System.Drawing.Point(5, 123);
            this.speechRight.Margin = new System.Windows.Forms.Padding(5);
            this.speechRight.Name = "speechRight";
            this.speechRight.Size = new System.Drawing.Size(367, 36);
            this.speechRight.TabIndex = 5;
            this.speechRight.Text = "Sinal à direita Ruído à frente";
            this.speechRight.UseVisualStyleBackColor = true;
            this.speechRight.Click += new System.EventHandler(this.speechRight_Click);
            // 
            // speechFront
            // 
            this.speechFront.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.speechFront.Location = new System.Drawing.Point(5, 169);
            this.speechFront.Margin = new System.Windows.Forms.Padding(5);
            this.speechFront.Name = "speechFront";
            this.speechFront.Size = new System.Drawing.Size(367, 38);
            this.speechFront.TabIndex = 6;
            this.speechFront.Text = "Sinal à frente Ruído à frente";
            this.speechFront.UseVisualStyleBackColor = true;
            this.speechFront.Click += new System.EventHandler(this.speechFront_Click);
            // 
            // speechLeft
            // 
            this.speechLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.speechLeft.Location = new System.Drawing.Point(5, 75);
            this.speechLeft.Margin = new System.Windows.Forms.Padding(5);
            this.speechLeft.Name = "speechLeft";
            this.speechLeft.Size = new System.Drawing.Size(367, 38);
            this.speechLeft.TabIndex = 4;
            this.speechLeft.Text = "Sinal à esquerda Ruído à frente";
            this.speechLeft.UseVisualStyleBackColor = true;
            this.speechLeft.Click += new System.EventHandler(this.speechLeft_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(5, 246);
            this.listBox1.Margin = new System.Windows.Forms.Padding(5);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(367, 164);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(5, 434);
            this.textBox2.Margin = new System.Windows.Forms.Padding(5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(367, 27);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(5, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Usar sinal selecionado";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel12);
            this.panel1.Location = new System.Drawing.Point(400, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 680);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel12.ColumnCount = 1;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.play2, 0, 5);
            this.tableLayoutPanel12.Controls.Add(this.speechFront, 0, 4);
            this.tableLayoutPanel12.Controls.Add(this.speechRight, 0, 3);
            this.tableLayoutPanel12.Controls.Add(this.speechLeft, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.zedGraphControl1, 0, 8);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel11, 0, 7);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 9;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.868494F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.741837F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.185629F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.886228F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.185629F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.886228F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.646707F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.32335F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.10778F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(377, 668);
            this.tableLayoutPanel12.TabIndex = 17;
            this.tableLayoutPanel12.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel12_Paint);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(371, 59);
            this.label3.TabIndex = 15;
            this.label3.Text = "Reprodução";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.Location = new System.Drawing.Point(6, 368);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(6, 10, 6, 10);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(365, 290);
            this.zedGraphControl1.TabIndex = 14;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.trackBar2, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.trackBar1, 1, 1);
            this.tableLayoutPanel11.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 272);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.04878F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.95122F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(371, 83);
            this.tableLayoutPanel11.TabIndex = 16;
            // 
            // testSetup
            // 
            this.testSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testSetup.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.testSetup.Location = new System.Drawing.Point(5, 146);
            this.testSetup.Margin = new System.Windows.Forms.Padding(5);
            this.testSetup.Name = "testSetup";
            this.testSetup.Size = new System.Drawing.Size(746, 40);
            this.testSetup.TabIndex = 3;
            this.testSetup.Text = "Avaliação customizada";
            this.testSetup.UseVisualStyleBackColor = false;
            this.testSetup.Click += new System.EventHandler(this.testSetup_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(5, 52);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(746, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "Fala a frente, Ruído a frente";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button5.Location = new System.Drawing.Point(5, 5);
            this.button5.Margin = new System.Windows.Forms.Padding(5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(746, 37);
            this.button5.TabIndex = 0;
            this.button5.Text = "Fala a esquerda Ruído a frente";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button6.Location = new System.Drawing.Point(5, 99);
            this.button6.Margin = new System.Windows.Forms.Padding(5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(746, 37);
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
            this.calibraçãoToolStripMenuItem,
            this.contactToolStripMenuItem,
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1210, 31);
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
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(131, 27);
            this.settingsToolStripMenuItem.Text = "Configurações";
            // 
            // audioDatabaseEditorAreaToolStripMenuItem
            // 
            this.audioDatabaseEditorAreaToolStripMenuItem.Name = "audioDatabaseEditorAreaToolStripMenuItem";
            this.audioDatabaseEditorAreaToolStripMenuItem.Size = new System.Drawing.Size(363, 26);
            this.audioDatabaseEditorAreaToolStripMenuItem.Text = "Área de edição de arquivos de áudio";
            this.audioDatabaseEditorAreaToolStripMenuItem.Click += new System.EventHandler(this.audioDatabaseEditorAreaToolStripMenuItem_Click);
            // 
            // resultsFolderToolStripMenuItem
            // 
            this.resultsFolderToolStripMenuItem.Name = "resultsFolderToolStripMenuItem";
            this.resultsFolderToolStripMenuItem.Size = new System.Drawing.Size(363, 26);
            this.resultsFolderToolStripMenuItem.Text = "Pasta destino dos resultados";
            this.resultsFolderToolStripMenuItem.Click += new System.EventHandler(this.resultsFolderToolStripMenuItem_Click);
            // 
            // vASettingsToolStripMenuItem
            // 
            this.vASettingsToolStripMenuItem.Name = "vASettingsToolStripMenuItem";
            this.vASettingsToolStripMenuItem.Size = new System.Drawing.Size(363, 26);
            this.vASettingsToolStripMenuItem.Text = "Configurações do VA";
            // 
            // patientAreaToolStripMenuItem
            // 
            this.patientAreaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientAreaToolStripMenuItem.Name = "patientAreaToolStripMenuItem";
            this.patientAreaToolStripMenuItem.Size = new System.Drawing.Size(134, 27);
            this.patientAreaToolStripMenuItem.Text = "Novo Paciente";
            this.patientAreaToolStripMenuItem.Click += new System.EventHandler(this.patientAreaToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 27);
            this.helpToolStripMenuItem.Text = "Ajuda";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // calibraçãoToolStripMenuItem
            // 
            this.calibraçãoToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calibraçãoToolStripMenuItem.Name = "calibraçãoToolStripMenuItem";
            this.calibraçãoToolStripMenuItem.Size = new System.Drawing.Size(104, 27);
            this.calibraçãoToolStripMenuItem.Text = "Calibração";
            this.calibraçãoToolStripMenuItem.Click += new System.EventHandler(this.calibraçãoToolStripMenuItem_Click);
            // 
            // contactToolStripMenuItem
            // 
            this.contactToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contactToolStripMenuItem.Name = "contactToolStripMenuItem";
            this.contactToolStripMenuItem.Size = new System.Drawing.Size(86, 27);
            this.contactToolStripMenuItem.Text = "Contato";
            this.contactToolStripMenuItem.Click += new System.EventHandler(this.contactToolStripMenuItem_Click);
            // 
            // áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem
            // 
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem.Name = "áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem";
            this.áreaDeEdiçãoDeArquivosDeÁudioToolStripMenuItem.Size = new System.Drawing.Size(14, 27);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(374, 5);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(807, 726);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage2.Size = new System.Drawing.Size(799, 693);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Área Clínica";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox6, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.groupBox8, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.20768F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.79232F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 233F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(783, 677);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.patientBox);
            this.groupBox7.Controls.Add(this.tableLayoutPanel1);
            this.groupBox7.Location = new System.Drawing.Point(5, 5);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox7.Size = new System.Drawing.Size(773, 239);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Paciente";
            // 
            // patientBox
            // 
            this.patientBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.patientBox.FormattingEnabled = true;
            this.patientBox.ItemHeight = 20;
            this.patientBox.Location = new System.Drawing.Point(20, 29);
            this.patientBox.Margin = new System.Windows.Forms.Padding(5);
            this.patientBox.Name = "patientBox";
            this.patientBox.Size = new System.Drawing.Size(348, 204);
            this.patientBox.TabIndex = 0;
            this.patientBox.SelectedIndexChanged += new System.EventHandler(this.patientBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.CreatePatient, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ShowPatientData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.DeletePatient, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(391, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(361, 204);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // CreatePatient
            // 
            this.CreatePatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreatePatient.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CreatePatient.Location = new System.Drawing.Point(5, 5);
            this.CreatePatient.Margin = new System.Windows.Forms.Padding(5);
            this.CreatePatient.Name = "CreatePatient";
            this.CreatePatient.Size = new System.Drawing.Size(351, 58);
            this.CreatePatient.TabIndex = 1;
            this.CreatePatient.Text = "Criar Paciente";
            this.CreatePatient.UseVisualStyleBackColor = false;
            this.CreatePatient.Click += new System.EventHandler(this.button7_Click);
            // 
            // ShowPatientData
            // 
            this.ShowPatientData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowPatientData.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ShowPatientData.Location = new System.Drawing.Point(5, 73);
            this.ShowPatientData.Margin = new System.Windows.Forms.Padding(5);
            this.ShowPatientData.Name = "ShowPatientData";
            this.ShowPatientData.Size = new System.Drawing.Size(351, 58);
            this.ShowPatientData.TabIndex = 2;
            this.ShowPatientData.Text = "Ver Dados do Paciente";
            this.ShowPatientData.UseVisualStyleBackColor = false;
            this.ShowPatientData.Click += new System.EventHandler(this.button8_Click);
            // 
            // DeletePatient
            // 
            this.DeletePatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DeletePatient.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DeletePatient.Location = new System.Drawing.Point(5, 141);
            this.DeletePatient.Margin = new System.Windows.Forms.Padding(5);
            this.DeletePatient.Name = "DeletePatient";
            this.DeletePatient.Size = new System.Drawing.Size(351, 58);
            this.DeletePatient.TabIndex = 3;
            this.DeletePatient.Text = "Deletar Paciente";
            this.DeletePatient.UseVisualStyleBackColor = false;
            this.DeletePatient.Click += new System.EventHandler(this.button10_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.tableLayoutPanel3);
            this.groupBox6.Location = new System.Drawing.Point(5, 448);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox6.Size = new System.Drawing.Size(773, 224);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Avaliaçao de percepção de fala";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.button5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.testSetup, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.button6, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.button2, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(8, 25);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(756, 191);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.applicatorBox);
            this.groupBox8.Location = new System.Drawing.Point(5, 254);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox8.Size = new System.Drawing.Size(773, 63);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Aplicador";
            // 
            // applicatorBox
            // 
            this.applicatorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.applicatorBox.Location = new System.Drawing.Point(10, 30);
            this.applicatorBox.Margin = new System.Windows.Forms.Padding(5);
            this.applicatorBox.Name = "applicatorBox";
            this.applicatorBox.Size = new System.Drawing.Size(753, 27);
            this.applicatorBox.TabIndex = 0;
            this.applicatorBox.Text = "Aplicador padrão";
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.tableLayoutPanel13);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage1.Size = new System.Drawing.Size(799, 693);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Área de teste";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(790, 690);
            this.tableLayoutPanel13.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel10);
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(385, 680);
            this.panel2.TabIndex = 0;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.getFolder, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel7, 0, 8);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel8, 0, 7);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel9, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.listBox2, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.comboBox3, 0, 6);
            this.tableLayoutPanel10.Controls.Add(this.listBox1, 0, 3);
            this.tableLayoutPanel10.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel10.Controls.Add(this.textBox2, 0, 4);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 9;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.248521F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.56213F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.840237F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.81065F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.769231F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.585799F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.248521F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.94675F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(377, 676);
            this.tableLayoutPanel10.TabIndex = 13;
            this.tableLayoutPanel10.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel10_Paint);
            // 
            // getFolder
            // 
            this.getFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.getFolder.Location = new System.Drawing.Point(5, 5);
            this.getFolder.Margin = new System.Windows.Forms.Padding(5);
            this.getFolder.Name = "getFolder";
            this.getFolder.Size = new System.Drawing.Size(367, 39);
            this.getFolder.TabIndex = 0;
            this.getFolder.Text = "Carregar pasta com sinais";
            this.getFolder.UseVisualStyleBackColor = true;
            this.getFolder.Click += new System.EventHandler(this.getFolder_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.ColumnCount = 4;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.67866F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.02057F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.87918F));
            this.tableLayoutPanel7.Controls.Add(this.cond1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cond2, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.cond3, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.cond4, 3, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 603);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(371, 70);
            this.tableLayoutPanel7.TabIndex = 10;
            // 
            // cond1
            // 
            this.cond1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cond1.AutoSize = true;
            this.cond1.Enabled = false;
            this.cond1.Location = new System.Drawing.Point(3, 3);
            this.cond1.Name = "cond1";
            this.cond1.Size = new System.Drawing.Size(86, 64);
            this.cond1.TabIndex = 6;
            this.cond1.Text = "Sinal";
            this.cond1.UseVisualStyleBackColor = true;
            // 
            // cond2
            // 
            this.cond2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cond2.AutoSize = true;
            this.cond2.Checked = true;
            this.cond2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cond2.Enabled = false;
            this.cond2.Location = new System.Drawing.Point(95, 3);
            this.cond2.Name = "cond2";
            this.cond2.Size = new System.Drawing.Size(85, 64);
            this.cond2.TabIndex = 7;
            this.cond2.Text = "Ruído";
            this.cond2.UseVisualStyleBackColor = true;
            // 
            // cond3
            // 
            this.cond3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cond3.AutoSize = true;
            this.cond3.Enabled = false;
            this.cond3.Location = new System.Drawing.Point(186, 3);
            this.cond3.Name = "cond3";
            this.cond3.Size = new System.Drawing.Size(97, 64);
            this.cond3.TabIndex = 8;
            this.cond3.Text = "Receptor";
            this.cond3.UseVisualStyleBackColor = true;
            // 
            // cond4
            // 
            this.cond4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cond4.AutoSize = true;
            this.cond4.Enabled = false;
            this.cond4.Location = new System.Drawing.Point(289, 3);
            this.cond4.Name = "cond4";
            this.cond4.Size = new System.Drawing.Size(79, 64);
            this.cond4.TabIndex = 9;
            this.cond4.Text = "Cena";
            this.cond4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.createReceiver, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.reset, 1, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 554);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(371, 43);
            this.tableLayoutPanel8.TabIndex = 11;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.createSource2, 1, 0);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 191);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(371, 47);
            this.tableLayoutPanel9.TabIndex = 12;
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(5, 54);
            this.listBox2.Margin = new System.Windows.Forms.Padding(5);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(367, 124);
            this.listBox2.TabIndex = 1;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(5, 504);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(367, 28);
            this.comboBox3.TabIndex = 3;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 468);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(367, 31);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ruído";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.BackgroundImage = global::perSONA.Properties.Resources.resized_help;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel4.Controls.Add(this.button9);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Location = new System.Drawing.Point(5, 5);
            this.panel4.Margin = new System.Windows.Forms.Padding(5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(359, 726);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tableLayoutPanel14);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(359, 726);
            this.panel3.TabIndex = 1;
            this.panel3.Visible = false;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.textBox, 0, 1);
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(4, 47);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 2;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.18518F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.81482F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(350, 675);
            this.tableLayoutPanel14.TabIndex = 0;
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(5, 147);
            this.textBox.Margin = new System.Windows.Forms.Padding(5);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(340, 523);
            this.textBox.TabIndex = 2;
            this.textBox.Visible = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.openServer, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.button3, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.70866F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.43307F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(344, 136);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // openServer
            // 
            this.openServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openServer.Location = new System.Drawing.Point(5, 5);
            this.openServer.Margin = new System.Windows.Forms.Padding(5);
            this.openServer.Name = "openServer";
            this.openServer.Size = new System.Drawing.Size(334, 31);
            this.openServer.TabIndex = 0;
            this.openServer.Text = "Iniciar VA";
            this.openServer.UseVisualStyleBackColor = true;
            this.openServer.Click += new System.EventHandler(this.openServer_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(5, 94);
            this.button3.Margin = new System.Windows.Forms.Padding(5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(334, 37);
            this.button3.TabIndex = 3;
            this.button3.Text = "Configuração de arquivos de audio";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.buttonDisconnect, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonConnect, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 44);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(338, 42);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisconnect.Location = new System.Drawing.Point(174, 5);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(5);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(159, 32);
            this.buttonDisconnect.TabIndex = 2;
            this.buttonDisconnect.Text = "Desconectar o VA";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Enabled = false;
            this.buttonConnect.Location = new System.Drawing.Point(5, 5);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(5);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(159, 32);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Conectar ao VA";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.1973F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.8027F));
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 27);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1186, 736);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 770);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
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
        private System.Windows.Forms.Button CreatePatient;
        private System.Windows.Forms.Button ShowPatientData;
        private System.Windows.Forms.Button DeletePatient;
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
        private Button button3;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private Button openServer;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel12;
        private TableLayoutPanel tableLayoutPanel11;
        private ZedGraphControl zedGraphControl1;
        private TableLayoutPanel tableLayoutPanel13;
        private Panel panel3;
        private TableLayoutPanel tableLayoutPanel14;
        private ToolStripMenuItem calibraçãoToolStripMenuItem;
    }
}

