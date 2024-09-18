using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataService;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Xml.Linq;

internal class ApplicationObjectListDAL :DataAccessBase
{

	public static void AddApplicationObjectList(ApplicationObjectListDC dc)
	{
	  try
	  {
		   System.Data.SqlClient.SqlCommand cmd = null;
		   SqlConnection connection = new SqlConnection(GetConnectionStringPlasmoSQL());
		   connection.Open();
		   cmd = new System.Data.SqlClient.SqlCommand("PlasmoAdmin.dbo.AddApplicationObjectList", connection);
		   cmd.CommandType = System.Data.CommandType.StoredProcedure;
	 
		   cmd.Parameters.Add("@ParentID", SqlDbType.Int,4);
		   cmd.Parameters["@ParentID"].Direction = System.Data.ParameterDirection.Input;
		   cmd.Parameters["@ParentID"].Value = dc.ParentID;
		   cmd.Parameters.Add("@NodeText", SqlDbType.VarChar,100);
		   cmd.Parameters["@NodeText"].Direction = System.Data.ParameterDirection.Input;
		   cmd.Parameters["@NodeText"].Value = dc.NodeText;
		   cmd.Parameters.Add("@Checked", SqlDbType.Bit,1);
		   cmd.Parameters["@Checked"].Direction = System.Data.ParameterDirection.Input;
		   cmd.Parameters["@Checked"].Value = dc.Checked;
		   cmd.Parameters.Add("@IconNo", SqlDbType.Int,4);
		   cmd.Parameters["@IconNo"].Direction = System.Data.ParameterDirection.Input;
		   cmd.Parameters["@IconNo"].Value = dc.IconNo;
		   cmd.Parameters.Add("@Tag", SqlDbType.VarChar,500);
		   cmd.Parameters["@Tag"].Direction = System.Data.ParameterDirection.Input;
		   cmd.Parameters["@Tag"].Value = dc.Tag;
	 
		   cmd.ExecuteNonQuery();
	 
		   connection.Close();
	  }
	catch(Exception excp)
	   {
			MessageBox.Show(excp.Message);
	   }
	}
}
