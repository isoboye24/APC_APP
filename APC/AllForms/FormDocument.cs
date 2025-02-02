using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using APC.DAL.DTO;
using APC.BLL;
using APC.DAL;
using System.Runtime.InteropServices;

namespace APC.AllForms
{
    public partial class FormDocument : Form
    {
        public FormDocument()
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
        public DocumentDetailDTO detail = new DocumentDetailDTO();
        public bool isUpdate = false;
        System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        string fileName;
        string fileExtension;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Word Documents|*.docx|Excel Spreadsheets|*.xlsx|Text Files|*.txt|All Files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDocumentPath.Text = openFileDialog1.FileName;
                fileExtension = Path.GetExtension(txtDocumentPath.Text);
                string unique = Guid.NewGuid().ToString();
                fileName += unique + openFileDialog1.SafeFileName;
                picFileImage.Visible = true;
            }
        }
        DocumentBLL bll = new DocumentBLL();
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDocumentName.Text.Trim() == "")
            {
                MessageBox.Show("Enter document name");
            }
            else if (txtDocumentPath.Text.Trim() == "")
            {
                MessageBox.Show("Select document from this device");
            }
            else if (fileExtension == "")
            {
                MessageBox.Show("Get the file extension");
            }
            else
            {
                if (!isUpdate)
                {
                    DocumentDetailDTO document = new DocumentDetailDTO();
                    document.DocumentName = txtDocumentName.Text;
                    document.DocumentPath = fileName;
                    document.DocumentType = General.GetDocumentType(fileExtension);
                    document.Day = DateTime.Today.Day;
                    document.MonthID = DateTime.Today.Month;
                    document.Year = DateTime.Today.Year.ToString();
                    if (bll.Insert(document))
                    {
                        try
                        {
                            File.Copy(txtDocumentPath.Text, @"documents\\" + fileName);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Cannot find the path to this document");
                        }
                        MessageBox.Show("Document was saved");
                        txtDocumentName.Clear();
                        txtDocumentPath.Clear();
                        picFileImage.Hide();
                    }
                }
                else if (isUpdate)
                {
                    if (txtDocumentPath.Text.Trim() == detail.DocumentPath && txtDocumentName.Text.Trim()==detail.DocumentName)
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        if (detail.DocumentPath != txtDocumentPath.Text.Trim())
                        {
                            if (File.Exists(@"documents\\" + detail.DocumentPath))
                            {
                                File.Delete(@"documents\\" + detail.DocumentPath);
                            }
                            File.Copy(txtDocumentPath.Text, @"documents\\" + fileName);
                            detail.DocumentPath = fileName;
                            fileExtension = Path.GetExtension(detail.DocumentPath);
                            detail.DocumentType = General.GetDocumentType(fileExtension);
                        }
                        else if (txtDocumentPath.Text == detail.DocumentPath)
                        {
                            detail.DocumentPath = txtDocumentPath.Text;
                            detail.DocumentType = detail.DocumentType;
                        }
                        detail.DocumentName = txtDocumentName.Text;                        
                        detail.Day = DateTime.Today.Day;
                        detail.MonthID = DateTime.Today.Month;
                        detail.Year = DateTime.Today.Year.ToString();
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Document was updated");
                            this.Close();
                        }
                    }
                }
            }
        }
        private void FormDocument_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtDocumentName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtDocumentPath.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            picFileImage.Hide();
            if (isUpdate)
            {
                labelTitle.Text = "Edit Document";
                picFileImage.Visible = true;
                txtDocumentName.Text = detail.DocumentName;
                txtDocumentPath.Text = detail.DocumentPath;
            }
            else
            {
                labelTitle.Text = "Add Document";
            }
        }       
    }
}
