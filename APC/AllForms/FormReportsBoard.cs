using APC.Applications.Interfaces;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormReportsBoard : Form
    {
        private readonly IFinancialReportService _financialReportService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IExpenditureService _expenditureService;
        private readonly IMonthService _monthService;

        private List<Applications.DTO.FinancialReportDTO> _financialReportDTOs;
        private List<Applications.DTO.ExpenditureDTO> _expenditureDTOs;

        private int currentYear = DateTime.Today.Year;

        public FormReportsBoard(IFinancialReportService financialReportService, ICurrentUserService currentUserService, 
            IExpenditureService expenditureService, IMonthService monthService)
        {
            InitializeComponent();
            _financialReportService = financialReportService;
            _currentUserService = currentUserService;
            _expenditureService = expenditureService;
            _monthService = monthService;
        }

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, label5, btnAddFinReport, btnUpdateFinReport, btnViewFinReport, btnDeleteFinReport, label1,
                label4, btnAddExpReport, btnUpdateExpReport, btnViewExpReport, btnDeleteExpReport, btnSearchExpReport, btnClearExpReport
                );

            GeneralHelper.ApplyBoldFont(16, label5, label2, label3);

            GeneralHelper.ApplyBoldFont(27, labelTotalAmountRaised, labelTotalAmountSpent, labelTotalBalance);

            GeneralHelper.ApplyRegularFont(11, labelTotalFinReport, labelTotalRowsExpReport, labelTotalExpReportYearly, labelTotalExpReport);
            GeneralHelper.ApplyRegularFont(16, txtYearFinReport, cmbMonthExpReport, cmbYearExpenditure);
        }

        private void loadFinancialReports()
        {
            dataGridViewExpReport.DataSource = _financialReportService.GetAll();
            _financialReportDTOs = _financialReportService.GetAll();
            FinancialReportHelper.ConfigureFinancialReportGrid(dataGridViewExpReport, FinancialReportHelper.FinancialReportGridType.Basic);
        }
        
        private void loadExpenditures(int year)
        {
            dataGridViewExpReport.DataSource = _expenditureService.GetAnnualExpenditures(year);
            _expenditureDTOs = _expenditureService.GetAll();
            ExpenditureHelper.ConfigureExpenditureGrid(dataGridViewExpReport, ExpenditureHelper.ExpenditureGridType.Basic);
        }

        private void FormReportsBoard_Load(object sender, EventArgs e)
        {
            controlsFont();

            loadFinancialReports();
            loadExpenditures(currentYear);

            cmbMonthExpReport.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthExpReport, "MonthName", "MonthID");
            cmbYearExpenditure.DataSource = _expenditureService.GetExpenditureYearsOnly();
            cmbYearExpenditure.SelectedIndex = -1;

            if (_currentUserService.AccessLevel != 4)
            {
                btnDeleteFinReport.Hide();
                btnDeleteExpReport.Hide();
            }

            Counts();
            RowsCount();
            ResizeableControls();
        }

        private void ResizeableControls()
        {
            labelTotalExpReportYearly.Tag = "Sizeable";
            labelTotalExpReport.Tag = "Sizeable";
        }

        private void ClearFilters()
        {
            txtYearFinReport.Clear();            
            dataGridViewFinReport.DataSource = _financialReportService.GetAll();

            txtSummaryExpReport.Clear();
            cmbMonthExpReport.SelectedIndex = -1;
            cmbYearExpenditure.SelectedIndex = -1;

            dataGridViewExpReport.DataSource = _expenditureService.GetAnnualExpenditures(currentYear);

            Counts();
            RowsCount();
        }

        private void Counts()
        {
            decimal overallRaisedAmount = _financialReportService.GetOverallTotalDues();
            decimal overallSpentAmount = _financialReportService.GetOverallExpenditures();

            labelTotalFinReport.Text = "Total: " + dataGridViewFinReport.RowCount.ToString();
            labelTotalAmountRaised.Text = overallRaisedAmount.ToString();
            labelTotalAmountSpent.Text = overallSpentAmount.ToString();
            labelTotalBalance.Text = (overallRaisedAmount - overallSpentAmount).ToString();

            labelTotalExpReport.Text = "Overall Total: " + overallSpentAmount.ToString() + " €";
            labelTotalExpReportYearly.Text = "Total in " + currentYear.ToString() + ": " + _expenditureService.GetAnnualExpenditures(currentYear).ToString() + " €";
        }

        private void RowsCount()
        {
            labelTotalRowsExpReport.Text = "Row: " + dataGridViewExpReport.RowCount.ToString();
        }

        private void btnAddFinReport_Click(object sender, EventArgs e)
        {
            var form = new FormFinancialReport(_financialReportService);
            form.ShowDialog();

            ClearFilters();
        }

        private Applications.DTO.FinancialReportDTO GetSelectedFinancialReport()
        {
            if (dataGridViewFinReport.CurrentRow == null)
                return null;

            return dataGridViewFinReport.CurrentRow.DataBoundItem as Applications.DTO.FinancialReportDTO;
        }

        private void btnUpdateFinReport_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFinancialReport();
            if (selected == null)
            {
                MessageBox.Show("Please select a financial report from the table");
                return;
            }

            var form = new FormFinancialReport(_financialReportService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnViewFinReport_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFinancialReport();
            if (selected == null)
            {
                MessageBox.Show("Please select a financial report from the table");
                return;
            }

            var form = new FormViewFinancialReport();
            form.loadForView(selected);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtYearFinReport_TextChanged(object sender, EventArgs e)
        {
            string search = txtYearFinReport.Text.Trim().ToLower();
            var filtered = _financialReportDTOs.Where(x => x.Year.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewFinReport.DataSource = filtered;            
        }

        private void btnDeleteFinReport_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFinancialReport();
            if (selected == null)
            {
                MessageBox.Show("Please select a financial report.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _financialReportService.Delete(selected.FinancialReportId);
                ClearFilters();
            }
        }

        // Expenditure Report //////

        private void btnAddExpReport_Click(object sender, EventArgs e)
        {
            var form = new FormExpenditure(_expenditureService);
            form.ShowDialog();

            ClearFilters();
        }

        private Applications.DTO.ExpenditureDTO GetSelectedExpenditure()
        {
            if (dataGridViewExpReport.CurrentRow == null)
                return null;

            return dataGridViewExpReport.CurrentRow.DataBoundItem as Applications.DTO.ExpenditureDTO;
        }


        private void btnUpdateExpReport_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedExpenditure();
            if (selected == null)
            {
                MessageBox.Show("Please select an expenditure from the table");
                return;
            }

            var form = new FormExpenditure(_expenditureService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnViewExpReport_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedExpenditure();
            if (selected == null)
            {
                MessageBox.Show("Please select an expenditure from the table");
                return;
            }

            var form = new FormViewExpenditure();
            form.loadForView(selected);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnClearExpReport_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnSearchExpReport_Click(object sender, EventArgs e)
        {
            var filtered = _expenditureDTOs.AsQueryable();

            if (cmbYearExpenditure.SelectedIndex == -1 && cmbMonthExpReport.SelectedIndex == -1)
            {
                MessageBox.Show("Please select either a month or year");
                return;
            }

            if (cmbYearExpenditure.SelectedIndex != -1)
            {
                int searchedYear = Convert.ToInt32(cmbYearExpenditure.SelectedValue);
                filtered = filtered.Where(x => x.ExpenditureDate.Year == searchedYear);
            }

            if (cmbMonthExpReport.SelectedIndex != -1)
            {
                int searchedMonth = Convert.ToInt32(cmbMonthExpReport.SelectedValue);
                filtered = filtered.Where(x => x.ExpenditureDate.Month == searchedMonth);
            }

            dataGridViewExpReport.DataSource = filtered.ToList();
        }

        private void btnDeleteExpReport_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedExpenditure();
            if (selected == null)
            {
                MessageBox.Show("Please select an expenditure from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _expenditureService.Delete(selected.ExpenditureId);
                ClearFilters();
            }
        }

        private void txtSummaryExpReport_TextChanged(object sender, EventArgs e)
        {
            string search = txtSummaryExpReport.Text.Trim().ToLower();
            var filtered = _expenditureDTOs.Where(x => x.Summary.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewExpReport.DataSource = filtered;

            Counts();
        }
    }
}
