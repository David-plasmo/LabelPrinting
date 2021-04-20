using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
    public class PlainLabel
    {
        /*
        subForm.PlainLabelID = Convert.ToInt32((dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["PlainLabelID"].ToString());
        subForm.Code = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Code"].ToString();
        subForm.Description = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Description"].ToString();
        subForm.ItemClass = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["ItemClass"].ToString();
        subForm.LabelNo = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["LabelNo"].ToString();
        subForm.Purpose = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Purpose"].ToString();
        subForm.LastUpdatedBy = (dgvEdit.CurrentRow.DataBoundItem as DataRowView).Row["Last_Updated_By"].ToString();
        subForm.LastUpdatedOn 
         */
        public int? PlainLabelID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ItemClass { get; set; }
        public string LabelNo { get; set; }
        public string Purpose { get; set; }
        public string last_updated_by { get; set; }
        public DateTime? last_updated_on { get; set; }

    }
}
