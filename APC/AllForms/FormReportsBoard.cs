using APC.Applications.Interfaces;
using APC.BLL;
using APC.DAL.DTO;
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
            FormFinancialReport open = new FormFinancialReport();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private Applications.DTO.FinancialReportDTO GetSelectedFinancialReport()
        {
            if (dataGridViewFinReport.CurrentRow == null)
                return null;

            return dataGridViewFinReport.CurrentRow.DataBoundItem as Applications.DTO.FinancialReportDTO;
        }


        private void dataGridViewFinReport_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;
            //finReportDetail = GeneralHelper.MapFromGrid<FinancialReportDetailDTO>(dataGridViewFinReport, e.RowIndex);
        }

        private void btnUpdateFinReport_Click(object sender, EventArgs e)
        {
            if (finReportDetail.FinancialReportID == 0)
            {
                MessageBox.Show("Please choose a Report from the table");
            }
            else
            {
                FormFinancialReport open = new FormFinancialReport();
                open.detail = finReportDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnViewFinReport_Click(object sender, EventArgs e)
        {
            if (finReportDetail.FinancialReportID == 0)
            {
                MessageBox.Show("Please choose a Report from the table");
            }
            else
            {
                FormViewFinancialReport open = new FormViewFinancialReport();
                open.detail = finReportDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtYearFinReport_TextChanged(object sender, EventArgs e)
        {
            List<FinancialReportDetailDTO> list = finReportDTO.FinancialReports;
            list = list.Where(x => x.Year.Contains(txtYearFinReport.Text.Trim())).ToList();
            dataGridViewFinReport.DataSource = list;
        }

        private void btnDeleteFinReport_Click(object sender, EventArgs e)
        {
            if (finReportDetail.FinancialReportID == 0)
            {
                MessageBox.Show("Please choose a Report from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (finReportBLL.Delete(finReportDetail))
                    {
                        MessageBox.Show("Financial report was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        ExpenditureBLL expReportBLL = new ExpenditureBLL();
        ExpenditureDTO expReportDTO = new ExpenditureDTO();
        ExpenditureDetailDTO expReportDetail = new ExpenditureDetailDTO();

        private void btnAddExpReport_Click(object sender, EventArgs e)
        {
            FormExpenditure open = new FormExpenditure();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateExpReport_Click(object sender, EventArgs e)
        {
            if (expReportDetail.ExpenditureID == 0)
            {
                MessageBox.Show("Please choose an expenditure from the table");
            }
            else
            {
                FormExpenditure open = new FormExpenditure();
                open.detail = expReportDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnViewExpReport_Click(object sender, EventArgs e)
        {
            if (expReportDetail.ExpenditureID == 0)
            {
                MessageBox.Show("Please choose an expenditure from the table");
            }
            else
            {
                FormViewExpenditure open = new FormViewExpenditure();
                open.detail = expReportDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnClearExpReport_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnSearchExpReport_Click(object sender, EventArgs e)
        {            
            if (cmbYearExpenditure.SelectedIndex == -1)            
            {
                MessageBox.Show("Select year");                
            }            
            else
            {
                expReportBLL = new ExpenditureBLL();
                expReportDTO = expReportBLL.Select(Convert.ToInt32(cmbYearExpenditure.SelectedValue));
                List<ExpenditureDetailDTO> list = expReportDTO.Expenditures;

                if (cmbMonthExpReport.SelectedIndex != -1 && cmbYearExpenditure.SelectedIndex != -1)
                {
                    list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonthExpReport.SelectedValue) && Convert.ToInt32(x.Year) == Convert.ToInt32(cmbYearExpenditure.SelectedValue)).ToList();
                }
                
                dataGridViewExpReport.DataSource = list;
                RowsCount();
                labelTotalExpReportYearly.Text = "Total in " + cmbYearExpenditure.SelectedValue + ": " + finReportBLL.SelectTotalExpenditureYearly(Convert.ToInt32(cmbYearExpenditure.SelectedValue)).ToString() + " €";
            }
           
        }

        private void dataGridViewExpReport_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            expReportDetail = GeneralHelper.MapFromGrid<ExpenditureDetailDTO>(dataGridViewExpReport, e.RowIndex);
        }

        private void btnDeleteExpReport_Click(object sender, EventArgs e)
        {
            if (expReportDetail.ExpenditureID == 0)
            {
                MessageBox.Show("Please select an expenditure from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (expReportBLL.Delete(expReportDetail))
                    {
                        MessageBox.Show("Expenditure was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        private void txtSummaryExpReport_TextChanged(object sender, EventArgs e)
        {
            List<ExpenditureDetailDTO> list = expReportDTO.Expenditures;
            list = list.Where(x => x.Summary.Contains(txtSummaryExpReport.Text)).ToList();
            dataGridViewExpReport.DataSource = list;

            Counts();
        }
    }
}
