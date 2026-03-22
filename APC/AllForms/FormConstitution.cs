using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormConstitution : Form
    {
        private readonly IConstitutionService _constitutionService;
        private Applications.DTO.ConstitutionDTO _constitutionDTO;
        private bool _isUpdate = false;

        public FormConstitution(IConstitutionService constitutionService)
        {
            InitializeComponent();
            _constitutionService = constitutionService;
        }

        // Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void loadForEdit(Applications.DTO.ConstitutionDTO dto, bool isUpdate)
        {
            _constitutionDTO = dto;
            _isUpdate = isUpdate;
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, labelTitle, btnClose, btnSave);
            GeneralHelper.ApplyRegularFont(14, txtConstitution, txtAmount, txtSection, txtShortDescription);
        }

        private void FormConstitution_Load(object sender, EventArgs e)
        {
            resizeControls();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit " + _constitutionDTO.ShortDescription;
                txtAmount.Text = _constitutionDTO.Fine.ToString();
                txtSection.Text = _constitutionDTO.Section;
                txtConstitution.Text = _constitutionDTO.ConstitutionText;
                txtShortDescription.Text = _constitutionDTO.ShortDescription;
            }
            else
            {
                labelTitle.Text = "Add Constitution";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string constitutionText = txtConstitution.Text.Trim();
                decimal fine = Convert.ToDecimal(txtAmount.Text.Trim());
                string section = txtSection.Text.Trim();
                string description = txtShortDescription.Text.Trim();

                if (_constitutionDTO.ConstitutionId == 0)
                {
                    var constitution = new Constitution(constitutionText, fine, section, description);
                    _constitutionService.Create(constitution);
                    MessageBox.Show("Constitution created successfully!");
                }
                else
                {
                    var constitution = new Constitution(constitutionText, fine, section, description);

                    _constitutionService.Update(constitution);
                    MessageBox.Show("Constitution updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
