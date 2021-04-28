using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabelPrinting;

namespace LabelPrinting
{
    public partial class EditPlainLabelRow : Form
    {
        DataSet ProductCode;        
        PlasmoLabelTypes plt;
        bool bIsLoading = true;

        public PlainLabel PL;
        //public int? PlainLabelID;
        //public string Code;
        //public string Description;
        //public string ItemClass;
        //public string LabelNo;
        //public string Purpose;
        //public string LastUpdatedBy;
        //public string LastUpdatedOn;                
        public LabelDictionary Labels;


        public EditPlainLabelRow()
        {
            InitializeComponent();
            LoadCombos();
            
        }

        private void LoadCombos()
        {
            try
            {
                ProductCode = new DataService.ProductDataService().GetPlasmoProductIndex();
                                                           
                cboCode.DisplayMember = "Code";
                cboCode.ValueMember = "Code";
                this.cboCode.DataSource = ProductCode.Tables[0].DefaultView;
                var appSettings = ConfigurationManager.AppSettings;
                string appSetting = appSettings["OtherLabelsGroup"] ?? "Not Found";
                string[] otherLabels = appSetting.Split(',');
                int index = -1;
                foreach (string lbl in otherLabels)
                {
                    index += 1;
                    cboLabels.Items.Add(lbl);                    
                }
            }            
            catch
            {
                throw;
            }
            
        }


        private void EditPlainLabelRow_Load(object sender, EventArgs e)
        {
            try
            {
                bIsLoading = true;
                //subForm.PlainLabelID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PlainLabelID"].ToString());
                //subForm.Code = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PlainLabelID"].ToString();
                //subForm.Description = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Description"].ToString();
                //subForm.ItemClass = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["ItemClass"].ToString();
                //subForm.LabelNo = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["LabelNo"].ToString();
                //subForm.Purpose = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Purpose"].ToString();
                //subForm.LastUpdatedBy = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["LastUpdatedBy"].ToString();
                //subForm.LastUpdatedOn = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["LastUpdatedOn"].ToString();
                this.txtPlainLabelID.Text = PL.PlainLabelID.ToString();
                //this.cboCode.SelectedText = this.Code;
                this.txtDescription.Text = PL.Description;
                this.txtItemClass.Text = PL.ItemClass;
                //this.cboLabelNo.SelectedItem = this.LabelNo;
                this.txtPurpose.Text = PL.Purpose;
                this.txtLastUpdatedBy.Text = PL.last_updated_by;
                this.txtLastUpdatedOn.Text = PL.last_updated_on.ToString();
                cboCode.SelectedIndex = -1;
                cboLabels.SelectedIndex = -1;
                int search = -1;
                if (PL.Code != null)
                {
                    search = cboCode.FindString(PL.Code.Trim());
                    cboCode.SelectedIndex = search;
                }             
                if (PL.LabelNo != null)
                {
                    search = cboLabels.FindString(PL.LabelNo.Trim());
                    cboLabels.SelectedIndex = search;
                }

                bIsLoading = false;                                    
            }
            catch (Exception ex)
            {
                //throw;
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                bool bErrors = false;
                errorProvider1.SetError(cboCode, "");
                errorProvider1.SetError(cboLabels, "");
                errorProvider1.SetError(txtItemClass, "");
                errorProvider1.SetError(txtPurpose, "");
                if (this.cboCode.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cboCode, "You must select a Code");
                    bErrors = true;
                }
                if (this.cboLabels.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cboLabels, "You must select a Label");
                    bErrors = true;
                }
                if (this.txtItemClass.Text.Trim().Length == 0)
                {
                    errorProvider1.SetError(txtItemClass, "You must enter the ItemClass");
                    bErrors = true;
                }
                if (this.txtPurpose.Text.Trim().Length == 0)
                {
                    errorProvider1.SetError(txtPurpose, "You must enter the Purpose");
                    bErrors = true;
                }
                if (bErrors)
                {
                    return;
                }
                else
                {
                    PL.Code = cboCode.SelectedValue.ToString();
                    PL.LabelNo = cboLabels.SelectedItem.ToString();
                    PL.Description = txtDescription.Text;
                    PL.ItemClass = txtItemClass.Text;
                    PL.Purpose = txtPurpose.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

     

        private void cboCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading)
            {

                //MessageBox.Show(cboCode.SelectedValue.ToString());
                string sFilter = "Code = '" + cboCode.SelectedValue.ToString() + "'";
                DataView dv = ProductCode.Tables[0].AsDataView();
                dv.RowFilter = sFilter;
                DataTable dt = dv.ToTable();
                if (dt.Rows.Count > 0)
                    txtDescription.Text = dt.Rows[0]["Description"].ToString().Trim();
            }
            
        }

        private void EditPlainLabelRow_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
