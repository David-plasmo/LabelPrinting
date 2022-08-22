
namespace LabelPrinting
{
    partial class PriceList
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
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            this.dgvHeader = new System.Windows.Forms.DataGridView();
            this.ITEMNMBR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RNDGAMNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROUNDHOW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROUNDTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRCLEVEL = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.UOFM = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.UOMPRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CURNCYID = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FROMQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeader)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.txtDescription);
            this.splitContainer1.Panel1.Controls.Add(this.txtCode);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnDone);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvHeader);
            this.splitContainer1.Size = new System.Drawing.Size(1134, 450);
            this.splitContainer1.SplitterDistance = 26;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(220, 6);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(359, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(85, 6);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(129, 20);
            this.txtCode.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item";
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(721, 5);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(56, 21);
            this.btnDone.TabIndex = 0;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // dgvHeader
            // 
            this.dgvHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHeader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEMNMBR,
            this.RNDGAMNT,
            this.ROUNDHOW,
            this.ROUNDTO,
            this.PRCLEVEL,
            this.UOFM,
            this.UOMPRICE,
            this.CURNCYID,
            this.FROMQTY,
            this.TOQTY});
            this.dgvHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHeader.Location = new System.Drawing.Point(0, 0);
            this.dgvHeader.Name = "dgvHeader";
            this.dgvHeader.RowHeadersWidth = 26;
            this.dgvHeader.Size = new System.Drawing.Size(1134, 420);
            this.dgvHeader.TabIndex = 0;
            this.dgvHeader.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHeader_CellClick);
            this.dgvHeader.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHeader_CellValueChanged);
            this.dgvHeader.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvHeader_CurrentCellDirtyStateChanged);
            this.dgvHeader.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvHeader_DataError);
            this.dgvHeader.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvHeader_UserDeletingRow);
            // 
            // ITEMNMBR
            // 
            this.ITEMNMBR.HeaderText = "Item Number";
            this.ITEMNMBR.Name = "ITEMNMBR";
            this.ITEMNMBR.Visible = false;
            // 
            // RNDGAMNT
            // 
            this.RNDGAMNT.HeaderText = "Round Amount";
            this.RNDGAMNT.Name = "RNDGAMNT";
            this.RNDGAMNT.Visible = false;
            // 
            // ROUNDHOW
            // 
            this.ROUNDHOW.HeaderText = "Round Option";
            this.ROUNDHOW.Name = "ROUNDHOW";
            this.ROUNDHOW.Visible = false;
            // 
            // ROUNDTO
            // 
            this.ROUNDTO.HeaderText = "Round Policy";
            this.ROUNDTO.Name = "ROUNDTO";
            this.ROUNDTO.Visible = false;
            // 
            // PRCLEVEL
            // 
            this.PRCLEVEL.HeaderText = "Price Level";
            this.PRCLEVEL.Name = "PRCLEVEL";
            this.PRCLEVEL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PRCLEVEL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // UOFM
            // 
            this.UOFM.HeaderText = "U of M";
            this.UOFM.Name = "UOFM";
            this.UOFM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UOFM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // UOMPRICE
            // 
            this.UOMPRICE.HeaderText = "Price";
            this.UOMPRICE.Name = "UOMPRICE";
            // 
            // CURNCYID
            // 
            this.CURNCYID.HeaderText = "CurrencyID";
            this.CURNCYID.Name = "CURNCYID";
            this.CURNCYID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CURNCYID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // FROMQTY
            // 
            this.FROMQTY.HeaderText = "Start Qty";
            this.FROMQTY.Name = "FROMQTY";
            // 
            // TOQTY
            // 
            this.TOQTY.HeaderText = "End Qty";
            this.TOQTY.Name = "TOQTY";
            // 
            // PriceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PriceList";
            this.Text = "PriceList";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PriceList_FormClosed);
            this.Load += new System.EventHandler(this.PriceList_Load);
            this.Shown += new System.EventHandler(this.PriceList_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.DataGridView dgvHeader;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMNMBR;
        private System.Windows.Forms.DataGridViewTextBoxColumn RNDGAMNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROUNDHOW;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROUNDTO;
        private System.Windows.Forms.DataGridViewComboBoxColumn PRCLEVEL;
        private System.Windows.Forms.DataGridViewComboBoxColumn UOFM;
        private System.Windows.Forms.DataGridViewTextBoxColumn UOMPRICE;
        private System.Windows.Forms.DataGridViewComboBoxColumn CURNCYID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FROMQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOQTY;
    }
}