using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabelPrinting;

namespace LabelPrinting
{
    public partial class EditPlainLabels : Form
    {
        delegate void SetComboBoxCellType(int iRowIndex);
        public event DataGridViewCellEventHandler RowValidated;
        DataSet PlainLabels;
         
        //EditPlainLabelRow subForm;
        bool bIsLoading;
        public LabelDictionary Labels;

        public EditPlainLabels()
        {
            InitializeComponent();            
        }

        private void LoadPlainLabels()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                dgvEdit.Visible = false;                              
                bIsLoading = true;                
                PlainLabels = new DataService.ProductDataService().GetPlainLabels();
                dgvEdit.DataSource = null;                           
                dgvEdit.Columns.Clear();               
                dgvEdit.DataSource = PlainLabels.Tables[0];
                dgvEdit.EditMode = DataGridViewEditMode.EditOnEnter;
                dgvEdit.Columns["Description"].ReadOnly = true;                
                dgvEdit.Columns["PlainLabelID"].Visible = true;                
                dgvEdit.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";//"yyyy-MM-dd HH:mm:ss.fffffff";
                dgvEdit.Columns["last_updated_on"].Visible = true;
                dgvEdit.Columns["last_updated_by"].Visible = true;
                dgvEdit.Columns["last_updated_by"].ReadOnly = true;
                dgvEdit.Columns["last_updated_on"].ReadOnly = true;
                
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
        
        private void EditPlainLabels_FormClosing(object sender, FormClosingEventArgs e)
        {
                
        }
        private void UpdateGridRow(PlainLabel pl)
        {
            if (pl.PlainLabelID.HasValue) dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["PlainLabelID"].Value = pl.PlainLabelID.Value;
            else dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["PlainLabelID"].Value = DBNull.Value;
            
            if (pl.last_updated_on.HasValue) dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["last_updated_on"].Value = pl.last_updated_on.Value;
            else dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["last_updated_on"].Value = DBNull.Value;

            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Code"].Value = pl.Code;
            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Description"].Value = pl.Description;
            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["ItemClass"].Value = pl.ItemClass;
            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["LabelNo"].Value = pl.LabelNo;
            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["Purpose"].Value = pl.Purpose;
            dgvEdit.Rows[dgvEdit.CurrentRow.Index].Cells["last_updated_by"].Value = pl.last_updated_by;
        }

        private void DoEdit()
        {
            EditPlainLabelRow subForm = new EditPlainLabelRow();
            PlainLabel pl = new PlainLabel();
            if (dgvEdit.CurrentRow.DataBoundItem != null)
            {
                /*
                subForm.PlainLabelID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PlainLabelID"].ToString());
                subForm.Code = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Code"].ToString();
                subForm.Description = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Description"].ToString();
                subForm.Description = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["ItemClass"].ToString();
                subForm.LabelNo = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["LabelNo"].ToString();
                subForm.Purpose = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Purpose"].ToString();
                subForm.LastUpdatedBy = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Last_Updated_By"].ToString();
                subForm.LastUpdatedOn 
                */
                int numValue;
                if (Int32.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PlainLabelID"].ToString(), out numValue))
                    pl.PlainLabelID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PlainLabelID"].ToString());
                else pl.PlainLabelID = (int?)null;
                DateTime dtValue;
                if (DateTime.TryParse((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["last_updated_on"].ToString(), out dtValue))
                    pl.last_updated_on = Convert.ToDateTime((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["last_updated_on"]);
                else pl.last_updated_on = (DateTime?)null;               
                pl.Code = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Code"].ToString();
                pl.Description = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Description"].ToString();
                pl.ItemClass = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["ItemClass"].ToString();
                pl.Purpose = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Purpose"].ToString();
                pl.LabelNo = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["LabelNo"].ToString();
                pl.last_updated_by = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["last_updated_by"].ToString();                               
            }

            subForm.PL = pl;
            subForm.ShowDialog();
            if (subForm.DialogResult == DialogResult.OK)
            {
                pl = subForm.PL;
                UpdateGridRow(pl);
                dgvEdit.NotifyCurrentCellDirty(true);
                dgvEdit.EndEdit();
                dgvEdit.NotifyCurrentCellDirty(false);
            }
        }
    

    private void dgvEdit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        DoEdit();
           
    }

        private void dgvEdit_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            e.ToolTipText = "Double-Click to Edit";
        }

        private void EditPlainLabels_Load(object sender, EventArgs e)
        {
            LoadPlainLabels();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {            
            try
            {
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                DataService.ProductDataService ds = new DataService.ProductDataService();
                ds.UpdatePlainLabels(PlainLabels);
                this.Close();                
            }
            catch
            {
                throw;
            }
        }
    }
    
}
