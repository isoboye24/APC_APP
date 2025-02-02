using APC.BLL;
using APC.DAL.DAO;
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

namespace APC
{
    public partial class FormChildren : Form
    {
        public FormChildren()
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
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void FormChildren_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        ChildBLL bll = new ChildBLL();
        ChildDTO dto = new ChildDTO();
        public ChildDetailDTO detail = new ChildDetailDTO();
        public bool isUpdate = false;
        FathersDetailDTO fatherDetail = new FathersDetailDTO();
        MothersDetailDTO motherDetail = new MothersDetailDTO();

        private void FontSizes()
        {
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label11.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label12.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label13.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label25.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label26.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label27.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtFathersName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtFatherSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtImagePath.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtMothersName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtMothersSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSearchFather.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSearchMother.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            cmbGender.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbNationality.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            dateTimePickerBirthday.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnBrowse.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            iconBtnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            iconBtnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }
        private void FormChildren_Load(object sender, EventArgs e)
        {
            FontSizes();

            int minWidthPercentage = 30;
            int minHeightPercentage = 30;
            int minWidth = Screen.PrimaryScreen.Bounds.Width * minWidthPercentage / 100;
            int minHeight = Screen.PrimaryScreen.Bounds.Height * minHeightPercentage / 100;
            this.MinimumSize = new Size(minWidth, minHeight);

            dto = bll.Select();

            cmbGender.DataSource = dto.Genders;
            cmbNationality.DataSource = dto.Nationalities;
            General.ComboBoxProps(cmbGender, "GenderName", "GenderID");
            General.ComboBoxProps(cmbNationality, "Nationality", "NationalityID");
            txtImagePath.Hide();
            label3.Hide();

            #region
            dataGridViewFathers.DataSource = dto.Fathers;
            dataGridViewFathers.Columns[0].Visible = false;
            dataGridViewFathers.Columns[1].Visible = false;
            dataGridViewFathers.Columns[2].Visible = false;
            dataGridViewFathers.Columns[3].HeaderText = "Surname";
            dataGridViewFathers.Columns[4].HeaderText = "Name";
            dataGridViewFathers.Columns[5].Visible = false;
            dataGridViewFathers.Columns[6].Visible = false;
            dataGridViewFathers.Columns[7].Visible = false;
            dataGridViewFathers.Columns[8].Visible = false;
            dataGridViewFathers.Columns[9].Visible = false;
            dataGridViewFathers.Columns[10].Visible = false;
            dataGridViewFathers.Columns[11].Visible = false;
            dataGridViewFathers.Columns[12].Visible = false;
            dataGridViewFathers.Columns[13].HeaderText = "Nationality";
            dataGridViewFathers.Columns[14].Visible = false;
            dataGridViewFathers.Columns[15].Visible = false;
            dataGridViewFathers.Columns[16].Visible = false;
            dataGridViewFathers.Columns[17].Visible = false;
            dataGridViewFathers.Columns[18].Visible = false;
            dataGridViewFathers.Columns[19].HeaderText = "Gender";
            dataGridViewFathers.Columns[20].Visible = false;
            dataGridViewFathers.Columns[21].Visible = false;
            dataGridViewFathers.Columns[22].Visible = false;
            dataGridViewFathers.Columns[23].Visible = false;
            dataGridViewFathers.Columns[24].Visible = false;
            dataGridViewFathers.Columns[25].Visible = false;
            dataGridViewFathers.Columns[26].Visible = false;
            dataGridViewFathers.Columns[27].Visible = false;
            dataGridViewFathers.Columns[28].Visible = false;
            dataGridViewFathers.Columns[29].Visible = false;
            dataGridViewFathers.Columns[30].Visible = false;
            dataGridViewFathers.Columns[31].Visible = false;
            dataGridViewFathers.Columns[32].Visible = false;
            dataGridViewFathers.Columns[33].Visible = false;
            dataGridViewFathers.Columns[34].Visible = false;
            dataGridViewFathers.Columns[35].Visible = false;
            dataGridViewFathers.Columns[36].Visible = false;
            dataGridViewFathers.Columns[37].Visible = false;
            dataGridViewFathers.Columns[38].Visible = false;
            dataGridViewFathers.Columns[39].Visible = false;
            dataGridViewFathers.Columns[40].Visible = false;
            dataGridViewFathers.Columns[41].Visible = false;
            dataGridViewFathers.Columns[42].Visible = false;
            foreach (DataGridViewColumn column in dataGridViewFathers.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }

            dataGridViewMothers.DataSource = dto.Mothers;
            dataGridViewMothers.Columns[0].Visible = false;
            dataGridViewMothers.Columns[1].Visible = false;
            dataGridViewMothers.Columns[2].Visible = false;
            dataGridViewMothers.Columns[3].HeaderText = "Surname";
            dataGridViewMothers.Columns[4].HeaderText = "Name";
            dataGridViewMothers.Columns[5].Visible = false;
            dataGridViewMothers.Columns[6].Visible = false;
            dataGridViewMothers.Columns[7].Visible = false;
            dataGridViewMothers.Columns[8].Visible = false;
            dataGridViewMothers.Columns[9].Visible = false;
            dataGridViewMothers.Columns[10].Visible = false;
            dataGridViewMothers.Columns[11].Visible = false;
            dataGridViewMothers.Columns[12].Visible = false;
            dataGridViewMothers.Columns[13].HeaderText = "Nationality";
            dataGridViewMothers.Columns[14].Visible = false;
            dataGridViewMothers.Columns[15].Visible = false;
            dataGridViewMothers.Columns[16].Visible = false;
            dataGridViewMothers.Columns[17].Visible = false;
            dataGridViewMothers.Columns[18].Visible = false;
            dataGridViewMothers.Columns[19].HeaderText = "Gender";
            dataGridViewMothers.Columns[20].Visible = false;
            dataGridViewMothers.Columns[21].Visible = false;
            dataGridViewMothers.Columns[22].Visible = false;
            dataGridViewMothers.Columns[23].Visible = false;
            dataGridViewMothers.Columns[24].Visible = false;
            dataGridViewMothers.Columns[25].Visible = false;
            dataGridViewMothers.Columns[26].Visible = false;
            dataGridViewMothers.Columns[27].Visible = false;
            dataGridViewMothers.Columns[28].Visible = false;
            dataGridViewMothers.Columns[29].Visible = false;
            dataGridViewMothers.Columns[30].Visible = false;
            dataGridViewMothers.Columns[31].Visible = false;
            dataGridViewMothers.Columns[32].Visible = false;
            dataGridViewMothers.Columns[33].Visible = false;
            dataGridViewMothers.Columns[34].Visible = false;
            dataGridViewMothers.Columns[35].Visible = false;
            dataGridViewMothers.Columns[36].Visible = false;
            dataGridViewMothers.Columns[37].Visible = false;
            dataGridViewMothers.Columns[38].Visible = false;
            dataGridViewMothers.Columns[39].Visible = false;
            dataGridViewMothers.Columns[40].Visible = false;
            dataGridViewMothers.Columns[41].Visible = false;
            dataGridViewMothers.Columns[42].Visible = false;
            foreach (DataGridViewColumn column in dataGridViewMothers.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
            #endregion

            if (isUpdate)
            {
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfilePic.ImageLocation = imagePath;
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                dateTimePickerBirthday.Value = Convert.ToDateTime(detail.Birthday);
                cmbGender.SelectedValue = detail.GenderID;
                cmbNationality.SelectedValue = detail.NationalityID;
                txtImagePath.Text = detail.ImagePath;
                txtMothersName.Text = detail.MothersName;
                txtMothersSurname.Text = detail.MothersSurname;
                txtFathersName.Text = detail.FathersName;
                txtFatherSurname.Text = detail.FathersSurname;
                motherDetail.MemberID = detail.MotherID;
                fatherDetail.MemberID = detail.FatherID;
                labelTitle.Text = "Edit Child";
            }
            else if (!isUpdate)
            {
                labelTitle.Text = "Add Child";
            }
        }

        private void dataGridViewMothers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            motherDetail = new MothersDetailDTO();
            motherDetail.MemberID = Convert.ToInt32(dataGridViewMothers.Rows[e.RowIndex].Cells[0].Value);
            txtMothersName.Text = dataGridViewMothers.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtMothersSurname.Text = dataGridViewMothers.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dataGridViewFathers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            fatherDetail = new FathersDetailDTO();
            fatherDetail.MemberID = Convert.ToInt32(dataGridViewFathers.Rows[e.RowIndex].Cells[0].Value);
            txtFathersName.Text = dataGridViewFathers.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtFatherSurname.Text = dataGridViewFathers.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void txtSearchMother_TextChanged(object sender, EventArgs e)
        {
            List<MothersDetailDTO> list = dto.Mothers;
            list = list.Where(x => x.Surname.Contains(txtSearchMother.Text)).ToList();
            dataGridViewMothers.DataSource = list;
        }

        private void txtSearchFather_TextChanged(object sender, EventArgs e)
        {
            List<FathersDetailDTO> list = dto.Fathers;
            list = list.Where(x => x.Surname.Contains(txtSearchFather.Text)).ToList();
            dataGridViewFathers.DataSource = list;
        }

        string fileName;
        OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picProfilePic.Load(OpenFileDialog1.FileName);
                txtImagePath.Text = OpenFileDialog1.FileName;
                string unique = Guid.NewGuid().ToString();
                fileName += unique + OpenFileDialog1.SafeFileName;
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

        private void iconBtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconBtnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter child's name");
            }
            else if (txtSurname.Text.Trim() == "")
            {
                MessageBox.Show("Please enter child's surname");
            }
            else if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select child's gender");
            }
            else if (cmbNationality.SelectedIndex == -1)
            {
                MessageBox.Show("Please select child's nationality");
            }
            else
            {
                if (!isUpdate)
                {
                    ChildDetailDTO child = new ChildDetailDTO();
                    child.Name = txtName.Text;
                    child.Surname = txtSurname.Text;
                    child.GenderID = Convert.ToInt32(cmbGender.SelectedValue);
                    child.NationalityID = Convert.ToInt32(cmbNationality.SelectedValue);
                    child.Birthday = dateTimePickerBirthday.Value;
                    child.ImagePath = fileName;
                    child.FatherID = fatherDetail.MemberID;
                    child.MotherID = motherDetail.MemberID;
                    if (bll.Insert(child))
                    {
                        MessageBox.Show("Child was added");
                        try
                        {
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Cannot find the path to this picture");
                        }
                        txtName.Clear();
                        txtSurname.Clear();
                        cmbGender.SelectedIndex = -1;
                        cmbNationality.SelectedIndex = -1;
                        txtImagePath.Clear();
                        dateTimePickerBirthday.Value = DateTime.Today;
                        picProfilePic.Image = null;
                    }
                }
                else if (isUpdate)
                {
                    if (detail.ImagePath == txtImagePath.Text.Trim() && detail.Name == txtName.Text.Trim() &&
                        detail.Surname == txtSurname.Text.Trim() && detail.Birthday == dateTimePickerBirthday.Value &&
                        detail.GenderID == cmbGender.SelectedIndex && detail.NationalityID == cmbNationality.SelectedIndex &&
                        detail.MotherID == motherDetail.MemberID && detail.FatherID == fatherDetail.MemberID)
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        if (txtImagePath.Text != detail.ImagePath)
                        {
                            if (File.Exists(@"images\\" + detail.ImagePath))
                            {
                                File.Delete(@"images\\" + detail.ImagePath);
                            }
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                            detail.ImagePath = fileName;
                        }
                        else if (txtImagePath.Text == detail.ImagePath)
                        {
                            detail.ImagePath = txtImagePath.Text;
                        }
                        detail.ImagePath = txtImagePath.Text.Trim();
                        detail.Name = txtName.Text.Trim();
                        detail.Surname = txtSurname.Text.Trim();
                        detail.Birthday = dateTimePickerBirthday.Value;
                        detail.GenderID = Convert.ToInt32(cmbGender.SelectedValue);
                        detail.NationalityID = Convert.ToInt32(cmbNationality.SelectedValue);
                        detail.MotherID = motherDetail.MemberID;
                        detail.FatherID = fatherDetail.MemberID;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Child was updated");
                            this.Close();
                        }
                    }
                }
            }
        }
    }
}
