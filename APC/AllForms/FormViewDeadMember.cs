using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using APC.Utility;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewDeadMember : BaseFormDashboard
    {
        private readonly IMemberService _memberService;
        private readonly IPersonalAttendanceService _personalAttendanceService;
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IGeneralMeetingService _generalMeetingService;
        private readonly IFinedMemberService _finedMemberService;
        private readonly IMonthService _monthService;
        private readonly IFinancialReportService _financialReportService;

        private MemberFullDetailsDTO _memberFullDetailsDTO;

        private int attendancePresentCount = 0;
        private int attendanceAbsentCount = 0;

        public FormViewDeadMember(IMemberService memberService, IPersonalAttendanceService personalAttendanceService, 
            IGeneralMeetingAttendanceService generalMeetingAttendanceService, IGeneralMeetingService generalMeetingService, 
            IFinedMemberService finedMemberService, IMonthService monthService, IFinancialReportService financialReportService)
        {
            InitializeComponent();
            _memberService = memberService;
            _personalAttendanceService = personalAttendanceService;
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
            _generalMeetingService = generalMeetingService;
            _finedMemberService = finedMemberService;
            _monthService = monthService;
            _financialReportService = financialReportService;
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

        public void MemberDetail(MemberFullDetailsDTO memberFullDetailsDTO)
        {
            _memberFullDetailsDTO = memberFullDetailsDTO;
        }

        private void FormViewDeadMember_Load(object sender, EventArgs e)
        {
            attendancePresentCount = _personalAttendanceService.GetTotalMembersPresentCountById(_memberFullDetailsDTO.MemberId);
            labelNoOfPresent.Text = attendancePresentCount.ToString();
            btnViewPresentAttendance.Hide();
            if (attendancePresentCount > 0)
            {
                btnViewPresentAttendance.Visible = true;
            }

            attendanceAbsentCount = _personalAttendanceService.GetTotalMembersAbsentCountById(_memberFullDetailsDTO.MemberId);
            labelNoOfAbsent.Text = attendanceAbsentCount.ToString();
            btnViewAbsentAttendance.Hide();
            if (attendanceAbsentCount > 0)
            {
                btnViewAbsentAttendance.Visible = true;
            }

            labelMemberNameTitle.Text = _memberFullDetailsDTO.LastName + " " + _memberFullDetailsDTO.FirstName;
            string imagePath = Application.StartupPath + "\\images\\" + _memberFullDetailsDTO.ImagePath;
            picProfilePic.ImageLocation = imagePath;

            txtName.Text = _memberFullDetailsDTO.FirstName;
            txtSurname.Text = _memberFullDetailsDTO.LastName;
            txtPosition.Text = _memberFullDetailsDTO.Position;
            labelBirthday.Text = _memberFullDetailsDTO.Birthday.ToShortDateString();
            DateTime memberSince = Convert.ToDateTime(_memberFullDetailsDTO.MembershipDate);
            labelMemSince.Text = memberSince.ToString("dd.MM.yyy");

            txtCountry.Text = _memberFullDetailsDTO.Country;
            txtProfession.Text = _memberFullDetailsDTO.Profession;
            txtGender.Text = _memberFullDetailsDTO.Gender;
            txtNationality.Text = _memberFullDetailsDTO.Nationality;
            txtMaritalStatus.Text = _memberFullDetailsDTO.MaritalStatus;

            TimeSpan difference;
            DateTime died = Convert.ToDateTime(_memberFullDetailsDTO.DeadDate);
            labelDeadDate.Text = died.ToShortDateString();
            difference = _memberFullDetailsDTO.DeadDate - _memberFullDetailsDTO.Birthday;
            labelAge.Text = Math.Floor(difference.TotalDays / 365.25).ToString() + " years";
            txtNameOfNextOfKin.Text = _memberFullDetailsDTO.NextOfKin;
            txtRelationshipToKin.Text = _memberFullDetailsDTO.RelationshipToNextOfKin;
        }

        private void btnViewPresentAttendance_Click(object sender, EventArgs e)
        {
            if (attendancePresentCount > 0)
            {
                var form = new FormViewPersonalAttendances(_memberService, _monthService, _generalMeetingService, _financialReportService,
                    _finedMemberService, _personalAttendanceService);
                form.GetMemberId(_memberFullDetailsDTO.MemberId);
                form.IsPresent(true);
                form.ShowDialog();
            }
        }

        private void btnViewAbsentAttendance_Click(object sender, EventArgs e)
        {
            if (attendanceAbsentCount > 0)
            {
                var form = new FormViewPersonalAttendances(_memberService, _monthService, _generalMeetingService, _financialReportService,
                    _finedMemberService, _personalAttendanceService);
                form.GetMemberId(_memberFullDetailsDTO.MemberId);
                form.IsAbsent(true);
                form.ShowDialog();
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
