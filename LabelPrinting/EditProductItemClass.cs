using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataService;


namespace LabelPrinting
{
    public partial class EditProductItemClass : Form
    {        
        ProductDataService pds;
        DataSet ItemClass, LabelType, ProductType, dsItemClassLabelLink;
        ComboBox cboCustomer;
        ComboBox cboCompanyCode;        
        ComboBox cboProductCode;
        bool bIsLoading = true;
        
                
        public EditProductItemClass()
        {
            InitializeComponent();
        }

        private void EditProductItemClass_Load(object sender, EventArgs e)
        {            
            DataGridViewCellStyle style = dgView.ColumnHeadersDefaultCellStyle;
            style.BackColor = Color.Navy;
            style.ForeColor = Color.White;
            style.Font = new Font(dgView.Font, FontStyle.Bold);
            //dgView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgView.GridColor = SystemColors.ActiveBorder;
            dgView.EnableHeadersVisualStyles = false;
            dgView.AutoGenerateColumns = true;
            pds = new ProductDataService();
            DataSet ds = pds.GetCompany();
            DataTable dt = ds.Tables[0];
            cboCompany.Items.Clear();
            cboCompany.DataSource = dt;
            cboCompany.DisplayMember = "CompanyCode";
            cboCompany.ValueMember = "CompanyCode";
            cboCompany.SelectedIndex = 0;
            cboCompany.Refresh();
            
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            try
            {
                
                DataSet dsType = pds.GetProductType();
                DataRowView dv = cboCompany.SelectedItem as DataRowView;
                string compCode = null;
                if (dv == null) { return; }
                dgView.Columns.Clear();   
                compCode = dv.Row["CompanyCode"].ToString(); 
                dsItemClassLabelLink = pds.SelectProductItemClassLabelLink(compCode);
                DataTable dt = dsItemClassLabelLink.Tables[0];
                dt.Columns["CompanyCode"].DefaultValue = compCode;
                dgView.DataSource = dt;

                dgView.Columns[0].Width = 50;
                dgView.Columns[1].Width = 50;
                dgView.Columns[2].Width = 50;
                dgView.Columns[3].Width = 100;  //ItemClass
                dgView.Columns[4].Width = 270;  //ItemClassDesc
                dgView.Columns[5].Width = 50;
                dgView.Columns[6].Width = 150;
                dgView.Columns[7].Width = 50;
                dgView.Columns[8].Width = 200;
                dgView.Columns[9].Width = 100;

                dgView.Columns[0].Visible = false;
                dgView.Columns[1].Visible = false;
                dgView.Columns[2].Visible = false;
                //dgView.Columns[5].Visible = false;
                
                dgView.Columns[7].HeaderText = "Label";
                dgView.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                
                //setup combobox column for item class
                dgView.Columns.RemoveAt(3);
                DataGridViewComboBoxColumn cbobcItemClass = new DataGridViewComboBoxColumn();
                dgView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgView_EditingControlShowing);
                cbobcItemClass.Items.Clear();
                ItemClass = pds.SelectItemClassByCompany(compCode);
                cbobcItemClass.DataSource = ItemClass.Tables[0];
                cbobcItemClass.ValueMember = "ITMCLSCD";
                cbobcItemClass.DisplayMember = "ITMCLSCD";
                cbobcItemClass.ValueType = typeof(string);
                cbobcItemClass.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cbobcItemClass.DisplayStyleForCurrentCellOnly = true;
                //cboItemClass.DisplayIndex = 2;
                cbobcItemClass.HeaderText = "ItemClass";
                cbobcItemClass.Name = "ItemClass";
                cbobcItemClass.DataPropertyName = "ItemClass";
                dgView.Columns.Insert(3, cbobcItemClass);
                dgView.Columns[3].Width = 100;

                //setup combobox column for Label Type
                dgView.Columns.RemoveAt(6);
                DataGridViewComboBoxColumn cbobcLabelType = new DataGridViewComboBoxColumn();
                cbobcLabelType.Items.Clear();
                LabelType = pds.GetLabelTypesByCompany(compCode);
                cbobcLabelType.DataSource = LabelType.Tables[0];
                cbobcLabelType.ValueMember = "LabelType";
                cbobcLabelType.DisplayMember = "LabelType";
                cbobcLabelType.ValueType = typeof(string);
                cbobcLabelType.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cbobcLabelType.DisplayStyleForCurrentCellOnly = true;                
                cbobcLabelType.HeaderText = "LabelType";
                cbobcLabelType.Name = "LabelType";
                cbobcLabelType.DataPropertyName = "LabelType";
                dgView.Columns.Insert(6, cbobcLabelType);
                dgView.Columns[6].Width = 100;

                //setup combobox column for Product Type
                dgView.Columns.RemoveAt(9);
                DataGridViewComboBoxColumn cbobcProductType = new DataGridViewComboBoxColumn();
                cbobcProductType.Items.Clear();
                ProductType = pds.GetProductType();
                cbobcProductType.DataSource = ProductType.Tables[0];
                cbobcProductType.ValueMember = "ProductType";
                cbobcProductType.DisplayMember = "ProductType";
                cbobcProductType.ValueType = typeof(string);
                cbobcProductType.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cbobcProductType.DisplayStyleForCurrentCellOnly = true;
                cbobcProductType.HeaderText = "ProductType";
                cbobcProductType.Name = "ProductType";
                cbobcProductType.DataPropertyName = "ProductType";
                dgView.Columns.Insert(9, cbobcProductType);
                dgView.Columns[9].Width = 100;
                
                dgView.Columns["ItemClass"].DefaultCellStyle.BackColor = SystemColors.Window;    //editable background colours
                dgView.Columns["LabelType"].DefaultCellStyle.BackColor = SystemColors.Window;
                dgView.Columns["ProductType"].DefaultCellStyle.BackColor = SystemColors.Window;
                dgView.Columns["ItemClassDesc"].DefaultCellStyle.BackColor = Color.Cornsilk;     //read only background colours
                dgView.Columns["CompanyCode"].DefaultCellStyle.BackColor = Color.Cornsilk;
                dgView.Columns["LabelNo"].DefaultCellStyle.BackColor = Color.Cornsilk;
                dgView.Columns["Description"].DefaultCellStyle.BackColor = Color.Cornsilk;
                dgView.Columns["last_updated_by"].DefaultCellStyle.BackColor = Color.Cornsilk;
                dgView.Columns["last_updated_on"].DefaultCellStyle.BackColor = Color.Cornsilk;

                dgView.Columns["ItemClassDesc"].ReadOnly = true;
                dgView.Columns["CompanyCode"].ReadOnly = true;
                dgView.Columns["LabelNo"].ReadOnly = true;
                dgView.Columns["Description"].ReadOnly = true;
                dgView.Columns["last_updated_by"].ReadOnly = true;
                dgView.Columns["last_updated_on"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading)
            {
                DoUpdate();
                RefreshGrid();
            }                
        }
        private void cbobcProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dgView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ComboBox cboCurrentCell = e.Control as ComboBox; 
                if (cboCurrentCell != null)
                {
                    // Remove an existing event-handler, if present, to avoid 
                    // adding multiple handlers when the editing control is reused.
                    cboCurrentCell.SelectedIndexChanged -= new EventHandler(cboCurrentCell_SelectedIndexChanged);

                    // Add the event handler. 
                    cboCurrentCell.SelectedIndexChanged +=
                        new EventHandler(cboCurrentCell_SelectedIndexChanged);
                }               
            }
        }
        private void cboCurrentCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            DataRowView dv = cbo.SelectedItem as DataRowView;
            if (dv != null)
            {
                if (dgView.CurrentCell.OwningColumn.Name == "ItemClass")
                {
                    string itmClass = dv.Row["ITMCLSCD"].ToString();
                    string itmDesc = dv.Row["ITMCLSDC"].ToString();
                    dgView.CurrentRow.Cells["ItemClassDesc"].Value = itmDesc;
                }
                else if (dgView.CurrentCell.OwningColumn.Name == "LabelType")
                {
                    string label = dv.Row["LabelNo"].ToString();
                    string desc = dv.Row["Description"].ToString();
                    int labelTypeID = (int)dv.Row["LabelTypeID"];
                    dgView.CurrentRow.Cells["LabelNo"].Value = label;
                    dgView.CurrentRow.Cells["Description"].Value = desc;
                    dgView.CurrentRow.Cells["LabelTypeID"].Value = labelTypeID;
                }
                else if (dgView.CurrentCell.OwningColumn.Name == "ProductType")
                {
                    string prodType = dv.Row["ProductType"].ToString();                    
                    int typeID = (int)dv.Row["TypeID"];
                    dgView.CurrentRow.Cells["ProductType"].Value = prodType;                    
                    dgView.CurrentRow.Cells["TypeID"].Value = typeID;
                }
            }        
        }

       

        private void EditProductItemClass_FormClosing(object sender, FormClosingEventArgs e)
        {
            DoUpdate();            
        }

        private void DoUpdate()
        {
            try
            {
                if (dgView.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgView.EndEdit();
                ProductItemLabelLinkDAL dal = new ProductItemLabelLinkDAL();
                dal.UpdateProductItemClassLabelLink(dsItemClassLabelLink);                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void dgView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    //try
        //    //{
        //    //    //if (e.ColumnIndex == dgView.Columns.IndexOf("ItemClass")) //VALIDATE FIRST COLUMN
        //    //    int columnIndex = dgView.CurrentCell.ColumnIndex;
        //    //    string columnName = dgView.Columns[columnIndex].Name;
        //    //    if (columnName == "ItemClass")
        //    //        for (int row = 0; row < dgView.Rows.Count - 1; row++)
        //    //        {

        //    //            if (dgView.Rows[row].Cells[0].Value != null 
        //    //                && row != e.RowIndex 
        //    //                && dgView.Rows[row].Cells[0].Value.Equals(dgView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
        //    //            {
        //    //                MessageBox.Show("This value cannot be duplicated");
        //    //                dgView.CurrentCell.Value = null;
        //    //            }                        
        //    //        }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show(ex.Message);
        //    //}
        //}

        private void dgView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in ((DataGridView)sender).Rows)
                {
                    if ((r.Cells["ItemClass"].Value ?? "").ToString().Equals(e.FormattedValue.ToString())
                        && r.Index != e.RowIndex)
                    {
                        MessageBox.Show("This item class already exists.");
                        e.Cancel = true;    // prevent the value being accepted
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgView_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void dgView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Confirm Delete?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK) { 
                e.Cancel = false; }
            else {
                e.Cancel = true; }

        }

        private void dgView_Leave(object sender, EventArgs e)
        {
            DoUpdate();
        }

        //try
        //{
        //    //if (e.ColumnIndex == dgView.Columns.IndexOf("ItemClass")) //VALIDATE FIRST COLUMN
        //    int columnIndex = dgView.CurrentCell.ColumnIndex;
        //    string columnName = dgView.Columns[columnIndex].Name;
        //    if (columnName == "ItemClass")
        //        for (int row = 0; row < dgView.Rows.Count - 1; row++)
        //        {

        //            if (dgView.Rows[row].Cells[0].Value != null
        //                && row != e.RowIndex
        //                && dgView.Rows[row].Cells[0].Value.Equals(dgView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
        //            {
        //                MessageBox.Show("This value cannot be duplicated");
        //                dgView.CurrentCell.Value = null;
        //            }
        //        }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);
        //}


        private void EditProductItemClass_Shown(object sender, EventArgs e)
        {
            bIsLoading = false;            
        }

        private void dgView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
       
    }
}
