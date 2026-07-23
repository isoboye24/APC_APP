using System;
using System.Windows.Forms;

namespace APC.Utility
{
    public class BaseFormDashboard : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            FontManagerDashboard.Apply(
                this,
                WindowState);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            FontManagerDashboard.Apply(
                this,
                WindowState);
        }
    }
}