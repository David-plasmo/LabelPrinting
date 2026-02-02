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

    public class ItemSitesDC
    {
        public string ITEMNMBR { get; set; }
        public string LOCNCODE { get; set; }
        public string BINNMBR { get; set; }
        public string PRIMVNDR { get; set; }
        public decimal QTYRQSTN { get; set; }
        public string Landed_Cost_Group_ID { get; set; }
        public int ID { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }

        public ItemSitesDC()
        {

        }

        public ItemSitesDC(string ITEMNMBR_, string LOCNCODE_, string BINNMBR_, string PRIMVNDR_, decimal QTYRQSTN_, string Landed_Cost_Group_ID_, int ID_, string TableName_, string DatabaseName_)
        {
            this.ITEMNMBR = ITEMNMBR_;
            this.LOCNCODE = LOCNCODE_;
            this.BINNMBR = BINNMBR_;
            this.PRIMVNDR = PRIMVNDR_;
            this.QTYRQSTN = QTYRQSTN_;
            this.Landed_Cost_Group_ID = Landed_Cost_Group_ID_;
            this.ID = ID_;
            this.TableName = TableName_;
            this.DatabaseName = DatabaseName_;
        }
    }


    class ItemSitesDAL : DataAccessBase
    {
        public DataSet GetGPItemSiteForEdit(string databaseName, int entryType, string itemNmbr)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetGPItemSiteForEdit]",
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

        //LookupSiteLocations(@CompDB varchar(10)=NULL)
        public DataSet LookupSiteLocations(string databaseName)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[LookupSiteLocations]",
                    CreateParameter("@CompDB", SqlDbType.VarChar, databaseName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        //LookupVendors
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

        //GetItemSite_Description
        public DataSet GetItemSite_Description()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetItemSite_Description]");
                    //CreateParameter("@GPDbase", SqlDbType.VarChar, databaseName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public void UpdateItemSite(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemSitesDC dc = DAL.CreateItemFromRow<ItemSitesDC>(dr);  //populate dataclass     
                    //dc.last_updated_on = DateTime.MinValue;
                    //dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    ItemSitesDAL.AddGP_Inventory_DataEntry_ItemSite(dc);
                    dr["ID"] = dc.ID;
                    //latestJobRun = (int)dr["JobRun"];
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemSitesDC dc = DAL.CreateItemFromRow<ItemSitesDC>(dr);  //populate JobRun dataclass                   
                    ItemSitesDAL.UpdateGP_Inventory_DataEntry_ItemSite(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    //ItemSitesDC dc = DAL.CreateItemFromRow<ItemSitesDC>(dr);
                    ItemSitesDC dc = new ItemSitesDC();
                    dc.ID = id;
                    ItemSitesDAL.DeleteGP_Inventory_DataEntry_ItemSites(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
    
        public static void AddGP_Inventory_DataEntry_ItemSite(ItemSitesDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddGP_Inventory_DataEntry_ItemSite", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@LOCNCODE", SqlDbType.Char, 11);
                cmd.Parameters["@LOCNCODE"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LOCNCODE"].Value = dc.LOCNCODE;
                cmd.Parameters.Add("@BINNMBR", SqlDbType.Char, 21);
                cmd.Parameters["@BINNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BINNMBR"].Value = dc.BINNMBR;
                cmd.Parameters.Add("@PRIMVNDR", SqlDbType.Char, 15);
                cmd.Parameters["@PRIMVNDR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PRIMVNDR"].Value = dc.PRIMVNDR;
                cmd.Parameters.Add("@QTYRQSTN", SqlDbType.Decimal, 9);
                cmd.Parameters["@QTYRQSTN"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QTYRQSTN"].Value = dc.QTYRQSTN;
                cmd.Parameters.Add("@Landed_Cost_Group_ID", SqlDbType.Char, 15);
                cmd.Parameters["@Landed_Cost_Group_ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Landed_Cost_Group_ID"].Value = dc.Landed_Cost_Group_ID;
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

        public static void UpdateGP_Inventory_DataEntry_ItemSite(ItemSitesDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateGP_Inventory_DataEntry_ItemSite", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@LOCNCODE", SqlDbType.Char, 11);
                cmd.Parameters["@LOCNCODE"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LOCNCODE"].Value = dc.LOCNCODE;
                cmd.Parameters.Add("@BINNMBR", SqlDbType.Char, 21);
                cmd.Parameters["@BINNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@BINNMBR"].Value = dc.BINNMBR;
                cmd.Parameters.Add("@PRIMVNDR", SqlDbType.Char, 15);
                cmd.Parameters["@PRIMVNDR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PRIMVNDR"].Value = dc.PRIMVNDR;
                cmd.Parameters.Add("@QTYRQSTN", SqlDbType.Decimal, 9);
                cmd.Parameters["@QTYRQSTN"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QTYRQSTN"].Value = dc.QTYRQSTN;
                cmd.Parameters.Add("@Landed_Cost_Group_ID", SqlDbType.Char, 15);
                cmd.Parameters["@Landed_Cost_Group_ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Landed_Cost_Group_ID"].Value = dc.Landed_Cost_Group_ID;
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

        public static void DeleteGP_Inventory_DataEntry_ItemSites(ItemSitesDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteGP_Inventory_DataEntry_ItemSite", connection);
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
