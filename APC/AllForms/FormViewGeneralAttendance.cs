using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Applications.Services;
using APC.BLL;
using APC.DAL.DTO;
using APC.Helper;
using OfficeOpenXml.Drawing.Chart;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormViewGeneralAttendance : Form
    {
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IAttendanceStatusService _attendanceStatusService;
        private readonly IMemberService _memberService;

        private readonly GeneralMeetingDTO _generalMeetingDTO;

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
            loadAttendances();

            ShowRecordData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, label5, label7, label9, label10, label11, btnAdd,
                labelTitle, btnClose, rbEqual, rbLess, rbMore, btnClear, btnSearch, btnUpdate, btnViewSummary);

            GeneralHelper.ApplyRegularFont(14, txtName, txtSurname, txtMonthlyDues, txtSummary, cmbAttendanceStatus);

            GeneralHelper.ApplyBoldFont(24, labelTotalBalanceNew, labelTotalDuesExpected, labelTotalDuesPaid, labelTotalMembersAbsent,
                labelTotalMembersPresent);
        }

        private void loadAttendances()
        {
            dataGridView1.DataSource = _generalMeetingAttendanceService.GetAllByGeneralMeetingId(_generalMeetingDTO.GeneralMeetingId);
            PersonalAttendanceHelper.ConfigurePersonalAttendanceGrid(dataGridView1, PersonalAttendanceHelper.PersonalAttendanceGridType.Basic);
        }

        private void FormViewAttendance_Load(object sender, EventArgs e)
        {
            resizeControls();

            cmbAttendanceStatus.DataSource = _attendanceStatusService.GetAll();
            GeneralHelper.ComboBoxProps(cmbAttendanceStatus, "AttendanceStatusName", "AttendanceStatusId");

            loadAttendances();

            labelTitle.Text = "Meeting on " + _generalMeetingDTO.Day + "." + _generalMeetingDTO.MonthID + "." + _generalMeetingDTO.Year;
            txtSummary.Text = _generalMeetingDTO.Summary;
            ShowRecordData();
        }
        private void ShowRecordData()
        {
            labelTotalMembersPresent.Text = _generalMeetingDTO.TotalMembersPresent.ToString();
            labelTotalMembersAbsent.Text = _generalMeetingDTO.TotalMembersAbsent.ToString();

            labelTotalDuesPaid.Text = _generalMeetingDTO.FormattedTotalDuesPaid;
            labelTotalDuesExpected.Text = _generalMeetingDTO.FormattedTotalDuesExpected;
            labelTotalMembersAbsent.Text = _generalMeetingDTO.TotalMembersAbsent.ToString();

            decimal balance =_generalMeetingDTO.TotalDuesExpected - _generalMeetingDTO.TotalDuesPaid;

            if (balance > 0)
            {
                labelBalanceStatus.Text = "Remaining";
                labelTotalBalanceNew.Text = balance.ToString();
            }
            else if (balance < 0)
            {
                labelBalanceStatus.Text = "Extra";
                labelTotalBalanceNew.Text = ((-1) * balance).ToString();
            }
            else
            {
                labelBalanceStatus.Text = "Exact";
                labelTotalBalanceNew.Text = balance.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new FormGeneralMeetingAttendance(_generalMeetingAttendanceService, _memberService, _attendanceStatusService);
            form.ShowDialog();

            ClearFilters();
        }

        private Applications.DTO.GeneralMeetingAttendanceDTO GetSelectedMeetingAttendance()
        {
            if (dataGridView1.CurrentRow == null)
                return null;

            return dataGridView1.CurrentRow.DataBoundItem as Applications.DTO.GeneralMeetingAttendanceDTO;
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
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();

        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            List<PersonalAttendanceDetailDTO> list = dto.PersonalAttendances;
            list = list.Where(x => x.Surname.Contains(txtSurname.Text.Trim())).ToList();
            dataGridView1.DataSource = list;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            List<PersonalAttendanceDetailDTO> list = dto.PersonalAttendances;
            list = list.Where(x => x.Name.Contains(txtName.Text.Trim())).ToList();
            dataGridView1.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<PersonalAttendanceDetailDTO> list = dto.PersonalAttendances;
            if (cmbAttendanceStatus.SelectedIndex != -1)
            {
                list = list.Where(x => x.AttendanceStatusID == Convert.ToInt32(cmbAttendanceStatus.SelectedValue)).ToList();
            }
            if (txtMonthlyDues.Text.Trim() != "")
            {
                if (rbEqual.Checked)
                {
                    list = list.Where(x => x.MonthlyDue == Convert.ToDecimal(txtMonthlyDues.Text)).ToList();
                }
                if (rbLess.Checked)
                {
                    list = list.Where(x => x.MonthlyDue < Convert.ToDecimal(txtMonthlyDues.Text)).ToList();
                }
                if (rbMore.Checked)
                {
                    list = list.Where(x => x.MonthlyDue > Convert.ToDecimal(txtMonthlyDues.Text)).ToList();
                }
            }
            dataGridView1.DataSource = list;
        }

        private void btnViewSummary_Click(object sender, EventArgs e)
        {
            FormViewMeetingsSummary open = new FormViewMeetingsSummary();            
            open.detail = detail;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            FillDataGrid();
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
