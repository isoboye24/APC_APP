using APC.BLL;
using APC.DAL;
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
using System.Xml.Linq;

namespace APC
{
    public partial class FormMembers : Form
    {
        public FormMembers()
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
        private void FormMembers_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        MemberBLL bll = new MemberBLL();
        MemberDTO dto = new MemberDTO();
        public MemberDetailDTO detail = new MemberDetailDTO();
        public bool isUpdate = false;
        public bool isUpdateDeadMember = false;
        private void FormMembers_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label11.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label12.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label14.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label15.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label19.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label25.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label26.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label27.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelAccessLevel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelDeceasedDate.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelMorePhone.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelPhone2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelPhone3.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtAddress.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtEmail.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtImagePath.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtLGA.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtNameOfNextOfKin.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPhone1.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPhone2.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtPhone3.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            cmbCountry.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbEmpStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbGender.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbMaritalStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbMembershipStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbNationality.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbPermission.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbPosition.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbProfession.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbRelationshipToNextOfKin.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            dateTimePickerBirthday.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            dateTimePickerDeceasedDate.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            dateTimePickerMemSince.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnBrowse.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            #region
            dto = bll.Select();
            cmbCountry.DataSource = dto.Countries;
            GeneralHelper.ComboBoxProps(cmbCountry, "CountryName", "countryID");
            cmbGender.DataSource = dto.Genders;
            GeneralHelper.ComboBoxProps(cmbGender, "GenderName", "genderID");
            cmbProfession.DataSource = dto.Professions;
            GeneralHelper.ComboBoxProps(cmbProfession, "Profession", "professionID");
            cmbPosition.DataSource = dto.Positions;
            GeneralHelper.ComboBoxProps(cmbPosition, "PositionName", "positionID");
            cmbMaritalStatus.DataSource = dto.MaritalStatuses;
            GeneralHelper.ComboBoxProps(cmbMaritalStatus, "MaritalStatus", "MaritalStatusID");
            cmbEmpStatus.DataSource = dto.EmploymentStatuses;
            GeneralHelper.ComboBoxProps(cmbEmpStatus, "EmploymentStatus", "EmploymentStatusID");
            cmbNationality.DataSource = dto.Nationalities;
            GeneralHelper.ComboBoxProps(cmbNationality, "Nationality", "NationalityID");
            cmbPermission.DataSource = dto.Permissions;
            GeneralHelper.ComboBoxProps(cmbPermission, "Permission", "PermissionID");
            cmbMembershipStatus.DataSource = dto.MembershipStatuses;
            GeneralHelper.ComboBoxProps(cmbMembershipStatus, "MembershipStatus", "MembershipStatusID");
            cmbRelationshipToNextOfKin.DataSource = dto.RelationshipsToNextOfKin;
            GeneralHelper.ComboBoxProps(cmbRelationshipToNextOfKin, "Relationship", "RelationshipToKinID");
            #endregion

