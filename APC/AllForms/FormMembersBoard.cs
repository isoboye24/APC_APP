using APC.Applications.DTO;
using APC.Applications.Entities;
using APC.Applications.Interfaces;
using APC.Helper;
using APC.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static APC.Helper.CommittmentHelper;
using static APC.Helper.MemberHelper;

namespace APC.AllForms
{
    public partial class FormMembersBoard : BaseFormDashboard
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMemberService _memberService;
        private readonly IGenderService _genderService;
        private readonly IPositionService _positionService;
        private readonly IProfessionService _professionService;

        private readonly INationalityService _nationalityService;
        private readonly IMonthService _monthService;
        private readonly IMemberCommittmentService _memberCommittmentService;
        private readonly IGeneralMeetingService _generalMeetingService;
        private readonly IPaymentStatusService _paymentStatusService;

        private readonly IFinedMemberService _finedMemberService;
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IPersonalAttendanceService _personalAttendanceService;
        private readonly IFinancialReportService _financialReportService;

        private readonly ICountryService _countryService;
        private readonly IMaritalStatusService _maritalStatusService;
        private readonly IEmploymentStatusService _employmentStatusService;
        private readonly IRelationshipToNextOfKinService _relationshipToNextOfKinService;
        private readonly IPermissionService _permissionService;
        private readonly IMembershipStatusService _membershipStatusService;

        private List<MemberFullDetailsDTO> _memberFullDetailsDTOs;
        private List<MemberFullDetailsDTO> _memberFullDetailsDTOsContacts;
        private List<MemberFullDetailsDTO> _formerMemberFullDetailsDTOs;
        private List<DeadMemberShortDetailDTO> _deceasedMemberFullDetailsDTOs;
        private List<MemberFullDetailsDTO> _inactiveMemberFullDetailsDTOs;
        private List<MemberFullDetailsDTO> _membersBirthdayFullDetailsDTOs;

        private List<MemberCommittmentDTO> _memberCommittmentDTOs;

        private int currMonth = DateTime.Today.Month;
        private int currYear = DateTime.Today.Year;

        public FormMembersBoard(ICurrentUserService currentUserService, IMemberService memberService, IGenderService genderService,
            IPositionService positionService, IProfessionService professionService, INationalityService nationalityService,
            IMonthService monthService, IMemberCommittmentService memberCommittmentService, IGeneralMeetingService generalMeetingService, 
            IPaymentStatusService paymentStatusService, IFinedMemberService finedMemberService, IGeneralMeetingAttendanceService generalMeetingAttendanceService, 
            IPersonalAttendanceService personalAttendanceService, ICountryService countryService, IMaritalStatusService maritalStatusService,
            IEmploymentStatusService employmentStatusService, IRelationshipToNextOfKinService relationshipToNextOfKinService, IPermissionService permissionService,
            IMembershipStatusService membershipStatusService, IFinancialReportService financialReportService)
        {
            InitializeComponent();
            _currentUserService = currentUserService;
            _memberService = memberService;
            _genderService = genderService;
            _positionService = positionService;
            _professionService = professionService;

            _nationalityService = nationalityService;
            _monthService = monthService;
            _memberCommittmentService = memberCommittmentService;
            _generalMeetingService = generalMeetingService;
            _paymentStatusService = paymentStatusService;

            _finedMemberService = finedMemberService;
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
            _personalAttendanceService = personalAttendanceService;
            _financialReportService = financialReportService;

            _countryService = countryService;
            _maritalStatusService = maritalStatusService;
            _employmentStatusService = employmentStatusService;
            _relationshipToNextOfKinService = relationshipToNextOfKinService;
            _permissionService = permissionService;
            _membershipStatusService = membershipStatusService;
        }

        private void loadRegisteredMembers()
        {
            dataGridViewRegisteredMembers.DataSource = _memberService.GetAllCurrentMembers();
            _memberFullDetailsDTOs = _memberService.GetAllCurrentMembers();
            ConfigureMemberGrid(dataGridViewRegisteredMembers, MemberGridType.SemiBasic);
        }

