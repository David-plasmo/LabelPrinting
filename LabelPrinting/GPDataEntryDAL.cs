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

    class GPDataEntryDAL :DataAccessBase
    {
        public DataSet GetCompany(string dbEnv)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[GetCompany]",
                    CreateParameter("@DBEnv", SqlDbType.VarChar, dbEnv));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetItemIndex(string dbComp)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[GetItemIndex]",
                    CreateParameter("@DBComp", SqlDbType.VarChar, dbComp));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public DataSet GetCurrency()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[GetCurrency]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public DataSet LookupUSCATVLS_ByCompany(string compCode, string dbEnv)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[LookupUSCATVLS_ByCompany]",
                     CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode),
                     CreateParameter("@dbEnv", SqlDbType.VarChar, dbEnv));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet LookupUOMSCHDL_ByCompany(string compCode, string dbEnv)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[LookupUOMSCHDL_ByCompany]",
                     CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode),
                     CreateParameter("@dbEnv", SqlDbType.VarChar, dbEnv));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectGPDataEntry(string GPDatabase, int EntryType, string ItemCode, string TableName )
        {
            try
            {
                // return ExecuteDataSet("TestPlasmoIntegration.[dbo].[SelectGP_Inventory_DataEntry]");
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[GetGPInventoryForEdit]",
                    CreateParameter("@GPDbase", SqlDbType.VarChar, GPDatabase),
                    CreateParameter("@EntryType", SqlDbType.Int, EntryType),
                    CreateParameter("@ITEMNMBR", SqlDbType.VarChar, ItemCode),
                    CreateParameter("@TableName", SqlDbType.VarChar, TableName)); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        //[dbo].[GP_LookupsByTargetCol]
        public DataSet GPLookupsByTargetCol(string GPTargetColumn)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[GP_LookupsByTargetCol]",
                    CreateParameter("@GPTargetColumn", SqlDbType.VarChar, GPTargetColumn));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        //[dbo].[[LookupItemClassByCompany]]
        public DataSet LookupItemClassByCompany (string compCode)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[LookupItemClassByCompany]",
                    CreateParameter("@CompCode", SqlDbType.VarChar, compCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        //[dbo].[LookupUOMSCHDL_ByCompany]
        public DataSet LookupUOMSCHDLByCompany(string compCode)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[LookupUOMSCHDL_ByCompany]",
                    CreateParameter("@CompCode", SqlDbType.VarChar, compCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        //[dbo].[LookupUSCACTVAL_ByCompany]
        public DataSet LookupUSCACTVALByCompany(string compCode)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[LookupUSCACTVAL_ByCompany]",
                    CreateParameter("@CompCode", SqlDbType.VarChar, compCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectGP_PackagingType()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.[dbo].[SelectGP_PackagingType]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }            
        }
        public void UpdateGPDataEntry(DataSet ds)
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
                    GPDataEntryDC dc = DAL.CreateItemFromRow<GPDataEntryDC>(dr);  //populate dataclass     
                    //dc.last_updated_on = DateTime.MinValue;
                    //dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    GPDataEntryDAL.AddGPDataEntry(dc);
                    dr["ID"] = dc.ID;
                    //latestJobRun = (int)dr["JobRun"];
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    GPDataEntryDC dc = DAL.CreateItemFromRow<GPDataEntryDC>(dr);  //populate JobRun dataclass                   
                    GPDataEntryDAL.UpdateGPDataEntry(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    //GPDataEntryDC dc = DAL.CreateItemFromRow<GPDataEntryDC>(dr);
                    GPDataEntryDC dc = new GPDataEntryDC();
                    dc.ID = id;
                    GPDataEntryDAL.DeleteGPDataEntry(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        public static void UpdateGPDataEntry(GPDataEntryDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.UpdateGP_Inventory_DataEntry", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;
                cmd.Parameters.Add("@SchemaName", SqlDbType.VarChar, 100);
                cmd.Parameters["@SchemaName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@SchemaName"].Value = dc.SchemaName;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 100);
                cmd.Parameters["@ColumnName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ColumnName"].Value = dc.ColumnName;
                cmd.Parameters.Add("@defltValue", SqlDbType.VarChar, 50);
                cmd.Parameters["@defltValue"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@defltValue"].Value = dc.defltValue;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
                cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;
                cmd.Parameters.Add("@Reqd", SqlDbType.Bit, 1);
                cmd.Parameters["@Reqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Reqd"].Value = dc.Reqd;
                cmd.Parameters.Add("@seq", SqlDbType.Int, 4);
                cmd.Parameters["@seq"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@seq"].Value = dc.seq;
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 100);
                cmd.Parameters["@Notes"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Notes"].Value = dc.Notes;
                cmd.Parameters.Add("@ControlType", SqlDbType.VarChar, 50);
                cmd.Parameters["@ControlType"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ControlType"].Value = dc.ControlType;
                cmd.Parameters.Add("@LookupSP", SqlDbType.VarChar, 50);
                cmd.Parameters["@LookupSP"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LookupSP"].Value = dc.LookupSP;
                cmd.Parameters.Add("@LookupArgs", SqlDbType.VarChar, 100);
                cmd.Parameters["@LookupArgs"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LookupArgs"].Value = dc.LookupArgs;
                cmd.Parameters.Add("@inputValue", SqlDbType.VarChar, 100);
                cmd.Parameters["@inputValue"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@inputValue"].Value = dc.inputValue;
                cmd.Parameters.Add("@ReadOnly", SqlDbType.Bit, 1);
                cmd.Parameters["@ReadOnly"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ReadOnly"].Value = dc.ReadOnly;
                cmd.Parameters.Add("@displayValue", SqlDbType.VarChar, 100);
                cmd.Parameters["@displayValue"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@displayValue"].Value = dc.displayValue;
                cmd.Parameters.Add("@taStoredProcName", SqlDbType.VarChar, 100);
                cmd.Parameters["@taStoredProcName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@taStoredProcName"].Value = dc.taStoredProcName;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
        public static void AddGPDataEntry(GPDataEntryDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.AddGP_Inventory_DataEntry", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;
                cmd.Parameters.Add("@SchemaName", SqlDbType.VarChar, 100);
                cmd.Parameters["@SchemaName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@SchemaName"].Value = dc.SchemaName;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 100);
                cmd.Parameters["@ColumnName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ColumnName"].Value = dc.ColumnName;
                cmd.Parameters.Add("@defltValue", SqlDbType.VarChar, 50);
                cmd.Parameters["@defltValue"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@defltValue"].Value = dc.defltValue;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
                cmd.Parameters["@Description"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;
                cmd.Parameters.Add("@Reqd", SqlDbType.Bit, 1);
                cmd.Parameters["@Reqd"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Reqd"].Value = dc.Reqd;
                cmd.Parameters.Add("@seq", SqlDbType.Int, 4);
                cmd.Parameters["@seq"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@seq"].Value = dc.seq;
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 100);
                cmd.Parameters["@Notes"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Notes"].Value = dc.Notes;
                cmd.Parameters.Add("@ControlType", SqlDbType.VarChar, 50);
                cmd.Parameters["@ControlType"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ControlType"].Value = dc.ControlType;
                cmd.Parameters.Add("@LookupSP", SqlDbType.VarChar, 50);
                cmd.Parameters["@LookupSP"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LookupSP"].Value = dc.LookupSP;
                cmd.Parameters.Add("@LookupArgs", SqlDbType.VarChar, 100);
                cmd.Parameters["@LookupArgs"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LookupArgs"].Value = dc.LookupArgs;
                cmd.Parameters.Add("@inputValue", SqlDbType.VarChar, 100);
                cmd.Parameters["@inputValue"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@inputValue"].Value = dc.inputValue;
                cmd.Parameters.Add("@ReadOnly", SqlDbType.Bit, 1);
                cmd.Parameters["@ReadOnly"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ReadOnly"].Value = dc.ReadOnly;
                cmd.Parameters.Add("@displayValue", SqlDbType.VarChar, 100);
                cmd.Parameters["@displayValue"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@displayValue"].Value = dc.displayValue;
                cmd.Parameters.Add("@taStoredProcName", SqlDbType.VarChar, 100);
                cmd.Parameters["@taStoredProcName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@taStoredProcName"].Value = dc.taStoredProcName;

                cmd.ExecuteNonQuery();

                dc.ID = (int)cmd.Parameters["@ID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
        public static void DeleteGPDataEntry(GPDataEntryDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.DeleteGP_Inventory_DataEntry", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;

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
