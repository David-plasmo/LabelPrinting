using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class ItemCurrenciesDC
	{
		public string ITEMNMBR { get; set; }
		public string CURNCYID { get; set; }
		public short CURRNIDX { get; set; }
		public short DECPLCUR { get; set; }
		public decimal LISTPRCE { get; set; }
		public int ID { get; set; }
		public string TableName { get; set; }
		public string DatabaseName { get; set; }

		public ItemCurrenciesDC()
		{
		}
		public ItemCurrenciesDC(string ITEMNMBR_, string CURNCYID_, short CURRNIDX_, short DECPLCUR_, decimal LISTPRCE_, int ID_, string TableName_, string DatabaseName_)
		{
			this.ITEMNMBR = ITEMNMBR_;
			this.CURNCYID = CURNCYID_;
			this.CURRNIDX = CURRNIDX_;
			this.DECPLCUR = DECPLCUR_;
			this.LISTPRCE = LISTPRCE_;
			this.ID = ID_;
			this.TableName = TableName_;
			this.DatabaseName = DatabaseName_;
		}
	}
}
