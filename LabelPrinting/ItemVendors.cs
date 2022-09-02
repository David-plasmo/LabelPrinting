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
    public partial class ItemVendors : Form
    {
        public bool CloseNow;
        public string CurrentItemNmbr;
        public string CurrentDescription;
        public string DatabaseName;
        public int CurrentEntryType;

        private DataSet dsItemVendor, dsItemVendorDesc, dsLocationCode, dsVendor;
        private ItemVendorsDAL dal;

        private bool bIsLoading;

        public ItemVendors()
        {
            InitializeComponent();
            CloseNow = false;
        }

        private void DoSave()
        {
            try
            {
                //MessageBox.Show("Do Save");
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                dal.UpdateItemVendor(dsItemVendor);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ItemSites_Shown(object sender, EventArgs e)
        {
            
        }

        private void RefreshGrid()
        {
            bIsLoading = true;
            dal = new ItemVendorsDAL();
            dsItemVendorDesc = dal.GetItemVendor_Description();
            dsItemVendor = dal.GetGPItemVendorForEdit(DatabaseName, CurrentEntryType, CurrentItemNmbr);
            dgvEdit.Columns.Clear();
            DataTable dt = dsItemVendor.Tables[0];
            dt.Columns["ITEMNMBR"].DefaultValue = CurrentItemNmbr;
            dt.Columns["DatabaseName"].DefaultValue = DatabaseName;
            dt.Columns["QTYRQSTN"].DefaultValue = 0.0000;
            dt.Columns["MINORQTY"].DefaultValue = 0.0000;
            dt.Columns["MAXORDQTY"].DefaultValue = 0.0000;
            dt.Columns["ECORDQTY"].DefaultValue = 0.0000;
            dt.Columns["Last_Originating_Cost"].DefaultValue = 0.01;
            dt.Columns["ORDERMULTIPLE"].DefaultValue = 1.0000;
            dt.Columns["AVRGLDTM"].DefaultValue = 0;
            dt.Columns["NORCTITM"].DefaultValue = 0;
            dt.Columns["FREEONBOARD"].DefaultValue = 1;
            dt.Columns["PLANNINGLEADTIME"].DefaultValue = 0;
            dt.Columns["TableName"].DefaultValue = "IV00102";

            dgvEdit.DataSource = dt;

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

            //Setup columns 
            DataView dvd = dsItemVendorDesc.Tables[0].DefaultView;
            for (var i = 0; i < dgvEdit.ColumnCount; i++)
            {
                string sFilter = "ColumnName= '" + dgvEdit.Columns[i].Name + "'";
                dvd.RowFilter = sFilter;
                dgvEdit.Columns[i].Visible = false;
                if (dvd.Count > 0)
                {
                    string sHeader = (string)dvd[0]["Description"];
                    dgvEdit.Columns[i].HeaderText = sHeader;
                    string name = dgvEdit.Columns[i].Name.Trim();
                    string hiddenNames = "ITEMNMBR ID TableName";
                    bool excluded = hiddenNames.Contains(name);
                    bool visible = ((bool)dvd[0]["reqd"] && !excluded);
                    dgvEdit.Columns[i].Visible = visible;

                    //set up combobox columns
                    if (dgvEdit.Columns[i].Name == "FREEONBOARD")
                    {
                        DataGridViewComboBoxColumn cbcFreeOnBoard = new DataGridViewComboBoxColumn();
                        cbcFreeOnBoard.Name = "FREEONBOARD";
                        DataTable dth = new DataTable();
                        dth.Columns.AddRange(new DataColumn[] { new DataColumn("Text"), new DataColumn("FREEONBOARD", typeof(int)) });
                        dth.Rows.Add("1=None", 1);
                        dth.Rows.Add("2=Origin", 2);
                        dth.Rows.Add("3=Destination", 3);
                        cbcFreeOnBoard.DataSource = dth;
                        cbcFreeOnBoard.DisplayMember = "Text";
                        cbcFreeOnBoard.ValueMember = "FREEONBOARD";
                        cbcFreeOnBoard.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        cbcFreeOnBoard.DisplayStyleForCurrentCellOnly = true;                        
                        dgvEdit.Columns.Remove("FREEONBOARD");
                        cbcFreeOnBoard.HeaderText = sHeader;
                        dgvEdit.Columns.Insert(i, cbcFreeOnBoard);
                        dgvEdit.Columns[i].Name = "LOCNCODE";
                        cbcFreeOnBoard.DataPropertyName = "FREEONBOARD";                       
                    }
                    else if (dgvEdit.Columns[i].Name == "VENDORID")
                    {
                        DataGridViewComboBoxColumn cbcVendor = new DataGridViewComboBoxColumn();
                        cbcVendor.Name = "VENDORID";
                        dsVendor = dal.LookupVendors(DatabaseName);
                        cbcVendor.DataSource = dsVendor.Tables[0];
                        cbcVendor.DisplayMember = "DisplayValue";
                        cbcVendor.ValueMember = "VENDORID";
                        cbcVendor.ValueType = typeof(string);
                        cbcVendor.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        cbcVendor.DisplayStyleForCurrentCellOnly = true;
                        //cbcVendor.SelectedIndex = 0;
                        dgvEdit.Columns.Remove("VENDORID");
                        cbcVendor.HeaderText = sHeader;
                        dgvEdit.Columns.Insert(i, cbcVendor);
                        dgvEdit.Columns[i].Name = "VENDORID";
                        cbcVendor.DataPropertyName = "VENDORID";
                        //dgvHeader.Columns.Add(cbcRoundHow);
                    }
                    dgvEdit.Columns["QTYRQSTN"].DefaultCellStyle.Format = "N5";
                    //dgvEdit.Columns["QTYONORD"].DefaultCellStyle.Format = "N5";
                    dgvEdit.Columns["AVRGLDTM"].DefaultCellStyle.Format = "G";
                    dgvEdit.Columns["NORCTITM"].DefaultCellStyle.Format = "G";
                    dgvEdit.Columns["MINORQTY"].DefaultCellStyle.Format = "N5";
                    dgvEdit.Columns["MAXORDQTY"].DefaultCellStyle.Format = "N5";
                    dgvEdit.Columns["ECORDQTY"].DefaultCellStyle.Format = "N5";
                    dgvEdit.Columns["Last_Originating_Cost"].DefaultCellStyle.Format = "N5";
                    dgvEdit.Columns["PLANNINGLEADTIME"].DefaultCellStyle.Format = "G";
                    dgvEdit.Columns["ORDERMULTIPLE"].DefaultCellStyle.Format = "N5";

                    dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    bIsLoading = false;
                }
            }
        }


        
        private void ItemSites_Load(object sender, EventArgs e)
        {
            
        }

        private void dgvEdit_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Confirm Delete?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control != null)
            {
                string numericColumns = "QTYRQSTN MINORQTY MAXORDQTY ECORDQTY Last_Originating_Cost ORDERMULTIPLE";
                string integerColumns = "AVRGLDTM NORCTITM PLANNINGLEADTIME";
                string colName = dgvEdit.CurrentCell.OwningColumn.Name;
                if  (numericColumns.Contains(colName))
                {
                    TextBox tb = (TextBox)e.Control;
                    tb.KeyPress += CheckNumeric_KeyPress;
                }
                else if (integerColumns.Contains(colName))
                {
                    TextBox tb = (TextBox)e.Control;
                    tb.KeyPress += CheckInteger_KeyPress;
                }
            }
            
            if (dgvEdit.CurrentCell.OwningColumn.Name == "QTYRQSTN" && e.Control != null)
            {
               
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

        private void CheckInteger_KeyPress(System.Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void ItemVendors_FormClosed(object sender, FormClosedEventArgs e)
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

        private void ItemVendors_Load(object sender, EventArgs e)
        {
            txtCode.Text = CurrentItemNmbr;
            txtDescription.Text = CurrentDescription;
        }

        private void ItemVendors_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgvEdit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || bIsLoading)
                return;

            if (dgvEdit.Columns[e.ColumnIndex].Name == "LOCNCODE")
            {
                bool rowOK = !IsDuplicate(dgvEdit, "LOCNCODE", e.RowIndex);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsDuplicate(DataGridView dgv, string colName, int testRow)
        {
            try
            {
                bool result = false;
                for (int row = 0; row < dgv.Rows.Count - 1; row++)
                {

                    if (dgv.Rows[row].Cells[colName].Value != null
                        && row != testRow
                        && dgv.Rows[row].Cells[colName].Value.Equals(dgv.Rows[testRow].Cells[colName].Value))
                    {

                        MessageBox.Show("Duplicate");
                        result = true;
                        break;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
