using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewFinancialReport : Form
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

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, btnClose);

            GeneralHelper.ApplyRegularFont(18, txtSummary);

            GeneralHelper.ApplyBoldFont(20,  label3, label4, label6);
            GeneralHelper.ApplyBoldFont(24, labelTotalAmountRaised, labelTotalAmountSpent, labelTotalBalance);
        }

        private void FormViewFinancialReport_Load(object sender, EventArgs e)
        {
            controlsFont();

            labelTitle.Text = "Financial Report in " + _financialReportDTO.Year;
            txtSummary.Text = _financialReportDTO.Summary;
            labelTotalAmountRaised.Text = _financialReportDTO.TotalAmountRaised.ToString();
            labelTotalAmountSpent.Text = _financialReportDTO.TotalAmountSpent.ToString();
            labelTotalBalance.Text = (_financialReportDTO.TotalAmountRaised - _financialReportDTO.TotalAmountSpent).ToString();
        }        
    }
}
