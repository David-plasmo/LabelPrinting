
namespace LabelPrinting
{
    partial class PromptPalletLabelPrint
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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStartNo = new System.Windows.Forms.TextBox();
            this.txtEndNo = new System.Windows.Forms.TextBox();
            this.cboPrinter = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStartNoInvalid = new System.Windows.Forms.Label();
            this.lblEndNoInvalid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Start Pallet No";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "End Pallet No";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Printer";
            // 
            // txtStartNo
            // 
            this.txtStartNo.Location = new System.Drawing.Point(131, 29);
            this.txtStartNo.Name = "txtStartNo";
            this.txtStartNo.Size = new System.Drawing.Size(47, 20);
            this.txtStartNo.TabIndex = 14;
            this.txtStartNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStartNo.TextChanged += new System.EventHandler(this.txtStartNo_TextChanged);
            this.txtStartNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartNo_KeyPress);
            // 
            // txtEndNo
            // 
            this.txtEndNo.Location = new System.Drawing.Point(131, 55);
            this.txtEndNo.Name = "txtEndNo";
            this.txtEndNo.Size = new System.Drawing.Size(47, 20);
            this.txtEndNo.TabIndex = 15;
            this.txtEndNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEndNo.TextChanged += new System.EventHandler(this.txtEndNo_TextChanged);
            this.txtEndNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEndNo_KeyPress);
            // 
            // cboPrinter
            // 
            this.cboPrinter.FormattingEnabled = true;
            this.cboPrinter.Location = new System.Drawing.Point(131, 81);
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.Size = new System.Drawing.Size(137, 21);
            this.cboPrinter.TabIndex = 16;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(25, 143);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(98, 30);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(276, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 30);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStartNoInvalid
            // 
            this.lblStartNoInvalid.AutoSize = true;
            this.lblStartNoInvalid.ForeColor = System.Drawing.Color.Red;
            this.lblStartNoInvalid.Location = new System.Drawing.Point(184, 32);
            this.lblStartNoInvalid.Name = "lblStartNoInvalid";
            this.lblStartNoInvalid.Size = new System.Drawing.Size(141, 13);
            this.lblStartNoInvalid.TabIndex = 22;
            this.lblStartNoInvalid.Text = "*  Start Pallet No is not valid.";
            this.lblStartNoInvalid.Visible = false;
            // 
            // lblEndNoInvalid
            // 
            this.lblEndNoInvalid.AutoSize = true;
            this.lblEndNoInvalid.ForeColor = System.Drawing.Color.Red;
            this.lblEndNoInvalid.Location = new System.Drawing.Point(184, 58);
            this.lblEndNoInvalid.Name = "lblEndNoInvalid";
            this.lblEndNoInvalid.Size = new System.Drawing.Size(138, 13);
            this.lblEndNoInvalid.TabIndex = 23;
            this.lblEndNoInvalid.Text = "*  End Pallet No is not valid.";
            this.lblEndNoInvalid.Visible = false;
            // 
            // PromptPalletLabelPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 358);
            this.Controls.Add(this.lblEndNoInvalid);
            this.Controls.Add(this.lblStartNoInvalid);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.cboPrinter);
            this.Controls.Add(this.txtEndNo);
            this.Controls.Add(this.txtStartNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Name = "PromptPalletLabelPrint";
            this.Text = "Print Labels";
            this.Load += new System.EventHandler(this.PromptLabelPrint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtStartNo;
        private System.Windows.Forms.TextBox txtEndNo;
        private System.Windows.Forms.ComboBox cboPrinter;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStartNoInvalid;
        private System.Windows.Forms.Label lblEndNoInvalid;
    }
}