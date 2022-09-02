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
    class ItemVendorsDAL : DataAccessBase
    {
        public DataSet GetGPItemVendorForEdit(string databaseName, int entryType, string itemNmbr)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetGPItemVendorForEdit]",
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
        public DataSet GetItemVendor_Description()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetItemVendor_Description]");
                //CreateParameter("@GPDbase", SqlDbType.VarChar, databaseName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public void UpdateItemVendor(DataSet ds)
        {
            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemVendorsDC dc = DAL.CreateItemFromRow<ItemVendorsDC>(dr);  //populate dataclass     
                    //dc.last_updated_on = DateTime.MinValue;
                    //dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    ItemVendorsDAL.AddGP_Inventory_DataEntry_ItemVendor(dc);
                    dr["ID"] = dc.ID;
                    //latestJobRun = (int)dr["JobRun"];
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemVendorsDC dc = DAL.CreateItemFromRow<ItemVendorsDC>(dr);  //populate JobRun dataclass                   
                    ItemVendorsDAL.UpdateGP_Inventory_DataEntry_ItemVendor(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    //ItemVendorsDC dc = DAL.CreateItemFromRow<ItemVendorsDC>(dr);
                    ItemVendorsDC dc = new ItemVendorsDC();
                    dc.ID = id;
                    ItemVendorsDAL.DeleteGP_Inventory_DataEntry_ItemVendor(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }

        public static void AddGP_Inventory_DataEntry_ItemVendor(ItemVendorsDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("AddGP_Inventory_DataEntry_ItemVendor", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@VENDORID", SqlDbType.Char, 15);
                cmd.Parameters["@VENDORID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@VENDORID"].Value = dc.VENDORID;
                cmd.Parameters.Add("@ITMVNDTY", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ITMVNDTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITMVNDTY"].Value = dc.ITMVNDTY;
                cmd.Parameters.Add("@VNDITNUM", SqlDbType.Char, 31);
                cmd.Parameters["@VNDITNUM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@VNDITNUM"].Value = dc.VNDITNUM;
                cmd.Parameters.Add("@QTYRQSTN", SqlDbType.Decimal, 9);
                cmd.Parameters["@QTYRQSTN"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QTYRQSTN"].Value = dc.QTYRQSTN;
                cmd.Parameters.Add("@AVRGLDTM", SqlDbType.Int, 4);
                cmd.Parameters["@AVRGLDTM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AVRGLDTM"].Value = dc.AVRGLDTM;
                cmd.Parameters.Add("@NORCTITM", SqlDbType.SmallInt, 2);
                cmd.Parameters["@NORCTITM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NORCTITM"].Value = dc.NORCTITM;
                cmd.Parameters.Add("@MINORQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@MINORQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MINORQTY"].Value = dc.MINORQTY;
                cmd.Parameters.Add("@MAXORDQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@MAXORDQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MAXORDQTY"].Value = dc.MAXORDQTY;
                cmd.Parameters.Add("@ECORDQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@ECORDQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ECORDQTY"].Value = dc.ECORDQTY;
                cmd.Parameters.Add("@VNDITDSC", SqlDbType.Char, 101);
                cmd.Parameters["@VNDITDSC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@VNDITDSC"].Value = dc.VNDITDSC;
                cmd.Parameters.Add("@Last_Originating_Cost", SqlDbType.Decimal, 9);
                cmd.Parameters["@Last_Originating_Cost"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Last_Originating_Cost"].Value = dc.Last_Originating_Cost;
                cmd.Parameters.Add("@Last_Currency_ID", SqlDbType.Char, 15);
                cmd.Parameters["@Last_Currency_ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Last_Currency_ID"].Value = dc.Last_Currency_ID;
                cmd.Parameters.Add("@FREEONBOARD", SqlDbType.SmallInt, 2);
                cmd.Parameters["@FREEONBOARD"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FREEONBOARD"].Value = dc.FREEONBOARD;
                cmd.Parameters.Add("@PRCHSUOM", SqlDbType.Char, 9);
                cmd.Parameters["@PRCHSUOM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PRCHSUOM"].Value = dc.PRCHSUOM;
                cmd.Parameters.Add("@CURRNIDX", SqlDbType.SmallInt, 2);
                cmd.Parameters["@CURRNIDX"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CURRNIDX"].Value = dc.CURRNIDX;
                cmd.Parameters.Add("@PLANNINGLEADTIME", SqlDbType.SmallInt, 2);
                cmd.Parameters["@PLANNINGLEADTIME"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PLANNINGLEADTIME"].Value = dc.PLANNINGLEADTIME;
                cmd.Parameters.Add("@ORDERMULTIPLE", SqlDbType.Decimal, 9);
                cmd.Parameters["@ORDERMULTIPLE"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ORDERMULTIPLE"].Value = dc.ORDERMULTIPLE;
                cmd.Parameters.Add("@MNFCTRITMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@MNFCTRITMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MNFCTRITMNMBR"].Value = dc.MNFCTRITMNMBR;
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

        public static void UpdateGP_Inventory_DataEntry_ItemVendor(ItemVendorsDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("UpdateGP_Inventory_DataEntry_ItemVendor", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@VENDORID", SqlDbType.Char, 15);
                cmd.Parameters["@VENDORID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@VENDORID"].Value = dc.VENDORID;
                cmd.Parameters.Add("@ITMVNDTY", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ITMVNDTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ITMVNDTY"].Value = dc.ITMVNDTY;
                cmd.Parameters.Add("@VNDITNUM", SqlDbType.Char, 31);
                cmd.Parameters["@VNDITNUM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@VNDITNUM"].Value = dc.VNDITNUM;
                cmd.Parameters.Add("@QTYRQSTN", SqlDbType.Decimal, 9);
                cmd.Parameters["@QTYRQSTN"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@QTYRQSTN"].Value = dc.QTYRQSTN;
                cmd.Parameters.Add("@AVRGLDTM", SqlDbType.Int, 4);
                cmd.Parameters["@AVRGLDTM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AVRGLDTM"].Value = dc.AVRGLDTM;
                cmd.Parameters.Add("@NORCTITM", SqlDbType.SmallInt, 2);
                cmd.Parameters["@NORCTITM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@NORCTITM"].Value = dc.NORCTITM;
                cmd.Parameters.Add("@MINORQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@MINORQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MINORQTY"].Value = dc.MINORQTY;
                cmd.Parameters.Add("@MAXORDQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@MAXORDQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MAXORDQTY"].Value = dc.MAXORDQTY;
                cmd.Parameters.Add("@ECORDQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@ECORDQTY"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ECORDQTY"].Value = dc.ECORDQTY;
                cmd.Parameters.Add("@VNDITDSC", SqlDbType.Char, 101);
                cmd.Parameters["@VNDITDSC"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@VNDITDSC"].Value = dc.VNDITDSC;
                cmd.Parameters.Add("@Last_Originating_Cost", SqlDbType.Decimal, 9);
                cmd.Parameters["@Last_Originating_Cost"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Last_Originating_Cost"].Value = dc.Last_Originating_Cost;
                cmd.Parameters.Add("@Last_Currency_ID", SqlDbType.Char, 15);
                cmd.Parameters["@Last_Currency_ID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@Last_Currency_ID"].Value = dc.Last_Currency_ID;
                cmd.Parameters.Add("@FREEONBOARD", SqlDbType.SmallInt, 2);
                cmd.Parameters["@FREEONBOARD"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FREEONBOARD"].Value = dc.FREEONBOARD;
                cmd.Parameters.Add("@PRCHSUOM", SqlDbType.Char, 9);
                cmd.Parameters["@PRCHSUOM"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PRCHSUOM"].Value = dc.PRCHSUOM;
                cmd.Parameters.Add("@CURRNIDX", SqlDbType.SmallInt, 2);
                cmd.Parameters["@CURRNIDX"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CURRNIDX"].Value = dc.CURRNIDX;
                cmd.Parameters.Add("@PLANNINGLEADTIME", SqlDbType.SmallInt, 2);
                cmd.Parameters["@PLANNINGLEADTIME"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PLANNINGLEADTIME"].Value = dc.PLANNINGLEADTIME;
                cmd.Parameters.Add("@ORDERMULTIPLE", SqlDbType.Decimal, 9);
                cmd.Parameters["@ORDERMULTIPLE"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ORDERMULTIPLE"].Value = dc.ORDERMULTIPLE;
                cmd.Parameters.Add("@MNFCTRITMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@MNFCTRITMNMBR"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@MNFCTRITMNMBR"].Value = dc.MNFCTRITMNMBR;
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

        public static void DeleteGP_Inventory_DataEntry_ItemVendor(ItemVendorsDC dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("DeleteGP_Inventory_DataEntry_ItemVendor", connection);
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
