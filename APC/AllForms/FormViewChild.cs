using APC.BLL;
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
    public partial class FormViewChild : Form
    {
        public FormViewChild()
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
        public ChildDetailDTO detail = new ChildDetailDTO();
        ChildBLL bll = new ChildBLL();
        ChildDTO dto = new ChildDTO();
        public int memberID;
        public bool isParent = false;
        private void FormViewChild_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 21, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 21, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label25.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label26.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label27.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            if (isParent)
            {
                dto = bll.SelectViewParentChild(memberID);
                List<ChildDetailDTO> list = dto.Children;
                foreach (var item in list)
                {
                    string imagePath = Application.StartupPath + "\\images\\" + item.ImagePath;
                    picChild.ImageLocation = imagePath;
                    txtName.Text = item.Name;
                    txtSurname.Text = item.Surname;
                    txtGender.Text = item.GenderName;
                    txtNationality.Text = item.NationalityName;
                    txtBirthday.Text = item.Birthday.ToShortDateString();
                    string fatherImagePath = Application.StartupPath + "\\images\\" + item.FatherImagePath;
                    picFather.ImageLocation = fatherImagePath;
                    txtFathersName.Text = item.FathersName;
                    txtFathersSurname.Text = item.FathersSurname;
                    txtFathersNationality.Text = item.FatherNationalityName;
                    string motherImagePath = Application.StartupPath + "\\images\\" + item.MotherImagePath;
                    picMother.ImageLocation = motherImagePath;
                    txtMothersName.Text = item.MothersName;
                    txtMothersSurname.Text = item.MothersSurname;
                    txtMothersNationality.Text = item.MotherNationalityName;
                    labelTitle.Text = item.Surname + " " + item.Name;
                } 
            }
            else
            {
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picChild.ImageLocation = imagePath;
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtGender.Text = detail.GenderName;
                txtNationality.Text = detail.NationalityName;
                txtBirthday.Text = detail.Birthday.ToShortDateString();
                string fatherImagePath = Application.StartupPath + "\\images\\" + detail.FatherImagePath;
                picFather.ImageLocation = fatherImagePath;
                txtFathersName.Text = detail.FathersName;
                txtFathersSurname.Text = detail.FathersSurname;
                txtFathersNationality.Text = detail.FatherNationalityName;
                string motherImagePath = Application.StartupPath + "\\images\\" + detail.MotherImagePath;
                picMother.ImageLocation = motherImagePath;
                txtMothersName.Text = detail.MothersName;
                txtMothersSurname.Text = detail.MothersSurname;
                txtMothersNationality.Text = detail.MotherNationalityName;
                labelTitle.Text = detail.Surname + " " + detail.Name;
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
