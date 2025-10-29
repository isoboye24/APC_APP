using System;
using System.Windows.Forms;

namespace APC.Utility
{
    public static class AutoResizeInitializer
    {
        public static void Initialize(Form form, float newFontSize, float resizeFactor = 1.1f)
        {
            TagCommonControls(form);
            ControlResize.ResizeTaggedControls(form, newFontSize, resizeFactor);
        }

        private static void TagCommonControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button || c is Label || c is TextBox || c is ComboBox || c is DataGridView)
                    c.Tag = "resizable";

                if (c.HasChildren)
                    TagCommonControls(c);
            }
        }
    }
}
