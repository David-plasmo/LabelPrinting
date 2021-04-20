using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Production;
using System.Windows.Forms;

namespace LabelPrinting
{
    public partial class EditPastelMasterRow : Form
    {
        public PastelDataClass PDC;

        DataSet ProductCode;               
        DataSet PastelCategory;
        bool bIsLoading = true;
       
        //public int? PastelID;
        //public string Code;
        //public string Description;
        //public int? CategoryID;
        //public int? CtnQty;
        //public string CtnSize;
        //public string Grade;
        //public string LastUpdatedBy;
        //public string LastUpdatedOn;
        public LabelDictionary Labels;
        public EditPastelMasterRow()
        {
            InitializeComponent();
            LoadCombos();
        }

        private void LoadCombos()
        {
            try
            {                
                //cboCode.DisplayMember = "Code";
                //cboCode.ValueMember = "Code";
                //this.cboCode.DataSource = ProductCode.Tables[0].DefaultView;
                

                PastelCategory = new DataService.ProductDataService().GetPastelCategory();
                foreach (DataRow row in PastelCategory.Tables[0].Rows)
                {                   
                    row["Category"] = row["CategoryID"].ToString().PadRight(3) 
                        + row["Description"].ToString().PadRight(32) 
                        + row["LabelNo"].ToString();                    
                }
                cboCategory.DisplayMember = "Category";
                cboCategory.ValueMember = "CategoryId";
                cboCategory.Font = new Font(FontFamily.GenericMonospace.Name, 8);
                this.cboCategory.DataSource = PastelCategory.Tables[0];
                
                var appSettings = ConfigurationManager.AppSettings;
                string appSetting = appSettings["PastelCtnSizes"] ?? "Not Found";
                string[] ctnSizes = appSetting.Split(',');
                int index = -1;
                foreach (string sz in ctnSizes)
                {
                    index += 1;
                    cboCtnSize.Items.Add(sz);
                }
                appSettings = ConfigurationManager.AppSettings;
                appSetting = appSettings["Grades"] ?? "Not Found";
                string[] grades = appSetting.Split(',');
                index = -1;
                foreach (string g in grades)
                {
                    index += 1;
                    cboGrade.Items.Add(g);
                }
            }
            catch
            {
                throw;
            }

        }

        private void EditPastelMasterRow_Load(object sender, EventArgs e)
        {
            try
            {
                bIsLoading = true;
                //subForm.PastelID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PastelID"].ToString());
                //subForm.Code = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Code"].ToString();
                //subForm.Description = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Description"].ToString();
                //subForm.CategoryID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["CategoryID"].ToString());
                //subForm.CtnQty = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["CtnQty"].ToString());
                //subForm.CtnSize = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["CtnSize"].ToString();
                //subForm.Grade = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Grade"].ToString();
                //subForm.LastUpdatedBy = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Last_Updated_By"].ToString();
                //subForm.LastUpdatedOn = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Last_Updated_On"].ToString();
                this.txtPastelID.Text = (PDC.PastelID.HasValue ? PDC.PastelID.ToString() :"");                
                this.txtCtnQty.Text = (PDC.CtnQty.HasValue ? PDC.CtnQty.ToString() : "");
                this.txtLastUpdatedOn.Text = (PDC.last_updated_on.HasValue ? PDC.last_updated_on.ToString() : "");
                this.txtDescription.Text = PDC.Description;
                this.txtDescription.ReadOnly = false;                           
                this.txtLastUpdatedBy.Text = PDC.last_updated_by;                
                this.txtCode.Text = PDC.Code;
                
                //allow edits on Code and Description for new record only:-
                this.txtDescription.ReadOnly = (PDC.PastelID != null);
                this.txtCode.ReadOnly = (PDC.PastelID != null);
                lblMandatory.Visible = (PDC.PastelID == null);
                label14.Visible = (PDC.PastelID == null);


                if (PDC.CategoryID != null)
                {
                    //int index = cboCategory.Items.IndexOf(this.CategoryID.ToString());
                    int index = cboCategory.FindString(PDC.CategoryID.ToString());
                    cboCategory.SelectedIndex = index;
                    //Console.WriteLine("{0,-10}\t{1,-5}\t{2,-5}\t{3,-10}", ID, Status, Available, Count);                    
                }              
                if (PDC.CtnSize != null)
                {
                    int search = cboCtnSize.FindString(PDC.CtnSize.Trim());
                    cboCtnSize.SelectedIndex = search;
                }
                    
                if (PDC.Grade != null)
                {
                    int search = cboGrade.FindString(PDC.Grade.Trim());
                    cboGrade.SelectedIndex = search;
                }

                bIsLoading = false;
            }
            catch (Exception ex)
            {
                //throw;
                MessageBox.Show(ex.Message);
            }

        }

       
           

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                bool bErrors = false;
                errorProvider1.SetError(txtCode, "");
                errorProvider1.SetError(txtDescription, "");
                errorProvider1.SetError(cboCategory, "");
                errorProvider1.SetError(txtCtnQty, "");
                errorProvider1.SetError(cboCtnSize, "");
                errorProvider1.SetError(cboGrade, "");

                //Tests applying for a new row:-
                if (PDC.PastelID == null)
                {
                    if (this.txtCode.Text.Trim().Length == 0)
                    {
                        errorProvider1.SetError(txtCode, "You must enter a code");
                        bErrors = true;
                    }
                    else
                    {
                        // test for unique code
                        ProductCode = new DataService.ProductDataService().GetPastelProductIndex();
                        DataRow[] dr = ProductCode.Tables[0].Select("Code = '" + txtCode.Text.Trim() + "'");
                        if (dr.Length != 0)
                        {
                            errorProvider1.SetError(txtCode, "This code already exists");
                            bErrors = true;
                        }
                    }
                    if (this.txtDescription.Text.Trim().Length == 0)
                    {
                        errorProvider1.SetError(txtDescription, "You must enter a description");
                        bErrors = true;
                    }
                }
                
                if (this.cboCategory.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cboCategory, "You must select a Category");
                    bErrors = true;
                }
                if (this.txtCtnQty.Text.Trim().Length == 0)
                {
                    errorProvider1.SetError(txtCtnQty, "You must enter the Carton Quantity");
                    bErrors = true;
                }
                else
                {
                    int testNonZero = Convert.ToInt32(txtCtnQty.Text);
                    if (testNonZero == 0)
                    {
                        errorProvider1.SetError(txtCtnQty, "Carton Quantity must not be zero");
                        bErrors = true;
                    }
                }                
                if (this.cboCtnSize.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cboCtnSize, "You must select a carton size");
                    bErrors = true;
                }
                if (this.cboGrade.SelectedIndex == -1)
                {
                    errorProvider1.SetError(cboGrade, "You must select a grade");
                    bErrors = true;
                }                
                if (bErrors)
                {
                    return;
                }
                else
                {
                    PDC.Code = txtCode.Text.Trim();
                    PDC.CategoryID = Convert.ToInt32(cboCategory.SelectedValue);
                    PDC.Description = txtDescription.Text;
                    PDC.CtnQty = Convert.ToInt32(txtCtnQty.Text);
                    PDC.CtnSize = cboCtnSize.SelectedItem.ToString();
                    PDC.Grade = cboGrade.SelectedItem.ToString();
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtCtnQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow integer only
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) 
                && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bIsLoading)
             PDC.CategoryID = Convert.ToInt32(cboCategory.SelectedValue.ToString());
        }
    }
}
