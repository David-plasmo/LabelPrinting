
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccessControl;

namespace LabelPrinting
{
    static class Program
    {
        // ***also dllimport of that function***
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            args = new[] { "MenuOptionJobRun" };
            //args = new[] { "MenuOptionPalletLabel" };
            //args = new[] { "Edit" };
            //args = new[] { "LabelPrinting" };  // default for release
#endif
            if (args.Length == 0)
                Application.Run(new MainForm(null));
            else if (args.Length == 1)
            {
                string menuOption = args[0];
                if (menuOption == "LabelPrinting")
                    Application.Run(new MainForm(null));
                else if (menuOption == "MenuOptionJobRun")
                    Application.Run(new JobRun());
                else if (menuOption == "MenuOptionPalletLabel")
                    Application.Run(new PromptPalletLabelPrint());                
                else if (menuOption == "Edit")
                {
                    throw new NotImplementedException("Edit functionality is not implemented in ApplicationAccess.");
                }
            }
        }
    }
}
