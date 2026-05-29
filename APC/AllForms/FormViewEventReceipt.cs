using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using APC.Utility;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewEventReceipt : Form
    {
        private EventReceiptDTO _eventReceiptDTO;
        private EventDTO _eventDTO;

        private int buttonSize = 14;
        private float panelSize;

        public FormViewEventReceipt()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                buttonSize = 18;
                panelSize = 3.05f;
                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
        }

        private void iconZoomOut_Click(object sender, EventArgs e)
        {
            buttonSize -= 1;
            panelSize -= 1.05f;
            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private void iconZoomIn_Click(object sender, EventArgs e)
        {
            buttonSize += 1;
            panelSize += 1.05f;
            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        public void loadForView(EventReceiptDTO eventReceiptDTO)
        {
            _eventReceiptDTO = eventReceiptDTO;
        }

        public void loadEventData(EventDTO eventDTO)
        {
            _eventDTO = eventDTO;
        }

        private void ControlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label3, btnClose);
            GeneralHelper.ApplyRegularFont(16, labelDate, labelSummary, labelAmount);
        }

        private void FormViewEventReceipt_Load(object sender, EventArgs e)
        {
            ControlsFont();

            labelTitle.Text = _eventReceiptDTO.Caption + " of " + _eventDTO.Title;
            labelDate.Text = _eventReceiptDTO.FormattedReceiptDate;
            labelSummary.Text = _eventReceiptDTO.Summary;
            labelAmount.Text = _eventReceiptDTO.AmountSpent + " €";

            string imagePath = System.IO.Path.Combine(Application.StartupPath, "images", _eventReceiptDTO.ImagePath);
            if (System.IO.File.Exists(imagePath))
                picEventReceipt.ImageLocation = imagePath;
            else
                picEventReceipt.Image = null;
        }
    }
}
