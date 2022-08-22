using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class PriceListDC
	{
		public string ITEMNMBR { get; set; }
		public string CURNCYID { get; set; }
		public string PRCLEVEL { get; set; }
		public string UOFM { get; set; }
		public decimal TOQTY { get; set; }
		public decimal UOMPRICE { get; set; }
		public decimal RNDGAMNT { get; set; }
		public short ROUNDHOW { get; set; }
		public short ROUNDTO { get; set; }
		public decimal FROMQTY { get; set; }
		public int ID { get; set; }
		public string TableName { get; set; }
		public string DatabaseName { get; set; }
		public short UMSLSOPT { get; set; }

		public PriceListDC(string ITEMNMBR_, string CURNCYID_, string PRCLEVEL_, string UOFM_, decimal TOQTY_, decimal UOMPRICE_, decimal RNDGAMNT_, short ROUNDHOW_, short ROUNDTO_, decimal FROMQTY_, int ID_, string TableName_, string DatabaseName_, short UMSLSOPT_)
		{
			this.ITEMNMBR = ITEMNMBR_;
			this.CURNCYID = CURNCYID_;
			this.PRCLEVEL = PRCLEVEL_;
			this.UOFM = UOFM_;
			this.TOQTY = TOQTY_;
			this.UOMPRICE = UOMPRICE_;
			this.RNDGAMNT = RNDGAMNT_;
			this.ROUNDHOW = ROUNDHOW_;
			this.ROUNDTO = ROUNDTO_;
			this.FROMQTY = FROMQTY_;
			this.ID = ID_;
			this.TableName = TableName_;
			this.DatabaseName = DatabaseName_;
			this.UMSLSOPT = UMSLSOPT_;
		}
		public PriceListDC() { }
	}
}

