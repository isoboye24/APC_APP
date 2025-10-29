using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.Utility
{
    public static class ZoomManager
    {
        public static float CurrentFontSize { get; private set; } = 10f;
        private const float ResizeStep = 1.1f;

        public static void ZoomIn(Form form)
        {
            CurrentFontSize += 1f;
            ControlResize.ResizeTaggedControls(form, CurrentFontSize, ResizeStep);
        }

        public static void ZoomOut(Form form)
        {
            if (CurrentFontSize > 6f) // prevent too small text
            {
                CurrentFontSize -= 1f;
                ControlResize.ResizeTaggedControls(form, CurrentFontSize, 1 / ResizeStep);
            }
        }

        public static void ResetZoom(Form form)
        {
            CurrentFontSize = 10f;
            ControlResize.ResizeTaggedControls(form, CurrentFontSize, 1f);
        }
    }
}
