using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class JobRunDC
	{
		public int JobRun { get; set; }
		public string CompanyCode { get; set; }
		public string Code { get; set; }
		public int NumReqd { get; set; }
		public string CUSTNMBR { get; set; }
		public DateTime? DateReqd { get; set; } = null;
		public int? MachineID { get; set; } = null;
		public int? Priority { get; set; } = null;
		public int? NumMade { get; set; } = null;
		public int? NumScanned { get; set; } = null;
		public int? DaysToComplete { get; set; } = null;
		public int? StatusID { get; set; } = null;
		public DateTime? DateCompleted { get; set; } = null;
		public string Comment { get; set; } = null;
		public DateTime last_updated_on { get; set; }
		public string last_updated_by { get; set; }

		public JobRunDC()
        {
        }
		public JobRunDC(int JobRun_, string CompanyCode_, string Code_, int NumReqd_, string CUSTNMBR_, DateTime DateReqd_, int MachineID_, int Priority_, int NumMade_, int NumScanned_, int DaysToComplete_, int StatusID_, DateTime DateCompleted_, string Comment_, DateTime last_updated_on_, string last_updated_by_)
		{
			this.JobRun = JobRun_;
			this.CompanyCode = CompanyCode_;
			this.Code = Code_;
			this.NumReqd = NumReqd_;
			this.CUSTNMBR = CUSTNMBR_;
			this.DateReqd = DateReqd_;
			this.MachineID = MachineID_;
			this.Priority = Priority_;
			this.NumMade = NumMade_;
			this.NumScanned = NumScanned_;
			this.DaysToComplete = DaysToComplete_;
			this.StatusID = StatusID_;
			this.DateCompleted = DateCompleted_;
			this.Comment = Comment_;
			this.last_updated_on = last_updated_on_;
			this.last_updated_by = last_updated_by_;
		}
	}
}
