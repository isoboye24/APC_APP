using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewExpenditure : Form
    {
        private Applications.DTO.ExpenditureDTO _expenditureDTO;
        public FormViewExpenditure()
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

        public void loadForView(Applications.DTO.ExpenditureDTO expenditureDTO)
        {
            _expenditureDTO = expenditureDTO;
        }

        private void controlsFont()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, label3, btnClose);

            GeneralHelper.ApplyRegularFont(14, txtSummary, txtAmountSpent, txtDate);
        }

        private void FormViewExpenditure_Load(object sender, EventArgs e)
        {
            controlsFont();

            txtAmountSpent.Text = _expenditureDTO.AmountSpent.ToString();
            txtSummary.Text = _expenditureDTO.Summary;
            txtDate.Text = _expenditureDTO.ExpenditureDate.Day + "/" + _expenditureDTO.ExpenditureDate.Month +"/"+ _expenditureDTO.ExpenditureDate.Year;
            labelTitle.Text = $"Expenditure on {txtDate.Text}";
        }        
    }
}
