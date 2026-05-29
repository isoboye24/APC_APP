using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using APC.Utility;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormEventReceipt : Form
    {
        private readonly IEventReceiptService _eventReceiptService;

        private EventDTO _eventDTO;
        private EventReceiptDTO _eventReceiptDTO;

        private bool _isUpdate = false;
        private int buttonSize = 14;
        private float panelSize;
        private string fileName;

        public FormEventReceipt(IEventReceiptService eventReceiptService)
        {
            InitializeComponent();
            _eventReceiptService = eventReceiptService;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void tableLayoutPanel7_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public void loadEventData(EventDTO eventDTO)
        {
            _eventDTO = eventDTO;
        }

        public void loadForEdit(EventReceiptDTO eventReceiptDTO, bool isUpdate)
        {
            _eventReceiptDTO = eventReceiptDTO;
            _isUpdate = isUpdate;
        }

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label3, label5, label6, btnBrowse, btnClose, btnSave);

            GeneralHelper.ApplyRegularFont(16, txtImageCaption, txtDescription, txtImagePath, dateTimePickerEventReceiptDate, txtReceiptAmount);
        }

        private void FormEventReceipt_Load(object sender, EventArgs e)
        {
            controlsFont();

            string caption;
            labelTitle.Text = "Add " + _eventDTO.Title + " Receipt.";

            if (_isUpdate)
            {
                caption = _eventReceiptDTO.Caption;
                if (caption.Length > 60)
                {
                    caption = caption.Substring(0, 60) + "...";
                }                    
                labelTitle.Text = "Edit " + _eventReceiptDTO.Caption + " of " + _eventDTO.Title;

                txtImagePath.Text = _eventReceiptDTO.ImagePath;
                txtImageCaption.Text = _eventReceiptDTO.Caption;
                txtDescription.Text = _eventReceiptDTO.Summary;
                dateTimePickerEventReceiptDate.Value = _eventReceiptDTO.ReceiptDate;
                txtReceiptAmount.Text = _eventReceiptDTO.AmountSpent.ToString();

                string imagePath = Application.StartupPath + "\\images\\" + _eventReceiptDTO.ImagePath;
                picEventReceipt.ImageLocation = imagePath;
            }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.Filter =
        "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";

            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picEventReceipt.Load(OpenFileDialog1.FileName);

                txtImagePath.Text = OpenFileDialog1.FileName;

                string unique = Guid.NewGuid().ToString();

                fileName = unique + "_" + OpenFileDialog1.SafeFileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sourcePath = txtImagePath.Text.Trim();
                string caption = txtImageCaption.Text.Trim();
                string description = txtDescription.Text.Trim();
                decimal amount = Convert.ToDecimal(txtReceiptAmount.Text.Trim());
                DateTime date = dateTimePickerEventReceiptDate.Value;

                string finalImagePath = _eventDTO.CoverImagePath;

                // Only copy image if user selected a file
                if (!string.IsNullOrWhiteSpace(sourcePath) && !string.IsNullOrWhiteSpace(fileName))
                {
                    string destinationFolder =
                        Path.Combine(Application.StartupPath, "images");

                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    string destinationPath =
                        Path.Combine(destinationFolder, fileName);

                    File.Copy(sourcePath, destinationPath, true);

                    finalImagePath = destinationPath;

                    if (_eventReceiptDTO.EventReceiptId > 0)
                    {
                        if (File.Exists(_eventReceiptDTO.ImagePath) &&
                            _eventReceiptDTO.ImagePath != destinationPath)
                        {
                            File.Delete(_eventReceiptDTO.ImagePath);
                        }
                    }
                }

                var eventReceiptData = new EventReceipt(_eventDTO.EventsId, finalImagePath, description, caption, date, amount);

                if (_eventDTO.EventsId == 0)
                {
                    _eventReceiptService.Create(eventReceiptData);
                    MessageBox.Show("Event receipt created successfully!");
                }
                else
                {
                    _eventReceiptService.Update(eventReceiptData);
                    MessageBox.Show("Event receipt updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtReceiptAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }
    }
}
