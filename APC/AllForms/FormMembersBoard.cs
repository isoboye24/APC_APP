using APC.Applications.DTO;
using APC.Applications.Interfaces;
using APC.Applications.Services;
using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using static APC.Helper.CommittmentHelper;
using static APC.Helper.MemberHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormMembersBoard : Form
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMemberService _memberService;
        private readonly IGenderService _genderService;
        private readonly IPositionService _positionService;
        private readonly IProfessionService _professionService;
        private readonly INationalityService _nationalityService;

        private List<MemberFullDetailsDTO> _memberFullDetailsDTOs;
        private List<MemberFullDetailsDTO> _memberFullDetailsDTOsContacts;
        private List<MemberFullDetailsDTO> _formerMemberFullDetailsDTOs;
        private List<MemberFullDetailsDTO> _deceasedMemberFullDetailsDTOs;

        public FormMembersBoard(ICurrentUserService currentUserService)
        {
            InitializeComponent();
            _currentUserService = currentUserService;
        }

        MembersCommittmentBLL committmentBLL = new MembersCommittmentBLL();
        MembersCommittmentDetailDTO committmentDetail = new MembersCommittmentDetailDTO();
        MembersCommittmentDTO committmentDTO = new MembersCommittmentDTO();

        MemberDetailDTO inactiveMembersDetail = new MemberDetailDTO();
        MemberBLL inactiveMembersBLL = new MemberBLL();
        MemberDTO inactiveMembersDTO = new MemberDTO();

        MemberBLL birthdayBLL = new MemberBLL();
        MemberDTO birthdayDTO = new MemberDTO();
        MemberDetailDTO birthdayDetail = new MemberDetailDTO();

        private void loadRegisteredMembers()
        {
            dataGridViewRegisteredMembers.DataSource = _memberService.GetAll();
            _memberFullDetailsDTOs = _memberService.GetAll();
            ConfigureMemberGrid(dataGridViewRegisteredMembers, MemberGridType.SemiBasic);
        }

        private void loadMembersContacts()
        {
            dataGridViewContacts.DataSource = _memberService.GetAll();
            _memberFullDetailsDTOsContacts = _memberService.GetAll();
            ConfigureMemberGrid(dataGridViewContacts, MemberGridType.Contact);
        }

        private void loadFormerMembers()
        {
            dataGridViewFormerMembers.DataSource = _memberService.GetAll();
            _formerMemberFullDetailsDTOs = _memberService.GetAll();
            ConfigureMemberGrid(dataGridViewFormerMembers, MemberGridType.SemiBasic);
        }
        
        private void loadDeceasedMembers()
        {
            dataGridViewDeadMembers.DataSource = _memberService.GetAll();
            _deceasedMemberFullDetailsDTOs = _memberService.GetAll();
            ConfigureMemberGrid(dataGridViewDeadMembers, MemberGridType.Dead);
        }

        private void FormMembersBoard_Load(object sender, EventArgs e)
        {
            if (_currentUserService.AccessLevel != 4)
            {
                btnDeleteRegisteredMembers.Hide();
            }

            ResizeControls();

            #region
            loadRegisteredMembers();
            FillRegisteredMemberComboBoxes();

            loadMembersContacts();

            loadFormerMembers();
            FillFormerMemberComboBoxes();

            birthdayDTO = birthdayBLL.SelectBirthdayMembers(DateTime.Now.Month);
            inactiveMembersDTO = inactiveMembersBLL.SelectInactiveMembers();
            committmentDTO = committmentBLL.Select(DateTime.Now.Year);

            LoadDataGridView.loadBirthdayMembers(dataGridViewBirthday, birthdayDTO);
            FillBirthdayMemberComboBoxes();

            loadDeceasedMembers();
            FillDeadMemberComboBoxes();

            LoadDataGridView.loadMembers(dataGridViewInactiveMembers, inactiveMembersDTO);
            FillInactiveMemberComboBoxes();

            LoadMemberCommittments();
            FillMemberCommittmentComboBoxes();
            #endregion

            // Members' Commitments
            #region
            cmbYearCommittment.DataSource = committmentDTO.Years;
            cmbYearCommittment.SelectedIndex = -1;

            #endregion

            GetCounts();
        }

        private void LoadMemberCommittments()
        {
            dataGridViewCommitments.DataSource = committmentDTO.Committments;
            ConfigureMemberCommittmentGrid(dataGridViewCommitments, MemberCommittmentGridType.Basic);
        }

        private void ResizeControls()
        {
            #region
            GeneralHelper.ApplyRegularFont(11, label4, label7, label8, label19, label20, label21, label28, label29, label30,
                label31, labelTotalContacts,
                labelNoOfDivisorRegisteredMembers, labelNoOfFemaleRegisteredMembers, labelNoOfMenRegisteredMembers,
                labelNoOfDivisorFormerMembers, labelNoOfFemaleFormerMembers, labelNoOfMenFormerMembers,
                labelNoOfDivisorDeadMembers, labelNoOfFemaleDeadMembers, labelNoOfMenDeadMembers);

            GeneralHelper.ApplyItalicFont(11, label24, label25, label26);

            GeneralHelper.ApplyItalicFont(14, label1, label2, label3, label5, label6, label9, label10, label11, label12,
                label13, label14, label15, label16, label17, label18, label22, label23, label27, label32,
                label33, label34, label35, label36, label38, label41, label42, btnAddRegisteredMembers,
                btnUpdateRegisteredMembers, btnViewRegisteredMembers, btnDeleteRegisteredMembers,
                btnSearchRegisteredMembers, btnClearRegisteredMembers, btnViewBirthday, btnSearchBirthday,
                btnClearBirthday, btnUpdateContacts, btnUpdateFormerMembers, btnViewFormerMembers,
                btnSearchFormerMembers, btnClearFormerMembers, btnUpdateDeadMembers, btnViewDeadMembers,
                btnSearchDeadMembers, btnClearDeadMembers);

            GeneralHelper.ApplyRegularFont(16, txtNameRegisteredMembers, txtSurnameRegisteredMembers,
                cmbGenderRegisteredMembers, cmbNationalityRegisteredMembers, cmbPositionRegisteredMembers,
                cmbProfessionRegisteredMembers, txtNameBirthday, txtSurnameBirthday, cmbGenderBirthday,
                cmbNationalityBirthday, cmbProfessionBirthday, cmbPositionBirthday, cmbMonthBirthday,
                txtSurnameContacts, txtNameFormerMembers, txtSurnameFormerMembers, cmbGenderFormerMembers,
                cmbNationalityFormerMembers, cmbPositionFormerMembers, cmbProfessionFormerMembers,
                txtNameDeadMembers, txtSurnameDeadMembers, cmbGenderDeadMembers, cmbNationalityDeadMembers,
                cmbPositionDeadMembers, cmbProfessionDeadMembers, cmbYearCommittment, cmbStatusCommittment, 
                txtNameCommittment, txtSurnameCommittment
                );

            txtNameCommittment.Tag = "resizeable";
            txtSurnameCommittment.Tag = "resizeable";
            cmbStatusCommittment.Tag = "resizeable";
            cmbYearCommittment.Tag = "resizeable";
            #endregion
        }

        private void FillRegisteredMemberComboBoxes()
        {
            cmbGenderRegisteredMembers.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderRegisteredMembers, "GenderName", "genderID");
            cmbProfessionRegisteredMembers.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionRegisteredMembers, "Profession", "professionID");
            cmbPositionRegisteredMembers.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionRegisteredMembers, "PositionName", "positionID");
            cmbNationalityRegisteredMembers.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityRegisteredMembers, "Nationality", "NationalityID");
        }

        private void FillBirthdayMemberComboBoxes()
        {
            cmbGenderBirthday.DataSource = birthdayDTO.Genders;
            GeneralHelper.ComboBoxProps(cmbGenderBirthday, "GenderName", "GenderID");
            cmbNationalityBirthday.DataSource = birthdayDTO.Nationalities;
            GeneralHelper.ComboBoxProps(cmbNationalityBirthday, "Nationality", "NationalityID");
            cmbMonthBirthday.DataSource = birthdayDTO.Months;
            GeneralHelper.ComboBoxProps(cmbMonthBirthday, "MonthName", "MonthID");
            cmbPositionBirthday.DataSource = birthdayDTO.Positions;
            GeneralHelper.ComboBoxProps(cmbPositionBirthday, "PositionName", "PositionID");
            cmbProfessionBirthday.DataSource = birthdayDTO.Professions;
            GeneralHelper.ComboBoxProps(cmbProfessionBirthday, "Profession", "professionID");
        }
        private void FillFormerMemberComboBoxes()
        {
            cmbGenderFormerMembers.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderFormerMembers, "GenderName", "genderID");
            cmbProfessionFormerMembers.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionFormerMembers, "Profession", "professionID");
            cmbPositionFormerMembers.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionFormerMembers, "PositionName", "positionID");
            cmbNationalityFormerMembers.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityFormerMembers, "Nationality", "NationalityID");
        }

        private void FillDeadMemberComboBoxes()
        {
            cmbGenderDeadMembers.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderDeadMembers, "GenderName", "genderID");
            cmbProfessionDeadMembers.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionDeadMembers, "Profession", "professionID");
            cmbPositionDeadMembers.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionDeadMembers, "PositionName", "positionID");
            cmbNationalityDeadMembers.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityDeadMembers, "Nationality", "NationalityID");
        }

        private void FillInactiveMemberComboBoxes()
        {
            cmbGenderInactiveMembers.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderInactiveMembers, "GenderName", "genderID");
            cmbProfessionInactiveMembers.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionInactiveMembers, "Profession", "professionID");
            cmbPositionInactiveMembers.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionInactiveMembers, "PositionName", "positionID");
            cmbNationalityInactiveMembers.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityInactiveMembers, "Nationality", "NationalityID");
        }

        private void FillMemberCommittmentComboBoxes()
        {
            cmbStatusCommittment.DataSource = committmentDTO.PaymentStatuses;
            GeneralHelper.ComboBoxProps(cmbStatusCommittment, "PaymentStatusName", "PaymentStatusID");
        }

        private void ClearFilters()
        {
            txtNameRegisteredMembers.Clear();
            txtSurnameRegisteredMembers.Clear();
            cmbNationalityRegisteredMembers.SelectedIndex = -1;
            cmbGenderRegisteredMembers.SelectedIndex = -1;
            cmbPositionRegisteredMembers.SelectedIndex = -1;
            cmbProfessionRegisteredMembers.SelectedIndex = -1;
            loadRegisteredMembers();

            txtNameBirthday.Clear();
            txtSurnameBirthday.Clear();
            cmbGenderBirthday.SelectedIndex = -1;
            cmbNationalityBirthday.SelectedIndex = -1;
            cmbProfessionBirthday.SelectedIndex = -1;
            cmbPositionBirthday.SelectedIndex = -1;
            cmbMonthBirthday.SelectedIndex = -1;
            birthdayDTO = birthdayBLL.SelectBirthdayMembers(DateTime.Now.Month);
            dataGridViewBirthday.DataSource = birthdayDTO.Members;

            txtSurnameContacts.Clear();
            loadMembersContacts();

            txtNameFormerMembers.Clear();
            txtSurnameFormerMembers.Clear();
            cmbNationalityFormerMembers.SelectedIndex = -1;
            cmbGenderFormerMembers.SelectedIndex = -1;
            cmbPositionFormerMembers.SelectedIndex = -1;
            cmbProfessionFormerMembers.SelectedIndex = -1;
            loadFormerMembers();

            txtNameDeadMembers.Clear();
            txtSurnameDeadMembers.Clear();
            cmbNationalityDeadMembers.SelectedIndex = -1;
            cmbGenderDeadMembers.SelectedIndex = -1;
            cmbPositionDeadMembers.SelectedIndex = -1;
            cmbProfessionDeadMembers.SelectedIndex = -1;
            loadDeceasedMembers();

            committmentBLL = new MembersCommittmentBLL();
            committmentDTO = committmentBLL.Select(DateTime.Now.Year);
            dataGridViewCommitments.DataSource = committmentDTO.Committments;
            txtNameCommittment.Clear();
            txtSurnameCommittment.Clear();
            cmbYearCommittment.SelectedIndex = -1;
            cmbStatusCommittment.SelectedIndex = -1;

            inactiveMembersBLL = new MemberBLL();
            inactiveMembersDTO = inactiveMembersBLL.SelectInactiveMembers();
            dataGridViewInactiveMembers.DataSource = inactiveMembersDTO.Members;
            txtNameInactiveMembers.Clear();
            txtSurnameInactiveMembers.Clear();
            cmbGenderInactiveMembers.SelectedIndex = -1;
            cmbPositionInactiveMembers.SelectedIndex = -1;
            cmbProfessionInactiveMembers.SelectedIndex = -1;

            GetCounts();
        }
        private void GetCounts()
        {
            labelNoOfMenRegisteredMembers.Text = _memberService.GetCurrentMaleCount().ToString();
            labelNoOfFemaleRegisteredMembers.Text = _memberService.GetCurrentFemaleCount().ToString();
            labelNoOfDivisorRegisteredMembers.Text = _memberService.GetCurrentDivisorCount().ToString();

            labelTotalContacts.Text = "Total: " + dataGridViewContacts.RowCount.ToString();

            labelNoOfMenFormerMembers.Text = _memberService.GetFormerMaleCount().ToString();
            labelNoOfFemaleFormerMembers.Text = _memberService.GetFormerFemaleCount().ToString();
            labelNoOfDivisorFormerMembers.Text = _memberService.GetFormerDivisorCount().ToString();

            labelNoOfMenDeadMembers.Text = _memberService.GetDeceasedMaleCount().ToString();
            labelNoOfFemaleDeadMembers.Text = _memberService.GetDeceasedFemaleCount().ToString();
            labelNoOfDivisorDeadMembers.Text = _memberService.GetDeceasedDivisorCount().ToString();

            labelTotalRowsCommittment.Text = "Row" + (dataGridViewCommitments.RowCount > 1 ? "s : " : " : ") + dataGridViewCommitments.RowCount.ToString();

        }

        ////////////////////////////////////////////////////////////////////////////
        /// Registered Members 
        ////////////////////////////////////////////////////////////////////////////

        private void btnAddRegisteredMembers_Click(object sender, EventArgs e)
        {
            var form = new FormMembers(_memberService);
            form.ShowDialog();

            ClearFilters();
        }

        private MemberFullDetailsDTO GetSelectedRegisteredMembers()
        {
            if (dataGridViewRegisteredMembers.CurrentRow == null)
                return null;

            return dataGridViewRegisteredMembers.CurrentRow.DataBoundItem as MemberFullDetailsDTO;
        }

        private void btnUpdateRegisteredMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedRegisteredMembers();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormMembers(_memberService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnViewRegisteredMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedRegisteredMembers();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormViewMember(_memberService);
            form.MemberDetail(selected);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtNameRegisteredMembers_TextChanged(object sender, EventArgs e)
        {
            string search = txtNameRegisteredMembers.Text.Trim().ToLower();
            var filtered = _memberFullDetailsDTOs.Where(x => x.FirstName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewRegisteredMembers.DataSource = filtered;
        }

        private void txtSurnameRegisteredMembers_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameRegisteredMembers.Text.Trim().ToLower();
            var filtered = _memberFullDetailsDTOs.Where(x => x.LastName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewRegisteredMembers.DataSource = filtered;
        }

        private void btnSearchRegisteredMembers_Click(object sender, EventArgs e)
        {
            var filtered = _memberFullDetailsDTOs.AsQueryable();

            if (cmbPositionRegisteredMembers.SelectedIndex == -1 && cmbNationalityRegisteredMembers.SelectedIndex == -1
                && cmbGenderRegisteredMembers.SelectedIndex == -1 && cmbProfessionRegisteredMembers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select either an option from any of the dropdown boxes");
                return;
            }

            if (cmbPositionRegisteredMembers.SelectedIndex != -1)
            {
                string search = cmbPositionRegisteredMembers.Text;
                filtered = filtered.Where(x => x.Position.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbNationalityRegisteredMembers.SelectedIndex != -1)
            {
                string search = cmbNationalityRegisteredMembers.Text;
                filtered = filtered.Where(x => x.Nationality.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            
            if (cmbGenderRegisteredMembers.SelectedIndex != -1)
            {
                string search = cmbGenderRegisteredMembers.Text;
                filtered = filtered.Where(x => x.Gender.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            
            if (cmbProfessionRegisteredMembers.SelectedIndex != -1)
            {
                string search = cmbProfessionRegisteredMembers.Text;
                filtered = filtered.Where(x => x.Profession.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            dataGridViewRegisteredMembers.DataSource = filtered.ToList();
        }

        private void btnClearRegisteredMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnDeleteRegisteredMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedRegisteredMembers();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _memberService.Delete(selected.MemberId);
                ClearFilters();
            }            
        }

        private void dataGridViewRegisteredMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var registeredMembersDetail = new MemberFullDetailsDTO();
            registeredMembersDetail = GeneralHelper.MapFromGrid<MemberFullDetailsDTO>(dataGridViewRegisteredMembers, e.RowIndex);

            string imagePath = Path.Combine(Application.StartupPath, "images", registeredMembersDetail.ImagePath);
            picRegisteredMember.ImageLocation = imagePath;
        }

        ////////////////////////////////////////////////////////////////////////////
        /// Member Contacts
        ////////////////////////////////////////////////////////////////////////////

        private void txtSurnameContacts_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameContacts.Text.Trim().ToLower();
            var filtered = _memberFullDetailsDTOsContacts.Where(x => x.LastName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewContacts.DataSource = filtered;
        }

        private MemberFullDetailsDTO GetSelectedMemberContact()
        {
            if (dataGridViewContacts.CurrentRow == null)
                return null;

            return dataGridViewContacts.CurrentRow.DataBoundItem as MemberFullDetailsDTO;
        }

        private void btnUpdateContacts_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMemberContact();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormMembers(_memberService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnViewContacts_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMemberContact();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormViewMember(_memberService);
            form.MemberDetail(selected);
            form.ShowDialog();

            ClearFilters();
        }

        ////////////////////////////////////////////////////////////////////////////
        /// Former Members 
        ////////////////////////////////////////////////////////////////////////////

        private void btnClearFormerMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private MemberFullDetailsDTO GetSelectedFormerMember()
        {
            if (dataGridViewFormerMembers.CurrentRow == null)
                return null;

            return dataGridViewFormerMembers.CurrentRow.DataBoundItem as MemberFullDetailsDTO;
        }

        private void btnUpdateFormerMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFormerMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormMembers(_memberService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnViewFormerMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFormerMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormViewMember(_memberService);
            form.MemberDetail(selected);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtNameFormerMembers_TextChanged(object sender, EventArgs e)
        {
            string search = txtNameFormerMembers.Text.Trim().ToLower();
            var filtered = _formerMemberFullDetailsDTOs.Where(x => x.FirstName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewFormerMembers.DataSource = filtered;
        }

        private void txtSurnameFormerMembers_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameFormerMembers.Text.Trim().ToLower();
            var filtered = _formerMemberFullDetailsDTOs.Where(x => x.LastName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewFormerMembers.DataSource = filtered;
        }

        private void btnSearchFormerMembers_Click(object sender, EventArgs e)
        {
            var filtered = _formerMemberFullDetailsDTOs.AsQueryable();

            if (cmbPositionFormerMembers.SelectedIndex == -1 && cmbNationalityFormerMembers.SelectedIndex == -1
                && cmbGenderFormerMembers.SelectedIndex == -1 && cmbProfessionFormerMembers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select either an option from any of the dropdown boxes");
                return;
            }

            if (cmbPositionFormerMembers.SelectedIndex != -1)
            {
                string search = cmbPositionFormerMembers.Text;
                filtered = filtered.Where(x => x.Position.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbNationalityFormerMembers.SelectedIndex != -1)
            {
                string search = cmbNationalityFormerMembers.Text;
                filtered = filtered.Where(x => x.Nationality.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbGenderFormerMembers.SelectedIndex != -1)
            {
                string search = cmbGenderFormerMembers.Text;
                filtered = filtered.Where(x => x.Gender.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbProfessionFormerMembers.SelectedIndex != -1)
            {
                string search = cmbProfessionFormerMembers.Text;
                filtered = filtered.Where(x => x.Profession.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            dataGridViewFormerMembers.DataSource = filtered.ToList();
        }

        ////////////////////////////////////////////////////////////////////////////
        /// Deceased Members 
        ////////////////////////////////////////////////////////////////////////////

        private MemberFullDetailsDTO GetSelectedDeceasedMember()
        {
            if (dataGridViewDeadMembers.CurrentRow == null)
                return null;

            return dataGridViewDeadMembers.CurrentRow.DataBoundItem as MemberFullDetailsDTO;
        }


        private void btnUpdateDeadMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedDeceasedMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormMembers(_memberService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtNameDeadMembers_TextChanged(object sender, EventArgs e)
        {
            string search = txtNameDeadMembers.Text.Trim().ToLower();
            var filtered = _deceasedMemberFullDetailsDTOs.Where(x => x.FirstName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewDeadMembers.DataSource = filtered;
        }

        private void txtSurnameDeadMembers_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameDeadMembers.Text.Trim().ToLower();
            var filtered = _deceasedMemberFullDetailsDTOs.Where(x => x.LastName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewDeadMembers.DataSource = filtered;
        }

        private void btnSearchDeadMembers_Click(object sender, EventArgs e)
        {
            var filtered = _deceasedMemberFullDetailsDTOs.AsQueryable();

            if (cmbPositionDeadMembers.SelectedIndex == -1 && cmbNationalityDeadMembers.SelectedIndex == -1
                && cmbGenderDeadMembers.SelectedIndex == -1 && cmbProfessionDeadMembers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select either an option from any of the dropdown boxes");
                return;
            }

            if (cmbPositionDeadMembers.SelectedIndex != -1)
            {
                string search = cmbPositionDeadMembers.Text;
                filtered = filtered.Where(x => x.Position.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbNationalityDeadMembers.SelectedIndex != -1)
            {
                string search = cmbNationalityDeadMembers.Text;
                filtered = filtered.Where(x => x.Nationality.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbGenderDeadMembers.SelectedIndex != -1)
            {
                string search = cmbGenderDeadMembers.Text;
                filtered = filtered.Where(x => x.Gender.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbProfessionDeadMembers.SelectedIndex != -1)
            {
                string search = cmbProfessionDeadMembers.Text;
                filtered = filtered.Where(x => x.Profession.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            dataGridViewDeadMembers.DataSource = filtered.ToList();
        }

        private void btnClearDeadMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnViewDeadMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedDeceasedMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormViewMember(_memberService);
            form.MemberDetail(selected);
            form.ShowDialog();

            ClearFilters();
        }
        
        private void dataGridViewCommitments_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            committmentDetail = GeneralHelper.MapFromGrid<MembersCommittmentDetailDTO>(dataGridViewCommitments, e.RowIndex);

            // Load image
            string imagePath = Path.Combine(Application.StartupPath, "images", committmentDetail.ImagePath);
            picCommittment.ImageLocation = imagePath;
        }

        private void dataGridViewCommitments_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Draw row numbers on the row header
            using (Font font = new Font("Segoe UI", 14, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(dataGridViewCommitments.RowHeadersDefaultCellStyle.ForeColor))
            {
                string rowNumber = (e.RowIndex + 1).ToString();
                e.Graphics.DrawString(
                    rowNumber,
                    font,
                    brush,
                    e.RowBounds.Location.X + 15,
                    e.RowBounds.Location.Y + 4
                );
            }
        }

        private void btnViewCommittment_Click(object sender, EventArgs e)
        {
            if (committmentDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormViewMember open = new FormViewMember();
                MemberBLL bll = new MemberBLL();
                MemberDTO DTO = new MemberDTO();
                DTO = bll.Select();
                MemberDetailDTO committmentMember = DTO.Members.FirstOrDefault(x => x.MemberID == committmentDetail.MemberID);
                
                if (committmentMember == null)
                {
                    MessageBox.Show("No member found!");
                    return;
                }

                open.detail = committmentMember;
                open.memberID = committmentMember.MemberID;
                open.isCommittment = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtNameCommittment_TextChanged(object sender, EventArgs e)
        {
            List<MembersCommittmentDetailDTO> list = committmentDTO.Committments;
            list = list.Where(x => x.Name.Contains(txtNameCommittment.Text.Trim())).ToList();
            dataGridViewCommitments.DataSource = list;
            GetCounts();
        }

        private void txtSurnameCommittment_TextChanged(object sender, EventArgs e)
        {
            List<MembersCommittmentDetailDTO> list = committmentDTO.Committments;
            list = list.Where(x => x.Surname.Contains(txtSurnameCommittment.Text.Trim())).ToList();
            dataGridViewCommitments.DataSource = list;
            GetCounts();
        }


        private void btnClearInactiveMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void dataGridViewInactiveMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            inactiveMembersDetail = GeneralHelper.MapFromGrid<MemberDetailDTO>(dataGridViewInactiveMembers, e.RowIndex);
        }

        private void btnViewInactiveMembers_Click(object sender, EventArgs e)
        {
            ViewMember(inactiveMembersDetail, false);
        }

        private void btnUpdateInactiveMembers_Click(object sender, EventArgs e)
        {
            if (inactiveMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table.");
            }
            else
            {
                FormMembers open = new FormMembers();
                open.isUpdate = true;
                open.detail = inactiveMembersDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtNameBirthday_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void txtSurnameBirthday_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchBirthday_Click(object sender, EventArgs e)
        {
            birthdayBLL = new MemberBLL();
            List<MemberDetailDTO> list = birthdayDTO.Members;

            if (cmbMonthBirthday.SelectedIndex != -1)
            {
                birthdayDTO = birthdayBLL.SelectBirthdayMembers(Convert.ToInt32(cmbMonthBirthday.SelectedValue));
                
                if (cmbGenderBirthday.SelectedIndex != -1)
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(cmbMonthBirthday.SelectedValue) && x.GenderName == cmbGenderBirthday.SelectedValue.ToString()).ToList();
                }
                else if (cmbPositionBirthday.SelectedIndex != -1)
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(cmbMonthBirthday.SelectedValue) && x.PositionName == cmbPositionBirthday.SelectedValue.ToString()).ToList();
                }
                else if (cmbProfessionBirthday.SelectedIndex != -1)
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(cmbMonthBirthday.SelectedValue) && x.ProfessionName == cmbProfessionBirthday.SelectedValue.ToString()).ToList();
                }
                else if (cmbNationalityBirthday.SelectedIndex != -1)
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(cmbMonthBirthday.SelectedValue) && x.NationalityName == cmbNationalityBirthday.SelectedValue.ToString()).ToList();
                }
                else if (txtNameBirthday.Text.Trim() != "")
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(cmbMonthBirthday.SelectedValue) && x.Name.Contains(txtNameBirthday.Text.Trim())).ToList();
                }
                else if (txtSurnameBirthday.Text.Trim() != "")
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(cmbMonthBirthday.SelectedValue) && x.Surname.Contains(txtSurnameBirthday.Text.Trim())).ToList();
                }
                else
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(cmbMonthBirthday.SelectedValue)).ToList();
                }                    
            }
            else
            {
                if (cmbGenderBirthday.SelectedIndex != -1)
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(DateTime.Now.Month) && x.GenderName == cmbGenderBirthday.SelectedValue.ToString()).ToList();
                }
                else if (cmbPositionBirthday.SelectedIndex != -1)
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(DateTime.Now.Month) && x.PositionName == cmbPositionBirthday.SelectedValue.ToString()).ToList();
                }
                else if (cmbProfessionBirthday.SelectedIndex != -1)
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(DateTime.Now.Month) && x.ProfessionName == cmbProfessionBirthday.SelectedValue.ToString()).ToList();
                }
                else if (cmbNationalityBirthday.SelectedIndex != -1)
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(DateTime.Now.Month) && x.NationalityName == cmbNationalityBirthday.SelectedValue.ToString()).ToList();
                }
                else if (txtNameBirthday.Text.Trim() != "")
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(DateTime.Now.Month) && x.Name.Contains(txtNameBirthday.Text.Trim())).ToList();
                }
                else if (txtSurnameBirthday.Text.Trim() != "")
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(DateTime.Now.Month) && x.Surname.Contains(txtSurnameBirthday.Text.Trim())).ToList();
                }
                else
                {
                    list = list.Where(x => x.Birthday.Month == Convert.ToInt32(DateTime.Now.Month)).ToList();
                }
            }

            dataGridViewBirthday.DataSource = list;
        }

        private void btnClearBirthday_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnViewBirthday_Click(object sender, EventArgs e)
        {
            ViewMember(birthdayDetail, false);
        }

        private void dataGridViewBirthday_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            birthdayDetail = GeneralHelper.MapFromGrid<MemberDetailDTO>(dataGridViewBirthday, e.RowIndex);
        }

        private void ViewMember(MemberDetailDTO detail, bool isFormer)
        {
            if (detail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormViewMember open = new FormViewMember();
                open.detail = detail;
                open.isView = true;
                if (isFormer)
                {
                    open.isFormer = true;
                }                
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnSearchInactiveMembers_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchCommittment_Click_1(object sender, EventArgs e)
        {
            if (cmbYearCommittment.SelectedIndex != -1)
            {
                committmentBLL = new MembersCommittmentBLL();
                committmentDTO = committmentBLL.Select(Convert.ToInt32(cmbYearCommittment.SelectedValue));
                dataGridViewCommitments.DataSource = committmentDTO.Committments;

                List<MembersCommittmentDetailDTO> list = committmentDTO.Committments;
                if (cmbStatusCommittment.SelectedIndex != -1)
                {
                    string selectedStatus = cmbStatusCommittment.Text;

                    list = list.Where(x => x.PaymentStatus == selectedStatus).ToList();
                    dataGridViewCommitments.DataSource = list;
                    GetCounts();
                }
            }
            else
            {
                MessageBox.Show("Please select year");
            }
        }

        private void btnClearCommittment_Click_1(object sender, EventArgs e)
        {
            ClearFilters();
        }

        
    }
}