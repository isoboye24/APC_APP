using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewComment : Form
    {
        public FormViewComment()
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
        public CommentDetailDTO detail = new CommentDetailDTO();
        private void FormViewComment_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 21, FontStyle.Bold);
            labelDate.Font = new Font("Segoe UI", 21, FontStyle.Bold);

            txtComment.Font = new Font("Segoe UI", 21, FontStyle.Bold);
            txtName.Font = new Font("Segoe UI", 21, FontStyle.Bold);
            txtSurname.Font = new Font("Segoe UI", 21, FontStyle.Bold);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            txtName.Text = detail.Name;
            txtSurname.Text = detail.Surname;
            txtComment.Text = detail.CommentName;
            string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
            picProfilePic.ImageLocation = imagePath;
            labelDate.Text = detail.Day.ToString() + "/" + detail.MonthID.ToString() + "/" + detail.Year;
            labelTitle.Text = detail.Surname + " " + detail.Name + "'s comment";
        }        
    }
}
