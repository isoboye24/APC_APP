using APC.BLL;
using APC.DAL.DTO;
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

namespace APC.AllForms
{
    public partial class FormViewGeneralAttendance : Form
    {
        public FormViewGeneralAttendance()
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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
            FillDataGrid();
        }
        PersonalAttendanceBLL bll = new PersonalAttendanceBLL();
        PersonalAttendanceDTO dto = new PersonalAttendanceDTO();
        public GeneralAttendanceDetailDTO detail = new GeneralAttendanceDetailDTO();
        PersonalAttendanceDetailDTO personalDetail = new PersonalAttendanceDetailDTO();
        private void FormViewAttendance_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label11.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalBalanceNew.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelTotalDuesExpected.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelTotalDuesPaid.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelTotalMembersAbsent.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            labelTotalMembersPresent.Font = new Font("Segoe UI", 24, FontStyle.Bold);

            rbEqual.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            rbLess.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            rbMore.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtMonthlyDues.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSummary.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            cmbAttendanceStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnAdd.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClear.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearch.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdate.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewSummary.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            dto = bll.Select();

            cmbAttendanceStatus.DataSource = dto.AttendanceStatuses;
            General.ComboBoxProps(cmbAttendanceStatus, "AttendanceStatusName", "AttendanceStatusID");

            dto = bll.SelectMembersSet(detail.GeneralAttendanceID);
            dataGridView1.DataSource = dto.PersonalAttendances;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Surname";
            dataGridView1.Columns[8].HeaderText = "Name";
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].HeaderText = "Gender";
            dataGridView1.Columns[12].HeaderText = "Status";
            dataGridView1.Columns[13].HeaderText = "Dues Paid";
            dataGridView1.Columns[14].HeaderText = "Expected Dues";
            dataGridView1.Columns[15].HeaderText = "Balance";
            dataGridView1.Columns[16].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }
            labelTitle.Text = "Meeting on " + detail.Day + "." + detail.MonthID + "." + detail.Year;
            txtSummary.Text = detail.Summary;
            ShowRecordData();
        }
        private void ShowRecordData()
        {
            General.ValueCount(labelTotalMembersPresent, detail.TotalMembersPresent, 100, 57);
            General.ValueCount(labelTotalMembersAbsent, detail.TotalMembersAbsent, 100, 57);

            labelTotalDuesPaid.Text = bll.DuesContributed(detail.GeneralAttendanceID).ToString();
            labelTotalDuesExpected.Text = bll.TotalDuesExpected(detail.GeneralAttendanceID).ToString();
            decimal balance = bll.TotalDuesExpected(detail.GeneralAttendanceID) - bll.DuesContributed(detail.GeneralAttendanceID);

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

        private void FillDataGrid()
        {
            bll = new PersonalAttendanceBLL();
            dto = bll.SelectMembersSet(detail.GeneralAttendanceID);
            dataGridView1.DataSource = dto.PersonalAttendances;

            ShowRecordData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormPersonalAttendance open = new FormPersonalAttendance();
            open.generalAttendanceDetail = detail;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            FillDataGrid();
            ShowRecordData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (personalDetail.AttendanceID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormPersonalAttendance open = new FormPersonalAttendance();
                open.personalDetail = personalDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                FillDataGrid();
                ShowRecordData();
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            personalDetail = new PersonalAttendanceDetailDTO();
            personalDetail.AttendanceID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            personalDetail.AttendanceStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            personalDetail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            personalDetail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            personalDetail.MonthName = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            personalDetail.Year = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            personalDetail.MemberID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            personalDetail.Surname = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            personalDetail.Name = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            personalDetail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            personalDetail.GenderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            personalDetail.Gender = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            personalDetail.AttendanceStatusName = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
            personalDetail.MonthlyDue = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
            personalDetail.ExpectedDue = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            personalDetail.Balance = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[15].Value);
            personalDetail.GeneralAttendanceID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[16].Value);
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