            txtPhone2.Hide();
            txtPhone3.Hide();
            labelPhone2.Hide();
            labelPhone3.Hide();
            txtImagePath.Hide();
            tableLayoutPanelDeceasedDate.Hide();
            labelTitle.Text = "Add Member";
            if (isUpdateDeadMember)
            {
                tableLayoutPanelDeceasedDate.Visible = true;
                dateTimePickerDeceasedDate.Value = detail.DeadDate;
            }
            if (isUpdate)
            {
                labelTitle.Text = "Edit Member";
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtAddress.Text = detail.HouseAddress;
                cmbPosition.SelectedValue = detail.PositionID;
                dateTimePickerBirthday.Value = Convert.ToDateTime(detail.Birthday);
                dateTimePickerMemSince.Value = Convert.ToDateTime(detail.MembershipDate);
                txtEmail.Text = detail.EmailAddress;
                txtLGA.Text = detail.LGA;
                txtImagePath.Text = detail.ImagePath;                
                txtPhone1.Text = detail.PhoneNumber;
                if (detail.PhoneNumber2 != "")
                {
                    txtPhone2.Visible = true;
                    labelPhone2.Visible = true;
                }
                if (detail.PhoneNumber3 != "")
                {
                    txtPhone3.Visible = true;
                    labelPhone3.Visible = true;
                }
                txtPhone2.Text = detail.PhoneNumber2;
                txtPhone3.Text = detail.PhoneNumber3;
                txtNameOfNextOfKin.Text = detail.NameOfNextOfKin;
                cmbCountry.SelectedValue = detail.CountryID;
                cmbProfession.SelectedValue = detail.ProfessionID;
                cmbEmpStatus.SelectedValue = detail.EmploymentStatusID;
                cmbGender.SelectedValue = detail.GenderID;
                cmbNationality.SelectedValue = detail.NationalityID;
                cmbMaritalStatus.SelectedValue = detail.MaritalStatusID;
                cmbMembershipStatus.SelectedValue = detail.MembershipStatusID;
                cmbRelationshipToNextOfKin.SelectedValue = detail.RelationshipToKinID;
                if (LoginInfo.AccessLevel != 4)
                {
                    labelAccessLevel.Hide();
                    cmbPermission.Hide();
                    detail.PermissionID = detail.PermissionID;
                }
                else
                {
                    labelAccessLevel.Visible = true;
                    cmbPermission.Visible = true;
                    cmbPermission.SelectedValue = detail.PermissionID;
                }
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfilePic.ImageLocation = imagePath;
            }
        }
        
