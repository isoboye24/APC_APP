using APC.Helper;
using APC.Utility;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewFinancialReport : BaseFormDashboard
    {
        private Applications.DTO.FinancialReportDTO _financialReportDTO;
        public FormViewFinancialReport()
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

        public void loadForView(Applications.DTO.FinancialReportDTO financialReportDTO)
        {
            _financialReportDTO = financialReportDTO;
        }

        private void FormViewFinancialReport_Load(object sender, EventArgs e)
        {
            labelTitle.Text = "Financial Report in " + _financialReportDTO.Year;
            txtSummary.Text = _financialReportDTO.Summary;
            dvLabelTotalAmountRaised.Text = AmountHelper.FormatAmount(_financialReportDTO.TotalAmountRaised);
            dvLabelTotalAmountSpent.Text =AmountHelper.FormatAmount(_financialReportDTO.TotalAmountSpent);
            dvLabelTotalBalance.Text = AmountHelper.FormatAmount(_financialReportDTO.TotalAmountRaised - _financialReportDTO.TotalAmountSpent);
        }        
    }
}
