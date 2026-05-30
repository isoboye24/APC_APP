using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewDocument : Form
    {
        private Applications.DTO.DocumentDTO _documentDTO;
        public FormViewDocument()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);
        
        private void FormViewDocument_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void loadForView(Applications.DTO.DocumentDTO documentDTO)
        {
            _documentDTO = documentDTO;
        }

        private void FormViewDocument_Load(object sender, EventArgs e)
        {
            labelWordsCount.Hide();
            this.Text = _documentDTO.DocumentName +" (" + _documentDTO.FormattedDate + ")";
            string filePath = System.Windows.Forms.Application.StartupPath + "\\documents\\" + _documentDTO.DocumentPath;
            if (_documentDTO.DocumentType == "Word Document")
            {
                ReadFiles.ReadWord(filePath, txtDocumentContent);
                string[] words = txtDocumentContent.Text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                labelWordsCount.Text = words.Length.ToString();
                labelWordsCount.Visible = true;
            }

            ControlsFont();
        }

        private void ControlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, label1, btnClose);
            GeneralHelper.ApplyRegularFont(16, txtDocumentContent);
            GeneralHelper.ApplyRegularFont(11, labelWordsCount);
        }
    }
}
