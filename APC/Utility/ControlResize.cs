using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace APC.Utility
{
    public static class ControlResize
    {
        public static void ResizeTaggedControls(Control parent, float newFontSize, float resizeFactor = 1.1f)
        {
            foreach (Control c in parent.Controls)
            {
                // Only resize controls tagged "resizable"
                if (c.Tag != null && c.Tag.ToString() == "resizable")
                {
                    // Resize font
                    if (c is Label || c is Button || c is TextBox)
                    {
                        c.Font = new Font("Segoe UI", newFontSize, c.Font.Style);
                    }

                    // 🟣 Resize FontAwesome IconButton
                    else if (c is IconButton iconButton)
                    {
                        iconButton.Font = new Font("Segoe UI", newFontSize, iconButton.Font.Style);
                        iconButton.IconSize = (int)(iconButton.IconSize * resizeFactor);
                    }

                    // 🟠 Resize FontAwesome IconPictureBox
                    else if (c is IconPictureBox iconPicture)
                    {
                        iconPicture.IconSize = (int)(iconPicture.IconSize * resizeFactor);
                    }

                    // Resize panel or group container
                    if (c is Panel || c is GroupBox)
                    {
                        c.Width = (int)(c.Width * resizeFactor);
                        c.Height = (int)(c.Height * resizeFactor);
                    }
                }

                // Recurse into children
                if (c.HasChildren)
                    ResizeTaggedControls(c, newFontSize, resizeFactor);
            }
        }

        // ✅ New helper — apply resizing to all open forms
        public static void ResizeAllOpenForms(float newFontSize, float resizeFactor = 1.1f)
        {
            foreach (Form f in Application.OpenForms)
            {
                ResizeTaggedControls(f, newFontSize, resizeFactor);
            }
        }
    }
}
