using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class ProductionOperator
	{
		public int OperatorID { get; set; }
		public string OperatorCode { get; set; }
		public string OperatorName { get; set; }
		public int OperatorClassID { get; set; }
		public string last_updated_by { get; set; }
		public DateTime last_updated_on { get; set; }

		public ProductionOperator()
		{ }
		public ProductionOperator(int OperatorID_, string OperatorCode_, string OperatorName_, int OperatorClassID_, string last_updated_by_, DateTime last_updated_on_)
		{
			this.OperatorID = OperatorID_;
			this.OperatorCode = OperatorCode_;
			this.OperatorName = OperatorName_;
			this.OperatorClassID = OperatorClassID_;
			this.last_updated_by = last_updated_by_;
			this.last_updated_on = last_updated_on_;
		}
	}
}
