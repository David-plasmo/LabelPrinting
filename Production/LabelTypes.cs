using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production
{
    public class LabelTypes
    {        
        public int LabelTypeID { get; set; }
        public string LabelType { get; set; }
        public string LabelNo { get; set; }
        public string DfltPrinter { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }
        public string ErrMsg { get; set; }



        public LabelTypes(string labelType, int labelTypeID, string labelNo, 
            string dfltPrinter, string description, string company, string status, string errMsg)
        {
            try
            {
                LabelType = labelType;
                LabelTypeID = labelTypeID;
                LabelNo = labelNo;
                DfltPrinter = dfltPrinter;
                Description = description;
                Company = company;
                Status = status;
                ErrMsg = errMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
 }
