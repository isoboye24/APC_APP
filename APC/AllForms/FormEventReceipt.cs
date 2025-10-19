using APC.BLL;
using APC.DAL;
using APC.DAL.DTO;
using APC.Utility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.IO.RecyclableMemoryStreamManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormEventReceipt : Form
    {
        public FormEventReceipt()
        {
            InitializeComponent();
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

        private int buttonSize = 14;
        private float panelSize;
        public bool isUpdate = false;

        public EventsDetailDTO detail = new EventsDetailDTO();
        public EventReceiptsDetailDTO detailReceipt = new EventReceiptsDetailDTO();
        EventReceiptsBLL bll = new EventReceiptsBLL();

        private void FormEventReceipt_Load(object sender, EventArgs e)
        {
            string caption;
            labelTitle.Text = labelTitle.Text = "Add " + detail.EventTitle + " (" + detail.Year + ") Receipt.";

            if (isUpdate)
            {
                caption = detailReceipt.ImageCaption;
                if (caption.Length > 60)
                {
                    caption = caption.Substring(0, 60) + "...";
                }                    
                labelTitle.Text = "Edit " + detailReceipt.ImageCaption;

                txtImagePath.Text = detailReceipt.ImagePath;
                txtImageCaption.Text = detailReceipt.ImageCaption;
                txtDescription.Text = detailReceipt.Summary;
                dateTimePickerEventReceiptDate.Value = detailReceipt.ReceiptDate;
                txtReceiptAmount.Text = detailReceipt.AmountSpent.ToString();

                string imagePath = Application.StartupPath + "\\images\\" + detailReceipt.ImagePath;
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

        string fileName;
        System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picEventReceipt.Load(OpenFileDialog1.FileName);
                txtImagePath.Text = OpenFileDialog1.FileName;

                // Generate a new unique filename
                string unique = Guid.NewGuid().ToString();
                fileName = unique + "_" + OpenFileDialog1.SafeFileName;
            }
        }

        int maxLength = 50;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (detail.EventID == 0)
            {
                MessageBox.Show("Please choose event");
            }
            else if (txtImagePath.Text.Trim() == "")
            {
                MessageBox.Show("Please upload an image");
            }
            else if (txtImageCaption.Text.Length > maxLength)
            {
                MessageBox.Show("The caption is too long!");
            }
            else if (txtImageCaption.Text.Trim() == "")
            {
                MessageBox.Show("Please enter image caption");
            }
            else if (txtReceiptAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please enter amount on the receipt");
            }
            else
            {
                string imagesDir;

                // If in Debug or writable folder, use app directory
                if (Directory.Exists(Application.StartupPath) &&
                   General.IsDirectoryWritable(Application.StartupPath))
                {
                    imagesDir = Path.Combine(Application.StartupPath, "images");
                }
                else
                {
                    // Otherwise, use AppData folder
                    imagesDir = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "APC",
                        "images"
                    );
                }

                // Ensure folder exists
                Directory.CreateDirectory(imagesDir);

                if (!isUpdate)
                {
                    EventReceiptsDetailDTO eventReceipt = new EventReceiptsDetailDTO();
                    eventReceipt.EventID = detail.EventID;
                    eventReceipt.Summary = txtDescription.Text.Trim();
                    eventReceipt.ImageCaption = txtImageCaption.Text.Trim();
                    eventReceipt.AmountSpent = Convert.ToDecimal(txtReceiptAmount.Text.Trim());
                    eventReceipt.ImagePath = fileName;
                    eventReceipt.ReceiptDate = dateTimePickerEventReceiptDate.Value;
                    eventReceipt.Day = dateTimePickerEventReceiptDate.Value.Day;
                    eventReceipt.MonthID = dateTimePickerEventReceiptDate.Value.Month;
                    eventReceipt.Year = dateTimePickerEventReceiptDate.Value.Year;
                    if (bll.Insert(eventReceipt))
                    {
                        string destination = Path.Combine(imagesDir, fileName);
                        File.Copy(txtImagePath.Text, destination, true);

                        MessageBox.Show("Event image was added");

                        txtImageCaption.Clear();
                        txtImagePath.Clear();
                        txtDescription.Clear();
                        picEventReceipt.Image = null;
                    }
                }
                else if (isUpdate)
                {
                    bool imageUnchanged = Path.GetFileName(txtImagePath.Text.Trim()) == detailReceipt.ImagePath;
                    if (detailReceipt.ImageCaption == txtImageCaption.Text.Trim() && detailReceipt.Summary == txtDescription.Text.Trim()
                        && imageUnchanged && detailReceipt.AmountSpent == Convert.ToDecimal(txtReceiptAmount.Text.Trim()))
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        string newFileName = fileName;
                        if (!imageUnchanged)
                        {
                            string oldImagePath = Path.Combine(imagesDir, detailReceipt.ImagePath);
                            if (File.Exists(oldImagePath))
                                File.Delete(oldImagePath);

                            string newImagePath = Path.Combine(imagesDir, newFileName);
                            File.Copy(txtImagePath.Text, newImagePath, true);

                            detailReceipt.ImagePath = newFileName;
                        }

                        detailReceipt.ImagePath = txtImagePath.Text;
                        detailReceipt.ImageCaption = txtImageCaption.Text;
                        detailReceipt.Summary = txtDescription.Text;
                        detailReceipt.EventID = detail.EventID;
                        detailReceipt.AmountSpent = Convert.ToDecimal(txtReceiptAmount.Text.Trim());
                        detailReceipt.ReceiptDate = dateTimePickerEventReceiptDate.Value;
                        detailReceipt.Day = dateTimePickerEventReceiptDate.Value.Day;
                        detailReceipt.MonthID = dateTimePickerEventReceiptDate.Value.Month;
                        detailReceipt.Year = dateTimePickerEventReceiptDate.Value.Year;
                        if (bll.Update(detailReceipt))
                        {
                            MessageBox.Show("Event Receipt was updated");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void txtReceiptAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e, (TextBox)sender);
        }
    }
}
