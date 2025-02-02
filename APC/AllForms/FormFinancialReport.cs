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

namespace APC
{
    public partial class FormFinancialReport : Form
    {
        public FormFinancialReport()
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
        private void FormFinancialReport_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public FinancialReportDetailDTO detail = new FinancialReportDetailDTO();
        public bool isUpdate = false;
        private void FormFinancialReport_Load(object sender, EventArgs e)
        {
            labelTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            txtSummary.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtYear.Font = new Font("Segoe UI", 16, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            btnSave.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            if (isUpdate)
            {
                labelTitle.Text = "Edit Financial Report";
                txtYear.Text = detail.Year;
                txtSummary.Text = detail.Summary;
            }
            else
            {
                labelTitle.Text = "Add Financial Report";
            }
        }
        FinancialReportBLL bll = new FinancialReportBLL();
        private void btnSave_Click(object sender, EventArgs e)
        {
            int conpareYear = Convert.ToInt32(txtYear.Text);
            bool newFinReport = bll.CheckTotalRaisedAmountAndTotalSpentAmount(conpareYear);
            if (txtYear.Text.Trim() == "")
            {
                MessageBox.Show("Please enter year");
            }
            else if (!newFinReport)
            {
                MessageBox.Show("There is neither total Raised amount nor total expenditure for " + txtYear.Text);
            }
            else
            {
                if (!isUpdate)
                {                    
                    
                    FinancialReportDetailDTO financialReport = new FinancialReportDetailDTO();
                    financialReport.Year = txtYear.Text;
                    financialReport.Summary = txtSummary.Text;
                    financialReport.TotalAmountRaised = 0;
                    financialReport.TotalAmountSpent = 0;
                    if (bll.Insert(financialReport))
                    {
                        MessageBox.Show("Financial Report is created");
                        txtSummary.Clear();
                        txtYear.Clear();
                    }                                       
                }
                else if (isUpdate)
                {
                    if (detail.Summary == txtSummary.Text.Trim() && detail.Year == txtYear.Text.Trim())
                    {
                        MessageBox.Show("There is no change");
                    }
                    else
                    {
                        detail.Year = txtYear.Text;
                        detail.Summary = txtSummary.Text;
                        detail.TotalAmountRaised = detail.TotalAmountRaised;
                        detail.TotalAmountSpent = detail.TotalAmountSpent;
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("Financial report was updated");
                            this.Close();
                        }
                    }
                }
            }
        }        
    }
}
