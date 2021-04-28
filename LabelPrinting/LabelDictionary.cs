using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LabelPrinting
{
    public class LabelDictionary : Dictionary<string, LabelTypes>
    {
        public LabelDictionary()
        {
            //1	Plasmo BIN
            //2	Plasmo MFG-BM
            //3	Plasmo MFG-IM
            //4	Printing
            //5	Labelling
            //6	Assembly
            //7	PlasmoUncoded BM
            //8	PlasmoUncoded IM
            //9	PlasmoUncoded EX
            //10	CP BIN
            //11	CP MFG-IM
            //12	CP MFG-EX
            //13	CPUncoded IM
            //14	CPUncoded EX
            //15	Angel BIN
            //16	Angel MFG
            //17	AngelUncoded
            //18	Other P4
            //19	Other P5
            //20	Other P6
            //21	Other P7
            //22	General

            DataSet ds = new DataService.ProductDataService().GetLabelTypes();
            DataViewRowState dvrs = DataViewRowState.CurrentRows;
            DataRow[] rows = ds.Tables[0].Select("", "", dvrs);

            for (int i = 0; i < rows.Length; i++)
            {
                DataRow dr = rows[i];
                
                Add(dr["LabelType"].ToString(),
                    new LabelTypes
                    (dr["LabelType"].ToString(),
                    Convert.ToInt32(dr["LabelTypeID"].ToString()),
                    dr["LabelNo"].ToString(),
                    dr["DfltPrinter"].ToString(),
                    dr["Description"].ToString(),
                    dr["Company"].ToString(),
                    "", ""));       //Status, ErrMsg (updated by BarTender)         
            }
        }
    }
}

