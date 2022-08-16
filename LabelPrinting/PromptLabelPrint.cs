using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using Seagull.BarTender.Print;


namespace LabelPrinting
{
    public partial class PromptLabelPrint : Form
    {
        //public string CompanyCode, ProductCode, Description, LabelNo, DfltPrinter;
        //public int CtnQty, NumReqd, StartNo, EndNo, LabelTypeID;

        public LabelPrintJobDC dc;
        public string LabelNo, DfltPrinter, Description, ItemClass;
        int NumSpare = 0;

        private void btnPrint_Click(object sender, EventArgs e)
        {    
            try
            {
                if (dc.StartNo > 0 && dc.EndNo > 0 && dc.StartNo <= dc.EndNo)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    btnPrint.Enabled = false;
                    dc.Description = Description;
                    LabelPrintJobDAL.AddPrintJob(ref dc);
                    DataSet BMPrintLabels = new DataService.ProductDataService().GetLabelPrintJob(dc.JobID, dc.LabelTypeId, NumSpare, dc.StartNo, dc.EndNo);
                    if (chkPrintOptions.Checked)
                    {
                        LabelOptions f = new LabelOptions(BMPrintLabels, dc.LabelTypeId);
                        f.ShowDialog();
                    }
                    DataService.ProductDataService pds = new DataService.ProductDataService();
                    pds.EnqueueBartenderLabels(BMPrintLabels, dc.LabelTypeId, dc.NumReqd, dc.JobRun);
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
                string printer = appSettings[cboPrinter.SelectedItem.ToString()] ?? "Not Found";
                string labelNo = LabelNo;
                string displayBatchCounter = appSettings["DisplayBatchCounter"] ?? "Not Found";
                //ds.UpdateLabelSetting(cboPrinter.SelectedItem.ToString(), labelNo);
                //txtLastSetting.Text = labelNo;

                p.StartInfo.FileName = appSettings["BarTenderExePath"] ?? "Not Found";
                string fmtFileKey = appSettings[LabelNo] ?? "Not Found";
                //check for special customer format labels
                if (LabelNo == "P5" || LabelNo == "P6")
                {
                    string[] fmtArray = fmtFileKey.Split(',');
                    foreach (string fmt in fmtArray)
                    {
                        if (fmt == dc.Code.Trim())
                        {
                            string fmtFile = appSettings[fmt] ?? "Not Found";
                            string sizes = appSettings["Sizes"] ?? "Not Found";
                            string[] lblSizes = sizes.Split(',');
                            string size = (LabelNo == "P6") ? lblSizes[0] : lblSizes[1];
                            fmtFile = fmtFile.Replace("[Size]", size);
                            //string printer = appSettings[cboPrinter.SelectedItem.ToString()] ?? "Not Found";
                            string printCmd = fmtFile.Replace("[Printer]", printer);
                            int numReqd = dc.NumReqd;
                            int ctnQty = dc.CtnQty;
                            int numCopies = (numReqd > ctnQty && ctnQty > 0) ? numReqd / ctnQty : 1;
                            printCmd = printCmd.Replace("[n]", numCopies.ToString());
                            p.StartInfo.Arguments = printCmd;
                            p.Start();                            
                            //RunBTPrint(printCmd);
                            break;
                        }
                    }
                }
                else
                {
                    //Check for food safe or medical label 
                    if ((dc.LabelTypeId >= 2 && dc.LabelTypeId <= 8)
                        || (dc.LabelTypeId >= 10 && dc.LabelTypeId <= 14)
                        || (dc.LabelTypeId >= 16 && dc.LabelTypeId <= 18)
                        || (dc.LabelTypeId == 23) || (dc.LabelTypeId == 24))
                    {

                        //string sFilter = "Code = '" + dc.Code.TrimEnd() + "'";
                        //DataView dv = ProductCode.Tables[0].AsDataView();
                        //dv.RowFilter = sFilter;
                        //DataTable dt = dv.ToTable();
                        string fmtValue = appSettings[fmtFileKey];
                        string printCmd = fmtValue.Replace("[Printer]", printer);
                        p.StartInfo.Arguments = printCmd;
                        p.Start();
                        //RunBTPrint(printCmd);
                        /*
                        if (dt.Rows.Count > 0  && dt.Rows[0]["Grade"] != null && dt.Rows[0]["Grade"].ToString().Length > 0 && dt.Rows[0]["Grade"].ToString() != "ST")
                        {
                            string fmtValue = appSettings[fmtFileKey];
                            //string printer = appSettings[cboPrinter.SelectedItem.ToString()] ?? "Not Found";
                            string printCmd = fmtValue.Replace("[Printer]", printer);
                            printCmd = printCmd.Replace("[Grade]", dt.Rows[0]["Grade"].ToString());
                            p.StartInfo.Arguments = printCmd;
                            p.Start();
                            RunBTPrint(printCmd);
                        }
                        else
                        {
                            string fmtValue = appSettings[fmtFileKey];                            
                            string printCmd = fmtValue.Replace("[Printer]", printer);
                            printCmd = printCmd.Replace("[Grade]", "ST");
                            //p.StartInfo.Arguments = printCmd;
                            //p.Start(); 
                            //string labelFmtDoc = fmtValue.Replace("[Grade]", "ST");
                            //labelFmtDoc = labelFmtDoc.Substring(labelFmtDoc.IndexOf("S:"), labelFmtDoc.LastIndexOf(".btw"));
                            //PrintJobStatusMonitor pjsm = new PrintJobStatusMonitor(labelFmtDoc);
                            //pjsm.ShowDialog();
                            RunBTPrint(printCmd);
                        }*/
                    }
                    else
                    {
                        //string printer = appSettings[cboPrinter.SelectedItem.ToString()] ?? "Not Found";
                        string printCmd = appSettings[fmtFileKey].Replace("[Printer]", printer);
                        p.StartInfo.Arguments = printCmd;
                        p.Start();
                        //RunBTPrint(printCmd);
                    }

                }

                //lblPrinter.Visible = false;
                //cboPrinter.Visible = false;
            }

            catch
            {
                throw;
            }
        }


