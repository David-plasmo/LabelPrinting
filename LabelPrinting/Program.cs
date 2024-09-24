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

        //        static void Main(string[] args)
        //        {
        //#if DEBUG
        //            //args = new[] { "MenuOptionJobRun" };
        //            //args = new[] { "MenuOptionPalletLabel" };
        //#endif

        //            if (args.Length == 0)
        //                Application.Run(new MainForm(null));
        //            else if (args.Length ==1)
        //            {
        //                string menuOption = args[0];
        //                Application.EnableVisualStyles();
        //                Application.SetCompatibleTextRenderingDefault(false);  


        //                if (menuOption == "MenuOptionJobRun")
        //                    Application.Run(new JobRun());
        //                else if (menuOption == "MenuOptionPalletLabel")
        //                    Application.Run(new PromptPalletLabelPrint());
        //            }

        //        }


        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //string[] args = Environment.GetCommandLineArgs();
#if DEBUG
            //args[0] = "AppendApplicationObjects";
#endif


            if (args.Length == 0)
            {
                Application.Run(new PromptPalletLabelPrint());
            }
            else if (args[0] == "GetFormNames")
            {
                ApplicationAccess aa = new ApplicationAccess();
                aa.GetFormNames("LabelPrinting");
            }
            else if (args[0] == "RefreshFormNames")
            {
                ApplicationAccess aa = new ApplicationAccess();
                aa.RefreshFormNames("LabelPrinting");
            }
            else if (args[0] == "AppendApplicationObjects")
            {
                ApplicationAccess aa = new ApplicationAccess();
                aa.AppendApplicationObjectList("LabelPrinting");
            }

        }
          
        }
    }
