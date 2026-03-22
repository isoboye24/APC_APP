using APC.Applications.DTO;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using APC.Applications.Interfaces;

namespace APC.AllForms
{
    public partial class FormViewConstitution : Form
    {
        private readonly IConstitutionService _constitutionService;
        private ConstitutionDTO _constitutionDTO;
        public FormViewConstitution(ConstitutionDTO constitutionDTO, IConstitutionService constitutionService)
        {
            InitializeComponent();
            _constitutionDTO = constitutionDTO;
            _constitutionService = constitutionService;
        }
        // Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);


        public bool isFinedMemberView = false;
        public int ID;

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, labelTitle, label1, labelFine, labelTitle, btnClose, labelSection);
        }

        private void FormViewConstitution_Load(object sender, EventArgs e)
        {
            resizeControls();

            txtConstitution.Text = _constitutionDTO.ConstitutionText;
            labelFine.Text = "€ " + _constitutionDTO.FineWithCurrency;
            labelSection.Text = _constitutionDTO.Section;

            if (isFinedMemberView)
            {

                var constitution = _constitutionService.GetById(ID);
                if (constitution.ConstitutionId == 0)
                {
                    MessageBox.Show("This constitution does not exist.");
                }
                else if (constitution.ConstitutionId != 0)
                {
                    txtConstitution.Text = constitution.ConstitutionText;
                    labelFine.Text = constitution.FineWithCurrency;
                    labelSection.Text = constitution.Section;
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
