using APC.Applications.DTO;
using APC.Helper;
using APC.Utility;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewEventSales : Form
    {
        private Applications.DTO.EventSalesDTO _eventSalesDTO;
        private EventDTO _eventDTO;

        public FormViewEventSales()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private int buttonSize = 14;
        private float panelSize;

        public void loadForView(Applications.DTO.EventSalesDTO eventSalesDTO)
        {
            _eventSalesDTO = eventSalesDTO;
        }

        public void loadEventData(EventDTO eventDTO)
        {
            _eventDTO = eventDTO;
        }

        private void ControlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label3);
            GeneralHelper.ApplyRegularFont(16, labelSummary, labelAmountSold, labelDate);
        }

        private void FormViewEventSales_Load(object sender, EventArgs e)
        {
            ControlsFont();

            labelTitle.Text = _eventSalesDTO.EventTitle;
            labelSummary.Text = _eventSalesDTO.Summary;
            labelAmountSold.Text = _eventSalesDTO.AmountSold.ToString() + " €";
            labelDate.Text = _eventSalesDTO.FormattedSalesDate;
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
                panel1.Size = new Size(1001, 60);
                iconZoomIn.IconSize = 40;
                iconZoomOut.IconSize = 40;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
                panel1.Size = new Size(1001, 51);
                iconZoomIn.IconSize = 32;
                iconZoomOut.IconSize = 32;

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
                panel1.Size = new Size(1001, 51);
                iconZoomIn.IconSize = 32;
                iconZoomOut.IconSize = 32;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
