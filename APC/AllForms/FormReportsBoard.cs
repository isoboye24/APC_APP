using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using APC.HelperServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormReportsBoard : Form
    {
        public FormReportsBoard()
        {
            InitializeComponent();
        }
        FinancialReportBLL finReportBLL = new FinancialReportBLL();
        FinancialReportDTO finReportDTO = new FinancialReportDTO();
        FinancialReportDetailDTO finReportDetail = new FinancialReportDetailDTO();
        private void FormReportsBoard_Load(object sender, EventArgs e)
        {
            #region
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelTotalFinReport.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelTotalAmountRaised.Font = new Font("Segoe UI", 27, FontStyle.Bold);
            labelTotalAmountSpent.Font = new Font("Segoe UI", 27, FontStyle.Bold);
            labelTotalBalance.Font = new Font("Segoe UI", 27, FontStyle.Bold);
            txtYearFinReport.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnAddFinReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateFinReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewFinReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteFinReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalRowsExpReport.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            cmbMonthExpReport.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbYearExpenditure.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnAddExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearchExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            labelTotalExpReportYearly.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            labelTotalExpReport.Font = new Font("Segoe UI", 12, FontStyle.Regular);

            #endregion

            #region
            finReportDTO = finReportBLL.Select();
            LoadDataGridView.loadFinancialReport(dataGridViewFinReport, finReportDTO);

            expReportDTO = expReportBLL.Select(DateTime.Now.Year);
            LoadDataGridView.loadExpenditure(dataGridViewExpReport, expReportDTO);

            cmbMonthExpReport.DataSource = expReportDTO.Months;
            GeneralHelper.ComboBoxProps(cmbMonthExpReport, "MonthName", "MonthID");
            cmbYearExpenditure.DataSource = expReportDTO.Years;
            cmbYearExpenditure.SelectedIndex = -1;

            #endregion

            if (LoginInfo.AccessLevel != 4)
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
            finReportBLL = new FinancialReportBLL();
            finReportDTO = finReportBLL.Select();
            dataGridViewFinReport.DataSource = finReportDTO.FinancialReports;

            txtSummaryExpReport.Clear();
            cmbMonthExpReport.SelectedIndex = -1;
            cmbYearExpenditure.SelectedIndex = -1;
            expReportBLL = new ExpenditureBLL();
            expReportDTO = expReportBLL.Select(DateTime.Now.Year);
            dataGridViewExpReport.DataSource = expReportDTO.Expenditures;

            Counts();
            RowsCount();
        }

        int currentYear = DateTime.Now.Year;
        private void Counts()
        {            
            labelTotalFinReport.Text = "Total: " + dataGridViewFinReport.RowCount.ToString();
            labelTotalAmountRaised.Text = finReportBLL.SelectTotalRaisedAmount().ToString();
            labelTotalAmountSpent.Text = finReportBLL.SelectTotalSpentAmount().ToString();
            labelTotalBalance.Text = (finReportBLL.SelectTotalRaisedAmount() - finReportBLL.SelectTotalSpentAmount()).ToString();

            labelTotalExpReport.Text = "Overall Total: " + finReportBLL.SelectTotalExpenditure().ToString() + " €";
            labelTotalExpReportYearly.Text = "Total in " + currentYear.ToString() + ": " + finReportBLL.SelectTotalExpenditureYearly(currentYear).ToString() + " €";
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

        private void dataGridViewFinReport_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            finReportDetail = GeneralHelper.MapFromGrid<FinancialReportDetailDTO>(dataGridViewFinReport, e.RowIndex);
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
