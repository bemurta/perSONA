namespace perSONA
{
    partial class speechIterTestForm
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
            this.components = new System.ComponentModel.Container();
            this.testWordsList = new System.Windows.Forms.ListBox();
            this.all_correct = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.all_incorrect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.detailsBox = new System.Windows.Forms.TextBox();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.currentTryal = new System.Windows.Forms.TextBox();
            this.streakText = new System.Windows.Forms.TextBox();
            this.computedAudioText = new System.Windows.Forms.TextBox();
            this.totalWordsText = new System.Windows.Forms.TextBox();
            this.continuousTimerText = new System.Windows.Forms.TextBox();
            this.filenameList = new System.Windows.Forms.ListBox();
            this.playCurrentScene = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.patientLabel = new System.Windows.Forms.Label();
            this.applicatorLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // testWordsList
            // 
            this.testWordsList.FormattingEnabled = true;
            this.testWordsList.Location = new System.Drawing.Point(31, 237);
            this.testWordsList.Name = "testWordsList";
            this.testWordsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.testWordsList.Size = new System.Drawing.Size(191, 199);
            this.testWordsList.TabIndex = 20;
            this.testWordsList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // all_correct
            // 
            this.all_correct.Location = new System.Drawing.Point(244, 273);
            this.all_correct.Name = "all_correct";
            this.all_correct.Size = new System.Drawing.Size(112, 23);
            this.all_correct.TabIndex = 21;
            this.all_correct.Text = "Todas corretas";
            this.all_correct.UseVisualStyleBackColor = true;
            this.all_correct.Click += new System.EventHandler(this.all_correct_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(400, 12);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(367, 175);
            this.zedGraphControl1.TabIndex = 22;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(397, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "SNR:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(397, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Sequência de acerto";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(397, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Sentença atual";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(397, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Total de sentenças";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // all_incorrect
            // 
            this.all_incorrect.Location = new System.Drawing.Point(244, 302);
            this.all_incorrect.Name = "all_incorrect";
            this.all_incorrect.Size = new System.Drawing.Size(112, 23);
            this.all_incorrect.TabIndex = 27;
            this.all_incorrect.Text = "Nenhum acerto";
            this.all_incorrect.UseVisualStyleBackColor = true;
            this.all_incorrect.Click += new System.EventHandler(this.all_incorrect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 370);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Porcentagem de acerto";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 335);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Palavras corretas";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(244, 351);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 20);
            this.textBox1.TabIndex = 31;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(244, 386);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(112, 20);
            this.textBox2.TabIndex = 32;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 413);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "Next sentence";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(397, 359);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Tempo de resposta";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(397, 398);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Duração do ensaio";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // detailsBox
            // 
            this.detailsBox.Location = new System.Drawing.Point(244, 80);
            this.detailsBox.Multiline = true;
            this.detailsBox.Name = "detailsBox";
            this.detailsBox.Size = new System.Drawing.Size(112, 147);
            this.detailsBox.TabIndex = 36;
            this.detailsBox.Visible = false;
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Location = new System.Drawing.Point(532, 202);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(236, 236);
            this.zedGraphControl2.TabIndex = 37;
            this.zedGraphControl2.UseExtendedPrintDialog = true;
            // 
            // currentTryal
            // 
            this.currentTryal.Enabled = false;
            this.currentTryal.Location = new System.Drawing.Point(400, 375);
            this.currentTryal.Name = "currentTryal";
            this.currentTryal.Size = new System.Drawing.Size(112, 20);
            this.currentTryal.TabIndex = 38;
            // 
            // streakText
            // 
            this.streakText.Enabled = false;
            this.streakText.Location = new System.Drawing.Point(400, 246);
            this.streakText.Name = "streakText";
            this.streakText.Size = new System.Drawing.Size(112, 20);
            this.streakText.TabIndex = 40;
            // 
            // computedAudioText
            // 
            this.computedAudioText.Enabled = false;
            this.computedAudioText.Location = new System.Drawing.Point(400, 285);
            this.computedAudioText.Name = "computedAudioText";
            this.computedAudioText.Size = new System.Drawing.Size(112, 20);
            this.computedAudioText.TabIndex = 41;
            // 
            // totalWordsText
            // 
            this.totalWordsText.Enabled = false;
            this.totalWordsText.Location = new System.Drawing.Point(400, 321);
            this.totalWordsText.Name = "totalWordsText";
            this.totalWordsText.Size = new System.Drawing.Size(112, 20);
            this.totalWordsText.TabIndex = 42;
            // 
            // continuousTimerText
            // 
            this.continuousTimerText.Enabled = false;
            this.continuousTimerText.Location = new System.Drawing.Point(400, 414);
            this.continuousTimerText.Name = "continuousTimerText";
            this.continuousTimerText.Size = new System.Drawing.Size(112, 20);
            this.continuousTimerText.TabIndex = 43;
            // 
            // filenameList
            // 
            this.filenameList.Enabled = false;
            this.filenameList.FormattingEnabled = true;
            this.filenameList.Location = new System.Drawing.Point(31, 80);
            this.filenameList.Name = "filenameList";
            this.filenameList.ScrollAlwaysVisible = true;
            this.filenameList.Size = new System.Drawing.Size(191, 147);
            this.filenameList.TabIndex = 44;
            this.filenameList.SelectedIndexChanged += new System.EventHandler(this.filenameList_SelectedIndexChanged);
            // 
            // playCurrentScene
            // 
            this.playCurrentScene.Location = new System.Drawing.Point(244, 238);
            this.playCurrentScene.Name = "playCurrentScene";
            this.playCurrentScene.Size = new System.Drawing.Size(112, 23);
            this.playCurrentScene.TabIndex = 45;
            this.playCurrentScene.Text = "Reproduzir áudio";
            this.playCurrentScene.UseVisualStyleBackColor = true;
            this.playCurrentScene.Click += new System.EventHandler(this.playCurrentScene_Click);
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(400, 207);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(112, 20);
            this.textBox3.TabIndex = 46;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.patientLabel);
            this.panel1.Controls.Add(this.applicatorLabel);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Location = new System.Drawing.Point(31, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 62);
            this.panel1.TabIndex = 47;
            // 
            // patientLabel
            // 
            this.patientLabel.AutoSize = true;
            this.patientLabel.Location = new System.Drawing.Point(75, 39);
            this.patientLabel.Name = "patientLabel";
            this.patientLabel.Size = new System.Drawing.Size(54, 13);
            this.patientLabel.TabIndex = 27;
            this.patientLabel.Text = "Aplicador:";
            // 
            // applicatorLabel
            // 
            this.applicatorLabel.AutoSize = true;
            this.applicatorLabel.Location = new System.Drawing.Point(75, 15);
            this.applicatorLabel.Name = "applicatorLabel";
            this.applicatorLabel.Size = new System.Drawing.Size(54, 13);
            this.applicatorLabel.TabIndex = 26;
            this.applicatorLabel.Text = "Aplicador:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Paciente:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Aplicador:";
            // 
            // speechIterTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.playCurrentScene);
            this.Controls.Add(this.filenameList);
            this.Controls.Add(this.continuousTimerText);
            this.Controls.Add(this.totalWordsText);
            this.Controls.Add(this.computedAudioText);
            this.Controls.Add(this.streakText);
            this.Controls.Add(this.currentTryal);
            this.Controls.Add(this.zedGraphControl2);
            this.Controls.Add(this.detailsBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.all_incorrect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.all_correct);
            this.Controls.Add(this.testWordsList);
            this.Name = "speechIterTestForm";
            this.Text = "perSONA 1.4.0 - Módulo de aplicação de ensaio";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox testWordsList;
        private System.Windows.Forms.Button all_correct;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button all_incorrect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox detailsBox;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.TextBox currentTryal;
        private System.Windows.Forms.TextBox streakText;
        private System.Windows.Forms.TextBox computedAudioText;
        private System.Windows.Forms.TextBox totalWordsText;
        private System.Windows.Forms.TextBox continuousTimerText;
        private System.Windows.Forms.ListBox filenameList;
        private System.Windows.Forms.Button playCurrentScene;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label patientLabel;
        private System.Windows.Forms.Label applicatorLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}