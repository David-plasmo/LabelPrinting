using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using Seagull.BarTender.Print;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace LabelPrinting
{
    public partial class PromptPalletLabelPrint : Form
    {
        //public string CompanyCode, ProductCode, Description, LabelNo, DfltPrinter;
        int  StartNo, EndNo;
        
        //public string LabelNo, DfltPrinter, Description, ItemClass;
        int NumSpare = 0;

        private void btnPrint_Click(object sender, EventArgs e)
        {    
            try
            {
                if (StartNo > 0 && EndNo > 0 && StartNo <= EndNo)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    btnPrint.Enabled = false;
                    //dc.Description = Description;
                    //LabelPrintJobDAL.AddPrintJob(ref dc);
                    new DataService.ProductDataService().PalletLabelPrintJob(StartNo, EndNo);
                    //if (chkPrintOptions.Checked)
                    //{
                    //    LabelOptions f = new LabelOptions(BMPrintLabels, dc.LabelTypeId);
                    //    f.ShowDialog();
                    //}
                    //DataService.ProductDataService pds = new DataService.ProductDataService();
                    //pds.EnqueueBartenderLabels(BMPrintLabels, dc.LabelTypeId, dc.NumReqd, dc.JobRun);
                    RunPrint();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Sent to printer.");
                    btnPrint.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Invalid CtnQty or StartNo or EndNo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
           
        }

        private void RunPrint()
        {
            try
            {
                var p = new System.Diagnostics.Process();
                var appSettings = ConfigurationManager.AppSettings;

                DataService.ProductDataService ds = new DataService.ProductDataService();
                string printer =cboPrinter.SelectedItem.ToString() ?? "Not Found";
                
                p.StartInfo.FileName = appSettings["BarTenderExePath"] ?? "Not Found";
                string fmtFileKey = "P4PlainCartonPallet";
                string fmtValue = appSettings[fmtFileKey];
                string printCmd = fmtValue.Replace("[Printer]", printer);               
                p.StartInfo.Arguments = printCmd;
                p.Start();                   
            }

            catch
            {
                throw;
            }
        }


        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void txtNumReqd_TextChanged(object sender, EventArgs e)
        //{
        //    int number;
        //    if (int.TryParse(txtNumReqd.Text, out number))
        //    {
        //        NumReqd = number;                
        //    }
        //    CheckPrintEnabled();
        //}

        private void CheckPrintEnabled()
        {
            //lblCtnQtyNotValid.Visible = false;
            lblStartNoInvalid.Visible = false;
            lblEndNoInvalid.Visible = false;
            //if (CtnQty > 0 && NumReqd > 0  && StartNo > 0 && EndNo > 0 && StartNo <= EndNo && EndNo <= (NumReqd - 1) / CtnQty + 1)
            if (StartNo > 0 && EndNo > 0 && StartNo <= EndNo )
            {
                btnPrint.Enabled = true;
            }
            else
            {
                btnPrint.Enabled = false;
                //if(dc.CtnQty ==0)
                //{
                //    lblCtnQtyNotValid.Text = "CtnQty must be greater than 0.";
                //    lblCtnQtyNotValid.Visible = true;
                //}
                //if (txtCtnQty.Text.Length == 0)
                //{
                //    lblCtnQtyNotValid.Text = "CtnQty must not be blank.";
                //    lblCtnQtyNotValid.Visible = true;
                //}
                //if (dc.NumReqd == 0)
                //{
                //    lblNumReqdNotValid.Text = "NumReqd must be greater than 0.";
                //    lblNumReqdNotValid.Visible = true;
                //}
                //if (txtNumReqd.Text.Length == 0)
                //{
                //    lblNumReqdNotValid.Text = "NumReqd must not be blank.";
                //    lblNumReqdNotValid.Visible = true;
                //}
                if (StartNo == 0)
                {
                    lblStartNoInvalid.Text = "StartNo must be greater than 0.";
                    lblStartNoInvalid.Visible = true;//MessageBox.Show("StartNo must be greater than 0");
                }
                if(txtStartNo.Text.Length == 0)
                {
                    lblStartNoInvalid.Text = "StartNo must not be blank.";
                    lblStartNoInvalid.Visible = true;
                }
                if (EndNo == 0)
                {
                    lblEndNoInvalid.Text = "EndNo must be greater than 0.";
                    lblEndNoInvalid.Visible = true;    //MessageBox.Show("EndNo must be greater than 0");
                }
                if(txtEndNo.Text.Length == 0)
                {
                    lblEndNoInvalid.Text = "EndNo must not be blank.";
                    lblEndNoInvalid.Visible = true;    //MessageBox.Show("EndNo must be greater than 0");
                }
                if (StartNo > EndNo && EndNo > 0)
                {
                    lblStartNoInvalid.Text = "StartNo must be less than or equal to EndNo.";
                    lblStartNoInvalid.Visible = true;
                    //MessageBox.Show("StartNo must be less than or equal to EndNo");
                }
                //if (EndNo > (NumReqd - 1) / CtnQty + 1)
                //{
                //    //MessageBox.Show("Invalid EndNo");
                //    lblEndNoInvalid.Text = "Invalid EndNo.";
                //    lblEndNoInvalid.Visible = true;    //MessageBox.Show("EndNo must be greater than 0");
                //}
            }
        }

        private void txtStartNo_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtStartNo.Text, out number))
            {
                StartNo = number;
            }
            CheckPrintEnabled();
        }

        private void txtEndNo_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtEndNo.Text, out number))
            {
                EndNo = number;
            }
            CheckPrintEnabled();
        }
        private void CheckInteger_OnKeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;

            // If you want, you can allow decimal(float) numbers
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
            }
        }
        private void txtNumReqd_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckInteger_OnKeyPress(sender, e);            
        }

        private void txtStartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckInteger_OnKeyPress(sender, e);
        }

        private void txtEndNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckInteger_OnKeyPress(sender, e);
        }

        //private void txtCtnQty_TextChanged(object sender, EventArgs e)
        //{
        //    int number;
        //    if (int.TryParse(txtCtnQty.Text, out number))
        //    {
        //        dc.CtnQty = number;
        //    }
        //    CheckPrintEnabled();
        //}

        //private void cboLabelType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //MessageBox.Show(cboLabelType.SelectedIndex.ToString());
        //    //dc.LabelTypeId = (int)cboLabelType.Items(cboLabelType[SelectedIndex]);
        //    ComboItem selectedLabel = cboLabelType.SelectedItem as ComboItem;
        //    dc.LabelTypeId = selectedLabel.Key;
        //    LabelNo = selectedLabel.Value;

        //}

        private void txtCtnQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckInteger_OnKeyPress(sender, e);
        }

        private void PromptLabelPrint_Load(object sender, EventArgs e)
        {
            //    txtCompany.Text = CompanyCode;
            //    txtProductCode.Text = Code;
            //    txtDescription.Text = Description;
            //    txtCtnQty.Text = CtnQty.ToString();
            //    txtNumReqd.Text = NumReqd.ToString();
           
            StartNo = 1;
            EndNo = 1;
            txtStartNo.Text = StartNo.ToString();
            txtEndNo.Text = EndNo.ToString();
            string DfltPrinter = "Zebra ZT410 (203 dpi)";
            //cboLabelType.Items.Clear();
            //List<ComboItem> cboItems = new List<ComboItem>();





            // Get the list of printers visible to BarTender (as named by printer driver)
            Printers btprinters = new Printers();
            var appSettings = ConfigurationManager.AppSettings;
            string appSetting = appSettings["Printers"] ?? "Not Found";
            string[] dfltPrinters = appSetting.Split(',');
            foreach (string prn in dfltPrinters) //plasmpo pet printer names
            {
                //check if printer is visible to Bartender
                foreach (Printer btp in btprinters)
                {
                    if (btp.PrinterName == appSettings[prn])  //this appSetting is the printer driver printer name
                        cboPrinter.Items.Add(btp);                    
                }
            }
            if (cboPrinter.Items.Count > 0)
            {
                // cboPrinter.SelectedIndex = 0;
                cboPrinter.Text = DfltPrinter;
                int index = cboPrinter.FindString(DfltPrinter);
                if (index >= 0) cboPrinter.SelectedIndex = index;
            }               
            CheckPrintEnabled();
        }

        
        public PromptPalletLabelPrint()
        {
            InitializeComponent();            
        }
    }
}
