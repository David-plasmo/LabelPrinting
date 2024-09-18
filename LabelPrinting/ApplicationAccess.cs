using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.SqlClient;
using DataService;
using System.Data;
using System.Windows.Forms;

namespace ApplicationAccessControl
{

    
    public class ApplicationAccess : DataAccessBase
    {
        /// <summary>
        /// Retrieves all types in a given namespace within a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to search for types.</param>
        /// <param name="nameSpace">The namespace to search for.</param>
        /// <returns>An array of types in the specified namespace.</returns>
        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        /// <summary>
        /// Retrieves and prints the names of all types that are subclasses of Form within the specified namespace.
        /// </summary>
        /// <param name="nameSpace">The namespace to search for forms.</param>
        public void GetFormNames(string nameSpace)
        {
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), nameSpace);
            for (int i = 0; i < typelist.Length; i++)
            {
                if (typelist[i].IsSubclassOf(typeof(Form)))
                {
                    Console.WriteLine(typelist[i].Name); // Only forms will be written here
                }
            }
        }

        /// <summary>
        /// Retrieves and updates the names of all types that are subclasses of Form within the specified namespace.
        /// </summary>
        /// <param name="nameSpace">The namespace to search for forms.</param>
        public void RefreshFormNames(string nameSpace) 
        {
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), nameSpace);
            for (int i = 0; i < typelist.Length; i++)
            {
                if (typelist[i].IsSubclassOf(typeof(Form)))
                {
                    string fname = typelist[i].Name;
                    UpdateFormNames(fname, nameSpace);
                }
            }
        }

        public void AppendApplicationObjectList(string nameSpace)
        {
            string controlsList = nameSpace;
            ApplicationObjectListDC dc = new ApplicationObjectListDC();

            //ApplicationObjectListDAL dal = new ApplicationObjectListDAL();
            Type[] typelist = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == nameSpace).ToArray();

            dc.Tag = nameSpace;
            dc.NodeText = nameSpace;
            dc.IconNo = 0;
            ApplicationObjectListDAL.AddApplicationObjectList(dc);

            foreach (Type type in typelist)
            {
                if (type.IsSubclassOf(typeof(Form)))
                {
                    controlsList += @"\" + nameSpace + @"\Forms\" + type.Name;

                    dc.Tag = @"\" + nameSpace + @"\Forms";
                    dc.NodeText = "Forms";
                    dc.IconNo = 0;
                    ApplicationObjectListDAL.AddApplicationObjectList(dc);

                    dc.Tag = controlsList;
                    dc.NodeText = type.Name;
                    dc.IconNo = 0;
                    dc.Checked = true;
                    ApplicationObjectListDAL.AddApplicationObjectList(dc);


                    Form formInstance;
                    formInstance = (Form)Activator.CreateInstance(type);
                    string fname = type.Name;

                    for (int j = 0; j < formInstance.Controls.Count; j++)
                    {
                        Control control = formInstance.Controls[j];

                        if (control is CheckedListBox || control is Button || control is TextBox)
                        {
                            controlsList += @"\" + nameSpace + @"\Forms\" + type.Name + @"\Controls\" + control.Name;
                            dc.Tag = controlsList;
                            dc.NodeText = control.Name;
                            dc.IconNo = 0;
                            dc.Checked = true;
                            ApplicationObjectListDAL.AddApplicationObjectList(dc);
                        }
                    }

                    formInstance.Dispose();
                }
            }
            //MessageBox.Show(controlsList);
        }

        /// <summary>
        /// Updates the form names in the database for a given form and application.
        /// </summary>
        /// <param name="fname">The name of the form.</param>
        /// <param name="appName">The name of the application.</param>
        private void UpdateFormNames(string fname, string appName) 
        { 
            try
            {
                // Establish connection to the database
                System.Data.SqlClient.SqlCommand cmd = null;
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                cmd = new System.Data.SqlClient.SqlCommand("PlasmoAdmin.dbo.AddUpdateFormNames", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.Add("@FormName", SqlDbType.VarChar, 100);
                cmd.Parameters["@FormName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@FormName"].Value = fname;

                cmd.Parameters.Add("@AppName", SqlDbType.VarChar, 100);
                cmd.Parameters["@AppName"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters["@AppName"].Value = appName;

                // Execute the stored procedure
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception ex) 
            {
                // Show error message if an exception occurs
                MessageBox.Show(ex.Message);
            }
        
        }
        
    }
    
}
