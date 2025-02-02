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
using System.Xml.Linq;

namespace APC.AllForms
{
    public partial class FormViewFinedMember : Form
    {
        public FormViewFinedMember()
        {
            InitializeComponent();
        }
        // Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        public FinedMemberDetailDTO detail = new FinedMemberDetailDTO();
        public int constID;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FormViewFinedMember_Load(object sender, EventArgs e)
        {
            #region
            labelTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label11.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelSectionBtn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelPaidAmount.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelBalance.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelExpectedAmount.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelName.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelPosition.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            labelSurname.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnClose.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            #endregion

            labelName.Text = detail.Name;
            labelSurname.Text = detail.Surname;
            labelPosition.Text = detail.Position;
            labelExpectedAmount.Text = detail.ExpectedAmountWithCurrency;
            labelPaidAmount.Text = detail.AmountPaidWithCurrency;
            labelBalance.Text = detail.BalanceWithCurrency;            
            labelPaymentStatus.Text = "Payment " + detail.FineStatus;
            labelSectionBtn.Text = detail.ConstitutionSection;
            txtSurmary.Text = detail.Summary;
            labelTitle.Text = "Fined on " + detail.Day + "." + detail.MonthID + "." + detail.Year;
            string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
            picProfilePic.ImageLocation = imagePath;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picMinimize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void labelSectionBtn_Click(object sender, EventArgs e)
        {
            FormViewConstitution open = new FormViewConstitution();
            open.isFinedMemberView = true;
            open.ID = constID;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
        }
    }
}
