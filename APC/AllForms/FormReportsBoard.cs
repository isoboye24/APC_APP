using APC.BLL;
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
            txtYearExpReport.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnAddExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearchExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearExpReport.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            #region
            finReportDTO = finReportBLL.Select();
            dataGridViewFinReport.DataSource = finReportDTO.FinancialReports;
            dataGridViewFinReport.Columns[0].Visible = false;
            dataGridViewFinReport.Columns[1].HeaderText = "Year";
            dataGridViewFinReport.Columns[2].HeaderText = "Amount Raised";
            dataGridViewFinReport.Columns[3].HeaderText = "Amount Spent";
            dataGridViewFinReport.Columns[4].HeaderText = "Balance";
            dataGridViewFinReport.Columns[5].Visible = false;
            foreach (DataGridViewColumn column in dataGridViewFinReport.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            expReportDTO = expReportBLL.Select();
            cmbMonthExpReport.DataSource = expReportDTO.Months;
            General.ComboBoxProps(cmbMonthExpReport, "MonthName", "MonthID");

            dataGridViewExpReport.DataSource = expReportDTO.Expenditures;
            dataGridViewExpReport.Columns[0].Visible = false;
            dataGridViewExpReport.Columns[1].HeaderText = "Summary";
            dataGridViewExpReport.Columns[2].HeaderText = "Amount Spent (€)";
            dataGridViewExpReport.Columns[3].HeaderText = "Day";
            dataGridViewExpReport.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewExpReport.Columns[4].Visible = false;
            dataGridViewExpReport.Columns[5].HeaderText = "Month";
            dataGridViewExpReport.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewExpReport.Columns[6].HeaderText = "Year";
            dataGridViewExpReport.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewExpReport.Columns[7].Visible = false;
            foreach (DataGridViewColumn column in dataGridViewExpReport.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }
            #endregion

            if (LoginInfo.AccessLevel != 4)
            {
                btnDeleteFinReport.Hide();
                btnDeleteExpReport.Hide();
            }

            Counts();
        }

        private void ClearFilters()
        {
            txtYearFinReport.Clear();
            finReportBLL = new FinancialReportBLL();
            finReportDTO = finReportBLL.Select();
            dataGridViewFinReport.DataSource = finReportDTO.FinancialReports;

            txtYearExpReport.Clear();
            txtSummaryExpReport.Clear();
            cmbMonthExpReport.SelectedIndex = -1;
            expReportBLL = new ExpenditureBLL();
            expReportDTO = expReportBLL.Select();
            dataGridViewExpReport.DataSource = expReportDTO.Expenditures;

            Counts();
        }

        private void Counts()
        {
            labelTotalFinReport.Text = "Total: " + dataGridViewFinReport.RowCount.ToString();
            labelTotalRowsExpReport.Text = "Row: " + dataGridViewExpReport.RowCount.ToString();

            labelTotalAmountRaised.Text = finReportBLL.SelectTotalRaisedAmount().ToString();

            labelTotalAmountSpent.Text = finReportBLL.SelectTotalSpentAmount().ToString();
            labelTotalExpReport.Text = "Total: " + finReportBLL.SelectTotalExpenditure().ToString();

            labelTotalBalance.Text = (finReportBLL.SelectTotalRaisedAmount() - finReportBLL.SelectTotalSpentAmount()).ToString();
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
            finReportDetail = new FinancialReportDetailDTO();
            finReportDetail.FinancialReportID = Convert.ToInt32(dataGridViewFinReport.Rows[e.RowIndex].Cells[0].Value);
            finReportDetail.Year = dataGridViewFinReport.Rows[e.RowIndex].Cells[1].Value.ToString();
            finReportDetail.TotalAmountRaised = Convert.ToDecimal(dataGridViewFinReport.Rows[e.RowIndex].Cells[2].Value);
            finReportDetail.TotalAmountSpent = Convert.ToDecimal(dataGridViewFinReport.Rows[e.RowIndex].Cells[3].Value);
            finReportDetail.Balance = Convert.ToDecimal(dataGridViewFinReport.Rows[e.RowIndex].Cells[4].Value);
            finReportDetail.Summary = dataGridViewFinReport.Rows[e.RowIndex].Cells[5].Value.ToString();
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

        private void txtYearExpReport_TextChanged(object sender, EventArgs e)
        {
            List<ExpenditureDetailDTO> list = expReportDTO.Expenditures;
            list = list.Where(x => x.Year.Contains(txtYearExpReport.Text)).ToList();
            dataGridViewExpReport.DataSource = list;
        }

        private void btnClearExpReport_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnSearchExpReport_Click(object sender, EventArgs e)
        {
            List<ExpenditureDetailDTO> list = expReportDTO.Expenditures;
            if (txtYearExpReport.Text.Trim() == "")
            {
                MessageBox.Show("Year is empty.");
            }
            else if (cmbMonthExpReport.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonthExpReport.SelectedValue)).ToList();
            }
            else if (cmbMonthExpReport.SelectedIndex != -1 && txtYearExpReport.Text.Trim() != "")
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonthExpReport.SelectedValue) && x.Year.ToString().Contains(txtYearExpReport.Text.Trim())).ToList();
            }
            else
            {
                MessageBox.Show("Unknown search!");
            }
            dataGridViewExpReport.DataSource = list;
        }

        private void dataGridViewExpReport_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            expReportDetail = new ExpenditureDetailDTO();
            expReportDetail.ExpenditureID = Convert.ToInt32(dataGridViewExpReport.Rows[e.RowIndex].Cells[0].Value);
            expReportDetail.Summary = dataGridViewExpReport.Rows[e.RowIndex].Cells[1].Value.ToString();
            expReportDetail.AmountSpent = Convert.ToDecimal(dataGridViewExpReport.Rows[e.RowIndex].Cells[2].Value);
            expReportDetail.Day = Convert.ToInt32(dataGridViewExpReport.Rows[e.RowIndex].Cells[3].Value);
            expReportDetail.MonthID = Convert.ToInt32(dataGridViewExpReport.Rows[e.RowIndex].Cells[4].Value);
            expReportDetail.Month = dataGridViewExpReport.Rows[e.RowIndex].Cells[5].Value.ToString();
            expReportDetail.Year = dataGridViewExpReport.Rows[e.RowIndex].Cells[6].Value.ToString();
            expReportDetail.ExpenditureDate = Convert.ToDateTime(dataGridViewExpReport.Rows[e.RowIndex].Cells[7].Value);
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
