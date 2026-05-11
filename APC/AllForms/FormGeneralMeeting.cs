using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormGeneralMeeting : Form
    {
        private readonly IGeneralMeetingService _generalMeetingService;
        private readonly IMemberService _memberService;
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;

        private Applications.DTO.GeneralMeetingDTO _generalMeetingDTO;
        private bool _isUpdate = false;

        public FormGeneralMeeting(IGeneralMeetingService generalMeetingService, IMemberService memberService,
            IGeneralMeetingAttendanceService generalMeetingAttendanceService)
        {
            InitializeComponent();
            _generalMeetingService = generalMeetingService;
            _memberService = memberService;
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
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

        public void loadForEdit(Applications.DTO.GeneralMeetingDTO generalMeetingDTO, bool isUpdate)
        {
            _generalMeetingDTO = generalMeetingDTO;
            _isUpdate = isUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime meetingDate = dateTimePickerGenAttDate.Value;
                string summary = txtSummary.Text.Trim();

                if (_generalMeetingDTO.GeneralMeetingId == 0)
                {
                    int totalMembersPresent = 0;
                    int totalMembersAbsent = 0;
                    decimal totalDuesPaid = 0;
                    int currentMembers = _memberService.GetAllCurrentMembersCount();
                    decimal totalDuesExpected = currentMembers * 10;
                    decimal totalBalance = totalDuesExpected - totalDuesPaid;

                    var generalMeeing = new GeneralMeeting(totalMembersPresent, totalMembersAbsent, totalDuesPaid, totalDuesExpected, totalBalance, 
                        summary, meetingDate);

                    _generalMeetingService.Create(generalMeeing);
                    MessageBox.Show("Meeting created successfully!");
                }
                else
                {
                    int totalMembersPresent = _generalMeetingAttendanceService.GetMembersPresentCount(_generalMeetingDTO.GeneralMeetingId);
                    int totalMembersAbsent = _generalMeetingAttendanceService.GetMembersPresentCount(_generalMeetingDTO.GeneralMeetingId);
                    decimal totalDuesPaid = _generalMeetingAttendanceService.GetTotalDuesPaid(_generalMeetingDTO.GeneralMeetingId);
                    int currentMembers = _memberService.GetAllCurrentMembersCount();

                    decimal totalDuesExpected = currentMembers * 10;
                    decimal totalBalance = totalDuesExpected - totalDuesPaid;

                    var generalMeeing = new GeneralMeeting(totalMembersPresent, totalMembersAbsent, totalDuesPaid, totalDuesExpected, totalBalance,
                        summary, meetingDate);


                    _generalMeetingService.Update(generalMeeing);
                    MessageBox.Show("General meeting updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, labelTitle, btnClose, btnSave);
            GeneralHelper.ApplyRegularFont(14, dateTimePickerGenAttDate, txtSummary);
        }

        private void FormGeneralAttendance_Load(object sender, EventArgs e)
        {
            resizeControls();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit meeting on " + _generalMeetingDTO.Day + "." +_generalMeetingDTO.MonthId + "." +_generalMeetingDTO.Year;
                dateTimePickerGenAttDate.Value = _generalMeetingDTO.GeneralMeetingDate;
                txtSummary.Text = _generalMeetingDTO.Summary;
            }
            else
            {
                labelTitle.Text = "Add Meeting";
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
