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
    public partial class ItemCurrencies : Form
    {
        public bool CloseNow;
        public string CurrentItemNmbr;
        public string CurrentDescription;
        public string DatabaseName;
        public int CurrentEntryType;

        private DataSet dsItemCurrency, dsItemCurrencyDesc, dsCurrency;
        private ItemCurrenciesDAL dal;

        private bool bIsLoading;

        public ItemCurrencies()
        {
            InitializeComponent();
            CloseNow = false;
        }

        private void ItemCurrencies_FormClosed(object sender, FormClosedEventArgs e)
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
                dal.UpdateItemCurrency(dsItemCurrency);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ItemCurrencies_Shown(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            bIsLoading = true;
            dal = new ItemCurrenciesDAL();
            dsItemCurrencyDesc = dal.GetItemCurrency_Description();
            dsItemCurrency = dal.GetGPItemCurrencyForEdit(DatabaseName, CurrentEntryType, CurrentItemNmbr);
            dgvEdit.Columns.Clear();
            DataTable dt = dsItemCurrency.Tables[0];
            dt.Columns["ITEMNMBR"].DefaultValue = CurrentItemNmbr;
            dt.Columns["DatabaseName"].DefaultValue = DatabaseName;
            dt.Columns["LISTPRCE"].DefaultValue = 0.000;
            dt.Columns["DECPLCUR"].DefaultValue = 3;
            dt.Columns["TableName"].DefaultValue = "IV00105";

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
            DataView dvd = dsItemCurrencyDesc.Tables[0].DefaultView;
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
                    if (dgvEdit.Columns[i].Name == "CURNCYID")
                    {
                        DataGridViewComboBoxColumn cbcCurrency = new DataGridViewComboBoxColumn();
                        cbcCurrency.Name = "CURNCYID";
                        dsCurrency = dal.LookupCurrencies();
                        cbcCurrency.DataSource = dsCurrency.Tables[0];
                        cbcCurrency.DisplayMember = "CURNCYID";
                        cbcCurrency.ValueMember = "CURNCYID";
                        cbcCurrency.ValueType = typeof(string);
                        cbcCurrency.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                        cbcCurrency.DisplayStyleForCurrentCellOnly = true;
                        //cbcCurrency.SelectedIndex = 0;
                        dgvEdit.Columns.Remove("CURNCYID");
                        cbcCurrency.HeaderText = sHeader;
                        dgvEdit.Columns.Insert(i, cbcCurrency);
                        dgvEdit.Columns[i].Name = "CURNCYID";
                        cbcCurrency.DataPropertyName = "CURNCYID";
                        //dgvHeader.Columns.Add(cbcRoundHow);
                    }                   
                    dgvEdit.Columns["LISTPRCE"].DefaultCellStyle.Format = "N3";
                    dgvEdit.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    bIsLoading = false;
                }
            }
        }


        
        private void ItemCurrencies_Load(object sender, EventArgs e)
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

            if (dgvEdit.Columns[e.ColumnIndex].Name == "CURNCYID")
            {
                bool rowOK = !IsDuplicate(dgvEdit, "CURNCYID", e.RowIndex);
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
