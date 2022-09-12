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
    class ItemCurrenciesDAL : DataAccessBase
    {
        public DataSet GetGPItemCurrencyForEdit(string databaseName, int entryType, string itemNmbr)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetGPItemCurrencyForEdit]",
                    CreateParameter("@GPDbase", SqlDbType.VarChar, databaseName),
                    CreateParameter("@EntryType", SqlDbType.Int, entryType),
                    CreateParameter("@ITEMNMBR", SqlDbType.VarChar, itemNmbr)
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

       
        public DataSet LookupCurrencies()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[LookupCurrencies]");
                //CreateParameter("@CompDB", SqlDbType.VarChar, databaseName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        /*LookupVendors
        public DataSet LookupVendors(string databaseName)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[LookupVendors]",
                    CreateParameter("@CompDB", SqlDbType.VarChar, databaseName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        */

        //GetItemCurrency_Description
        public DataSet GetItemCurrency_Description()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetItemCurrency_Description]");
                //CreateParameter("@GPDbase", SqlDbType.VarChar, databaseName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public void UpdateItemCurrency(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemCurrenciesDC dc = DAL.CreateItemFromRow<ItemCurrenciesDC>(dr);  //populate dataclass     
                    //dc.last_updated_on = DateTime.MinValue;
                    //dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    ItemCurrenciesDAL.AddGP_Inventory_DataEntry_ItemCurrency(dc);
                    dr["ID"] = dc.ID;
                    //latestJobRun = (int)dr["JobRun"];
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemCurrenciesDC dc = DAL.CreateItemFromRow<ItemCurrenciesDC>(dr);  //populate JobRun dataclass                   
                    ItemCurrenciesDAL.UpdateGP_Inventory_DataEntry_ItemCurrency(dc);
                                      
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    //ItemCurrenciesDC dc = DAL.CreateItemFromRow<ItemCurrenciesDC>(dr);
                    ItemCurrenciesDC dc = new ItemCurrenciesDC();
                    dc.ID = id;
                    ItemCurrenciesDAL.DeleteGP_Inventory_DataEntry_ItemCurrency(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        public static void AddGP_Inventory_DataEntry_ItemCurrency(ItemCurrenciesDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.AddGP_Inventory_DataEntry_ItemCurrency", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@CURNCYID", SqlDbType.Char, 15);
                cmd.Parameters["@CURNCYID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CURNCYID"].Value = dc.CURNCYID;
                cmd.Parameters.Add("@CURRNIDX", SqlDbType.SmallInt, 2);
                cmd.Parameters["@CURRNIDX"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CURRNIDX"].Value = dc.CURRNIDX;
                cmd.Parameters.Add("@DECPLCUR", SqlDbType.SmallInt, 2);
                cmd.Parameters["@DECPLCUR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DECPLCUR"].Value = dc.DECPLCUR;
                cmd.Parameters.Add("@LISTPRCE", SqlDbType.Decimal, 9);
                cmd.Parameters["@LISTPRCE"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LISTPRCE"].Value = dc.LISTPRCE;
                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;

                cmd.ExecuteNonQuery();

                dc.ID = (int)cmd.Parameters["@ID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void UpdateGP_Inventory_DataEntry_ItemCurrency(ItemCurrenciesDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.UpdateGP_Inventory_DataEntry_ItemCurrency", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@CURNCYID", SqlDbType.Char, 15);
                cmd.Parameters["@CURNCYID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CURNCYID"].Value = dc.CURNCYID;
                cmd.Parameters.Add("@CURRNIDX", SqlDbType.SmallInt, 2);
                cmd.Parameters["@CURRNIDX"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CURRNIDX"].Value = dc.CURRNIDX;
                cmd.Parameters.Add("@DECPLCUR", SqlDbType.SmallInt, 2);
                cmd.Parameters["@DECPLCUR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DECPLCUR"].Value = dc.DECPLCUR;
                cmd.Parameters.Add("@LISTPRCE", SqlDbType.Decimal, 9);
                cmd.Parameters["@LISTPRCE"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LISTPRCE"].Value = dc.LISTPRCE;
                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void DeleteGP_Inventory_DataEntry_ItemCurrency(ItemCurrenciesDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.DeleteGP_Inventory_DataEntry_ItemCurrency", connection);
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
