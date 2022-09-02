using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class ItemSitesDC
	{
		public string ITEMNMBR { get; set; }
		public string LOCNCODE { get; set; }
		public string BINNMBR { get; set; }
		public string PRIMVNDR { get; set; }
		public decimal QTYRQSTN { get; set; }
		public string Landed_Cost_Group_ID { get; set; }
		public int ID { get; set; }
		public string TableName { get; set; }
		public string DatabaseName { get; set; }

		public ItemSitesDC()
        {

        }

		public ItemSitesDC(string ITEMNMBR_, string LOCNCODE_, string BINNMBR_, string PRIMVNDR_, decimal QTYRQSTN_, string Landed_Cost_Group_ID_, int ID_, string TableName_, string DatabaseName_)
		{
			this.ITEMNMBR = ITEMNMBR_;
			this.LOCNCODE = LOCNCODE_;
			this.BINNMBR = BINNMBR_;
			this.PRIMVNDR = PRIMVNDR_;
			this.QTYRQSTN = QTYRQSTN_;
			this.Landed_Cost_Group_ID = Landed_Cost_Group_ID_;
			this.ID = ID_;
			this.TableName = TableName_;
			this.DatabaseName = DatabaseName_;
		}
	}
}
