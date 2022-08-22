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
    class PriceListDAL : DataAccessBase
    {
        public DataSet GetPriceListDescription()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetPriceList_Description]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public DataSet GetGPPriceListForEdit(string databaseName, int entryType, string itemNmbr)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetGPPriceListForEdit]",
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

        public DataSet SelectPriceListQty(string itemNmbr, string curncyID, string prcLevel, string uofm)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[SelectPriceListQty]",
                    CreateParameter("@ITEMNMBR", SqlDbType.VarChar, itemNmbr),
                    CreateParameter("@CURNCYID", SqlDbType.VarChar, curncyID),
                    CreateParameter("@PRCLEVEL", SqlDbType.VarChar, prcLevel),
                    CreateParameter("@UOFM", SqlDbType.VarChar, uofm)
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }


        public DataSet SelectPriceLevel(string databaseName )
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[SelectPriceLevel]",
                    CreateParameter("@DatabaseName", SqlDbType.VarChar, databaseName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public void UpdatePriceList(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    PriceListDC dc = DAL.CreateItemFromRow<PriceListDC>(dr);  //populate dataclass     
                    //dc.last_updated_on = DateTime.MinValue;
                    //dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    PriceListDAL.AddGP_Inventory_PriceList(dc);
                    dr["ID"] = dc.ID;
                    //latestJobRun = (int)dr["JobRun"];
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    PriceListDC dc = DAL.CreateItemFromRow<PriceListDC>(dr);  //populate JobRun dataclass                   
                    PriceListDAL.UpdateGP_Inventory_PriceList(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    //PriceListDC dc = DAL.CreateItemFromRow<PriceListDC>(dr);
                    PriceListDC dc = new PriceListDC();
                    dc.ID = id;
                    PriceListDAL.DeleteGP_Inventory_PriceList(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        
        public static void AddGP_Inventory_PriceList(PriceListDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.AddGP_Inventory_PriceList", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@CURNCYID", SqlDbType.Char, 15);
                cmd.Parameters["@CURNCYID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CURNCYID"].Value = dc.CURNCYID;
                cmd.Parameters.Add("@PRCLEVEL", SqlDbType.Char, 11);
                cmd.Parameters["@PRCLEVEL"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PRCLEVEL"].Value = dc.PRCLEVEL;
                cmd.Parameters.Add("@UOFM", SqlDbType.Char, 9);
                cmd.Parameters["@UOFM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@UOFM"].Value = dc.UOFM;
                cmd.Parameters.Add("@TOQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@TOQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TOQTY"].Value = dc.TOQTY;
                cmd.Parameters.Add("@UOMPRICE", SqlDbType.Decimal, 9);
                cmd.Parameters["@UOMPRICE"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@UOMPRICE"].Value = dc.UOMPRICE;
                cmd.Parameters.Add("@RNDGAMNT", SqlDbType.Decimal, 9);
                cmd.Parameters["@RNDGAMNT"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@RNDGAMNT"].Value = dc.RNDGAMNT;
                cmd.Parameters.Add("@ROUNDHOW", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ROUNDHOW"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ROUNDHOW"].Value = dc.ROUNDHOW;
                cmd.Parameters.Add("@ROUNDTO", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ROUNDTO"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ROUNDTO"].Value = dc.ROUNDTO;
                cmd.Parameters.Add("@FROMQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@FROMQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FROMQTY"].Value = dc.FROMQTY;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;
                cmd.Parameters.Add("@UMSLSOPT", SqlDbType.SmallInt, 2);
                cmd.Parameters["@UMSLSOPT"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@UMSLSOPT"].Value = dc.UMSLSOPT;

                cmd.ExecuteNonQuery();

                dc.ID = (int)cmd.Parameters["@ID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }
                           
        public static void UpdateGP_Inventory_PriceList(PriceListDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.UpdateGP_Inventory_PriceList", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@CURNCYID", SqlDbType.Char, 15);
                cmd.Parameters["@CURNCYID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CURNCYID"].Value = dc.CURNCYID;
                cmd.Parameters.Add("@PRCLEVEL", SqlDbType.Char, 11);
                cmd.Parameters["@PRCLEVEL"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PRCLEVEL"].Value = dc.PRCLEVEL;
                cmd.Parameters.Add("@UOFM", SqlDbType.Char, 9);
                cmd.Parameters["@UOFM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@UOFM"].Value = dc.UOFM;
                cmd.Parameters.Add("@TOQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@TOQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TOQTY"].Value = dc.TOQTY;
                cmd.Parameters.Add("@UOMPRICE", SqlDbType.Decimal, 9);
                cmd.Parameters["@UOMPRICE"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@UOMPRICE"].Value = dc.UOMPRICE;
                cmd.Parameters.Add("@RNDGAMNT", SqlDbType.Decimal, 9);
                cmd.Parameters["@RNDGAMNT"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@RNDGAMNT"].Value = dc.RNDGAMNT;
                cmd.Parameters.Add("@ROUNDHOW", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ROUNDHOW"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ROUNDHOW"].Value = dc.ROUNDHOW;
                cmd.Parameters.Add("@ROUNDTO", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ROUNDTO"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ROUNDTO"].Value = dc.ROUNDTO;
                cmd.Parameters.Add("@FROMQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@FROMQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FROMQTY"].Value = dc.FROMQTY;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;
                cmd.Parameters.Add("@UMSLSOPT", SqlDbType.SmallInt, 2);
                cmd.Parameters["@UMSLSOPT"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@UMSLSOPT"].Value = dc.UMSLSOPT;

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        public static void DeleteGP_Inventory_PriceList(PriceListDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("TestPlasmoIntegration.dbo.DeleteGP_Inventory_PriceList", connection);
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