        private void loadMembersCommittments(int year)
        {
            dataGridViewCommitments.DataSource = _memberCommittmentService.GetMembersCommittment(year);
            _memberCommittmentDTOs = _memberCommittmentService.GetMembersCommittment(year);
            ConfigureMemberCommittmentGrid(dataGridViewCommitments, MemberCommittmentGridType.Basic);
        }

        private void loadMembersContacts()
        {
            dataGridViewContacts.DataSource = _memberService.GetAll();
            _memberFullDetailsDTOsContacts = _memberService.GetAll();
            ConfigureMemberGrid(dataGridViewContacts, MemberGridType.Contact);
        }

        private void loadFormerMembers()
        {
            dataGridViewFormerMembers.DataSource = _memberService.GetFormerMembers();
            _formerMemberFullDetailsDTOs = _memberService.GetFormerMembers();
            ConfigureMemberGrid(dataGridViewFormerMembers, MemberGridType.SemiBasic);
        }
        
        private void loadDeceasedMembers()
        {
            dataGridViewDeadMembers.DataSource = _memberService.GetDeceasedMembers();
            _deceasedMemberFullDetailsDTOs = _memberService.GetDeceasedMembers();
            ConfigureMemberGrid(dataGridViewDeadMembers, MemberGridType.Dead);
        }
        
        private void loadMembersBirthday(int month)
        {
            dataGridViewBirthday.DataSource = _memberService.GetBirthdayMembers(month);
            _membersBirthdayFullDetailsDTOs = _memberService.GetAllCurrentMembers();
            ConfigureMemberGrid(dataGridViewBirthday, MemberGridType.Birthday);
        }
        
        private void loadInactiveMembers()
        {
            dataGridViewInactiveMembers.DataSource = _memberService.GetInactiveMembers();
            _inactiveMemberFullDetailsDTOs = _memberService.GetInactiveMembers();
            ConfigureMemberGrid(dataGridViewInactiveMembers, MemberGridType.SemiBasic);
        }

        private void FormMembersBoard_Load(object sender, EventArgs e)
        {
            if (_currentUserService.AccessLevel != 4)
            {
                btnDeleteRegisteredMembers.Hide();
            }

            //ResizeControls();

            #region
            loadRegisteredMembers();
            FillRegisteredMemberComboBoxes();

            loadMembersContacts();

            loadFormerMembers();
            FillFormerMemberComboBoxes();

            loadMembersBirthday(currMonth);
            FillBirthdayMemberComboBoxes();

            loadDeceasedMembers();
            FillDeadMemberComboBoxes();

            loadInactiveMembers();
            FillInactiveMemberComboBoxes();

            loadMembersCommittments(currYear);
            FillMemberCommittmentComboBoxes();
            #endregion

            // Members' Commitments
            #region
            cmbYearCommittment.DataSource = _generalMeetingService.GetMeetingYears();
            cmbYearCommittment.SelectedIndex = -1;

            #endregion

            GetCounts();
        }

        private void FillRegisteredMemberComboBoxes()
        {
            cmbGenderRegisteredMembers.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderRegisteredMembers, "GenderName", "genderId");

