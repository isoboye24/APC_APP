using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using APC.Utility;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormEventSales : Form
    {
        private readonly IEventSalesService _eventSalesService;

        private EventDTO _eventDTO;
        private EventSalesDTO _eventSalesDTO;

        private bool _isUpdate = false;
        private int buttonSize = 14;
        private float panelSize;

        public FormEventSales(IEventSalesService eventSalesService)
        {
            InitializeComponent();
            _eventSalesService = eventSalesService;
        }

        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public void loadEventData(EventDTO eventDTO)
        {
            _eventDTO = eventDTO;
        }
        
        public void loadForEdit(EventSalesDTO eventSalesDTO, bool isUpdate)
        {
            _eventSalesDTO = eventSalesDTO;
            _isUpdate = isUpdate;
        }

        private void ControlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label3, btnClose, btnSave);
            GeneralHelper.ApplyRegularFont(16, txtAmountSold, txtSummary, dateTimePickerEventSalesDate);
        }

        private void FormEventSales_Load(object sender, EventArgs e)
        {
            ControlsFont();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit " + _eventSalesDTO.Summary + " of " + _eventDTO.Title;

                txtAmountSold.Text = _eventSalesDTO.AmountSold.ToString();
                txtSummary.Text = _eventSalesDTO.Summary;
                dateTimePickerEventSalesDate.Value = _eventSalesDTO.SalesDate;
            }
            else
            {
                labelTitle.Text = "Add Sales";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = Convert.ToDecimal(txtAmountSold.Text.Trim());
                var summary = txtSummary.Text.Trim();
                DateTime date = dateTimePickerEventSalesDate.Value;

                var eventSalesData = new EventSales(_eventDTO.EventsId, amount, summary, date);

                if (_eventSalesDTO.EventSalesId == 0)
                {
                    _eventSalesService.Create(eventSalesData);
                    MessageBox.Show("Event Sale created successfully!");
                }
                else
                {
                    _eventSalesService.Update(eventSalesData);
                    MessageBox.Show("Event Sale updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
