using APC.BLL;
using APC.DAL;
using APC.DAL.DTO;
using APC.Helper;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormViewConstitution : Form
    {
        private Applications.DTO.ConstitutionDTO _constitutionDTO;
        public FormViewConstitution(Applications.DTO.ConstitutionDTO constitutionDTO)
        {
            InitializeComponent();
            _constitutionDTO = constitutionDTO;
        }
        // Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        public ConstitutionDetailDTO detail = new ConstitutionDetailDTO();
        ConstitutionBLL bll = new ConstitutionBLL();
        public bool isFinedMemberView = false;
        public int ID;

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, labelFine, labelTitle, btnClose, labelSection);
        }

        private void FormViewConstitution_Load(object sender, EventArgs e)
        {
            resizeControls();

            txtConstitution.Text = detail.ConstitutionText;
            labelFine.Text = "€ " + detail.Fine;
            labelSection.Text = detail.Section;
            if (isFinedMemberView)
            {

                List<CONSTITUTION> singleConstitution = bll.GetSingleConstitution(ID);
                if (singleConstitution.Count == 0)
                {
                    MessageBox.Show("This constitution does not exist.");
                }
                else if (singleConstitution.Count > 0)
                {
                    CONSTITUTION constit = new CONSTITUTION();
                    constit = singleConstitution.First();
                    txtConstitution.Text = constit.constitution1;
                    labelFine.Text = "€ " + constit.fine;
                    labelSection.Text = constit.section;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
