using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using APC.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC
{
    public partial class FormGeneralMeetingAttendance : Form
    {
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IMemberService _memberService;
        private readonly IAttendanceStatusService _attendanceStatusService;

        private GeneralMeetingAttendanceDTO _generalMeetingAttendanceDTO;
        private GeneralMeetingDTO _generalMeetingDTO;
        private List<MemberFullDetailsDTO> _memberDTO;
        private bool _isUpdate = false;
        private int _memberId = 0;
        private int _attendanceStatusId = 0;

        public FormGeneralMeetingAttendance(IGeneralMeetingAttendanceService generalMeetingAttendanceService, IMemberService memberService, 
            IAttendanceStatusService attendanceStatusService)
        {
            InitializeComponent();
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
            _memberService = memberService;
            _attendanceStatusService = attendanceStatusService;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void loadGeneralMeetingData(GeneralMeetingDTO generalMeetingDTO)
        {
            _generalMeetingDTO = generalMeetingDTO;
        }

        public void loadForEdit(int memberId, bool isUpdate)
        {
            _generalMeetingAttendanceDTO = _generalMeetingAttendanceService.GetPersonalAttendanceById(memberId, _generalMeetingDTO.GeneralMeetingId);
            _isUpdate = isUpdate;
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, label5, label6, labelTitle, btnClose, btnSave);

            GeneralHelper.ApplyRegularFont(14, txtAttendanceStatus, txtGender, txtMonthlyDues, txtName, txtSearchSurname, txtSurname);
        }

        private void loadMembers()
        {
            dataGridViewMembers.DataSource = _memberService.GetAllCurrentMembers();
            _memberDTO = _memberService.GetAllCurrentMembers();
            MemberHelper.ConfigureMemberGrid(dataGridViewMembers, MemberHelper.MemberGridType.Basic);
        }

        private void loadAttendanceStatuses()
        {
            dataGridViewAttendanceStatuses.DataSource = _attendanceStatusService.GetAll();
            AttendanceStatusHelper.ConfigureAttendanceStatusGrid(dataGridViewAttendanceStatuses, AttendanceStatusHelper.AttendanceStatusGridType.Basic);
        }

        private void FormAttendance_Load(object sender, EventArgs e)
        {
            resizeControls();

            loadAttendanceStatuses();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit member at meeting on " + _generalMeetingDTO.GeneralMeetingDate.ToString("dd.MM.yyyy");

                txtSurname.Text = _generalMeetingAttendanceDTO.LastName;
                txtName.Text = _generalMeetingAttendanceDTO.FirstName;
                txtGender.Text = _generalMeetingAttendanceDTO.Gender;
                txtAttendanceStatus.Text = _generalMeetingAttendanceDTO.AttendanceStatus;
                txtMonthlyDues.Text = _generalMeetingAttendanceDTO.DuesPaid.ToString();
                string imagePath = Application.StartupPath + "\\images\\" + _generalMeetingAttendanceDTO.ImagePath;
                picProfilePic.ImageLocation = imagePath;

                tableLayoutPanelMemberList.Hide();
                _memberId = _generalMeetingAttendanceDTO.MemberId;
            }
            else
            {
                labelTitle.Text = "Add member to meeting on " + _generalMeetingDTO.GeneralMeetingDate.ToString("dd.MM.yyyy");

                loadMembers();
            }
        }

        private void txtMonthlyDues_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearchSurname_TextChanged(object sender, EventArgs e)
        {
            string searchedText = txtSurname.Text.Trim().ToLower();
            var filtered = _memberDTO.Where(x => x.LastName.IndexOf(searchedText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewMembers.DataSource = filtered;
        }

        private void dataGridViewMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dataGridViewMembers.Rows[e.RowIndex];

            var selected = row.DataBoundItem as MemberFullDetailsDTO;

            if (selected == null)
                return;

            _memberId = selected.MemberId;
            txtSurname.Text = selected.LastName;
            txtName.Text = selected.FirstName;

            string imagePath = Application.StartupPath + "\\images\\" + selected.ImagePath;
            picProfilePic.ImageLocation = imagePath;
            txtGender.Text = selected.Gender;
        }

        private void dataGridViewAttendanceStatuses_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dataGridViewAttendanceStatuses.Rows[e.RowIndex];

            var selected = row.DataBoundItem as AttendanceStatusDTO;

            if (selected == null)
                return;

            _attendanceStatusId = selected.AttendanceStatusId;
            txtAttendanceStatus.Text = selected.AttendanceStatusName;
        }
        
        private void txtSearchSurname_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal expectedMonthlyDues = 10;
                decimal monthlyDues = decimal.TryParse(txtMonthlyDues.Text.Trim(), out decimal result) ? result: 0;
                decimal totalBalance = Convert.ToDecimal(expectedMonthlyDues - monthlyDues);

                if (!_isUpdate)
                {
                    var personalAttendance = new PersonalAttendance(_attendanceStatusId, _memberId, monthlyDues, expectedMonthlyDues,
                        totalBalance, _generalMeetingDTO.GeneralMeetingId, _generalMeetingDTO.GeneralMeetingDate);

                    _generalMeetingAttendanceService.Create(personalAttendance);
                    MessageBox.Show("Member added successfully!");
                }
                else
                {
                    var personalAttendance = PersonalAttendance.Rehydrate(_generalMeetingAttendanceDTO.PersonalAttendanceId, _attendanceStatusId, _memberId, monthlyDues, expectedMonthlyDues,
                        totalBalance, _generalMeetingDTO.GeneralMeetingId, _generalMeetingDTO.GeneralMeetingDate);

                    _generalMeetingAttendanceService.Update(personalAttendance);
                    MessageBox.Show("Member updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtExpectedMonthlyDue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }

        private void txtExpectedMonthlyDue_Click(object sender, EventArgs e)
        {

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
    }
}
