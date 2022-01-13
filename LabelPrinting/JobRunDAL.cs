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
    class JobRunDAL : DataAccessBase
    {
        public DataSet SelectJobRun()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.SelectJobRun");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetCompany()
        {
            try
            {
                return ExecuteDataSet("PlasmoIntegration.dbo.GetCompany");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetJobRunStatus()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.GetJobRunStatus");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetMachine()
        {
            try
            {
                string @machineType = null;
                return ExecuteDataSet("PlasmoIntegration.dbo.GetMachine",
                    CreateParameter("@MachineType", SqlDbType.VarChar, machineType));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectProductMaterialAndGrade(string compCode)
        {
            try
            {
                DataSet ds = ExecuteDataSet("[PlasmoIntegration].[dbo].[SelectProductMaterialAndGrade]",
                     CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        //GetCustomerIndexByCompany
        public DataSet GetCustomerIndexByCompany(string compCode)
        {
            try
            {
                DataSet ds = ExecuteDataSet("[PlasmoIntegration].[dbo].[GetCustomerIndexByCompany]",
                     CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataSet GetProductLabelInfo(string compCode, string prodCode)
        {
            try
            {
                DataSet ds = ExecuteDataSet("[TestPlasmoIntegration].[dbo].[GetProductLabelInfo]",
                     CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode),
                     CreateParameter("@ProductCode", SqlDbType.VarChar, prodCode));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdateJobRun(DataSet ds, ref int latestJobRun )
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                //MachineDAL md = new MachineDAL();


                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    JobRunDC dc = DAL.CreateItemFromRow<JobRunDC>(dr);  //populate dataclass     
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    JobRunDAL.AddJobRun(dc);
                    dr["JobRun"] = dc.JobRun;
                    latestJobRun = (int)dr["JobRun"];
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    JobRunDC dc = DAL.CreateItemFromRow<JobRunDC>(dr);  //populate JobRun dataclass                   
                    JobRunDAL.UpdateJobRun(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);                
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];                    
                    int jobRun = (int)dr["JobRun", DataRowVersion.Original];
                    //JobRunDC dc = DAL.CreateItemFromRow<JobRunDC>(dr);
                    JobRunDC dc = new JobRunDC();
                    dc.JobRun = jobRun;                                                    
                    JobRunDAL.DeleteJobRun(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        public static void AddJobRun(JobRunDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.AddJobRun", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@JobRun", SqlDbType.Int, 4);
                cmd.Parameters["@JobRun"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@JobRun"].Value = dc.JobRun;
                cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar, 2);
                cmd.Parameters["@CompanyCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CompanyCode"].Value = dc.CompanyCode;
                cmd.Parameters.Add("@Code", SqlDbType.Char, 31);
                cmd.Parameters["@Code"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Code"].Value = dc.Code;
                cmd.Parameters.Add("@NumReqd", SqlDbType.Int, 4);
                cmd.Parameters["@NumReqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NumReqd"].Value = dc.NumReqd;
                cmd.Parameters.Add("@CUSTNMBR", SqlDbType.Char, 15);
                cmd.Parameters["@CUSTNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CUSTNMBR"].Value = dc.CUSTNMBR;
                cmd.Parameters.Add("@DateReqd", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateReqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DateReqd"].Value = dc.DateReqd;
                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;
                cmd.Parameters.Add("@Priority", SqlDbType.Int, 4);
                cmd.Parameters["@Priority"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Priority"].Value = dc.Priority;
                cmd.Parameters.Add("@NumMade", SqlDbType.Int, 4);
                cmd.Parameters["@NumMade"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NumMade"].Value = dc.NumMade;
                cmd.Parameters.Add("@NumScanned", SqlDbType.Int, 4);
                cmd.Parameters["@NumScanned"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NumScanned"].Value = dc.NumScanned;
                cmd.Parameters.Add("@DaysToComplete", SqlDbType.Int, 4);
                cmd.Parameters["@DaysToComplete"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DaysToComplete"].Value = dc.DaysToComplete;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int, 4);
                cmd.Parameters["@StatusID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@StatusID"].Value = dc.StatusID;
                cmd.Parameters.Add("@DateCompleted", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateCompleted"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DateCompleted"].Value = dc.DateCompleted;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                cmd.ExecuteNonQuery();

                dc.JobRun = (int)cmd.Parameters["@JobRun"].Value;
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateJobRun(JobRunDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.UpdateJobRun", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@JobRun", SqlDbType.Int, 4);
                cmd.Parameters["@JobRun"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@JobRun"].Value = dc.JobRun;
                cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar, 2);
                cmd.Parameters["@CompanyCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CompanyCode"].Value = dc.CompanyCode;
                cmd.Parameters.Add("@Code", SqlDbType.Char, 31);
                cmd.Parameters["@Code"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Code"].Value = dc.Code;
                cmd.Parameters.Add("@NumReqd", SqlDbType.Int, 4);
                cmd.Parameters["@NumReqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NumReqd"].Value = dc.NumReqd;
                cmd.Parameters.Add("@CUSTNMBR", SqlDbType.Char, 15);
                cmd.Parameters["@CUSTNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CUSTNMBR"].Value = dc.CUSTNMBR;
                cmd.Parameters.Add("@DateReqd", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateReqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DateReqd"].Value = dc.DateReqd;
                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;
                cmd.Parameters.Add("@Priority", SqlDbType.Int, 4);
                cmd.Parameters["@Priority"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Priority"].Value = dc.Priority;
                cmd.Parameters.Add("@NumMade", SqlDbType.Int, 4);
                cmd.Parameters["@NumMade"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NumMade"].Value = dc.NumMade;
                cmd.Parameters.Add("@NumScanned", SqlDbType.Int, 4);
                cmd.Parameters["@NumScanned"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NumScanned"].Value = dc.NumScanned;
                cmd.Parameters.Add("@DaysToComplete", SqlDbType.Int, 4);
                cmd.Parameters["@DaysToComplete"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DaysToComplete"].Value = dc.DaysToComplete;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int, 4);
                cmd.Parameters["@StatusID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@StatusID"].Value = dc.StatusID;
                cmd.Parameters.Add("@DateCompleted", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateCompleted"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DateCompleted"].Value = dc.DateCompleted;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;

                cmd.ExecuteNonQuery();

                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
        public static void DeleteJobRun(JobRunDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.DeleteJobRun", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@JobRun", SqlDbType.Int, 4);
                cmd.Parameters["@JobRun"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@JobRun"].Value = dc.JobRun;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
    }
}
