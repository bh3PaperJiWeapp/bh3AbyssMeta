namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.RefreshMetaButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.AbyssMetaFolderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.角色数据窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.武器数据窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.圣痕数据窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stigmataLabel3 = new System.Windows.Forms.Label();
            this.stigmataLabel2 = new System.Windows.Forms.Label();
            this.stigmataLabel1 = new System.Windows.Forms.Label();
            this.weaponLabel1 = new System.Windows.Forms.Label();
            this.characterLabel1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.stigmataLabel6 = new System.Windows.Forms.Label();
            this.stigmataLabel5 = new System.Windows.Forms.Label();
            this.stigmataLabel4 = new System.Windows.Forms.Label();
            this.weaponLabel2 = new System.Windows.Forms.Label();
            this.characterLabel2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.stigmataLabel9 = new System.Windows.Forms.Label();
            this.stigmataLabel8 = new System.Windows.Forms.Label();
            this.stigmataLabel7 = new System.Windows.Forms.Label();
            this.weaponLabel3 = new System.Windows.Forms.Label();
            this.characterLabel3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.exportButton = new System.Windows.Forms.Button();
            this.exportFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // RefreshMetaButton
            // 
            this.RefreshMetaButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshMetaButton.Location = new System.Drawing.Point(1712, 9);
            this.RefreshMetaButton.Name = "RefreshMetaButton";
            this.RefreshMetaButton.Size = new System.Drawing.Size(189, 23);
            this.RefreshMetaButton.TabIndex = 1;
            this.RefreshMetaButton.Text = "刷新深渊Meta文件夹";
            this.RefreshMetaButton.UseVisualStyleBackColor = true;
            this.RefreshMetaButton.Click += new System.EventHandler(this.RefreshMetaButton_Click);
            // 
            // AbyssMetaFolderTextBox
            // 
            this.AbyssMetaFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AbyssMetaFolderTextBox.Location = new System.Drawing.Point(102, 11);
            this.AbyssMetaFolderTextBox.Name = "AbyssMetaFolderTextBox";
            this.AbyssMetaFolderTextBox.Size = new System.Drawing.Size(1604, 21);
            this.AbyssMetaFolderTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据文件夹路径";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.AbyssMetaFolderTextBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.RefreshMetaButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1904, 41);
            this.panel2.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.FileName,
            this.FolderName});
            this.dataGridView1.Location = new System.Drawing.Point(3, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(237, 717);
            this.dataGridView1.TabIndex = 7;
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // FileName
            // 
            this.FileName.HeaderText = "文件名称";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // FolderName
            // 
            this.FolderName.HeaderText = "文件路径";
            this.FolderName.Name = "FolderName";
            this.FolderName.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.角色数据窗口ToolStripMenuItem,
            this.武器数据窗口ToolStripMenuItem,
            this.圣痕数据窗口ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1904, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 角色数据窗口ToolStripMenuItem
            // 
            this.角色数据窗口ToolStripMenuItem.Name = "角色数据窗口ToolStripMenuItem";
            this.角色数据窗口ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.角色数据窗口ToolStripMenuItem.Text = "角色数据窗口";
            this.角色数据窗口ToolStripMenuItem.Click += new System.EventHandler(this.角色数据窗口ToolStripMenuItem_Click);
            // 
            // 武器数据窗口ToolStripMenuItem
            // 
            this.武器数据窗口ToolStripMenuItem.Name = "武器数据窗口ToolStripMenuItem";
            this.武器数据窗口ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.武器数据窗口ToolStripMenuItem.Text = "武器数据窗口";
            this.武器数据窗口ToolStripMenuItem.Click += new System.EventHandler(this.武器数据窗口ToolStripMenuItem_Click);
            // 
            // 圣痕数据窗口ToolStripMenuItem
            // 
            this.圣痕数据窗口ToolStripMenuItem.Name = "圣痕数据窗口ToolStripMenuItem";
            this.圣痕数据窗口ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.圣痕数据窗口ToolStripMenuItem.Text = "圣痕数据窗口";
            this.圣痕数据窗口ToolStripMenuItem.Click += new System.EventHandler(this.圣痕数据窗口ToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1655, 206);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(345, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.stigmataLabel3);
            this.panel1.Controls.Add(this.stigmataLabel2);
            this.panel1.Controls.Add(this.stigmataLabel1);
            this.panel1.Controls.Add(this.weaponLabel1);
            this.panel1.Controls.Add(this.characterLabel1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(249, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1655, 259);
            this.panel1.TabIndex = 13;
            // 
            // stigmataLabel3
            // 
            this.stigmataLabel3.AutoSize = true;
            this.stigmataLabel3.Location = new System.Drawing.Point(757, 218);
            this.stigmataLabel3.Name = "stigmataLabel3";
            this.stigmataLabel3.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel3.TabIndex = 14;
            this.stigmataLabel3.Text = "label6";
            // 
            // stigmataLabel2
            // 
            this.stigmataLabel2.AutoSize = true;
            this.stigmataLabel2.Location = new System.Drawing.Point(585, 218);
            this.stigmataLabel2.Name = "stigmataLabel2";
            this.stigmataLabel2.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel2.TabIndex = 13;
            this.stigmataLabel2.Text = "label5";
            // 
            // stigmataLabel1
            // 
            this.stigmataLabel1.AutoSize = true;
            this.stigmataLabel1.Location = new System.Drawing.Point(406, 218);
            this.stigmataLabel1.Name = "stigmataLabel1";
            this.stigmataLabel1.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel1.TabIndex = 12;
            this.stigmataLabel1.Text = "label4";
            // 
            // weaponLabel1
            // 
            this.weaponLabel1.AutoSize = true;
            this.weaponLabel1.Location = new System.Drawing.Point(231, 218);
            this.weaponLabel1.Name = "weaponLabel1";
            this.weaponLabel1.Size = new System.Drawing.Size(41, 12);
            this.weaponLabel1.TabIndex = 11;
            this.weaponLabel1.Text = "label3";
            // 
            // characterLabel1
            // 
            this.characterLabel1.AutoSize = true;
            this.characterLabel1.Location = new System.Drawing.Point(28, 218);
            this.characterLabel1.Name = "characterLabel1";
            this.characterLabel1.Size = new System.Drawing.Size(41, 12);
            this.characterLabel1.TabIndex = 10;
            this.characterLabel1.Text = "label2";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.stigmataLabel6);
            this.panel3.Controls.Add(this.stigmataLabel5);
            this.panel3.Controls.Add(this.stigmataLabel4);
            this.panel3.Controls.Add(this.weaponLabel2);
            this.panel3.Controls.Add(this.characterLabel2);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Location = new System.Drawing.Point(249, 331);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1655, 254);
            this.panel3.TabIndex = 14;
            // 
            // stigmataLabel6
            // 
            this.stigmataLabel6.AutoSize = true;
            this.stigmataLabel6.Location = new System.Drawing.Point(757, 222);
            this.stigmataLabel6.Name = "stigmataLabel6";
            this.stigmataLabel6.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel6.TabIndex = 19;
            this.stigmataLabel6.Text = "label6";
            // 
            // stigmataLabel5
            // 
            this.stigmataLabel5.AutoSize = true;
            this.stigmataLabel5.Location = new System.Drawing.Point(585, 222);
            this.stigmataLabel5.Name = "stigmataLabel5";
            this.stigmataLabel5.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel5.TabIndex = 18;
            this.stigmataLabel5.Text = "label5";
            // 
            // stigmataLabel4
            // 
            this.stigmataLabel4.AutoSize = true;
            this.stigmataLabel4.Location = new System.Drawing.Point(406, 222);
            this.stigmataLabel4.Name = "stigmataLabel4";
            this.stigmataLabel4.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel4.TabIndex = 17;
            this.stigmataLabel4.Text = "label4";
            // 
            // weaponLabel2
            // 
            this.weaponLabel2.AutoSize = true;
            this.weaponLabel2.Location = new System.Drawing.Point(231, 222);
            this.weaponLabel2.Name = "weaponLabel2";
            this.weaponLabel2.Size = new System.Drawing.Size(41, 12);
            this.weaponLabel2.TabIndex = 16;
            this.weaponLabel2.Text = "label3";
            // 
            // characterLabel2
            // 
            this.characterLabel2.AutoSize = true;
            this.characterLabel2.Location = new System.Drawing.Point(28, 222);
            this.characterLabel2.Name = "characterLabel2";
            this.characterLabel2.Size = new System.Drawing.Size(41, 12);
            this.characterLabel2.TabIndex = 15;
            this.characterLabel2.Text = "label2";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1655, 206);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.stigmataLabel9);
            this.panel4.Controls.Add(this.stigmataLabel8);
            this.panel4.Controls.Add(this.stigmataLabel7);
            this.panel4.Controls.Add(this.weaponLabel3);
            this.panel4.Controls.Add(this.characterLabel3);
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Location = new System.Drawing.Point(249, 591);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1655, 249);
            this.panel4.TabIndex = 14;
            // 
            // stigmataLabel9
            // 
            this.stigmataLabel9.AutoSize = true;
            this.stigmataLabel9.Location = new System.Drawing.Point(757, 218);
            this.stigmataLabel9.Name = "stigmataLabel9";
            this.stigmataLabel9.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel9.TabIndex = 19;
            this.stigmataLabel9.Text = "label6";
            // 
            // stigmataLabel8
            // 
            this.stigmataLabel8.AutoSize = true;
            this.stigmataLabel8.Location = new System.Drawing.Point(585, 218);
            this.stigmataLabel8.Name = "stigmataLabel8";
            this.stigmataLabel8.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel8.TabIndex = 18;
            this.stigmataLabel8.Text = "label5";
            // 
            // stigmataLabel7
            // 
            this.stigmataLabel7.AutoSize = true;
            this.stigmataLabel7.Location = new System.Drawing.Point(406, 218);
            this.stigmataLabel7.Name = "stigmataLabel7";
            this.stigmataLabel7.Size = new System.Drawing.Size(41, 12);
            this.stigmataLabel7.TabIndex = 17;
            this.stigmataLabel7.Text = "label4";
            // 
            // weaponLabel3
            // 
            this.weaponLabel3.AutoSize = true;
            this.weaponLabel3.Location = new System.Drawing.Point(231, 218);
            this.weaponLabel3.Name = "weaponLabel3";
            this.weaponLabel3.Size = new System.Drawing.Size(41, 12);
            this.weaponLabel3.TabIndex = 16;
            this.weaponLabel3.Text = "label3";
            // 
            // characterLabel3
            // 
            this.characterLabel3.AutoSize = true;
            this.characterLabel3.Location = new System.Drawing.Point(28, 218);
            this.characterLabel3.Name = "characterLabel3";
            this.characterLabel3.Size = new System.Drawing.Size(41, 12);
            this.characterLabel3.TabIndex = 15;
            this.characterLabel3.Text = "label2";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1655, 206);
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.exportButton);
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 66);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(243, 775);
            this.panel5.TabIndex = 16;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(9, 723);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(228, 49);
            this.exportButton.TabIndex = 8;
            this.exportButton.Text = "输出数据";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 841);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button RefreshMetaButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox AbyssMetaFolderTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolderName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 角色数据窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 武器数据窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 圣痕数据窗口ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label stigmataLabel3;
        private System.Windows.Forms.Label stigmataLabel2;
        private System.Windows.Forms.Label stigmataLabel1;
        private System.Windows.Forms.Label weaponLabel1;
        private System.Windows.Forms.Label characterLabel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label stigmataLabel6;
        private System.Windows.Forms.Label stigmataLabel5;
        private System.Windows.Forms.Label stigmataLabel4;
        private System.Windows.Forms.Label weaponLabel2;
        private System.Windows.Forms.Label characterLabel2;
        private System.Windows.Forms.Label stigmataLabel9;
        private System.Windows.Forms.Label stigmataLabel8;
        private System.Windows.Forms.Label stigmataLabel7;
        private System.Windows.Forms.Label weaponLabel3;
        private System.Windows.Forms.Label characterLabel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.FolderBrowserDialog exportFolderBrowserDialog;
    }
}

