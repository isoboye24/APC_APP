using APC.Applications.DTO;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewComment : Form
    {
        private string _firstName;
        private string _lastName;
        private string _imagePath;
        private string _content;
        private string _formattedDate;

        public FormViewComment(CommentDTO dto)
        {
            InitializeComponent();

            _firstName = dto.FirstName;
            _lastName = dto.LastName;
            _imagePath = dto.ImagePath;
            _content = dto.Content;
            _formattedDate = dto.FormattedDate;
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

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label3, label5, labelTitle, btnClose);
            GeneralHelper.ApplyBoldFont(21, label6, labelDate, txtComment, txtName, txtSurname);
        }
        
        private void FormViewComment_Load(object sender, EventArgs e)
        {
            resizeControls();

            txtName.Text = _firstName;
            txtSurname.Text = _lastName;
            txtComment.Text = _content;
            string imagePath = Application.StartupPath + "\\images\\" + _imagePath;
            picProfilePic.ImageLocation = imagePath;
            labelDate.Text = _formattedDate;
            labelTitle.Text = _firstName + " " + _lastName + "'s comment";
        }        
    }
}
