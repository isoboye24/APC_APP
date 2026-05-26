using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using APC.Helper;
using OfficeOpenXml.Drawing.Slicer.Style;
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
using static APC.Helper.FinedMemberHelper;
using static APC.Helper.PersonalAttendanceHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace APC.AllForms
{
    public partial class FormViewPersonalAttendances : Form
    {
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IMemberService _memberService;
        private readonly IMonthService _monthService;
        private readonly IGeneralMeetingService _generalMeetingService;
        private readonly IFinancialReportService _financialReportService;
        private readonly IFinedMemberService _finedMemberService;
        private readonly IPersonalAttendanceService _personalAttendanceService;

        private List<PersonalAttendanceDetailsDTO> _personalAttendanceDetailDTOs;
        private MemberFullDetailsDTO _memberFullDetailsDTO;
        private List<PersonalAttendanceDetailsDTO> _filteredPresents;
        private List<PersonalAttendanceDetailsDTO> _filteredAbsents;
        private List<PersonalAttendanceDetailsDTO> _filteredAnnualContribution;
        private List<Applications.DTO.FinedMemberDTO> _finedMemberDTOs;

        private int _memberId = 0;
        private bool _isPresent = false;
        private bool _isAbsent = false;
        private bool _isAmountContributed = false;
        private bool _isAmountExpected = false;

        private bool _isPersonalBalance = false;
        private bool _isPersonalFines = false;
        private decimal amountContributed = 0;
        private decimal amountExpected = 0;
        private decimal Balance = 0;

        private decimal paidFinesAmount = 0;
        private decimal expectedFineAmount = 0;
        private decimal fineBalance = 0;
        private int currYear = DateTime.Today.Year;

        public FormViewPersonalAttendances(IGeneralMeetingAttendanceService generalMeetingAttendanceService, IMemberService memberService,
            IMonthService monthService, IGeneralMeetingService generalMeetingService, IFinancialReportService financialReportService, 
            IFinedMemberService finedMemberService)
        {
            InitializeComponent();
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
            _memberService = memberService;
            _monthService = monthService;
            _generalMeetingService = generalMeetingService;
            _financialReportService = financialReportService;
            _finedMemberService = finedMemberService;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void GetMemberId(int memberId)
        {
            _memberId = memberId;
            _memberFullDetailsDTO = _memberService.GetMemberById(memberId);
        }
        
        public void IsPresent(bool isPresent)
        {
            _isPresent = isPresent;
        }
        
        public void IsAbsent(bool isAbsent)
        {
            _isAbsent = isAbsent;
        }
        
        public void IsAmountContributed(bool isAmountContributed)
        {
            _isAmountContributed = isAmountContributed;
        }
        
        public void IsAmountExpected(bool isAmountExpected)
        {
            _isAmountExpected = isAmountExpected;
        }
        
        public void IsPersonalBalance(bool isPersonalBalance)
        {
            _isPersonalBalance = isPersonalBalance;
        }
        
        public void IsPersonalFines(bool isPersonalFines)
        {
            _isPersonalFines = isPersonalFines;
        }

        private void controlFonts()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, label5, labelTotalAmount, btnClose);
            GeneralHelper.ApplyRegularFont(14, labelTotalName, txtAmount, cmbMonth, cmbYear);
            GeneralHelper.ApplyBoldFont(11, rbEqualAmount, rbLessAmount, rbMoreAmount);
        }



        private void loadMemberAttendanceDetails()
        {
            dataGridView1.DataSource = _personalAttendanceService.GetAnnualGeneralMeetingAttendanceById(_memberId, currYear);
            _personalAttendanceDetailDTOs = _personalAttendanceService.GetTotalGeneralMeetingAttendanceById(_memberId);
            ConfigurePersonalAttendanceGrid(dataGridView1, PersonalAttendanceGridType.Details);
        }
        
        private void loadMemberAttendancePresent()
        {
            string search = "Present";
            var filtered = _personalAttendanceDetailDTOs.Where(x => x.AttendanceStatus.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            _filteredPresents = filtered;
            dataGridView1.DataSource = filtered;
        }
        
        private void loadMemberAttendanceAnnualPresent(int year)
        {
            string search = "Present";
            var filtered = _personalAttendanceDetailDTOs.Where(x => x.Year == year && x.AttendanceStatus.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void loadMemberAttendanceAbsent()
        {
            string search = "Absent";
            var filtered = _personalAttendanceDetailDTOs.Where(x => x.AttendanceStatus.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            _filteredAbsents = filtered;
            dataGridView1.DataSource = filtered;
        }

        private void loadMemberAttendanceAnnualAbsent(int year)
        {
            string search = "Absent";
            var filtered = _personalAttendanceDetailDTOs.Where(x => x.Year == year && x.AttendanceStatus.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridView1.DataSource = filtered;
        }

        private void loadMemberContributionDetails()
        {
            _filteredAnnualContribution = _personalAttendanceService.GetAnnualGeneralMeetingAttendanceById(_memberId, currYear);
            var filtered = _filteredAnnualContribution.AsQueryable();

            filtered = filtered.Where(x => x.MonthlyDues > 0);
            dataGridView1.DataSource = filtered.ToList();
            ConfigurePersonalAttendanceGrid(dataGridView1, PersonalAttendanceGridType.Details);
        }

        private void loadMemberFineDetails()
        {
            dataGridView1.DataSource = _finedMemberService.GetAnnualFineListsById(_memberId, currYear);
            _finedMemberDTOs = _finedMemberService.GetAllFineListsById(_memberId);
            ConfigureFinedMemberGrid(dataGridView1, FinedMemberGridType.PersonalDetails);
        }

        private void FormViewPersonalAttendances_Load(object sender, EventArgs e)
        {
            controlFonts();

            cmbMonth.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonth, "MonthName", "MonthID");
            cmbYear.DataSource = _generalMeetingService.GetMeetingYears();
            cmbYear.SelectedIndex = -1;
            tableLayoutPanelChangingAmount.Hide();
            tableLayoutPanelTotal.Hide();

            amountContributed = _financialReportService.GetTotalAnnualDuesById(_memberId, currYear);
            amountExpected = _financialReportService.GetTotalAnnualDuesExpectedById(_memberId, currYear);
            Balance = amountExpected - amountContributed;

            paidFinesAmount = _finedMemberService.GetTotalFinesPaidByMember(_memberId);
            expectedFineAmount = _finedMemberService.GetTotalFinesExpectedByMember(_memberId);
            fineBalance = expectedFineAmount - paidFinesAmount;

            labelTitle.Text = _memberFullDetailsDTO.LastName + " " + _memberFullDetailsDTO.FirstName + "'s present attendance record";
            string imagePath = Application.StartupPath + "\\images\\" + _memberFullDetailsDTO.ImagePath;
            picProfile.ImageLocation = imagePath;

            if (_isPresent)
            {
                loadMemberAttendanceAnnualPresent(currYear);
            }
            if (_isAbsent)
            {
                loadMemberAttendanceAnnualAbsent(currYear);
            }
            if (_isAmountContributed)
            {
                loadMemberContributionDetails();

                tableLayoutPanelChangingAmount.Visible = true;
                tableLayoutPanelTotal.Visible = true;
                labelTotalAmount.Text = "€" + _financialReportService.GetTotalAnnualDuesById(_memberId, currYear);
                labelTotalName.Text = "Total Amt. Contributed";
            }
            if (_isAmountExpected)
            {
                loadMemberAttendanceDetails();

                tableLayoutPanelTotal.Visible = true;
                labelTotalAmount.Text = "€ 120.00"; // To be made dynamic
                labelTotalName.Text = "Total Amt. Expected per Year";
            }
            if (_isPersonalBalance)
            {
                loadMemberAttendanceDetails();

                tableLayoutPanelChangingAmount.Visible = true;
                tableLayoutPanelTotal.Visible = true;
                labelTotalAmount.Text = "€" + (120 - _financialReportService.GetTotalAnnualDuesById(_memberId, currYear)); // 120 is to be made more dynamic
                labelTotalName.Text = "Remaining Amt.";
            }
            if (_isPersonalFines)
            {
                loadMemberFineDetails();

                tableLayoutPanelChangingAmount.Visible = true;
                tableLayoutPanelTotal.Visible = true;
                labelTotalAmount.Text = "€ " + _finedMemberService.GetTotalFinesPaidByMember(_memberId);
                labelTotalName.Text = "Total Fines";
            }
        }

        private void ClearFilters()
        {
            cmbYear.DataSource = _generalMeetingService.GetMeetingYears();
            cmbYear.SelectedIndex = -1;
            cmbMonth.DataSource = _monthService.GetAll();
            cmbMonth.SelectedIndex = -1;

            txtAmount.Clear();
            rbEqualAmount.Checked = false;
            rbLessAmount.Checked = false;
            rbMoreAmount.Checked = false;
            loadMemberAttendanceDetails();


            if (_isPresent)
            {
                loadMemberAttendanceAnnualPresent(currYear);
            }
            else if (_isAbsent)
            {
                loadMemberAttendanceAnnualAbsent(currYear);
            }
            else if (_isAmountContributed)
            {
                loadMemberContributionDetails();
            }
            else if (_isAmountExpected)
            {
                loadMemberContributionDetails();
            }
            else if (_isPersonalBalance)
            {
                loadMemberContributionDetails();
            }
            else if (_isPersonalFines)
            {
                loadMemberFineDetails();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void StatusSearch(string status, bool isAmount, bool isFines)
        {
            var filtered = _personalAttendanceDetailDTOs.AsQueryable();
            var filteredFinedMember = _finedMemberDTOs.AsQueryable();
            string month = cmbMonth.Text;
            int year = Convert.ToInt32(cmbYear.SelectedValue);

            if (cmbMonth.SelectedIndex == -1 && cmbYear.SelectedIndex == -1 && txtAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please select a month or year or amount");
                return;
            }

            if (cmbMonth.SelectedIndex != -1 && cmbYear.SelectedIndex == -1)
            {               
                filtered = filtered.Where(x => x.Month.ToString().IndexOf(month, StringComparison.OrdinalIgnoreCase) >= 0
                                        && x.AttendanceStatus.ToString().IndexOf(status, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else if (cmbMonth.SelectedIndex == -1 && cmbYear.SelectedIndex != -1)
            {
                filtered = filtered.Where(x => x.Year == year && x.AttendanceStatus.ToString().IndexOf(status, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            else if (isAmount)
            {
                decimal searchedAmount = Convert.ToDecimal(txtAmount.Text.Trim());

                if (txtAmount.Text.Trim() != "" && cmbYear.SelectedIndex == -1)
                {
                    if (rbEqualAmount.Checked)
                    {
                        if (!isFines)
                        {
                            filtered = filtered.Where(x => x.MonthlyDues == searchedAmount);
                        }
                        else if (isFines)
                        {
                            filteredFinedMember = filteredFinedMember.Where(x => x.AmountExpected == searchedAmount);
                        }
                    }
                    else if (rbLessAmount.Checked)
                    {
                        if (!isFines)
                        {
                            filtered = filtered.Where(x => x.MonthlyDues < searchedAmount);
                        }
                        else if (isFines)
                        {
                            filteredFinedMember = filteredFinedMember.Where(x => x.AmountExpected < searchedAmount);
                        }
                    }
                    else if (rbMoreAmount.Checked)
                    {
                        if (!isFines)
                        {
                            filtered = filtered.Where(x => x.MonthlyDues > searchedAmount);
                        }
                        else if (isFines)
                        {
                            filteredFinedMember = filteredFinedMember.Where(x => x.AmountExpected > searchedAmount);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please choose Less or Equal or more buttons");
                    }
                }
                if (txtAmount.Text.Trim() != "" && cmbYear.SelectedIndex != -1)
                {
                    if (rbEqualAmount.Checked)
                    {
                        filtered = filtered.Where(x => x.MonthlyDues == searchedAmount && x.Year == year);
                    }
                    else if (rbLessAmount.Checked)
                    {
                        filtered = filtered.Where(x => x.MonthlyDues < searchedAmount && x.Year == year);
                    }
                    else if (rbMoreAmount.Checked)
                    {
                        filtered = filtered.Where(x => x.MonthlyDues > searchedAmount && x.Year == year);
                    }
                    else
                    {
                        MessageBox.Show("Please choose Less or Equal or more buttons");
                    }
                }
            }
            else
            {
                filtered = filtered.Where(x => x.Year == year 
                && x.Month.ToString().IndexOf(month, StringComparison.OrdinalIgnoreCase) >= 0 
                && x.AttendanceStatus.ToString().IndexOf(status, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (_isPersonalFines)
            {
                dataGridView1.DataSource = filteredFinedMember.ToList();
            }
            else
            {
                dataGridView1.DataSource = filtered.ToList();
            }        
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (_isPresent)
            {
                string status = "Present";
                StatusSearch(status, false, false);
            }

            if (_isAbsent)
            {
                string status = "Absent";
                StatusSearch(status, false, false);
            }

            if (_isAmountContributed)
            {
                string status = "";
                StatusSearch(status, true, false);
            }

            if (_isPersonalBalance)
            {
                string status = "";
                StatusSearch(status, true, false);
            }

            if (_isPersonalFines)
            {
                string status = "";
                StatusSearch(status, true, true);
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
