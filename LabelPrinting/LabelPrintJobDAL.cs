using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataService;
using System.Data;
using System.Windows.Forms;

namespace LabelPrinting
{
    public class LabelPrintJobDAL
    {
        public static void AddPrintJob(ref LabelPrintJobDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.AddPrintJob", connection);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.Parameters.Add("@LabelTypeId", SqlDbType.Int, 4);
                cmd.Parameters["@LabelTypeId"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelTypeId"].Value = dc.LabelTypeId;
                cmd.Parameters.Add("@Code", SqlDbType.Char, 31);
                cmd.Parameters["@Code"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Code"].Value = dc.Code;
                cmd.Parameters.Add("@Description", SqlDbType.Char, 101);
                cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;
                cmd.Parameters.Add("@NumReqd", SqlDbType.Int, 4);
                cmd.Parameters["@NumReqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NumReqd"].Value = dc.NumReqd;
                cmd.Parameters.Add("@StartNo", SqlDbType.Int, 4);
                cmd.Parameters["@StartNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@StartNo"].Value = dc.StartNo;
                cmd.Parameters.Add("@EndNo", SqlDbType.Int, 4);
                cmd.Parameters["@EndNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@EndNo"].Value = dc.EndNo;
                cmd.Parameters.Add("@CompanyCode", SqlDbType.Char, 2);
                cmd.Parameters["@CompanyCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CompanyCode"].Value = dc.CompanyCode;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50);
                cmd.Parameters["@Status"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Status"].Value = dc.Status;
                cmd.Parameters.Add("@CtnQty", SqlDbType.Int, 4);
                cmd.Parameters["@CtnQty"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CtnQty"].Value = dc.CtnQty;
                cmd.Parameters.Add("@BottleSize", SqlDbType.Char, 11);
                cmd.Parameters["@BottleSize"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BottleSize"].Value = dc.BottleSize;
                cmd.Parameters.Add("@Style", SqlDbType.Char, 11);
                cmd.Parameters["@Style"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Style"].Value = dc.Style;
                cmd.Parameters.Add("@NeckSize", SqlDbType.Char, 11);
                cmd.Parameters["@NeckSize"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NeckSize"].Value = dc.NeckSize;
                cmd.Parameters.Add("@Colour", SqlDbType.Char, 11);
                cmd.Parameters["@Colour"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Colour"].Value = dc.Colour;
                cmd.Parameters.Add("@Material", SqlDbType.VarChar, 20);
                cmd.Parameters["@Material"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Material"].Value = dc.Material;
                cmd.Parameters.Add("@JobRun", SqlDbType.Int, 4);
                cmd.Parameters["@JobRun"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@JobRun"].Value = dc.JobRun;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@JobID", SqlDbType.Int, 4);
                cmd.Parameters["@JobID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@JobID"].Value = dc.JobID;

                cmd.ExecuteNonQuery();

                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.JobID = (int)cmd.Parameters["@JobID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
    }
}
