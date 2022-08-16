using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPrinting
{
    public partial class PriceList : Form
    {
        public bool CloseNow;
        public string CurrentItemNmbr;
        public string CurrentDescription;
        public string DatabaseName;
        public int CurrentEntryType;


        public DataSet dsCurrency;
        public DataSet dsUOFM;

        //public event System.Windows.Forms.DataGridViewCellEventHandler CellValueChanged;
        //public event System.Windows.Forms.DataGridViewRowsRemovedEventHandler RowsRemoved;

        //public delegate void DataGridViewCellEventHandler(object? sender, DataGridViewCellEventArgs e);

        private DataSet dsPriceListDesc;
        private DataSet dsPriceListH;  //header     IV00107
        private DataSet dsPriceListQ;  //quantities IV00108
        private DataSet dsPriceLevel;
        private PriceListDAL dal;

        private DataGridView dgvDetail;
        private DataGridViewButtonCell buttonCell;



        private bool bIsLoading;
        private bool buttonCellEnabled = true;
        private bool buttonCellExpanded = false;

        public PriceList()
        {
            InitializeComponent();
            CloseNow = false;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                //    //click the collapse/expand button on the owner form (!!)
                //    GPDataEntry f = Owner as GPDataEntry;
                //    f.dgvEdit_CellClick(f.dgvEdit, new DataGridViewCellEventArgs(f.dgvEdit.CurrentCell.ColumnIndex, f.dgvEdit.CurrentRow.Index));
                CollapseDetail();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                DoSave();
                if (!CloseNow)
                {
                    //click the collapse/expand button on the owner form (!!)
                    GPDataEntry f = Owner as GPDataEntry;
                    f.dgvEdit_CellClick(f.dgvEdit, new DataGridViewCellEventArgs(f.dgvEdit.CurrentCell.ColumnIndex, f.dgvEdit.CurrentRow.Index));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DoSave()
        {
            try
            {
                //MessageBox.Show("Do Save");
                if (dgvHeader.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvHeader.EndEdit();
                dal.UpdatePriceList(dsPriceListH);
                if(dgvDetail != null && dsPriceListQ != null)
                {
                    dgvDetail.EndEdit();
                    dal.UpdatePriceList(dsPriceListQ);
                }          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void PriceList_Load(object sender, EventArgs e)
        {
            txtCode.Text = CurrentItemNmbr;
            txtDescription.Text = CurrentDescription;
        }
        void RefreshGrid()
        {
            try
            {
                bIsLoading = true;
                dal = new PriceListDAL();
                dsPriceListDesc = dal.GetPriceListDescription();
                dsPriceListH = dal.GetGPPriceListForEdit(DatabaseName, CurrentEntryType, CurrentItemNmbr);
                dgvHeader.Columns.Clear();
                DataTable dt = dsPriceListH.Tables[0];
                dt.Columns["ITEMNMBR"].DefaultValue = CurrentItemNmbr;
                dt.Columns["DatabaseName"].DefaultValue = DatabaseName;
                dt.Columns["CURNCYID"].DefaultValue = "Z-AUD";
                dt.Columns["PRCLEVEL"].DefaultValue = "STANDARD";
                dt.Columns["TableName"].DefaultValue = "IV00107";
                dt.Columns["UOFM"].DefaultValue = "EACH";
                dt.Columns["RNDGAMNT"].DefaultValue = 0.00000;
                dt.Columns["ROUNDHOW"].DefaultValue = 0;
                dt.Columns["ROUNDTO"].DefaultValue = 1;
                dt.Columns["UMSLSOPT"].DefaultValue = 2;

                DataView dv = dsPriceListH.Tables[0].DefaultView;                
                dgvHeader.DataSource = dv;

                DataGridViewCellStyle style = dgvHeader.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.Navy;
                style.ForeColor = Color.White;
                style.Font = new Font(dgvHeader.Font, FontStyle.Bold);
                //dgvHeader.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                dgvHeader.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                dgvHeader.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvHeader.GridColor = SystemColors.ActiveBorder;
                dgvHeader.EnableHeadersVisualStyles = false;
                dgvHeader.AutoGenerateColumns = true;

                //Setup columns 
                DataView dvd = dsPriceListDesc.Tables[0].DefaultView;
                for (var i = 0; i < dgvHeader.ColumnCount; i++)
                {
                    string sFilter = "ColumnName= '" + dgvHeader.Columns[i].Name + "'";
                    dvd.RowFilter = sFilter;
                    dgvHeader.Columns[i].Visible = false;
                    if (dvd.Count > 0)
                    {
                        string sHeader = (string)dvd[0]["Description"];
                        dgvHeader.Columns[i].HeaderText = sHeader;
                        string name = dgvHeader.Columns[i].Name.Trim();
                        string hiddenNames = "ITEMNMBR ID TableName";
                        bool excluded = hiddenNames.Contains(name);
                        bool visible = ((bool)dvd[0]["reqd"] && !excluded);
                        dgvHeader.Columns[i].Visible = visible;

                        //set up combobox columns
                        if (dgvHeader.Columns[i].Name == "ROUNDHOW")
                        {
                            DataGridViewComboBoxColumn cbcRoundHow = new DataGridViewComboBoxColumn();
                            cbcRoundHow.Name = "ROUNDHOW";
                            DataTable dth = new DataTable();
                            dth.Columns.AddRange(new DataColumn[] { new DataColumn("Text"), new DataColumn("ROUNDHOW", typeof(int)) });
                            dth.Rows.Add("0=None", 0);
                            dth.Rows.Add("1=Multiple of", 1);
                            dth.Rows.Add("2=Ends in", 2);
                            cbcRoundHow.DataSource = dth;
                            cbcRoundHow.DisplayMember = "Text";
                            cbcRoundHow.ValueMember = "ROUNDHOW";
                            //cbcRoundHow.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            cbcRoundHow.ValueType = typeof(int);
                            cbcRoundHow.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                            cbcRoundHow.DisplayStyleForCurrentCellOnly = true;
                            //cbcRoundHow.SelectedIndex = 0;
                            dgvHeader.Columns.Remove("ROUNDHOW");
                            cbcRoundHow.HeaderText = sHeader;
                            dgvHeader.Columns.Insert(i, cbcRoundHow);
                            dgvHeader.Columns[i].Name = "ROUNDHOW";
                            cbcRoundHow.DataPropertyName = "ROUNDHOW";
                            //dgvHeader.Columns.Add(cbcRoundHow);
                        }
                        if (dgvHeader.Columns[i].Name == "ROUNDTO")
                        {
                            DataGridViewComboBoxColumn cbcRoundTo = new DataGridViewComboBoxColumn();
                            cbcRoundTo.Name = "ROUNDTO";
                            DataTable dt2 = new DataTable();
                            dt2.Columns.AddRange(new DataColumn[] { new DataColumn("Text"), new DataColumn("ROUNDTO", typeof(int)) });
                            dt2.Rows.Add("1=None", 1);
                            dt2.Rows.Add("2=Up", 2);
                            dt2.Rows.Add("3=Down", 3);
                            dt2.Rows.Add("4=To nearest", 4);
                            cbcRoundTo.DataSource = dt2;
                            cbcRoundTo.DisplayMember = "Text";
                            cbcRoundTo.ValueMember = "ROUNDTO";
                            //cbcRoundTo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            cbcRoundTo.ValueType = typeof(int);
                            cbcRoundTo.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                            cbcRoundTo.DisplayStyleForCurrentCellOnly = true;
                            //cbcRoundTo.SelectedIndex = 0;
                            dgvHeader.Columns.Remove("ROUNDTO");
                            cbcRoundTo.HeaderText = sHeader;
                            dgvHeader.Columns.Insert(i, cbcRoundTo);
                            dgvHeader.Columns[i].Name = "ROUNDTO";
                            cbcRoundTo.DataPropertyName = "ROUNDTO";
                            //dgvHeader.Columns.Add(cbcRoundHow);
                        }
                        if (dgvHeader.Columns[i].Name == "UMSLSOPT")
                        {
                            DataTable dtso = new DataTable();
                            dtso.Columns.AddRange(new DataColumn[] { new DataColumn("Text"), new DataColumn("Value", typeof(int)) });
                            dtso.Rows.Add("1=Not available", 1);
                            dtso.Rows.Add("2=Whole", 2);
                            dtso.Rows.Add("3=Whole and fractional", 3);
                            DataGridViewComboBoxColumn cbcUOMSellingOpt = new DataGridViewComboBoxColumn();
                            cbcUOMSellingOpt.DataSource = dtso;
                            cbcUOMSellingOpt.DisplayMember = "Text";
                            cbcUOMSellingOpt.ValueMember = "Value";
                            cbcUOMSellingOpt.ValueType = typeof(int);
                            cbcUOMSellingOpt.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                            cbcUOMSellingOpt.DisplayStyleForCurrentCellOnly = true;
                            //cbcUOMSellingOpt.SelectedIndex = 0;
                            dgvHeader.Columns.Remove("UMSLSOPT");
                            cbcUOMSellingOpt.HeaderText = sHeader;
                            dgvHeader.Columns.Insert(i, cbcUOMSellingOpt);
                            dgvHeader.Columns[i].Name = "UMSLSOPT";
                            cbcUOMSellingOpt.DataPropertyName = "UMSLSOPT";
                        }
                        if (dgvHeader.Columns[i].Name == "CURNCYID")
                        {
                            DataGridViewComboBoxColumn cbcCurrency = new DataGridViewComboBoxColumn();
                            cbcCurrency.DataSource = dsCurrency.Tables[0];
                            cbcCurrency.DisplayMember = "CURNCYID";
                            cbcCurrency.ValueMember = "CURNCYID";
                            cbcCurrency.ValueType = typeof(string);
                            cbcCurrency.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                            cbcCurrency.DisplayStyleForCurrentCellOnly = true;
                            //cbcCurrency.SelectedIndex = 0;
                            dgvHeader.Columns.Remove("CURNCYID");
                            cbcCurrency.HeaderText = sHeader;
                            dgvHeader.Columns.Insert(i, cbcCurrency);
                            dgvHeader.Columns[i].Name = "CURNCYID";
                            cbcCurrency.DataPropertyName = "CURNCYID";
                        }
                        if (dgvHeader.Columns[i].Name == "UOFM")
                        {
                            DataGridViewComboBoxColumn cbcUOM = new DataGridViewComboBoxColumn();
                            cbcUOM.DataSource = null;
                            cbcUOM.Items.Clear();
                            cbcUOM.DataSource = dsUOFM.Tables[0];
                            cbcUOM.DisplayMember = "UOMSCHDL";
                            cbcUOM.ValueMember = "UOMSCHDL";
                            cbcUOM.ValueType = typeof(string);
                            cbcUOM.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                            cbcUOM.DisplayStyleForCurrentCellOnly = true;
                            //cbcUOM.SelectedIndex = 0;
                            dgvHeader.Columns.Remove("UOFM");
                            cbcUOM.HeaderText = sHeader;
                            dgvHeader.Columns.Insert(i, cbcUOM);
                            dgvHeader.Columns[i].Name = "UOFM";
                            cbcUOM.DataPropertyName = "UOFM";
                        }
                        if (dgvHeader.Columns[i].Name == "PRCLEVEL")
                        {
                            DataGridViewComboBoxColumn cbcPriceLevel = new DataGridViewComboBoxColumn();
                            cbcPriceLevel.DataSource = null;
                            cbcPriceLevel.Items.Clear();
                            dsPriceLevel = dal.SelectPriceLevel(DatabaseName);
                            cbcPriceLevel.DataSource = dsPriceLevel.Tables[0];
                            cbcPriceLevel.DisplayMember = "DisplayValue";
                            cbcPriceLevel.ValueMember = "PRCLEVEL";
                            cbcPriceLevel.ValueType = typeof(string);
                            cbcPriceLevel.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                            cbcPriceLevel.DisplayStyleForCurrentCellOnly = true;
                            cbcPriceLevel.DropDownWidth = 300;
                            dgvHeader.Columns.Remove("PRCLEVEL");
                            cbcPriceLevel.HeaderText = sHeader;
                            dgvHeader.Columns.Insert(i, cbcPriceLevel);
                            dgvHeader.Columns[i].Name = "PRCLEVEL";
                            cbcPriceLevel.DataPropertyName = "PRCLEVEL";                            
                        }                        
                    }
                }
                dgvHeader.Columns["RNDGAMNT"].DefaultCellStyle.Format = "N5";
                //dgvHeader.Columns["displayValue"].HeaderText = "Value";
                //DataGridViewDisableButtonColumn column1 = new DataGridViewDisableButtonColumn();
                var column1 = new DataGridViewButtonColumn();
                column1.Name = "Buttons";
                column1.HeaderText = "";
                column1.Width = 45;
                column1.UseColumnTextForButtonValue = false;
                column1.DefaultCellStyle = new DataGridViewCellStyle()
                {
                    NullValue = "",
                    Font = new Font("WingDings 3", 8, FontStyle.Regular)
                };
                dgvHeader.Columns.Add(column1);               
                dgvHeader.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);                
                bIsLoading = false;

                //show expand button on first row only
                if (dv.Count > 0)
                {
                    dgvHeader.Rows[0].Cells["Buttons"].Value = "s";
                    buttonCellEnabled = true;
                    buttonCellExpanded = false;
                }

                //add validating event handlers
                dgvHeader.CellValidating += new DataGridViewCellValidatingEventHandler(dgvHeader_CellValidating);
                dgvHeader.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvHeader_EditingControlShowing);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PriceList_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dgvHeader_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvHeader_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;

                //if (dgvHeader.Columns[e.ColumnIndex].Name == "Buttons")
                //{                    
                //    buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                //    buttonCellEnabled = (buttonCell.Value != DBNull.Value && buttonCell.Value != null);
                //    if (buttonCellEnabled)
                //    {
                //        //show expand icon "s"
                //        buttonCell.Style.Font = new Font("WingDings 3", 8);
                //        buttonCellExpanded = ((string)buttonCell.Value == "s") ? false : true;
                //    }

                //    else
                //        buttonCell.Value = DBNull.Value;


                //    dgvHeader.Invalidate();
                //}
                string checkColumns = "[CURNCYID],[PRCLEVEL],[UOFM]"; //,[UMSLSOPT],[RNDGAMNT],[ROUNDHOW],[ROUNDTO]";
                string name = dgvHeader.Columns[e.ColumnIndex].Name.Trim();
                if (checkColumns.Contains(name))
                {
                    DataGridViewCell currency = dgvHeader.Rows[e.RowIndex].Cells["CURNCYID"];
                    DataGridViewCell pricelevel = dgvHeader.Rows[e.RowIndex].Cells["PRCLEVEL"];
                    DataGridViewCell uofm = dgvHeader.Rows[e.RowIndex].Cells["UOFM"];
                    buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                    bool rowOK = (currency.FormattedValue.ToString().Length != 0 && pricelevel.FormattedValue.ToString().Length != 0 && uofm.FormattedValue.ToString().Length != 0);
                    if (rowOK)
                    {
                        buttonCellExpanded = false;
                        buttonCellEnabled = true;
                        buttonCell.Style.Font = new Font("Wingdings 3", 8, FontStyle.Regular);
                        buttonCell.Value = "s"; //show expand arrow      
                    }
                    else
                    {
                        buttonCell.Value = DBNull.Value; //show blank button face
                        buttonCellEnabled = false;
                    }
                    //remove expand/collapse icon for all other rows
                    foreach (DataGridViewRow row in dgvHeader.Rows)
                    {
                        //if (row.Cells[1].Value.ToString().Equals(searchValue))
                        if (row.Index != e.RowIndex)
                        {
                            //rowIndex = row.Index;
                            //break;
                            dgvHeader.Rows[row.Index].Cells["Buttons"].Value = DBNull.Value;
                        }
                    }
                }
                                
                dgvHeader.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvHeader_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvHeader.IsCurrentCellDirty)
            {
                dgvHeader.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvHeader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || dgvHeader.CurrentRow.IsNewRow)
                    return;

                if (dgvHeader.Columns[e.ColumnIndex].Name == "Buttons")
                {
                    //enables expand/collapse button for appropriate row
                    //DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                    //DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                    buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                    DataGridViewRow row = dgvHeader.Rows[e.RowIndex];
                    buttonCellEnabled = (buttonCell.Value != DBNull.Value && buttonCell.Value != null);
                    if (buttonCellEnabled)
                    {
                        if (!buttonCellExpanded)
                        {
                            //if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList")
                            //{
                            buttonCellExpanded = true;
                            buttonCell.Style.Font = new Font("WingDings 3", 8);
                            buttonCell.Value = "r";
                            //buttonCell.Value = "s";  //show collapse arrow
                            //MessageBox.Show("Do expand");
                            //fpl = new PriceList();
                            //fpl.CurrentItemNmbr = CurrentCode;
                            //fpl.CurrentDescription = CurrentDesc;
                            //fpl.dsCurrency = dsCurrency;
                            //fpl.dsUOFM = dsUOFM;
                            //fpl.DatabaseName = GPDbase;
                            //fpl.CurrentEntryType = EntryType;
                            //fpl.Show(this);

                            string curncyID = row.Cells["CURNCYID"].Value.ToString();
                            string prcLevel = row.Cells["PRCLEVEL"].Value.ToString();
                            string uofm = row.Cells["UOFM"].Value.ToString();
                            dsPriceListQ = dal.SelectPriceListQty(CurrentItemNmbr, curncyID, prcLevel, uofm);
                            dgvDetail = new DataGridView();
                            DataTable dt = dsPriceListQ.Tables[0];
                            dt.Columns["ITEMNMBR"].DefaultValue = CurrentItemNmbr;
                            dt.Columns["DatabaseName"].DefaultValue = DatabaseName;
                            dt.Columns["CURNCYID"].DefaultValue = "Z-AUD";
                            dt.Columns["PRCLEVEL"].DefaultValue = "STANDARD";
                            dt.Columns["TableName"].DefaultValue = "IV00108";
                            dt.Columns["UOFM"].DefaultValue = "EACH";
                            dt.Columns["UOMPRICE"].DefaultValue = 0.00000;
                            dt.Columns["FROMQTY"].DefaultValue = 1.00000;
                            dt.Columns["TOQTY"].DefaultValue = 999999999999.00000;

                            dgvDetail.DataSource = dsPriceListQ.Tables[0];
                            DataGridViewCellStyle style = dgvDetail.ColumnHeadersDefaultCellStyle;
                            style.BackColor = Color.Navy;
                            style.ForeColor = Color.White;
                            style.Font = new Font(dgvDetail.Font, FontStyle.Bold);
                            //dgvDetail.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                            dgvDetail.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                            dgvDetail.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                            dgvDetail.GridColor = SystemColors.ActiveBorder;
                            dgvDetail.EnableHeadersVisualStyles = false;
                            dgvDetail.RowHeadersWidth = 26;
                            dgvDetail.AutoSize = true;
                            dgvDetail.AutoGenerateColumns = true;                                                        
                            dgvDetail.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                            //this.Controls.Add(dgvDetail);
                            this.splitContainer1.Panel2.Controls.Add(dgvDetail);

                            //position detail grid below current cell row
                            DataGridViewCell cell = dgvHeader.Rows[e.RowIndex].Cells[e.ColumnIndex];
                            Point point = dgvHeader.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                            int Ypos = point.Y + cell.Size.Height;
                            dgvDetail.Location = new Point(this.dgvHeader.Location.X, this.dgvHeader.Location.Y + Ypos);
                            dgvDetail.BringToFront();
                            //dgvDetail.Visible = true;

                            //Setup column headings 
                            DataView dvd = dsPriceListDesc.Tables[0].DefaultView;
                            for (var i = 0; i < dgvDetail.ColumnCount; i++)
                            {
                                string sFilter = "ColumnName= '" + dgvDetail.Columns[i].Name + "'";
                                dvd.RowFilter = sFilter;
                                if (dvd.Count > 0)
                                {
                                    string sHeader = (string)dvd[0]["Description"];
                                    string name = dgvDetail.Columns[i].Name.Trim();
                                    string hiddenNames = "ITEMNMBR CURNCYID PRCLEVEL UOFM ID TableName";
                                    bool excluded = hiddenNames.Contains(name);
                                    bool visible = ((bool)dvd[0]["reqd"] && !excluded);
                                    dgvDetail.Columns[i].HeaderText = sHeader;
                                    dgvDetail.Columns[i].Visible = visible;
                                }
                                else
                                    dgvDetail.Columns[i].Visible = false;
                            }

                            dgvDetail.Visible = true;

                            dgvDetail.Columns["UOMPRICE"].DefaultCellStyle.Format = "N5";
                            dgvDetail.Columns["FROMQTY"].DefaultCellStyle.Format = "N5";
                            dgvDetail.Columns["TOQTY"].DefaultCellStyle.Format = "N5";
                            dgvDetail.Columns["FROMQTY"].ReadOnly = true;

                            //add event handlers
                            dgvDetail.Leave += new System.EventHandler(dgvDetail_Leave);
                            dgvDetail.CellValueChanged += new DataGridViewCellEventHandler(dgvDetail_CellValueChanged);
                            dgvDetail.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dgvDetail_RowsRemoved);
                        }
                        else  // is expanded                    
                        {
                            //if (fpl != null && fpl.Visible)
                            //{
                            //if (fpl.Visible)
                            //    fpl.CloseNow = true;
                            //fpl.Close();
                            //fpl.Dispose();
                            buttonCellExpanded = false;
                            buttonCell.Style.Font = new Font("WingDings 3", 8);
                            //MessageBox.Show("Do collapse");
                            CollapseDetail();
                        }
                    }
                }
                else if (dgvDetail != null && dgvDetail.Visible)
                    CollapseDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void dgvDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateFromQty();
        }

        private void dgvDetail_RowsRemoved(
            object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // Update the balance column whenever rows are deleted.
            UpdateFromQty();
        }

        private void UpdateFromQty()
        {
            try
            {
                int counter;
                decimal FromQty;
                // Iterate through the rows, skipping the starting row.
                for (counter = 1; counter < (dgvDetail.Rows.Count - 1);
                    counter++)
                {  
                    FromQty = decimal.Parse(dgvDetail.Rows[counter - 1]
                        .Cells["TOQTY"].Value.ToString()) + 1;
                    dgvDetail.Rows[counter].Cells["FROMQTY"].Value = FromQty;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void dgvDetail_Leave(object sender, EventArgs e)
        {
            CollapseDetail();
        }

        private void CollapseDetail()
        {
            try
            {
                //
                // save detail rows 
                //
                
                if (dgvDetail != null)
                {
                    DoSave();
                    dgvDetail.Visible = false;
                    buttonCell.Value = "s"; //show expand icon
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvHeader_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (dgvHeader.Rows[e.RowIndex].IsNewRow) { return; }
                string checkColumns = "[CURNCYID],[PRCLEVEL],[UOFM]"; //,[UMSLSOPT],[RNDGAMNT],[ROUNDHOW],[ROUNDTO]";
                string name = dgvHeader.Columns[e.ColumnIndex].Name.Trim();
                if (checkColumns.Contains(name))
                {
                    DataGridViewCell currency = dgvHeader.Rows[e.RowIndex].Cells["CURNCYID"];
                    DataGridViewCell pricelevel = dgvHeader.Rows[e.RowIndex].Cells["PRCLEVEL"];
                    DataGridViewCell uofm = dgvHeader.Rows[e.RowIndex].Cells["UOFM"];
                    buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                    bool rowOK = (currency.FormattedValue.ToString().Length !=0 && pricelevel.FormattedValue.ToString().Length!=0 && uofm.FormattedValue.ToString().Length!=0);
                    if (rowOK)
                    {
                        buttonCellExpanded = false;
                        buttonCellEnabled = true;
                        buttonCell.Style.Font = new Font("Wingdings 3", 8, FontStyle.Regular);
                        buttonCell.Value = "s"; //show expand arrow
                        //remove expand/collapse icons for all other rows
                        //String searchValue = "somestring";
                        //int rowIndex = -1;
                        foreach (DataGridViewRow row in dgvHeader.Rows)
                        {
                            //if (row.Cells[1].Value.ToString().Equals(searchValue))
                            if (row.Index != e.RowIndex)
                            {
                                //rowIndex = row.Index;
                                //break;
                                dgvHeader.Rows[row.Index].Cells["Buttons"].Value = DBNull.Value;
                            }
                        }
                    }
                    else
                    {
                        buttonCell.Value = DBNull.Value; //show blank button face
                        buttonCellEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvHeader_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvHeader.Rows[e.RowIndex];
                    //if (row.Cells["ColumnName"].Value.ToString().Trim() == "Buttons")
                    if (row.Cells[e.ColumnIndex].OwningColumn.Name == "Buttons")
                    {
                        e.CellStyle.Font = new Font("Wingdings 3", 8, FontStyle.Regular);
                        //if (buttonCellEnabled)
                        //{
                        //    e.Value = buttonCellExpanded ? "s" : "r";
                        //}      
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvHeader_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvHeader.CurrentCell.OwningColumn.Name == "RNDGAMNT" && e.Control != null)
            {
                TextBox tb = (TextBox)e.Control;
                tb.KeyPress += CheckNumeric_KeyPress;
            }         
        }

        private void CheckNumeric_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            {
                //---if textbox is empty and user pressed a decimal char---
                if (((TextBox)sender).Text == string.Empty & e.KeyChar == (char)46)
                {
                    e.Handled = true;
                    return;
                }
                //---if textbox already has a decimal point---
                if (((TextBox)sender).Text.Contains(Convert.ToString((char)46)) & e.KeyChar == (char)46)
                {
                    e.Handled = true;
                    return;
                }
                //if (!(char.IsDigit(e.KeyChar).ToString() ==","))
                if (!(e.KeyChar == 44) & !(e.KeyChar == 45))
                {
                    if ((!(char.IsDigit(e.KeyChar) | char.IsControl(e.KeyChar) | (e.KeyChar == (char)46))))
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void dgvHeader_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool skip = false;
                if (e.RowIndex < 0 || bIsLoading || dgvHeader.CurrentRow == null || skip)
                    return;

                if (!dgvHeader.CurrentRow.IsNewRow && e.RowIndex > 0)
                {
                    //DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                    buttonCell = (DataGridViewButtonCell)dgvHeader.CurrentRow.Cells["Buttons"];
                    buttonCell.Value = DBNull.Value;
                    buttonCellEnabled = false;
                }
                else
                {
                    DataGridViewRow row = dgvHeader.Rows[e.RowIndex];
                    if (row.Cells["Buttons"].Value == DBNull.Value || row.Cells["Buttons"].Value == null)
                    {
                        //DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                        buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                        buttonCellExpanded = false;
                        buttonCellEnabled = true;
                        buttonCell.Style.Font = new Font("Wingdings 3", 8, FontStyle.Regular);
                        buttonCell.Value = "s"; //show expand arrow                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dgvHeader_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dgvHeader_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!bIsLoading && !btnDone.Focused)  //&& !dgvHeader.CurrentRow.IsNewRow && e.RowIndex > 0)
                {
                    //DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                    buttonCell = (DataGridViewButtonCell)dgvHeader.CurrentRow.Cells["Buttons"];
                    if (dgvDetail != null && dgvDetail.Visible)
                    {
                        buttonCell.Value = "r";
                        buttonCellEnabled = false;
                    }
                    else
                    {
                        buttonCell.Value = DBNull.Value;
                        buttonCellEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvHeader_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (!bIsLoading)
                {
                    {
                        DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                        buttonCell = (DataGridViewButtonCell)dgvHeader.Rows[e.RowIndex].Cells["Buttons"];
                        if (dgvDetail != null && dgvDetail.Visible)
                        {
                            buttonCell.Value = "r";
                            buttonCellEnabled = false;
                        }
                        else
                        {
                            buttonCell.Value = DBNull.Value;
                            buttonCellEnabled = false;
                        }
                    }
                }

            }
            catch (Exception ex)
            
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
