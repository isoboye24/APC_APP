using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormDocument : Form
    {
        private readonly IDocumentService _documentService;

        private Applications.DTO.DocumentDTO _documentDTO;

        private bool _isUpdate = false;
        private string fileName;
        private string fileExtension;

        public FormDocument(IDocumentService documentService)
        {
            InitializeComponent();
            _documentService = documentService;
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

        public void loadForEdit(Applications.DTO.DocumentDTO documentDTO, bool isUpdate)
        {
            _documentDTO = documentDTO;
            _isUpdate = isUpdate;
        }


        System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Word Documents|*.docx|Excel Spreadsheets|*.xlsx|Text Files|*.txt|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDocumentPath.Text = openFileDialog1.FileName;
                fileExtension = Path.GetExtension(openFileDialog1.FileName);

                fileName = $"{Guid.NewGuid()}_{openFileDialog1.SafeFileName}";

                picFileImage.Visible = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sourcePath = txtDocumentPath.Text.Trim();
                string documentName = txtDocumentName.Text.Trim();
                string documentType = GeneralHelper.GetDocumentType(fileExtension);
                DateTime date = DateTime.Today;

                string finalDocPath = _documentDTO.DocumentPath;

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

                    finalDocPath = destinationPath;

                    if (_documentDTO.DocumentId > 0)
                    {
                        if (File.Exists(_documentDTO.DocumentPath) &&
                            _documentDTO.DocumentPath != destinationPath)
                        {
                            File.Delete(_documentDTO.DocumentPath);
                        }
                    }
                }

                var documentData = new Document(documentName, finalDocPath, documentType, date);

                if (_documentDTO.DocumentId == 0)
                {
                    _documentService.Create(documentData);
                    MessageBox.Show("Document created successfully!");
                }
                else
                {
                    _documentService.Update(documentData);
                    MessageBox.Show("Document updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ControlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, btnClose, btnSave, btnBrowse);
            GeneralHelper.ApplyRegularFont(14, txtDocumentPath);
            GeneralHelper.ApplyRegularFont(16, txtDocumentName);
        }

        private void FormDocument_Load(object sender, EventArgs e)
        {
            ControlsFont();

            picFileImage.Hide();
            if (_isUpdate)
            {
                labelTitle.Text = "Edit " + _documentDTO.DocumentName;
                picFileImage.Visible = true;
                txtDocumentName.Text = _documentDTO.DocumentName;
                txtDocumentPath.Text = _documentDTO.DocumentPath;
            }
            else
            {
                labelTitle.Text = "Add Document";
            }
        }       
    }
}
