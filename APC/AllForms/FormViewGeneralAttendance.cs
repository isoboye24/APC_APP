using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewGeneralAttendance : Form
    {
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IAttendanceStatusService _attendanceStatusService;
        private readonly IMemberService _memberService;

        private readonly GeneralMeetingDTO _generalMeetingDTO;

        private List<Applications.DTO.GeneralMeetingAttendanceDTO> _generalMeetingAttendanceDTOs;

        public FormViewGeneralAttendance(GeneralMeetingDTO generalMeetingDTO, IGeneralMeetingAttendanceService generalMeetingAttendanceService, IMemberService memberService)
        {
            InitializeComponent();
            _generalMeetingDTO = generalMeetingDTO;
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
            _memberService = memberService;
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
        private void ClearFilters()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtMonthlyDues.Clear();
            rbLess.Checked = false;
            rbMore.Checked = false;
            rbEqual.Checked = false;
            cmbAttendanceStatus.SelectedIndex = -1;
            loadGeneralMeetingAttendances();

            ShowRecordData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, label5, label7, label9, btnAdd,
                labelTitle, btnClose, rbEqual, rbLess, rbMore, btnClear, btnSearch, btnUpdate, btnViewSummary);

            GeneralHelper.ApplyRegularFont(14, txtName, txtSurname, txtMonthlyDues, txtSummary, cmbAttendanceStatus);

            GeneralHelper.ApplyBoldFont(24, labelTotalDuesPaid, labelTotalMembersAbsent, labelTotalMembersPresent);
        }

        private void loadGeneralMeetingAttendances()
        {
            dataGridView1.DataSource = _generalMeetingAttendanceService.GetAllByGeneralMeetingId(_generalMeetingDTO.GeneralMeetingId);
            PersonalAttendanceHelper.ConfigurePersonalAttendanceGrid(dataGridView1, PersonalAttendanceHelper.PersonalAttendanceGridType.Basic);
        }

        private void FormViewAttendance_Load(object sender, EventArgs e)
        {
            resizeControls();

            cmbAttendanceStatus.DataSource = _attendanceStatusService.GetAll();
            GeneralHelper.ComboBoxProps(cmbAttendanceStatus, "AttendanceStatusName", "AttendanceStatusId");

            loadGeneralMeetingAttendances();

            labelTitle.Text = "Meeting on " + _generalMeetingDTO.Day + "." + _generalMeetingDTO.MonthId + "." + _generalMeetingDTO.Year;
            txtSummary.Text = _generalMeetingDTO.Summary;
            ShowRecordData();
        }

        private void ShowRecordData()
        {
            labelTotalMembersPresent.Text = _generalMeetingDTO.TotalMembersPresent.ToString();
            labelTotalMembersAbsent.Text = _generalMeetingDTO.TotalMembersAbsent.ToString();

            labelTotalDuesPaid.Text = _generalMeetingDTO.FormattedTotalDuesPaid;
            labelTotalMembersAbsent.Text = _generalMeetingDTO.TotalMembersAbsent.ToString();

            decimal balance =_generalMeetingDTO.TotalDuesExpected - _generalMeetingDTO.TotalDuesPaid;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new FormGeneralMeetingAttendance(_generalMeetingAttendanceService, _memberService, _attendanceStatusService, _generalMeetingDTO);
            form.ShowDialog();

            ClearFilters();
        }

        private GeneralMeetingAttendanceDTO GetSelectedMeetingAttendance()
        {
            if (dataGridView1.CurrentRow == null)
                return null;

            return dataGridView1.CurrentRow.DataBoundItem as GeneralMeetingAttendanceDTO;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMeetingAttendance();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormGeneralMeetingAttendance(_generalMeetingAttendanceService, _memberService, _attendanceStatusService, _generalMeetingDTO);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();

        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            string searchedText = txtSurname.Text.Trim().ToLower();
            var filtered = _generalMeetingAttendanceDTOs.Where(x => x.LastName.IndexOf(searchedText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string searchedText = txtName.Text.Trim().ToLower();
            var filtered = _generalMeetingAttendanceDTOs.Where(x => x.FirstName.IndexOf(searchedText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var filtered = _generalMeetingAttendanceDTOs.AsQueryable();

            if (cmbAttendanceStatus.SelectedIndex != -1)
            {
                string searchedStatus = cmbAttendanceStatus.SelectedValue.ToString();

                filtered = filtered.Where(x => x.AttendanceStatus == searchedStatus);
            }
            if (txtMonthlyDues.Text.Trim() != "")
            {
                if (rbEqual.Checked)
                {
                    filtered = filtered.Where(x => x.DuesPaid == Convert.ToDecimal(txtMonthlyDues.Text));
                }
                else if (rbLess.Checked)
                {
                    filtered = filtered.Where(x => x.DuesPaid < Convert.ToDecimal(txtMonthlyDues.Text));
                }
                else if (rbMore.Checked)
                {
                    filtered = filtered.Where(x => x.DuesPaid > Convert.ToDecimal(txtMonthlyDues.Text));
                }
                else
                {
                    MessageBox.Show("Please select a criterion from the monthly dues group");
                }
            }
            dataGridView1.DataSource = filtered.ToList();
        }

        private void btnViewSummary_Click(object sender, EventArgs e)
        {
            var form = new FormViewMeetingsSummary(_generalMeetingDTO);
            form.ShowDialog();

            ClearFilters();
            ShowRecordData();            
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
