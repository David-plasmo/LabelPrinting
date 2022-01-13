using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class LabelPrintJobDC
	{
		public int JobID { get; set; }
		public int LabelTypeId { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public int CtnQty { get; set; }
		public int StartNo { get; set; }
		public int EndNo { get; set; }
		public string BottleSize { get; set; }
		public string Style { get; set; }
		public string NeckSize { get; set; }
		public string Colour { get; set; }
		public string Material { get; set; }
		public int NumReqd { get; set; }
		public int JobRun { get; set; }
		public string CompanyCode { get; set; }
		public string Status { get; set; }
		public string last_updated_by { get; set; }
		public DateTime last_updated_on { get; set; }

		public LabelPrintJobDC()
        {
        }
		public LabelPrintJobDC(int JobID_, int LabelTypeId_, string Code_, string Description_, int CtnQty_, int StartNo_, int EndNo_, string BottleSize_, string Style_, string NeckSize_, string Colour_, string Material_, int NumReqd_, int JobRun_, string CompanyCode_, string Status_, string last_updated_by_, DateTime last_updated_on_)
		{
			this.JobID = JobID_;
			this.LabelTypeId = LabelTypeId_;
			this.Code = Code_;
			this.Description = Description_;
			this.CtnQty = CtnQty_;
			this.StartNo = StartNo_;
			this.EndNo = EndNo_;
			this.BottleSize = BottleSize_;
			this.Style = Style_;
			this.NeckSize = NeckSize_;
			this.Colour = Colour_;
			this.Material = Material_;
			this.NumReqd = NumReqd_;
			this.JobRun = JobRun_;
			this.CompanyCode = CompanyCode_;
			this.Status = Status_;
			this.last_updated_by = last_updated_by_;
			this.last_updated_on = last_updated_on_;
		}
	}
}
