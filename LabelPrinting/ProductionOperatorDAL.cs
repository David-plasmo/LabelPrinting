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
    /// <summary>
    /// Data Access Layer for Production Operators maintenance 
    /// </summary>
    class ProductionOperatorDAL : DataAccessBase
    {
        /// <summary>
        /// Retrieves Production Operators
        /// </summary>
        /// <returns>DataSet for editing</returns>        
        public DataSet SelectProductionOperator()
        {
            try
            {
                return ExecuteDataSet("PlasmoIntegration.dbo.SelectProductionOperator");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectOperatorClass()
        {
            try
            {
                return ExecuteDataSet("PlasmoIntegration.dbo.SelectOperatorClass");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// Performs insert, update and delete functions for Production Operators 
        /// </summary>
        /// <param name="ds">Production Operator DataSet for updating</param>        
        public void UpdateProductionOperator(DataSet ds)
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
                    ProductionOperator dc = DAL.CreateItemFromRow<ProductionOperator>(dr);  //populate dataclass     
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    ProductionOperatorDAL.UpdateProductionOperator(dc);
                    dr["OperatorID"] = dc.OperatorID;
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductionOperator dc = DAL.CreateItemFromRow<ProductionOperator>(dr);  //populate BMSetupData dataclass                   
                    ProductionOperatorDAL.UpdateProductionOperator(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductionOperator dc = DAL.CreateItemFromRow<ProductionOperator>(dr);  //populate BMSetupData dataclass                   
                    ProductionOperatorDAL.DeleteProductionOperator(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        private static void AddProductionOperator(ProductionOperator dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddProductionOperator", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@OperatorID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@OperatorID"].Value = dc.OperatorID;
                cmd.Parameters.Add("@OperatorCode", SqlDbType.NVarChar, 4);
                cmd.Parameters["@OperatorCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OperatorCode"].Value = dc.OperatorCode;
                cmd.Parameters.Add("@OperatorName", SqlDbType.NVarChar, 100);
                cmd.Parameters["@OperatorName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OperatorName"].Value = dc.OperatorName;
                cmd.Parameters.Add("@OperatorClassID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorClassID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OperatorClassID"].Value = dc.OperatorClassID;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.OperatorID = (int)cmd.Parameters["@OperatorID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private static void UpdateProductionOperator(ProductionOperator dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateProductionOperator", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@OperatorID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OperatorID"].Value = dc.OperatorID;
                cmd.Parameters.Add("@OperatorCode", SqlDbType.NVarChar, 4);
                cmd.Parameters["@OperatorCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OperatorCode"].Value = dc.OperatorCode;
                cmd.Parameters.Add("@OperatorName", SqlDbType.NVarChar, 100);
                cmd.Parameters["@OperatorName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OperatorName"].Value = dc.OperatorName;
                cmd.Parameters.Add("@OperatorClassID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorClassID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OperatorClassID"].Value = dc.OperatorClassID;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();

                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private static void DeleteProductionOperator(ProductionOperator dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteProductionOperator", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@OperatorID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@OperatorID"].Value = dc.OperatorID;

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
