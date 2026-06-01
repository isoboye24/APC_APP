using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewMember : Form
    {
        private readonly IMemberService _memberService;
        private readonly IFinedMemberService _finedMemberService;
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IGeneralMeetingService _generalMeetingService;
        private readonly IPersonalAttendanceService _personalAttendanceService;
        private readonly IFinancialReportService _financialReportService;
        private readonly IMonthService _monthService;

        private MemberFullDetailsDTO _memberFullDetailsDTOById;

        private int attendancePresentCount = 0;
        private int attendanceAbsentCount = 0;
        private decimal amountContributed = 0;
        private decimal amountExpected = 0;
        private decimal balanceCurrYear = 0;
        private int finesCount = 0;
        private int currYear = DateTime.Today.Year;

        public FormViewMember(IMemberService memberService, IFinedMemberService finedMemberService, IGeneralMeetingAttendanceService generalMeetingAttendanceService,
            IPersonalAttendanceService personalAttendanceService, IFinancialReportService financialReportService, IMonthService monthService,
            IGeneralMeetingService generalMeetingService)
        {
            InitializeComponent();
            _memberService = memberService;
            _finedMemberService = finedMemberService;
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
            _personalAttendanceService = personalAttendanceService;
            _financialReportService = financialReportService;
            _monthService = monthService;
            _generalMeetingService = generalMeetingService;
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


        public void MemberDetail(int memberId)
        {
            _memberFullDetailsDTOById = _memberService.GetMemberById(memberId);
        }

        private void ResizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelMemberNameTitle, label1, label2, label3, label4, label5, label6, label7, label8, label9,
                label10, label11, label12, label13, label14, label15, label16, label17, label18, label19, label20, label21, label22,
                label23, label25, label26, label27, labelPhone2, labelPhone3, labelBirthday, labelMemSince, labelAmountContributed,
                labelAmountExpected, labelFinesText, labelNoOfAbsent, labelNoOfFines, labelNoOfPresent,
                labelPersonalBalance, btnClose, btnViewAbsentAttendance, btnViewAmountContributed, btnViewAmountExpected,
                btnViewFines, btnViewPersonalBalance, btnViewPresentAttendance
                );

            GeneralHelper.ApplyRegularFont(14, txtAddress, txtEmail, txtLGA, txtName, txtPhone1, txtCountry, txtEmpStatus, txtGender,
                txtPhone2, txtPhone3, txtSurname, txtMaritalStatus, txtNationality, txtNextOfKin, txtNextOfKinRelationship, txtPermission,
                txtPosition, txtProfession
                );
        }

        public void ViewMemberDetails(int Id)
        {
            _memberFullDetailsDTOById = _memberService.GetMemberById(Id);

            labelMemberNameTitle.Text = _memberFullDetailsDTOById.LastName + " " + _memberFullDetailsDTOById.FirstName;
            string imagePath = Application.StartupPath + "\\images\\" + _memberFullDetailsDTOById.ImagePath;
            picProfilePic.ImageLocation = imagePath;

            txtName.Text = _memberFullDetailsDTOById.FirstName;
            txtSurname.Text = _memberFullDetailsDTOById.LastName;
            txtAddress.Text = _memberFullDetailsDTOById.HouseAddress;
            txtPosition.Text = _memberFullDetailsDTOById.Position;
            labelBirthday.Text = _memberFullDetailsDTOById.Birthday.ToShortDateString();
            labelMemSince.Text = _memberFullDetailsDTOById.MembershipDate.ToString();
            txtEmail.Text = _memberFullDetailsDTOById.Email;
            txtPhone1.Text = _memberFullDetailsDTOById.PhoneNumber;
            txtLGA.Text = _memberFullDetailsDTOById.LGA;
            if (_memberFullDetailsDTOById.PhoneNumber2 != "")
            {
                txtPhone2.Visible = true;
                labelPhone2.Visible = true;
            }
            if (_memberFullDetailsDTOById.PhoneNumber3 != "")
            {
                txtPhone3.Visible = true;
                labelPhone3.Visible = true;
            }
            txtPhone2.Text = _memberFullDetailsDTOById.PhoneNumber2;
            txtPhone3.Text = _memberFullDetailsDTOById.PhoneNumber3;
            txtCountry.Text = _memberFullDetailsDTOById.Country;
            txtProfession.Text = _memberFullDetailsDTOById.Profession;
            txtEmpStatus.Text = _memberFullDetailsDTOById.EmploymentStatus;
            txtGender.Text = _memberFullDetailsDTOById.Gender;
            txtNationality.Text = _memberFullDetailsDTOById.Nationality;
            txtMaritalStatus.Text = _memberFullDetailsDTOById.MaritalStatus;
            txtPermission.Text = _memberFullDetailsDTOById.Permission;
            txtNextOfKin.Text = _memberFullDetailsDTOById.NextOfKin;
            txtNextOfKinRelationship.Text = _memberFullDetailsDTOById.RelationshipToNextOfKin;
        }

        private void FormViewMember_Load(object sender, EventArgs e)
        {
            ResizeControls();

            ViewMemberDetails(_memberFullDetailsDTOById.MemberId);         

            
            finesCount = _finedMemberService.AnnualFinesCountById(_memberFullDetailsDTOById.MemberId, currYear);
            btnViewFines.Hide();
            labelNoOfFines.Text = finesCount.ToString();
            if (finesCount > 0)
            {
                labelFinesText.Text = "Fine";
                btnViewFines.Text = "View Fine";
                btnViewFines.Visible = true;
            }
            if (finesCount > 1)
            {
                labelFinesText.Text = "Fines";
                btnViewFines.Text = "View Fines";
                btnViewFines.Visible = true;
            }

            attendancePresentCount = _personalAttendanceService.GetAnnualMembersPresentCountById(_memberFullDetailsDTOById.MemberId, currYear);
            labelNoOfPresent.Text = attendancePresentCount.ToString();
            btnViewPresentAttendance.Hide();
            if (attendancePresentCount > 0)
            {
                btnViewPresentAttendance.Visible = true;
                btnViewPresentAttendance.Text = "View Attendance";
            }

            attendanceAbsentCount = _personalAttendanceService.GetAnnualMembersAbsentCountById(_memberFullDetailsDTOById.MemberId, currYear);
            labelNoOfAbsent.Text = attendanceAbsentCount.ToString();
            btnViewAbsentAttendance.Hide();
            if (attendanceAbsentCount > 0)
            {
                btnViewAbsentAttendance.Visible = true;
                btnViewAbsentAttendance.Text = "View Attendance";
            }

            amountContributed = _financialReportService.GetTotalAnnualDuesById(_memberFullDetailsDTOById.MemberId, currYear);
            labelAmountContributed.Text = "€" + amountContributed;
            btnViewAmountContributed.Hide();
            if (amountContributed > 0)
            {
                btnViewAmountContributed.Visible = true;
                btnViewAmountContributed.Text = "View Amount";
            }
            amountExpected = _financialReportService.GetTotalAnnualDuesExpectedById(_memberFullDetailsDTOById.MemberId, currYear);
            labelAmountExpected.Text = "€ 120"; // To be dynamically calculated
            btnViewAmountExpected.Hide();
            if (amountExpected > 0)
            {
                btnViewAmountExpected.Visible = true;
                btnViewAmountExpected.Text = "View Amount";
            }
            balanceCurrYear = amountExpected - amountContributed;
            btnViewPersonalBalance.Hide();
            if (balanceCurrYear > 0)
            {
                labelPersonalBalance.Text = "€" + balanceCurrYear;
                btnViewPersonalBalance.Visible = true;
                btnViewPersonalBalance.Text = "View Amount";    
            }

            txtPhone2.Hide();
            txtPhone3.Hide();
            labelPhone2.Hide();
            labelPhone3.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
        
        private void btnViewPresentAttendance_Click(object sender, EventArgs e)
        {            
            if (attendancePresentCount > 0)
            {
                var form = new FormViewPersonalAttendances(_memberService, _monthService, _generalMeetingService, _financialReportService, 
                    _finedMemberService);
                form.GetMemberId(_memberFullDetailsDTOById.MemberId);
                form.IsPresent(true);
                form.ShowDialog();
            }
        }

        private void btnViewAbsentAttendance_Click_1(object sender, EventArgs e)
        {
            if (attendanceAbsentCount > 0)
            {
                var form = new FormViewPersonalAttendances(_memberService, _monthService, _generalMeetingService, _financialReportService,
                    _finedMemberService);
                form.GetMemberId(_memberFullDetailsDTOById.MemberId);
                form.IsAbsent(true);
                form.ShowDialog();
            }
        }

        private void btnViewAmountContributed_Click_1(object sender, EventArgs e)
        {
            if (amountContributed > 0)
            {
                var form = new FormViewPersonalAttendances(_memberService, _monthService, _generalMeetingService, _financialReportService,
                    _finedMemberService);
                form.GetMemberId(_memberFullDetailsDTOById.MemberId);
                form.IsAmountContributed(true);
                form.ShowDialog();
            }
        }

        private void btnViewAmountExpected_Click_1(object sender, EventArgs e)
        {
            if (balanceCurrYear > 0)
            {
                var form = new FormViewPersonalAttendances(_memberService, _monthService, _generalMeetingService, _financialReportService,
                    _finedMemberService);
                form.GetMemberId(_memberFullDetailsDTOById.MemberId);
                form.IsAmountExpected(true);
                form.ShowDialog();
            }
        }

        private void btnViewPersonalBalance_Click_1(object sender, EventArgs e)
        {
            if (balanceCurrYear > 0)
            {
                var form = new FormViewPersonalAttendances(_memberService, _monthService, _generalMeetingService, _financialReportService,
                    _finedMemberService);
                form.GetMemberId(_memberFullDetailsDTOById.MemberId);
                form.IsPersonalBalance(true);
                form.ShowDialog();
            }
        }

        private void btnViewFines_Click(object sender, EventArgs e)
        {
            if (finesCount > 0)
            {
                var form = new FormViewPersonalAttendances(_memberService, _monthService, _generalMeetingService, _financialReportService,
                    _finedMemberService);
                form.GetMemberId(_memberFullDetailsDTOById.MemberId);
                form.IsPersonalFines(true);
                form.ShowDialog();
            }
        }
    }
}