            cmbProfessionRegisteredMembers.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionRegisteredMembers, "ProfessionName", "professionId");

            cmbPositionRegisteredMembers.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionRegisteredMembers, "PositionName", "positionId");

            cmbNationalityRegisteredMembers.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityRegisteredMembers, "NationalityName", "NationalityId");
        }

        private void FillBirthdayMemberComboBoxes()
        {
            cmbGenderBirthday.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderBirthday, "GenderName", "GenderId");

            cmbNationalityBirthday.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityBirthday, "NationalityName", "NationalityId");

            cmbMonthBirthday.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthBirthday, "MonthName", "MonthId");

            cmbPositionBirthday.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionBirthday, "PositionName", "PositionId");

            cmbProfessionBirthday.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionBirthday, "ProfessionName", "ProfessionId");
        }

        private void FillFormerMemberComboBoxes()
        {
            cmbGenderFormerMembers.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderFormerMembers, "GenderName", "genderId");

            cmbProfessionFormerMembers.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionFormerMembers, "ProfessionName", "professionId");

            cmbPositionFormerMembers.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionFormerMembers, "PositionName", "positionId");

            cmbNationalityFormerMembers.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityFormerMembers, "NationalityName", "NationalityId");
        }

        private void FillDeadMemberComboBoxes()
        {
            cmbGenderDeadMembers.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderDeadMembers, "GenderName", "genderId");

            cmbProfessionDeadMembers.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionDeadMembers, "ProfessionName", "professionId");

            cmbPositionDeadMembers.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionDeadMembers, "PositionName", "positionId");

            cmbNationalityDeadMembers.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityDeadMembers, "NationalityName", "NationalityId");
        }

        private void FillInactiveMemberComboBoxes()
        {
            cmbGenderInactiveMembers.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderInactiveMembers, "GenderName", "genderId");

            cmbProfessionInactiveMembers.DataSource = _professionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbProfessionInactiveMembers, "ProfessionName", "professionId");

            cmbPositionInactiveMembers.DataSource = _positionService.GetAll();
            GeneralHelper.ComboBoxProps(cmbPositionInactiveMembers, "PositionName", "positionId");

            cmbNationalityInactiveMembers.DataSource = _nationalityService.GetAll();
            GeneralHelper.ComboBoxProps(cmbNationalityInactiveMembers, "NationalityName", "NationalityId");
        }

        private void FillMemberCommittmentComboBoxes()
        {
            cmbStatusCommittment.DataSource = _paymentStatusService.GetAll();
            GeneralHelper.ComboBoxProps(cmbStatusCommittment, "PaymentStatusName", "PaymentStatusId");
            
            cmbYearCommittment.DataSource = _memberCommittmentService.GetMeetingYears();
            GeneralHelper.ComboBoxProps(cmbYearCommittment, "YearInText", "YearInValue");
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
            loadMembersBirthday(currMonth);

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

            txtNameCommittment.Clear();
            txtSurnameCommittment.Clear();
            cmbYearCommittment.SelectedIndex = -1;
            cmbStatusCommittment.SelectedIndex = -1;
            loadMembersCommittments(currYear);

            txtNameInactiveMembers.Clear();
            txtSurnameInactiveMembers.Clear();
            cmbGenderInactiveMembers.SelectedIndex = -1;
            cmbPositionInactiveMembers.SelectedIndex = -1;
            cmbProfessionInactiveMembers.SelectedIndex = -1;
            loadInactiveMembers();

            GetCounts();
        }
        private void GetCounts()
        {
            svNoOfMenRegisteredMembers.Text = _memberService.GetCurrentMaleCount().ToString();
            svNoOfFemaleRegisteredMembers.Text = _memberService.GetCurrentFemaleCount().ToString();
            svNoOfDivisorRegisteredMembers.Text = _memberService.GetCurrentDivisorCount().ToString();

            svTotalContacts.Text = "Total: " + dataGridViewContacts.RowCount.ToString();

            svNoOfMenFormerMembers.Text = _memberService.GetFormerMaleCount().ToString();
            svNoOfFemaleFormerMembers.Text = _memberService.GetFormerFemaleCount().ToString();
            svNoOfDivisorFormerMembers.Text = _memberService.GetFormerDivisorCount().ToString();

            svNoOfMenDeadMembers.Text = _memberService.GetDeceasedMaleCount().ToString();
            svNoOfFemaleDeadMembers.Text = _memberService.GetDeceasedFemaleCount().ToString();
            svNoOfDivisorDeadMembers.Text = _memberService.GetDeceasedDivisorCount().ToString();

            svTotalRowsCommittment.Text = "Row" + (dataGridViewCommitments.RowCount > 1 ? "s : " : " : ") + dataGridViewCommitments.RowCount.ToString();

        }

        ////////////////////////////////////////////////////////////////////////////
        /// Registered Members 
        ////////////////////////////////////////////////////////////////////////////

        private void btnAddRegisteredMembers_Click(object sender, EventArgs e)
        {
            var form = new FormMembers(_memberService, _countryService, _nationalityService, _professionService, _positionService, _currentUserService,
                                        _genderService, _maritalStatusService, _employmentStatusService, _relationshipToNextOfKinService, _permissionService,
                                        _membershipStatusService);
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

            var form = new FormMembers(_memberService, _countryService, _nationalityService, _professionService, _positionService, _currentUserService,
                                        _genderService, _maritalStatusService, _employmentStatusService, _relationshipToNextOfKinService, _permissionService,
                                        _membershipStatusService);
            form.loadForEdit(selected.MemberId, true);
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

            var form = new FormViewMember(_memberService, _finedMemberService, _generalMeetingAttendanceService, _personalAttendanceService, _financialReportService, 
                                            _monthService, _generalMeetingService);
            form.MemberDetail(selected.MemberId);
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
            if (e.RowIndex < 0)
                return;

            var row = dataGridViewRegisteredMembers.Rows[e.RowIndex];

            var selected = row.DataBoundItem as MemberFullDetailsDTO;

            if (selected == null)
                return;

            string imagePath = Path.Combine(Application.StartupPath, "images", selected.ImagePath);
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

            var form = new FormMembers(_memberService, _countryService, _nationalityService, _professionService, _positionService, _currentUserService,
                                        _genderService, _maritalStatusService, _employmentStatusService, _relationshipToNextOfKinService, _permissionService,
                                        _membershipStatusService);
            form.loadForEdit(selected.MemberId, true);
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

            var form = new FormViewMember(_memberService, _finedMemberService, _generalMeetingAttendanceService, _personalAttendanceService, _financialReportService, 
                                            _monthService, _generalMeetingService);
            form.MemberDetail(selected.MemberId);
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

            var form = new FormMembers(_memberService, _countryService, _nationalityService, _professionService, _positionService, _currentUserService, 
                                        _genderService, _maritalStatusService, _employmentStatusService, _relationshipToNextOfKinService, _permissionService, 
                                        _membershipStatusService);
            form.loadForEdit(selected.MemberId, true);
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

            var form = new FormViewMember(_memberService, _finedMemberService, _generalMeetingAttendanceService, _personalAttendanceService, _financialReportService, 
                                            _monthService, _generalMeetingService);
            form.MemberDetail(selected.MemberId);
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

            var form = new FormMembers(_memberService, _countryService, _nationalityService, _professionService, _positionService, _currentUserService,
                                        _genderService, _maritalStatusService, _employmentStatusService, _relationshipToNextOfKinService, _permissionService,
                                        _membershipStatusService);
            form.loadForEdit(selected.MemberId, true);
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

            var form = new FormViewDeadMember(_memberService, _personalAttendanceService, _generalMeetingAttendanceService, _generalMeetingService, 
                                                _finedMemberService, _monthService, _financialReportService);
            form.MemberDetail(selected);
            form.ShowDialog();

            ClearFilters();
        }

        ////////////////////////////////////////////////////////////////////////////
        /// Members Committments 
        ////////////////////////////////////////////////////////////////////////////

        private MemberCommittmentDTO GetSelectedMemberCommittment()
        {
            if (dataGridViewCommitments.CurrentRow == null)
                return null;

            return dataGridViewCommitments.CurrentRow.DataBoundItem as MemberCommittmentDTO;
        }

        private void dataGridViewCommitments_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dataGridViewCommitments.Rows[e.RowIndex];

            var selected = row.DataBoundItem as MemberCommittmentDTO;

            if (selected == null)
                return;

            string imagePath = Path.Combine(Application.StartupPath, "images", selected.ImagePath);
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
            var selected = GetSelectedMemberCommittment();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormViewMember(_memberService, _finedMemberService, _generalMeetingAttendanceService, _personalAttendanceService, _financialReportService, 
                                            _monthService, _generalMeetingService);
            form.MemberDetail(selected.MemberId);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtNameCommittment_TextChanged(object sender, EventArgs e)
        {
            string search = txtNameCommittment.Text.Trim().ToLower();
            var filtered = _memberCommittmentDTOs.Where(x => x.FirstName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewCommitments.DataSource = filtered;

            GetCounts();
        }

        private void txtSurnameCommittment_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameCommittment.Text.Trim().ToLower();
            var filtered = _memberCommittmentDTOs.Where(x => x.LastName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewCommitments.DataSource = filtered;

            GetCounts();
        }

        private void btnSearchCommittment_Click_1(object sender, EventArgs e)
        {
            if (cmbYearCommittment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select either a year");
                return;
            }
            else
            {
                if (cmbYearCommittment.SelectedIndex != -1)
                {
                    int search = Convert.ToInt32(cmbYearCommittment.SelectedValue);
                    loadMembersCommittments(search);

                    if (cmbStatusCommittment.SelectedIndex != -1)
                    {
                        var filtered = _memberCommittmentDTOs.AsQueryable();
                        string searchStatus = cmbStatusCommittment.Text;

                        filtered = filtered.Where(x =>
                            x.Status.ToString()
                                .IndexOf(searchStatus, StringComparison.OrdinalIgnoreCase) >= 0);

                        dataGridViewCommitments.DataSource = filtered.ToList();
                    }
                }
            }
            
        }

        private void btnClearCommittment_Click_1(object sender, EventArgs e)
        {
            ClearFilters();
        }

        ////////////////////////////////////////////////////////////////////////////
        /// Inactive Members 
        ////////////////////////////////////////////////////////////////////////////

        private void btnClearInactiveMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private MemberFullDetailsDTO GetSelectedInactiveMember()
        {
            if (dataGridViewInactiveMembers.CurrentRow == null)
                return null;

            return dataGridViewInactiveMembers.CurrentRow.DataBoundItem as MemberFullDetailsDTO;
        }

        private void btnViewInactiveMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedInactiveMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormViewMember(_memberService, _finedMemberService, _generalMeetingAttendanceService, _personalAttendanceService, _financialReportService,
                                            _monthService, _generalMeetingService);
            form.MemberDetail(selected.MemberId);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnUpdateInactiveMembers_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedInactiveMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormMembers(_memberService, _countryService, _nationalityService, _professionService, _positionService, _currentUserService,
                                        _genderService, _maritalStatusService, _employmentStatusService, _relationshipToNextOfKinService, _permissionService,
                                        _membershipStatusService);
            form.loadForEdit(selected.MemberId, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtNameInactiveMembers_TextChanged(object sender, EventArgs e)
        {
            string search = txtNameInactiveMembers.Text.Trim().ToLower();
            var filtered = _inactiveMemberFullDetailsDTOs.Where(x => x.FirstName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewInactiveMembers.DataSource = filtered;
        }

        private void txtSurnameInactiveMembers_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameInactiveMembers.Text.Trim().ToLower();
            var filtered = _inactiveMemberFullDetailsDTOs.Where(x => x.LastName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewInactiveMembers.DataSource = filtered;
        }

        private void btnSearchInactiveMembers_Click(object sender, EventArgs e)
        {
            var filtered = _inactiveMemberFullDetailsDTOs.AsQueryable();

            if (cmbPositionInactiveMembers.SelectedIndex == -1 && cmbNationalityInactiveMembers.SelectedIndex == -1
                && cmbGenderInactiveMembers.SelectedIndex == -1 && cmbProfessionInactiveMembers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select either an option from any of the dropdown boxes");
                return;
            }

            if (cmbPositionInactiveMembers.SelectedIndex != -1)
            {
                string search = cmbPositionInactiveMembers.Text;
                filtered = filtered.Where(x => x.Position.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbNationalityInactiveMembers.SelectedIndex != -1)
            {
                string search = cmbNationalityInactiveMembers.Text;
                filtered = filtered.Where(x => x.Nationality.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbGenderInactiveMembers.SelectedIndex != -1)
            {
                string search = cmbGenderInactiveMembers.Text;
                filtered = filtered.Where(x => x.Gender.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbProfessionInactiveMembers.SelectedIndex != -1)
            {
                string search = cmbProfessionInactiveMembers.Text;
                filtered = filtered.Where(x => x.Profession.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            dataGridViewInactiveMembers.DataSource = filtered.ToList();
        }

        ////////////////////////////////////////////////////////////////////////////
        /// Members Birthday
        ////////////////////////////////////////////////////////////////////////////

        private MemberFullDetailsDTO GetSelectedBirthdayMember()
        {
            if (dataGridViewBirthday.CurrentRow == null)
                return null;

            return dataGridViewBirthday.CurrentRow.DataBoundItem as MemberFullDetailsDTO;
        }

        private void txtNameBirthday_TextChanged(object sender, EventArgs e)
        {
            string search = txtNameBirthday.Text.Trim().ToLower();
            var filtered = _membersBirthdayFullDetailsDTOs.Where(x => x.FirstName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewBirthday.DataSource = filtered;
        }
        
        private void txtSurnameBirthday_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameBirthday.Text.Trim().ToLower();
            var filtered = _membersBirthdayFullDetailsDTOs.Where(x => x.LastName.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewBirthday.DataSource = filtered;
        }

        private void btnSearchBirthday_Click(object sender, EventArgs e)
        {
            var filtered = _membersBirthdayFullDetailsDTOs.AsQueryable();

            if (cmbPositionBirthday.SelectedIndex == -1 && cmbNationalityBirthday.SelectedIndex == -1
                && cmbGenderBirthday.SelectedIndex == -1 && cmbProfessionBirthday.SelectedIndex == -1
                && cmbMonthBirthday.SelectedIndex == -1)
            {
                MessageBox.Show("Please select either an option from any of the dropdown boxes");
                return;
            }

            if (cmbPositionBirthday.SelectedIndex != -1)
            {
                string search = cmbPositionBirthday.Text;
                filtered = filtered.Where(x => x.Position.ToString()
                                    .IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbNationalityBirthday.SelectedIndex != -1)
            {
                string search = cmbNationalityBirthday.Text;
                filtered = filtered.Where(x => x.Nationality.ToString()
                                    .IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbGenderBirthday.SelectedIndex != -1)
            {
                string search = cmbGenderBirthday.Text;
                filtered = filtered.Where(x => x.Gender.ToString()
                                    .IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (cmbProfessionBirthday.SelectedIndex != -1)
            {
                string search = cmbProfessionBirthday.Text;
                filtered = filtered.Where(x => x.Profession.ToString()
                                    .IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            
            if (cmbMonthBirthday.SelectedIndex != -1)
            {
                string search = cmbMonthBirthday.Text;
                filtered = filtered.Where(x => x.Birthday.ToString("MMMM")
                                    .Equals(search, StringComparison.OrdinalIgnoreCase));
            }

            dataGridViewBirthday.DataSource = filtered.ToList();
        }

        private void btnClearBirthday_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnViewBirthday_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedBirthdayMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a member from the table");
                return;
            }

            var form = new FormViewMember(_memberService, _finedMemberService, _generalMeetingAttendanceService, _personalAttendanceService, _financialReportService, 
                                            _monthService, _generalMeetingService);
            form.MemberDetail(selected.MemberId);
            form.ShowDialog();

            ClearFilters();
        }

    }
}