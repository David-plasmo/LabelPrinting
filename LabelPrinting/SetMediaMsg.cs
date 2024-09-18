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
    public partial class SetMediaMsg : Form
    {
        bool IsButtonClick = false;

        public SetMediaMsg(string msg)
        {
            InitializeComponent();
            lblMsg.Text = msg;            
        }

        public SetMediaMsg()
        {
           
        }

        private void SetMediaMsg_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            PlasmoLabelTypes plt = new PlasmoLabelTypes();
            plt.Show();
            this.DialogResult = DialogResult.None;
            e.Cancel = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            IsButtonClick = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            IsButtonClick = true;
            this.Close();
        }

        private void SetMediaMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsButtonClick)
            {
                //User close form with Button
            }
            else
            {
                //User close form with (X) button at the top right corner of the
                //control box of a form/window
                //return cancel
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            btnHelp.Enabled = false;
            PlasmoLabelTypes plt = new PlasmoLabelTypes();
            plt.ShowDialog();
            this.DialogResult = DialogResult.None;
            btnHelp.Enabled = true;
        }
    }
}
