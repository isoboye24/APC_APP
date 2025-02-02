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

namespace APC.AllForms
{
    public partial class FormEventSingleImage : Form
    {
        public FormEventSingleImage()
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int eventID;
        EventImageBLL bll = new EventImageBLL();
        public EventImageDetailDTO detail = new EventImageDetailDTO();
        public bool isUdate = false;
        public bool isView = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (eventID == 0)
            {
                MessageBox.Show("Please choose event");
            }
            else if (txtImagePath.Text.Trim()=="")
            {
                MessageBox.Show("Please upload an image");
            }
            else if (txtImageCaption.Text.Length > maxLength)
            {
                MessageBox.Show("The caption is too long!");
            }
            else
            {
                if (!isUdate)
                {
                    EventImageDetailDTO eventImage = new EventImageDetailDTO();
                    eventImage.EventID = eventID;
                    eventImage.Summary = txtImageSummary.Text.Trim();
                    eventImage.ImageCaption = txtImageCaption.Text.Trim();
                    eventImage.ImagePath = fileName;
                    if (bll.Insert(eventImage))
                    {
                        MessageBox.Show("Event image was added");
                        try
                        {
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Cannot find the path to this picture");
                        }
                        txtImageCaption.Clear();
                        txtImagePath.Clear();
                        txtImageSummary.Clear();
                        picEventImage.Image = null;
                    }
                }
                else if (isUdate)
                {
                    if (detail.ImageCaption == txtImageCaption.Text.Trim() && detail.Summary == txtImageSummary.Text.Trim() 
                        && detail.ImagePath == txtImagePath.Text)
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        detail.ImagePath = txtImagePath.Text;
                        detail.ImageCaption = txtImageCaption.Text;
                        detail.Summary = txtImageSummary.Text;
                        detail.EventID = eventID;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Event image was updated");
                            this.Close();
                        }
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
                picEventImage.Load(OpenFileDialog1.FileName);
                txtImagePath.Text = OpenFileDialog1.FileName;
                string unique = Guid.NewGuid().ToString();
                fileName += unique + OpenFileDialog1.SafeFileName;
            }
        }

        private void FormEventSingleImage_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtImagePath.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtImageCaption.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtImageSummary.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnBrowse.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtImagePath.Hide();
            label3.Hide();
            if (isUdate)
            {
                txtImagePath.Text = detail.ImagePath;
                txtImageCaption.Text = detail.ImageCaption;
                txtImageSummary.Text = detail.Summary;
                eventID = detail.EventID;
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picEventImage.ImageLocation = imagePath;
                labelTitle.Text = detail.EventTitle + " (" + detail.EventYear + ")";
            }
            else if (isView)
            {
                txtImagePath.Text = detail.ImagePath;
                txtImagePath.Hide();
                label3.Hide();

                txtImageCaption.Text = detail.ImageCaption;
                txtImageCaption.ReadOnly = true;
                txtImageCaption.BorderStyle = BorderStyle.None;
                tableLayoutPanel5.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanel5.RowStyles[1].Height = 70;
                

                txtImageSummary.Text = detail.Summary;
                txtImageSummary.ReadOnly = true;
                txtImageSummary.BorderStyle = BorderStyle.None;
                eventID = detail.EventID;
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picEventImage.ImageLocation = imagePath;
                labelTitle.Text = detail.EventTitle + " (" + detail.EventYear + ")";
                btnSave.Hide();
                btnBrowse.Hide();
                label1.Hide();
            }
        }
        int maxLength = 50;
        private void txtImageCaption_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
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
            }

        }
    }
}
