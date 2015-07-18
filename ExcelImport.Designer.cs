namespace ExcelImport
{
    partial class ExcelImport
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ImportSet = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.StartImport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ImportFileBtn = new System.Windows.Forms.Button();
            this.ImportPathBtn = new System.Windows.Forms.Button();
            this.mClientImportPath = new System.Windows.Forms.TextBox();
            this.ImportFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LogList = new System.Windows.Forms.ListBox();
            this.mTimer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.mServerImportPath = new System.Windows.Forms.TextBox();
            this.mServerImportBtn = new System.Windows.Forms.Button();
            this.ImportSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImportSet
            // 
            this.ImportSet.Controls.Add(this.mServerImportBtn);
            this.ImportSet.Controls.Add(this.mServerImportPath);
            this.ImportSet.Controls.Add(this.label4);
            this.ImportSet.Controls.Add(this.label3);
            this.ImportSet.Controls.Add(this.StartImport);
            this.ImportSet.Controls.Add(this.label2);
            this.ImportSet.Controls.Add(this.ImportFileBtn);
            this.ImportSet.Controls.Add(this.ImportPathBtn);
            this.ImportSet.Controls.Add(this.mClientImportPath);
            this.ImportSet.Controls.Add(this.ImportFile);
            this.ImportSet.Controls.Add(this.label1);
            this.ImportSet.Location = new System.Drawing.Point(13, 13);
            this.ImportSet.Name = "ImportSet";
            this.ImportSet.Size = new System.Drawing.Size(655, 158);
            this.ImportSet.TabIndex = 0;
            this.ImportSet.TabStop = false;
            this.ImportSet.Text = "导出设置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(101, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "可以拖动文件或者文件夹到上面的文本框";
            // 
            // StartImport
            // 
            this.StartImport.Location = new System.Drawing.Point(514, 129);
            this.StartImport.Name = "StartImport";
            this.StartImport.Size = new System.Drawing.Size(135, 23);
            this.StartImport.TabIndex = 5;
            this.StartImport.Text = "开始导出";
            this.StartImport.UseVisualStyleBackColor = true;
            this.StartImport.Click += new System.EventHandler(this.StartImport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "文件或文件夹";
            // 
            // ImportFileBtn
            // 
            this.ImportFileBtn.Location = new System.Drawing.Point(514, 17);
            this.ImportFileBtn.Name = "ImportFileBtn";
            this.ImportFileBtn.Size = new System.Drawing.Size(135, 23);
            this.ImportFileBtn.TabIndex = 2;
            this.ImportFileBtn.Text = "选择文件或文件夹";
            this.ImportFileBtn.UseVisualStyleBackColor = true;
            this.ImportFileBtn.Click += new System.EventHandler(this.ImportFileBtn_Click);
            // 
            // ImportPathBtn
            // 
            this.ImportPathBtn.Location = new System.Drawing.Point(514, 52);
            this.ImportPathBtn.Name = "ImportPathBtn";
            this.ImportPathBtn.Size = new System.Drawing.Size(135, 23);
            this.ImportPathBtn.TabIndex = 2;
            this.ImportPathBtn.Text = "选择导出路径";
            this.ImportPathBtn.UseVisualStyleBackColor = true;
            this.ImportPathBtn.Click += new System.EventHandler(this.ImportPathBtn_Click);
            // 
            // mClientImportPath
            // 
            this.mClientImportPath.AllowDrop = true;
            this.mClientImportPath.Location = new System.Drawing.Point(103, 54);
            this.mClientImportPath.Name = "mClientImportPath";
            this.mClientImportPath.Size = new System.Drawing.Size(389, 21);
            this.mClientImportPath.TabIndex = 2;
            this.mClientImportPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImportPath_DragDrop);
            this.mClientImportPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImportPath_DragEnter);
            // 
            // ImportFile
            // 
            this.ImportFile.AllowDrop = true;
            this.ImportFile.Location = new System.Drawing.Point(103, 20);
            this.ImportFile.Name = "ImportFile";
            this.ImportFile.Size = new System.Drawing.Size(389, 21);
            this.ImportFile.TabIndex = 1;
            this.ImportFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImportFile_DragDrop);
            this.ImportFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImportPath_DragEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户端导出路径:";
            // 
            // LogList
            // 
            this.LogList.FormattingEnabled = true;
            this.LogList.ItemHeight = 12;
            this.LogList.Location = new System.Drawing.Point(15, 177);
            this.LogList.Name = "LogList";
            this.LogList.Size = new System.Drawing.Size(647, 220);
            this.LogList.TabIndex = 1;
            // 
            // mTimer
            // 
            this.mTimer.Interval = 10;
            this.mTimer.Tick += new System.EventHandler(this.mTimer_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "服务器导出路径:";
            // 
            // mServerImportPath
            // 
            this.mServerImportPath.AllowDrop = true;
            this.mServerImportPath.Location = new System.Drawing.Point(103, 88);
            this.mServerImportPath.Name = "mServerImportPath";
            this.mServerImportPath.Size = new System.Drawing.Size(389, 21);
            this.mServerImportPath.TabIndex = 8;
            this.mServerImportPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerImportPath_DragDrop);
            this.mServerImportPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.ImportPath_DragEnter);
            // 
            // mServerImportBtn
            // 
            this.mServerImportBtn.Location = new System.Drawing.Point(514, 85);
            this.mServerImportBtn.Name = "mServerImportBtn";
            this.mServerImportBtn.Size = new System.Drawing.Size(135, 23);
            this.mServerImportBtn.TabIndex = 9;
            this.mServerImportBtn.Text = "选择导出路径";
            this.mServerImportBtn.UseVisualStyleBackColor = true;
            this.mServerImportBtn.Click += new System.EventHandler(this.mServerImportBtn_Click);
            // 
            // ExcelImport
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 409);
            this.Controls.Add(this.ImportSet);
            this.Controls.Add(this.LogList);
            this.Name = "ExcelImport";
            this.Text = "Excel导出";
            this.ImportSet.ResumeLayout(false);
            this.ImportSet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ImportSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mClientImportPath;
        private System.Windows.Forms.Button ImportPathBtn;
        private System.Windows.Forms.ListBox LogList;
        private System.Windows.Forms.Button ImportFileBtn;
        private System.Windows.Forms.TextBox ImportFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StartImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer mTimer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mServerImportPath;
        private System.Windows.Forms.Button mServerImportBtn;
    }
}

