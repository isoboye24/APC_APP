using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC
{
    public partial class FormEvent : Form
    {
        private readonly IEventsService _eventsService;

        private Applications.DTO.EventDTO _eventDTO;

        private bool _isUpdate = false;
        private string fileName;

        public FormEvent(IEventsService eventsService)
        {
            InitializeComponent();
            _eventsService = eventsService;
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
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loadForEdit(Applications.DTO.EventDTO eventDTO, bool isUpdate)
        {
             _eventDTO = eventDTO;
            _isUpdate = isUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sourcePath = txtImagePath.Text.Trim();
                string title = txtTitle.Text.Trim();
                var summary = txtSummary.Text.Trim();
                DateTime date = dateTimePickerEvent.Value;

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

                    if (_eventDTO.EventsId > 0)
                    {
                        if (File.Exists(_eventDTO.CoverImagePath) &&
                            _eventDTO.CoverImagePath != destinationPath)
                        {
                            File.Delete(_eventDTO.CoverImagePath);
                        }
                    }
                }

                var eventData = new TheEvents(title, summary, finalImagePath, date);

                if (_eventDTO.EventsId == 0)
                {
                    _eventsService.Create(eventData);
                    MessageBox.Show("Event created successfully!");
                }
                else
                {
                    _eventsService.Update(eventData);
                    MessageBox.Show("Event updated successfully!");
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
                picEventCoverImage.Load(OpenFileDialog1.FileName);

                txtImagePath.Text = OpenFileDialog1.FileName;

                string unique = Guid.NewGuid().ToString();

                fileName = unique + "_" + OpenFileDialog1.SafeFileName;
            }
        }

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label3, btnBrowse, btnClose, btnSave);

            GeneralHelper.ApplyRegularFont(14, txtSummary, txtTitle, txtImagePath, dateTimePickerEvent);
        }

        private void FormEvent_Load(object sender, EventArgs e)
        {
          controlsFont();

            txtImagePath.Hide();
            label4.Hide();
            if (_isUpdate)
            {
                txtImagePath.Text = _eventDTO.CoverImagePath;
                txtSummary.Text = _eventDTO.Summary;
                txtTitle.Text = _eventDTO.Title;
                labelTitle.Text = "Edit "+ _eventDTO.Title;
                dateTimePickerEvent.Value = _eventDTO.EventsDate;
                string imagePath = Application.StartupPath + "\\images\\" + _eventDTO.CoverImagePath;
                picEventCoverImage.ImageLocation = imagePath;
            }
        }        
    }
}
