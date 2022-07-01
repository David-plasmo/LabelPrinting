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
    public partial class LabelOptions : Form
    {
        public DataSet BMPrintLabels;
        public int LabelTypeID;

        public LabelOptions(DataSet bmPrintLabels, int labelTypeID)
        {
            BMPrintLabels = bmPrintLabels;
            LabelTypeID = labelTypeID;

            InitializeComponent();
        }

        private void LabelOptions_Load(object sender, EventArgs e)
        {
            dgvLabels.Columns.Clear();
            if (BMPrintLabels != null && BMPrintLabels.Tables.Count > 0)
            {
                dgvLabels.DataSource = BMPrintLabels.Tables[0];
                dgvLabels.Columns["ID"].Visible = false;
                dgvLabels.Columns["BottleSize"].Visible = (LabelTypeID == 2 || LabelTypeID == 7) ? true : false;
                dgvLabels.Columns["Style"].Visible = (LabelTypeID == 2 || LabelTypeID == 7) ? true : false;
                dgvLabels.Columns["NeckSize"].Visible = (LabelTypeID == 2 || LabelTypeID == 7) ? true : false;
                dgvLabels.Columns["Colour"].Visible = (LabelTypeID == 2 || LabelTypeID == 7) ? true : false;
                dgvLabels.Columns["Material"].Visible = (LabelTypeID == 2 || LabelTypeID == 7) ? true : false;
                dgvLabels.Columns["JobRun"].Visible = (LabelTypeID == 2 || LabelTypeID == 3) ? true : false;
                dgvLabels.Columns["BottleSize"].ReadOnly = (LabelTypeID == 7) ? true : false;
                dgvLabels.Columns["NeckSize"].ReadOnly = (LabelTypeID == 7) ? true : false;
                dgvLabels.Columns["Colour"].ReadOnly = (LabelTypeID == 7) ? true : false;
                dgvLabels.Columns["Material"].ReadOnly = (LabelTypeID == 7) ? true : false;
                dgvLabels.AutoResizeColumns();
                //btnPrint.Enabled = (dgvBMLabels.RowCount > 0 ? true : false);
                // lblProductType.Text =  "Non Stock Product Labels: Job " + RunNmbr + " - " + manCompany;                              
                //lblProductType.Visible = (dgvBMLabels.RowCount > 0 ? true : false);
                //lblNumSpare.Visible = (dgvBMLabels.RowCount > 0 ? true : false);
                //txtNumSpare.Visible = (dgvBMLabels.RowCount > 0 ? true : false);
                //btnSetupLabels.Enabled = false;
                dgvLabels.Visible = true;
                //lblPrinter.Visible = true;
                //cboPrinter.Visible = true;
                dgvLabels.Focus();
            }
        }

        private void LabelOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { dgvLabels.EndEdit(); }
            catch(Exception ex) { MessageBox.Show(ex.Message); }            
        }
    }
}
