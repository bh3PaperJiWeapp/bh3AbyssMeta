namespace WindowsFormsApp1
{
    partial class WeaponDataForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.refreshWeaponDatabutton = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categortIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weaponBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.selectWeaponFolderbutton = new System.Windows.Forms.Button();
            this.weaponFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.refreshWeaponDatabutton);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.selectWeaponFolderbutton);
            this.groupBox2.Controls.Add(this.weaponFolderPathTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 450);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "武器";
            // 
            // refreshWeaponDatabutton
            // 
            this.refreshWeaponDatabutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshWeaponDatabutton.Location = new System.Drawing.Point(638, 57);
            this.refreshWeaponDatabutton.Name = "refreshWeaponDatabutton";
            this.refreshWeaponDatabutton.Size = new System.Drawing.Size(75, 23);
            this.refreshWeaponDatabutton.TabIndex = 8;
            this.refreshWeaponDatabutton.Text = "刷新数据";
            this.refreshWeaponDatabutton.UseVisualStyleBackColor = true;
            this.refreshWeaponDatabutton.Click += new System.EventHandler(this.refreshWeaponDatabutton_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.categortIdDataGridViewTextBoxColumn,
            this.filePathDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.weaponBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(6, 86);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(788, 350);
            this.dataGridView2.TabIndex = 7;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "名称";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // categortIdDataGridViewTextBoxColumn
            // 
            this.categortIdDataGridViewTextBoxColumn.DataPropertyName = "categortId";
            this.categortIdDataGridViewTextBoxColumn.HeaderText = "武器类型";
            this.categortIdDataGridViewTextBoxColumn.Name = "categortIdDataGridViewTextBoxColumn";
            // 
            // filePathDataGridViewTextBoxColumn
            // 
            this.filePathDataGridViewTextBoxColumn.DataPropertyName = "filePath";
            this.filePathDataGridViewTextBoxColumn.HeaderText = "文件路径";
            this.filePathDataGridViewTextBoxColumn.Name = "filePathDataGridViewTextBoxColumn";
            // 
            // weaponBindingSource
            // 
            this.weaponBindingSource.DataSource = typeof(WindowsFormsApp1.Entity.Weapon);
            // 
            // selectWeaponFolderbutton
            // 
            this.selectWeaponFolderbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectWeaponFolderbutton.Location = new System.Drawing.Point(719, 57);
            this.selectWeaponFolderbutton.Name = "selectWeaponFolderbutton";
            this.selectWeaponFolderbutton.Size = new System.Drawing.Size(75, 23);
            this.selectWeaponFolderbutton.TabIndex = 6;
            this.selectWeaponFolderbutton.Text = "选择文件夹";
            this.selectWeaponFolderbutton.UseVisualStyleBackColor = true;
            this.selectWeaponFolderbutton.Click += new System.EventHandler(this.selectWeaponFolderbutton_Click);
            // 
            // weaponFolderPathTextBox
            // 
            this.weaponFolderPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.weaponFolderPathTextBox.Location = new System.Drawing.Point(6, 30);
            this.weaponFolderPathTextBox.Name = "weaponFolderPathTextBox";
            this.weaponFolderPathTextBox.Size = new System.Drawing.Size(788, 21);
            this.weaponFolderPathTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "文件夹路径";
            // 
            // WeaponDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Name = "WeaponDataForm";
            this.Text = "WeaponDataForm";
            this.Load += new System.EventHandler(this.WeaponDataForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button refreshWeaponDatabutton;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button selectWeaponFolderbutton;
        private System.Windows.Forms.TextBox weaponFolderPathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn categortIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource weaponBindingSource;
    }
}