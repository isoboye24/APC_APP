using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC
{
    public partial class FormFinancialReport : Form
    {
        private readonly IFinancialReportService _financialReportService;

        private Applications.DTO.FinancialReportDTO _financialReportDTO;
        private bool _isUpdate = false;

        public FormFinancialReport(IFinancialReportService financialReportService)
        {
            InitializeComponent();
            _financialReportService = financialReportService;
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
        
        public void loadForEdit(Applications.DTO.FinancialReportDTO financialReportDTO, bool isUpdate)
        {
            _financialReportDTO = financialReportDTO;
            _isUpdate = isUpdate;
        }

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label2, btnClose, btnSave);

            GeneralHelper.ApplyRegularFont(16, txtSummary, txtYear);
        }

        private void FormFinancialReport_Load(object sender, EventArgs e)
        {
            controlsFont();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit Financial Report";
                txtYear.Text = _financialReportDTO.Year.ToString();
                txtSummary.Text = _financialReportDTO.Summary;
            }
            else
            {
                labelTitle.Text = "Add Financial Report";
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string summary = txtSummary.Text.Trim();
                int year = Convert.ToInt32(txtYear.Text.Trim());
                decimal totalAmountRaised = 0;
                decimal totalAmountSpent = 0;

                if (!_isUpdate)
                {
                    var financialReport = new FinancialReport(totalAmountRaised, totalAmountSpent, year, summary);
                    _financialReportService.Create(financialReport);
                    MessageBox.Show("Financial Report created successfully!");
                }
                else
                {
                    var financialReport = FinancialReport.Rehydrate(_financialReportDTO.FinancialReportId, totalAmountRaised, totalAmountSpent, year, summary);

                    _financialReportService.Update(financialReport);
                    MessageBox.Show("Financial Report updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
    }
}
