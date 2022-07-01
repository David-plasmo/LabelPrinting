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


    }
}