        private void labelMorePhone_Click(object sender, EventArgs e)
        {
            txtPhone2.Visible = true;
            txtPhone3.Visible = true;
            labelPhone2.Visible = true;
            labelPhone3.Visible = true;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter name");
            }
            if (txtLGA.Text.Trim() == "")
            {
                MessageBox.Show("Please enter LGA of country of origin");
            }
            if (txtImagePath.Text.Trim() == "")
            {
                MessageBox.Show("The imagePath is empty. Choose picture");
            }
            else if (txtSurname.Text.Trim() == "")
            {
                MessageBox.Show("Please enter surname");
            }
            else if (txtPhone1.Text.Trim() == "")
            {
                MessageBox.Show("Please enter phone number");
            }
            else if (txtEmail.Text.Trim()=="")
            {
                MessageBox.Show("Please enter email");
            }
            else if (cmbCountry.SelectedIndex ==- 1)
            {
                MessageBox.Show("Please select a country");
            }
            else if (cmbEmpStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an employment status");
            }
            else if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender");
            }
            else if (cmbMaritalStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a marital status");
            }
            else if (cmbNationality.SelectedIndex == -1)
            {
                MessageBox.Show("Please select nationality");
            }
            else if (cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a position");
            }
            else if (cmbProfession.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a profession");
            }
            else if (cmbMembershipStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a membership status");
            }
            else if (cmbRelationshipToNextOfKin.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a relationship to next of kin");
            }
            else if (LoginInfo.AccessLevel == 4 && cmbPermission.SelectedIndex == -1)
            {                
                MessageBox.Show("Please select an access level");
            }                 
            else
            {
                if (!isUpdate)
                {                    
                    MemberDetailDTO member = new MemberDetailDTO();
                    member.Name = txtName.Text;                    
                    member.Surname = txtSurname.Text;
                    member.LGA = txtLGA.Text;
                    member.Birthday = dateTimePickerBirthday.Value;
                    int day, month, yearDigit;
                    string year;
                    day = dateTimePickerBirthday.Value.Day;
                    month = dateTimePickerBirthday.Value.Month;
                    yearDigit = dateTimePickerBirthday.Value.Year % 100;
                    if (yearDigit == 0)
                    {
                        year = "0" + yearDigit;
                    }
                    else
                    {
                        year = (dateTimePickerBirthday.Value.Year % 100).ToString();
                    }
                    string lastUsername = bll.GetLastMemberUsername();
                    if (lastUsername == null)
                    {
                        member.Username = "apc20001";
                    }
                    else
                    {
                        string getDigits = lastUsername.Substring(3);
                        int convertDigits = Convert.ToInt32(getDigits) + 1;
                        member.Username = "apc" + convertDigits;
                    }                    
                    if (day < 10 && month < 10)
                    {
                        member.Password = "0"+ day + "0" + month + "" + year;
                    }
                    else if (day < 10 && month >= 10)
                    {
                        member.Password = "0" + day + "" + month + "" + year;
                    }
                    else if (day >= 10 && month < 10)
                    {
                        member.Password = "" + day + "0" + month + "" + year;
                    }
                    else
                    {
                        member.Password = "" + day + "" + month + "" + year;
                    }
                    member.EmailAddress = txtEmail.Text;
                    member.HouseAddress = txtAddress.Text;
                    member.CountryID = Convert.ToInt32(cmbCountry.SelectedValue);
                    member.ProfessionID = Convert.ToInt32(cmbProfession.SelectedValue);
                    member.GenderID = Convert.ToInt32(cmbGender.SelectedValue);
                    member.EmploymentStatusID = Convert.ToInt32(cmbEmpStatus.SelectedValue);
                    member.NationalityID = Convert.ToInt32(cmbNationality.SelectedValue);
                    member.MaritalStatusID = Convert.ToInt32(cmbMaritalStatus.SelectedValue);
                    member.NameOfNextOfKin = txtNameOfNextOfKin.Text;
                    member.RelationshipToKinID = Convert.ToInt32(cmbRelationshipToNextOfKin.SelectedValue);
                    if (LoginInfo.AccessLevel != 4)
                    {
                        member.PermissionID = 2;
                    }
                    else
                    {
                        member.PermissionID = Convert.ToInt32(cmbPermission.SelectedValue);
                    }
                    member.PermissionID = Convert.ToInt32(cmbPermission.SelectedValue);
                    member.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                    member.MembershipStatusID = Convert.ToInt32(cmbMembershipStatus.SelectedValue);
                    member.ImagePath = fileName;
                    member.PhoneNumber = txtPhone1.Text;
                    member.PhoneNumber2 = txtPhone2.Text;
                    member.PhoneNumber3 = txtPhone3.Text;       
                    member.MembershipDate = dateTimePickerMemSince.Value;
                    member.DeadDate = DateTime.Today;
                    if (bll.Insert(member))
                    {
                        MessageBox.Show("Member was added");
                        try
                        {
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Cannot find the path to this picture");
                        }
                        txtName.Clear();
                        txtSurname.Clear();
                        dateTimePickerBirthday.Value = DateTime.Today;
                        dateTimePickerMemSince.Value = DateTime.Today;
                        txtEmail.Clear();
                        txtLGA.Clear();
                        txtAddress.Clear();
                        txtImagePath.Clear();
                        txtPhone1.Clear();
                        txtPhone2.Clear();
                        txtPhone3.Clear();
                        txtNameOfNextOfKin.Clear();
                        cmbCountry.SelectedIndex = -1;
                        cmbCountry.DataSource = dto.Countries;
                        cmbNationality.SelectedIndex = -1;
                        cmbNationality.DataSource = dto.Nationalities;
                        cmbEmpStatus.SelectedIndex = -1;
                        cmbEmpStatus.DataSource = dto.EmploymentStatuses;
                        cmbGender.SelectedIndex = -1;
                        cmbGender.DataSource = dto.Genders;
                        cmbMaritalStatus.SelectedIndex = -1;
                        cmbMaritalStatus.DataSource = dto.MaritalStatuses;
                        cmbPosition.SelectedIndex = -1;
                        cmbPosition.DataSource = dto.Positions;
                        cmbProfession.SelectedIndex = -1;
                        cmbProfession.DataSource = dto.Professions;
                        cmbPermission.SelectedIndex = -1;
                        cmbPermission.DataSource = dto.Permissions;
                        cmbMembershipStatus.SelectedIndex = -1;
                        cmbMembershipStatus.DataSource = dto.MembershipStatuses;
                        cmbRelationshipToNextOfKin.SelectedIndex = -1;
                        cmbRelationshipToNextOfKin.DataSource = dto.RelationshipsToNextOfKin;
                        picProfilePic.Image = null;
                        txtPhone2.Hide();
                        txtPhone3.Hide();
                        labelPhone2.Hide();
                        labelPhone3.Hide();
                    }
                }
                else if(isUpdate)
                {
                    if (
                            !isUpdateDeadMember &&
                            detail.Name == txtName.Text.Trim() && detail.Surname == txtSurname.Text.Trim() && detail.LGA == txtLGA.Text.Trim()
                            && detail.EmailAddress == txtEmail.Text.Trim() && detail.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)
                            && detail.Birthday == dateTimePickerBirthday.Value && detail.MembershipDate == dateTimePickerMemSince.Value
                            && detail.HouseAddress == txtAddress.Text.Trim() && detail.ImagePath == txtImagePath.Text.Trim()
                            && detail.PhoneNumber == txtPhone1.Text.Trim() && detail.PhoneNumber2 == txtPhone2.Text.Trim()
                            && detail.PhoneNumber3 == txtPhone3.Text.Trim() && detail.CountryID == Convert.ToInt32(cmbCountry.SelectedValue)
                            && detail.ProfessionID == Convert.ToInt32(cmbProfession.SelectedValue) && detail.EmploymentStatusID == Convert.ToInt32(cmbEmpStatus.SelectedValue)
                            && detail.GenderID == Convert.ToInt32(cmbGender.SelectedValue) && detail.NationalityID == Convert.ToInt32(cmbNationality.SelectedValue)
                            && detail.MaritalStatusID == Convert.ToInt32(cmbMaritalStatus.SelectedValue) && detail.PermissionID == Convert.ToInt32(cmbPermission.SelectedValue)
                            && detail.MembershipStatusID == Convert.ToInt32(cmbMembershipStatus.SelectedValue) && detail.RelationshipToKinID == Convert.ToInt32(cmbRelationshipToNextOfKin.SelectedValue)
                            && detail.NameOfNextOfKin == txtNameOfNextOfKin.Text.Trim()
                        )
                    {
                        MessageBox.Show("There is no change");
                    }
                    else if (
                            isUpdateDeadMember && detail.DeadDate == dateTimePickerDeceasedDate.Value && detail.LGA == txtLGA.Text.Trim()
                            && detail.Name == txtName.Text.Trim() && detail.Surname == txtSurname.Text.Trim()
                            && detail.EmailAddress == txtEmail.Text.Trim() && detail.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)
                            && detail.Birthday == dateTimePickerBirthday.Value && detail.MembershipDate == dateTimePickerMemSince.Value
                            && detail.HouseAddress == txtAddress.Text.Trim() && detail.ImagePath == txtImagePath.Text.Trim()
                            && detail.PhoneNumber == txtPhone1.Text.Trim() && detail.PhoneNumber2 == txtPhone2.Text.Trim()
                            && detail.PhoneNumber3 == txtPhone3.Text.Trim() && detail.CountryID == Convert.ToInt32(cmbCountry.SelectedValue)
                            && detail.ProfessionID == Convert.ToInt32(cmbProfession.SelectedValue) && detail.EmploymentStatusID == Convert.ToInt32(cmbEmpStatus.SelectedValue)
                            && detail.GenderID == Convert.ToInt32(cmbGender.SelectedValue) && detail.NationalityID == Convert.ToInt32(cmbNationality.SelectedValue)
                            && detail.MaritalStatusID == Convert.ToInt32(cmbMaritalStatus.SelectedValue) && detail.PermissionID == Convert.ToInt32(cmbPermission.SelectedValue)
                            && detail.MembershipStatusID == Convert.ToInt32(cmbMembershipStatus.SelectedValue) && detail.RelationshipToKinID == Convert.ToInt32(cmbRelationshipToNextOfKin.SelectedValue)
                            && detail.NameOfNextOfKin == txtNameOfNextOfKin.Text.Trim()
                        )
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        detail.Name = txtName.Text;
                        detail.Surname = txtSurname.Text;
                        detail.HouseAddress = txtAddress.Text;
                        detail.NameOfNextOfKin = txtNameOfNextOfKin.Text;
                        detail.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);                        
                        detail.MembershipDate = dateTimePickerMemSince.Value;
                        detail.Username = detail.Username;
                        if (detail.Birthday == dateTimePickerBirthday.Value)
                        {
                            detail.Password = detail.Password;
                            detail.Birthday = detail.Birthday;
                        }
                        else if (detail.Birthday != dateTimePickerBirthday.Value)
                        {
                            int day, month, yearDigit;
                            string year;
                            day = dateTimePickerBirthday.Value.Day;
                            month = dateTimePickerBirthday.Value.Month;
                            yearDigit = dateTimePickerBirthday.Value.Year % 100;
                            if (yearDigit == 0 )
                            {
                                year = "0" + yearDigit;
                            }
                            else
                            {
                                year = (dateTimePickerBirthday.Value.Year % 100).ToString();
                            }
                            if (day < 10 && month < 10)
                            {
                                detail.Password = "0" + day + "0" + month + "" + year;
                            }
                            else if (day < 10 && month >= 10)
                            {
                                detail.Password = "0" + day + "" + month + "" + year;
                            }
                            else if (day >= 10 && month < 10)
                            {
                                detail.Password = "" + day + "0" + month + "" + year;
                            }
                            else if (day >= 10 && month >= 10)
                            {
                                detail.Password = "" + day + "" + month + "" + year;
                            }
                            detail.Birthday = dateTimePickerBirthday.Value;
                        }                        
                        detail.EmailAddress = txtEmail.Text;
                        detail.LGA = txtLGA.Text;
                        if (detail.ImagePath != txtImagePath.Text.Trim())
                        {
                            if (File.Exists(@"images\\" + detail.ImagePath))
                            {
                                File.Delete(@"images\\" + detail.ImagePath);
                            }
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                            detail.ImagePath = fileName;
                        }
                        else if (detail.ImagePath == txtImagePath.Text.Trim())
                        {
                            detail.ImagePath = txtImagePath.Text;
                        }
                        detail.PhoneNumber = txtPhone1.Text;
                        detail.PhoneNumber2 = txtPhone2.Text;
                        detail.PhoneNumber3 = txtPhone3.Text;
                        detail.CountryID = Convert.ToInt32(cmbCountry.SelectedValue);
                        detail.ProfessionID = Convert.ToInt32(cmbProfession.SelectedValue);
                        detail.EmploymentStatusID = Convert.ToInt32(cmbEmpStatus.SelectedValue);
                        detail.GenderID = Convert.ToInt32(cmbGender.SelectedValue);
                        detail.NationalityID = Convert.ToInt32(cmbNationality.SelectedValue);
                        detail.MaritalStatusID = Convert.ToInt32(cmbMaritalStatus.SelectedValue);
                        detail.PermissionID = Convert.ToInt32(cmbPermission.SelectedValue);
                        detail.MembershipStatusID = Convert.ToInt32(cmbMembershipStatus.SelectedValue);
                        detail.RelationshipToKinID = Convert.ToInt32(cmbRelationshipToNextOfKin.SelectedValue);
                        if (isUpdateDeadMember)
                        {
                            detail.DeadDate = dateTimePickerDeceasedDate.Value;
                        }
                        if (!isUpdateDeadMember)
                        {
                            detail.DeadDate = detail.DeadDate;
                        }                        
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Member was updated");
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
                picProfilePic.Load(OpenFileDialog1.FileName);
                txtImagePath.Text = OpenFileDialog1.FileName;
                string unique = Guid.NewGuid().ToString();
                fileName += unique + OpenFileDialog1.SafeFileName;
            }
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
