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
            if (CurrentFontSize > 6f)
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
