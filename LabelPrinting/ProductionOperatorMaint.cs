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
    public partial class ProductionOperatorMaint : Form
    {
        ProductionOperatorDAL dal;
        DataSet dsOperator, dsOperatorClass;

        bool bIsLoading = true;

        private void ProductionOperatorMaint_Load(object sender, EventArgs e)
        {
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
            dal = new ProductionOperatorDAL();
            DataSet ds = dal.SelectProductionOperator();
            DataTable dt = ds.Tables[0];

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {                
                dsOperator = dal.SelectProductionOperator();
                DataTable dt = dsOperator.Tables[0];
                dgvEdit.DataSource = dt;

                dgvEdit.Columns[0].Width = 50;
                dgvEdit.Columns[1].Width = 50;
                dgvEdit.Columns[2].Width = 200;
                dgvEdit.Columns[3].Width = 50;
                dgvEdit.Columns[4].Width = 100;
                dgvEdit.Columns[5].Width = 200;                

                dgvEdit.Columns[0].Visible = false;
                dgvEdit.Columns[1].Visible = false;
                
                dgvEdit.Columns[3].HeaderText = "Code";
                dgvEdit.Columns["last_updated_on"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

                //setup combobox column for operator type
                dgvEdit.Columns.RemoveAt(4);
                DataGridViewComboBoxColumn cbobcOperatorClass = new DataGridViewComboBoxColumn();
                dgvEdit.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgvEdit_EditingControlShowing);
                cbobcOperatorClass.Items.Clear();
                dsOperatorClass = dal.SelectOperatorClass();
                cbobcOperatorClass.DataSource = dsOperatorClass.Tables[0];
                cbobcOperatorClass.ValueMember = "OperatorClassID";
                cbobcOperatorClass.DisplayMember = "OperatorClass";
                cbobcOperatorClass.ValueType = typeof(int);
                cbobcOperatorClass.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cbobcOperatorClass.DisplayStyleForCurrentCellOnly = true;
                //cboItemClass.DisplayIndex = 2;
                cbobcOperatorClass.HeaderText = "Type";
                cbobcOperatorClass.Name = "OperatorClass";
                cbobcOperatorClass.DataPropertyName = "OperatorClass";
                dgvEdit.Columns.Insert(4, cbobcOperatorClass);
                dgvEdit.Columns[3].Width = 100;
                dgvEdit.Columns["last_updated_by"].ReadOnly = true;
                dgvEdit.Columns["last_updated_on"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cboCurrentCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            DataRowView dv = cbo.SelectedItem as DataRowView;
            if (dv != null)
            {
                if (dgvEdit.CurrentCell.OwningColumn.Name == "OperatorClass")
                {
                    string operatorClass = dv.Row["OperatorClass"].ToString();
                    int classID = (int)dv.Row["OperatorClassID"];
                    dgvEdit.CurrentRow.Cells["OperatorClass"].Value = operatorClass;
                    dgvEdit.CurrentRow.Cells["OperatorClassID"].Value = classID;
                }
            }
        }
        private void dgvEdit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        private void dgvEdit_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {                        
            DataGridViewCell cell = dgvEdit.CurrentCell as DataGridViewCell;
            if (cell != null && cell.OwningColumn.Name == "OperatorCode" && cell.Value != e.FormattedValue)
            {
                foreach (DataGridViewRow r in ((DataGridView)sender).Rows)
                {
                    if ((r.Cells["OperatorCode"].Value ?? "").ToString().Equals(e.FormattedValue.ToString()))
                    {
                        MessageBox.Show("This operator code already exists: " + e.FormattedValue.ToString());
                        e.Cancel = true;    // prevent the value being accepted
                    }
                }
            }
            
        }

        private void ProductionOperatorMaint_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (dgvEdit.IsCurrentRowDirty)
                {
                    this.Validate();
                }
                dgvEdit.EndEdit();
                dal.UpdateProductionOperator(dsOperator);
                //this.Close();
            }
            catch
            {
                throw;
            }
        }

        private void ProductionOperatorMaint_Shown(object sender, EventArgs e)
        {
            bIsLoading = false;
        }

        private void dgvEdit_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        public ProductionOperatorMaint()
        {
            InitializeComponent();
        }
    }
}
