using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class ItemVendorsDC
	{
		public string ITEMNMBR { get; set; }
		public string VENDORID { get; set; }
		public short ITMVNDTY { get; set; }
		public string VNDITNUM { get; set; }
		public decimal QTYRQSTN { get; set; }
		public int AVRGLDTM { get; set; }
		public short NORCTITM { get; set; }
		public decimal MINORQTY { get; set; }
		public decimal MAXORDQTY { get; set; }
		public decimal ECORDQTY { get; set; }
		public string VNDITDSC { get; set; }
		public decimal Last_Originating_Cost { get; set; }
		public string Last_Currency_ID { get; set; }
		public short FREEONBOARD { get; set; }
		public string PRCHSUOM { get; set; }
		public short CURRNIDX { get; set; }
		public short PLANNINGLEADTIME { get; set; }
		public decimal ORDERMULTIPLE { get; set; }
		public string MNFCTRITMNMBR { get; set; }
		public int ID { get; set; }
		public string TableName { get; set; }
		public string DatabaseName { get; set; }

		public ItemVendorsDC()
        {

        }

		public ItemVendorsDC(string ITEMNMBR_, string VENDORID_, short ITMVNDTY_, string VNDITNUM_, decimal QTYRQSTN_, int AVRGLDTM_, short NORCTITM_, decimal MINORQTY_, decimal MAXORDQTY_, decimal ECORDQTY_, string VNDITDSC_, decimal Last_Originating_Cost_, string Last_Currency_ID_, short FREEONBOARD_, string PRCHSUOM_, short CURRNIDX_, short PLANNINGLEADTIME_, decimal ORDERMULTIPLE_, string MNFCTRITMNMBR_, int ID_, string TableName_, string DatabaseName_)
		{
			this.ITEMNMBR = ITEMNMBR_;
			this.VENDORID = VENDORID_;
			this.ITMVNDTY = ITMVNDTY_;
			this.VNDITNUM = VNDITNUM_;
			this.QTYRQSTN = QTYRQSTN_;
			this.AVRGLDTM = AVRGLDTM_;
			this.NORCTITM = NORCTITM_;
			this.MINORQTY = MINORQTY_;
			this.MAXORDQTY = MAXORDQTY_;
			this.ECORDQTY = ECORDQTY_;
			this.VNDITDSC = VNDITDSC_;
			this.Last_Originating_Cost = Last_Originating_Cost_;
			this.Last_Currency_ID = Last_Currency_ID_;
			this.FREEONBOARD = FREEONBOARD_;
			this.PRCHSUOM = PRCHSUOM_;
			this.CURRNIDX = CURRNIDX_;
			this.PLANNINGLEADTIME = PLANNINGLEADTIME_;
			this.ORDERMULTIPLE = ORDERMULTIPLE_;
			this.MNFCTRITMNMBR = MNFCTRITMNMBR_;
			this.ID = ID_;
			this.TableName = TableName_;
			this.DatabaseName = DatabaseName_;
		}
	}
}
