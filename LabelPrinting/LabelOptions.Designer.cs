
namespace LabelPrinting
{
    partial class LabelOptions
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
            this.dgvLabels = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLabels
            // 
            this.dgvLabels.AllowUserToAddRows = false;
            this.dgvLabels.AllowUserToDeleteRows = false;
            this.dgvLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLabels.Location = new System.Drawing.Point(0, 0);
            this.dgvLabels.Name = "dgvLabels";
            this.dgvLabels.Size = new System.Drawing.Size(1346, 450);
            this.dgvLabels.TabIndex = 0;
            // 
            // LabelOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 450);
            this.Controls.Add(this.dgvLabels);
            this.Name = "LabelOptions";
            this.Text = "LabelOptions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LabelOptions_FormClosing);
            this.Load += new System.EventHandler(this.LabelOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLabels;
    }
}