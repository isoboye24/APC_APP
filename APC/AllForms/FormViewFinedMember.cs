using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.DAL.DTO;
using APC.Helper;
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
        private readonly IConstitutionService _constitutionService;
        private readonly Applications.DTO.FinedMemberDTO _finedMemberDTO;
        private readonly Applications.DTO.ConstitutionDTO _constitutionDTO;
        public FormViewFinedMember(Applications.DTO.FinedMemberDTO finedMemberDTO)
        {
            InitializeComponent();
            _finedMemberDTO = finedMemberDTO;
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

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label2, label3, label4, label5, labelSectionBtn, label8, label9, label10, label11,
                labelTitle, btnClose);
            GeneralHelper.ApplyRegularFont(14, labelName, labelSurname, labelPosition, labelBalance, labelPaidAmount, labelExpectedAmount);
        }

        private void FormViewFinedMember_Load(object sender, EventArgs e)
        {
            resizeControls();

            labelName.Text = _finedMemberDTO.FirstName;
            labelSurname.Text = _finedMemberDTO.LastName;
            labelPosition.Text = _finedMemberDTO.PositionName;
            labelExpectedAmount.Text = _finedMemberDTO.AmountExpected;
            labelPaidAmount.Text = _finedMemberDTO.FormattedAmountPaid;
            labelBalance.Text = _finedMemberDTO.Balance;            
            labelPaymentStatus.Text = "Payment " + _finedMemberDTO.Status;
            labelSectionBtn.Text = _finedMemberDTO.Section;
            txtSurmary.Text = _finedMemberDTO.Summary;
            labelTitle.Text = _finedMemberDTO.FirstName + " " + _finedMemberDTO.LastName + "'s fine on " + _finedMemberDTO.FormattedFineDate;
            string imagePath = Application.StartupPath + "\\images\\" + _finedMemberDTO.ImagePath;
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
            string section = _finedMemberDTO.Section;
            
            var form = new FormViewConstitution(_constitutionDTO, _constitutionService);
            form.loadFromFinedMember(section, true);
            form.ShowDialog();
        }
    }
}
