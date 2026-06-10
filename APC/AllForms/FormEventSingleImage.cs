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
    public partial class FormEventSingleImage : Form
    {
        private readonly IEventImagesService _eventImagesService;

        private EventDTO _eventDTO;
        private Applications.DTO.EventImageDTO _eventImageDTO;

        private bool _isUpdate = false;
        private string fileName = "";
        private int buttonSize = 14;
        private float panelSize;
        int maxLength = 50;

        public FormEventSingleImage(IEventImagesService eventImagesService)
        {
            InitializeComponent();
            _eventImagesService = eventImagesService;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);
        
        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var sourcePath = txtImagePath.Text.Trim();
                var summary = txtImageSummary.Text.Trim();
                var caption = txtImageCaption.Text.Trim();

                string finalImagePath = _eventImageDTO.ImagePath;

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

                    if (_eventImageDTO.EventImageId > 0)
                    {
                        if (File.Exists(_eventImageDTO.ImagePath) &&
                            _eventImageDTO.ImagePath != destinationPath)
                        {
                            File.Delete(_eventImageDTO.ImagePath);
                        }
                    }
                }

                

                if (!_isUpdate)
                {
                    var eventImageData = new EventImages(
                    _eventDTO.EventsId,
                    summary,
                    finalImagePath,
                    caption
                );
                    _eventImagesService.Create(eventImageData);

                    MessageBox.Show("Event Image created successfully!");

                    ClearFields();
                }
                else
                {
                    var eventImageData = EventImages.Rehydrate(_eventImageDTO.EventImageId, 
                    _eventDTO.EventsId,
                    summary,
                    finalImagePath,
                    caption
                );
                    _eventImagesService.Update(eventImageData);

                    MessageBox.Show("Event Image updated successfully!");

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.Filter =
        "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";

            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picEventImage.Load(OpenFileDialog1.FileName);

                txtImagePath.Text = OpenFileDialog1.FileName;

                string unique = Guid.NewGuid().ToString();

                fileName = unique + "_" + OpenFileDialog1.SafeFileName;
            }
        }

        public void loadEventData(EventDTO eventDTO)
        {
            _eventDTO = eventDTO;
        }

        public void loadForEdit(Applications.DTO.EventImageDTO eventImageDTO, bool isUpdate)
        {
            _eventImageDTO = eventImageDTO;
            _isUpdate = isUpdate;
        }

        private void ControlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label3, btnClose, btnSave, btnBrowse);
            GeneralHelper.ApplyRegularFont(16, txtImagePath, txtImageCaption, txtImageSummary);
        }

        private void ClearFields()
        {
            txtImageCaption.Clear();
            txtImagePath.Clear();
            txtImageSummary.Clear();
            picEventImage.Image = null;
        }

        private void FormEventSingleImage_Load(object sender, EventArgs e)
        {
            ControlsFont();

            txtImagePath.Hide();
            label3.Hide();

            labelTitle.Text = labelTitle.Text = "Add " + _eventDTO.Title + " Picture.";
            if (_isUpdate)
            {
                txtImagePath.Text = _eventImageDTO.ImagePath;
                txtImageCaption.Text = _eventImageDTO.ImageCaption;
                txtImageSummary.Text = _eventImageDTO.Summary;

                string imagePath = Application.StartupPath + "\\images\\" + _eventImageDTO.ImagePath;
                picEventImage.ImageLocation = imagePath;

                labelTitle.Text = "Edit " + _eventImageDTO.ImageCaption + " of " + _eventDTO.Title;
            }
            
        }
        
        private void txtImageCaption_TextChanged(object sender, EventArgs e)
        {

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
    }
}
