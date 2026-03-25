using APC.Applications.Interfaces;
using APC.Applications.Services;
using APC.BLL;
using APC.DAL.DTO;
using APC.Domain.Entities;
using APC.Helper;
using APC.Utility;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormSpecialContribution : Form
    {
        private readonly ISpecialContributionService _specialContributionService;
        private readonly IMemberService _memberService;

        private Applications.DTO.SpecialContributionDTO _specialContributionDTO;
        private bool _isUpdate = false;
        private bool _isChangeMember = false;
        private int buttonSize = 14;
        private float panelSize;

        public FormSpecialContribution(ISpecialContributionService specialContributionService, IMemberService memberService)
        {
            InitializeComponent();
            _specialContributionService = specialContributionService;
            _memberService = memberService;
        }

        /// <summary>
        ///  Drag
        /// </summary>
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int IParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                buttonSize = 18;
                panelSize = 3.05f;
                panel1.Size = new Size(1001, 60);
                iconZoomIn.IconSize = 40;
                iconZoomOut.IconSize = 40;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonSize = 14;
                panelSize = 1.05f;
                panel1.Size = new Size(1001, 51);
                iconZoomIn.IconSize = 32;
                iconZoomOut.IconSize = 32;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
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
                buttonSize = 14;
                panelSize = 1.05f;
                panel1.Size = new Size(1001, 51);
                iconZoomIn.IconSize = 32;
                iconZoomOut.IconSize = 32;

                ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
            }
        }

        private void iconZoomOut_Click(object sender, EventArgs e)
        {
            buttonSize -= 1;
            panelSize -= 1.05f;
            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private void iconZoomIn_Click(object sender, EventArgs e)
        {
            buttonSize += 1;
            panelSize += 1.05f;

            ControlResize.ResizeTaggedControls(this, buttonSize, panelSize);
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangeSupervisor_Click(object sender, EventArgs e)
        {
            _isChangeMember = !_isChangeMember;
            if (_isChangeMember)
            {
                btnChangeSupervisor.Text = "prev. Supervisor";
            }
            else
            {
                btnChangeSupervisor.Text = "Change Supervisor";
            }
        }

        public void loadForEdit(Applications.DTO.SpecialContributionDTO dto, bool isUpdate)
        {
            _specialContributionDTO = dto;
            _isUpdate = isUpdate;
        }

        private void loadMembers()
        {
            dataGridViewMembers.DataSource = _memberService.GetAll();
            MemberHelper.ConfigureMemberGrid(dataGridViewMembers, MemberHelper.MemberGridType.Basic);
        }

        private void FormSpecialContribution_Load(object sender, EventArgs e)
        {
            btnChangeSupervisor.Hide();

            loadMembers();

            if (_isUpdate)
            {
                labelTitle.Text = "Edit " + _specialContributionDTO.Title;

                txtTitle.Text = _specialContributionDTO.Title;
                txtSummary.Text = _specialContributionDTO.Summary;
                txtAmountExpected.Text = _specialContributionDTO.AmountExpected.ToString();
                txtAmountToContribute.Text = _specialContributionDTO.AmountToContribute.ToString();
                dateTimePickerStartDate.Value = _specialContributionDTO.ContributionStartDate;
                dateTimePickerEndDate.Value = _specialContributionDTO.ContributionEndDate;
                labelSupervisorsName.Text = _specialContributionDTO.FirstName;
                labelSupervisorsSurname.Text = _specialContributionDTO.LastName;

                string imagePath = Application.StartupPath + "\\images\\" + _specialContributionDTO.ImagePath;
                picSupervisor.ImageLocation = imagePath;

                btnChangeSupervisor.Visible = true;

            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amountToContribute = Convert.ToDecimal(txtAmountToContribute.Text.Trim());
                decimal amountExpected = Convert.ToDecimal(txtAmountExpected.Text.Trim());
                string summary = txtSummary.Text.Trim();
                string title = txtTitle.Text.Trim();
                DateTime startDate = dateTimePickerStartDate.Value;
                DateTime endDate = dateTimePickerEndDate.Value;

                if (_specialContributionDTO.SpecialContributionId == 0)
                {
                    var contribution = new SpecialContribution(title, summary, amountToContribute, _specialContributionDTO.SupervisorId, 
                        startDate, endDate, amountExpected);
                    _specialContributionService.Create(contribution);
                    MessageBox.Show("Special contribution created successfully!");
                }
                else
                {
                    var contribution = new SpecialContribution(title, summary, amountToContribute, _specialContributionDTO.SupervisorId,
                        startDate, endDate, amountExpected);

                    _specialContributionService.Update(contribution);
                    MessageBox.Show("Special contribution updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (_isChangeMember)
            {
                string imagePath = Application.StartupPath + "\\images\\" + _specialContributionDTO.ImagePath;
                picSupervisor.ImageLocation = imagePath;

                labelSupervisorsName.Text = _specialContributionDTO.FirstName;
                labelSupervisorsSurname.Text = _specialContributionDTO.LastName;
            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
