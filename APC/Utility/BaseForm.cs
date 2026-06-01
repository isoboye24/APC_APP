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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(387, 253);
            this.Name = "BaseForm";
            this.ResumeLayout(false);

        }
    }
}
