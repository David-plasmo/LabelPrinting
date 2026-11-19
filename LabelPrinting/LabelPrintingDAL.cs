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
    #region Data Classes

    // GP Data Entry Data Class
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

        public GPDataEntryDC() { }

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

    // Item Currencies Data Class
    public class ItemCurrenciesDC
    {
        public string ITEMNMBR { get; set; }
        public string CURNCYID { get; set; }
        public short CURRNIDX { get; set; }
        public short DECPLCUR { get; set; }
        public decimal LISTPRCE { get; set; }
        public int ID { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }

        public ItemCurrenciesDC() { }

        public ItemCurrenciesDC(string ITEMNMBR_, string CURNCYID_, short CURRNIDX_, short DECPLCUR_, decimal LISTPRCE_, int ID_, string TableName_, string DatabaseName_)
        {
            this.ITEMNMBR = ITEMNMBR_;
            this.CURNCYID = CURNCYID_;
            this.CURRNIDX = CURRNIDX_;
            this.DECPLCUR = DECPLCUR_;
            this.LISTPRCE = LISTPRCE_;
            this.ID = ID_;
            this.TableName = TableName_;
            this.DatabaseName = DatabaseName_;
        }
    }

    // Item Sites Data Class
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

        public ItemSitesDC() { }

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

    // Item Vendors Data Class
    public class ItemVendorsDC
    {
        public string ITEMNMBR { get; set; }
        public string VENDORID { get; set; }
        public short ITMVNDTY { get; set; }
        public string VNDITNUM { get; set; }
        public decimal QTYRQSTN { get; set; }
        public int AVRGLDTM { get; set; }
        public short NORCTITM { get; set; }
        public decimal MINORQTY { get; set; }
        public decimal MAXORDQTY { get; set; }
        public decimal ECORDQTY { get; set; }
        public string VNDITDSC { get; set; }
        public decimal Last_Originating_Cost { get; set; }
        public string Last_Currency_ID { get; set; }
        public short FREEONBOARD { get; set; }
        public string PRCHSUOM { get; set; }
        public short CURRNIDX { get; set; }
        public short PLANNINGLEADTIME { get; set; }
        public decimal ORDERMULTIPLE { get; set; }
        public string MNFCTRITMNMBR { get; set; }
        public int ID { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }

        public ItemVendorsDC() { }

        public ItemVendorsDC(string ITEMNMBR_, string VENDORID_, short ITMVNDTY_, string VNDITNUM_, decimal QTYRQSTN_, int AVRGLDTM_, short NORCTITM_, decimal MINORQTY_, decimal MAXORDQTY_, decimal ECORDQTY_, string VNDITDSC_, decimal Last_Originating_Cost_, string Last_Currency_ID_, short FREEONBOARD_, string PRCHSUOM_, short CURRNIDX_, short PLANNINGLEADTIME_, decimal ORDERMULTIPLE_, string MNFCTRITMNMBR_, int ID_, string TableName_, string DatabaseName_)
        {
            this.ITEMNMBR = ITEMNMBR_;
            this.VENDORID = VENDORID_;
            this.ITMVNDTY = ITMVNDTY_;
            this.VNDITNUM = VNDITNUM_;
            this.QTYRQSTN = QTYRQSTN_;
            this.AVRGLDTM = AVRGLDTM_;
            this.NORCTITM = NORCTITM_;
            this.MINORQTY = MINORQTY_;
            this.MAXORDQTY = MAXORDQTY_;
            this.ECORDQTY = ECORDQTY_;
            this.VNDITDSC = VNDITDSC_;
            this.Last_Originating_Cost = Last_Originating_Cost_;
            this.Last_Currency_ID = Last_Currency_ID_;
            this.FREEONBOARD = FREEONBOARD_;
            this.PRCHSUOM = PRCHSUOM_;
            this.CURRNIDX = CURRNIDX_;
            this.PLANNINGLEADTIME = PLANNINGLEADTIME_;
            this.ORDERMULTIPLE = ORDERMULTIPLE_;
            this.MNFCTRITMNMBR = MNFCTRITMNMBR_;
            this.ID = ID_;
            this.TableName = TableName_;
            this.DatabaseName = DatabaseName_;
        }
    }

    // Job Run Data Class
    public class JobRunDC
    {
        public int JobRun { get; set; }
        public string CompanyCode { get; set; }
        public string Code { get; set; }
        public int NumReqd { get; set; }
        public string CUSTNMBR { get; set; }
        public DateTime? DateReqd { get; set; } = null;
        public int? MachineID { get; set; } = null;
        public int? Priority { get; set; } = null;
        public int? NumMade { get; set; } = null;
        public int? NumScanned { get; set; } = null;
        public int? DaysToComplete { get; set; } = null;
        public int? StatusID { get; set; } = null;
        public DateTime? DateCompleted { get; set; } = null;
        public string Comment { get; set; } = null;
        public DateTime last_updated_on { get; set; }
        public string last_updated_by { get; set; }

        public JobRunDC() { }

        public JobRunDC(int JobRun_, string CompanyCode_, string Code_, int NumReqd_, string CUSTNMBR_, DateTime DateReqd_, int MachineID_, int Priority_, int NumMade_, int NumScanned_, int DaysToComplete_, int StatusID_, DateTime DateCompleted_, string Comment_, DateTime last_updated_on_, string last_updated_by_)
        {
            this.JobRun = JobRun_;
            this.CompanyCode = CompanyCode_;
            this.Code = Code_;
            this.NumReqd = NumReqd_;
            this.CUSTNMBR = CUSTNMBR_;
            this.DateReqd = DateReqd_;
            this.MachineID = MachineID_;
            this.Priority = Priority_;
            this.NumMade = NumMade_;
            this.NumScanned = NumScanned_;
            this.DaysToComplete = DaysToComplete_;
            this.StatusID = StatusID_;
            this.DateCompleted = DateCompleted_;
            this.Comment = Comment_;
            this.last_updated_on = last_updated_on_;
            this.last_updated_by = last_updated_by_;
        }
    }

    // Label Print Job Data Class
    public class LabelPrintJobDC
    {
        public int JobID { get; set; }
        public int LabelTypeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int CtnQty { get; set; }
        public int StartNo { get; set; }
        public int EndNo { get; set; }
        public string BottleSize { get; set; } = "";
        public string Style { get; set; } = "";
        public string NeckSize { get; set; } = "";
        public string Colour { get; set; } = "";
        public string Material { get; set; } = "";
        public int NumReqd { get; set; }
        public int JobRun { get; set; }
        public string CompanyCode { get; set; }
        public string Status { get; set; } = "";
        public string last_updated_by { get; set; } = "";
        public DateTime? last_updated_on { get; set; } = DateTime.MinValue;

        public LabelPrintJobDC() { }

        public LabelPrintJobDC(int JobID_, int LabelTypeId_, string Code_, string Description_, int CtnQty_, int StartNo_, int EndNo_, string BottleSize_, string Style_, string NeckSize_, string Colour_, string Material_, int NumReqd_, int JobRun_, string CompanyCode_, string Status_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.JobID = JobID_;
            this.LabelTypeId = LabelTypeId_;
            this.Code = Code_;
            this.Description = Description_;
            this.CtnQty = CtnQty_;
            this.StartNo = StartNo_;
            this.EndNo = EndNo_;
            this.BottleSize = BottleSize_;
            this.Style = Style_;
            this.NeckSize = NeckSize_;
            this.Colour = Colour_;
            this.Material = Material_;
            this.NumReqd = NumReqd_;
            this.JobRun = JobRun_;
            this.CompanyCode = CompanyCode_;
            this.Status = Status_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }
    }

    // Production Operator Data Class
    public class ProductionOperator
    {
        public int OperatorID { get; set; }
        public string OperatorCode { get; set; }
        public string OperatorName { get; set; }
        public int OperatorClassID { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public ProductionOperator() { }

        public ProductionOperator(int OperatorID_, string OperatorCode_, string OperatorName_, int OperatorClassID_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.OperatorID = OperatorID_;
            this.OperatorCode = OperatorCode_;
            this.OperatorName = OperatorName_;
            this.OperatorClassID = OperatorClassID_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }
    }

    // Product Item Class Label Link Data Class
    public class ProductItemClassLabelLink
    {
        public int PiclID { get; set; }
        public int TypeID { get; set; }
        public string ItemClass { get; set; }
        public string ItemClassDesc { get; set; }
        public string LabelNo { get; set; }
        public int LabelTypeID { get; set; }
        public string CompanyCode { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

        public ProductItemClassLabelLink() { }

        public ProductItemClassLabelLink(int PiclID_, int TypeID_, string ItemClass_, string ItemClassDesc_, string LabelNo_, int LabelTypeID_, string CompanyCode_, string last_updated_by_, DateTime last_updated_on_)
        {
            this.PiclID = PiclID_;
            this.TypeID = TypeID_;
            this.ItemClass = ItemClass_;
            this.ItemClassDesc = ItemClassDesc_;
            this.LabelNo = LabelNo_;
            this.LabelTypeID = LabelTypeID_;
            this.CompanyCode = CompanyCode_;
            this.last_updated_by = last_updated_by_;
            this.last_updated_on = last_updated_on_;
        }
    }

    // Price List Data Class
    public class PriceListDC
    {
        public string ITEMNMBR { get; set; }
        public string CURNCYID { get; set; }
        public string PRCLEVEL { get; set; }
        public string UOFM { get; set; }
        public decimal TOQTY { get; set; }
        public decimal UOMPRICE { get; set; }
        public decimal RNDGAMNT { get; set; }
        public short ROUNDHOW { get; set; }
        public short ROUNDTO { get; set; }
        public decimal FROMQTY { get; set; }
        public int ID { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }
        public short UMSLSOPT { get; set; }

        public PriceListDC() { }

        public PriceListDC(string ITEMNMBR_, string CURNCYID_, string PRCLEVEL_, string UOFM_, decimal TOQTY_, decimal UOMPRICE_, decimal RNDGAMNT_, short ROUNDHOW_, short ROUNDTO_, decimal FROMQTY_, int ID_, string TableName_, string DatabaseName_, short UMSLSOPT_)
        {
            this.ITEMNMBR = ITEMNMBR_;
            this.CURNCYID = CURNCYID_;
            this.PRCLEVEL = PRCLEVEL_;
            this.UOFM = UOFM_;
            this.TOQTY = TOQTY_;
            this.UOMPRICE = UOMPRICE_;
            this.RNDGAMNT = RNDGAMNT_;
            this.ROUNDHOW = ROUNDHOW_;
            this.ROUNDTO = ROUNDTO_;
            this.FROMQTY = FROMQTY_;
            this.ID = ID_;
            this.TableName = TableName_;
            this.DatabaseName = DatabaseName_;
            this.UMSLSOPT = UMSLSOPT_;
        }
    }

    #endregion

    /// <summary>
    /// Consolidated Data Access Layer for Label Printing application
    /// Contains all database operations for GP Data Entry, Item management, Job Runs, Print Jobs, Production Operators, and Product Items
    /// </summary>
    public class LabelPrintingDAL : DataAccessBase
    {
        #region GP Data Entry Methods

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

        public DataSet SelectGPDataEntry(string GPDatabase, int EntryType, string ItemCode, string TableName)
        {
            try
            {
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

        public DataSet LookupItemClassByCompany(string compCode)
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
                // Process new rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    GPDataEntryDC dc = DAL.CreateItemFromRow<GPDataEntryDC>(dr);
                    AddGPDataEntry(dc);
                    dr["ID"] = dc.ID;
                }

                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    GPDataEntryDC dc = DAL.CreateItemFromRow<GPDataEntryDC>(dr);
                    UpdateGPDataEntry(dc);
                }

                // Process deleted rows
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    GPDataEntryDC dc = new GPDataEntryDC();
                    dc.ID = id;
                    DeleteGPDataEntry(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateGPDataEntry(GPDataEntryDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.UpdateGP_Inventory_DataEntry", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;
                cmd.Parameters.Add("@SchemaName", SqlDbType.VarChar, 100);
                cmd.Parameters["@SchemaName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@SchemaName"].Value = dc.SchemaName;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 100);
                cmd.Parameters["@ColumnName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ColumnName"].Value = dc.ColumnName;
                cmd.Parameters.Add("@defltValue", SqlDbType.VarChar, 50);
                cmd.Parameters["@defltValue"].Direction = ParameterDirection.Input;
                cmd.Parameters["@defltValue"].Value = dc.defltValue;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
                cmd.Parameters["@Description"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;
                cmd.Parameters.Add("@Reqd", SqlDbType.Bit, 1);
                cmd.Parameters["@Reqd"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Reqd"].Value = dc.Reqd;
                cmd.Parameters.Add("@seq", SqlDbType.Int, 4);
                cmd.Parameters["@seq"].Direction = ParameterDirection.Input;
                cmd.Parameters["@seq"].Value = dc.seq;
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 100);
                cmd.Parameters["@Notes"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Notes"].Value = dc.Notes;
                cmd.Parameters.Add("@ControlType", SqlDbType.VarChar, 50);
                cmd.Parameters["@ControlType"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ControlType"].Value = dc.ControlType;
                cmd.Parameters.Add("@LookupSP", SqlDbType.VarChar, 50);
                cmd.Parameters["@LookupSP"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LookupSP"].Value = dc.LookupSP;
                cmd.Parameters.Add("@LookupArgs", SqlDbType.VarChar, 100);
                cmd.Parameters["@LookupArgs"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LookupArgs"].Value = dc.LookupArgs;
                cmd.Parameters.Add("@inputValue", SqlDbType.VarChar, 100);
                cmd.Parameters["@inputValue"].Direction = ParameterDirection.Input;
                cmd.Parameters["@inputValue"].Value = dc.inputValue;
                cmd.Parameters.Add("@ReadOnly", SqlDbType.Bit, 1);
                cmd.Parameters["@ReadOnly"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ReadOnly"].Value = dc.ReadOnly;
                cmd.Parameters.Add("@displayValue", SqlDbType.VarChar, 100);
                cmd.Parameters["@displayValue"].Direction = ParameterDirection.Input;
                cmd.Parameters["@displayValue"].Value = dc.displayValue;
                cmd.Parameters.Add("@taStoredProcName", SqlDbType.VarChar, 100);
                cmd.Parameters["@taStoredProcName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@taStoredProcName"].Value = dc.taStoredProcName;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void AddGPDataEntry(GPDataEntryDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.AddGP_Inventory_DataEntry", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;
                cmd.Parameters.Add("@SchemaName", SqlDbType.VarChar, 100);
                cmd.Parameters["@SchemaName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@SchemaName"].Value = dc.SchemaName;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 100);
                cmd.Parameters["@ColumnName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ColumnName"].Value = dc.ColumnName;
                cmd.Parameters.Add("@defltValue", SqlDbType.VarChar, 50);
                cmd.Parameters["@defltValue"].Direction = ParameterDirection.Input;
                cmd.Parameters["@defltValue"].Value = dc.defltValue;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100);
                cmd.Parameters["@Description"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;
                cmd.Parameters.Add("@Reqd", SqlDbType.Bit, 1);
                cmd.Parameters["@Reqd"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Reqd"].Value = dc.Reqd;
                cmd.Parameters.Add("@seq", SqlDbType.Int, 4);
                cmd.Parameters["@seq"].Direction = ParameterDirection.Input;
                cmd.Parameters["@seq"].Value = dc.seq;
                cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 100);
                cmd.Parameters["@Notes"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Notes"].Value = dc.Notes;
                cmd.Parameters.Add("@ControlType", SqlDbType.VarChar, 50);
                cmd.Parameters["@ControlType"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ControlType"].Value = dc.ControlType;
                cmd.Parameters.Add("@LookupSP", SqlDbType.VarChar, 50);
                cmd.Parameters["@LookupSP"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LookupSP"].Value = dc.LookupSP;
                cmd.Parameters.Add("@LookupArgs", SqlDbType.VarChar, 100);
                cmd.Parameters["@LookupArgs"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LookupArgs"].Value = dc.LookupArgs;
                cmd.Parameters.Add("@inputValue", SqlDbType.VarChar, 100);
                cmd.Parameters["@inputValue"].Direction = ParameterDirection.Input;
                cmd.Parameters["@inputValue"].Value = dc.inputValue;
                cmd.Parameters.Add("@ReadOnly", SqlDbType.Bit, 1);
                cmd.Parameters["@ReadOnly"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ReadOnly"].Value = dc.ReadOnly;
                cmd.Parameters.Add("@displayValue", SqlDbType.VarChar, 100);
                cmd.Parameters["@displayValue"].Direction = ParameterDirection.Input;
                cmd.Parameters["@displayValue"].Value = dc.displayValue;
                cmd.Parameters.Add("@taStoredProcName", SqlDbType.VarChar, 100);
                cmd.Parameters["@taStoredProcName"].Direction = ParameterDirection.Input;
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

        private void DeleteGPDataEntry(GPDataEntryDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.DeleteGP_Inventory_DataEntry", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        #endregion

        #region Item Currencies Methods

        public DataSet GetGPItemCurrencyForEdit(string databaseName, int entryType, string itemNmbr)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetGPItemCurrencyForEdit]",
                    CreateParameter("@GPDbase", SqlDbType.VarChar, databaseName),
                    CreateParameter("@EntryType", SqlDbType.Int, entryType),
                    CreateParameter("@ITEMNMBR", SqlDbType.VarChar, itemNmbr));
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public DataSet GetItemCurrency_Description()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetItemCurrency_Description]");
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
                // Process new rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemCurrenciesDC dc = DAL.CreateItemFromRow<ItemCurrenciesDC>(dr);
                    AddGP_Inventory_DataEntry_ItemCurrency(dc);
                    dr["ID"] = dc.ID;
                }

                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemCurrenciesDC dc = DAL.CreateItemFromRow<ItemCurrenciesDC>(dr);
                    UpdateGP_Inventory_DataEntry_ItemCurrency(dc);
                }

                // Process deleted rows
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    ItemCurrenciesDC dc = new ItemCurrenciesDC();
                    dc.ID = id;
                    DeleteGP_Inventory_DataEntry_ItemCurrency(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddGP_Inventory_DataEntry_ItemCurrency(ItemCurrenciesDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.AddGP_Inventory_DataEntry_ItemCurrency", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@CURNCYID", SqlDbType.Char, 15);
                cmd.Parameters["@CURNCYID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CURNCYID"].Value = dc.CURNCYID;
                cmd.Parameters.Add("@CURRNIDX", SqlDbType.SmallInt, 2);
                cmd.Parameters["@CURRNIDX"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CURRNIDX"].Value = dc.CURRNIDX;
                cmd.Parameters.Add("@DECPLCUR", SqlDbType.SmallInt, 2);
                cmd.Parameters["@DECPLCUR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DECPLCUR"].Value = dc.DECPLCUR;
                cmd.Parameters.Add("@LISTPRCE", SqlDbType.Decimal, 9);
                cmd.Parameters["@LISTPRCE"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LISTPRCE"].Value = dc.LISTPRCE;
                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
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

        private void UpdateGP_Inventory_DataEntry_ItemCurrency(ItemCurrenciesDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.UpdateGP_Inventory_DataEntry_ItemCurrency", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@CURNCYID", SqlDbType.Char, 15);
                cmd.Parameters["@CURNCYID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CURNCYID"].Value = dc.CURNCYID;
                cmd.Parameters.Add("@CURRNIDX", SqlDbType.SmallInt, 2);
                cmd.Parameters["@CURRNIDX"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CURRNIDX"].Value = dc.CURRNIDX;
                cmd.Parameters.Add("@DECPLCUR", SqlDbType.SmallInt, 2);
                cmd.Parameters["@DECPLCUR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DECPLCUR"].Value = dc.DECPLCUR;
                cmd.Parameters.Add("@LISTPRCE", SqlDbType.Decimal, 9);
                cmd.Parameters["@LISTPRCE"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LISTPRCE"].Value = dc.LISTPRCE;
                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void DeleteGP_Inventory_DataEntry_ItemCurrency(ItemCurrenciesDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.DeleteGP_Inventory_DataEntry_ItemCurrency", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        #endregion

        #region Item Sites Methods

        public DataSet GetGPItemSiteForEdit(string databaseName, int entryType, string itemNmbr)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetGPItemSiteForEdit]",
                    CreateParameter("@GPDbase", SqlDbType.VarChar, databaseName),
                    CreateParameter("@EntryType", SqlDbType.Int, entryType),
                    CreateParameter("@ITEMNMBR", SqlDbType.VarChar, itemNmbr));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

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

        public DataSet GetItemSite_Description()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetItemSite_Description]");
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
                // Process new rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemSitesDC dc = DAL.CreateItemFromRow<ItemSitesDC>(dr);
                    AddGP_Inventory_DataEntry_ItemSite(dc);
                    dr["ID"] = dc.ID;
                }

                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemSitesDC dc = DAL.CreateItemFromRow<ItemSitesDC>(dr);
                    UpdateGP_Inventory_DataEntry_ItemSite(dc);
                }

                // Process deleted rows
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    ItemSitesDC dc = new ItemSitesDC();
                    dc.ID = id;
                    DeleteGP_Inventory_DataEntry_ItemSites(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddGP_Inventory_DataEntry_ItemSite(ItemSitesDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("AddGP_Inventory_DataEntry_ItemSite", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@LOCNCODE", SqlDbType.Char, 11);
                cmd.Parameters["@LOCNCODE"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LOCNCODE"].Value = dc.LOCNCODE;
                cmd.Parameters.Add("@BINNMBR", SqlDbType.Char, 21);
                cmd.Parameters["@BINNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@BINNMBR"].Value = dc.BINNMBR;
                cmd.Parameters.Add("@PRIMVNDR", SqlDbType.Char, 15);
                cmd.Parameters["@PRIMVNDR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PRIMVNDR"].Value = dc.PRIMVNDR;
                cmd.Parameters.Add("@QTYRQSTN", SqlDbType.Decimal, 9);
                cmd.Parameters["@QTYRQSTN"].Direction = ParameterDirection.Input;
                cmd.Parameters["@QTYRQSTN"].Value = dc.QTYRQSTN;
                cmd.Parameters.Add("@Landed_Cost_Group_ID", SqlDbType.Char, 15);
                cmd.Parameters["@Landed_Cost_Group_ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Landed_Cost_Group_ID"].Value = dc.Landed_Cost_Group_ID;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
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

        private void UpdateGP_Inventory_DataEntry_ItemSite(ItemSitesDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("UpdateGP_Inventory_DataEntry_ItemSite", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@LOCNCODE", SqlDbType.Char, 11);
                cmd.Parameters["@LOCNCODE"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LOCNCODE"].Value = dc.LOCNCODE;
                cmd.Parameters.Add("@BINNMBR", SqlDbType.Char, 21);
                cmd.Parameters["@BINNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@BINNMBR"].Value = dc.BINNMBR;
                cmd.Parameters.Add("@PRIMVNDR", SqlDbType.Char, 15);
                cmd.Parameters["@PRIMVNDR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PRIMVNDR"].Value = dc.PRIMVNDR;
                cmd.Parameters.Add("@QTYRQSTN", SqlDbType.Decimal, 9);
                cmd.Parameters["@QTYRQSTN"].Direction = ParameterDirection.Input;
                cmd.Parameters["@QTYRQSTN"].Value = dc.QTYRQSTN;
                cmd.Parameters.Add("@Landed_Cost_Group_ID", SqlDbType.Char, 15);
                cmd.Parameters["@Landed_Cost_Group_ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Landed_Cost_Group_ID"].Value = dc.Landed_Cost_Group_ID;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void DeleteGP_Inventory_DataEntry_ItemSites(ItemSitesDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("DeleteGP_Inventory_DataEntry_ItemSite", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        #endregion

        #region Item Vendors Methods

        public DataSet GetGPItemVendorForEdit(string databaseName, int entryType, string itemNmbr)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetGPItemVendorForEdit]",
                    CreateParameter("@GPDbase", SqlDbType.VarChar, databaseName),
                    CreateParameter("@EntryType", SqlDbType.Int, entryType),
                    CreateParameter("@ITEMNMBR", SqlDbType.VarChar, itemNmbr));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

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

        public DataSet GetItemVendor_Description()
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.[GetItemVendor_Description]");
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
                // Process new rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemVendorsDC dc = DAL.CreateItemFromRow<ItemVendorsDC>(dr);
                    AddGP_Inventory_DataEntry_ItemVendor(dc);
                    dr["ID"] = dc.ID;
                }

                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ItemVendorsDC dc = DAL.CreateItemFromRow<ItemVendorsDC>(dr);
                    UpdateGP_Inventory_DataEntry_ItemVendor(dc);
                }

                // Process deleted rows
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    ItemVendorsDC dc = new ItemVendorsDC();
                    dc.ID = id;
                    DeleteGP_Inventory_DataEntry_ItemVendor(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddGP_Inventory_DataEntry_ItemVendor(ItemVendorsDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("AddGP_Inventory_DataEntry_ItemVendor", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@VENDORID", SqlDbType.Char, 15);
                cmd.Parameters["@VENDORID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@VENDORID"].Value = dc.VENDORID;
                cmd.Parameters.Add("@ITMVNDTY", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ITMVNDTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITMVNDTY"].Value = dc.ITMVNDTY;
                cmd.Parameters.Add("@VNDITNUM", SqlDbType.Char, 31);
                cmd.Parameters["@VNDITNUM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@VNDITNUM"].Value = dc.VNDITNUM;
                cmd.Parameters.Add("@QTYRQSTN", SqlDbType.Decimal, 9);
                cmd.Parameters["@QTYRQSTN"].Direction = ParameterDirection.Input;
                cmd.Parameters["@QTYRQSTN"].Value = dc.QTYRQSTN;
                cmd.Parameters.Add("@AVRGLDTM", SqlDbType.Int, 4);
                cmd.Parameters["@AVRGLDTM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@AVRGLDTM"].Value = dc.AVRGLDTM;
                cmd.Parameters.Add("@NORCTITM", SqlDbType.SmallInt, 2);
                cmd.Parameters["@NORCTITM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NORCTITM"].Value = dc.NORCTITM;
                cmd.Parameters.Add("@MINORQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@MINORQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@MINORQTY"].Value = dc.MINORQTY;
                cmd.Parameters.Add("@MAXORDQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@MAXORDQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@MAXORDQTY"].Value = dc.MAXORDQTY;
                cmd.Parameters.Add("@ECORDQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@ECORDQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ECORDQTY"].Value = dc.ECORDQTY;
                cmd.Parameters.Add("@VNDITDSC", SqlDbType.Char, 101);
                cmd.Parameters["@VNDITDSC"].Direction = ParameterDirection.Input;
                cmd.Parameters["@VNDITDSC"].Value = dc.VNDITDSC;
                cmd.Parameters.Add("@Last_Originating_Cost", SqlDbType.Decimal, 9);
                cmd.Parameters["@Last_Originating_Cost"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Last_Originating_Cost"].Value = dc.Last_Originating_Cost;
                cmd.Parameters.Add("@Last_Currency_ID", SqlDbType.Char, 15);
                cmd.Parameters["@Last_Currency_ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Last_Currency_ID"].Value = dc.Last_Currency_ID;
                cmd.Parameters.Add("@FREEONBOARD", SqlDbType.SmallInt, 2);
                cmd.Parameters["@FREEONBOARD"].Direction = ParameterDirection.Input;
                cmd.Parameters["@FREEONBOARD"].Value = dc.FREEONBOARD;
                cmd.Parameters.Add("@PRCHSUOM", SqlDbType.Char, 9);
                cmd.Parameters["@PRCHSUOM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PRCHSUOM"].Value = dc.PRCHSUOM;
                cmd.Parameters.Add("@CURRNIDX", SqlDbType.SmallInt, 2);
                cmd.Parameters["@CURRNIDX"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CURRNIDX"].Value = dc.CURRNIDX;
                cmd.Parameters.Add("@PLANNINGLEADTIME", SqlDbType.SmallInt, 2);
                cmd.Parameters["@PLANNINGLEADTIME"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PLANNINGLEADTIME"].Value = dc.PLANNINGLEADTIME;
                cmd.Parameters.Add("@ORDERMULTIPLE", SqlDbType.Decimal, 9);
                cmd.Parameters["@ORDERMULTIPLE"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ORDERMULTIPLE"].Value = dc.ORDERMULTIPLE;
                cmd.Parameters.Add("@MNFCTRITMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@MNFCTRITMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@MNFCTRITMNMBR"].Value = dc.MNFCTRITMNMBR;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
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

        private void UpdateGP_Inventory_DataEntry_ItemVendor(ItemVendorsDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("UpdateGP_Inventory_DataEntry_ItemVendor", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@VENDORID", SqlDbType.Char, 15);
                cmd.Parameters["@VENDORID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@VENDORID"].Value = dc.VENDORID;
                cmd.Parameters.Add("@ITMVNDTY", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ITMVNDTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITMVNDTY"].Value = dc.ITMVNDTY;
                cmd.Parameters.Add("@VNDITNUM", SqlDbType.Char, 31);
                cmd.Parameters["@VNDITNUM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@VNDITNUM"].Value = dc.VNDITNUM;
                cmd.Parameters.Add("@QTYRQSTN", SqlDbType.Decimal, 9);
                cmd.Parameters["@QTYRQSTN"].Direction = ParameterDirection.Input;
                cmd.Parameters["@QTYRQSTN"].Value = dc.QTYRQSTN;
                cmd.Parameters.Add("@AVRGLDTM", SqlDbType.Int, 4);
                cmd.Parameters["@AVRGLDTM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@AVRGLDTM"].Value = dc.AVRGLDTM;
                cmd.Parameters.Add("@NORCTITM", SqlDbType.SmallInt, 2);
                cmd.Parameters["@NORCTITM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NORCTITM"].Value = dc.NORCTITM;
                cmd.Parameters.Add("@MINORQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@MINORQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@MINORQTY"].Value = dc.MINORQTY;
                cmd.Parameters.Add("@MAXORDQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@MAXORDQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@MAXORDQTY"].Value = dc.MAXORDQTY;
                cmd.Parameters.Add("@ECORDQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@ECORDQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ECORDQTY"].Value = dc.ECORDQTY;
                cmd.Parameters.Add("@VNDITDSC", SqlDbType.Char, 101);
                cmd.Parameters["@VNDITDSC"].Direction = ParameterDirection.Input;
                cmd.Parameters["@VNDITDSC"].Value = dc.VNDITDSC;
                cmd.Parameters.Add("@Last_Originating_Cost", SqlDbType.Decimal, 9);
                cmd.Parameters["@Last_Originating_Cost"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Last_Originating_Cost"].Value = dc.Last_Originating_Cost;
                cmd.Parameters.Add("@Last_Currency_ID", SqlDbType.Char, 15);
                cmd.Parameters["@Last_Currency_ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Last_Currency_ID"].Value = dc.Last_Currency_ID;
                cmd.Parameters.Add("@FREEONBOARD", SqlDbType.SmallInt, 2);
                cmd.Parameters["@FREEONBOARD"].Direction = ParameterDirection.Input;
                cmd.Parameters["@FREEONBOARD"].Value = dc.FREEONBOARD;
                cmd.Parameters.Add("@PRCHSUOM", SqlDbType.Char, 9);
                cmd.Parameters["@PRCHSUOM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PRCHSUOM"].Value = dc.PRCHSUOM;
                cmd.Parameters.Add("@CURRNIDX", SqlDbType.SmallInt, 2);
                cmd.Parameters["@CURRNIDX"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CURRNIDX"].Value = dc.CURRNIDX;
                cmd.Parameters.Add("@PLANNINGLEADTIME", SqlDbType.SmallInt, 2);
                cmd.Parameters["@PLANNINGLEADTIME"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PLANNINGLEADTIME"].Value = dc.PLANNINGLEADTIME;
                cmd.Parameters.Add("@ORDERMULTIPLE", SqlDbType.Decimal, 9);
                cmd.Parameters["@ORDERMULTIPLE"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ORDERMULTIPLE"].Value = dc.ORDERMULTIPLE;
                cmd.Parameters.Add("@MNFCTRITMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@MNFCTRITMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@MNFCTRITMNMBR"].Value = dc.MNFCTRITMNMBR;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void DeleteGP_Inventory_DataEntry_ItemVendor(ItemVendorsDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("DeleteGP_Inventory_DataEntry_ItemVendor", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        #endregion

        #region Job Run Methods

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

        public DataSet SelectJobRun(DateTime? DateFrom, DateTime? DateTo)
        {
            try
            {
                return ExecuteDataSet("TestPlasmoIntegration.dbo.SelectJobRun",
                     CreateParameter("@DateFrom", SqlDbType.DateTime2, DateFrom),
                     CreateParameter("@DateTo", SqlDbType.DateTime2, DateTo));
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
                string machineType = null;
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

        public void UpdateJobRun(DataSet ds, ref int latestJobRun)
        {
            try
            {
                // Process new rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    JobRunDC dc = DAL.CreateItemFromRow<JobRunDC>(dr);
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    AddJobRun(dc);
                    dr["JobRun"] = dc.JobRun;
                    latestJobRun = (int)dr["JobRun"];
                }

                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    JobRunDC dc = DAL.CreateItemFromRow<JobRunDC>(dr);
                    UpdateJobRun(dc);
                }

                // Process deleted rows
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    JobRunDC dc = DAL.CreateItemFromRow<JobRunDC>(dr);
                    DeleteJobRun(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddJobRun(JobRunDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.AddJobRun", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@JobRun", SqlDbType.Int, 4);
                cmd.Parameters["@JobRun"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@JobRun"].Value = dc.JobRun;
                cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar, 2);
                cmd.Parameters["@CompanyCode"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CompanyCode"].Value = dc.CompanyCode;
                cmd.Parameters.Add("@Code", SqlDbType.Char, 31);
                cmd.Parameters["@Code"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Code"].Value = dc.Code;
                cmd.Parameters.Add("@NumReqd", SqlDbType.Int, 4);
                cmd.Parameters["@NumReqd"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NumReqd"].Value = dc.NumReqd;
                cmd.Parameters.Add("@CUSTNMBR", SqlDbType.Char, 15);
                cmd.Parameters["@CUSTNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CUSTNMBR"].Value = dc.CUSTNMBR;
                cmd.Parameters.Add("@DateReqd", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateReqd"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DateReqd"].Value = dc.DateReqd;
                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;
                cmd.Parameters.Add("@Priority", SqlDbType.Int, 4);
                cmd.Parameters["@Priority"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Priority"].Value = dc.Priority;
                cmd.Parameters.Add("@NumMade", SqlDbType.Int, 4);
                cmd.Parameters["@NumMade"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NumMade"].Value = dc.NumMade;
                cmd.Parameters.Add("@NumScanned", SqlDbType.Int, 4);
                cmd.Parameters["@NumScanned"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NumScanned"].Value = dc.NumScanned;
                cmd.Parameters.Add("@DaysToComplete", SqlDbType.Int, 4);
                cmd.Parameters["@DaysToComplete"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DaysToComplete"].Value = dc.DaysToComplete;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int, 4);
                cmd.Parameters["@StatusID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@StatusID"].Value = dc.StatusID;
                cmd.Parameters.Add("@DateCompleted", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateCompleted"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DateCompleted"].Value = dc.DateCompleted;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = ParameterDirection.InputOutput;
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

        private void UpdateJobRun(JobRunDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.UpdateJobRun", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@JobRun", SqlDbType.Int, 4);
                cmd.Parameters["@JobRun"].Direction = ParameterDirection.Input;
                cmd.Parameters["@JobRun"].Value = dc.JobRun;
                cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar, 2);
                cmd.Parameters["@CompanyCode"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CompanyCode"].Value = dc.CompanyCode;
                cmd.Parameters.Add("@Code", SqlDbType.Char, 31);
                cmd.Parameters["@Code"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Code"].Value = dc.Code;
                cmd.Parameters.Add("@NumReqd", SqlDbType.Int, 4);
                cmd.Parameters["@NumReqd"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NumReqd"].Value = dc.NumReqd;
                cmd.Parameters.Add("@CUSTNMBR", SqlDbType.Char, 15);
                cmd.Parameters["@CUSTNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CUSTNMBR"].Value = dc.CUSTNMBR;
                cmd.Parameters.Add("@DateReqd", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateReqd"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DateReqd"].Value = dc.DateReqd;
                cmd.Parameters.Add("@MachineID", SqlDbType.Int, 4);
                cmd.Parameters["@MachineID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@MachineID"].Value = dc.MachineID;
                cmd.Parameters.Add("@Priority", SqlDbType.Int, 4);
                cmd.Parameters["@Priority"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Priority"].Value = dc.Priority;
                cmd.Parameters.Add("@NumMade", SqlDbType.Int, 4);
                cmd.Parameters["@NumMade"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NumMade"].Value = dc.NumMade;
                cmd.Parameters.Add("@NumScanned", SqlDbType.Int, 4);
                cmd.Parameters["@NumScanned"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NumScanned"].Value = dc.NumScanned;
                cmd.Parameters.Add("@DaysToComplete", SqlDbType.Int, 4);
                cmd.Parameters["@DaysToComplete"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DaysToComplete"].Value = dc.DaysToComplete;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int, 4);
                cmd.Parameters["@StatusID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@StatusID"].Value = dc.StatusID;
                cmd.Parameters.Add("@DateCompleted", SqlDbType.DateTime2, 8);
                cmd.Parameters["@DateCompleted"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DateCompleted"].Value = dc.DateCompleted;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 100);
                cmd.Parameters["@Comment"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Comment"].Value = dc.Comment;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = ParameterDirection.InputOutput;
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

        private void DeleteJobRun(JobRunDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.DeleteJobRun", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@JobRun", SqlDbType.Int, 4);
                cmd.Parameters["@JobRun"].Direction = ParameterDirection.Input;
                cmd.Parameters["@JobRun"].Value = dc.JobRun;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        #endregion

        #region Label Print Job Methods

        public static void AddPrintJob(ref LabelPrintJobDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.AddPrintJob", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@LabelTypeId", SqlDbType.Int, 4);
                cmd.Parameters["@LabelTypeId"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LabelTypeId"].Value = dc.LabelTypeId;
                cmd.Parameters.Add("@Code", SqlDbType.Char, 31);
                cmd.Parameters["@Code"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Code"].Value = dc.Code;
                cmd.Parameters.Add("@Description", SqlDbType.Char, 101);
                cmd.Parameters["@Description"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Description"].Value = dc.Description;
                cmd.Parameters.Add("@NumReqd", SqlDbType.Int, 4);
                cmd.Parameters["@NumReqd"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NumReqd"].Value = dc.NumReqd;
                cmd.Parameters.Add("@StartNo", SqlDbType.Int, 4);
                cmd.Parameters["@StartNo"].Direction = ParameterDirection.Input;
                cmd.Parameters["@StartNo"].Value = dc.StartNo;
                cmd.Parameters.Add("@EndNo", SqlDbType.Int, 4);
                cmd.Parameters["@EndNo"].Direction = ParameterDirection.Input;
                cmd.Parameters["@EndNo"].Value = dc.EndNo;
                cmd.Parameters.Add("@CompanyCode", SqlDbType.Char, 2);
                cmd.Parameters["@CompanyCode"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CompanyCode"].Value = dc.CompanyCode;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Status"].Value = dc.Status;
                cmd.Parameters.Add("@CtnQty", SqlDbType.Int, 4);
                cmd.Parameters["@CtnQty"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CtnQty"].Value = dc.CtnQty;
                cmd.Parameters.Add("@BottleSize", SqlDbType.Char, 11);
                cmd.Parameters["@BottleSize"].Direction = ParameterDirection.Input;
                cmd.Parameters["@BottleSize"].Value = dc.BottleSize;
                cmd.Parameters.Add("@Style", SqlDbType.Char, 11);
                cmd.Parameters["@Style"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Style"].Value = dc.Style;
                cmd.Parameters.Add("@NeckSize", SqlDbType.Char, 11);
                cmd.Parameters["@NeckSize"].Direction = ParameterDirection.Input;
                cmd.Parameters["@NeckSize"].Value = dc.NeckSize;
                cmd.Parameters.Add("@Colour", SqlDbType.Char, 11);
                cmd.Parameters["@Colour"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Colour"].Value = dc.Colour;
                cmd.Parameters.Add("@Material", SqlDbType.VarChar, 20);
                cmd.Parameters["@Material"].Direction = ParameterDirection.Input;
                cmd.Parameters["@Material"].Value = dc.Material;
                cmd.Parameters.Add("@JobRun", SqlDbType.Int, 4);
                cmd.Parameters["@JobRun"].Direction = ParameterDirection.Input;
                cmd.Parameters["@JobRun"].Value = dc.JobRun;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@JobID", SqlDbType.Int, 4);
                cmd.Parameters["@JobID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@JobID"].Value = dc.JobID;

                cmd.ExecuteNonQuery();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.JobID = (int)cmd.Parameters["@JobID"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message + "\n\n" + excp.StackTrace);
            }
        }

        #endregion

        #region Production Operator Methods

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

        public void UpdateProductionOperator(DataSet ds)
        {
            try
            {
                // Process new rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductionOperator dc = DAL.CreateItemFromRow<ProductionOperator>(dr);
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    UpdateProductionOperator(dc);
                    dr["OperatorID"] = dc.OperatorID;
                }

                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductionOperator dc = DAL.CreateItemFromRow<ProductionOperator>(dr);
                    UpdateProductionOperator(dc);
                }

                // Process deleted rows
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductionOperator dc = DAL.CreateItemFromRow<ProductionOperator>(dr);
                    DeleteProductionOperator(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddProductionOperator(ProductionOperator dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("PlasmoIntegration.dbo.AddProductionOperator", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@OperatorID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@OperatorID"].Value = dc.OperatorID;
                cmd.Parameters.Add("@OperatorCode", SqlDbType.NVarChar, 4);
                cmd.Parameters["@OperatorCode"].Direction = ParameterDirection.Input;
                cmd.Parameters["@OperatorCode"].Value = dc.OperatorCode;
                cmd.Parameters.Add("@OperatorName", SqlDbType.NVarChar, 100);
                cmd.Parameters["@OperatorName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@OperatorName"].Value = dc.OperatorName;
                cmd.Parameters.Add("@OperatorClassID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorClassID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@OperatorClassID"].Value = dc.OperatorClassID;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = ParameterDirection.InputOutput;
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

        private void UpdateProductionOperator(ProductionOperator dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("PlasmoIntegration.dbo.UpdateProductionOperator", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@OperatorID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@OperatorID"].Value = dc.OperatorID;
                cmd.Parameters.Add("@OperatorCode", SqlDbType.NVarChar, 4);
                cmd.Parameters["@OperatorCode"].Direction = ParameterDirection.Input;
                cmd.Parameters["@OperatorCode"].Value = dc.OperatorCode;
                cmd.Parameters.Add("@OperatorName", SqlDbType.NVarChar, 100);
                cmd.Parameters["@OperatorName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@OperatorName"].Value = dc.OperatorName;
                cmd.Parameters.Add("@OperatorClassID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorClassID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@OperatorClassID"].Value = dc.OperatorClassID;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = ParameterDirection.InputOutput;
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

        private void DeleteProductionOperator(ProductionOperator dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("PlasmoIntegration.dbo.DeleteProductionOperator", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@OperatorID", SqlDbType.Int, 4);
                cmd.Parameters["@OperatorID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@OperatorID"].Value = dc.OperatorID;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        #endregion

        #region Product Item Label Link Methods

        public void UpdateProductItemClassLabelLink(DataSet ds)
        {
            try
            {
                // Process new rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductItemClassLabelLink dc = DAL.CreateItemFromRow<ProductItemClassLabelLink>(dr);
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    UpdateProductItemClassLabelLink(dc);
                    dr["PiclID"] = dc.PiclID;
                }

                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductItemClassLabelLink dc = DAL.CreateItemFromRow<ProductItemClassLabelLink>(dr);
                    UpdateProductItemClassLabelLink(dc);
                }

                // Process deleted rows
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int piclID = (int)dr["PiclID", DataRowVersion.Original];
                    ProductItemClassLabelLink dc = new ProductItemClassLabelLink();
                    dc.PiclID = piclID;
                    DeleteProductItemClassLabelLink(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateProductItemClassLabelLink(ProductItemClassLabelLink dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("PlasmoIntegration.dbo.UpdateProductItemClassLabelLink", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@PiclID", SqlDbType.Int, 4);
                cmd.Parameters["@PiclID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@PiclID"].Value = dc.PiclID;
                cmd.Parameters.Add("@TypeID", SqlDbType.Int, 4);
                cmd.Parameters["@TypeID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TypeID"].Value = dc.TypeID;
                cmd.Parameters.Add("@ItemClass", SqlDbType.Char, 11);
                cmd.Parameters["@ItemClass"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ItemClass"].Value = dc.ItemClass;
                cmd.Parameters.Add("@ItemClassDesc", SqlDbType.Char, 31);
                cmd.Parameters["@ItemClassDesc"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ItemClassDesc"].Value = dc.ItemClassDesc;
                cmd.Parameters.Add("@LabelNo", SqlDbType.VarChar, 10);
                cmd.Parameters["@LabelNo"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LabelNo"].Value = dc.LabelNo;
                cmd.Parameters.Add("@LabelTypeID", SqlDbType.Int, 4);
                cmd.Parameters["@LabelTypeID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@LabelTypeID"].Value = dc.LabelTypeID;
                cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar, 10);
                cmd.Parameters["@CompanyCode"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CompanyCode"].Value = dc.CompanyCode;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_on"].Value = dc.last_updated_on;

                cmd.ExecuteNonQuery();
                dc.PiclID = (int)cmd.Parameters["@PiclID"].Value;
                dc.last_updated_by = cmd.Parameters["@last_updated_by"].Value.ToString();
                dc.last_updated_on = (DateTime)cmd.Parameters["@last_updated_on"].Value;
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void DeleteProductItemClassLabelLink(ProductItemClassLabelLink dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("PlasmoIntegration.dbo.DeleteProductItemClassLabelLink", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@PiclID", SqlDbType.Int, 4);
                cmd.Parameters["@PiclID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PiclID"].Value = dc.PiclID;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        #endregion

        #region Price List Methods

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
                    CreateParameter("@ITEMNMBR", SqlDbType.VarChar, itemNmbr));
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
                    CreateParameter("@UOFM", SqlDbType.VarChar, uofm));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public DataSet SelectPriceLevel(string databaseName)
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
                // Process new rows
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    PriceListDC dc = DAL.CreateItemFromRow<PriceListDC>(dr);
                    AddGP_Inventory_PriceList(dc);
                    dr["ID"] = dc.ID;
                }

                // Process modified rows
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    PriceListDC dc = DAL.CreateItemFromRow<PriceListDC>(dr);
                    UpdateGP_Inventory_PriceList(dc);
                }

                // Process deleted rows
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int id = (int)dr["ID", DataRowVersion.Original];
                    PriceListDC dc = new PriceListDC();
                    dc.ID = id;
                    DeleteGP_Inventory_PriceList(dc);
                }

                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddGP_Inventory_PriceList(PriceListDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.AddGP_Inventory_PriceList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@CURNCYID", SqlDbType.Char, 15);
                cmd.Parameters["@CURNCYID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CURNCYID"].Value = dc.CURNCYID;
                cmd.Parameters.Add("@PRCLEVEL", SqlDbType.Char, 11);
                cmd.Parameters["@PRCLEVEL"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PRCLEVEL"].Value = dc.PRCLEVEL;
                cmd.Parameters.Add("@UOFM", SqlDbType.Char, 9);
                cmd.Parameters["@UOFM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@UOFM"].Value = dc.UOFM;
                cmd.Parameters.Add("@TOQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@TOQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TOQTY"].Value = dc.TOQTY;
                cmd.Parameters.Add("@UOMPRICE", SqlDbType.Decimal, 9);
                cmd.Parameters["@UOMPRICE"].Direction = ParameterDirection.Input;
                cmd.Parameters["@UOMPRICE"].Value = dc.UOMPRICE;
                cmd.Parameters.Add("@RNDGAMNT", SqlDbType.Decimal, 9);
                cmd.Parameters["@RNDGAMNT"].Direction = ParameterDirection.Input;
                cmd.Parameters["@RNDGAMNT"].Value = dc.RNDGAMNT;
                cmd.Parameters.Add("@ROUNDHOW", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ROUNDHOW"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ROUNDHOW"].Value = dc.ROUNDHOW;
                cmd.Parameters.Add("@ROUNDTO", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ROUNDTO"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ROUNDTO"].Value = dc.ROUNDTO;
                cmd.Parameters.Add("@FROMQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@FROMQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@FROMQTY"].Value = dc.FROMQTY;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;
                cmd.Parameters.Add("@UMSLSOPT", SqlDbType.SmallInt, 2);
                cmd.Parameters["@UMSLSOPT"].Direction = ParameterDirection.Input;
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

        private void UpdateGP_Inventory_PriceList(PriceListDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.UpdateGP_Inventory_PriceList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;
                cmd.Parameters.Add("@ITEMNMBR", SqlDbType.Char, 31);
                cmd.Parameters["@ITEMNMBR"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ITEMNMBR"].Value = dc.ITEMNMBR;
                cmd.Parameters.Add("@CURNCYID", SqlDbType.Char, 15);
                cmd.Parameters["@CURNCYID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@CURNCYID"].Value = dc.CURNCYID;
                cmd.Parameters.Add("@PRCLEVEL", SqlDbType.Char, 11);
                cmd.Parameters["@PRCLEVEL"].Direction = ParameterDirection.Input;
                cmd.Parameters["@PRCLEVEL"].Value = dc.PRCLEVEL;
                cmd.Parameters.Add("@UOFM", SqlDbType.Char, 9);
                cmd.Parameters["@UOFM"].Direction = ParameterDirection.Input;
                cmd.Parameters["@UOFM"].Value = dc.UOFM;
                cmd.Parameters.Add("@TOQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@TOQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TOQTY"].Value = dc.TOQTY;
                cmd.Parameters.Add("@UOMPRICE", SqlDbType.Decimal, 9);
                cmd.Parameters["@UOMPRICE"].Direction = ParameterDirection.Input;
                cmd.Parameters["@UOMPRICE"].Value = dc.UOMPRICE;
                cmd.Parameters.Add("@RNDGAMNT", SqlDbType.Decimal, 9);
                cmd.Parameters["@RNDGAMNT"].Direction = ParameterDirection.Input;
                cmd.Parameters["@RNDGAMNT"].Value = dc.RNDGAMNT;
                cmd.Parameters.Add("@ROUNDHOW", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ROUNDHOW"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ROUNDHOW"].Value = dc.ROUNDHOW;
                cmd.Parameters.Add("@ROUNDTO", SqlDbType.SmallInt, 2);
                cmd.Parameters["@ROUNDTO"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ROUNDTO"].Value = dc.ROUNDTO;
                cmd.Parameters.Add("@FROMQTY", SqlDbType.Decimal, 9);
                cmd.Parameters["@FROMQTY"].Direction = ParameterDirection.Input;
                cmd.Parameters["@FROMQTY"].Value = dc.FROMQTY;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 100);
                cmd.Parameters["@TableName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@TableName"].Value = dc.TableName;
                cmd.Parameters.Add("@DatabaseName", SqlDbType.VarChar, 100);
                cmd.Parameters["@DatabaseName"].Direction = ParameterDirection.Input;
                cmd.Parameters["@DatabaseName"].Value = dc.DatabaseName;
                cmd.Parameters.Add("@UMSLSOPT", SqlDbType.SmallInt, 2);
                cmd.Parameters["@UMSLSOPT"].Direction = ParameterDirection.Input;
                cmd.Parameters["@UMSLSOPT"].Value = dc.UMSLSOPT;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        private void DeleteGP_Inventory_PriceList(PriceListDC dc)
        {
            try
            {
                SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new SqlCommand("TestPlasmoIntegration.dbo.DeleteGP_Inventory_PriceList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int, 4);
                cmd.Parameters["@ID"].Direction = ParameterDirection.Input;
                cmd.Parameters["@ID"].Value = dc.ID;

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
        }

        #endregion
    }
}
