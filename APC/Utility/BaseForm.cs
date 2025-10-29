using System;
using System.Windows.Forms;

namespace APC.Utility
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            this.Load += (s, e) =>
            {
                // ✅ Correct call with 3 arguments
                ControlResize.ResizeTaggedControls(this, ZoomManager.CurrentFontSize, 1.1f);
            };
        }
    }
}
