using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Utility;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormEventExpenditure : Form
    {
        private readonly IEventExpenditureService _eventExpenditureService;

        private EventDTO _eventDTO;
        private EventExpenditureDTO _eventExpenditureDTO;

        private bool _isUpdate = false;
        private int buttonSize = 14;
        private float panelSize;

        public FormEventExpenditure(IEventExpenditureService eventExpenditureService)
        {
            InitializeComponent();
            _eventExpenditureService = eventExpenditureService;
        }

        /// <summary>
        ///  Drag
        /// </summary>
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

        public void loadEventData(EventDTO eventDTO)
        {
            _eventDTO = eventDTO;
        }

        public void loadForEdit(EventExpenditureDTO eventExpenditureDTO, bool isUpdate)
        {
            _eventExpenditureDTO = eventExpenditureDTO;
            _isUpdate = isUpdate;
        }

        private void controlsFont()
        {
            label1.Tag = "resizable";
            label2.Tag = "resizable";
            label3.Tag = "resizable";

            txtAmountSpent.Tag = "resizable";
            txtSummary.Tag = "resizable";

            dateTimePickerEventExpDate.Tag = "resizable";

            btnClose.Tag = "resizable";
            btnSave.Tag = "resizable";
        }

        private void FormEventExpenditure_Load(object sender, EventArgs e)
        {
            controlsFont();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit " + _eventExpenditureDTO.Summary + " of " + _eventDTO.Title;

                txtAmountSpent.Text = _eventExpenditureDTO.SpentAmount.ToString();
                txtSummary.Text = _eventExpenditureDTO.Summary;
                dateTimePickerEventExpDate.Value = _eventExpenditureDTO.ExpenditureDate;
            }
            else
            {
                labelTitle.Text = "Add Expenditure";
            }

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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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

        private void iconZoomIn_Click(object sender, EventArgs e)
        {
            buttonSize += 1;
            panelSize += 1.05f;

            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private void iconZoomOut_Click(object sender, EventArgs e)
        {
            buttonSize -= 1;
            panelSize -= 1.05f;
            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }
     
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = Convert.ToDecimal(txtAmountSpent.Text.Trim());
                var summary = txtSummary.Text.Trim();
                DateTime date = dateTimePickerEventExpDate.Value;

                var eventExpenditureData = new EventExpenditure(_eventDTO.EventsId, amount, date, summary);

                if (_eventDTO.EventsId == 0)
                {
                    _eventExpenditureService.Create(eventExpenditureData);
                    MessageBox.Show("Event Expenditure created successfully!");
                }
                else
                {
                    _eventExpenditureService.Update(eventExpenditureData);
                    MessageBox.Show("Event Expenditure updated successfully!");
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