        public void RunBTPrint(string cmdLine)
        {

            //ZPL command to set print mode for Rewind
            //^XA^MMR^JUS^XZ

            //ZPL commabd to set print mode for Tear Off
            //^XA^MTT^FS^XZ     

            // Initialize a new BarTender print engine.
            using (Engine btEngine = new Engine())
            {
                // Start the BarTender print engine.
                btEngine.Start();
                btEngine.ResponsiveTimeout = TimeSpan.FromTicks(System.Threading.Timeout.Infinite);


                // Open a format to be printed.
                //btEngine.Documents.Open(@"C:\Format1.btw");

                // Hook up to command line event.
                btEngine.CommandLineCompleted +=
                  new EventHandler<CommandLineCompletedEventArgs>(engine_CommandLineCompleted);

                // Sign up for print job events.
                btEngine.JobCancelled += new EventHandler<PrintJobEventArgs>(Engine_JobCancelled);
                btEngine.JobErrorOccurred += new EventHandler<PrintJobEventArgs>(Engine_JobErrorOccurred);
                btEngine.JobPaused += new EventHandler<PrintJobEventArgs>(Engine_JobPaused);
                btEngine.JobQueued += new EventHandler<PrintJobEventArgs>(Engine_JobQueued);
                btEngine.JobRestarted += new EventHandler<PrintJobEventArgs>(Engine_JobRestarted);
                btEngine.JobResumed += new EventHandler<PrintJobEventArgs>(Engine_JobResumed);



                // Declare a commandline and execute it
                // (this commandline will print all open formats).
                //String commandLine = "/P";
                //btEngine.CommandLine(commandLine);
                var appSettings = ConfigurationManager.AppSettings;
                string printer = appSettings[cboPrinter.SelectedItem.ToString()] ?? "Not Found";
                if (printer == "Zebra ZT410 (203 dpi)")
                {
                    string enableCounterZPL = appSettings["DisplayBatchCounter"] ?? "Not Found";
                    btEngine.CommandLine(enableCounterZPL);
                }

                //input parameter cmdLine contains the format to be printed
                btEngine.CommandLine(cmdLine); 

                // Since the commandline is processed asynchronously, we must
                // wait for printing to complete before stopping the engine.
                while (btEngine.IsProcessingCommandLines || btEngine.IsPrinting)
                    System.Threading.Thread.Sleep(2000);

                    //Stop the BarTender print engine.
                btEngine.Stop(SaveOptions.DoNotSaveChanges);
                //btEngine.Stop();
            }
        }

