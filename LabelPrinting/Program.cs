using ApplicationAccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelPrinting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            //args = new[] { "MenuOptionJobRun" };
            args = new[] { "MenuOptionPalletLabel" };
            //args = new[] { "AppendApplicationObjects" };
#endif
            if (args.Length == 0)
                Application.Run(new MainForm(null));
            else if (args.Length == 1)
            {
                string menuOption = args[0];

                if (menuOption == "MenuOptionJobRun")
                    Application.Run(new JobRun());
                else if (menuOption == "MenuOptionPalletLabel")
                    Application.Run(new PromptPalletLabelPrint());
                //else if (menuOption == "GetFormNames")
                //{
                //    ApplicationAccess aa = new ApplicationAccess();
                //    aa.GetFormNames("LabelPrinting");
                //}
                //else if (menuOption == "RefreshFormNames")
                //{
                //    ApplicationAccess aa = new ApplicationAccess();
                //    aa.RefreshFormNames("LabelPrinting");
                //}
                else if (menuOption == "AppendApplicationObjects")
                {
                    ApplicationAccess aa = new ApplicationAccess();
                    aa.AppendApplicationObjectList("LabelPrinting");
                }
            }
        }
    }
}
