using APC.Applications.Interfaces;
using APC.Domain.Entities;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormFinedMember : Form
    {
        private readonly IFinedMemberService _finedMemberService;
        private readonly IMemberService _memberService;
        private readonly IConstitutionService _constitutionService;

        private Applications.DTO.FinedMemberDTO _finedMemberDTO;
        private List<Applications.DTO.MemberFullDetailsDTO> _memberDTO;
        private List<Applications.DTO.ConstitutionDTO> _constitutionDTO;

        private bool _isUpdate = false;
        private int _memberId = 0;
        private int _constitutionId = 0;

        private bool isChangeMember = false;
        private bool isChangeConstitution = false;

        public FormFinedMember(IFinedMemberService finedMemberService, IMemberService memberService, IConstitutionService constitutionService)
        {
            InitializeComponent();
            _finedMemberService = finedMemberService;
            _memberService = memberService;
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

        private void btnChangeMember_Click(object sender, EventArgs e)
        {
            isChangeMember = !isChangeMember;
            if (isChangeMember)
            {
                btnChangeMember.Text = "prev. mem.";
            }
            else
            {
                btnChangeMember.Text = "Change member";
            }
        }

        private void btnChangeConstitution_Click(object sender, EventArgs e)
        {
            isChangeConstitution = !isChangeConstitution;
            if (isChangeConstitution)
            {
                btnChangeConstitution.Text = "prev. const.";
            }
            else
            {
                btnChangeConstitution.Text = "Change const.";
            }
        }

        public void loadForEdit(Applications.DTO.FinedMemberDTO dto, bool isUpdate)
        {
            _finedMemberDTO = dto;
            _isUpdate = isUpdate;
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, label5, label6, label7, label8, label9, label10,
                labelTitle, btnClose, btnSave);
            GeneralHelper.ApplyRegularFont(14, labelName, labelSurname, labelPosition, labelConstitutionSection, labelFine, txtAmount, txtSummary,
                txtSearchSurname, txtShortDescription);
        }

        private void loadMembers()
        {
            dataGridViewMembers.DataSource = _memberService.GetAll();
            _memberDTO = _memberService.GetAll();
            MemberHelper.ConfigureMemberGrid(dataGridViewMembers, MemberHelper.MemberGridType.Basic);
        }

        private Applications.DTO.MemberFullDetailsDTO GetSelectedMember()
        {
            if (dataGridViewMembers.CurrentRow == null)
                return null;

            return dataGridViewMembers.CurrentRow.DataBoundItem as Applications.DTO.MemberFullDetailsDTO;
        }

        private void loadConstitutions()
        {
            dataGridViewConstitutions.DataSource = _constitutionService.GetAll();
            _constitutionDTO = _constitutionService.GetAll();
            ConstitutionHelper.ConfigureConstitutionGrid(dataGridViewConstitutions, ConstitutionHelper.ConstitutionGridType.Basic);
        }

        private Applications.DTO.ConstitutionDTO GetSelectedConstitution()
        {
            if (dataGridViewConstitutions.CurrentRow == null)
                return null;

            return dataGridViewConstitutions.CurrentRow.DataBoundItem as Applications.DTO.ConstitutionDTO;
        }

       
        private void FormFinedMember_Load(object sender, EventArgs e)
        {            
            resizeControls();

            loadMembers();

            loadConstitutions();

            var selectMember = GetSelectedMember();
            var selectConstitution = GetSelectedConstitution();

            if (_isUpdate)
            {
                txtSummary.Text = _finedMemberDTO.Summary;
                txtAmount.Text = _finedMemberDTO.AmountPaid.ToString();
                dateTimePickerFineDate.Value = _finedMemberDTO.FineDate;

                labelTitle.Text = "Edit Fined Member";
                btnChangeMember.Visible = true;
                btnChangeConstitution.Visible = true;

                if (!isChangeConstitution)
                {
                    labelFine.Text = _finedMemberDTO.FormattedAmountExpected;
                    labelConstitutionSection.Text = _finedMemberDTO.Section;
                    _constitutionId = _finedMemberDTO.ConstitutionId;
                }

                if (!isChangeMember)
                {
                    labelPosition.Text = _finedMemberDTO.PositionName;
                    labelSurname.Text = _finedMemberDTO.LastName;
                    labelName.Text = _finedMemberDTO.FirstName;
                    string imagePath = Application.StartupPath + "\\images\\" + _finedMemberDTO.ImagePath;
                    picProfilePic.ImageLocation = imagePath;

                    _memberId = _finedMemberDTO.MemberId;
                }                
            }
            else
            {
                labelTitle.Text = "Add Fined Member";

                btnChangeMember.Hide();
                btnChangeConstitution.Hide();
            }
        }

        
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string imagePath;

            if (e.RowIndex < 0)
                return;

            var row = dataGridViewMembers.Rows[e.RowIndex];

            var selected = row.DataBoundItem as Applications.DTO.MemberFullDetailsDTO;

            if (selected == null)
                return;

            _memberId = selected.MemberId;
            labelSurname.Text = selected.LastName;
            labelName.Text = selected.FirstName;

            imagePath = Application.StartupPath + "\\images\\" + selected.ImagePath;
            picProfilePic.ImageLocation = imagePath;        
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }

        private void dataGridViewConstitutions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dataGridViewConstitutions.Rows[e.RowIndex];

            var selected = row.DataBoundItem as Applications.DTO.ConstitutionDTO;

            if (selected == null)
                return;

            _constitutionId = selected.ConstitutionId;
            labelConstitutionSection.Text = selected.Section;
            labelFine.Text = selected.FineWithCurrency;     
        }

        private void txtSearchSurname_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearchSurname.Text.Trim().ToLower();
            var filtered = _memberDTO.Where(x => x.LastName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewMembers.DataSource = filtered;
        }

        private void txtSection_TextChanged(object sender, EventArgs e)
        {
            string search = txtShortDescription.Text.Trim().ToLower();
            var filtered = _constitutionDTO.Where(x => x.ShortDescription.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewConstitutions.DataSource = filtered;
        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amountPaid = decimal.TryParse(txtAmount.Text.Trim(), out decimal result) ? result : 0;
                string summary = txtSummary.Text.Trim();

                if (!_isUpdate)
                {
                    var finedMember = new FinedMember(amountPaid, summary, _constitutionId, _memberId, DateTime.Today);
                    _finedMemberService.Create(finedMember);
                    MessageBox.Show("Fined member created successfully!");

                    txtAmount.Clear();
                    txtSummary.Clear();
                    dateTimePickerFineDate.Value = DateTime.Today;
                }
                else
                {
                    var finedMember = FinedMember.Rehydrate(_finedMemberDTO.FinedMemberId, amountPaid, summary, _constitutionId, _memberId, _finedMemberDTO.FineDate);

                    _finedMemberService.Update(finedMember);
                    MessageBox.Show("Fined member updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
