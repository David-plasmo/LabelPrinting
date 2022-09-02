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
    public partial class ItemSites : Form
    {
        public bool CloseNow;
        public string CurrentItemNmbr;
        public string CurrentDescription;
        public string DatabaseName;
        public int CurrentEntryType;

        private DataSet dsItemSite, dsItemSiteDesc, dsLocationCode, dsVendor;
        private ItemSitesDAL dal;

        private bool bIsLoading;

        public ItemSites()
        {
            InitializeComponent();
            CloseNow = false;
        }

        private void ItemSites_FormClosed(object sender, FormClosedEventArgs e)
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
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                dal.UpdateItemSite(dsItemSite);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ItemSites_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            bIsLoading = true;
            dal = new ItemSitesDAL();
            dsItemSiteDesc = dal.GetItemSite_Description();
            dsItemSite = dal.GetGPItemSiteForEdit(DatabaseName, CurrentEntryType, CurrentItemNmbr);
            dgvEdit.Columns.Clear();
            DataTable dt = dsItemSite.Tables[0];
            dt.Columns["ITEMNMBR"].DefaultValue = CurrentItemNmbr;
            dt.Columns["DatabaseName"].DefaultValue = DatabaseName;
            dt.Columns["QTYRQSTN"].DefaultValue = 0.0000;
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
            DataView dvd = dsItemSiteDesc.Tables[0].DefaultView;
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
                    if (dgvEdit.Columns[i].Name == "LOCNCODE")
                    {
                        DataGridViewComboBoxColumn cbcLocnCode = new DataGridViewComboBoxColumn();
                        cbcLocnCode.Name = "LOCNCODE";
                        dsLocationCode = dal.LookupSiteLocations(DatabaseName);
                        cbcLocnCode.DataSource = dsLocationCode.Tables[0];
                        cbcLocnCode.DisplayMember = "LOCNCODE";
                        cbcLocnCode.ValueMember = "LOCNCODE";
                        cbcLocnCode.ValueType = typeof(string);
                        cbcLocnCode.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        cbcLocnCode.DisplayStyleForCurrentCellOnly = true;
                        //cbcLocnCode.SelectedIndex = 0;
                        dgvEdit.Columns.Remove("LOCNCODE");
                        cbcLocnCode.HeaderText = sHeader;
                        dgvEdit.Columns.Insert(i, cbcLocnCode);
                        dgvEdit.Columns[i].Name = "LOCNCODE";
                        cbcLocnCode.DataPropertyName = "LOCNCODE";
                        //dgvHeader.Columns.Add(cbcRoundHow);
                    }
                    else if (dgvEdit.Columns[i].Name == "PRIMVNDR")
                    {
                        DataGridViewComboBoxColumn cbcVendor = new DataGridViewComboBoxColumn();
                        cbcVendor.Name = "PRIMVNDR";
                        dsVendor = dal.LookupVendors(DatabaseName);
                        cbcVendor.DataSource = dsVendor.Tables[0];
                        cbcVendor.DisplayMember = "DisplayValue";
                        cbcVendor.ValueMember = "VENDORID";
                        cbcVendor.ValueType = typeof(string);
                        cbcVendor.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        cbcVendor.DisplayStyleForCurrentCellOnly = true;
                        //cbcVendor.SelectedIndex = 0;
                        dgvEdit.Columns.Remove("PRIMVNDR");
                        cbcVendor.HeaderText = sHeader;
                        dgvEdit.Columns.Insert(i, cbcVendor);
                        dgvEdit.Columns[i].Name = "PRIMVNDR";
                        cbcVendor.DataPropertyName = "PRIMVNDR";
                        //dgvHeader.Columns.Add(cbcRoundHow);
                    }
                    dgvEdit.Columns["QTYRQSTN"].DefaultCellStyle.Format = "N5";
                    dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    bIsLoading = false;
                }
            }
        }


        
        private void ItemSites_Load(object sender, EventArgs e)
        {
            txtCode.Text = CurrentItemNmbr;
            txtDescription.Text = CurrentDescription;
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
            if (dgvEdit.CurrentCell.OwningColumn.Name == "QTYRQSTN" && e.Control != null)
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

        private void txtDescription_TextChanged(object sender, EventArgs e)
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
