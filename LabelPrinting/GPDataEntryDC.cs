using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
	public class GPDataEntryDC
	{
		public string DatabaseName { get; set; }
		public string SchemaName { get; set; }
		public string TableName { get; set; }
		public string ColumnName { get; set; }
		public string ColumnDataType { get; set; }
		public string MaxValue { get; set; }
		public string MinValue { get; set; }
		public string defltValue { get; set; }
		public string Description { get; set; }
		public bool Reqd { get; set; }
		public int seq { get; set; }
		public string Notes { get; set; }
		public string ControlType { get; set; }
		public string LookupSP { get; set; }
		public string LookupArgs { get; set; }
		public string inputValue { get; set; }
		public int ID { get; set; }
		public string taColumnName { get; set; }
		public string taDataType { get; set; }
		public int taLength { get; set; }
		public bool ReadOnly { get; set; }
		public string displayValue { get; set; }
		public string taStoredProcName { get; set; }

		public GPDataEntryDC()
        {

        }
		public GPDataEntryDC(string DatabaseName_, string SchemaName_, string TableName_, string ColumnName_, string ColumnDataType_, string MaxValue_, string MinValue_, string defltValue_, string Description_, bool Reqd_, int seq_, string Notes_, string ControlType_, string LookupSP_, string LookupArgs_, string inputValue_, int ID_, string taColumnName_, string taDataType_, int taLength_, bool ReadOnly_, string displayValue_, string taStoredProcName_)
		{
			this.DatabaseName = DatabaseName_;
			this.SchemaName = SchemaName_;
			this.TableName = TableName_;
			this.ColumnName = ColumnName_;
			this.ColumnDataType = ColumnDataType_;
			this.MaxValue = MaxValue_;
			this.MinValue = MinValue_;
			this.defltValue = defltValue_;
			this.Description = Description_;
			this.Reqd = Reqd_;
			this.seq = seq_;
			this.Notes = Notes_;
			this.ControlType = ControlType_;
			this.LookupSP = LookupSP_;
			this.LookupArgs = LookupArgs_;
			this.inputValue = inputValue_;
			this.ID = ID_;
			this.taColumnName = taColumnName_;
			this.taDataType = taDataType_;
			this.taLength = taLength_;
			this.ReadOnly = ReadOnly_;
			this.displayValue = displayValue_;
			this.taStoredProcName = taStoredProcName_;
		}
	}
}
