using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace LabelPrinting
{
    public partial class PlasmoLabelTypes : Form
    {
        public bool bAcceptButton = false;
        public string LabelType;
        public PlasmoLabelTypes()
        {
            InitializeComponent();            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.DialogResult = DialogResult.None;
           this.Close();
        }

        private void PlasmoLabelTypes_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataService.ProductDataService().GetLabelTypesTV();
                DataViewRowState dvrs = DataViewRowState.CurrentRows;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);

                string thisLabel = "";
                string lastLabel = "";
                string labelStart = "P4P5P6P7";

                TreeNode treeNode = null;
                TreeNode node2 = null;

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    
                    thisLabel = dr["LabelNo"].ToString();
                    if (labelStart.IndexOf(thisLabel.Substring(0, 2)) >= 0)
                    {
                        //put labels P4 - P7 into the same node
                        thisLabel = "P4 - P7";
                    }
                       
                    if (thisLabel != lastLabel)
                    {
                        if (treeNode != null)
                        {
                            tvLabels.Nodes.Add(treeNode);
                            treeNode = new TreeNode(thisLabel);
                        }                           
                        else
                        {                            
                            // This is the first node in the view.  
                            treeNode = new TreeNode(thisLabel);                            
                        }                            
                    }
                    string desc = dr["Description"].ToString();
                    node2 = new TreeNode(desc);
                    node2.Name = dr["LabelType"].ToString();
                    treeNode.Nodes.Add(node2);
                    lastLabel = thisLabel;                                      
                }
                //final node
                tvLabels.Nodes.Add(treeNode);
                tvLabels.ExpandAll();
                tvLabels.SelectedNode = tvLabels.Nodes[0].Nodes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void tvLabels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                label1.Text = e.Node.Name;
                var appSettings = System.Configuration.ConfigurationManager.AppSettings;                
                string imagePath = appSettings[e.Node.Name] ?? "Not Found";                
                picBox1.Image = new Bitmap(imagePath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.LabelType = label1.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //this.LabelType = label1.Text;
            this.Close();
        }

        private void PlasmoLabelTypes_Shown(object sender, EventArgs e)
        {

        }

        private void PlasmoLabelTypes_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (bAcceptButton)
                {
                    this.btnAccept.Visible = true;
                    this.btnCancel.Visible = true;
                    this.btnClose.Visible = false;
                }
                else
                {
                    this.btnAccept.Visible = false;
                    this.btnCancel.Visible = false;
                    this.btnClose.Visible = true;
                }
            }
            
        }
    }
}
