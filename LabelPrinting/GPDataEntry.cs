using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace LabelPrinting
{
    public partial class GPDataEntry : Form
    {
        bool bIsLoading;
        int EntryType;
        string GPDbase, CurrentCode, CompCode, CurrentDesc;
        DataSet dsGPDataEntry, dsCompany, dsPackagingType, dsProductCode, dsItemClass, dsCurrency, dsUserCategory, dsUOFM;
        GPDataEntryDAL dal;
        ComboBox cboCell; 
        string dbEnv;
        PriceList fpl = null;
        //internal protected DataGridView dgvEdit;

        public GPDataEntry()
        {
            InitializeComponent();            
        }

        private void GPDataEntry_Shown(object sender, EventArgs e)
        {
            //RefreshGrid();
            bIsLoading = false;
            SetCompany();
        }

        private void GPDataEntry_Load(object sender, EventArgs e)
        {
            EntryType = 3;  //fetch current edit item
            bIsLoading = true;
            //buttonCell.Expanded = false;
            dal = new GPDataEntryDAL();
            dsPackagingType = dal.SelectGP_PackagingType();
            dsCurrency = dal.GetCurrency();                      
            dgvEdit.CellValueChanged += new DataGridViewCellEventHandler(dgvEdit_CellValueChanged);
            dgvEdit.CurrentCellDirtyStateChanged += new EventHandler(dgvEdit_CurrentCellDirtyStateChanged);     
       }
            
        private void SetCompany()
        {
            if (bIsLoading)
                return;

            if (optTest.Checked)
            {
                dbEnv = optTest.Text;
            }
            if (optLive.Checked)
            {
                dbEnv = optLive.Text;
            }
            dsCompany = dal.GetCompany(dbEnv);
            cboCompany.DataSource = null;
            cboCompany.Items.Clear();            
            cboCompany.DisplayMember = "CMPNYNAM";
            cboCompany.ValueMember = "CMPANYID";
            cboCompany.DataSource = dsCompany.Tables[0];            
            cboCompany.ResetText();
            cboCompany.SelectedIndex = 2;
            btnGo.Enabled = true;
            cboCode.Enabled = true;
            //lblCode.Enabled = false;
            optNew.Enabled = true;
            optExisting.Enabled = true;
            optCurrent.Enabled = true;
        }
        private void RefreshGrid(int EntryType)
        {
            try
            {
                //bExpanded = false;
                bIsLoading = true;
                dsItemClass = dal.LookupItemClassByCompany(CompCode);
                dgvEdit.DataSource = null;
                dgvEdit.Rows.Clear();
                dgvEdit.Columns.Clear();
                cboCell = new ComboBox();
                cboCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                cboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cboCell.AutoCompleteSource = AutoCompleteSource.ListItems;
                this.dgvEdit.Controls.Add(cboCell);
                cboCell.Visible = false;
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
                
                //string GPDatabase, Boolean IsNew, string ItemCode, string TableName 
                dsGPDataEntry = dal.SelectGPDataEntry(GPDbase, EntryType, cboCode.Text, "IV00101");
                DataTable dt = dsGPDataEntry.Tables[0];
                DataView dv = dt.DefaultView;
                //set current code and description
                dv.RowFilter = "ColumnName = 'ITEMNMBR'";
                CurrentCode = (string)dv[0]["inputValue"];                
                dv.RowFilter = "ColumnName = 'ITEMDESC'";
                CurrentDesc = (string)dv[0]["inputValue"];
                dv.RowFilter = "";
                if (!chkConfig.Checked)
                {
                    dv.RowFilter = "(TableName='IV00101' OR LEN(ISNULL(TableName,'')) = 0) AND reqd = 1";
                    dgvEdit.DataSource = dv;
                    //dgvEdit.Columns["displayValue"].MappingName = "Value";                    
                    for (var i = 0; i < dgvEdit.ColumnCount; i++)
                    {
                        //Console.WriteLine(dgvEdit.Columns[i].Name);
                        if (//dgvEdit.Columns[i].Name == "defltValue" || 
                              dgvEdit.Columns[i].Name == "Description"
                            || dgvEdit.Columns[i].Name == "displayValue")
                            dgvEdit.Columns[i].Visible = true;
                        else
                            dgvEdit.Columns[i].Visible = false;

                        if (dgvEdit.Columns[i].Name == "displayValue"
                            || dgvEdit.Columns[i].Name == "defltValue"
                            || dgvEdit.Columns[i].Name == "seq"
                            || dgvEdit.Columns[i].Name == "reqd")
                        {
                            dgvEdit.Columns[i].ReadOnly = false;                           
                        }
                        else
                        {
                            dgvEdit.Columns[i].ReadOnly = true;
                            dgvEdit.Columns[i].DefaultCellStyle.BackColor = SystemColors.Control;
                        }                        
                    }
                }                   
                else
                {
                    dv.RowFilter = "";
                    dgvEdit.DataSource = dv;                    
                    for (var i = 0; i < dgvEdit.ColumnCount; i++)
                    {
                        dgvEdit.Columns[i].Visible = true;
                        if (dgvEdit.Columns[i].Name == "Description"
                            || dgvEdit.Columns[i].Name == "displayValue"
                            || dgvEdit.Columns[i].Name == "defltValue"
                            || dgvEdit.Columns[i].Name == "seq"
                            || dgvEdit.Columns[i].Name == "Reqd")
                        {
                            dgvEdit.Columns[i].ReadOnly = false;
                        }
                        else
                        {
                            dgvEdit.Columns[i].ReadOnly = true;
                            dgvEdit.Columns[i].DefaultCellStyle.BackColor = SystemColors.Control;
                        }
                    }                                            
                }
                
                dgvEdit.Columns["displayValue"].HeaderText = "Value";
                DataGridViewDisableButtonColumn column1 = new DataGridViewDisableButtonColumn();                
                column1.Name = "Buttons";
                column1.HeaderText = "";
                column1.Width = 41;                
                dgvEdit.Columns.Insert(2, column1);                
                dgvEdit.AutoSize = true;
                dgvEdit.AllowUserToAddRows = false;
                //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Set the text for each button.               
                for (int i = 0; i < dgvEdit.RowCount; i++)
                {
                    //dgvEdit.Rows[i].Cells["Buttons"].Value = DBNull.Value;
                    dgvEdit.RefreshEdit();
                    dgvEdit.Rows[i].Cells[2].Value = DBNull.Value;
                    // "Button " + i.ToString();
                }
                
                dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                bIsLoading = false;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void dgvEdit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 ) //|| bIsLoading)
                    return;

                //else if (dgvEdit.Columns[e.ColumnIndex].Name == "CheckBoxes")            
                if (dgvEdit.Columns[e.ColumnIndex].Name == "Buttons")
                {
                    DataGridViewDisableButtonCell buttonCell =
                        (DataGridViewDisableButtonCell)dgvEdit.Rows[e.RowIndex].Cells["Buttons"];


                    //if (dgvEdit.Rows[e.RowIndex].Cells["displayValue"].Value.ToString().Trim() == "1")
                    //    buttonCell.Enabled = true;
                    //else
                    //    buttonCell.Enabled = false;
                    ////Console.WriteLine(dgvEdit.Rows[e.RowIndex].Cells[11].ToString().Trim());
                    buttonCell.Enabled = !(dgvEdit.Rows[e.RowIndex].Cells["displayValue"].Value.ToString().Trim() == "1"
                        && (dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList"
                            || dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency"
                            || dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasItemSite"
                            || dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasItemVendor"));
                    if (!buttonCell.Enabled)
                    {
                        //show expand icon

                        if (!buttonCell.Expanded)
                        {
                            buttonCell.Style.Font = new Font("WingDings 3", 8);
                            buttonCell.Value = "s";
                            //buttonCell.Expanded = true;
                        }
                        //else
                        //{
                        //    buttonCell.Style.Font = new Font("WingDings 3", 8);
                        //    buttonCell.Value = "v";
                        //    //buttonCell.Expanded = false;
                        //}
                    }
                    else
                    {
                        buttonCell.Value = DBNull.Value;
                        buttonCell.Expanded = false;
                    }




                    dgvEdit.Invalidate();
                }
                if (dgvEdit.Columns[e.ColumnIndex].Name == "displayValue"
                    && (dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList"
                        || dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency"
                        || dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasItemSite"
                        || dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasItemVendor"))
                {
                    DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)dgvEdit.Rows[e.RowIndex].Cells["Buttons"];


                    //if (dgvEdit.Rows[e.RowIndex].Cells["displayValue"].Value.ToString().Trim() == "1")
                    //    buttonCell.Enabled = true;
                    buttonCell.Enabled = !(dgvEdit.Rows[e.RowIndex].Cells["displayValue"].Value.ToString().Trim() == "1");
                    //&& (dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList"
                    //    || dgvEdit.Rows[e.RowIndex].Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency"));
                    if (!buttonCell.Enabled)
                    {
                        //show expand icon
                        buttonCell.Style.Font = new Font("WingDings 3", 8);
                        buttonCell.Value = "s";
                    }

                    else
                        buttonCell.Value = DBNull.Value;

                    dgvEdit.Invalidate();
                }
            }    
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        



        // This event handler manually raises the CellValueChanged event
        // by calling the CommitEdit method.
        void dgvEdit_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvEdit.IsCurrentCellDirty)
            {
                dgvEdit.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        internal protected void dgvEdit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvEdit.Rows[e.RowIndex];
            if (e.ColumnIndex == dgvEdit.Columns["displayValue"].Index)
            {    
                //set read-only textbox rows:-
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "GP_updated_when"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "GP_updated_by"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "GP_update_result"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "NewItem"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "GP_Submitted")
                {
                    row.Cells["displayValue"].ReadOnly = true;
                }
                    
                //implement dropdown list for appropriate rows:-
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "ITMGEDSC")
                {                    
                    cboCell.SelectedIndexChanged -= new System.EventHandler(cboCell_SelectedIndexChanged);
                    cboCell.DataSource = null;
                    cboCell.Items.Clear();
                    cboCell.DataSource = dsPackagingType.Tables[0];
                    cboCell.DisplayMember = "ITMGEDSC";
                    cboCell.ValueMember = "ITMGEDSC";
                    cboCell.ResetText();
                    cboCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboCell.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //dgvEdit[e.ColumnIndex, e.RowIndex] = cboCell;
                    cboCell.SelectedIndexChanged += new System.EventHandler(cboCell_SelectedIndexChanged);
                }
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "ITMCLSCD")
                {
                    cboCell.SelectedIndexChanged -= new System.EventHandler(cboCell_SelectedIndexChanged);
                    cboCell.DataSource = null;
                    cboCell.Items.Clear();
                    cboCell.DataSource = dsItemClass.Tables[0];
                    cboCell.DisplayMember = "ITMCLSCD";
                    cboCell.ValueMember = "ITMCLSCD";
                    cboCell.ResetText();
                    cboCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboCell.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //dgvEdit[e.ColumnIndex, e.RowIndex] = cboCell;
                    cboCell.SelectedIndexChanged += new System.EventHandler(cboCell_SelectedIndexChanged);
                }
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "CURNCYID")
                {
                    cboCell.SelectedIndexChanged -= new System.EventHandler(cboCell_SelectedIndexChanged);
                    cboCell.DataSource = null;
                    cboCell.Items.Clear();
                    cboCell.DataSource = dsCurrency.Tables[0];
                    cboCell.DisplayMember = "CURNCYID";
                    cboCell.ValueMember = "CURNCYID";
                    cboCell.ResetText();
                    cboCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboCell.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //dgvEdit[e.ColumnIndex, e.RowIndex] = cboCell;
                    cboCell.SelectedIndexChanged += new System.EventHandler(cboCell_SelectedIndexChanged);
                }
                //U of Measure Schedule
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "UOMSCHDL")
                {
                    cboCell.SelectedIndexChanged -= new System.EventHandler(cboCell_SelectedIndexChanged);
                    cboCell.DataSource = null;
                    cboCell.Items.Clear();
                    cboCell.DataSource = dsUOFM.Tables[0];
                    cboCell.DisplayMember = "UOMSCHDL";
                    cboCell.ValueMember = "UOMSCHDL";
                    cboCell.ResetText();
                    cboCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboCell.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //dgvEdit[e.ColumnIndex, e.RowIndex] = cboCell;
                    cboCell.SelectedIndexChanged += new System.EventHandler(cboCell_SelectedIndexChanged);
                }
                //Purchasing U of M
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "PRCHSUOM")
                {
                    cboCell.SelectedIndexChanged -= new System.EventHandler(cboCell_SelectedIndexChanged);
                    cboCell.DataSource = null;
                    cboCell.Items.Clear();
                    cboCell.DataSource = dsUOFM.Tables[0];
                    cboCell.DisplayMember = "UOMSCHDL";
                    cboCell.ValueMember = "UOMSCHDL";
                    cboCell.ResetText();
                    cboCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboCell.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //dgvEdit[e.ColumnIndex, e.RowIndex] = cboCell;
                    cboCell.SelectedIndexChanged += new System.EventHandler(cboCell_SelectedIndexChanged);
                }
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "ITEMTYPE"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "DECPLQTY"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "VCTNMTHD"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "DECPLCUR"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "TAXOPTNS"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "Purchase_Tax_Options"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ITMTRKOP"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "PRICMTHD"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ABCCODE")
                {
                    cboCell.SelectedIndexChanged -= new System.EventHandler(cboCell_SelectedIndexChanged);
                    cboCell.DataSource = null;
                    cboCell.Items.Clear();
                    string targetColumn = row.Cells["ColumnName"].Value.ToString().Trim();
                    DataSet dsLookupByTargetColumn = dal.GPLookupsByTargetCol(targetColumn);
                    cboCell.DataSource = dsLookupByTargetColumn.Tables[0];
                    cboCell.DisplayMember = "DisplayMember";
                    cboCell.ValueMember = "ListOptionValue";
                    cboCell.ResetText();
                    cboCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboCell.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //dgvEdit[e.ColumnIndex, e.RowIndex] = cboCell;
                    cboCell.SelectedIndexChanged += new System.EventHandler(cboCell_SelectedIndexChanged);
                }
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_1"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_2"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_3"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_4"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_5"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_6"
                    )
                {
                    cboCell.SelectedIndexChanged -= new System.EventHandler(cboCell_SelectedIndexChanged);
                    cboCell.DataSource = null;
                    cboCell.Items.Clear();
                    string targetColumn = row.Cells["ColumnName"].Value.ToString().Trim();
                    //DataSet dsLookupByTargetColumn = dal.GPLookupsByTargetCol(targetColumn);
                    //cboCell.DataSource = dsLookupByTargetColumn.Tables[0];
                    DataView dv = new DataView(dsUserCategory.Tables[0]);
                    dv.RowFilter = ("CompanyCode = '" + CompCode + "'AND USERCATNAME = '" + targetColumn + "'");
                    cboCell.DataSource = dv;
                    cboCell.DisplayMember = "USCATVAL";
                    cboCell.ValueMember = "USERCATNAME";
                    cboCell.ResetText();
                    cboCell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    cboCell.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboCell.AutoCompleteSource = AutoCompleteSource.ListItems;
                    //dgvEdit[e.ColumnIndex, e.RowIndex] = cboCell;
                    cboCell.SelectedIndexChanged += new System.EventHandler(cboCell_SelectedIndexChanged);
                }

                //use wingdings to implement a checkbox                
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "INCLUDEINDP"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "LOTEXPWARN"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ALWBKORD"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_5"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_6"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "KPCALHST"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "KPERHIST"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "KPTRXHST"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "KPDSTHST"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemSite" 
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemVendor"
                    //|| row.Cells["ColumnName"].Value.ToString().Trim() == "NewItem"
                    //|| row.Cells["ColumnName"].Value.ToString().Trim() == "GP_Submitted"                    
                    )  
                {
                    row.Cells["displayValue"].ReadOnly = true;
                    if (row.Cells["displayValue"].Value == DBNull.Value)
                    {
                        row.Cells["displayValue"].Value = row.Cells["defltValue"].Value.ToString();
                    }
                    else if (row.Cells["displayValue"].Value.ToString() == "0")
                    {
                        row.Cells["displayValue"].Value = "1";
                        //if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList"
                        //    || row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency")
                        //    bExpanded = true;
                    }
                    else if (row.Cells["displayValue"].Value.ToString().Trim() == "YES")
                    {
                        row.Cells["displayValue"].Value = "NO";                        
                    }
                    else if (row.Cells["displayValue"].Value.ToString() == "1")
                    {
                        row.Cells["displayValue"].Value = "0";
                        //if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList"
                        //    || row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency")
                        //    bExpanded = false;
                    }
                    else if (row.Cells["displayValue"].Value.ToString() == "NO")
                    {
                        row.Cells["displayValue"].Value = "YES";                    
                    }

                   row.Cells["inputValue"].Value = row.Cells["displayValue"].Value;
                }
            }
            else if (dgvEdit.Columns[e.ColumnIndex].Name == "Buttons" && dgvEdit.Rows[e.RowIndex].Cells["Buttons"].Value != DBNull.Value)
            {
                //enables expand/collapse button for appropriate row
                DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)dgvEdit.Rows[e.RowIndex].Cells["Buttons"];

                //if (buttonCell.Enabled)
                if (!buttonCell.Enabled)
                {
                    if (!buttonCell.Expanded)
                    {
                        if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList")
                        {
                            buttonCell.Expanded = true;
                            buttonCell.Style.Font = new Font("WingDings 3", 8);
                            buttonCell.Value = "r";  //show collapse arrow
                            //MessageBox.Show("Do expand");
                            fpl = new PriceList();
                            fpl.CurrentItemNmbr = CurrentCode;
                            fpl.CurrentDescription = CurrentDesc;
                            fpl.dsCurrency = dsCurrency;
                            fpl.dsUOFM = dsUOFM;
                            fpl.DatabaseName = GPDbase;
                            fpl.CurrentEntryType = EntryType;
                            //fpl.Show(this);
                            fpl.ShowDialog(this);
                            //buttonCell.Expanded = false;
                            //buttonCell.Value = "s";

                        }
                        else if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency")
                        {
                            //buttonCell.Value = DBNull.Value;                            
                            buttonCell.Expanded = true;
                            buttonCell.Style.Font = new Font("WingDings 3", 8);
                            buttonCell.Value = "r";  //show collapse arrow
                            MessageBox.Show("Do Expand");
                            buttonCell.Expanded = false;
                            buttonCell.Value = "s";   //show expand arrow 
                        }
                        else if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemSite")
                        {
                            //buttonCell.Value = DBNull.Value;                            
                            buttonCell.Expanded = true;
                            buttonCell.Style.Font = new Font("WingDings 3", 8);
                            buttonCell.Value = "r";  //show collapse arrow
                            MessageBox.Show("Do Expand");
                            buttonCell.Expanded = false;
                            buttonCell.Value = "s";   //show expand arrow 
                        }
                        else if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemVendor")
                        {
                            //buttonCell.Value = DBNull.Value;                            
                            buttonCell.Expanded = true;
                            buttonCell.Style.Font = new Font("WingDings 3", 8);
                            buttonCell.Value = "r";  //show collapse arrow
                            MessageBox.Show("Do Expand");
                            buttonCell.Expanded = false;
                            buttonCell.Value = "s"; //show expand arrow 
                        }
                    }                    
                    else  // is expanded
                    if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList")
                    {                        
                        if (fpl != null && fpl.Visible)
                        {
                            if (fpl.Visible)
                                fpl.CloseNow = true;
                                fpl.Close();
                            fpl.Dispose();
                            buttonCell.Expanded = false;
                            buttonCell.Style.Font = new Font("WingDings 3", 8);
                            buttonCell.Value = "s";
                            //MessageBox.Show("Do collapse");
                        }
                        buttonCell.Value = DBNull.Value;
                    }
                    else if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency")
                    {                                               
                        buttonCell.Expanded = false;
                        buttonCell.Style.Font = new Font("WingDings 3", 8);
                        buttonCell.Value = "r";
                        MessageBox.Show("Do collapse");
                    }
                    else if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemSite")
                    {                        
                        buttonCell.Expanded = false;
                        buttonCell.Style.Font = new Font("WingDings 3", 8);
                        buttonCell.Value = "r";
                        MessageBox.Show("Do collapse");
                    }
                    else if (row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemVendor")
                    {                        
                        buttonCell.Expanded = false;
                        buttonCell.Style.Font = new Font("WingDings 3", 8);
                        buttonCell.Value = "r";
                        MessageBox.Show("Do collapse");
                    }
                }
            }
        }
        private void cboCell_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboCell.SelectedIndex > -1)
            {
                if (dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["ColumnName"].Value.ToString().Trim() == "ITMGEDSC"
                    || dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["ColumnName"].Value.ToString().Trim() == "ITMCLSCD"
                    || dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["ColumnName"].Value.ToString().Trim() == "CURNCYID")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["displayValue"].Value = cboCell.Text;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["inputValue"].Value = cboCell.Text; ;
                }
                DataGridViewRow row = dgvEdit.Rows[dgvEdit.CurrentRow.Index];
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "ITEMTYPE"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "DECPLQTY"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "VCTNMTHD"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "DECPLCUR"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "TAXOPTNS"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "Purchase_Tax_Options"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ITMTRKOP"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "PRICMTHD"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ABCCODE"
                    || row.Cells["ColumnName"].Value.ToString().Contains("USCATVLS_") 
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "UOMSCHDL"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "PRCHSUOM")
                {
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["displayValue"].Value = cboCell.Text;
                    dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["inputValue"].Value = cboCell.SelectedValue.ToString(); ;
                }
            }
        }
        private void optTest_Click(object sender, EventArgs e)
        {
            SetCompany();
        }

        private void dgvEdit_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {           
            if (e.ColumnIndex == dgvEdit.Columns["displayValue"].Index)
            {
                DataGridViewRow row = dgvEdit.Rows[dgvEdit.CurrentRow.Index];
                DataGridViewColumn thisCol = dgvEdit.Columns[e.ColumnIndex];
                if (row.Cells["ColumnName"].Value.ToString().Trim() == "ITMGEDSC"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ITMCLSCD"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ITEMTYPE"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "DECPLQTY"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "VCTNMTHD"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "DECPLCUR"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "TAXOPTNS"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "Purchase_Tax_Options"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ITMTRKOP"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "PRICMTHD"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "ABCCODE"
                    || row.Cells["ColumnName"].Value.ToString().Contains("USCATVLS_")
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "PRCHSUOM"
                    || row.Cells["ColumnName"].Value.ToString().Trim() == "UOMSCHDL")
                {
                    cboCell.Location = dgvEdit.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    cboCell.Width = thisCol.Width;
                    cboCell.Visible = true;
                }
            }
                
            //}
        }

        private void dgvEdit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cboCell.Visible == true)
            {
                this.dgvEdit.CurrentCell.Value = this.cboCell.Text;
                if (e.ColumnIndex == dgvEdit.Columns["displayValue"].Index)
                {
                    DataGridViewRow row = dgvEdit.Rows[dgvEdit.CurrentRow.Index];
                    row.Cells["inputValue"].Value = this.cboCell.Text;
                }                    
                this.cboCell.Visible = false;
            }
        }

        private void GPDataEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (dsGPDataEntry != null && dgvEdit != null)
                {
                    if (dgvEdit.IsCurrentRowDirty)
                    {
                        this.Validate();
                    }
                    dgvEdit.EndEdit();
                    dal.UpdateGPDataEntry(dsGPDataEntry);
                }                  
            }
            catch
            {
                throw;
            }
        }

        private void optNew_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void optExisting_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void ResetControls()
        {
            if (optExisting.Checked)
            {
                EntryType = 2;
                if (cboCompany.SelectedIndex > -1)
                {
                    cboCode.Enabled = true;
                    //lblCode.Enabled = true;
                    btnGo.Enabled = (cboCode.Text.Length > 0);
                }

            }
            else if (optNew.Checked)
            {
                EntryType = 1;
                cboCode.Enabled = false;
                btnGo.Enabled = true;
                //lblCode.Enabled = false;
            }
            else if (optCurrent.Checked)
                btnGo.Enabled = true;            
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading && cboCompany.SelectedIndex > -1)
            {
                string sv = cboCompany.SelectedValue.ToString();
                DataRow[] result = dsCompany.Tables[0].Select("CMPANYID = " + sv);
                if (result.Length > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    GPDbase = result[0]["INTERID"].ToString();
                    CompCode = result[0]["CompanyCode"].ToString();
                    dsUOFM = dal.LookupUOMSCHDL_ByCompany(CompCode, dbEnv);
                    dsUserCategory = dal.LookupUSCATVLS_ByCompany(CompCode, dbEnv);
                    btnGo.Enabled = true;
                    cboCode.Enabled = optExisting.Checked;
                    //lblCode.Enabled = optExisting.Checked;                           
                    dsProductCode = dal.GetItemIndex(GPDbase);
                    cboCode.ValueMember = "ITEMNMBR";
                    cboCode.DisplayMember = "ITEMNMBR";
                    cboCode.DataSource = dsProductCode.Tables[0];
                    cboCode.ResetText();
                    optExisting.Enabled = true;
                    optNew.Enabled = true;
                    optCurrent.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    btnGo.Enabled = false;
                    cboCode.Enabled = false;
                    //lblCode.Enabled = false;
                    optExisting.Enabled = false;
                    optNew.Enabled = false;
                    optCurrent.Enabled = false;
                }                    
            }
        }

        private void cboCode_TextUpdate(object sender, EventArgs e)
        {

        }

        private void cboCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading && cboCode.SelectedIndex > -1)
            {
                CurrentCode = cboCode.SelectedItem.ToString();                
                btnGo.Enabled = true;                
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (optNew.Checked)
                EntryType = 1;
            else if (optExisting.Checked && CompCode.Length > 0)
                EntryType = 2;
            else if (optCurrent.Checked)
                EntryType = 3;
            RefreshGrid(EntryType);
        }

        private void optLive_Click(object sender, EventArgs e)
        {
            SetCompany();
        }

        private void chkConfig_CheckedChanged(object sender, EventArgs e)
        {
            //optTest.Visible = chkConfig.Checked;
            //optLive.Visible = chkConfig.Checked;
            groupInputDB.Visible = chkConfig.Checked;
            groupTargetDB.Visible = chkConfig.Checked;
            if (optNew.Checked)
                EntryType = 1;
            else if (optExisting.Checked && CompCode.Length > 0)
                EntryType = 2;
            else if (optCurrent.Checked)
                EntryType = 3;
            if (dgvEdit.RowCount > 0)
            {
                dgvEdit.EndEdit();
                dal.UpdateGPDataEntry(dsGPDataEntry);
                RefreshGrid(EntryType);
            }                
        }

        private void dgvEdit_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvEdit.Columns[e.ColumnIndex].Name == "displayValue")
                {
                    DataGridViewRow row = dgvEdit.Rows[e.RowIndex];
                    //Console.WriteLine(row.Cells["ColumnName"].Value.ToString().Trim());
                    //Use wingdings to implement a checkbox
                    //LOTEXPWARN
                    if (row.Cells["ColumnName"].Value.ToString().Trim() == "INCLUDEINDP"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "LOTEXPWARN"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "ALWBKORD"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_5"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "USCATVLS_6"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "KPCALHST"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "KPERHIST"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "KPTRXHST"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "KPDSTHST"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "HasPriceList"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemCurrency"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemSite"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "HasItemVendor"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "NewItem"
                        || row.Cells["ColumnName"].Value.ToString().Trim() == "GP_Submitted"
                        )
                    {
                        e.CellStyle.Font = new Font("Wingdings 2", 12, FontStyle.Regular);
                        if (e.Value.ToString() == "0" || e.Value.ToString().Trim() == "NO")
                        {
                            e.Value = "S";
                        }
                        else if (e.Value.ToString() == "1" || e.Value.ToString().Trim() == "YES")
                        {
                            e.Value = "R";
                        }
                        else //if (e.Value == DBNull.Value)
                        {
                            e.Value = "0";
                        }
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        e.FormattingApplied = true;

                    }
                }               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
   
}
