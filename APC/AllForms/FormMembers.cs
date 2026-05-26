using APC.Applications.Entities;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC
{
    public partial class FormMembers : Form
    {
        private readonly IMemberService _memberService;
        private readonly ICountryService _countryService;
        private readonly INationalityService _nationalityService;
        private readonly IProfessionService _professionService;
        private readonly IPositionService _positionService;
        private readonly ICurrentUserService _currentUserService;

        private readonly IGenderService _genderService;
        private readonly IMaritalStatusService _maritalStatusService;
        private readonly IEmploymentStatusService _employmentStatusService;
        private readonly INextOfKinService _nextOfKinService;
        private readonly IPermissionService _permissionService;
        private readonly IMembershipStatusService _membershipStatusService;

        private Applications.DTO.MemberFullDetailsDTO _memberFullDetailsDTO;

        private bool _isUpdate = false;
        private bool _isUpdateDeadMember = false;
        private string fileName;

        public FormMembers(IMemberService memberService, ICountryService countryService, INationalityService nationalityService,
            IProfessionService professionService, IPositionService positionService, ICurrentUserService currentUserService, IGenderService genderService,
            IMaritalStatusService maritalStatusService, IEmploymentStatusService employmentStatusService, INextOfKinService nextOfKinService,
            IPermissionService permissionService, IMembershipStatusService membershipStatusService)
        {
            InitializeComponent();
            _memberService = memberService;
            _countryService = countryService;
            _nationalityService = nationalityService;
            _professionService = professionService;
            _positionService = positionService;
            _currentUserService = currentUserService;

            _genderService = genderService;
            _maritalStatusService = maritalStatusService;
            _employmentStatusService = employmentStatusService;
            _membershipStatusService = membershipStatusService;
            _nextOfKinService = nextOfKinService;
            _permissionService = permissionService;
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

        public void loadForEdit(Applications.DTO.MemberFullDetailsDTO memberFullDetailsDTO, bool isUpdate)
        {
            _memberFullDetailsDTO = memberFullDetailsDTO;
            _isUpdate = isUpdate;
        }

        private void ResizeControls()
        {
            GeneralHelper.ApplyItalicFont(14, labelTitle, label1, label2, label3, label5, label6, label9, label10, label11, label12, label4, label7, label8, 
                label25, label26, label14, label15, label19, label27, labelAccessLevel, labelDeceasedDate, labelMorePhone, labelPhone2, labelPhone3,
                btnBrowse, btnClose, btnSave);

            GeneralHelper.ApplyRegularFont(14, cmbCountry, txtAddress, txtEmail, txtImagePath, txtLGA, txtName, txtNameOfNextOfKin, txtPhone1, txtPhone2,
                txtPhone3, txtSurname, cmbEmpStatus, cmbGender, cmbMaritalStatus, cmbMembershipStatus, cmbNationality, cmbPermission, cmbPosition,
                cmbProfession, cmbRelationshipToNextOfKin, dateTimePickerBirthday, dateTimePickerDeceasedDate, dateTimePickerMemSince
                );
        }

        private void FormMembers_Load(object sender, EventArgs e)
        {
            ResizeControls();

            #region
            cmbCountry.DataSource = _countryService.GetAll();
            GeneralHelper.ComboBoxProps(cmbCountry, "CountryName", "countryID");
            cmbGender.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGender, "GenderName", "genderID");
            cmbProfession.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfession, "Profession", "professionID");
            cmbPosition.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPosition, "PositionName", "positionID");
            cmbMaritalStatus.DataSource = _maritalStatusService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMaritalStatus, "MaritalStatus", "MaritalStatusID");
            cmbEmpStatus.DataSource = _employmentStatusService.GetAll();
            GeneralHelper.ComboBoxProps(cmbEmpStatus, "EmploymentStatus", "EmploymentStatusID");
            cmbNationality.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationality, "Nationality", "NationalityID");
            cmbPermission.DataSource = _permissionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPermission, "Permission", "PermissionID");
            cmbMembershipStatus.DataSource = _membershipStatusService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMembershipStatus, "MembershipStatus", "MembershipStatusID");
            cmbRelationshipToNextOfKin.DataSource = _nextOfKinService.GetAll();
            GeneralHelper.ComboBoxProps(cmbRelationshipToNextOfKin, "Relationship", "RelationshipToKinID");
            #endregion

            txtPhone2.Hide();
            txtPhone3.Hide();
            labelPhone2.Hide();
            labelPhone3.Hide();
            txtImagePath.Hide();
            tableLayoutPanelDeceasedDate.Hide();
            labelTitle.Text = "Add Member";
            if (_isUpdateDeadMember)
            {
                tableLayoutPanelDeceasedDate.Visible = true;
                dateTimePickerDeceasedDate.Value = _memberFullDetailsDTO.DeadDate;
            }
            if (_isUpdate)
            {
                labelTitle.Text = "Edit Member";
                txtName.Text = _memberFullDetailsDTO.FirstName;
                txtSurname.Text = _memberFullDetailsDTO.LastName;
                txtAddress.Text = _memberFullDetailsDTO.HouseAddress;
                cmbPosition.SelectedValue = _memberFullDetailsDTO.PositionId;
                dateTimePickerBirthday.Value = Convert.ToDateTime(_memberFullDetailsDTO.Birthday);
                dateTimePickerMemSince.Value = Convert.ToDateTime(_memberFullDetailsDTO.MembershipDate);
                txtEmail.Text = _memberFullDetailsDTO.Email;
                txtLGA.Text = _memberFullDetailsDTO.LGA;
                txtImagePath.Text = _memberFullDetailsDTO.ImagePath;                
                txtPhone1.Text = _memberFullDetailsDTO.PhoneNumber;
                if (_memberFullDetailsDTO.PhoneNumber2 != "")
                {
                    txtPhone2.Visible = true;
                    labelPhone2.Visible = true;
                }
                if (_memberFullDetailsDTO.PhoneNumber3 != "")
                {
                    txtPhone3.Visible = true;
                    labelPhone3.Visible = true;
                }
                txtPhone2.Text = _memberFullDetailsDTO.PhoneNumber2;
                txtPhone3.Text = _memberFullDetailsDTO.PhoneNumber3;
                txtNameOfNextOfKin.Text = _memberFullDetailsDTO.NextOfKin;
                cmbCountry.SelectedValue = _memberFullDetailsDTO.CountryId;
                cmbProfession.SelectedValue = _memberFullDetailsDTO.ProfessionId;
                cmbEmpStatus.SelectedValue = _memberFullDetailsDTO.EmploymentStatusId;
                cmbGender.SelectedValue = _memberFullDetailsDTO.GenderId;
                cmbNationality.SelectedValue = _memberFullDetailsDTO.NationalityId;
                cmbMaritalStatus.SelectedValue = _memberFullDetailsDTO.MaritalStatusId;
                cmbMembershipStatus.SelectedValue = _memberFullDetailsDTO.MembershipStatusId;
                cmbRelationshipToNextOfKin.SelectedValue = _memberFullDetailsDTO.RelationshipToNextOfKinId;
                if (_currentUserService.AccessLevel != 4)
                {
                    labelAccessLevel.Hide();
                    cmbPermission.Hide();
                    _memberFullDetailsDTO.PermissionId = _memberFullDetailsDTO.PermissionId;
                }
                else
                {
                    labelAccessLevel.Visible = true;
                    cmbPermission.Visible = true;
                    cmbPermission.SelectedValue = _memberFullDetailsDTO.PermissionId;
                }
                string imagePath = Application.StartupPath + "\\images\\" + _memberFullDetailsDTO.ImagePath;
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

        private void ClearControls()
        {
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
            cmbCountry.DataSource = _countryService.GetAll();
            cmbNationality.SelectedIndex = -1;
            cmbNationality.DataSource = _nationalityService.GetAll();

            cmbEmpStatus.SelectedIndex = -1;
            cmbEmpStatus.DataSource = _employmentStatusService.GetAll();
            cmbGender.SelectedIndex = -1;
            cmbGender.DataSource = _genderService.GetAll();
            cmbMaritalStatus.SelectedIndex = -1;
            cmbMaritalStatus.DataSource = _maritalStatusService.GetAll();
            cmbPosition.SelectedIndex = -1;
            cmbPosition.DataSource = _positionService.GetAll();

            cmbProfession.SelectedIndex = -1;
            cmbProfession.DataSource = _professionService.GetAll();
            cmbPermission.SelectedIndex = -1;
            cmbPermission.DataSource = _permissionService.GetAll();
            cmbMembershipStatus.SelectedIndex = -1;
            cmbMembershipStatus.DataSource = _membershipStatusService.GetAll();

            cmbRelationshipToNextOfKin.SelectedIndex = -1;
            cmbRelationshipToNextOfKin.DataSource = _nextOfKinService.GetAll();
            picProfilePic.Image = null;
            txtPhone2.Hide();
            txtPhone3.Hide();
            labelPhone2.Hide();
            labelPhone3.Hide();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string lastUsername = _memberService.GetLastMemberUsername();

                DateTime birthday = dateTimePickerBirthday.Value;
                DateTime memberSince = dateTimePickerMemSince.Value;
                DateTime deadDate = DateTime.Today;

                string firstName = txtName.Text.Trim();
                string lastName = txtSurname.Text.Trim();
                string LGA = txtLGA.Text.Trim();
                string email = txtEmail.Text.Trim();
                string houseAddress = txtAddress.Text.Trim();

                int countryId = Convert.ToInt32(cmbCountry.SelectedValue);
                int professionId = Convert.ToInt32(cmbProfession.SelectedValue);
                int genderId = Convert.ToInt32(cmbGender.SelectedValue);
                int empStatusId = Convert.ToInt32(cmbEmpStatus.SelectedValue);
                int nationalityId = Convert.ToInt32(cmbNationality.SelectedValue);

                int maritalStatusId = Convert.ToInt32(cmbMaritalStatus.SelectedValue);
                int relationshipToNextOfKinId = Convert.ToInt32(cmbRelationshipToNextOfKin.SelectedValue);

                string nameOfNextOfKin = txtNameOfNextOfKin.Text.Trim();
                int permissionId = 0;
                if (_currentUserService.AccessLevel != 4)
                {
                    permissionId = 2;
                }
                else
                {
                    permissionId = Convert.ToInt32(cmbPermission.SelectedValue);
                }

                int positionId = Convert.ToInt32(cmbPosition.SelectedValue);
                int membershipStatusId = Convert.ToInt32(cmbMembershipStatus.SelectedValue);

                string imagePath = fileName;
                string phone1 = txtPhone1.Text.Trim();
                string phone2 = txtPhone2.Text.Trim();
                string phone3 = txtPhone3.Text.Trim();

                var authentication = new MemberAuthentication(lastUsername, birthday);
                var personalInfo = new PersonalInfo(firstName, lastName, birthday, imagePath, genderId);
                var contactInfo = new ContactInfo(email, houseAddress, phone1, phone2, phone3);
                var membershipInfo = new MembershipInfo(memberSince, membershipStatusId, positionId, permissionId);
                var demographicInfo = new DemographicInfo(countryId, nationalityId, professionId, empStatusId, maritalStatusId, LGA);
                var emergencyContact = new EmergencyContact(nameOfNextOfKin, relationshipToNextOfKinId);
                var lifeStatus = new LifeStatus(deadDate);

                var member = new Member(authentication, personalInfo, contactInfo, membershipInfo, demographicInfo, emergencyContact, lifeStatus);


                if (_memberFullDetailsDTO.MemberId == 0)
                {
                    _memberService.Create(member);

                    MessageBox.Show("Member was added");
                    try
                    {
                        File.Copy(txtImagePath.Text, @"images\\" + fileName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Cannot find the path to this picture");
                    }
                    ClearControls();
                }
                else
                {
                    _memberService.Update(member);
                    MessageBox.Show("Member updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
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
