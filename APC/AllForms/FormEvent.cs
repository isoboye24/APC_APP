using APC.BLL;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace APC
{
    public partial class FormEvent : Form
    {
        public FormEvent()
        {
            InitializeComponent();
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
        EventsBLL bll = new EventsBLL();
        public EventsDetailDTO detail = new EventsDetailDTO();
        public bool isUpdate = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim()=="")
            {
                MessageBox.Show("Enter event title");
            }
            else
            {
                if (!isUpdate)
                {
                    EventsDetailDTO events = new EventsDetailDTO();
                    events.EventTitle = txtTitle.Text;
                    events.Summary = txtSummary.Text;
                    events.CoverImagePath = fileName;
                    events.Day = dateTimePickerEvent.Value.Day;
                    events.MonthID = dateTimePickerEvent.Value.Month;
                    events.Year = dateTimePickerEvent.Value.Year.ToString();
                    events.EventDate = dateTimePickerEvent.Value;
                    if (bll.Insert(events))
                    {
                        MessageBox.Show("Event was added");
                        try
                        {
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Cannot find the path to this picture");
                        }
                        txtSummary.Clear();
                        txtTitle.Clear();
                        txtImagePath.Clear();
                        picEventCoverImage.Image = null;
                        dateTimePickerEvent.Value = DateTime.Today;
                    }
                }
                else if (isUpdate)
                {
                    // Check if all values are unchanged (including image)
                    bool imageUnchanged = Path.GetFileName(txtImagePath.Text.Trim()) == detail.CoverImagePath;
                    bool noChanges =
                        detail.EventDate == dateTimePickerEvent.Value &&
                        txtTitle.Text.Trim() == detail.EventTitle &&
                        txtSummary.Text.Trim() == detail.Summary &&
                        imageUnchanged;

                    if (noChanges)
                    {
                        MessageBox.Show("There is no change");
                        return;
                    }

                    string imagesDir = Path.Combine(Application.StartupPath, "images");
                    string newFileName = fileName;

                    if (!imageUnchanged)
                    {
                        string oldImagePath = Path.Combine(imagesDir, detail.CoverImagePath);
                        if (File.Exists(oldImagePath))
                        {
                            File.Delete(oldImagePath);
                        }

                        // Copy new image
                        string newImagePath = Path.Combine(imagesDir, newFileName);
                        File.Copy(txtImagePath.Text, newImagePath, overwrite: true);

                        detail.CoverImagePath = newFileName;
                    }

                    detail.EventDate = dateTimePickerEvent.Value;
                    detail.Day = dateTimePickerEvent.Value.Day;
                    detail.MonthID = dateTimePickerEvent.Value.Month;
                    detail.Year = dateTimePickerEvent.Value.Year.ToString();
                    detail.Summary = txtSummary.Text.Trim();
                    detail.EventTitle = txtTitle.Text.Trim();

                    if (bll.Update(detail))
                    {
                        MessageBox.Show("Event was updated successfully");
                        this.Close();
                    }
                }

            }
        }
        string fileName;
        OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
        private void btnBrowse_Click(object sender, EventArgs e)
        {            
            OpenFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picEventCoverImage.Load(OpenFileDialog1.FileName);
                txtImagePath.Text = OpenFileDialog1.FileName;
                string unique = Guid.NewGuid().ToString();
                fileName += unique + OpenFileDialog1.SafeFileName;
            }
        }

        private void FormEvent_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtSummary.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtTitle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtImagePath.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            dateTimePickerEvent.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnBrowse.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtImagePath.Hide();
            label4.Hide();
            if (isUpdate)
            {
                txtImagePath.Text = detail.CoverImagePath;
                txtSummary.Text = detail.Summary;
                txtTitle.Text = detail.EventTitle;
                labelTitle.Text = "Edit "+ detail.EventTitle;
                dateTimePickerEvent.Value = detail.EventDate;
                string imagePath = Application.StartupPath + "\\images\\" + detail.CoverImagePath;
                picEventCoverImage.ImageLocation = imagePath;
            }
        }        
    }
}