        void Engine_JobCancelled(object sender, PrintJobEventArgs printJob)
        {
            dc.Status = "Cancelled";
        }
        void Engine_JobErrorOccurred(object sender, PrintJobEventArgs printJob)
        {
            dc.Status = "Error";
            string ErrMsg = printJob.PrinterInfo.Message;
            MessageBox.Show(ErrMsg);
        }

        void Engine_JobPaused(object sender, PrintJobEventArgs printJob)
        {
            dc.Status = "Paused";
        }
        void Engine_JobQueued(object sender, PrintJobEventArgs printJob)
        {
            dc.Status = "Queued";
        }
        void Engine_JobRestarted(object sender, PrintJobEventArgs printJob)
        {
            dc.Status = "Restarted";
        }
        void Engine_JobResumed(object sender, PrintJobEventArgs printJob)
        {
            dc.Status = "Resumed";
        }
        void engine_CommandLineCompleted(object sender, CommandLineCompletedEventArgs printJob)
        {
            dc.Status = "CommandLine Completed";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNumReqd_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtNumReqd.Text, out number))
            {
                dc.NumReqd = number;                
            }
            CheckPrintEnabled();
        }

        private void CheckPrintEnabled()
        {
            lblCtnQtyNotValid.Visible = false;
            lblStartNoInvalid.Visible = false;
            lblEndNoInvalid.Visible = false;
            if (dc.CtnQty > 0 && dc.NumReqd > 0  && dc.StartNo > 0 && dc.EndNo > 0 && dc.StartNo <= dc.EndNo && dc.EndNo <= (dc.NumReqd - 1) / dc.CtnQty + 1)
            {
                btnPrint.Enabled = true;
            }
            else
            {
                btnPrint.Enabled = false;
                if(dc.CtnQty ==0)
                {
                    lblCtnQtyNotValid.Text = "CtnQty must be greater than 0.";
                    lblCtnQtyNotValid.Visible = true;
                }
                if (txtCtnQty.Text.Length == 0)
                {
                    lblCtnQtyNotValid.Text = "CtnQty must not be blank.";
                    lblCtnQtyNotValid.Visible = true;
                }
                if (dc.NumReqd == 0)
                {
                    lblNumReqdNotValid.Text = "NumReqd must be greater than 0.";
                    lblNumReqdNotValid.Visible = true;
                }
                if (txtNumReqd.Text.Length == 0)
                {
                    lblNumReqdNotValid.Text = "NumReqd must not be blank.";
                    lblNumReqdNotValid.Visible = true;
                }
                if (dc.StartNo == 0)
                {
                    lblStartNoInvalid.Text = "StartNo must be greater than 0.";
                    lblStartNoInvalid.Visible = true;//MessageBox.Show("StartNo must be greater than 0");
                }
                if(txtStartNo.Text.Length == 0)
                {
                    lblStartNoInvalid.Text = "StartNo must not be blank.";
                    lblStartNoInvalid.Visible = true;
                }
                if (dc.EndNo == 0)
                {
                    lblEndNoInvalid.Text = "EndNo must be greater than 0.";
                    lblEndNoInvalid.Visible = true;    //MessageBox.Show("EndNo must be greater than 0");
                }
                if(txtEndNo.Text.Length == 0)
                {
                    lblEndNoInvalid.Text = "EndNo must not be blank.";
                    lblEndNoInvalid.Visible = true;    //MessageBox.Show("EndNo must be greater than 0");
                }
                if (dc.StartNo > dc.EndNo && dc.EndNo > 0)
                {
                    lblStartNoInvalid.Text = "StartNo must be less than or equal to EndNo.";
                    lblStartNoInvalid.Visible = true;
                    //MessageBox.Show("StartNo must be less than or equal to EndNo");
                }
                if (dc.EndNo > (dc.NumReqd - 1) / dc.CtnQty + 1)
                {
                    //MessageBox.Show("Invalid EndNo");
                    lblEndNoInvalid.Text = "Invalid EndNo.";
                    lblEndNoInvalid.Visible = true;    //MessageBox.Show("EndNo must be greater than 0");
                }
            }
        }

        private void txtStartNo_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtStartNo.Text, out number))
            {
                dc.StartNo = number;
            }
            CheckPrintEnabled();
        }

        private void txtEndNo_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtEndNo.Text, out number))
            {
                dc.EndNo = number;
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

        private void txtCtnQty_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtCtnQty.Text, out number))
            {
                dc.CtnQty = number;
            }
            CheckPrintEnabled();
        }

        private void cboLabelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cboLabelType.SelectedIndex.ToString());
            //dc.LabelTypeId = (int)cboLabelType.Items(cboLabelType[SelectedIndex]);
            ComboItem selectedLabel = cboLabelType.SelectedItem as ComboItem;
            dc.LabelTypeId = selectedLabel.Key;
            LabelNo = selectedLabel.Value;

        }

        private void txtCtnQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckInteger_OnKeyPress(sender, e);
        }

        private void PromptLabelPrint_Load(object sender, EventArgs e)
        {
            txtCompany.Text = dc.CompanyCode;
            txtProductCode.Text = dc.Code;
            txtDescription.Text = Description;
            txtCtnQty.Text = dc.CtnQty.ToString();
            txtNumReqd.Text = dc.NumReqd.ToString();
            txtStartNo.Text = dc.StartNo.ToString();
            txtEndNo.Text = dc.EndNo.ToString();
            cboLabelType.Items.Clear();
            List<ComboItem> cboItems = new List<ComboItem>();
            



            //special labels for Yates products
            if (dc.Code.Contains("09-YAT"))
            {
                cboItems.Add(new ComboItem(3, "P2"));
                cboItems.Add(new ComboItem(19, "P5"));
                cboItems.Add(new ComboItem(20, "P6"));
                LabelNo = "P5";
                dc.LabelTypeId = 19;
            }  
            // Specific Jars - to be treated like Customer Injection Mould
            //Changed 29/06/22, request by Susan Brennan.  Labels now to be printed on Plasmo Labels, on Plasmo Cartons
            //else if (dc.Code.Contains("08-3000J-6PH"))  
            //{
            //    cboItems.Add(new ComboItem(3, "P2"));
            //    cboItems.Add(new ComboItem(23, "P4"));
            //    LabelNo = "P4";                
            //}
            //allow large size of plain label print option for Plasmo Blow Mould                               
            else if (dc.LabelTypeId == 2 && ItemClass.Trim() != "BLOW-CUS")
            {
                cboItems.Add(new ComboItem(dc.LabelTypeId, LabelNo));
                cboItems.Add(new ComboItem(24, "P1b"));
            }
            //allow smaller size of plain label print option for Customer-owned Plasmo Blow Mould 
            else if (dc.LabelTypeId == 2 && ItemClass.Trim() == "BLOW-CUS")
            {                                
                cboItems.Add(new ComboItem(dc.LabelTypeId, LabelNo));  // LabelNo will be P1, for Plasmo Injection Mould
                cboItems.Add(new ComboItem(23, "P4"));
                LabelNo = "P4";
            }                
            //allow plain label print option for Plasmo Injection Mould Customer owned labels
            else if (dc.LabelTypeId == 3 && ItemClass.Trim() == "INJ-M-CUS")
            {
                cboItems.Add(new ComboItem(dc.LabelTypeId, LabelNo));
                cboItems.Add(new ComboItem(23, "P4"));
                LabelNo = "P4";
            }                
            else
                cboLabelType.Items.Add(new ComboItem(dc.LabelTypeId, LabelNo));

            cboLabelType.DataSource = cboItems;
            cboLabelType.DisplayMember = "Value";
            cboLabelType.ValueMember = "Key";
            cboLabelType.Text = LabelNo;
            cboLabelType.Enabled = cboLabelType.Items.Count > 1;

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
                        cboPrinter.Items.Add(prn);
                    //cboPrinter.Items.Add(btp);
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

        
        public PromptLabelPrint()
        {
            InitializeComponent();            
        }
    }
}
