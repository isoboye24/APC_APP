using APC.Applications.Interfaces;
using APC.Helper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace APC.AllForms
{
    public partial class FormMeetingBoard : Form
    {
        private readonly IMemberService _memberService;
        private readonly IGenderService _genderService;
        private readonly IMonthService _monthService;
        private readonly IConstitutionService _constitutionService;
        private readonly IFinedMemberService _finedMemberService;
        private readonly ISpecialContributionService _specialContributionService;
        private readonly ISpecialContributorService _specialContributorService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGeneralMeetingService _generalMeetingService;
        private readonly IGeneralMeetingAttendanceService _generalMeetingAttendanceService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAttendanceStatusService _attendanceStatusService;
        private readonly IPaymentStatusService _paymentStatusService;

        private List<Applications.DTO.ConstitutionDTO> _constitutionDTO;
        private List<Applications.DTO.FinedMemberDTO> _finedMemberDTO;
        private List<Applications.DTO.SpecialContributionDTO> _specialContributionDTO;
        private List<Applications.DTO.SpecialContributorDTO> _specialContributorsDTO;
        private List<Applications.DTO.GeneralMeetingDTO> _generalMeetingDTOs;

        private int year = DateTime.Now.Year;

        public FormMeetingBoard(IGenderService genderService, IMemberService memberService, 
            IMonthService monthService, IConstitutionService constitutionService, IFinedMemberService finedMemberService, 
            ISpecialContributionService specialContributionService, ISpecialContributorService specialContributorService, 
            ICurrentUserService currentUserService, IGeneralMeetingService generalMeetingService, 
            IGeneralMeetingAttendanceService generalMeetingAttendanceService, IServiceProvider serviceProvider, 
            IAttendanceStatusService attendanceStatusService, IPaymentStatusService paymentStatusService)
        {
            InitializeComponent();
            _genderService = genderService;
            _memberService = memberService;
            _monthService = monthService;
            _constitutionService = constitutionService;
            _finedMemberService = finedMemberService;
            _specialContributionService = specialContributionService;
            _specialContributorService = specialContributorService;
            _currentUserService = currentUserService;
            _generalMeetingService = generalMeetingService;
            _generalMeetingAttendanceService = generalMeetingAttendanceService;
            _serviceProvider = serviceProvider;
            _attendanceStatusService = attendanceStatusService;
            _paymentStatusService = paymentStatusService;
        }

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, rbEqualAttend, rbEqualMonDues, rbLessAttend, rbLessMonDues,
                rbMoreAttend, rbMoreMonDues, btnUpdate, btnView, btnAdd, btnAbsentees, btnDelete, btnSearch, btnClear, label18, label19, label21, btnAddConstitution, btnDeleteConstitution, btnUpdateConstitution,
                btnViewConstitution, btnClearConstitution, label12, label20, label22, label23, label24, label25, label26, btnSearchFinedMember,
                btnClearFinedMember);

            GeneralHelper.ApplyRegularFont(11, labelTotalMeetings, labelTotalPaidFines, labelTotalConstitutions);

            GeneralHelper.ApplyRegularFont(14, labelTotalFineMembers);

            GeneralHelper.ApplyRegularFont(16, txtMonthlyDues, txtNoOfAttend, txtYear, cmbMonth, txtNameFinedMember, txtSurnameFinedMember,
                txtConstitutionSection, cmbGenderFinedMember, cmbMonthFinedMember, cmbFineStatus, cmbYearMeeting);
        }


        private void loadConstitutions()
        {
            dataGridViewConstitution.DataSource = _constitutionService.GetAll();
            _constitutionDTO = _constitutionService.GetAll();
            ConstitutionHelper.ConfigureConstitutionGrid(dataGridViewConstitution, ConstitutionHelper.ConstitutionGridType.Normal);
        }
        
        private void loadFinedMembers()
        {
            dataGridViewFinedMembers.DataSource = _finedMemberService.GetAll();
            _finedMemberDTO = _finedMemberService.GetAll();
            FinedMemberHelper.ConfigureFinedMemberGrid(dataGridViewFinedMembers, FinedMemberHelper.FinedMemberGridType.Basic);
        }
        
        private void loadSpecialContributions()
        {
            dataGridViewSpecialContributions.DataSource = _specialContributionService.GetAll();
            _specialContributionDTO = _specialContributionService.GetAll();
            SpecialContributionHelper.ConfigureSpecialContributionGrid(dataGridViewSpecialContributions, SpecialContributionHelper.SpecialContributionGridType.Basic);
        }

        private void loadGeneralMeeting(int year)
        {
            dataGridViewGeneralMeeting.DataSource = _generalMeetingService.GetAllByYear(year);
            _generalMeetingDTOs = _generalMeetingService.GetAll();
            GeneralMeetingHelper.ConfigureGeneralMeetingGrid(dataGridViewGeneralMeeting, GeneralMeetingHelper.GeneralMeetingGridType.Basic);
        }

        private void loadGeneralMeetingAttendance(int year)
        {
            dataGridViewGeneralMeeting.DataSource = _generalMeetingService.GetAllByYear(year);
            _generalMeetingDTOs = _generalMeetingService.GetAll();
            GeneralMeetingHelper.ConfigureGeneralMeetingGrid(dataGridViewGeneralMeeting, GeneralMeetingHelper.GeneralMeetingGridType.Basic);
        }

        private void FormMeetingBoard_Load(object sender, EventArgs e)
        {
            resizeControls();
            
            loadConstitutions();

            loadGeneralMeeting(year);

            cmbMonth.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonth, "MonthName", "MonthID");

            cmbYearMeeting.DataSource = _generalMeetingService.GetMeetingYears();
            GeneralHelper.ComboBoxProps(cmbYearMeeting, "YearInText", "YearInValue");

            loadFinedMembers();
            cmbMonthFinedMember.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthFinedMember, "MonthName", "MonthID");
            cmbGenderFinedMember.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderFinedMember, "GenderName", "GenderID");

            cmbFineStatus.DataSource = _paymentStatusService.GetAll();
            GeneralHelper.ComboBoxProps(cmbFineStatus, "PaymentStatusName", "PaymentStatusId");


            loadSpecialContributions();
            cmbMonthContribution.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthContribution, "MonthName", "MonthID");

            if (_currentUserService.AccessLevel != 4)
            {
                btnDelete.Hide();
                btnAddConstitution.Hide();
                btnUpdateConstitution.Hide();
                btnDeleteConstitution.Hide();
                btnDeleteFinedMember.Hide();
                btnDeleteContribution.Hide();
            }
            RefreshCounts();
            ResizeableControls();
        }
        private void RefreshCounts()
        {
            labelTotalMeetings.Text = "Rows: " + dataGridViewGeneralMeeting.RowCount.ToString();
            labelTotalConstitutions.Text = "Rows: " + dataGridViewConstitution.RowCount.ToString();
            labelTotalFineMembers.Text = "Rows: " + dataGridViewFinedMembers.RowCount.ToString();
            labelTotalPaidFines.Text = "Total Paid: " + _finedMemberService.GetTotalPaidFines() + " €";
            labelTotalRowsContributions.Text = "Rows: " + dataGridViewSpecialContributions.RowCount.ToString();
            labelOverallTotalContributions.Text = "Total : " + _specialContributionService.GetAllContributedAmount().ToString() + " €";
        }

        private void ResizeableControls()
        {
            #region
            label18.Tag = "resizable";
            label1.Tag = "resizable";
            label19.Tag = "resizable";
            label21.Tag = "resizable";
            label23.Tag = "resizable";
            label24.Tag = "resizable";
            label12.Tag = "resizable";
            label13.Tag = "resizable";
            label15.Tag = "resizable";
            label16.Tag = "resizable";
            label18.Tag = "resizable";
            label2.Tag = "resizable";
            label20.Tag = "resizable";
            label22.Tag = "resizable";
            label23.Tag = "resizable";
            label24.Tag = "resizable";
            label25.Tag = "resizable";
            label26.Tag = "resizable";
            label3.Tag = "resizable";
            label4.Tag = "resizable";
            labelOverallTotalContributions.Tag = "resizable";
            labelTotalConstitutions.Tag = "resizable";
            labelTotalFineMembers.Tag = "resizable";
            labelTotalMeetings.Tag = "resizable";
            labelTotalPaidFines.Tag = "resizable";
            labelTotalRowsContributions.Tag = "resizable";
            #endregion

            #region
            btnAbsentees.Tag = "resizable";
            btnAdd.Tag = "resizable";
            btnAddConstitution.Tag = "resizable";
            btnAddContribution.Tag = "resizable";
            btnAddFinedMember.Tag = "resizable";
            btnClear.Tag = "resizable";
            btnClearConstitution.Tag = "resizable";
            btnClearContribution.Tag = "resizable";
            btnClearFinedMember.Tag = "resizable";
            btnDelete.Tag = "resizable";
            btnDeleteConstitution.Tag = "resizable";
            btnDeleteContribution.Tag = "resizable";
            btnDeleteFinedMember.Tag = "resizable";
            btnSearch.Tag = "resizable";
            btnSearchContribution.Tag = "resizable";
            btnSearchFinedMember.Tag = "resizable";
            btnUpdate.Tag = "resizable";
            btnUpdateConstitution.Tag = "resizable";
            btnUpdateContribution.Tag = "resizable";
            btnUpdateFinedMember.Tag = "resizable";
            btnView.Tag = "resizable";
            btnViewConstitution.Tag = "resizable";
            btnViewContribution.Tag = "resizable";
            btnViewFinedMember.Tag = "resizable";

            #endregion

            #region
            txtAmountSContributions.Tag = "resizable";
            txtConstitution.Tag = "resizable";
            txtConstitutionSection.Tag = "resizable";
            txtFine.Tag = "resizable";
            txtMonthlyDues.Tag = "resizable";
            txtNameFinedMember.Tag = "resizable";
            txtNoOfAttend.Tag = "resizable";
            txtSection.Tag = "resizable";
            txtSurnameFinedMember.Tag = "resizable";
            txtYear.Tag = "resizable";
            cmbYearContribution.Tag = "resizable";
            txtYearFinedMember.Tag = "resizable";
            #endregion

            #region
            cmbFineStatus.Tag = "resizable";
            cmbGenderFinedMember.Tag = "resizable";
            cmbMonth.Tag = "resizable";
            cmbMonthContribution.Tag = "resizable";
            cmbMonthFinedMember.Tag = "resizable";
            #endregion

            #region
            rbEqualAttend.Tag = "resizable";
            rbEqualContAmount.Tag = "resizable";
            rbEqualMonDues.Tag = "resizable";
            rbLessAttend.Tag = "resizable";
            rbLessContAmount.Tag = "resizable";
            rbLessMonDues.Tag = "resizable";
            rbMoreAttend.Tag = "resizable";
            rbMoreContAmount.Tag = "resizable";
            rbMoreMonDues.Tag = "resizable";
            #endregion

            #region
            dataGridViewGeneralMeeting.Tag = "resizable";
            dataGridViewConstitution.Tag = "resizable";
            dataGridViewFinedMembers.Tag = "resizable";
            dataGridViewSpecialContributions.Tag = "resizable";
            #endregion
        }

        private void ClearFilters()
        {
            txtYear.Clear();
            txtNoOfAttend.Clear();
            txtMonthlyDues.Clear();
            rbEqualMonDues.Checked = false;
            rbLessMonDues.Checked = false;
            rbMoreMonDues.Checked = false;
            rbEqualAttend.Checked = false;
            rbLessAttend.Checked = false;
            rbMoreAttend.Checked = false;
            cmbMonth.SelectedIndex = -1;
            cmbYearMeeting.SelectedIndex = -1;
            loadGeneralMeeting(year);

            txtConstitution.Clear();
            txtSection.Clear();
            txtFine.Clear();
            loadConstitutions();

            txtNameFinedMember.Clear();
            txtSurnameFinedMember.Clear();
            txtYearFinedMember.Clear();
            txtConstitutionSection.Clear();
            cmbFineStatus.SelectedIndex = -1;
            cmbMonthFinedMember.SelectedIndex = -1;
            cmbGenderFinedMember.SelectedIndex = -1;
            loadFinedMembers();

            txtAmountSContributions.Clear();
            cmbMonthContribution.SelectedIndex = -1;
            cmbYearContribution.SelectedIndex = -1;
            rbEqualContAmount.Checked = false;
            rbLessContAmount.Checked = false;
            rbMoreContAmount.Checked = false;
            loadSpecialContributions();

            RefreshCounts();
        }

        // -------------------------------------------------------------------------
        // -------------------- GENERAL MEETING ----------------------
        // -------------------------------------------------------------------------

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new FormGeneralMeeting(_generalMeetingService, _memberService, _generalMeetingAttendanceService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.GeneralMeetingDTO GetGenralMeeting()
        {
            if (dataGridViewGeneralMeeting.CurrentRow == null)
                return null;

            return dataGridViewGeneralMeeting.CurrentRow.DataBoundItem as Applications.DTO.GeneralMeetingDTO;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var selected = GetGenralMeeting();
            if (selected == null)
            {
                MessageBox.Show("Please select a general meeting from the table");
                return;
            }

            var form = new FormViewGeneralAttendance(_generalMeetingAttendanceService, _memberService, _attendanceStatusService);
            form.loadForView(selected);
            form.ShowDialog();

            ClearFilters();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var selected = GetGenralMeeting();
            if (selected == null)
            {
                MessageBox.Show("Please select a comment from the table");
                return;
            }

            var form = new FormGeneralMeeting(_generalMeetingService, _memberService, _generalMeetingAttendanceService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }

        private void txtNoOfAttend_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, txtNoOfAttend);
        }

        private void txtMonthlyDues_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var filtered = _generalMeetingDTOs.AsQueryable();

            if (cmbYearMeeting.SelectedIndex == -1 )
            {
                MessageBox.Show("Please select a year");
                return;
            }
            else
            {
                if (cmbYearMeeting.SelectedIndex != -1)
                {
                    int searchedYear = Convert.ToInt32(cmbYearMeeting.SelectedValue);
                    filtered = filtered.Where(x => x.Year == searchedYear);
                }

                if (cmbMonth.SelectedIndex != -1)
                {
                    int searchedMonth = Convert.ToInt32(cmbMonth.SelectedValue);
                    filtered = filtered.Where(x => x.MonthId == searchedMonth);
                }

                if (txtMonthlyDues.Text.Trim() != "")
                {
                    if (rbEqualMonDues.Checked)
                    {
                        filtered = filtered.Where(x => x.TotalDuesPaid == Convert.ToDecimal(txtMonthlyDues.Text));
                    }
                    else if (rbLessMonDues.Checked)
                    {
                        filtered = filtered.Where(x => x.TotalDuesPaid < Convert.ToDecimal(txtMonthlyDues.Text));
                    }
                    else if (rbMoreMonDues.Checked)
                    {
                        filtered = filtered.Where(x => x.TotalDuesPaid > Convert.ToDecimal(txtMonthlyDues.Text));
                    }
                    else
                    {
                        MessageBox.Show("Please select a criterion from the monthly dues group");
                    }
                }
                if (txtNoOfAttend.Text.Trim() != "")
                {
                    if (rbEqualAttend.Checked)
                    {
                        filtered = filtered.Where(x => x.TotalMembersPresent == Convert.ToInt32(txtNoOfAttend.Text));
                    }
                    else if (rbLessAttend.Checked)
                    {
                        filtered = filtered.Where(x => x.TotalMembersPresent < Convert.ToInt32(txtNoOfAttend.Text));
                    }
                    else if (rbMoreAttend.Checked)
                    {
                        filtered = filtered.Where(x => x.TotalMembersPresent > Convert.ToInt32(txtNoOfAttend.Text));
                    }
                    else
                    {
                        MessageBox.Show("Please select a criterion from the total number of attendance group");
                    }
                }
            }

            dataGridViewGeneralMeeting.DataSource = filtered.ToList();
            RefreshCounts();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }
        

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;
            //detail = GeneralHelper.MapFromGrid<GeneralAttendanceDetailDTO>(dataGridViewGeneralMeeting, e.RowIndex);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetGenralMeeting();
            if (selected == null)
            {
                MessageBox.Show("Please select a meeting.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _generalMeetingService.Delete(selected.GeneralMeetingId);
                ClearFilters();
            }
        }

        // -------------------------------------------------------------------------
        // -------------------- ABSENTEES ----------------------
        // -------------------------------------------------------------------------
        private void btnAbsentees_Click(object sender, EventArgs e)
        {
            var form = new FormNotifications(_serviceProvider, _memberService);
            form.ShowDialog();
            ClearFilters();
        }

        // -------------------------------------------------------------------------
        // -------------------- FINED MEMBERS ----------------------
        // -------------------------------------------------------------------------

        private void btnAddFinedMember_Click(object sender, EventArgs e)
        {
            var form = new FormFinedMember(_finedMemberService, _memberService, _constitutionService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.FinedMemberDTO GetSelectedFinedMember()
        {
            if (dataGridViewFinedMembers.CurrentRow == null)
                return null;

            return dataGridViewFinedMembers.CurrentRow.DataBoundItem as Applications.DTO.FinedMemberDTO;
        }

        private void btnUpdateFinedMember_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFinedMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a fined member from the table");
                return;
            }

            var form = new FormFinedMember(_finedMemberService, _memberService, _constitutionService);
            form.loadForEdit(selected, true);
            form.ShowDialog();
            ClearFilters();
        }

        private void btnViewFinedMember_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFinedMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a fined member from the table");
                return;
            }

            var form = new FormViewFinedMember(selected);
            form.ShowDialog();

            ClearFilters();
        }
        private void btnDeleteFinedMember_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedFinedMember();
            if (selected == null)
            {
                MessageBox.Show("Please select a fined member.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _finedMemberService.Delete(selected.FinedMemberId);
                ClearFilters();
            }
        }

        private void txtYearFinedMember_TextChanged(object sender, EventArgs e)
        {
            string search = txtYearFinedMember.Text.Trim().ToLower();
            var filtered = _finedMemberDTO.Where(x => x.FineDate.Year.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewFinedMembers.DataSource = filtered;
        }

        private void txtYearFinedMember_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }

        private void txtNameFinedMember_TextChanged(object sender, EventArgs e)
        {
            string search = txtNameFinedMember.Text.Trim().ToLower();
            var filtered = _finedMemberDTO.Where(x => x.FirstName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewFinedMembers.DataSource = filtered;
        }

        private void txtSurnameFinedMember_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameFinedMember.Text.Trim().ToLower();
            var filtered = _finedMemberDTO.Where(x => x.LastName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewFinedMembers.DataSource = filtered;
        }

        private void txtConstitutionSection_TextChanged(object sender, EventArgs e)
        {
            string search = txtConstitutionSection.Text.Trim().ToLower();
            var filtered = _finedMemberDTO.Where(x => x.Section.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewFinedMembers.DataSource = filtered;
        }

        private void btnSearchFinedMember_Click(object sender, EventArgs e)
        {
            var filtered = _finedMemberDTO.AsQueryable();

            if (cmbGenderFinedMember.SelectedIndex == -1 && cmbMonthFinedMember.SelectedIndex == -1 && cmbFineStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender or month or a fine status");
                return;
            }

            if (cmbGenderFinedMember.SelectedIndex != -1)
            {
                int searchedGender = Convert.ToInt32(cmbGenderFinedMember.SelectedValue);
                filtered = filtered.Where(x => x.GenderId == searchedGender);
            }

            if (cmbMonthFinedMember.SelectedIndex != -1)
            {
                int searchedMonth = Convert.ToInt32(cmbMonthFinedMember.SelectedValue);
                filtered = filtered.Where(x => x.FineDate.Month == searchedMonth);
            }

            if (cmbFineStatus.SelectedIndex != -1)
            {
                string searchedStatus = cmbFineStatus.Text;
                filtered = filtered.Where(x => x.Status != null && x.Status.IndexOf(searchedStatus, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            dataGridViewFinedMembers.DataSource = filtered.ToList();
        }

        private void btnClearFinedMember_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }


        // -------------------------------------------------------------------------
        // -------------------- CONSTITUTION ----------------------
        // -------------------------------------------------------------------------

        private void btnAddConstitution_Click(object sender, EventArgs e)
        {
            var form = new FormConstitution(_constitutionService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.ConstitutionDTO GetSelectedConstitution()
        {
            if (dataGridViewConstitution.CurrentRow == null)
                return null;

            return dataGridViewConstitution.CurrentRow.DataBoundItem as Applications.DTO.ConstitutionDTO;
        }

        private void btnViewConstitution_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedConstitution();
            if (selected == null)
            {
                MessageBox.Show("Please select a constitution from the table");
                return;
            }

            var form = new FormViewConstitution(selected, _constitutionService);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnDeleteConstitution_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedConstitution();
            if (selected == null)
            {
                MessageBox.Show("Please select a constitution.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _constitutionService.Delete(selected.ConstitutionId);
                ClearFilters();
            }
        }

        private void btnUpdateConstitution_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedConstitution();
            if (selected == null)
            {
                MessageBox.Show("Please select a constitution from the table");
                return;
            }

            var form = new FormConstitution(_constitutionService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtConstitution_TextChanged(object sender, EventArgs e)
        {
            string search = txtConstitution.Text.Trim().ToLower();
            var filtered = _constitutionDTO.Where(x => x.ShortDescription.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewConstitution.DataSource = filtered;
        }

        private void txtSection_TextChanged(object sender, EventArgs e)
        {
            string search = txtSection.Text.Trim().ToLower();
            var filtered = _constitutionDTO.Where(x => x.Section.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewConstitution.DataSource = filtered;
        }

        private void btnClearConstitution_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void txtFine_TextChanged(object sender, EventArgs e)
        {
            string search = txtFine.Text.Trim().ToLower();
            var filtered = _constitutionDTO.Where(x => x.FineWithCurrency.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewConstitution.DataSource = filtered;
        }


        // -------------------------------------------------------------------------
        // -------------------- SPECIAL CONTRIBUTIONS ----------------------
        // -------------------------------------------------------------------------

        private void btnAddContribution_Click(object sender, EventArgs e)
        {
            var form = new FormSpecialContribution(_specialContributionService, _memberService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.SpecialContributionDTO GetSelectedSpecialContribution()
        {
            if (dataGridViewSpecialContributions.CurrentRow == null)
                return null;

            return dataGridViewSpecialContributions.CurrentRow.DataBoundItem as Applications.DTO.SpecialContributionDTO;
        }

        private void btnUpdateContribution_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedSpecialContribution();
            if (selected == null)
            {
                MessageBox.Show("Please select a special contribution from the table");
                return;
            }

            var form = new FormSpecialContribution(_specialContributionService, _memberService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnClearContribution_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void txtAmountSContributions_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, txtAmountSContributions);
        }

        private void txtAmountSContributions_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtNoOfContributors_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnViewContribution_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedSpecialContribution();
            if (selected == null)
            {
                MessageBox.Show("Please select a special contribution from the table");
                return;
            }

            var form = new FormViewSpecialContribution(selected, _memberService, _specialContributorService, _specialContributorsDTO);
            form.ShowDialog();

            ClearFilters();      
        }

        private void btnDeleteContribution_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedSpecialContribution();
            if (selected == null)
            {
                MessageBox.Show("Please select a special contribution.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _specialContributionService.Delete(selected.SpecialContributionId);
                ClearFilters();
            }
        }

        private void btnSearchContribution_Click(object sender, EventArgs e)
        {
            var filtered = _specialContributionDTO.AsQueryable();

            if (cmbMonthContribution.SelectedIndex == -1 && cmbYearContribution.SelectedIndex == -1 && txtAmountSContributions.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please select either a month or year or an amount");
                return;
            }

            if (cmbYearContribution.SelectedIndex != -1)
            {
                int searchedYear = Convert.ToInt32(cmbYearContribution.SelectedValue);
                filtered = filtered.Where(x => x.ContributionStartDate.Year == searchedYear || x.ContributionEndDate.Year == searchedYear);
            }

            if (cmbMonthContribution.SelectedIndex != -1)
            {
                int searchedMonth = Convert.ToInt32(cmbMonthContribution.SelectedValue);
                filtered = filtered.Where(x => x.ContributionStartDate.Month == searchedMonth || x.ContributionEndDate.Month == searchedMonth);
            }

            if (txtAmountSContributions.Text.Trim().Length != 0)
            {
                decimal amount = Convert.ToDecimal(txtAmountSContributions.Text.Trim());
                if (rbEqualContAmount.Checked)
                {
                    filtered = filtered.Where(x => x.TotalContributedAmount == amount);
                }
                else if (rbLessContAmount.Checked)
                {
                    filtered = filtered.Where(x => x.TotalContributedAmount < amount);
                }
                else if (rbMoreContAmount.Checked)
                {
                    filtered = filtered.Where(x => x.TotalContributedAmount > amount);
                }
                else
                {
                    MessageBox.Show("Unknown filter");
                }
            }

            dataGridViewFinedMembers.DataSource = filtered.ToList();
        }
    }
}
