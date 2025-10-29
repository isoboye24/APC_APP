using APC.AllForms;
using APC.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var initializedForms = new HashSet<Form>();

            Application.Idle += (s, e) =>
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (!initializedForms.Contains(form))
                    {
                        initializedForms.Add(form);
                        form.Load += (sender, args) =>
                        {
                            AutoResizeInitializer.Initialize(form, ZoomManager.CurrentFontSize);
                        };
                    }
                }
            };

            Application.Run(new FormLogin());
        }
    }
}
