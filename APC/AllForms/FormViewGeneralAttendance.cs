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

        private GeneralMeetingDTO _generalMeetingDTO;

        private List<GeneralMeetingAttendanceDTO> _generalMeetingAttendanceDTOs;

        public FormViewGeneralAttendance(IGeneralMeetingAttendanceService generalMeetingAttendanceService, IMemberService memberService,
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

        public void loadForView(GeneralMeetingDTO generalMeetingDTO)
        {
            _generalMeetingDTO = generalMeetingDTO;
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, label5, label7, label9, btnAdd,
                labelTitle, btnClose, rbEqual, rbLess, rbMore, btnClear, btnSearch, btnUpdate, btnViewSummary);

            GeneralHelper.ApplyRegularFont(14, txtName, txtSurname, txtMonthlyDues, txtSummary, cmbAttendanceStatus, labelTotalMembers);

            GeneralHelper.ApplyBoldFont(24, labelTotalDuesPaid, labelTotalMembersAbsent, labelTotalMembersPresent);
        }

        private void loadGeneralMeetingAttendances()
        {
            dataGridView1.DataSource = _generalMeetingAttendanceService.GetAllByGeneralMeetingId(_generalMeetingDTO.GeneralMeetingId);
            _generalMeetingAttendanceDTOs = _generalMeetingAttendanceService.GetAllByGeneralMeetingId(_generalMeetingDTO.GeneralMeetingId);
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
            labelTotalMembers.Text = dataGridView1.RowCount.ToString() + " Member" + (dataGridView1.RowCount > 1 ? "s" : "");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new FormGeneralMeetingAttendance(_generalMeetingAttendanceService, _memberService, _attendanceStatusService);
            form.loadGeneralMeetingData(_generalMeetingDTO);
            form.ShowDialog();

            ClearFilters();
            ShowRecordData();
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

            var form = new FormGeneralMeetingAttendance(_generalMeetingAttendanceService, _memberService, _attendanceStatusService);
            form.loadGeneralMeetingData(_generalMeetingDTO);
            form.loadForEdit(selected.MemberId, true);
            form.ShowDialog();

            ClearFilters();
            ShowRecordData();

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

            if (cmbAttendanceStatus.SelectedValue != null)
            {
                string searchedStatus = cmbAttendanceStatus.Text;

                filtered = filtered.Where(x => x.AttendanceStatus == searchedStatus);
            }

            if (!string.IsNullOrWhiteSpace(txtMonthlyDues.Text))
            {
                if (!decimal.TryParse(txtMonthlyDues.Text, out decimal dues))
                {
                    MessageBox.Show("Please enter a valid amount.");
                    return;
                }

                if (rbEqual.Checked)
                {
                    filtered = filtered.Where(x => x.DuesPaid == dues);
                }
                else if (rbLess.Checked)
                {
                    filtered = filtered.Where(x => x.DuesPaid < dues);
                }
                else if (rbMore.Checked)
                {
                    filtered = filtered.Where(x => x.DuesPaid > dues);
                }
                else
                {
                    MessageBox.Show("Please select a criterion from the monthly dues group");
                    return;
                }
            }

            dataGridView1.DataSource = filtered.ToList();
            ShowRecordData();
        }

        private void btnViewSummary_Click(object sender, EventArgs e)
        {
            var form = new FormViewMeetingsSummary();
            form.loadForView(_generalMeetingDTO);
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
