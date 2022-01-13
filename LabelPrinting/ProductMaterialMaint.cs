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
    public partial class ProductMaterialMaint : Form
    {
        DataSet MaterialAndGrade, dsCompany;        
        ProductMaterial pm;
        bool bIsLoading;
        string CurrentCompanyCode;

        public ProductMaterialMaint()
        {
            InitializeComponent();           
        }

       
        private void SetupDataGrid()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                dgvEdit.Visible = false;
                /*
                 SELECT [PmID]
                  ,[Code]
                  ,[MaterialID]
                  ,[GradeID]
                  ,[Material]
                  ,[Grade]
                  ,[ImagePath]
                  ,[CompanyCode]
                  FROM [PlasmoIntegration].[dbo].[vuMaterialAndGrade]
                  ORDER BY CompanyCode, Code
                */
                bIsLoading = true;
                MaterialAndGrade = new DataService.ProductDataService().GetMaterialAndGrade(CurrentCompanyCode);
                DataSet ds = MaterialAndGrade.Copy();
                cboSearchCode.DataSource = ds.Tables[0];
                cboSearchCode.DisplayMember = "Code";
                cboSearchCode.ValueMember = "PmID";
                cboSearchCode.ResetText();
                dgvEdit.DataSource = null;
                dgvEdit.Columns.Clear();
                dgvEdit.DataSource = MaterialAndGrade.Tables[0];
                dgvEdit.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";//"yyyy-MM-dd HH:mm:ss.fffffff";
                dgvEdit.Columns["PmID"].Visible = false;
                dgvEdit.Columns["MaterialID"].Visible = false;
                dgvEdit.Columns["GradeID"].Visible = false;
                dgvEdit.AutoResizeColumns();
                for (int i = 0; i < dgvEdit.ColumnCount; i++) { dgvEdit.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable; }
                bIsLoading = false;
                //dgvEdit.ResumeLayout(true);
                dgvEdit.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }
        private void dgvEdit_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "Double-Click to Edit";
        }

        private void dgvEdit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditProductMaterialRow subForm = new EditProductMaterialRow();
            pm = new ProductMaterial();            
            pm.Code = (string)dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Code"].Value;
            pm.Description = (string)dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Description"].Value;
            pm.CompanyCode = (string)dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CompanyCode"].Value;
            pm.CompanyCode = (string)dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["CompanyCode"].Value;
            pm.DGNumber = dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["DGNumber"].Value.ToString();
            pm.last_updated_by = (string)dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["last_updated_by"].Value;
            int numValue;
            if (Int32.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PmID"].ToString(), out numValue))
                pm.PmID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PmID"].ToString());
            if (Int32.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["GradeID"].ToString(), out numValue))
                pm.GradeID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["GradeID"].ToString());
            if (Int32.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["MaterialID"].ToString(), out numValue))
                pm.MaterialID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["MaterialID"].ToString());
            DateTime dtValue;
            if (DateTime.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["last_updated_on"].ToString(), out dtValue))
                pm.last_updated_on = Convert.ToDateTime((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["last_updated_on"]);

            subForm.PM = pm;            
            subForm.ShowDialog();            
            if (subForm.DialogResult == DialogResult.OK )
            {
                pm = subForm.PM;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["GradeID"].Value = (int)pm.GradeID;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["MaterialID"].Value = (pm.MaterialID != null ?(int)pm.MaterialID : 0);
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Material"].Value = subForm.MaterialText;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Grade"].Value = subForm.GradeText;
                dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["DGNumber"].Value = pm.DGNumber;
            }               
        }

        private void ProductMaterialMaint_Load(object sender, EventArgs e)
        {
            bIsLoading = true;            
            

            dsCompany = new DataService.ProductDataService().GetCompany();
            DataView dv = new DataView(dsCompany.Tables[0]);
            //dv.RowFilter = "CompanyCode <> 'PL'";
            //this.cboCompanyCode = new ComboBox();
            this.cboCompanyCode.ValueMember = "CMPANYID";
            this.cboCompanyCode.DisplayMember = "CompanyCode";
            this.cboCompanyCode.DataSource = dv;
            //this.cboCompanyCode.Visible = false;
            cboSearchCode.ResetText();
            CurrentCompanyCode = cboCompanyCode.Text;
            //this.dgvEdit.Controls.Add(this.cboCompanyCode);
            //Associate the event-handling method with the 
            // SelectedIndexChanged event.
            this.cboCompanyCode.SelectedIndexChanged += new System.EventHandler(cboCompanyCode_SelectedIndexChanged);
            SetupDataGrid();
            
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                DataService.ProductDataService ds = new DataService.ProductDataService();
                ds.UpdateProductMaterial(MaterialAndGrade);
                this.Close();
            }
            catch
            {
                throw;
            }
        }

        private void cboSearchCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bIsLoading || cboSearchCode.SelectedIndex < 0)
                return;

            //int iFindNo = 14;
            // int iFindNo = (int)cboSearchCode.SelectedValue;
            //int selectedValue;
            //bool parseOK = Int32.TryParse(cboSearchCode.SelectedValue.ToString(), out selectedValue);
            DataRowView drv = (DataRowView)cboSearchCode.SelectedItem;
            int iFindNo = (int)drv.Row.ItemArray[0];

            int j = (int)dgvEdit.Rows.Count - 1;
            int iRowIndex = -1;
            for (int i = 0; i < Convert.ToInt32(dgvEdit.Rows.Count / 2) + 1; i++)
            {
                if (Convert.ToInt32(dgvEdit.Rows[i].Cells[0].Value) == iFindNo)
                {
                    iRowIndex = i;
                    break;
                }
                if (Convert.ToInt32(dgvEdit.Rows[j].Cells[0].Value) == iFindNo)
                {
                    iRowIndex = j;
                    break;
                }
                j--;
            }
           
            if (iRowIndex > -1)
            {
                dgvEdit.CurrentCell = dgvEdit.Rows[iRowIndex].Cells[1];
                dgvEdit.Rows[dgvEdit.CurrentCell.RowIndex].Selected = true;
            }
            else
                MessageBox.Show("Index not found.");
        }

        private void ProductMaterialMaint_Shown(object sender, EventArgs e)
        {
            bIsLoading = false;
        }

        private void cboCompanyCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading)
            {
                int pmID = (int)cboCompanyCode.SelectedValue;
                DataTable table = dsCompany.Copy().Tables[0];
                DataRow[] row = table.Select("CMPANYID = " + pmID.ToString());
                string compCode = row[0]["CompanyCode"].ToString();
                CurrentCompanyCode = compCode;
                SetupDataGrid();
                //CurrentCompanyCode = compCode;
                //DataSet ds = pds.GetCustomerIndexByCompany(compCode);                                
                //LoadProductCbo(compCode);
                // locate product in grid goes here
            }
        }        
    }
    
}
