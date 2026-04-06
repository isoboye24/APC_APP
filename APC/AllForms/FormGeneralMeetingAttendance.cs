using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.BLL;
using APC.DAL.DTO;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        private bool _isUpdate = false;

        public FormGeneralMeetingAttendance(IGeneralMeetingAttendanceService generalMeetingAttendanceService, IMemberService memberService, IAttendanceStatusService attendanceStatusService)
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
        public void loadForEdit(GeneralMeetingAttendanceDTO dto, bool isUpdate)
        {
            _generalMeetingAttendanceDTO = dto;
            _isUpdate = isUpdate;
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, label5, label6, labelTitle, btnClose, btnSave);

            GeneralHelper.ApplyRegularFont(14, txtAttendanceStatus, txtGender, txtMonthlyDues, txtName, txtSearchSurname, txtSurname);
        }

        private void loadMembers()
        {
            dataGridViewMembers.DataSource = _memberService.GetAll();
            MemberHelper.ConfigureMemberGrid(dataGridViewMembers, MemberHelper.MemberGridType.Basic);
        }

        private void FormAttendance_Load(object sender, EventArgs e)
        {
            loadMembers();

            if (_isUpdate)
            {
                txtSurname.Text = _generalMeetingAttendanceDTO.LastName;
                txtName.Text = _generalMeetingAttendanceDTO.FirstName;
                txtGender.Text = _generalMeetingAttendanceDTO.Gender;
                txtAttendanceStatus.Text = _generalMeetingAttendanceDTO.AttendanceStatus;
                txtMonthlyDues.Text = _generalMeetingAttendanceDTO.DuesPaid.ToString();
                string imagePath = Application.StartupPath + "\\images\\" + _generalMeetingAttendanceDTO.ImagePath;
                picProfilePic.ImageLocation = imagePath;

                tableLayoutPanelMemberList.Hide();
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
            List<MemberDetailDTO> list = dto.Members;
            list = list.Where(x => x.Surname.Contains(txtSearchSurname.Text)).ToList();
            dataGridViewMembers.DataSource = list;
        }

        private void dataGridViewMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            memberDetail = new MemberDetailDTO();
            memberDetail.MemberID = Convert.ToInt32(dataGridViewMembers.Rows[e.RowIndex].Cells[0].Value);
            txtSurname.Text = dataGridViewMembers.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtName.Text = dataGridViewMembers.Rows[e.RowIndex].Cells[4].Value.ToString();
            memberDetail.ImagePath = dataGridViewMembers.Rows[e.RowIndex].Cells[6].Value.ToString();
            string imagePath = Application.StartupPath + "\\images\\" + memberDetail.ImagePath;
            picProfilePic.ImageLocation = imagePath;
            txtGender.Text = dataGridViewMembers.Rows[e.RowIndex].Cells[19].Value.ToString();
        }

        private void dataGridViewAttendanceStatuses_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            attStatusDetail = new AttendanceStatusDetailDTO();
            attStatusDetail.AttendanceStatusID = Convert.ToInt32(dataGridViewAttendanceStatuses.Rows[e.RowIndex].Cells[0].Value);
            txtAttendanceStatus.Text = dataGridViewAttendanceStatuses.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        
        private void txtSearchSurname_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (memberDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            if (attStatusDetail.AttendanceStatusID == 0)
            {
                MessageBox.Show("Please choose an attendance status from the table");
            }            
            else
            {
                if (!isUpdate)
                {
                    bool isUnique = bll.IsUnique(memberDetail.MemberID, generalAttendanceDetail.GeneralAttendanceID);
                    if (!isUnique)
                    {
                        MessageBox.Show("This member has been added");
                    }
                    else
                    {
                        PersonalAttendanceDetailDTO attendance = new PersonalAttendanceDetailDTO();
                        attendance.AttendanceStatusID = attStatusDetail.AttendanceStatusID;
                        attendance.MemberID = memberDetail.MemberID;
                        attendance.ExpectedDue = 10;
                        if (txtMonthlyDues.Text.Trim() == "")
                        {
                            attendance.MonthlyDue = 0;
                        }
                        else
                        {
                            attendance.MonthlyDue = Convert.ToDecimal(txtMonthlyDues.Text);
                        }
                        attendance.Balance = attendance.ExpectedDue - attendance.MonthlyDue;
                        attendance.Day = generalAttendanceDetail.Day;
                        attendance.MonthID = generalAttendanceDetail.MonthID;
                        attendance.Year = generalAttendanceDetail.Year;
                        attendance.GeneralAttendanceID = generalAttendanceDetail.GeneralAttendanceID;
                        if (bll.Insert(attendance))
                        {
                            MessageBox.Show("Attendance was added");
                            txtMonthlyDues.Clear();
                        }               
                    }
                }
                else if (isUpdate)
                {
                    if (personalDetail.AttendanceStatusName == txtAttendanceStatus.Text && personalDetail.MonthlyDue == Convert.ToDecimal(txtMonthlyDues.Text))
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        personalDetail.AttendanceStatusID = attStatusDetail.AttendanceStatusID;
                        personalDetail.ExpectedDue = 10;
                        if (txtMonthlyDues.Text.Trim() == "")
                        {
                            personalDetail.MonthlyDue = 0;
                        }
                        else
                        {
                            personalDetail.MonthlyDue = Convert.ToDecimal(txtMonthlyDues.Text);
                        }
                        personalDetail.Balance = personalDetail.ExpectedDue - personalDetail.MonthlyDue;
                        personalDetail.Day = personalDetail.Day;
                        personalDetail.MonthID = personalDetail.MonthID;
                        personalDetail.Year = personalDetail.Year;
                        personalDetail.GeneralAttendanceID = personalDetail.GeneralAttendanceID;
                        if (bll.Update(personalDetail))
                        {
                            MessageBox.Show("The member was updated");
                            this.Close();
                        }
                    }
                }
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
