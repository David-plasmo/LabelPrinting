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
    public partial class EditProductMaterialRow : Form
    {
        public ProductMaterial PM;
        DataSet Material;
        DataSet Grade;
        public string MaterialText;
        public string GradeText;
                               
        public EditProductMaterialRow()
        {
            InitializeComponent();            
        }

        private void EditProductMaterialRow_Load(object sender, EventArgs e)
        {
            try
            {               
                txtCode.Text = PM.Code;
                txtCompany.Text = PM.CompanyCode;
                txtDescription.Text = PM.Description;
                txtImagePath.Text = PM.ImagePath;
                txtLastUpdatedBy.Text = PM.last_updated_by;
                txtPmlID.Text = PM.PmID.ToString();
                if (PM.last_updated_on.ToString().Length > 0) txtLastUpdatedOn.Text = PM.last_updated_on.ToString();

                Material = new DataService.ProductDataService().GetMaterialIndex();
                cboMaterial.DisplayMember = "ShortDesc";
                cboMaterial.ValueMember = "MaterialId";
                cboMaterial.DataSource = Material.Tables[0];
                if (!PM.MaterialID.HasValue)
                {
                    cboMaterial.ResetText();
                }
                else
                {
                    string search = PM.MaterialID.ToString();
                    int selected = -1;
                    int count = cboMaterial.Items.Count;
                    for (int i = 0; (i <= (count - 1)); i++)
                    {
                        cboMaterial.SelectedIndex = i;
                        if (cboMaterial.SelectedValue.ToString() == search)
                        {
                            selected = i;
                            break;
                        }
                    }
                    cboMaterial.SelectedIndex = selected;
                }
                
                Grade = new DataService.ProductDataService().GetProductGradeIndex();
                cboGrade.DisplayMember = "ShortDesc";
                cboGrade.ValueMember = "GradeId";
                cboGrade.DataSource = Grade.Tables[0];
                if (!PM.GradeID.HasValue)
                {
                    cboGrade.ResetText();
                }
                else
                {
                    //DOESNT WORK !!!
                    //int index = cboGrade.Items.IndexOf((int)(PM.GradeID));
                    //cboGrade.SelectedIndex = index;
                    string search = PM.GradeID.ToString();
                    int selected = -1;
                    int count = cboGrade.Items.Count;
                    for (int i = 0; (i <= (count - 1)); i++)
                    {
                        cboGrade.SelectedIndex = i;
                        if (cboGrade.SelectedValue.ToString() == search)
                        {
                            selected = i;
                            break;
                        }
                    }
                    cboGrade.SelectedIndex = selected;

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

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                bool bErrors = false;               
                errorProvider1.SetError(cboGrade, "");
                errorProvider1.SetError(cboMaterial, "");
                                             
                if (this.cboGrade.Text.Length == 0)
                {
                    errorProvider1.SetError(cboGrade, "You must select a Grade");
                    bErrors = true;
                }
                /*
                if (this.cboMaterial.Text.Length == 0)
                {
                    errorProvider1.SetError(cboMaterial, "You must select a Material");
                    bErrors = true;
                }
                */               
                if (bErrors)
                {
                    return;
                }
                else
                {                    
                    PM.GradeID = Convert.ToInt32(cboGrade.SelectedValue);
                    if (this.cboMaterial.Text.Length != 0) { PM.MaterialID = Convert.ToInt32(cboMaterial.SelectedValue); } else { PM.MaterialID = null; }                      
                    this.MaterialText = cboMaterial.Text;
                    this.GradeText = cboGrade.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
