using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seagull.BarTender.Print;
using LabelPrinting;

namespace LabelPrinting
{
    public partial class MainForm : Form
    {
        delegate void SetComboBoxCellType(int iRowIndex);
        public event DataGridViewCellEventHandler RowValidated;
        //LabelTypes CurrentLabel = new LabelTypes();        
        //LabelDictionary Labels = new LabelDictionary();
        LabelTypes CurrentLabel;
        LabelDictionary Labels;

        //private bool eventHandled;
        //private int elapsedTime;

        //* no longer needed * 
        //bool bIsManufacturing = false;
        //bool bIsBlowMould = false;
        //bool bIsInjection = false;
        bool bIsPrinting = false;
        //bool bIsLabelling = false;
        //bool bIsAssembly = false;
        //bool bIsBoughtIn = false;
        //bool bIsNonStock = false;
        int lastComboIndex = 0;
        bool bIsLoading = true;
        bool bIsComboSet = false;
        bool bInvoked = false;
        bool bIsComboBox = false;
        bool bOkToCancel = false;
        bool bAbort = false;
        //bool bPlainLabel = false;
        //bool cancelValidation = true;
        //string CustNmbr = null;
        string Code = null;
        string defaultBinCode = null;
        string defaultManCode = null;
        string firstMachine = null;
        string machineType = null;
        string manCompany = null;
        //**
        int RunNmbr;
        //int JobNmbr;
        //*
        DataSet JobRun;
        DataSet Company;
        DataSet Machine;
        DataSet Customer;
        DataSet Status;
        DataSet ProductCode;
        DataSet PurchaseOrder;
        DataSet BMPrintLabels;
        DataSet BinRun;
        DataSet NonStockRun;
        DataSet PrintJobs;
        DataSet LabelSetting;
        

        Control cntObject;
        //*

        public MainForm()
        {
            InitializeComponent();           
        }
        
       
        
        
        private void LoadPrintJobs()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SaveGrid();
                //dgvJobRun.ClearSelection();
                dgvJobRun.Visible = false;
                int index = cboPrinter.FindString(CurrentLabel.DfltPrinter);
                if (index >= 0) cboPrinter.SelectedIndex = index;
                bIsComboBox = false;
                bIsLoading = true;
                int Selected;
                int count = cboLabelType.Items.Count;
                for (int i = 0; (i <= (count - 1)); i++)
                {
                    cboLabelType.SelectedIndex = i;
                    ComboBoxItem res = cboLabelType.SelectedItem as ComboBoxItem;
                    //Console.WriteLine("{0}: {1},{2}", res.Address, res.Longtitude, res.Latitude);
                    if (res.Value == CurrentLabel.LabelType)
                    {
                        Selected = i;
                        break;
                    }
                }                
                PrintJobs = new DataService.ProductDataService().PromptPrintJob((int)CurrentLabel.LabelTypeID);
                dgvJobRun.DataSource = null;
                //dgvJobRun.SuspendLayout();                
                dgvJobRun.Columns.Clear();                                                        
                PrintJobs.Tables[0].Columns["LabelTypeID"].DefaultValue = CurrentLabel.LabelTypeID;
                if (PrintJobs.Tables[0].Columns.Contains("Company"))
                    PrintJobs.Tables[0].Columns["Company"].DefaultValue = CurrentLabel.Company;
                //PrintJobs.Tables[0].Columns["Code"].DefaultValue = "05-15415ND00-10";
                //PrintJobs.Tables[0].Columns["Description"].DefaultValue = "05-15415ND00-10";
                //PrintJobs.Tables[0].Columns["NumReqd"].DefaultValue = 1;
                //PrintJobs.Tables[0].Columns["CtnQty"].DefaultValue = 1;
                dgvJobRun.DataSource = PrintJobs.Tables[0];
                //dgvJobRun.Columns["JobID"].ReadOnly = true;
                //dgvJobRun.Columns["LabelTypeId"].ReadOnly = true;
                dgvJobRun.Columns["JobID"].Visible = false;
                dgvJobRun.Columns["LabelTypeId"].Visible = false;

                dgvJobRun.Columns["Description"].ReadOnly = (CurrentLabel.LabelTypeID > 1 && CurrentLabel.LabelTypeID <= 6
                   || CurrentLabel.LabelTypeID == 11 || CurrentLabel.LabelTypeID == 12
                   || CurrentLabel.LabelTypeID >= 16 && CurrentLabel.LabelTypeID <= 21
                   || CurrentLabel.LabelTypeID == 23 || CurrentLabel.LabelTypeID == 24);

                dgvJobRun.Columns["Status"].ReadOnly = true;
                dgvJobRun.Columns["Company"].Visible = (CurrentLabel.LabelTypeID == 22) ? true : false;
                //dgvJobRun.Columns["BottleSize"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                //dgvJobRun.Columns["Style"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                //dgvJobRun.Columns["NeckSize"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                //dgvJobRun.Columns["Colour"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                //dgvJobRun.Columns["Material"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                if (PrintJobs.Tables[0].Columns.Contains("JobRun"))
                {
                    //IF @LabelTypeId <> 1 AND @LabelTypeId <> 7 AND @LabelTypeId <> 10 AND @LabelTypeId <> 15 AND @LabelTypeId < 18
                    dgvJobRun.Columns["JobRun"].Visible = (CurrentLabel.LabelTypeID != 1 && CurrentLabel.LabelTypeID != 7 && CurrentLabel.LabelTypeID != 10
                        && CurrentLabel.LabelTypeID != 15 && CurrentLabel.LabelTypeID < 18) ? true : false;
                    dgvJobRun.Columns["JobRun"].ReadOnly = true;
                }
                
                //if (CurrentLabel.LabelTypeID == 7 || CurrentLabel.LabelTypeID == 2)
                //{ 
                //    dgvJobRun.Columns["BottleSize"].ReadOnly = (CurrentLabel.LabelTypeID == 7) ? true : false;
                //    dgvJobRun.Columns["NeckSize"].ReadOnly = (CurrentLabel.LabelTypeID == 7) ? true : false;
                //    dgvJobRun.Columns["Colour"].ReadOnly = (CurrentLabel.LabelTypeID == 7) ? true : false;
                //    dgvJobRun.Columns["Material"].ReadOnly = (CurrentLabel.LabelTypeID == 7) ? true : false;
                //}

                dgvJobRun.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";//"yyyy-MM-dd HH:mm:ss.fffffff";
                dgvJobRun.Columns["last_updated_on"].Visible = true;
                dgvJobRun.Columns["last_updated_by"].Visible = true;
                dgvJobRun.Columns["last_updated_by"].ReadOnly = true;
                dgvJobRun.Columns["last_updated_on"].ReadOnly = true;

                //if (PrintJobs.Tables[0].Columns.Contains("CompanyCode"))
                //    dgvJobRun.Columns["CompanyCode"].Visible = false;
                btnSetupLabels.Enabled = false;
                btnPrint.Enabled = false;
                dgvBMLabels.Visible = false;
                //lblPrinter.Visible = false;
                //cboPrinter.Visible = false;
                lblNumSpare.Enabled = (PrintJobs.Tables[0].Rows.Count > 0 ? true : false);
                txtNumSpare.Enabled = (PrintJobs.Tables[0].Rows.Count > 0 ? true : false);
                
