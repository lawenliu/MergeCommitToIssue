namespace MergeCommitToIssue
{
    partial class MergeMain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbTargetFolder = new System.Windows.Forms.TextBox();
            this.btnTargetFolder = new System.Windows.Forms.Button();
            this.btnCommitFolder = new System.Windows.Forms.Button();
            this.btnIssueFolder = new System.Windows.Forms.Button();
            this.tbCommitFolder = new System.Windows.Forms.TextBox();
            this.tbIssueFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cbProjectName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbProjectName);
            this.groupBox1.Controls.Add(this.tbTargetFolder);
            this.groupBox1.Controls.Add(this.btnTargetFolder);
            this.groupBox1.Controls.Add(this.btnCommitFolder);
            this.groupBox1.Controls.Add(this.btnIssueFolder);
            this.groupBox1.Controls.Add(this.tbCommitFolder);
            this.groupBox1.Controls.Add(this.tbIssueFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(30, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 299);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Folder";
            // 
            // tbTargetFolder
            // 
            this.tbTargetFolder.Location = new System.Drawing.Point(93, 259);
            this.tbTargetFolder.Name = "tbTargetFolder";
            this.tbTargetFolder.Size = new System.Drawing.Size(342, 20);
            this.tbTargetFolder.TabIndex = 3;
            // 
            // btnTargetFolder
            // 
            this.btnTargetFolder.Location = new System.Drawing.Point(501, 257);
            this.btnTargetFolder.Name = "btnTargetFolder";
            this.btnTargetFolder.Size = new System.Drawing.Size(84, 23);
            this.btnTargetFolder.TabIndex = 1;
            this.btnTargetFolder.Text = "Target Folder";
            this.btnTargetFolder.UseVisualStyleBackColor = true;
            this.btnTargetFolder.Click += new System.EventHandler(this.btnTargetFolder_Click);
            // 
            // btnCommitFolder
            // 
            this.btnCommitFolder.Location = new System.Drawing.Point(501, 173);
            this.btnCommitFolder.Name = "btnCommitFolder";
            this.btnCommitFolder.Size = new System.Drawing.Size(84, 23);
            this.btnCommitFolder.TabIndex = 1;
            this.btnCommitFolder.Text = "Commit Folder";
            this.btnCommitFolder.UseVisualStyleBackColor = true;
            this.btnCommitFolder.Click += new System.EventHandler(this.btnCommitFolder_Click);
            // 
            // btnIssueFolder
            // 
            this.btnIssueFolder.Location = new System.Drawing.Point(501, 98);
            this.btnIssueFolder.Name = "btnIssueFolder";
            this.btnIssueFolder.Size = new System.Drawing.Size(84, 23);
            this.btnIssueFolder.TabIndex = 1;
            this.btnIssueFolder.Text = "Issue Folder";
            this.btnIssueFolder.UseVisualStyleBackColor = true;
            this.btnIssueFolder.Click += new System.EventHandler(this.btnIssueFolder_Click);
            // 
            // tbCommitFolder
            // 
            this.tbCommitFolder.Location = new System.Drawing.Point(93, 173);
            this.tbCommitFolder.Name = "tbCommitFolder";
            this.tbCommitFolder.Size = new System.Drawing.Size(342, 20);
            this.tbCommitFolder.TabIndex = 3;
            // 
            // tbIssueFolder
            // 
            this.tbIssueFolder.Location = new System.Drawing.Point(93, 100);
            this.tbIssueFolder.Name = "tbIssueFolder";
            this.tbIssueFolder.Size = new System.Drawing.Size(342, 20);
            this.tbIssueFolder.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Target Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Commit Folder";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Issue Folder";
            // 
            // btnStart
            // 
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStart.Location = new System.Drawing.Point(407, 365);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(565, 365);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbProjectName
            // 
            this.cbProjectName.FormattingEnabled = true;
            this.cbProjectName.Location = new System.Drawing.Point(141, 44);
            this.cbProjectName.Name = "cbProjectName";
            this.cbProjectName.Size = new System.Drawing.Size(207, 21);
            this.cbProjectName.TabIndex = 4;
            this.cbProjectName.SelectedIndexChanged += new System.EventHandler(this.cbProjectName_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Select Project:";
            // 
            // MergeMain
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(709, 413);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "MergeMain";
            this.Text = "Merge Main Window";
            this.Load += new System.EventHandler(this.MergeMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbTargetFolder;
        private System.Windows.Forms.Button btnTargetFolder;
        private System.Windows.Forms.Button btnCommitFolder;
        private System.Windows.Forms.Button btnIssueFolder;
        private System.Windows.Forms.TextBox tbCommitFolder;
        private System.Windows.Forms.TextBox tbIssueFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbProjectName;
    }
}

