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
    class ProductItemLabelLinkDAL : DataAccessBase
    {
        public void UpdateProductItemClassLabelLink(DataSet ds)
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
                    ProductItemClassLabelLink dc = DAL.CreateItemFromRow<ProductItemClassLabelLink>(dr);  //populate dataclass     
                    dc.last_updated_on = DateTime.MinValue;
                    dc.last_updated_by = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    ProductItemLabelLinkDAL.UpdateProductItemClassLabelLink(dc);
                    dr["PiclID"] = dc.PiclID;
                }

                //Process modified rows:-
                dvrs = DataViewRowState.ModifiedCurrent;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    ProductItemClassLabelLink dc = DAL.CreateItemFromRow<ProductItemClassLabelLink>(dr);  //populate BMSetupData dataclass                   
                    ProductItemLabelLinkDAL.UpdateProductItemClassLabelLink(dc);
                }

                //process deleted rows:-                
                dvrs = DataViewRowState.Deleted;
                rows = ds.Tables[0].Select("", "", dvrs);
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    int piclID = (int)dr["PiclID", DataRowVersion.Original];
                    ProductItemClassLabelLink dc = new ProductItemClassLabelLink();
                    dc.PiclID = piclID;
                    //ProductItemClassLabelLink dc = DAL.CreateItemFromRow<ProductItemClassLabelLink>(dr);  //populate dataclass                   
                    ProductItemLabelLinkDAL.DeleteProductItemClassLabelLink(dc);
                }
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }           
        }
        private static void UpdateProductItemClassLabelLink(ProductItemClassLabelLink dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("PlasmoIntegration.dbo.UpdateProductItemClassLabelLink", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PiclID", SqlDbType.Int, 4);
                cmd.Parameters["@PiclID"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@PiclID"].Value = dc.PiclID;
                cmd.Parameters.Add("@TypeID", SqlDbType.Int, 4);
                cmd.Parameters["@TypeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@TypeID"].Value = dc.TypeID;
                cmd.Parameters.Add("@ItemClass", SqlDbType.Char, 11);
                cmd.Parameters["@ItemClass"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemClass"].Value = dc.ItemClass;
                cmd.Parameters.Add("@ItemClassDesc", SqlDbType.Char, 31);
                cmd.Parameters["@ItemClassDesc"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@ItemClassDesc"].Value = dc.ItemClassDesc;
                cmd.Parameters.Add("@LabelNo", SqlDbType.VarChar, 10);
                cmd.Parameters["@LabelNo"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelNo"].Value = dc.LabelNo;
                cmd.Parameters.Add("@LabelTypeID", SqlDbType.Int, 4);
                cmd.Parameters["@LabelTypeID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@LabelTypeID"].Value = dc.LabelTypeID;
                cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar, 10);
                cmd.Parameters["@CompanyCode"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@CompanyCode"].Value = dc.CompanyCode;
                cmd.Parameters.Add("@last_updated_by", SqlDbType.VarChar, 50);
                cmd.Parameters["@last_updated_by"].Direction = System.Data.ParameterDirection.InputOutput;
                cmd.Parameters["@last_updated_by"].Value = dc.last_updated_by;
                cmd.Parameters.Add("@last_updated_on", SqlDbType.DateTime2, 8);
                cmd.Parameters["@last_updated_on"].Direction = System.Data.ParameterDirection.InputOutput;
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
        public static void DeleteProductItemClassLabelLink(ProductItemClassLabelLink dc)
        {
            try
            {
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(ProductDataService.GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("PlasmoIntegration.dbo.DeleteProductItemClassLabelLink", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("@PiclID", SqlDbType.Int, 4);
                cmd.Parameters["@PiclID"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@PiclID"].Value = dc.PiclID;

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
