using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class ProductItemClassLabelLink
	{
		public int PiclID { get; set; }
		public int TypeID { get; set; }
		public string ItemClass { get; set; }
		public string ItemClassDesc { get; set; }
		public string LabelNo { get; set; }
		public int LabelTypeID { get; set; }
		public string CompanyCode { get; set; }
		public string last_updated_by { get; set; }
		public DateTime last_updated_on { get; set; }

		public ProductItemClassLabelLink()
        {
        }
		public ProductItemClassLabelLink(int PiclID_, int TypeID_, string ItemClass_, string ItemClassDesc_, string LabelNo_, int LabelTypeID_, string CompanyCode_, string last_updated_by_, DateTime last_updated_on_)
		{
			this.PiclID = PiclID_;
			this.TypeID = TypeID_;
			this.ItemClass = ItemClass_;
			this.ItemClassDesc = ItemClassDesc_;
			this.LabelNo = LabelNo_;
			this.LabelTypeID = LabelTypeID_;
			this.CompanyCode = CompanyCode_;
			this.last_updated_by = last_updated_by_;
			this.last_updated_on = last_updated_on_;
		}
	}
}