                ProductCode = new DataService.ProductDataService().GetProductIndex(CurrentLabel.LabelTypeID, CurrentLabel.LabelNo, CurrentLabel.Company);
                RefreshComboColumns();
                //dgvJobRun.Columns["Description"].Visible = true;
                dgvJobRun.AutoResizeColumns();
                for (int i = 0; i < dgvJobRun.ColumnCount; i++) { dgvJobRun.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable; }
                if (dgvJobRun.Rows.Count > 1)
                    dgvJobRun.CurrentCell = dgvJobRun.Rows[dgvJobRun.RowCount - 1].Cells[2];
                bIsLoading = false;
                //dgvJobRun.ResumeLayout(true);
                dgvJobRun.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bAbort = true;
                Application.Exit();
            }
        }

       
        private void RefreshComboColumns()
        {
           // dgvJobRun.Columns["Description"].Visible = false;
            DataGridViewComboBoxCell dgComboCell = null;
            foreach (DataGridViewRow row in dgvJobRun.Rows)
            {
                try
                {
                    bool bRefresh = false;                    
                    //if (CurrentLabel.LabelTypeID <= 6 
                    //    || (CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 12)
                    //    || (CurrentLabel.LabelTypeID >= 15 && CurrentLabel.LabelTypeID <= 16)
                    //    || (CurrentLabel.LabelTypeID >= 18 && CurrentLabel.LabelTypeID <= 21))
                    //{
                    //    dgComboCell = new DataGridViewComboBoxCell();
                    //    dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    //    dgComboCell.DataSource = ProductCode.Tables[0];
                    //    dgComboCell.ValueMember = "Code";
                    //    dgComboCell.DisplayMember = "Description";
                    //    row.Cells["Description"] = dgComboCell;
                    //    bRefresh = true;
                    //}
                                            
                    if (PrintJobs.Tables[0].Columns.Contains("Company"))
                    {
                        dgComboCell = new DataGridViewComboBoxCell();
                        dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        dgComboCell.DataSource = Company.Tables[0];
                        dgComboCell.ValueMember = "CompanyCode";
                        dgComboCell.DisplayMember = "CompanyName";
                        row.Cells["Company"] = dgComboCell;
                        bRefresh = true;
                    }

                    //dgvJobRun.Columns["Description"].Visible = true;
                    if (bRefresh) dgvJobRun.Refresh();
                   
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }                          
        }
        private void ChangeCellToComboBox(int iRowIndex)
        {
            try
            {
                if (dgvJobRun.CurrentCell == null || dgvJobRun.CurrentCell.ReadOnly || bOkToCancel || bInvoked)
                {
                    return;
                }                   
                DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
                dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                bIsComboBox = false;                
                if (PrintJobs.Tables[0].Columns.Contains("Company") 
                    && dgvJobRun.CurrentCell.ColumnIndex == this.dgvJobRun.Columns["Company"].Index && !dgvJobRun.CurrentCell.ReadOnly)
                {
                    dgComboCell.DataSource = Company.Tables[0];
                    dgComboCell.ValueMember = "CompanyCode";
                    dgComboCell.DisplayMember = "CompanyName";
                    bIsComboBox = true;
                }

                else if (dgvJobRun.CurrentCell.ColumnIndex == this.dgvJobRun.Columns["Status"].Index && !dgvJobRun.CurrentCell.ReadOnly)
                {
                    dgComboCell.DataSource = Status.Tables[0];
                    dgComboCell.ValueMember = "Status";
                    dgComboCell.DisplayMember = "Status";
                    bIsComboBox = true;
                }
                else if (dgvJobRun.CurrentCell.ColumnIndex == this.dgvJobRun.Columns["Code"].Index)
                {
                    if (CurrentLabel.LabelTypeID <= 6
                        || (CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 12)
                        || (CurrentLabel.LabelTypeID >= 15 && CurrentLabel.LabelTypeID <= 16)
                        || (CurrentLabel.LabelTypeID >= 18 && CurrentLabel.LabelTypeID <= 21)
                        )
                    {
                        dgComboCell.DataSource = ProductCode.Tables[0];
                        dgComboCell.ValueMember = "Code";
                        dgComboCell.DisplayMember = "Code";
                        bIsComboBox = true;
                    }
                }
                //else if (dgvJobRun.CurrentCell.ColumnIndex == this.dgvJobRun.Columns["Description"].Index  && !dgvJobRun.CurrentCell.ReadOnly)
                //{
                //    if (CurrentLabel.LabelTypeID <= 6
                //            || (CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 12)
                //            || (CurrentLabel.LabelTypeID >= 15 && CurrentLabel.LabelTypeID <= 16)
                //            || (CurrentLabel.LabelTypeID >= 18 && CurrentLabel.LabelTypeID <= 21)
                //            )
                //    {
                //        dgComboCell.DataSource = ProductCode.Tables[0];
                //        dgComboCell.ValueMember = "Code";
                //        dgComboCell.DisplayMember = "Description";
                //        bIsComboBox = true;
                //    }
                //}

                if (bIsComboBox)
                {
                    bIsComboSet = true;                    
                    dgvJobRun.Rows[iRowIndex].Cells[dgvJobRun.CurrentCell.ColumnIndex] = dgComboCell;                                        
                    //if (!dgvJobRun.IsCurrentCellInEditMode 
                    //    && dgvJobRun.CurrentCell.EditType.Name == "DataGridViewComboBoxEditingControl"
                    //    && !bIsPrinting)
                    //{
                    //   dgvJobRun.BeginEdit(true);
                    //   ComboBox cb = (ComboBox)dgvJobRun.EditingControl;
                    //   cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;                       
                    //   cb.DroppedDown = false;
                    //}                   
                    bInvoked = true;
                    lastComboIndex = dgvJobRun.CurrentCell.ColumnIndex;
                }
            }
            catch (Exception ex)
            {
                throw;                
            }
            
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

            try
            {
                //CurrentLabel = new LabelTypes();
                Labels = new LabelDictionary();
                Company = new DataService.ProductDataService().GetProductCompany();
               
                // Get the list of printers visible to BarTender (as named by printer driver)
                Printers btprinters = new Printers();

                // Get the list of Plasmo label printers
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
                    cboPrinter.SelectedIndex = 0;

                btnPrint.Enabled = false;
                //cboPrinter.Visible = false;

                ToolTip tt = new ToolTip();
                tt.InitialDelay = 1;
                tt.ShowAlways = true;
                tt.SetToolTip(lblHelp, "Help on Plasmo Label Types...");
                tt.SetToolTip(txtLastSetting, "Help on Plasmo Label Types...");
                tt.SetToolTip(cboLabelType, "Help on Plasmo Label Types...");
                tt.SetToolTip(lblMediaType, "Help on Plasmo Label Types...");
                tt.SetToolTip(lblLastSetting, "Help on Plasmo Label Types...");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        private void RefreshCaptions()
        {
            if (CurrentLabel != null)
            {
                lblProductTypeHdg.Text = CurrentLabel.Description;
                //lblProductType.Text = CurrentLabel.Description;
                lblProductTypeHdg.Visible = true;
                //lblProductType.Visible = true;  //set false when tested
                btnPrint.Enabled = false;
                
            }
        }       
        private void btnSetupLabels_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SaveGrid();
            //RefreshComboColumns();
            int i, numSpare;
            numSpare = (!int.TryParse(Convert.ToString(txtNumSpare.Text), out i) ? 0 : Convert.ToInt32(txtNumSpare.Text));
            //if (bIsManufacturing)
            //{
                if (dgvJobRun.CurrentRow.Index > -1)
                {                    
                    if (dgvJobRun.CurrentRow.Cells["Code"].Value.ToString().Length > 0)
                    {
                        int jobID = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["JobID"].Value.ToString());
                        int labelTypeId = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["LabelTypeId"].Value.ToString());
                        int startNo = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["StartNo"].Value.ToString());
                        int endNo = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["EndNo"].Value.ToString());
                        BMPrintLabels = new DataService.ProductDataService().GetLabelPrintJob(jobID, labelTypeId, numSpare, startNo, endNo);                    
                        dgvBMLabels.Columns.Clear();
                        if (BMPrintLabels != null && BMPrintLabels.Tables.Count > 0)
                        {
                            dgvBMLabels.DataSource = BMPrintLabels.Tables[0];
                            dgvBMLabels.Columns["ID"].Visible = false;
                            dgvBMLabels.Columns["BottleSize"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.Columns["Style"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.Columns["NeckSize"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.Columns["Colour"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.Columns["Material"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.Columns["JobRun"].Visible = (CurrentLabel.LabelTypeID == 2 || CurrentLabel.LabelTypeID == 3) ? true : false;
                            dgvBMLabels.Columns["BottleSize"].ReadOnly = (CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.Columns["NeckSize"].ReadOnly = (CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.Columns["Colour"].ReadOnly = (CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.Columns["Material"].ReadOnly = (CurrentLabel.LabelTypeID == 7) ? true : false;
                            dgvBMLabels.AutoResizeColumns();
                            btnPrint.Enabled = (dgvBMLabels.RowCount > 0 ? true : false);
                           // lblProductType.Text =  "Non Stock Product Labels: Job " + RunNmbr + " - " + manCompany;                              
                            lblProductType.Visible = (dgvBMLabels.RowCount > 0 ? true : false);
                            lblNumSpare.Visible = (dgvBMLabels.RowCount > 0 ? true : false);
                            txtNumSpare.Visible = (dgvBMLabels.RowCount > 0 ? true : false);
                            btnSetupLabels.Enabled = false;
                            dgvBMLabels.Visible = true;
                            lblPrinter.Visible = true;
                            cboPrinter.Visible = true;
                            dgvBMLabels.Focus();
                        }
                    }
                }
            Cursor = Cursors.Default;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {

            try
            {
                string printer = cboPrinter.SelectedItem.ToString();
                string labelNo = CurrentLabel.LabelNo;
                string desc = CurrentLabel.Description;
                if (labelNo != txtLastSetting.Text)
                {                
                    string msg = "Check " + printer + " is set for printing " + labelNo + " Labels (" + desc + ").";
                    //if (MessageBox.Show(msg, "Check Media Setting", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    SetMediaMsg smm = new SetMediaMsg(msg);
                    smm.ShowDialog();
                    if (smm.DialogResult == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                //
                //
                Cursor = Cursors.WaitCursor;
                DataService.ProductDataService ds = new DataService.ProductDataService();
                CurrentLabel.Status = "";
                CurrentLabel.ErrMsg = "";
                bIsPrinting = true;
                int? newJobRun = null;
                int numReqd = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["NumReqd"].Value.ToString());
                if (!dgvBMLabels.Visible)
                {
                    SaveGrid();
                    //RefreshComboColumns();
                    int numSpare = 0;
                    if (dgvJobRun.CurrentRow.Index > -1)
                    {
                        if (dgvJobRun.CurrentRow.Cells["Code"].Value.ToString().Length > 0)
                        {
                            int jobID = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["JobID"].Value.ToString());
                            int labelTypeId = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["LabelTypeId"].Value.ToString());
                            int startNo = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["StartNo"].Value.ToString());
                            int endNo = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["EndNo"].Value.ToString());
                            BMPrintLabels = new DataService.ProductDataService().GetLabelPrintJob(jobID, labelTypeId, numSpare, startNo, endNo);
                        }
                    }
                }
                ds.EnqueueBartenderLabels(BMPrintLabels, CurrentLabel.LabelTypeID, numReqd, ref newJobRun);
                RunPrint();

                if (newJobRun != null)
                {                                                       
                    (dgvJobRun.CurrentRow.DataBoundItem as DataRowView).Row["JobRun"] = newJobRun;
                }
                
                (dgvJobRun.CurrentRow.DataBoundItem as DataRowView).Row["Status"] = (CurrentLabel.Status != "Error")  ? CurrentLabel.Status : CurrentLabel.ErrMsg; 

                btnPrint.Enabled = false;
                dgvBMLabels.Visible = false;
                lblProductType.Visible = false;
                lblNumSpare.Visible = false;
                txtNumSpare.Visible = false;
                SaveGrid();
                btnPrint.Enabled = false;
                dgvJobRun.Visible = false;                                
                RefreshComboColumns();
                dgvJobRun.Visible = true;              
                Cursor = Cursors.Default;                            
                if (CurrentLabel.Status == "Error")
                {
                    MessageBox.Show("Error Printing " + CurrentLabel.Description + ": " + CurrentLabel.ErrMsg);
                }
                else
                {
                    // MessageBox.Show("Completed Printing " + CurrentLabel.Description);
                    MessageBox.Show("Sent to Printer.");
                    bIsPrinting = false;
                }

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bAbort = true;
                Application.Exit();
            }
            

            
        }
        private void SaveGrid()
        {            
            try
            {

                //BindingSource bs = dgvJobRun.DataSource as BindingSource;
                //if (bs != null)
                //{
                //    bs.EndEdit();
                            
                if (PrintJobs != null && PrintJobs.Tables[0].GetChanges() != null)
                {                    
                    DataService.ProductDataService ds = new DataService.ProductDataService();                   
                    ds.UpdatePrintJobs(PrintJobs, (int)CurrentLabel.LabelTypeID);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
           
                                
        private void RunPrint()
        {            
            try
            {                
                //var p = new System.Diagnostics.Process();
                var appSettings = ConfigurationManager.AppSettings;
                

                DataService.ProductDataService ds = new DataService.ProductDataService();
                string printer = appSettings[cboPrinter.SelectedItem.ToString()] ?? "Not Found";
                string labelNo = CurrentLabel.LabelNo;
                string displayBatchCounter = appSettings["DisplayBatchCounter"] ?? "Not Found";
                ds.UpdateLabelSetting(cboPrinter.SelectedItem.ToString(), labelNo);
                txtLastSetting.Text = labelNo;

                //p.StartInfo.FileName = appSettings["BarTenderExePath"] ?? "Not Found";
                string fmtFileKey = appSettings[CurrentLabel.LabelNo] ?? "Not Found";
                //check for special customer format labels
                if (CurrentLabel.LabelNo == "P5" || CurrentLabel.LabelNo == "P6")
                {
                    string[] fmtArray = fmtFileKey.Split(',');
                    foreach (string fmt in fmtArray)
                    {
                        if (fmt == dgvJobRun.CurrentRow.Cells["Code"].Value.ToString().Trim())
                        {
                            string fmtFile = appSettings[fmt] ?? "Not Found";
                            string sizes = appSettings["Sizes"] ?? "Not Found";
                            string[] lblSizes = sizes.Split(',');
                            string size = (CurrentLabel.LabelNo == "P6") ? lblSizes[0] : lblSizes[1];
                            fmtFile = fmtFile.Replace("[Size]", size);
                            //string printer = appSettings[cboPrinter.SelectedItem.ToString()] ?? "Not Found";
                            string printCmd = fmtFile.Replace("[Printer]", printer);
                            int numReqd = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["NumReqd"].Value.ToString());
                            int ctnQty = Convert.ToInt32(dgvJobRun.CurrentRow.Cells["CtnQty"].Value.ToString());
                            int numCopies = (numReqd > ctnQty && ctnQty > 0) ? numReqd / ctnQty : 1;
                            printCmd = printCmd.Replace("[n]", numCopies.ToString());
                            //p.StartInfo.Arguments = printCmd;
                            //p.Start();                            
                            RunBTPrint(printCmd);
                            break;
                        }
                    }
                }
                else
                {
                    //Check for food safe or medical label 
                    if ((CurrentLabel.LabelTypeID >= 2 && CurrentLabel.LabelTypeID <= 8)                         
                        ||(CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 14)
                        ||(CurrentLabel.LabelTypeID >= 16 && CurrentLabel.LabelTypeID <= 18)
                        ||(CurrentLabel.LabelTypeID == 23) || (CurrentLabel.LabelTypeID == 24))
                    {

                        string sFilter = "Code = '" + dgvJobRun.CurrentRow.Cells["Code"].Value.ToString().Trim() + "'";
                        DataView dv = ProductCode.Tables[0].AsDataView();
                        dv.RowFilter = sFilter;
                        DataTable dt = dv.ToTable();
                        string fmtValue = appSettings[fmtFileKey];
                        string printCmd = fmtValue.Replace("[Printer]", printer);
                        RunBTPrint(printCmd);
                        /*
                        if (dt.Rows.Count > 0  && dt.Rows[0]["Grade"] != null && dt.Rows[0]["Grade"].ToString().Length > 0 && dt.Rows[0]["Grade"].ToString() != "ST")
                        {
                            string fmtValue = appSettings[fmtFileKey];
                            //string printer = appSettings[cboPrinter.SelectedItem.ToString()] ?? "Not Found";
                            string printCmd = fmtValue.Replace("[Printer]", printer);
                            printCmd = printCmd.Replace("[Grade]", dt.Rows[0]["Grade"].ToString());
                            //p.StartInfo.Arguments = printCmd;
                            //p.Start();
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
                        //p.StartInfo.Arguments = printCmd;
                        //p.Start();
                        RunBTPrint(printCmd);
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
            // Initialize a new BarTender print engine.
            using (Engine btEngine = new Engine())
            {
                // Start the BarTender print engine.
                btEngine.Start();
                btEngine.ResponsiveTimeout = TimeSpan.FromTicks(System.Threading.Timeout.Infinite);


                // Open a format to be printed.
                //btEngine.Documents.Open(@"C:\Format1.btw");

                // Hook up to command line event.
                //btEngine.CommandLineCompleted +=
                //   new EventHandler<CommandLineCompletedEventArgs>(engine_CommandLineCompleted);

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

                // Stop the BarTender print engine.
                btEngine.Stop(SaveOptions.DoNotSaveChanges);
               // btEngine.Stop();
            }
        }

        void Engine_JobCancelled(object sender, PrintJobEventArgs printJob)
        {
            CurrentLabel.Status = "Cancelled";
        }
        void Engine_JobErrorOccurred(object sender, PrintJobEventArgs printJob)
        {
            CurrentLabel.Status = "Error";
            CurrentLabel.ErrMsg = printJob.PrinterInfo.Message;            
        }
        
        void Engine_JobPaused(object sender, PrintJobEventArgs printJob)
        {
            CurrentLabel.Status = "Paused";            
        }
        void Engine_JobQueued(object sender, PrintJobEventArgs printJob)
        {
            CurrentLabel.Status = "Printed";
        }
        void Engine_JobRestarted(object sender, PrintJobEventArgs printJob)
        {
            CurrentLabel.Status = "Restarted";
        }
        void Engine_JobResumed(object sender, PrintJobEventArgs printJob)
        {
            CurrentLabel.Status = "Resumed";
        }
        private void dgvJobRun_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

            //MessageBox.Show("Error happened " + anError.Context.ToString());
            if (anError.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
            {
                //if (anError.ColumnIndex == 4)
                //{
                //    dgvJobRun.Rows[anError.RowIndex].Cells[anError.ColumnIndex].Value = firstMachine;
                //    return;
                //}
                //if (anError.ColumnIndex == 5)
                //{
                //    dgvJobRun.Rows[anError.RowIndex].Cells[anError.ColumnIndex].Value = firstOrder;
                //    return;
                //}
            }

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                //MessageBox.Show("Commit error");
                MessageBox.Show("Integer expected");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }

        }
        
        private void dgvJobRun_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            //System.Diagnostics.Debug.Print("CellEnter:-");
            //System.Diagnostics.Debug.Print
            //    ("CellDirty=" + dgvJobRun.IsCurrentCellDirty.ToString()
            //     + "; RowDirty=" + dgvJobRun.IsCurrentRowDirty.ToString()
            //     + "; InEditMode=" + dgvJobRun.IsCurrentCellInEditMode.ToString()
            //     + "; CurrentRow=" + dgvJobRun.CurrentRow.Index.ToString()
            //     + "; LastRow=" + dgvJobRun.RowCount);


            //if (bIsLoading || dgvJobRun.IsCurrentRowDirty || dgvJobRun.CurrentRow.Index == dgvJobRun.RowCount - 2 || e.ColumnIndex == -1 )
            if (bIsLoading) // || dgvJobRun.IsCurrentCellInEditMode && bInvoked)
            {                            
                return;
            }
            //bInvoked = !bInvoked;
            if (bInvoked && bIsComboSet && e.ColumnIndex == lastComboIndex && e.ColumnIndex != 2 && e.ColumnIndex != 3)
            {
                return;
            }                
            if (((PrintJobs.Tables[0].Columns.Contains("Company") && e.ColumnIndex == this.dgvJobRun.Columns["Company"].Index)
                || e.ColumnIndex == this.dgvJobRun.Columns["Code"].Index
                //|| e.ColumnIndex == this.dgvJobRun.Columns["Description"].Index
                )
                && (CurrentLabel.LabelTypeID <= 6
                || (CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 12)
                || (CurrentLabel.LabelTypeID >= 15 && CurrentLabel.LabelTypeID <= 16)
                || (CurrentLabel.LabelTypeID >= 18 && CurrentLabel.LabelTypeID <= 21) )
               )
            {                                
                SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);
                this.dgvJobRun.BeginInvoke(objChangeCellType, e.RowIndex);               
            }                                           
        }
        private void dgvJobRun_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {


                //if (bOkToCancel)
                //{
                //    e.Cancel = true;
                //    return;
                //}

                if (bIsLoading || !dgvJobRun.IsCurrentRowDirty)
                    return;
                
                DataGridViewRow row = dgvJobRun.Rows[e.RowIndex];
               

                DataGridViewCell rowIDCell = row.Cells[dgvJobRun.Columns["JobID"].Index];
                DataGridViewCell codeCell = row.Cells[dgvJobRun.Columns["Code"].Index];
                //DataGridViewCell typeCell = row.Cells[dgvJobRun.Columns["MachineType"].Index];
                DataGridViewCell numReqdCell = row.Cells[dgvJobRun.Columns["NumReqd"].Index];
                DataGridViewCell ctnQtyCell = row.Cells[dgvJobRun.Columns["CtnQty"].Index];
                DataGridViewCell startNoCell = row.Cells[dgvJobRun.Columns["StartNo"].Index];
                DataGridViewCell endNoCell = row.Cells[dgvJobRun.Columns["EndNo"].Index];
                DataGridViewCell companyCell = null;
                if (PrintJobs.Tables[0].Columns.Contains("CompanyCode"))
                {
                    companyCell = row.Cells[dgvJobRun.Columns["Company"].Index];
                }

                bool allGood = (IsCodeGood(codeCell)
                    && (!numReqdCell.ReadOnly && IsNumReqdGood(numReqdCell) && numReqdCell.Value.ToString().Length != 0)
                    && (!ctnQtyCell.ReadOnly && IsNumReqdGood(ctnQtyCell) && ctnQtyCell.Value.ToString().Length != 0)
                    && (!startNoCell.ReadOnly && IsNumReqdGood(startNoCell) && startNoCell.Value.ToString().Length != 0)
                    && (!endNoCell.ReadOnly && IsNumReqdGood(endNoCell) && endNoCell.Value.ToString().Length != 0)
                    && (companyCell == null || companyCell != null && (!companyCell.ReadOnly && IsCompanyGood(companyCell))));
                if (allGood)
                {
                    int ctnQty = Convert.ToInt32(ctnQtyCell.Value);
                    int numReqd = Convert.ToInt32(numReqdCell.Value);
                    int numLabels = (numReqd - 1) / ctnQty + 1;
                    int startNo = Convert.ToInt32(startNoCell.Value);
                    int endNo = Convert.ToInt32(endNoCell.Value);
                    bool labelNumsOK1 = true; // (startNo <= endNo && startNo <= numLabels);
                    //if (!labelNumsOK1)
                    //{
                    //    //if (startNo > numLabels)
                    //    //{
                    //    //    endNoCell.ErrorText = "Start label number must be less than or equal to " + numLabels.ToString();
                    //    //    dgvJobRun.Rows[endNoCell.RowIndex].ErrorText = "Start label number must be less than or equal to " + numLabels.ToString();
                    //    //}
                    if (!(startNo >=  1 && startNo <= numLabels))
                    {
                        startNoCell.ErrorText = "Invalid Start label number";
                        dgvJobRun.Rows[startNoCell.RowIndex].ErrorText = "Invalid Start label number";
                        labelNumsOK1 = false;
                    }
                    //}
                    bool labelNumsOK2 = true;  //(endNo >= startNo && endNo <= numLabels);
                    //if (!labelNumsOK2)
                    //{
                    //    //if (endNo < startNo)
                    //    //{
                    //    //    endNoCell.ErrorText = "End label number must not be less than start label number.";
                    //    //    dgvJobRun.Rows[endNoCell.RowIndex].ErrorText = "End label number must not be less than start label number."; 
                    //    //}
                    //    ////if (endNo >  numLabels)
                    if (!(endNo >= startNo && endNo <= numLabels) && labelNumsOK1) 
                    {
                        endNoCell.ErrorText = "Invalid End label number";
                        dgvJobRun.Rows[endNoCell.RowIndex].ErrorText = "Invalid End label number"; 
                        labelNumsOK2 = false;
                    }
                    //}

                    e.Cancel = !(allGood && labelNumsOK1 && labelNumsOK2);
                    btnSetupLabels.Enabled = (allGood && labelNumsOK1 && labelNumsOK2);
                    btnPrint.Enabled = (allGood && labelNumsOK1 && labelNumsOK2);
                }
                else
                {
                    e.Cancel = false;
                    btnSetupLabels.Enabled = false;
                    btnPrint.Enabled = false;                    
                }
                    




            }
            catch (Exception ex)
            {
                throw;
            }                                      
        }

        private void RemoveAnnotations(Object sender, DataGridViewCellEventArgs args)
        {
            foreach (DataGridViewCell cell in
                dgvJobRun.Rows[args.RowIndex].Cells)
            {
                cell.ErrorText = String.Empty;
            }

            foreach (DataGridViewRow row in dgvJobRun.Rows)
            {
                row.ErrorText = String.Empty;
            }
        }
        private Boolean IsNumReqdGood(DataGridViewCell cell)
        {           
            int i;
            if (cell == null || cell.Value.ToString().Length == 0)
                return true;

            if (cell.Value.ToString().Length == 0 || (!int.TryParse(Convert.ToString(cell.FormattedValue), out i)))
            {
                cell.ErrorText = "Please enter an integer";
                dgvJobRun.Rows[cell.RowIndex].ErrorText =  "Please enter an integer";
                return false;
            }
            if (i <= 0 )
            {
                cell.ErrorText = "Please enter a positive integer";
                dgvJobRun.Rows[cell.RowIndex].ErrorText = "Please enter a positive integer";
                return false;
            }
            cell.ErrorText = "";
            dgvJobRun.Rows[cell.RowIndex].ErrorText = "";
            return true;
        }
        private Boolean IsNumReqdGood1(DataGridViewCell cell)
        {
            int i;
            if (cell == null)
                return true;

            if (cell.Value.ToString().Length == 0 || (!int.TryParse(Convert.ToString(cell.FormattedValue), out i)))
            {
                //cell.ErrorText = "Please enter an integer";
                //dgvJobRun.Rows[cell.RowIndex].ErrorText = "Please enter an integer";
                return false;
            }
            //cell.ErrorText = "";
            //dgvJobRun.Rows[cell.RowIndex].ErrorText = "";
            return true;
        }
        private Boolean IsCodeGood(DataGridViewCell cell)
        {            
            if (cell.FormattedValue == null || cell.FormattedValue.ToString().Length == 0 )
            {
                cell.ErrorText ="Please enter a product code";
                dgvJobRun.Rows[cell.RowIndex].ErrorText = "Please enter a product code";
                return false;
            }
            else
            {
                cell.ErrorText = "";
                dgvJobRun.Rows[cell.RowIndex].ErrorText = "";
                return true;
            }            
        }
        private Boolean IsDescGood(DataGridViewCell cell)
        {
            if (cell.Value == null || cell.Value.ToString().Length == 0 && !cell.ReadOnly)
            {
                cell.ErrorText = "Please enter a product description";
                dgvJobRun.Rows[cell.RowIndex].ErrorText = "Please enter a product description";
                return false;
            }
            else
            {
                cell.ErrorText = "";
                dgvJobRun.Rows[cell.RowIndex].ErrorText = "";
                return true;
            }
        }
        private Boolean IsCodeGood1(DataGridViewCell cell)
        {
            if (cell.Value == null || cell.Value.ToString().Length == 0)
            {
                //cell.ErrorText = "Please enter a product code";
                //dgvJobRun.Rows[cell.RowIndex].ErrorText = "Please enter a product code";
                return false;
            }
            else
            {
                //cell.ErrorText = "";
                //dgvJobRun.Rows[cell.RowIndex].ErrorText = "";
                return true;
            }

        }
        //private Boolean IsTypeGood(DataGridViewCell cell, bool noWarnings)
        //{

        //    if (cell.Value != null && cell.Value.ToString().Length == 0) // && !newRowNeeded)
        //    {
        //        cell.ErrorText = (noWarnings ? "" : "Please enter a machine type");
        //        dgvJobRun.Rows[cell.RowIndex].ErrorText = (noWarnings ? "" : "Please enter a machine type");
        //        return false;
        //    }
        //    cell.ErrorText = "";
        //    dgvJobRun.Rows[cell.RowIndex].ErrorText = "";
        //    return true;
        //}
        private Boolean IsCompanyGood(DataGridViewCell cell)
        {

            if (cell == null)
            {                
                return true;
            }
            else if (cell.Value == null || cell.Value.ToString().Length == 0)
            {
                cell.ErrorText = "Please enter a manufacturing company";
                dgvJobRun.Rows[cell.RowIndex].ErrorText = "Please enter a manufacturing company";
                return false;
            }
            else
            {
                cell.ErrorText = "";
                dgvJobRun.Rows[cell.RowIndex].ErrorText = "";
                return true;
            }     
        }
        private Boolean IsCompanyGood1(DataGridViewCell cell)
        {

            if (cell == null)
            {
                return true;
            }
            else if (cell.Value == null || cell.Value.ToString().Length == 0)
            {
                //cell.ErrorText = "Please enter a manufacturing company";
                //dgvJobRun.Rows[cell.RowIndex].ErrorText = "Please enter a manufacturing company";
                return false;
            }
            else
            {
                //cell.ErrorText = "";
                //dgvJobRun.Rows[cell.RowIndex].ErrorText = "";
                return true;
            }
        }
        private void dgvJobRun_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            
        }
        
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bAbort) SaveGrid();
        }
        private void txtNumSpare_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }       
        private void chkDone_CheckedChanged(object sender, EventArgs e)
        {
            
        }                     
        private void btnSetupBoughtIn_Click(object sender, EventArgs e)
        {

        }
        private void txtNumReqd_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }
        private void dgvBMLabels_Leave(object sender, EventArgs e)
        {
            btnSetupLabels.Enabled = true;
        }       
        
        
        private void dgvJobRun_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //MessageBox.Show("handle row deleted");
            if (!bIsLoading)
            {
                SaveGrid();
                LoadPrintJobs();
            }
            
        }
        private void dgvJobRun_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2 && e.ColumnIndex != 3)
                return;

            if (!((CurrentLabel.LabelTypeID <= 6)
                      || (CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 12)
                      || (CurrentLabel.LabelTypeID >= 15 && CurrentLabel.LabelTypeID <= 16)
                      || (CurrentLabel.LabelTypeID >= 18 && CurrentLabel.LabelTypeID <= 21)
                    )
                )
                return;


           if (e.RowIndex == dgvJobRun.RowCount - 2)
            {
                //DataGridViewRow row = dgvJobRun.Rows[e.RowIndex];
                //DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
                //dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                //dgComboCell.DataSource = ProductCode.Tables[0];
                //dgComboCell.ValueMember = "Code";
                //dgComboCell.DisplayMember = "Description";
                //row.Cells["Description"] = dgComboCell;                
            }

            //if (((CurrentLabel.LabelTypeID <= 6)
            //          || (CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 12)
            //          || (CurrentLabel.LabelTypeID >= 15 && CurrentLabel.LabelTypeID <= 16)
            //          || (CurrentLabel.LabelTypeID >= 18 && CurrentLabel.LabelTypeID <= 21)
            //        )
            //        && e.ColumnIndex == dgvJobRun.Columns["Description"].Index && e.RowIndex >= 0) //check if combobox column
            //{
            //    object selectedValue = dgvJobRun.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //    dgvJobRun.Rows[e.RowIndex].Cells["Code"].Value = selectedValue;
            //}
            //else 
            if (((CurrentLabel.LabelTypeID <= 6) 
                      || (CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 12)
                      || (CurrentLabel.LabelTypeID >= 15 && CurrentLabel.LabelTypeID <= 16)
                      || (CurrentLabel.LabelTypeID >= 18 && CurrentLabel.LabelTypeID <= 21)
                    ) 
                    && e.ColumnIndex == dgvJobRun.Columns["Code"].Index && e.RowIndex >= 0) //check if combobox column
            {
                object selectedValue = dgvJobRun.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                //dgvJobRun.Rows[e.RowIndex].Cells["Description"].Value = selectedValue;
                //dgvJobRun.Rows[e.RowIndex].Cells["Description"].ReadOnly = false;
                dgvJobRun.Rows[e.RowIndex].Cells["NumReqd"].ReadOnly = false;
                string sFilter = "Code = '" + selectedValue.ToString() + "'";
                DataView dv = ProductCode.Tables[0].AsDataView();
                dv.RowFilter = sFilter;
                DataTable dt = dv.ToTable();                
                DataGridViewRow row = dgvJobRun.Rows[e.RowIndex];
                DataGridViewCell desc = row.Cells[dgvJobRun.Columns["Description"].Index];
                DataGridViewCell ctnQty = row.Cells[dgvJobRun.Columns["CtnQty"].Index];
                DataGridViewCell numReqd = row.Cells[dgvJobRun.Columns["NumReqd"].Index];
                DataGridViewCell startNo = row.Cells[dgvJobRun.Columns["StartNo"].Index];
                DataGridViewCell endNo = row.Cells[dgvJobRun.Columns["EndNo"].Index];
                ctnQty.ReadOnly = false;
                numReqd.ReadOnly = false;
                startNo.ReadOnly = false;
                if (dt.Rows[0]["CtnQty"] != null && dt.Rows[0]["CtnQty"].ToString().Length > 0)
                {
                    ctnQty.Value = dt.Rows[0]["CtnQty"].ToString();
                    //numReqd.Value = dt.Rows[0]["CtnQty"].ToString();
                }
                if (dt.Rows[0]["Description"] != null && dt.Rows[0]["Description"].ToString().Length > 0)
                {
                    desc.Value = dt.Rows[0]["Description"].ToString();
                    //numReqd.Value = dt.Rows[0]["CtnQty"].ToString();
                }
                //startNo.Value = 1;
                //endNo.Value = 1;


            }
        }
        private void dgvJobRun_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvJobRun.IsCurrentCellDirty)
            {
                dgvJobRun.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
      
        private void MainForm_Shown(object sender, EventArgs e)
        {

            this.menuStrip1.Focus();
            this.plasmoToolStripMenuItem.ShowDropDown();
            this.plasmoToolStripMenuItem.DropDownItems[0].Select();


        }
        //private void plasmoToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}
        private void boughtInToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (Labels.TryGetValue("Plasmo BIN", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();       
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }
        private void blowMouldToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (Labels.TryGetValue("Plasmo MFG-BM", out CurrentLabel))
            {                
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LabelTypes PlainLabel;
                if (Labels.TryGetValue("Plasmo Plain BM", out PlainLabel))
                    cboLabelType.Items.Add(new ComboBoxItem(PlainLabel.LabelType, PlainLabel.LabelNo));                         
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }
        

        private void injectionMouldToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (Labels.TryGetValue("Plasmo MFG-IM", out CurrentLabel))
            {

                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LabelTypes PlainLabel;
                if (Labels.TryGetValue("Plasmo Plain IM", out PlainLabel))
                    cboLabelType.Items.Add(new ComboBoxItem(PlainLabel.LabelType, PlainLabel.LabelNo));                
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }
        
        private void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            string args = menuItem.Tag.ToString();
            MessageBox.Show("to do:  handle item click for " + args);
        }

        private void bartenderToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (Labels.TryGetValue("Other P5", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void printingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Printing", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));                
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void labellingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Labelling", out CurrentLabel))
            {
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void assemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Assembly", out CurrentLabel))
            {
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void blowMouldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("PlasmoUncoded BM", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void injectionMouldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("PlasmoUncoded IM", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void extrusionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("PlasmoUncoded EX", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void boughtInToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("CP BIN", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }

        }

        private void injectionMouldedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("CP MFG-IM", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

       

        //private void nonCodedToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}


        private void extrusionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("CP Uncoded EX", out CurrentLabel))
            {
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void injectionMouldToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("CP Uncoded IM", out CurrentLabel))
            {
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void cPInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To Do:  CP Inventory DB Maintenance");
        }

        private void cPCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To Do:  CP Category DB Maintenance");
        }

        private void angelInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To Do:  Angel Inventory DB Maintenance");
        }

        private void angelCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To Do:  Angel Category DB Maintenance");
        }

        private void boughtInToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Angel BIN", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void manufacturedToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Angel MFG", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void nonCodedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Angel Uncoded", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void customerDBMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGrid();
            EditPlainLabels epl = new EditPlainLabels();
            epl.Labels = Labels;
            epl.ShowDialog();
            this.DialogResult = DialogResult.None;
        }

        private void productionJobRunMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To Do:  Production Job Maintenance");
        }

        private void customerSpecificP6EgYatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Other P6", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void p4EgBubblesUrnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Plasmo Plain Customer", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                //LabelTypes PlainLabel = new LabelTypes();
                //if (Labels.TryGetValue("Plasmo Plain Agent", out PlainLabel))
                //    cboLabelType.Items.Add(new ComboBoxItem(PlainLabel.LabelType, PlainLabel.LabelNo));
                cboLabelType.SelectedIndex = 0;
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void p7EgKomatsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("Other P7", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }

        private void dgvJobRun_SelectionChanged(object sender, EventArgs e)
        {
            if (bIsLoading || dgvJobRun.CurrentRow == null)
                return;

            dgvBMLabels.Visible = false;
            lblProductType.Visible = false;
            lblNumSpare.Visible = false;
            txtNumSpare.Visible = false;
            
            //deselect all readonly cells other than Code 
            if (dgvJobRun.CurrentRow.Index == dgvJobRun.NewRowIndex && dgvJobRun.CurrentCell.ColumnIndex != dgvJobRun.Columns["Code"].Index) 
            {
                foreach (DataGridViewCell cell in dgvJobRun.CurrentRow.Cells)
                {
                    if (cell.ReadOnly && cell.ColumnIndex != dgvJobRun.Columns["Code"].Index) cell.Selected = false;
                }
            }

            if (dgvJobRun.CurrentRow.Index == dgvJobRun.NewRowIndex
                 && (CurrentLabel.LabelTypeID <= 6
                        || (CurrentLabel.LabelTypeID >= 10 && CurrentLabel.LabelTypeID <= 12)
                        || (CurrentLabel.LabelTypeID >= 15 && CurrentLabel.LabelTypeID <= 16)
                        || (CurrentLabel.LabelTypeID >= 18 && CurrentLabel.LabelTypeID <= 21)
                    )
               )
            {
                DataGridViewRow row = dgvJobRun.CurrentRow;
                row.Cells["Description"].ReadOnly = true;
                row.Cells["CtnQty"].ReadOnly = true;
                row.Cells["NumReqd"].ReadOnly = true;
                if (PrintJobs.Tables[0].Columns.Contains("Company")) row.Cells["Company"].ReadOnly = true;
            }
            //btnSetupLabels.Enabled = (PrintJobs.Tables[0].Rows.Count > 0 && dgvJobRun.CurrentRow.Index != dgvJobRun.NewRowIndex ? true : false);
            //btnPrint.Enabled = btnSetupLabels.Enabled;
            bInvoked = false;            
        }




        private void dgvJobRun_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //if (bIsComboSet)
            //   return;

            if (bIsLoading || !dgvJobRun.IsCurrentRowDirty)
                return;
                
            dgvBMLabels.Visible = false;
            lblProductType.Visible = false;
            lblNumSpare.Visible = false;
            txtNumSpare.Visible = false;            
            //btnSetupLabels.Enabled = (PrintJobs.Tables[0].Rows.Count > 0 && dgvJobRun.CurrentRow.Index != dgvJobRun.NewRowIndex ? true : false);
            RemoveAnnotations(sender, e);                       
            //System.Diagnostics.Debug.Print
            //    ("RowValidated:-"
            //     + "CellDirty=" + dgvJobRun.IsCurrentCellDirty.ToString()
            //     + " RowDirty=" + dgvJobRun.IsCurrentRowDirty.ToString()
            //     + " Dirty=" + dgvJobRun.IsCurrentCellInEditMode.ToString());


        }

        private void dgvJobRun_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (bIsLoading)
                return;

            if (e.RowIndex == dgvJobRun.NewRowIndex )
            {
                btnSetupLabels.Enabled = false;
                btnPrint.Enabled = false;               
            }
            else
            {
                DataGridViewRow row = dgvJobRun.Rows[e.RowIndex];
                DataGridViewCell rowIDCell = row.Cells[dgvJobRun.Columns["JobID"].Index];
                DataGridViewCell codeCell = row.Cells[dgvJobRun.Columns["Code"].Index];
                DataGridViewCell numReqdCell = row.Cells[dgvJobRun.Columns["NumReqd"].Index];
                DataGridViewCell ctnQtyCell = row.Cells[dgvJobRun.Columns["CtnQty"].Index];
                DataGridViewCell startNoCell = row.Cells[dgvJobRun.Columns["StartNo"].Index];
                DataGridViewCell endNoCell = row.Cells[dgvJobRun.Columns["EndNo"].Index];

                btnSetupLabels.Enabled = (codeCell.Value.ToString().Length > 0 && numReqdCell.Value.ToString().Length > 0
                    && ctnQtyCell.Value.ToString().Length > 0 && numReqdCell.Value.ToString().Length > 0
                    && startNoCell.Value.ToString().Length > 0 && endNoCell.Value.ToString().Length > 0);
                btnPrint.Enabled = btnSetupLabels.Enabled;
            }
            

        }

        private void cboPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string printer = cboPrinter.SelectedItem.ToString();
            LabelSetting = new DataService.ProductDataService().GetLabelSetting(printer);
            if (LabelSetting != null && LabelSetting.Tables[0].Rows.Count > 0)
                txtLastSetting.Text = LabelSetting.Tables[0].Rows[0]["LabelNo"].ToString();
            //MessageBox.Show(cboPrinter.SelectedIndex.ToString());
        }

        private void dgvJobRun_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (bIsLoading ) //   || !dgvJobRun.IsCurrentCellDirty) //|| e.RowIndex == dgvJobRun.NewRowIndex || bIsComboSet)
                    return;


                DataGridViewRow row = dgvJobRun.Rows[e.RowIndex];

                
                DataGridViewCell rowIDCell = row.Cells[dgvJobRun.Columns["JobID"].Index];
                DataGridViewCell codeCell = row.Cells[dgvJobRun.Columns["Code"].Index];
                DataGridViewCell numReqdCell = row.Cells[dgvJobRun.Columns["NumReqd"].Index];
                DataGridViewCell ctnQtyCell = row.Cells[dgvJobRun.Columns["CtnQty"].Index];
                DataGridViewCell startNoCell = row.Cells[dgvJobRun.Columns["StartNo"].Index];
                DataGridViewCell endNoCell = row.Cells[dgvJobRun.Columns["EndNo"].Index];
                DataGridViewCell companyCell = null;


                if (e.ColumnIndex == codeCell.ColumnIndex )
                {
                    if (dgvJobRun.IsCurrentCellDirty)
                        IsCodeGood(codeCell);                   
                    return;
                }
               
                if (e.ColumnIndex == numReqdCell.ColumnIndex && !numReqdCell.ReadOnly && numReqdCell != null & numReqdCell.Value.ToString().Length != 0)
                {
                    //IsNumReqdGood(numReqdCell);
                    //return;
                    if (IsNumReqdGood1(numReqdCell) && IsNumReqdGood(ctnQtyCell))
                    {
                        int numReqd = Convert.ToInt32(numReqdCell.Value);
                        int ctnQty = Convert.ToInt32(ctnQtyCell.Value);
                        endNoCell.Value = (numReqd - 1) / ctnQty + 1;
                        startNoCell.Value = 1;
                        //return;
                    }
                }
                
                else if (e.ColumnIndex == ctnQtyCell.ColumnIndex && !ctnQtyCell.ReadOnly && ctnQtyCell != null & ctnQtyCell.Value.ToString().Length != 0)
                {
                    if (IsNumReqdGood1(numReqdCell) && IsNumReqdGood(ctnQtyCell))
                    {
                        int numReqd = Convert.ToInt32(numReqdCell.Value);
                        int ctnQty = Convert.ToInt32(ctnQtyCell.Value);
                        endNoCell.Value = (numReqd - 1) / ctnQty + 1;
                        startNoCell.Value = 1;
                        //return;
                    }
                }

                //DataGridViewCell typeCell = row.Cells[dgvJobRun.Columns["MachineType"].Index];                
                //DataGridViewCell ctnQtyCell = null;
                
                else if (PrintJobs.Tables[0].Columns.Contains("CompanyCode") && !row.Cells["Company"].ReadOnly)
                {
                    companyCell = row.Cells[dgvJobRun.Columns["Company"].Index];
                    IsCompanyGood(companyCell);
                    //return;
                }
                btnSetupLabels.Enabled = (codeCell.Value.ToString().Length > 0 && numReqdCell.Value.ToString().Length > 0
               && ctnQtyCell.Value.ToString().Length > 0 && numReqdCell.Value.ToString().Length > 0
               && startNoCell.Value.ToString().Length > 0 && endNoCell.Value.ToString().Length > 0);
                btnPrint.Enabled = btnSetupLabels.Enabled;
                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {
            lblHelp.Enabled = false;
            PlasmoLabelTypes plt = new PlasmoLabelTypes();
            plt.ShowDialog();
            this.DialogResult = DialogResult.None;
            lblHelp.Enabled = true;
        }

        //private void txtMediaType_Click(object sender, EventArgs e)
        //{
        //    cboLabelType.Enabled = false;
        //    PlasmoLabelTypes plt = new PlasmoLabelTypes();
        //    plt.ShowDialog();
        //    this.DialogResult = DialogResult.None;
        //    cboLabelType.Enabled = true;
        //}

        private void txtLastSetting_Click(object sender, EventArgs e)
        {
            txtLastSetting.Enabled = false;
            PlasmoLabelTypes plt = new PlasmoLabelTypes();
            plt.ShowDialog();
            this.DialogResult = DialogResult.None;
            txtLastSetting.Enabled = true;
        }

        private void lblMediaType_Click(object sender, EventArgs e)
        {
            lblMediaType.Enabled = false;
            PlasmoLabelTypes plt = new PlasmoLabelTypes();
            plt.ShowDialog();
            this.DialogResult = DialogResult.None;
            lblMediaType.Enabled = true;
        }

        private void lblLastSetting_Click(object sender, EventArgs e)
        {
            lblLastSetting.Enabled = false;
            PlasmoLabelTypes plt = new PlasmoLabelTypes();
            plt.ShowDialog();
            this.DialogResult = DialogResult.None;
            lblLastSetting.Enabled = true;
        }

        private void dgvJobRun_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                //if (bIsLoading || e.RowIndex == dgvJobRun.NewRowIndex || bIsComboSet )
                //{                    
                //    return;
                //}
                if (bIsLoading) // || !dgvJobRun.IsCurrentCellDirty)
                    return;

                DataGridViewRow row = dgvJobRun.Rows[e.RowIndex];


                DataGridViewCell rowIDCell = row.Cells[dgvJobRun.Columns["JobID"].Index];
                DataGridViewCell codeCell = row.Cells[dgvJobRun.Columns["Code"].Index];
                DataGridViewCell descCell = row.Cells[dgvJobRun.Columns["Description"].Index];
                //DataGridViewCell typeCell = row.Cells[dgvJobRun.Columns["MachineType"].Index];
                DataGridViewCell numReqdCell = row.Cells[dgvJobRun.Columns["NumReqd"].Index];
                DataGridViewCell ctnQtyCell = row.Cells[dgvJobRun.Columns["CtnQty"].Index];
                DataGridViewCell startNoCell = row.Cells[dgvJobRun.Columns["StartNo"].Index];
                DataGridViewCell endNoCell = row.Cells[dgvJobRun.Columns["EndNo"].Index];
                DataGridViewCell companyCell = null;
                if (PrintJobs.Tables[0].Columns.Contains("Company"))
                {
                    companyCell = row.Cells[dgvJobRun.Columns["Company"].Index];
                }

                if (e.ColumnIndex == codeCell.ColumnIndex && dgvJobRun.IsCurrentCellDirty)
                {
                    //object selectedValue = dgvJobRun.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    //dgvJobRun.Rows[e.RowIndex].Cells["Code"].Value = selectedValue;
                    e.Cancel = !IsCodeGood(codeCell);                   
                }
                    
                    
                if (e.ColumnIndex == descCell.ColumnIndex && !descCell.ReadOnly)
                    e.Cancel = !IsDescGood(descCell);
                if (e.ColumnIndex == numReqdCell.ColumnIndex && !numReqdCell.ReadOnly)
                    e.Cancel = !IsNumReqdGood(numReqdCell);
                if (e.ColumnIndex == ctnQtyCell.ColumnIndex  &&!ctnQtyCell.ReadOnly)
                    e.Cancel = !IsNumReqdGood(ctnQtyCell);
                if (e.ColumnIndex == startNoCell.ColumnIndex && !startNoCell.ReadOnly)
                    e.Cancel = !IsNumReqdGood(startNoCell);
                if (e.ColumnIndex == endNoCell.ColumnIndex && !endNoCell.ReadOnly)
                    e.Cancel = !IsNumReqdGood(endNoCell);
                if (companyCell != null && e.ColumnIndex == companyCell.ColumnIndex  && !companyCell.ReadOnly)
                    e.Cancel = !IsCompanyGood(companyCell);

                //btnSetupLabels.Enabled = (IsCodeGood1(codeCell)
                //&& IsNumReqdGood1(ctnQtyCell)
                //&& IsNumReqdGood1(numReqdCell)
                //&& IsCompanyGood1(companyCell));
                //btnPrint.Enabled = btnSetupLabels.Enabled;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void dgvJobRun_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            RemoveAnnotations(sender, e);
        }

        private void dgvJobRun_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvJobRun.CurrentCell.EditType.Name == "DataGridViewComboBoxEditingControl"
                 && !bIsPrinting)
            {
                var comboBox = e.Control as DataGridViewComboBoxEditingControl;
                if (comboBox != null)
                {
                    comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                }
            }
            
        }

        private void cboLabelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem res = cboLabelType.SelectedItem as ComboBoxItem;
            LabelTypes thisLabel;
            if (Labels.TryGetValue(res.Value, out thisLabel))
                CurrentLabel = thisLabel;                
        }

        private void pastelDataImportMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGrid();
            EditPastelMaster epm = new EditPastelMaster();            
            epm.ShowDialog();
            this.DialogResult = DialogResult.None;
        }

        private void p110X10ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void p210X75ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void p310X75ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void p410X75ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void p510X6ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void p65X5ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void p75X5ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productMaterialAndGradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveGrid();
            ProductMaterialMaint pm = new ProductMaterialMaint();
            pm.ShowDialog();
            this.DialogResult = DialogResult.None;
        }

        private void extrudedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Labels.TryGetValue("CP MFG-EX", out CurrentLabel))
            {
                cboLabelType.Items.Clear();
                cboLabelType.Items.Add(new ComboBoxItem(CurrentLabel.LabelType, CurrentLabel.LabelNo));
                LoadPrintJobs();
                RefreshCaptions();
            }
            else
            {
                // do something when the value is not there
            }
        }





        //force commit
        //if (dgvJobRun.IsCurrentCellDirty)
        //{
        //    dgvJobRun.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //    btnSetupLabels.Enabled = true;
        //}


        //private void dgvJobRun_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //if (e.RowIndex == dgvJobRun.NewRowIndex)
        //    //{
        //    //    // user is in the new row, disable controls.
        //    //}
        //}
    }
}

