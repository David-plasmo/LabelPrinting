using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace DataService
{

    public class ProductDataService : DataAccessBase
    {
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///	Creates a new DataService
        /// </summary>
        ////////////////////////////////////////////////////////////////////////
        public ProductDataService() : base() { }

        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///	Creates a new DataService and specifies a transaction with
        ///	which to operate
        /// </summary>
        ////////////////////////////////////////////////////////////////////////
        public ProductDataService(IDbTransaction txn) : base(txn) { }
        /// <summary>
        /// Creates a BarTender print queue              
        /// <para>Implemented by table LabelPrintQueue in Database BarTender<br/>
        /// Invokes SP BarTender.dbo.ClearBarTenderPrintQueue, PlasmoIntegration.dbo.EnqueuePrintLabels
        /// </para>
        /// </summary>
        /// <param name="ds">DataSet containing print queue</param>
        /// <param name="labelTypeId">Print label identifier</param>
        /// <param name="numReqd">Number of labels to print</param>
        /// <param name="newJobRun">Identifies a production job run</param>        
        public void EnqueueBartenderLabels(DataSet ds, int labelTypeId, int numReqd, int newJobRun)
        {           
            try
            {
                //string qCode = "MP";
                ExecuteNonQuery("ClearBarTenderPrintQueue");

                DataViewRowState dvrs = DataViewRowState.CurrentRows;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                int result;
                //newJobRun = null;

               //ExecuteNonQuery("PlasmoIntegration.dbo.CreateJobRun",
               //                     CreateParameter("@JobRun", SqlDbType.Int, newJobRun, ParameterDirection.Output));
               // newJobRun = (int)this.DABCmd.Parameters["@JobRun"].Value;

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];    
                                   
                    if (Convert.ToBoolean(dr["DoPrint"].ToString()))
                    {
                        //[EnqueueMouldLabels](@Code char(31), @Description varchar(101), @CtnQty int, @BottleSize char(11),
                        //@Style char(11), @NeckSize char(11), @Colour char(11), @Material char(11), @JobRun int, @CompanyCode char(2),
                        //@LabelNo int, @DoLabelNo bit, @QCode varchar(2))  
                                               
                        if (Int32.TryParse(dr["LabelNo"].ToString(), out result))
                        {
                            ExecuteNonQuery("[PlasmoIntegration].[dbo].[EnqueuePrintLabels]",
                                            CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                                            CreateParameter("@Description", SqlDbType.VarChar, dr["Description"].ToString()),
                                            CreateParameter("@CtnQty", SqlDbType.Int, Convert.ToInt32(dr["CtnQty"].ToString())),
                                            CreateParameter("@NumReqd", SqlDbType.Int, numReqd),
                                            CreateParameter("@BottleSize", SqlDbType.VarChar, dr["BottleSize"].ToString()),
                                            CreateParameter("@Style", SqlDbType.VarChar, dr["Style"].ToString()),
                                            CreateParameter("@NeckSize", SqlDbType.VarChar, dr["NeckSize"].ToString()),
                                            CreateParameter("@Colour", SqlDbType.VarChar, dr["Colour"].ToString()),
                                            CreateParameter("@Material", SqlDbType.VarChar, dr["Material"].ToString()),
                                            CreateParameter("@JobRun", SqlDbType.Int, newJobRun),
                                            CreateParameter("@CompanyCode", SqlDbType.VarChar, dr["CompanyCode"].ToString()),
                                            CreateParameter("@LabelNo", SqlDbType.Int, Convert.ToInt32(dr["LabelNo"].ToString())),
                                            CreateParameter("@DoLabelNo", SqlDbType.Bit, Convert.ToBoolean(dr["DoLabelNo"].ToString())),
                                            CreateParameter("@LabelTypeId", SqlDbType.Int, labelTypeId),
                                            CreateParameter("@ImagePath", SqlDbType.VarChar, dr["ImagePath"].ToString())
                                           );
                            
                        }

                                       
                    }	 
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PalletLabelPrintJob(int palletStartNo, int palletEndNo)
        {
            try
            {
                ExecuteNonQuery("[BarTender].[dbo].[CreatePalletLabelJob]",
                             CreateParameter("@PalletStart", SqlDbType.Int, palletStartNo),
                             CreateParameter("@PalletFin", SqlDbType.Int, palletEndNo));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Logs label printing jobs, and updates print status        
        /// <para>Implemented by table LabelPrintJob in database BarTender<br/>
        /// Invokes AddPrintJob, UpdatePrintJob, DeletePrintJob</para>
        /// <summary>
        /// <param name="ds">DataSet containing BarTender label print job details</param>
        /// <param name="LabelTypeId">Label type identifier</param>
        public void UpdatePrintJobs(DataSet ds, int LabelTypeId)
        {
            //CREATE PROCEDURE [dbo].[AddPrintJob](
            //@LabelTypeId int, @Code char(31), @Description char(101), @NumReqd int, @MachineID int, @CUSTNMBR char(15),
            //@CompanyCode char(2), @Status varchar(50), @CtnQty int, @BottleSize char(11), @Style char(11),
            //@NeckSize char(11), @Colour char(11), @Material varchar(20),
            //@JobRun int OUTPUT, @last_updated_on datetime2 OUTPUT, @JobID int OUTPUT)

            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                
                int newJobID = 0;
                string newLastUpdatedBy = "last_updated_by";
                DateTime newLastUpdatedOn = DateTime.MinValue;
                DateTime date;
                string format = "yyyy-MM-dd";
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    string description = (dr["Description"].ToString() != dr["Code"].ToString()) ? dr["Description"].ToString() : null;
                    string bottlesize = (dr.Table.Columns.Contains("BottleSize")) ? dr["BottleSize"].ToString() : null;
                    string style = (dr.Table.Columns.Contains("Style")) ? dr["Style"].ToString() : null;
                    string necksize = (dr.Table.Columns.Contains("NeckSize")) ? dr["NeckSize"].ToString() : null;
                    string colour = (dr.Table.Columns.Contains("Colour")) ? dr["Colour"].ToString() : null;
                    string material = (dr.Table.Columns.Contains("Material")) ? dr["Material"].ToString() : null;
                    string company = (dr.Table.Columns.Contains("Company")) ? dr["Company"].ToString() : null;
                    int? ctnQty = null;
                    int? numReqd = null;
                    int? startNo = null;
                    int? endNo = null;
                    int? newJobRun = null;
                    int number, result;
                    if (dr.Table.Columns.Contains("CtnQty"))
                    {
                        if (Int32.TryParse(dr["CtnQty"].ToString(), out number))
                            ctnQty = number;
                    }
                    if (dr.Table.Columns.Contains("NumReqd"))
                    {
                        if (Int32.TryParse(dr["NumReqd"].ToString(), out number))
                            numReqd = number;
                    }
                    if (dr.Table.Columns.Contains("StartNo"))
                    {
                        if (Int32.TryParse(dr["StartNo"].ToString(), out number))
                            startNo = number;
                    }
                    if (dr.Table.Columns.Contains("EndNo"))
                    {
                        if (Int32.TryParse(dr["EndNo"].ToString(), out number))
                            endNo = number;
                    }
                    
                    ExecuteNonQuery("PlasmoIntegration.dbo.CreateJobRun",
                                         CreateParameter("@JobRun", SqlDbType.Int, newJobRun, ParameterDirection.Output));

                    newJobRun = (int)this.DABCmd.Parameters["@JobRun"].Value;
                    

                    ExecuteNonQuery("AddPrintJob",
                        CreateParameter("@LabelTypeId", SqlDbType.Int, Convert.ToInt32(dr["LabelTypeId"].ToString())),
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@Description", SqlDbType.VarChar, description),
                        CreateParameter("@NumReqd", SqlDbType.Int, numReqd),
                        CreateParameter("@StartNo", SqlDbType.Int, startNo),
                        CreateParameter("@EndNo", SqlDbType.Int, endNo),
                        CreateParameter("@MachineID", SqlDbType.Int, null),
                        CreateParameter("@CUSTNMBR", SqlDbType.VarChar, null),
                        CreateParameter("@CompanyCode", SqlDbType.VarChar, company),
                        CreateParameter("@Status", SqlDbType.VarChar, dr["Status"].ToString()),
                        CreateParameter("@CtnQty", SqlDbType.Int, ctnQty),
                        CreateParameter("@BottleSize", SqlDbType.VarChar, bottlesize),
                        CreateParameter("@Style", SqlDbType.VarChar, style),
                        CreateParameter("@NeckSize", SqlDbType.VarChar, necksize),
                        CreateParameter("@Colour", SqlDbType.VarChar, colour),
                        CreateParameter("@Material", SqlDbType.VarChar, material),
                        CreateParameter("@JobRun", SqlDbType.Int, newJobRun),
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, newLastUpdatedOn, ParameterDirection.Output),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.InputOutput),
                        CreateParameter("@JobID", SqlDbType.Int, newJobID, ParameterDirection.Output));

                    if (dr.Table.Columns.Contains("Run"))
                    {
                        dr["Run"] = null;
                        if (Int32.TryParse(dr["Run"].ToString(), out number)) dr["Run"] = number;
                    }
                    newLastUpdatedOn = (DateTime)this.DABCmd.Parameters["@last_updated_on"].Value;
                    newLastUpdatedBy = (string)this.DABCmd.Parameters["@last_updated_by"].Value;
                    newJobID = (int)this.DABCmd.Parameters["@JobID"].Value;
                    //newJobRun = (int)this.DABCmd.Parameters["@JobRun"].Value;

                    dr["last_updated_on"] = newLastUpdatedOn;
                    dr["last_updated_by"] = newLastUpdatedBy;
                    dr["JobID"] = newJobID;
                    dr["JobRun"] = newJobRun;
                }


                //Process modified rows:-
                //CREATE PROCEDURE [dbo].[UpdatePrintJob](@JobID int, @LabelTypeId int, @Code char(31), @Description char(101), @NumReqd int, @MachineID int, @CUSTNMBR char(15),
                //@CompanyCode char(2), @Status varchar(50), @CtnQty int, @BottleSize char(11), @Style char(11),
                // @NeckSize char(11), @Colour char(11), @Material varchar(20),
                //@JobRun int, @last_updated_on datetime2 OUTPUT)	

                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    date = Convert.ToDateTime(dr["last_updated_on"]);
                    string description = (dr["Description"].ToString() != dr["Code"].ToString()) ? dr["Description"].ToString() : null;
                    string bottlesize = (dr.Table.Columns.Contains("BottleSize")) ? dr["BottleSize"].ToString() : null;
                    string style = (dr.Table.Columns.Contains("Style")) ? dr["Style"].ToString() : null;
                    string necksize = (dr.Table.Columns.Contains("NeckSize")) ? dr["NeckSize"].ToString() : null;
                    string colour = (dr.Table.Columns.Contains("Colour")) ? dr["Colour"].ToString() : null;
                    string material = (dr.Table.Columns.Contains("Material")) ? dr["Material"].ToString() : null;
                    string company = (dr.Table.Columns.Contains("Company")) ? dr["Company"].ToString() : null;
                    int? jobrun = null;
                    int number;
                    if (dr.Table.Columns.Contains("JobRun"))
                    {
                        if (Int32.TryParse(dr["JobRun"].ToString(), out number))
                            //dr["JobRun"] = number;
                            jobrun = number;
                    }
                    int? ctnQty = null;
                    int? numReqd = null;
                    int? startNo = null;
                    int? endNo = null;
                    if (dr.Table.Columns.Contains("CtnQty"))
                    {
                        if (Int32.TryParse(dr["CtnQty"].ToString(), out number))
                            ctnQty = number;
                    }
                    if (dr.Table.Columns.Contains("NumReqd"))
                    {
                        if (Int32.TryParse(dr["NumReqd"].ToString(), out number))
                            numReqd = number;
                    }
                    if (dr.Table.Columns.Contains("StartNo"))
                    {
                        if (Int32.TryParse(dr["StartNo"].ToString(), out number))
                            startNo = number;
                    }
                    if (dr.Table.Columns.Contains("EndNo"))
                    {
                        if (Int32.TryParse(dr["EndNo"].ToString(), out number))
                            endNo = number;
                    }
                    ExecuteNonQuery("UpdatePrintJob",
                        CreateParameter("@JobID", SqlDbType.Int, Convert.ToInt32(dr["JobId"].ToString())),
                        CreateParameter("@LabelTypeId", SqlDbType.Int, Convert.ToInt32(dr["LabelTypeId"].ToString())),
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@Description", SqlDbType.VarChar, description),
                        CreateParameter("@NumReqd", SqlDbType.Int, numReqd),
                        CreateParameter("@StartNo", SqlDbType.Int, startNo),
                        CreateParameter("@EndNo", SqlDbType.Int, endNo),
                        CreateParameter("@MachineID", SqlDbType.Int, null),
                        CreateParameter("@CUSTNMBR", SqlDbType.VarChar, null),
                        CreateParameter("@CompanyCode", SqlDbType.VarChar, company),
                        CreateParameter("@Status", SqlDbType.VarChar, dr["Status"].ToString()),
                        CreateParameter("@CtnQty", SqlDbType.Int, ctnQty),
                        CreateParameter("@BottleSize", SqlDbType.VarChar, bottlesize),
                        CreateParameter("@Style", SqlDbType.VarChar, style),
                        CreateParameter("@NeckSize", SqlDbType.VarChar, necksize),
                        CreateParameter("@Colour", SqlDbType.VarChar, colour),
                        CreateParameter("@Material", SqlDbType.VarChar, material),
                        CreateParameter("@JobRun", SqlDbType.Int, jobrun),
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, date, ParameterDirection.Output),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.InputOutput));

                    newLastUpdatedOn = (DateTime)this.DABCmd.Parameters["@last_updated_on"].Value;
                    newLastUpdatedBy = (string)this.DABCmd.Parameters["@last_updated_by"].Value;
                    dr["last_updated_on"] = newLastUpdatedOn;
                    dr["last_updated_by"] = newLastUpdatedBy;
                }


                //process deleted rows:-
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    // Console.WriteLine(dr["Run", DataRowVersion.Original].ToString());
                    if (dr["JobID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("DeletePrintJob",
                          CreateParameter("@JobID", SqlDbType.Int, Convert.ToInt32(dr["JobID", DataRowVersion.Original].ToString())));
                    }

                }

                ds.AcceptChanges();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        /// <summary>
        /// Maintains a table of items designated for printing onto plain labels        
        /// <para>Implemented by table PlasmoPlainLabels, in database BarTender <br/>
        /// Invokes SP AddPlasmoLabels, UpdatePlasmoPlainLabels, DeletePlasmoPlainLabels</para>
        /// </summary>
        /// <param name="ds">DataSet containing plain label product items</param>
        public void UpdatePlainLabels(DataSet ds)
        {
            //CREATE PROCEDURE[dbo].[AddPlasmoPlainLabels](@Code char(31), @Description varchar(101), 
	        //@ItemClass char(11), @LabelNo varchar(10), @Purpose varchar(100), @last_updated_on datetime2(7) output,
	        //@PlainLabelID int output)

            try
            {

                //Process new rows:-
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);                
                int newPlainLabelID = 0;
                string newLastUpdatedBy = "last_updated_by";
                DateTime newLastUpdatedOn = DateTime.MinValue;
                //DateTime date = DateTime.MinValue;
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    
                    ExecuteNonQuery("AddPlasmoPlainLabels",                        
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@Description", SqlDbType.VarChar, dr["Description"].ToString()),                       
                        CreateParameter("@ItemClass", SqlDbType.VarChar, dr["ItemClass"].ToString()),
                        CreateParameter("@LabelNo", SqlDbType.VarChar, dr["LabelNo"].ToString()),
                        CreateParameter("@Purpose", SqlDbType.VarChar, dr["Purpose"].ToString()),                        
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, newLastUpdatedOn, ParameterDirection.Output),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.InputOutput),
                        CreateParameter("@PlainLabelID", SqlDbType.Int, newPlainLabelID, ParameterDirection.Output));

                    newLastUpdatedOn = (DateTime)this.DABCmd.Parameters["@last_updated_on"].Value;
                    newLastUpdatedBy = (string)this.DABCmd.Parameters["@last_updated_by"].Value;
                    newPlainLabelID = (int)this.DABCmd.Parameters["@PlainLabelID"].Value;                    
                    dr["last_updated_on"] = newLastUpdatedOn;
                    dr["last_updated_by"] = newLastUpdatedBy;
                    dr["PlainLabelID"] = newPlainLabelID;
                }


                //Process modified rows:-
                //CREATE PROCEDURE [dbo].[UpdatePlasmoPlainLabels](@PlainLabelID int, @Code char(31), @Description varchar(101), 
                //@ItemClass char(11), @LabelNo varchar(10), @Purpose varchar(100), @last_updated_on datetime2(7) output)	

                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    newLastUpdatedOn = Convert.ToDateTime(dr["last_updated_on"]);
                    //CREATE PROCEDURE [dbo].[UpdatePlasmoPlainLabels](@PlainLabelID int, @Code char(31), @Description varchar(101), 
                    //@ItemClass char(11), @LabelNo varchar(10), @Purpose varchar(100), @last_updated_on datetime2(7) output)
                    ExecuteNonQuery("UpdatePlasmoPlainLabels",
                        CreateParameter("@PlainLabelID", SqlDbType.Int, Convert.ToInt32(dr["PlainLabelID"].ToString())),                       
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@Description", SqlDbType.VarChar, dr["Description"].ToString()),
                        CreateParameter("@ItemClass", SqlDbType.VarChar, dr["ItemClass"].ToString()),
                        CreateParameter("@LabelNo", SqlDbType.VarChar, dr["LabelNo"].ToString()),
                        CreateParameter("@Purpose", SqlDbType.VarChar, dr["Purpose"].ToString()),                        
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, newLastUpdatedOn, ParameterDirection.InputOutput),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.InputOutput));

                    newLastUpdatedOn = (DateTime)this.DABCmd.Parameters["@last_updated_on"].Value;
                    newLastUpdatedBy = (string)this.DABCmd.Parameters["@last_updated_by"].Value;
                    dr["last_updated_on"] = newLastUpdatedOn;
                    dr["last_updated_by"] = newLastUpdatedBy;
                }


                //process deleted rows:-
                //CREATE PROCEDURE [dbo].[DeletePlasmoPlainLabels]( @PlainLabelID int)	
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    // Console.WriteLine(dr["Run", DataRowVersion.Original].ToString());
                    if (dr["PlainLabelID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("DeletePlasmoPlainLabels",
                          CreateParameter("@PlainLabelID", SqlDbType.Int, Convert.ToInt32(dr["PlainLabelID", DataRowVersion.Original].ToString())));
                    }

                }

                ds.AcceptChanges();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw;
            }
        }

       /// <summary>
       /// Retrieves products designated for printing onto plain labels, for presentation on DataGridView
       /// <para>Invokes SP GetPlasmoPlainLabels in database BarTender</para>
       /// </summary>
       /// <returns>DataSet</returns>
        public DataSet GetPlainLabels()
        {
            try
            {
                DataSet ds = ExecuteDataSet("BarTender.dbo.GetPlasmoPlainLabels");
                    //,
                    //CreateParameter("@LabelTypeId", SqlDbType.Int, LabelTypeId));
                //if (ds.Tables[0].Rows.Count == 1 && ds.Tables[0].Rows[0][0].ToString().Length == 0 ) { ds.Tables[0].Rows[0].Delete(); }
                
                //ds.Tables[0].Columns["Code"].DefaultValue = "<New>";
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetPastelMaster()
        {
            try
            {
                DataSet ds = ExecuteDataSet("BarTender.dbo.GetCPMasterListExport");                
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public DataSet PromptPrintJob(int LabelTypeId)
        {            
            try
            {                
                DataSet ds = ExecuteDataSet("PromptPrintJob",
                    CreateParameter("@LabelTypeId", SqlDbType.Int, LabelTypeId));                
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        

        public void UpdatePastelMaster(DataSet ds)
        {            
            try
            {

                //Process new rows:-
                ///CREATE PROCEDURE [dbo].[AddCPMasterListExport](@Code char(31), @Description varchar(101), 
                //@CategoryID int, @CtnQty int, @CtnSize varchar(11), @Grade char(2), @last_updated_on datetime2(7) output,
                //@last_updated_by varchar(50) output, @PastelID int output)

                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                int newPastelID = 0;
                string newLastUpdatedBy = "last_updated_by";
                DateTime newLastUpdatedOn = DateTime.MinValue;
                DateTime date = DateTime.MinValue;

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    ExecuteNonQuery("[PlasmoIntegration].[dbo].[AddCPMasterListExport]",
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@Description", SqlDbType.VarChar, dr["Description"].ToString()),
                        CreateParameter("@CategoryID", SqlDbType.Int, Convert.ToInt32(dr["CategoryID"].ToString())),
                        CreateParameter("@CtnQty", SqlDbType.Int, Convert.ToInt32(dr["CtnQty"].ToString())),
                        CreateParameter("@CtnSize", SqlDbType.VarChar, dr["CtnSize"].ToString()),
                        CreateParameter("@Grade", SqlDbType.VarChar, dr["Grade"].ToString()),
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, newLastUpdatedOn, ParameterDirection.Output),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.Output),
                        CreateParameter("@PastelID", SqlDbType.Int, newPastelID, ParameterDirection.Output));
                    
                    if (DateTime.TryParse(this.DABCmd.Parameters["@last_updated_on"].Value.ToString(), out date))
                    {
                        newLastUpdatedOn = date;
                        dr["last_updated_on"] = newLastUpdatedOn;
                    }
                                       
                    newLastUpdatedBy = this.DABCmd.Parameters["@last_updated_by"].Value.ToString();
                    newPastelID = (int)this.DABCmd.Parameters["@PastelID"].Value;
                   
                    dr["last_updated_by"] = newLastUpdatedBy;
                    dr["PastelID"] = newPastelID;
                }


                //Process modified rows:-
                //CREATE PROCEDURE [dbo].[UpdateCPMasterListExport](@PastelID int, @Code char(31), @Description varchar(101), 
                //@CategoryID int, @CtnQty int, @CtnSize varchar(11), @Grade char(2), @last_updated_on datetime2(7) output,
                //@last_updated_by varchar(50) output)

                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    date = Convert.ToDateTime(dr["last_updated_on"]);
                    
                    ExecuteNonQuery("[PlasmoIntegration].[dbo].[UpdateCPMasterListExport]",
                        CreateParameter("@PastelID", SqlDbType.Int, Convert.ToInt32(dr["PastelID"].ToString())),
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@Description", SqlDbType.VarChar, dr["Description"].ToString()),
                        CreateParameter("@CategoryID", SqlDbType.Int, Convert.ToInt32(dr["CategoryID"].ToString())),
                        CreateParameter("@CtnQty", SqlDbType.Int, Convert.ToInt32(dr["CtnQty"].ToString())),
                        CreateParameter("@CtnSize", SqlDbType.VarChar, dr["CtnSize"].ToString()),
                        CreateParameter("@Grade", SqlDbType.VarChar, dr["Grade"].ToString()),
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, date, ParameterDirection.InputOutput),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.InputOutput));

                    newLastUpdatedOn = (DateTime)this.DABCmd.Parameters["@last_updated_on"].Value;
                    newLastUpdatedBy = (string)this.DABCmd.Parameters["@last_updated_by"].Value;
                    dr["last_updated_on"] = newLastUpdatedOn;
                    dr["last_updated_by"] = newLastUpdatedBy;
                }


                //process deleted rows:-
                //CREATE PROCEDURE [dbo].[DeleteCPMasterListExport]( @PastelID int)	
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    // Console.WriteLine(dr["Run", DataRowVersion.Original].ToString());
                    if (dr["PastelID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("[PlasmoIntegration].[dbo].[DeleteCPMasterListExport]",
                          CreateParameter("@PastelID", SqlDbType.Int, Convert.ToInt32(dr["PastelID", DataRowVersion.Original].ToString())));
                    }

                }

                ds.AcceptChanges();


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                throw;
            }
        }


        public void UpdateProductMaterial(DataSet ds)
        {
            try
            {

                //Process new rows:-                
                DataViewRowState dvrs = DataViewRowState.Added;
                DataRow[] rows = ds.Tables[0].Select("", "", dvrs);
                int newID = 0;
                string newLastUpdatedBy = "last_updated_by";
                DateTime newLastUpdatedOn = DateTime.MinValue;
                DateTime date = DateTime.MinValue;

                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];

                    ExecuteNonQuery("[PlasmoIntegration].[dbo].[AddProductMaterial]",
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@CompanyCode", SqlDbType.VarChar, dr["CompanyCode"].ToString()),
                        CreateParameter("@MaterialID", SqlDbType.Int, Convert.ToInt32(dr["MaterialID"].ToString())),
                        CreateParameter("@GradeID", SqlDbType.Int, Convert.ToInt32(dr["GradeID"].ToString())),
                        CreateParameter("@DGNumber", SqlDbType.VarChar, dr["DGNumber"].ToString()),
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, date, ParameterDirection.Output),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.Output),
                        CreateParameter("@PmID", SqlDbType.Int, newID, ParameterDirection.Output));

                    if (DateTime.TryParse(this.DABCmd.Parameters["@last_updated_on"].Value.ToString(), out date))
                    {
                        newLastUpdatedOn = date;
                        dr["last_updated_on"] = newLastUpdatedOn;
                    }

                    newLastUpdatedBy = this.DABCmd.Parameters["@last_updated_by"].Value.ToString();
                    newID = (int)this.DABCmd.Parameters["@PmID"].Value;

                    dr["last_updated_by"] = newLastUpdatedBy;
                    dr["PmID"] = newID;
                }


                //Process modified rows:-                
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    date = Convert.ToDateTime(dr["last_updated_on"]);

                    ExecuteNonQuery("[PlasmoIntegration].[dbo].[UpdateProductMaterial]",
                        CreateParameter("@Code", SqlDbType.VarChar, dr["Code"].ToString()),
                        CreateParameter("@CompanyCode", SqlDbType.VarChar, dr["CompanyCode"].ToString()),
                        CreateParameter("@MaterialID", SqlDbType.Int, Convert.ToInt32(dr["MaterialID"].ToString())),
                        CreateParameter("@GradeID", SqlDbType.Int, Convert.ToInt32(dr["GradeID"].ToString())),
                        CreateParameter("@DGNumber", SqlDbType.VarChar, dr["DGNumber"].ToString()),
                        CreateParameter("@last_updated_on", SqlDbType.DateTime2, date, ParameterDirection.InputOutput),
                        CreateParameter("@last_updated_by", SqlDbType.VarChar, newLastUpdatedBy, ParameterDirection.Output),
                        CreateParameter("@PmID", SqlDbType.Int, Convert.ToInt32(dr["pmID"].ToString())));

                    newLastUpdatedOn = (DateTime)this.DABCmd.Parameters["@last_updated_on"].Value;
                    newLastUpdatedBy = (string)this.DABCmd.Parameters["@last_updated_by"].Value;
                    dr["last_updated_on"] = newLastUpdatedOn;
                    dr["last_updated_by"] = newLastUpdatedBy;
                }


                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    // Console.WriteLine(dr["Run", DataRowVersion.Original].ToString());
                    if (dr["PmID", DataRowVersion.Original] != null)
                    {
                        ExecuteNonQuery("[PlasmoIntegration].[dbo].[DeleteProductMaterial]",
                          CreateParameter("@PmID", SqlDbType.Int, Convert.ToInt32(dr["PmID", DataRowVersion.Original].ToString())));
                    }
                }

                ds.AcceptChanges();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }


        public DataSet GetMaterialAndGrade(string compCode)
        {
            try
            {
                DataSet ds = ExecuteDataSet("[PlasmoIntegration].[dbo].[GetMaterialAndGrade]",
                     CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode));
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataSet GetLabelTypes()
        {            
            try
            {                
                DataSet ds = ExecuteDataSet("GetLabelTypes");               
                //ds.Tables[0].Columns["Code"].DefaultValue = defaultCode;
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }
        public DataSet GetLabelTypesByCompany(string compCode)
        {
            try
            {
                DataSet ds = ExecuteDataSet("BarTender.dbo.GetLabelTypesByCompany",
                    CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode));
                //ds.Tables[0].Columns["Code"].DefaultValue = defaultCode;
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }
        public DataSet GetLabelTypesTV()
        {
            try
            {
                DataSet ds = ExecuteDataSet("GetLabelTypesTV");
                //ds.Tables[0].Columns["Code"].DefaultValue = defaultCode;
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }
       
        public DataSet GetProductCompany()
        {
            try
            {
                return ExecuteDataSet("GetProductCompany");
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

        public DataSet GetMaterialIndex()
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[GetMaterialIndex]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public DataSet GetProductGradeIndex()
        {
            try
            {
                return ExecuteDataSet("[PlasmoIntegration].[dbo].[GetProductGradeIndex]");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetLabelSetting(string printer)
        {
            try
            {
                return ExecuteDataSet("GetCurrentLabelSetting",
                    CreateParameter("@Printer", SqlDbType.VarChar, printer));            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public void UpdateLabelSetting(string printer, string labelNo)
        {
            try
            {
                ExecuteNonQuery("UpdateCurrentLabelSetting",
                    CreateParameter("@Printer", SqlDbType.VarChar, printer),
                    CreateParameter("@LabelNo", SqlDbType.VarChar, labelNo));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());               
            }
        }

        //public DataSet GetMachine()
        //{
        //    try
        //    {
        //        return ExecuteDataSet("GetMachine");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return null;
        //    }
        //}

        //public DataSet GetCustomer()
        //{
        //    try
        //    {
        //        return ExecuteDataSet("GetCustomer");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return null;
        //    }
        //}

        //public DataSet GetProductionStatus()
        //{
        //    try
        //    {
        //        return ExecuteDataSet("GetProductionStatus");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return null;
        //    }
        //}
        /// <summary>
        /// Gets product labels of a specified type for a specificed company
        /// </summary>
        /// <param name="LabelTypeId"></param>
        /// <param name="LabelNo"></param>
        /// <param name="Company"></param>
        /// <remarks>References stored procedure GetProductIndex</remarks>
        /// <returns>DataSet</returns>     
        public DataSet GetProductIndex(int LabelTypeId, string LabelNo, string Company)
        {
            //CREATE PROCEDURE [dbo].[GetProductIndex](@LabelTypeId int, @LabelNo varchar(10), @Company varchar(2))
            try
            {
                return ExecuteDataSet("GetProductIndex",
                    CreateParameter("@LabelTypeId", SqlDbType.Int, LabelTypeId),
                    CreateParameter("@LabelNo", SqlDbType.VarChar, LabelNo),
                    CreateParameter("@Company", SqlDbType.VarChar, Company));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetProductType()
        {
            try
            {
                return ExecuteDataSet("ProdPlasmoProduct.dbo.GetProductType");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet SelectProductItemClassLabelLink(string compCode)
        {
            try
            {
                return ExecuteDataSet("PlasmoIntegration.dbo.SelectProductItemClassLabelLink",
                    CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        //SelectItemClassByCompany
        public DataSet SelectItemClassByCompany(string compCode)
        {
            try
            {
                return ExecuteDataSet("PlasmoIntegration.dbo.SelectItemClassByCompany",
                    CreateParameter("@CompanyCode", SqlDbType.VarChar, compCode));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataSet GetPlasmoProductIndex()
        {
            try
            {
                return ExecuteDataSet("GetPlasmoProductIndex");
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetPastelProductIndex()
        {
            try
            {
                return ExecuteDataSet("GetPastelProductIndex");
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetPastelCategory()
        {
            try
            {
                return ExecuteDataSet("BarTender.dbo.GetPastelCategory");
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Fetches current label print job summary for editing
        /// </summary>
        /// <param name="jobId">Print job identifier</param>
        /// <param name="labelTypeId">Label type identifier</param>
        /// <param name="numSpare">Number of spare labels to print</param>
        /// <param name="startNo">Label number to start printing from, eg. 1</param>
        /// <param name="endNo">Label number to end printing on, eg. 10</param>
        /// <remarks>References stored procedure GetLabelPrintJob</remarks>
        /// <returns>DataSet</returns>
        public DataSet GetLabelPrintJob(int jobId, int labelTypeId, int numSpare, int startNo, int endNo)
        {
            try
            {
                //PROCEDURE [dbo].[GetLabelPrintJob] (@JobID int, @LabelTypeID int, @NumSpare int)
                //return ExecuteDataSet("TestPlasmoIntegration.dbo.GetLabelPrintJob",
                  return ExecuteDataSet("GetLabelPrintJob",
                    CreateParameter("@JobId", SqlDbType.Int, jobId),
                    CreateParameter("@labelTypeID", SqlDbType.Int, labelTypeId),                    
                    CreateParameter("@NumSpare", SqlDbType.Int, numSpare),
                    CreateParameter("@StartNo", SqlDbType.Int, startNo),
                    CreateParameter("@EndNo", SqlDbType.Int, endNo));
                    //CreateParameter("@BoughtInForCo", SqlDbType.VarChar, manCompany));                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show(ex.ToString());
                return null;
            }
        }

        //public void ClearBMPrintQueu()
        //{
        //    try
        //    {
        //        ExecuteNonQuery("ClearBMPrintQueue");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());                
        //    }
        //}

        public DataView PSClasses { get; set; }

        //function to create temporary unique ID keys, for insert operations in datagridview controls
        // - assumes an autoincrementing primary key is required in a destination table, and that an appropriate
        //   Insert Stored Procedure that does not require the ID key to be passed will be used.  This allows edits
        //   to continue in datagridview, before committing.
        private static int NextID = 0;
        public int GetNextID()
        {
            NextID -= 1;
            return NextID;
        }

        

    } //class

} //namespace

