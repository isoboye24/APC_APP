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

namespace APC.AllForms
{
    public partial class FormViewExpenditure : Form
    {
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
        public ExpenditureDetailDTO detail = new ExpenditureDetailDTO();
        private void FormViewExpenditure_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            txtAmountSpent.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtDate.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            txtSummary.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);            
            #endregion

            txtAmountSpent.Text = detail.AmountSpent.ToString();
            txtSummary.Text = detail.Summary;
            txtDate.Text = detail.Day + "/" + detail.MonthID +"/"+ detail.Year;
            labelTitle.Text = $"Expenditure on {txtDate.Text}";
        }        
    }
}
