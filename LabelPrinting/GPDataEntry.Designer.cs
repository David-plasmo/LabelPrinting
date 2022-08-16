
namespace LabelPrinting
{
    partial class GPDataEntry
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupTargetDB = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.optTargetTest = new System.Windows.Forms.RadioButton();
            this.optTargetLive = new System.Windows.Forms.RadioButton();
            this.groupInputDB = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.optTest = new System.Windows.Forms.RadioButton();
            this.optLive = new System.Windows.Forms.RadioButton();
            this.btnCommit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optCurrent = new System.Windows.Forms.RadioButton();
            this.btnGo = new System.Windows.Forms.Button();
            this.cboCode = new System.Windows.Forms.ComboBox();
            this.optExisting = new System.Windows.Forms.RadioButton();
            this.optNew = new System.Windows.Forms.RadioButton();
            this.chkConfig = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.dgvEdit = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupTargetDB.SuspendLayout();
            this.groupInputDB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupTargetDB);
            this.splitContainer1.Panel1.Controls.Add(this.groupInputDB);
            this.splitContainer1.Panel1.Controls.Add(this.btnCommit);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.chkConfig);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cboCompany);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvEdit);
            this.splitContainer1.Size = new System.Drawing.Size(1084, 875);
            this.splitContainer1.SplitterDistance = 54;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupTargetDB
            // 
            this.groupTargetDB.Controls.Add(this.label3);
            this.groupTargetDB.Controls.Add(this.optTargetTest);
            this.groupTargetDB.Controls.Add(this.optTargetLive);
            this.groupTargetDB.Location = new System.Drawing.Point(1281, 12);
            this.groupTargetDB.Name = "groupTargetDB";
            this.groupTargetDB.Size = new System.Drawing.Size(190, 27);
            this.groupTargetDB.TabIndex = 9;
            this.groupTargetDB.TabStop = false;
            this.groupTargetDB.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "TargetDB";
            // 
            // optTargetTest
            // 
            this.optTargetTest.AutoSize = true;
            this.optTargetTest.Location = new System.Drawing.Point(72, 7);
            this.optTargetTest.Name = "optTargetTest";
            this.optTargetTest.Size = new System.Drawing.Size(46, 17);
            this.optTargetTest.TabIndex = 0;
            this.optTargetTest.Text = "Test";
            this.optTargetTest.UseVisualStyleBackColor = true;
            // 
            // optTargetLive
            // 
            this.optTargetLive.AutoSize = true;
            this.optTargetLive.Checked = true;
            this.optTargetLive.Location = new System.Drawing.Point(139, 7);
            this.optTargetLive.Name = "optTargetLive";
            this.optTargetLive.Size = new System.Drawing.Size(45, 17);
            this.optTargetLive.TabIndex = 1;
            this.optTargetLive.TabStop = true;
            this.optTargetLive.Text = "Live";
            this.optTargetLive.UseVisualStyleBackColor = true;
            // 
            // groupInputDB
            // 
            this.groupInputDB.Controls.Add(this.label2);
            this.groupInputDB.Controls.Add(this.optTest);
            this.groupInputDB.Controls.Add(this.optLive);
            this.groupInputDB.Location = new System.Drawing.Point(1073, 12);
            this.groupInputDB.Name = "groupInputDB";
            this.groupInputDB.Size = new System.Drawing.Size(190, 27);
            this.groupInputDB.TabIndex = 7;
            this.groupInputDB.TabStop = false;
            this.groupInputDB.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "InputDB";
            // 
            // optTest
            // 
            this.optTest.AutoSize = true;
            this.optTest.Location = new System.Drawing.Point(72, 7);
            this.optTest.Name = "optTest";
            this.optTest.Size = new System.Drawing.Size(46, 17);
            this.optTest.TabIndex = 0;
            this.optTest.Text = "Test";
            this.optTest.UseVisualStyleBackColor = true;
            this.optTest.Click += new System.EventHandler(this.optTest_Click);
            // 
            // optLive
            // 
            this.optLive.AutoSize = true;
            this.optLive.Checked = true;
            this.optLive.Location = new System.Drawing.Point(139, 7);
            this.optLive.Name = "optLive";
            this.optLive.Size = new System.Drawing.Size(45, 17);
            this.optLive.TabIndex = 1;
            this.optLive.TabStop = true;
            this.optLive.Text = "Live";
            this.optLive.UseVisualStyleBackColor = true;
            this.optLive.Click += new System.EventHandler(this.optLive_Click);
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(1478, 13);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(78, 28);
            this.btnCommit.TabIndex = 6;
            this.btnCommit.Text = "Commit";
            this.btnCommit.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optCurrent);
            this.groupBox1.Controls.Add(this.btnGo);
            this.groupBox1.Controls.Add(this.cboCode);
            this.groupBox1.Controls.Add(this.optExisting);
            this.groupBox1.Controls.Add(this.optNew);
            this.groupBox1.Location = new System.Drawing.Point(370, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 36);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // optCurrent
            // 
            this.optCurrent.AutoSize = true;
            this.optCurrent.Checked = true;
            this.optCurrent.Location = new System.Drawing.Point(367, 12);
            this.optCurrent.Name = "optCurrent";
            this.optCurrent.Size = new System.Drawing.Size(82, 17);
            this.optCurrent.TabIndex = 5;
            this.optCurrent.TabStop = true;
            this.optCurrent.Text = "Current Item";
            this.optCurrent.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Enabled = false;
            this.btnGo.Location = new System.Drawing.Point(6, 9);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(42, 23);
            this.btnGo.TabIndex = 4;
            this.btnGo.Text = "Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cboCode
            // 
            this.cboCode.Enabled = false;
            this.cboCode.FormattingEnabled = true;
            this.cboCode.Location = new System.Drawing.Point(168, 10);
            this.cboCode.Name = "cboCode";
            this.cboCode.Size = new System.Drawing.Size(181, 21);
            this.cboCode.TabIndex = 3;
            this.cboCode.SelectedIndexChanged += new System.EventHandler(this.cboCode_SelectedIndexChanged);
            this.cboCode.TextUpdate += new System.EventHandler(this.cboCode_TextUpdate);
            // 
            // optExisting
            // 
            this.optExisting.AutoSize = true;
            this.optExisting.Location = new System.Drawing.Point(69, 12);
            this.optExisting.Name = "optExisting";
            this.optExisting.Size = new System.Drawing.Size(84, 17);
            this.optExisting.TabIndex = 1;
            this.optExisting.Text = "Lookup Item";
            this.optExisting.UseVisualStyleBackColor = true;
            this.optExisting.Click += new System.EventHandler(this.optExisting_Click);
            // 
            // optNew
            // 
            this.optNew.AutoSize = true;
            this.optNew.Location = new System.Drawing.Point(481, 13);
            this.optNew.Name = "optNew";
            this.optNew.Size = new System.Drawing.Size(70, 17);
            this.optNew.TabIndex = 0;
            this.optNew.Text = "New Item";
            this.optNew.UseVisualStyleBackColor = true;
            this.optNew.Click += new System.EventHandler(this.optNew_Click);
            // 
            // chkConfig
            // 
            this.chkConfig.AutoSize = true;
            this.chkConfig.Location = new System.Drawing.Point(967, 18);
            this.chkConfig.Name = "chkConfig";
            this.chkConfig.Size = new System.Drawing.Size(89, 17);
            this.chkConfig.TabIndex = 4;
            this.chkConfig.Text = "Show Config.";
            this.chkConfig.UseVisualStyleBackColor = true;
            this.chkConfig.CheckedChanged += new System.EventHandler(this.chkConfig_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Company";
            // 
            // cboCompany
            // 
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(75, 13);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(249, 21);
            this.cboCompany.TabIndex = 2;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // dgvEdit
            // 
            this.dgvEdit.AllowUserToAddRows = false;
            this.dgvEdit.AllowUserToDeleteRows = false;
            this.dgvEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEdit.Location = new System.Drawing.Point(0, 0);
            this.dgvEdit.Name = "dgvEdit";
            this.dgvEdit.RowHeadersWidth = 26;
            this.dgvEdit.Size = new System.Drawing.Size(1084, 817);
            this.dgvEdit.TabIndex = 0;
            this.dgvEdit.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvEdit_CellBeginEdit);
            this.dgvEdit.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEdit_CellClick);
            this.dgvEdit.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEdit_CellEndEdit);
            this.dgvEdit.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEdit_CellFormatting);
            // 
            // GPDataEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 875);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GPDataEntry";
            this.Text = "GPDataEntry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GPDataEntry_FormClosed);
            this.Load += new System.EventHandler(this.GPDataEntry_Load);
            this.Shown += new System.EventHandler(this.GPDataEntry_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupTargetDB.ResumeLayout(false);
            this.groupTargetDB.PerformLayout();
            this.groupInputDB.ResumeLayout(false);
            this.groupInputDB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton optLive;
        private System.Windows.Forms.RadioButton optTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.CheckBox chkConfig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboCode;
        private System.Windows.Forms.RadioButton optExisting;
        private System.Windows.Forms.RadioButton optNew;
        private System.Windows.Forms.Button btnCommit;
        protected internal System.Windows.Forms.DataGridView dgvEdit;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.RadioButton optCurrent;
        private System.Windows.Forms.GroupBox groupTargetDB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton optTargetTest;
        private System.Windows.Forms.RadioButton optTargetLive;
        private System.Windows.Forms.GroupBox groupInputDB;
        private System.Windows.Forms.Label label2;
    }
}