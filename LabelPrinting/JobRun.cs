using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace LabelPrinting
{
    public partial class JobRun : Form
    {
        //delegate void SetComboBoxCellType(int iRowIndex);
        //public event DataGridViewCellEventHandler RowValidated;

        bool bIsLoading = false;
        //bool bInvoked = false;
        //bool bOkToCancel = false;
        bool bIsComboBox = false;
        //bool bIsComboSet = false;
        //int lastComboIndex;

        ComboBox dgvCombo;

        JobRunDAL dal;
        DataSet dsJobRun, dsCompany, dsMachine, dsProductCode, dsCustomer, dsStatus;
        DateTimePicker dtp;
        //ComboBox cboCurrentCell, cboProductCode, cboCustomer, cboCompany;
        string CurrentCompany = null;

        public JobRun()
        {
            InitializeComponent();
        }

        private void dgvEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // determine if click was on our date column
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dgvEdit.Columns[e.ColumnIndex].DataPropertyName == "DateReqd")
                {
                    //initialize DateTimePicker
                    dtp = new DateTimePicker();
                    dtp.Format = DateTimePickerFormat.Short;
                    dtp.Visible = false;
                    if (dgvEdit.CurrentCell.Value != null && dgvEdit.CurrentCell.Value.ToString().Length > 0)
                    {
                        DateTime testDate; // = DateTime.MinValue;
                        if (DateTime.TryParse(dgvEdit.CurrentCell.Value.ToString(), out testDate))
                        {
                            //dtp.Text = testDate.ToShortDateString();
                            if (testDate > DateTime.MinValue && testDate < DateTime.MaxValue)
                                dtp.Value = testDate;
                        }
                        //else
                        //    dtp.Text = testDate.ToShortDateString();
                    }


                    // set size and location
                    var rect = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dtp.Size = new Size(rect.Width, rect.Height);
                    dtp.Location = new Point(rect.X, rect.Y);

                    // attach events
                    dtp.CloseUp += new EventHandler(dtp_CloseUp);
                    dtp.TextChanged += new EventHandler(dtp_OnTextChange);
                    dtp.PreviewKeyDown += new PreviewKeyDownEventHandler(dtp_OnPreviewKeyDown);

                    dgvEdit.Controls.Add(dtp);
                    dtp.Visible = true;
                }

            }
        }


        // on text change of dtp, assign back to cell        
        private void dtp_OnTextChange(object sender, EventArgs e)
        {
            dgvEdit.CurrentCell.Value = dtp.Text.ToString();
            //MessageBox.Show(dtp.Text.ToString());

        }
        // on delete key press, set to min value (null)
        private void dtp_OnKeyDown(object sender, KeyEventArgs e)
        {
        }
        private void dtp_OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // MessageBox.Show((e.KeyCode == Keys.Delete).ToString() + ", PreviewKeyDown = " + e.KeyCode.ToString());
            if (e.KeyCode == Keys.Delete)
            {
                dtp.Visible = false;
                dgvEdit.CurrentCell.Value = DateTime.MinValue;
            }
        }
        // on close of cell, hide dtp
        private void dtp_CloseUp(object sender, EventArgs e)
        {
            dtp.Visible = false;
            //MessageBox.Show("You are in the DateTimePicker.CloseUp event. dtp.Text.ToString() = " + dtp.Text.ToString());
        }
        private void JobRun_Load(object sender, EventArgs e)
        {
            bIsLoading = true;
        }


        private void RefreshGrid()
        {
            try
            {
                dgvEdit.DataSource = null;
                dgvEdit.Rows.Clear();
                dgvEdit.Columns.Clear();
                DataGridViewCellStyle style = dgvEdit.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvEdit.Font, FontStyle.Bold);
                //dgvEdit.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                dgvEdit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvEdit.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvEdit.GridColor = SystemColors.ActiveBorder;
                dgvEdit.EnableHeadersVisualStyles = false;
                dgvEdit.AutoGenerateColumns = true;

                dal = new JobRunDAL();
                dsJobRun = dal.SelectJobRun();
                DataTable dt = dsJobRun.Tables[0];
                dgvEdit.DataSource = dt;

                // Create a Print button column                
                DataGridViewImageButtonPrintColumn columnPrint =
                    new DataGridViewImageButtonPrintColumn();
                //DataGridViewImageButtonExampleColumn columnPrint =
                //   new DataGridViewImageButtonExampleColumn();
                columnPrint.Name = "Print";
                columnPrint.HeaderText = "";
                //dgvEdit.Columns.Add(columnPrint);
                dgvEdit.Columns.Insert(3, columnPrint);
                dgvEdit.Columns[4].Width = 50;  //CompanyCode
                dgvEdit.Columns[5].Width = 150;  //ProductCode
                dgvEdit.Columns[6].Width = 250; //Description
                dgvEdit.Columns[7].Width = 60;  //CtnQty
                dgvEdit.Columns[8].Width = 60;  //NumReqd
                dgvEdit.Columns[9].Width = 60;  //StartNo
                dgvEdit.Columns[10].Width = 60;  //EndNo
                dgvEdit.Columns[11].Width = 80; //JobRun
                dgvEdit.Columns[12].Width = 150; //Customer
                dgvEdit.Columns[13].Width = 100;//DateReqd
                dgvEdit.Columns[14].Width = 100;//Machine
                dgvEdit.Columns[15].Width = 65; //Priority
                dgvEdit.Columns[16].Width = 65; //NumMade
                dgvEdit.Columns[17].Width = 75; //NumScanned
                dgvEdit.Columns[18].Width = 65; //DaysToComplete
                dgvEdit.Columns[19].Width = 100;//Status
                dgvEdit.Columns[20].Width = 100;//Comment
                dgvEdit.Columns[21].Width = 100;//LastUpdatedOn
                dgvEdit.Columns[22].Width = 100;//LastUpdatedBy


                dgvEdit.Columns[0].Visible = false;
                dgvEdit.Columns[1].Visible = false;
                dgvEdit.Columns[2].Visible = false;

                dgvEdit.Columns[4].HeaderText = "Company";
                dgvEdit.Columns[5].HeaderText = "Code";
                dgvEdit.Columns[18].HeaderText = "DaysRem";
                dgvEdit.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                dgvEdit.Columns["DateReqd"].DefaultCellStyle.Format = "yyyy-MM-dd";

                dsCompany = dal.GetCompany();

                dgvEdit.Columns["CompanyCode"].Name = "Company";
                dgvEdit.Columns["Company"].HeaderText = "Comp";
                //dgvEdit.Columns["CompanyCode"].ValueType = string;
                dgvEdit.Columns["Company"].Width = 50;
                dgvEdit.Columns["Company"].SortMode = DataGridViewColumnSortMode.Automatic;


                dsStatus = dal.GetJobRunStatus();
                dgvEdit.Columns["Status"].SortMode = DataGridViewColumnSortMode.Automatic;

                dsMachine = dal.GetMachine();

                dgvEdit.Columns["Machine"].SortMode = DataGridViewColumnSortMode.Automatic;

                dgvEdit.Columns["JobRun"].ReadOnly = true;
                dgvEdit.Columns["NumMade"].ReadOnly = true;
                dgvEdit.Columns["NumScanned"].ReadOnly = true;
                dgvEdit.Columns["DaysToComplete"].ReadOnly = true;
                dgvEdit.Columns["last_updated_by"].ReadOnly = true;
                dgvEdit.Columns["last_updated_on"].ReadOnly = true;
                dgvEdit.Columns["Description"].ReadOnly = true;

                this.dgvCombo = new ComboBox();
                this.dgvCombo.Visible = false;
                this.dgvEdit.Controls.Add(this.dgvCombo);

                SetImageColumn();
                bIsLoading = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            //MessageBox.Show("Error happened = " + anError.Context.ToString());
            if (anError.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
            {
                //dgvEdit.Rows[anError.RowIndex].Cells[anError.ColumnIndex].Value.ToString()
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
                //MessageBox.Show("Integer expected");
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
            if ((anError.Exception) is FormatException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "DataGridViewComboBoxCell value is not valid";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "DataGridViewComboBoxCell value is not valid";

                anError.ThrowException = false;
            }
        }



        private void dgvEdit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0) // && !bOkToCancel)
                {                    
                    string cname = dgvEdit.Columns[e.ColumnIndex].Name;
                    string[] clist = { "CtnQty", "NumReqd", "StartNo", "EndNo" };
                    if (Array.IndexOf(clist, cname) >= 0)
                        SetImageRow(sender, e);
                    else
                    if (cname == "DateReqd")
                    {
                        if (this.dgvEdit.Rows[e.RowIndex].Cells["DateReqd"].Value.ToString() == DateTime.MinValue.ToString())
                        {
                            //this.dgvEdit.Columns["DateReqd"].DefaultCellStyle.Format = "";
                            // MessageBox.Show("cellvalueChangedTransparent");
                            this.dgvEdit.Rows[e.RowIndex].Cells["DateReqd"].Style.ForeColor = Color.Transparent;
                            //dgvEdit.RefreshEdit();

                        }
                        else
                        {
                            //MessageBox.Show("cellvalueChangedBlack");
                            this.dgvEdit.Rows[e.RowIndex].Cells["DateReqd"].Style.ForeColor = Color.Black;
                            //dgvEdit.RefreshEdit();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgvEdit_Sorted(object sender, EventArgs e)
        {
            SetImageColumn();
        }

        private void dgvEdit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool bNewJobRun;
                string curCompany, curCode;
                int curNumReqd, curJobRun, lastJobRun, numStart, numFinish, numQty;
                curJobRun = 0;
                DataGridViewRow curRow = null;

                if ((e.RowIndex > -1) && (e.ColumnIndex > -1))
                {
                    if ((dgvEdit.Columns[e.ColumnIndex].GetType().Equals(typeof(DataGridViewImageButtonSaveColumn))) ||
                        (dgvEdit.Columns[e.ColumnIndex].GetType().Equals(typeof(DataGridViewImageButtonPrintColumn))) ||
                        (dgvEdit.Columns[e.ColumnIndex].GetType().Equals(typeof(DataGridViewImageButtonEmailColumn))))
                    {
                        DataGridViewImageButtonCell buttonCell = (DataGridViewImageButtonCell)dgvEdit.Rows[e.RowIndex].Cells[e.ColumnIndex];

                        if (buttonCell.Enabled && dgvEdit.Columns[e.ColumnIndex].Name == "Print" &&
                            dgvEdit.Rows[e.RowIndex].Cells["Company"].Value != DBNull.Value &&
                            dgvEdit.Rows[e.RowIndex].Cells["Code"].Value != DBNull.Value &&
                            dgvEdit.Rows[e.RowIndex].Cells["NumReqd"].Value != DBNull.Value)
                        {
                            if (dgvEdit.Rows[e.RowIndex].Cells["JobRun"].Value == DBNull.Value ||
                                dgvEdit.Rows[e.RowIndex].Cells["JobRun"].Value == null)
                            {
                                bNewJobRun = true;
                            }
                            else
                            {
                                bNewJobRun = false;
                                curJobRun = (int)dgvEdit.Rows[e.RowIndex].Cells["JobRun"].Value;
                            }
                            if (dgvEdit.IsCurrentRowDirty)
                            {
                                this.Validate();
                            }
                            curCompany = dgvEdit.Rows[e.RowIndex].Cells["Company"].Value.ToString();
                            curCode = dgvEdit.Rows[e.RowIndex].Cells["Code"].Value.ToString().TrimEnd();
                            curNumReqd = (int)dgvEdit.Rows[e.RowIndex].Cells["NumReqd"].Value;
                            numStart = (int)dgvEdit.Rows[e.RowIndex].Cells["StartNo"].Value;
                            numFinish = (int)dgvEdit.Rows[e.RowIndex].Cells["EndNo"].Value;
                            numQty = (int)dgvEdit.Rows[e.RowIndex].Cells["CtnQty"].Value;
                            dgvEdit.EndEdit();
                            lastJobRun = 0;
                            dal.UpdateJobRun(dsJobRun, ref lastJobRun);
                            if (bNewJobRun) curJobRun = lastJobRun;
                            RefreshGrid();

                            //position grid at row being printed
                            foreach (DataGridViewRow row in dgvEdit.Rows)
                            {
                                if ((int)row.Cells["JobRun"].Value == curJobRun &&
                                    row.Cells["Company"].Value.ToString() == curCompany &&
                                    row.Cells["Code"].Value.ToString().TrimEnd() == curCode &&
                                    (int)row.Cells["NumReqd"].Value == curNumReqd)
                                {
                                    int rowIndex = row.Index;
                                    row.Selected = true;
                                    curRow = row;
                                    break;
                                }
                            }

                            //MessageBox.Show("ToDo:  print label...");
                            PromptLabelPrint f = new PromptLabelPrint();
                            DataSet ds = dal.GetProductLabelInfo(curCompany, curCode);
                            if (ds != null)
                            {
                                DataTable dt = ds.Tables[0];
                                LabelPrintJobDC dc = new LabelPrintJobDC();
                                dc.CompanyCode = dt.Rows[0]["Company"].ToString();
                                dc.Code = dt.Rows[0]["Code"].ToString();
                                //int number;
                                //if (int.TryParse(dt.Rows[0]["CtnQty"].ToString(), out number))
                                //{
                                //    dc.CtnQty = (int)dt.Rows[0]["CtnQty"];                                    
                                //    if (dc.CtnQty > 0) dc.EndNo = curNumReqd / dc.CtnQty;
                                //}
                                //
                                dc.CtnQty = numQty;
                                dc.StartNo = 1;
                                dc.LabelTypeId = (int)dt.Rows[0]["LabelTypeID"];
                                dc.NumReqd = curNumReqd;
                                dc.JobRun = curJobRun;
                                dc.EndNo = numFinish;
                                f.Description = dt.Rows[0]["Description"].ToString();
                                f.dc = dc;
                                f.LabelNo = dt.Rows[0]["LabelNo"].ToString();
                                f.DfltPrinter = dt.Rows[0]["DfltPrinter"].ToString();

                                f.ShowDialog();
                                curRow.Cells["CtnQty"].Value = numQty;
                                curRow.Cells["StartNo"].Value = dc.StartNo;
                                curRow.Cells["EndNo"].Value = dc.EndNo;
                                curRow.Cells["NumReqd"].Value = dc.NumReqd;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEdit_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (bIsLoading) // || dgvEdit.IsCurrentCellInEditMode && bInvoked)
            {
                return;
            }
            this.dgvCombo.DataSource = null;
            this.dgvCombo.Items.Clear();
            string dpName = (string)this.dgvEdit.Columns[e.ColumnIndex].DataPropertyName;
            switch (dpName)
            {
                case "CompanyCode":
                    dgvCombo.DataSource = dsCompany.Tables[0];
                    dgvCombo.ValueMember = "CompanyCode";
                    dgvCombo.DisplayMember = "CompanyCode";
                    break;
                case "Code":
                    if (this.dgvEdit["Company", e.RowIndex].Value != DBNull.Value)
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                        string compCode = (string)this.dgvEdit["Company", e.RowIndex].Value;
                        dsProductCode = dal.SelectProductMaterialAndGrade(compCode);
                        dgvCombo.ValueMember = "PmID";
                        dgvCombo.DisplayMember = "Code";
                        dgvCombo.DataSource = dsProductCode.Tables[0];
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    }
                    break;
                case "Customer":
                    if (this.dgvEdit["Company", e.RowIndex].Value != DBNull.Value)
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                        string compCode = (string)this.dgvEdit["Company", e.RowIndex].Value;
                        dsCustomer = dal.GetCustomerIndexByCompany(compCode);
                        dgvCombo.ValueMember = "CUSTNBR";
                        dgvCombo.DisplayMember = "CUSTNAME";
                        dgvCombo.DataSource = dsCustomer.Tables[0];
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    }
                    break;
                case "Machine":
                    dgvCombo.ValueMember = "MachineID";
                    dgvCombo.DisplayMember = "Machine";
                    dgvCombo.DataSource = dsMachine.Tables[0];
                    break;
                case "Status":
                    dgvCombo.ValueMember = "StatusID";
                    dgvCombo.DisplayMember = "Status";
                    dgvCombo.DataSource = dsStatus.Tables[0];
                    break;

                default:
                    return;
            }
            this.dgvCombo.SelectedIndex = 0;
            this.dgvCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.dgvCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.dgvCombo.Location = this.dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
            this.dgvCombo.Size = this.dgvEdit.CurrentCell.Size;
            this.dgvCombo.Visible = true;
        }

        private void JobRun_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                int lastJobRun = 0;
                dal.UpdateJobRun(dsJobRun, ref lastJobRun);
            }
            catch
            {
                throw;
            }
        }

        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!e.Row.IsNewRow)
            {
                DialogResult response = MessageBox.Show("Are you sure?", "Delete row?",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);

                if (response == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void dgvEdit_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvEdit.IsCurrentCellDirty)
            {
                dgvEdit.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string cname = dgvEdit.CurrentCell.OwningColumn.Name;

            //Allow integer only in these columns:-
            string[] clist = { "CtnQty", "NumReqd", "StartNo", "EndNo" };
            if (Array.IndexOf(clist, cname) >= 0)
            {
                //CheckKeyInteger(sender, e);
                e.Control.KeyPress += new KeyPressEventHandler(CheckKeyInteger_KeyPress);
            }

            //Allow date column to be set null by delete key
            if (cname == "DateReqd")
            {
                e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(dtp_OnPreviewKeyDown);

            }
        }

        private void dgvEdit_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            string dpName = (string)this.dgvEdit.Columns[e.ColumnIndex].DataPropertyName;
            if (dpName == "DateReqd")
            {
                dtp.Visible = false;
            }
        }

        private void dgvEdit_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvEdit.Columns[e.ColumnIndex].Name == "DateReqd")
            {
                if (e.Value != null)
                {
                    if (this.dgvEdit.Rows[e.RowIndex].Cells["DateReqd"].Value.ToString() == DateTime.MinValue.ToString())
                    {
                        //this.dgvEdit.Rows[e.RowIndex].Cells["DateReqd"].Style.Format = "";
                        e.CellStyle.ForeColor = Color.Transparent;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }


        private void dgvEdit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string dpName = (string)this.dgvEdit.Columns[e.ColumnIndex].DataPropertyName;
            switch (dpName)
            {
                case "CompanyCode":
                    if (this.dgvCombo.SelectedItem != null)
                    {
                        int index = this.dgvCombo.SelectedIndex;
                        DataRowView dv = this.dgvCombo.SelectedItem as DataRowView;
                        string selectedItem = string.Empty;
                        string selectedValue = string.Empty;
                        if (dv != null)
                        {
                            selectedItem = dv.Row["CompanyCode"] as string;
                            this.dgvEdit.Rows[e.RowIndex].Cells["Company"].Value = selectedItem;
                            this.dgvEdit.Rows[e.RowIndex].Cells["Code"].Value = DBNull.Value;
                            this.dgvEdit.Rows[e.RowIndex].Cells["Description"].Value = DBNull.Value;
                            this.dgvEdit.Rows[e.RowIndex].Cells["Customer"].Value = DBNull.Value;
                            this.dgvEdit.Rows[e.RowIndex].Cells["CtnQty"].Value = DBNull.Value;

                            ////set code and customer combo datasources
                            //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                            //dsProductCode = dal.SelectProductMaterialAndGrade(selectedItem);
                            //dsCustomer = dal.GetCustomerIndexByCompany(selectedItem);
                            //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        }
                    }
                    dgvCombo.Visible = false;
                    break;
                case "Code":
                    if (this.dgvCombo.SelectedItem != null)
                    {
                        int index = this.dgvCombo.SelectedIndex;
                        int selectedValue = -1;
                        DataRowView dv = this.dgvCombo.SelectedItem as DataRowView;
                        string selectedItem = string.Empty;
                        if (dv != null)
                        {
                            selectedItem = dv.Row["Code"] as string;
                            selectedValue = (int)this.dgvCombo.SelectedValue;  //PmID  -- not needed here, but that is how to get it
                            this.dgvEdit.Rows[e.RowIndex].Cells["Code"].Value = selectedItem;
                            this.dgvEdit.Rows[e.RowIndex].Cells["Description"].Value = dv.Row["Description"] as string;
                            if (dv.Row["CtnQty"] != DBNull.Value)
                            {
                                this.dgvEdit.Rows[e.RowIndex].Cells["CtnQty"].Value = (int)dv.Row["CtnQty"];
                            }
                            this.dgvEdit.Rows[e.RowIndex].Cells["NumReqd"].Value = DBNull.Value;
                            this.dgvEdit.Rows[e.RowIndex].Cells["StartNo"].Value = DBNull.Value;
                            this.dgvEdit.Rows[e.RowIndex].Cells["EndNo"].Value = DBNull.Value;
                        }
                    }
                    dgvCombo.Visible = false;
                    break;
                case "Customer":
                    if (this.dgvCombo.SelectedItem != null)
                    {
                        int index = this.dgvCombo.SelectedIndex;
                        DataRowView dv = this.dgvCombo.SelectedItem as DataRowView;
                        string selectedItem = string.Empty;
                        string selectedValue = string.Empty;
                        if (dv != null)
                        {
                            selectedItem = dv.Row["CUSTNAME"] as string;
                            selectedValue = dv.Row["CUSTNMBR"] as string;
                            this.dgvEdit.Rows[e.RowIndex].Cells["Customer"].Value = selectedItem;
                            this.dgvEdit.Rows[e.RowIndex].Cells["CUSTNMBR"].Value = selectedValue;
                        }
                    }
                    else
                    {
                        this.dgvEdit.Rows[e.RowIndex].Cells["Customer"].Value = DBNull.Value;
                        this.dgvEdit.Rows[e.RowIndex].Cells["CUSTNMBR"].Value = DBNull.Value;
                    }
                    dgvCombo.Visible = false;
                    break;
                case "Machine":
                    if (this.dgvCombo.SelectedItem != null)
                    {
                        int index = this.dgvCombo.SelectedIndex;
                        DataRowView dv = this.dgvCombo.SelectedItem as DataRowView;
                        string selectedItem = string.Empty;
                        int selectedValue = -1;
                        if (dv != null)
                        {
                            selectedItem = dv.Row["Machine"] as string;
                            selectedValue = (int)dv.Row["MachineID"];
                            this.dgvEdit.Rows[e.RowIndex].Cells["Machine"].Value = selectedItem;
                            this.dgvEdit.Rows[e.RowIndex].Cells["MachineID"].Value = selectedValue;
                        }
                    }
                    else
                    {
                        this.dgvEdit.Rows[e.RowIndex].Cells["Machine"].Value = DBNull.Value;
                        this.dgvEdit.Rows[e.RowIndex].Cells["MachineID"].Value = DBNull.Value;
                    }
                    dgvCombo.Visible = false;
                    break;

                case "Status":
                    if (this.dgvCombo.SelectedItem != null)
                    {
                        int index = this.dgvCombo.SelectedIndex;
                        DataRowView dv = this.dgvCombo.SelectedItem as DataRowView;
                        string selectedItem = string.Empty;
                        int selectedValue = -1;
                        if (dv != null)
                        {
                            selectedItem = dv.Row["Status"] as string;
                            selectedValue = (int)dv.Row["StatusID"];
                            this.dgvEdit.Rows[e.RowIndex].Cells["Status"].Value = selectedItem;
                            this.dgvEdit.Rows[e.RowIndex].Cells["StatusID"].Value = selectedValue;
                        }
                    }
                    else
                    {
                        this.dgvEdit.Rows[e.RowIndex].Cells["Status"].Value = DBNull.Value;

                    }
                    dgvCombo.Visible = false;
                    break;

                case "NumReqd":
                    CheckQty(sender, e);
                    break;

                case "StartNo":
                    CheckQty(sender, e);
                    break;

                case "EndNo":
                    CheckQty(sender, e);
                    break;

                //case "DateReqd":
                //    //if (this.dgvCombo.SelectedItem != null)
                //    //{
                //    //    int index = this.dgvCombo.SelectedIndex;
                //    //    DataRowView dv = this.dgvCombo.SelectedItem as DataRowView;
                //    //    string selectedItem = string.Empty;
                //    //    int selectedValue = -1;
                //    //    if (dv != null)
                //    //    {
                //    //        selectedItem = dv.Row["Status"] as string;
                //    //        selectedValue = (int)dv.Row["StatusID"];
                //    //        this.dgvEdit.Rows[e.RowIndex].Cells["Status"].Value = selectedItem;
                //    //        this.dgvEdit.Rows[e.RowIndex].Cells["StatusID"].Value = selectedValue;
                //    //    }
                //    //}

                //    //dtp.Visible = false;
                //    //break;
                //    //dgvEdit.CurrentCell.Style.ForeColor = Color.Transparent;
                //    //dgvEdit.RefreshEdit();
                //    //break;

                //if (dtp.Value == dtp.MinDate)
                //{
                //        this.dgvEdit.Rows[e.RowIndex].Cells["StatusID"].Value = DBNull.Value;
                //}
                //dtp.Visible = false;
                //break;

                default:
                    return;
            }
        }

        private void CheckQty(object sender, DataGridViewCellEventArgs e)
        {
            int ctnQty = 1;
            int startNo = 1;
            int endNo = 1;
            int nrq = 1;
            if (this.dgvEdit.Rows[e.RowIndex].Cells["NumReqd"].Value != DBNull.Value)
                nrq = (int)this.dgvEdit.Rows[e.RowIndex].Cells["NumReqd"].Value;
            if (nrq == 0) nrq = 1;
            if (this.dgvEdit.Rows[e.RowIndex].Cells["CtnQty"].Value != DBNull.Value)
                ctnQty = (int)this.dgvEdit.Rows[e.RowIndex].Cells["CtnQty"].Value;
            if (ctnQty == 0) ctnQty = 1;
            if (this.dgvEdit.Rows[e.RowIndex].Cells["StartNo"].Value != DBNull.Value)
                startNo = (int)this.dgvEdit.Rows[e.RowIndex].Cells["StartNo"].Value;
            else
                startNo = 1;
            if (startNo == 0) startNo = 1;
            if (this.dgvEdit.Rows[e.RowIndex].Cells["EndNo"].Value != DBNull.Value)
                endNo = (int)this.dgvEdit.Rows[e.RowIndex].Cells["EndNo"].Value;
            else
                endNo = (nrq - 1) / ctnQty + 1;
            if (endNo == 0) endNo = 1;
            if (startNo > (nrq - 1) / ctnQty + 1)
                startNo = (nrq - 1) / ctnQty + 1;
            if (endNo > (nrq - 1) / ctnQty + 1)
                endNo = (nrq - 1) / ctnQty + 1;
            this.dgvEdit.Rows[e.RowIndex].Cells["StartNo"].Value = startNo;
            this.dgvEdit.Rows[e.RowIndex].Cells["EndNo"].Value = endNo;

            SetImageRow(sender, e);
        }

        private void JobRun_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void JobRun_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }
        private void CheckKeyInteger_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        public void SetImageColumn()
        {
            DataTable dt = (DataTable)dgvEdit.DataSource;
            MemoryStream ms;

            foreach (DataGridViewRow row in dgvEdit.Rows)
            {
                if (row.IsNewRow || row.Index > dgvEdit.Rows.Count - 1)
                {
                    ((DataGridViewImageButtonCell)(row.Cells["Print"])).Enabled = false;
                    ((DataGridViewImageButtonCell)(row.Cells["Print"])).ButtonState = PushButtonState.Disabled;
                }
                else
                if (//row.Cells["JobRun"].Value != DBNull.Value && row.Cells["JobRun"].Value != null &&
                    row.Cells["Company"].Value != DBNull.Value &&
                    row.Cells["Code"].Value != DBNull.Value &&
                    row.Cells["CtnQty"].Value != DBNull.Value &&
                    row.Cells["StartNo"].Value != DBNull.Value &&
                    row.Cells["EndNo"].Value != DBNull.Value &&
                    row.Cells["NumReqd"].Value != DBNull.Value)
                {
                    ((DataGridViewImageButtonCell)(row.Cells["Print"])).Enabled = true;
                    ((DataGridViewImageButtonCell)(row.Cells["Print"])).ButtonState = PushButtonState.Normal;
                }
                else
                {
                    ((DataGridViewImageButtonCell)(row.Cells["Print"])).Enabled = false;
                    ((DataGridViewImageButtonCell)(row.Cells["Print"])).ButtonState = PushButtonState.Disabled;
                }
            }
        }
        public void SetImageRow(object sender, DataGridViewCellEventArgs e)
        {
            //DataTable dt = (DataTable)dgvEdit.DataSource;
            //MemoryStream ms;
            DataGridViewRow row = dgvEdit.Rows[e.RowIndex];
            //foreach (DataGridViewRow row in dgvEdit.Rows)
            //{
            if (//row.Cells["JobRun"].Value != DBNull.Value && row.Cells["JobRun"].Value != null &&
                row.Cells["Company"].Value != DBNull.Value &&
                row.Cells["Code"].Value != DBNull.Value &&
                row.Cells["CtnQty"].Value != DBNull.Value &&
                row.Cells["StartNo"].Value != DBNull.Value &&
                row.Cells["EndNo"].Value != DBNull.Value &&
                row.Cells["NumReqd"].Value != DBNull.Value)
            {
                ((DataGridViewImageButtonCell)(row.Cells["Print"])).Enabled = true;
                ((DataGridViewImageButtonCell)(row.Cells["Print"])).ButtonState = PushButtonState.Normal;
            }
            else
            {
                ((DataGridViewImageButtonCell)(row.Cells["Print"])).Enabled = false;
                ((DataGridViewImageButtonCell)(row.Cells["Print"])).ButtonState = PushButtonState.Disabled;
            }
            //}
        }
    }
}
