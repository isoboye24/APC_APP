using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace APC.AllForms
{
    public partial class FormViewPersonalAttendances : Form
    {
        public FormViewPersonalAttendances()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        PersonalAttendanceBLL bll = new PersonalAttendanceBLL();
        PersonalAttendanceDTO dto = new PersonalAttendanceDTO();
        public MemberDetailDTO detail = new MemberDetailDTO();
        MemberBLL memberBLL = new MemberBLL();
        public bool isPresent = false;
        public bool isAbsent = false;
        public bool isAmountContributed = false;
        public bool isAmountExpected = false;
        public bool isPersonalBalance = false;
        public bool isPersonalFines = false;
        decimal amountContributed = 0;
        decimal amountExpected = 0;
        decimal Balance = 0;

        decimal paidFinesAmount = 0;
        decimal expectedFineAmount = 0;
        decimal fineBalance = 0;

        private void FormViewPersonalAttendances_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalAmount.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalName.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            rbEqualAttend.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            rbLessAttend.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            rbMoreAttend.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            txtAmount.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            cmbMonth.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            cmbYear.Font = new Font("Segoe UI", 14, FontStyle.Regular);   
            
            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            dto = bll.Select(detail.MemberID);
            cmbMonth.DataSource = dto.Months;
            General.ComboBoxProps(cmbMonth, "MonthName", "MonthID");
            cmbYear.DataSource = dto.Years;
            cmbYear.SelectedIndex = -1;
            tableLayoutPanelChangingAmount.Hide();
            tableLayoutPanelTotal.Hide();

            amountContributed = memberBLL.GetAmountContributed(detail.MemberID);
            amountExpected = memberBLL.GetAmountExpected(detail.MemberID);
            Balance = amountExpected - amountContributed;

            paidFinesAmount = memberBLL.GetFinedAmountPaid(detail.MemberID);
            expectedFineAmount = memberBLL.GetFinedAmountExpected(detail.MemberID);
            fineBalance = expectedFineAmount - paidFinesAmount;

            if (isPresent)
            {
                dataGridView1.DataSource = dto.PresentMember;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Month";
                dataGridView1.Columns[5].HeaderText = "Year";
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].HeaderText = "Gender";
                dataGridView1.Columns[12].HeaderText = "Att. Status";
                dataGridView1.Columns[13].HeaderText = "Monthly Dues";
                dataGridView1.Columns[14].HeaderText = "Expected Dues";
                dataGridView1.Columns[15].HeaderText = "Balance";
                dataGridView1.Columns[16].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                }
                labelTitle.Text = detail.Surname + " " + detail.Name + "'s present attendance record";
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfile.ImageLocation = imagePath;
            }
            if (isAbsent)
            {
                dataGridView1.DataSource = dto.AbsentMember;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Month";
                dataGridView1.Columns[5].HeaderText = "Year";
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].HeaderText = "Gender";
                dataGridView1.Columns[12].HeaderText = "Att. Status";
                dataGridView1.Columns[13].HeaderText = "Monthly Dues";
                dataGridView1.Columns[14].HeaderText = "Expected Dues";
                dataGridView1.Columns[15].HeaderText = "Balance";
                dataGridView1.Columns[16].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                }
                labelTitle.Text = detail.Surname + " " + detail.Name + "'s absent attendance record";
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfile.ImageLocation = imagePath;
            }
            if (isAmountContributed)
            {
                dataGridView1.DataSource = dto.AmountsContributed;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Month";
                dataGridView1.Columns[5].HeaderText = "Year";
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].HeaderText = "Gender";
                dataGridView1.Columns[12].HeaderText = "Att. Status";
                dataGridView1.Columns[13].HeaderText = "Monthly Dues";
                dataGridView1.Columns[14].HeaderText = "Expected Dues";
                dataGridView1.Columns[15].HeaderText = "Balance";
                dataGridView1.Columns[16].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                }
                labelTitle.Text = detail.Surname + " " + detail.Name + "'s contributed amount record";
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfile.ImageLocation = imagePath;
                tableLayoutPanelChangingAmount.Visible = true;
                tableLayoutPanelTotal.Visible = true;
                labelTotalAmount.Text = "€" + amountContributed;
                labelTotalName.Text = "Total Amt. Contributed";
            }
            if (isAmountExpected)
            {
                dataGridView1.DataSource = dto.AmountExpected;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Month";
                dataGridView1.Columns[5].HeaderText = "Year";
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].HeaderText = "Gender";
                dataGridView1.Columns[12].HeaderText = "Att. Status";
                dataGridView1.Columns[13].HeaderText = "Monthly Dues";
                dataGridView1.Columns[14].HeaderText = "Expected Dues";
                dataGridView1.Columns[15].HeaderText = "Balance";
                dataGridView1.Columns[16].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                }
                labelTitle.Text = detail.Surname + " " + detail.Name + "'s expected amount record";
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfile.ImageLocation = imagePath;
                tableLayoutPanelTotal.Visible = true;
                labelTotalAmount.Text = "€ 120.00";
                labelTotalName.Text = "Total Amt. Expected per Year";
            }
            if (isPersonalBalance)
            {
                dataGridView1.DataSource = dto.AmountsBalance;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Month";
                dataGridView1.Columns[5].HeaderText = "Year";
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].HeaderText = "Gender";
                dataGridView1.Columns[12].HeaderText = "Att. Status";
                dataGridView1.Columns[13].HeaderText = "Monthly Dues";
                dataGridView1.Columns[14].HeaderText = "Expected Dues";
                dataGridView1.Columns[15].HeaderText = "Balance";
                dataGridView1.Columns[16].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                }
                labelTitle.Text = detail.Surname + " " + detail.Name + "'s balance amount record";
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfile.ImageLocation = imagePath;
                tableLayoutPanelChangingAmount.Visible = true;
                tableLayoutPanelTotal.Visible = true;
                labelTotalAmount.Text = "€" + (120 - amountContributed);
                labelTotalName.Text = "Remaining Amt.";
            }
            if (isPersonalFines)
            {
                dataGridView1.DataSource = dto.FinedMember;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Name";
                dataGridView1.Columns[2].HeaderText = "Surname";
                dataGridView1.Columns[3].HeaderText = "Violated";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Fine";
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].HeaderText = "Paid";
                dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].HeaderText = "Balance";
                dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[10].HeaderText = "Status";
                dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                dataGridView1.Columns[14].Visible = false;
                dataGridView1.Columns[15].Visible = false;
                dataGridView1.Columns[16].Visible = false;
                dataGridView1.Columns[17].Visible = false;
                dataGridView1.Columns[18].Visible = false;
                dataGridView1.Columns[19].HeaderText = "Day";
                dataGridView1.Columns[19].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[20].Visible = false;
                dataGridView1.Columns[21].HeaderText = "Month";
                dataGridView1.Columns[21].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[22].HeaderText = "Year";
                dataGridView1.Columns[22].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[23].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                }
                labelTitle.Text = detail.Surname + " " + detail.Name + "'s fine records";
                string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                picProfile.ImageLocation = imagePath;
                tableLayoutPanelChangingAmount.Visible = true;
                tableLayoutPanelTotal.Visible = true;
                labelTotalAmount.Text = "€ " + fineBalance;
                labelTotalName.Text = "Total Fines";
            }
        }

        private void ClearFilters()
        {
            cmbYear.DataSource = dto.Years;
            cmbYear.SelectedIndex = -1;
            cmbMonth.DataSource = dto.Months;
            cmbMonth.SelectedIndex = -1;
            txtAmount.Clear();
            rbEqualAttend.Checked = false;
            rbLessAttend.Checked = false;
            rbMoreAttend.Checked = false;
            bll = new PersonalAttendanceBLL();
            dto = bll.Select(detail.MemberID);
            if (isPresent)
            {
                dataGridView1.DataSource = dto.PresentMember;
            }
            else if (isAbsent)
            {
                dataGridView1.DataSource = dto.AbsentMember;
            }
            else if (isAmountContributed)
            {
                dataGridView1.DataSource = dto.AmountsContributed;
            }
            else if (isAmountExpected)
            {
                dataGridView1.DataSource = dto.AmountExpected;
            }
            else if (isPersonalBalance)
            {
                dataGridView1.DataSource = dto.AmountsBalance;
            }
            else if (isPersonalFines)
            {
                dataGridView1.DataSource = dto.FinedMember;                
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dto = bll.Select(detail.MemberID);

            List<PersonalAttendanceDetailDTO> list = dto.PresentMember;
            List<FinedMemberDetailDTO> fineMemberlist = dto.FinedMember;
            if (cmbMonth.SelectedIndex != -1 && cmbYear.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonth.SelectedValue) && x.Year.Contains(cmbYear.SelectedValue.ToString())).ToList();
                if (isPersonalFines)
                {
                    fineMemberlist = fineMemberlist.Where(x => x.MonthID == Convert.ToInt32(cmbMonth.SelectedValue) && x.Year.ToString().Contains(cmbYear.SelectedValue.ToString())).ToList();
                }
            }
            if (cmbMonth.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonth.SelectedValue)).ToList();
                if (isPersonalFines)
                {
                    fineMemberlist = fineMemberlist.Where(x => x.MonthID == Convert.ToInt32(cmbMonth.SelectedValue)).ToList();
                }
            }
            if (cmbYear.SelectedIndex != -1)
            {
                list = list.Where(x => x.Year.Contains(cmbYear.SelectedValue.ToString())).ToList();
                if (isPersonalFines)
                {
                    fineMemberlist = fineMemberlist.Where(x => x.Year.ToString().Contains(cmbYear.SelectedValue.ToString())).ToList();
                }
            }
            if (txtAmount.Text.Trim() != "")
            {
                if (isAmountContributed)
                {
                    if (rbLessAttend.Checked)
                    {
                        list = list.Where(x => x.MonthlyDue < Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else if (rbEqualAttend.Checked)
                    {
                        list = list.Where(x => x.MonthlyDue == Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else if (rbMoreAttend.Checked)
                    {
                        list = list.Where(x => x.MonthlyDue > Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else
                    {
                        MessageBox.Show("There is no result for amount contributed search");
                    }
                }
                if (isPersonalBalance)
                {
                    if (rbLessAttend.Checked)
                    {
                        list = list.Where(x => x.Balance < Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else if (rbEqualAttend.Checked)
                    {
                        list = list.Where(x => x.Balance == Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else if (rbMoreAttend.Checked)
                    {
                        list = list.Where(x => x.Balance > Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else
                    {
                        MessageBox.Show("There is no result for amount contributed search");
                    }
                }
                if (isPersonalFines)
                {
                    if (rbLessAttend.Checked)
                    {
                        fineMemberlist = fineMemberlist.Where(x => x.Balance < Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else if (rbEqualAttend.Checked)
                    {
                        fineMemberlist = fineMemberlist.Where(x => x.Balance == Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else if (rbMoreAttend.Checked)
                    {
                        fineMemberlist = fineMemberlist.Where(x => x.Balance > Convert.ToDecimal(txtAmount.Text.Trim())).ToList();
                    }
                    else
                    {
                        MessageBox.Show("There is no result for amount contributed search");
                    }
                }
            }
            if (!isPersonalFines)
            {
                dataGridView1.DataSource = list;
            }    
            else if (isPersonalFines)
            {
                dataGridView1.DataSource = fineMemberlist;
            }
        }

        private void tableLayoutPanelChangingAmount_Paint(object sender, PaintEventArgs e)
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
