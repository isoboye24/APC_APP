using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.Data.Entity.Validation;
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


        OpenFileDialog openFileDialog1 = new OpenFileDialog();

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sourcePath = txtDocumentPath.Text.Trim();
                string documentName = txtDocumentName.Text.Trim();
                string documentType = "";
                DateTime date = DateTime.Today;


                if (!_isUpdate)
                {
                    documentType = GeneralHelper.GetDocumentType(fileExtension);
                    var documentData = new Document(documentName, fileName, documentType, date);
                    _documentService.Create(documentData);
                    try
                    {
                        File.Copy(txtDocumentPath.Text, @"documents\\" + fileName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Cannot find the path to this document");
                    }
                    MessageBox.Show("Document created successfully!");
                    txtDocumentName.Clear();
                    txtDocumentPath.Clear();
                    picFileImage.Hide();
                }
                else
                {
                    if (_documentDTO.DocumentPath != txtDocumentPath.Text.Trim())
                    {
                        if (File.Exists(@"documents\\" + _documentDTO.DocumentPath))
                        {
                            File.Delete(@"documents\\" + _documentDTO.DocumentPath);
                        }
                        File.Copy(txtDocumentPath.Text, @"documents\\" + fileName);
                        _documentDTO.DocumentPath = fileName;
                        
                    }
                    else
                    {
                        _documentDTO.DocumentPath = _documentDTO.DocumentPath;
                    }

                    fileExtension = Path.GetExtension(_documentDTO.DocumentPath);
                    documentType = GeneralHelper.GetDocumentType(fileExtension);

                    var documentData = Document.Rehydrate(_documentDTO.DocumentId, documentName, _documentDTO.DocumentPath, documentType, date);
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

        
    }
}
