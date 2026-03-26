using APC.Applications.Interfaces;
using APC.BLL;
using APC.DAL.DTO;
using APC.Helper;
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
        private readonly ICommentService _commentService;
        private readonly IGenderService _genderService;
        private readonly IMonthService _monthService;
        private readonly IConstitutionService _constitutionService;
        private readonly IFinedMemberService _finedMemberService;
        private readonly ISpecialContributionService _specialContributionService;
        private readonly ISpecialContributorService _specialContributorService;
        private readonly ICurrentUserService _currentUserService;

        private List<Applications.DTO.CommentDTO> _commentDTO;
        private List<Applications.DTO.ConstitutionDTO> _constitutionDTO;
        private List<Applications.DTO.FinedMemberDTO> _finedMemberDTO;
        private List<Applications.DTO.SpecialContributionDTO> _specialContributionDTO;
        private List<Applications.DTO.SpecialContributorDTO> _specialContributorsDTO;

        public FormMeetingBoard(ICommentService commentService, IGenderService genderService, IMemberService memberService, 
            IMonthService monthService, IConstitutionService constitutionService, IFinedMemberService finedMemberService, 
            ISpecialContributionService specialContributionService, ISpecialContributorService specialContributorService, 
            ICurrentUserService currentUserService)
        {
            InitializeComponent();
            _commentService = commentService;
            _genderService = genderService;
            _memberService = memberService;
            _monthService = monthService;
            _constitutionService = constitutionService;
            _finedMemberService = finedMemberService;
            _specialContributionService = specialContributionService;
            _specialContributorService = specialContributorService;
            _currentUserService = currentUserService;
        }

        GeneralAttendanceBLL bll = new GeneralAttendanceBLL();
        GeneralAttendanceDTO dto = new GeneralAttendanceDTO();
        GeneralAttendanceDetailDTO detail = new GeneralAttendanceDetailDTO();

        private void resizeControls()
        {
            GeneralHelper.ApplyBoldFont(14, label1, label2, label3, label4, rbEqualAttend, rbEqualMonDues, rbLessAttend, rbLessMonDues,
                rbMoreAttend, rbMoreMonDues, btnUpdate, btnView, btnAdd, btnAbsentees, btnDelete, btnSearch, btnClear, label5, label6,
                label7, label8, label9, label10, btnAddComments, btnDeleteComments, btnUpdateComments, btnViewComments, btnSearchComments,
                btnClearComments, label18, label19, label21, btnAddConstitution, btnDeleteConstitution, btnUpdateConstitution,
                btnViewConstitution, btnClearConstitution, label12, label20, label22, label23, label24, label25, label26, btnSearchFinedMember,
                btnClearFinedMember);

            GeneralHelper.ApplyRegularFont(11, labelTotalMeetings, labelTotalPaidFines, labelTotalComments, labelTotalConstitutions);

            GeneralHelper.ApplyRegularFont(14, labelTotalFineMembers);

            GeneralHelper.ApplyRegularFont(16, txtMonthlyDues, txtNoOfAttend, txtYear, cmbMonth, cmbYearComments, txtNameComments,
                txtComment, txtSurnameComments, cmbGenderComments, cmbMonthComments, txtNameFinedMember, txtSurnameFinedMember,
                txtConstitutionSection, cmbGenderFinedMember, cmbMonthFinedMember, cmbFineStatus, cmbYearMeeting);
        }

        private void loadComments()
        {
            dataGridViewComments.DataSource = _commentService.GetAll();
            CommentHelper.ConfigureCommentGrid(dataGridViewComments, CommentHelper.CommentGridType.Basic);
        }

        private void loadConstitutions()
        {
            dataGridViewConstitution.DataSource = _constitutionService.GetAll();
            ConstitutionHelper.ConfigureConstitutionGrid(dataGridViewConstitution, ConstitutionHelper.ConstitutionGridType.Normal);
        }
        
        private void loadFinedMembers()
        {
            dataGridViewFinedMembers.DataSource = _finedMemberService.GetAll();
            FinedMemberHelper.ConfigureFinedMemberGrid(dataGridViewFinedMembers, FinedMemberHelper.FinedMemberGridType.Basic);
        }
        
        private void loadSpecialContributions()
        {
            dataGridViewSpecialContributions.DataSource = _specialContributionService.GetAll();
            SpecialContributionHelper.ConfigureSpecialContributionGrid(dataGridViewSpecialContributions, SpecialContributionHelper.SpecialContributionGridType.Basic);
        }

        private void FormMeetingBoard_Load(object sender, EventArgs e)
        {
            resizeControls();
            
            loadConstitutions();

            dto = bll.Select(DateTime.Now.Year);
            cmbMonth.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonth, "MonthName", "MonthID");

            cmbYearMeeting.DataSource = dto.Years;
            cmbYearMeeting.SelectedIndex = -1;

            loadComments();
            cmbGenderComments.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderComments, "GenderName", "GenderId");
            cmbMonthComments.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthComments, "MonthName", "MonthId");

            loadFinedMembers();
            cmbMonthFinedMember.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthFinedMember, "MonthName", "MonthID");
            cmbGenderFinedMember.DataSource = _genderService.GetAll();
            GeneralHelper.ComboBoxProps(cmbGenderFinedMember, "GenderName", "GenderID");

            cmbFineStatus.Items.Add("Completed");
            cmbFineStatus.Items.Add("NOT Completed");
            cmbFineStatus.Items.Add("NOT Paid");


            loadSpecialContributions();
            cmbMonthContribution.DataSource = _monthService.GetAll();
            GeneralHelper.ComboBoxProps(cmbMonthContribution, "MonthName", "MonthID");

            #region
            LoadDataGridView.loadGeneralAttendances(dataGridView1, dto);
            
            #endregion

            if (_currentUserService.AccessLevel != 4)
            {
                btnDelete.Hide();
                btnDeleteComments.Hide();
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
            labelTotalMeetings.Text = "Rows: " + dataGridView1.RowCount.ToString();
            labelTotalComments.Text = "Rows: " + dataGridViewComments.RowCount.ToString();
            labelTotalConstitutions.Text = "Rows: " + dataGridViewConstitution.RowCount.ToString();
            labelTotalFineMembers.Text = "Rows: " + dataGridViewFinedMembers.RowCount.ToString();
            labelTotalPaidFines.Text = "Total Paid: " + bll.TotalPaidFines();
            labelTotalRowsContributions.Text = "Rows: " + dataGridViewSpecialContributions.RowCount.ToString();
            labelOverallTotalContributions.Text = "Total : ";
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
            label10.Tag = "resizable";
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
            label5.Tag = "resizable";
            label6.Tag = "resizable";
            label7.Tag = "resizable";
            label8.Tag = "resizable";
            label9.Tag = "resizable";
            labelOverallTotalContributions.Tag = "resizable";
            labelTotalComments.Tag = "resizable";
            labelTotalConstitutions.Tag = "resizable";
            labelTotalFineMembers.Tag = "resizable";
            labelTotalMeetings.Tag = "resizable";
            labelTotalPaidFines.Tag = "resizable";
            labelTotalRowsContributions.Tag = "resizable";
            #endregion

            #region
            btnAbsentees.Tag = "resizable";
            btnAdd.Tag = "resizable";
            btnAddComments.Tag = "resizable";
            btnAddConstitution.Tag = "resizable";
            btnAddContribution.Tag = "resizable";
            btnAddFinedMember.Tag = "resizable";
            btnClear.Tag = "resizable";
            btnClearComments.Tag = "resizable";
            btnClearConstitution.Tag = "resizable";
            btnClearContribution.Tag = "resizable";
            btnClearFinedMember.Tag = "resizable";
            btnDelete.Tag = "resizable";
            btnDeleteComments.Tag = "resizable";
            btnDeleteConstitution.Tag = "resizable";
            btnDeleteContribution.Tag = "resizable";
            btnDeleteFinedMember.Tag = "resizable";
            btnSearch.Tag = "resizable";
            btnSearchComments.Tag = "resizable";
            btnSearchContribution.Tag = "resizable";
            btnSearchFinedMember.Tag = "resizable";
            btnUpdate.Tag = "resizable";
            btnUpdateComments.Tag = "resizable";
            btnUpdateConstitution.Tag = "resizable";
            btnUpdateContribution.Tag = "resizable";
            btnUpdateFinedMember.Tag = "resizable";
            btnView.Tag = "resizable";
            btnViewComments.Tag = "resizable";
            btnViewConstitution.Tag = "resizable";
            btnViewContribution.Tag = "resizable";
            btnViewFinedMember.Tag = "resizable";

            #endregion

            #region
            txtAmountSContributions.Tag = "resizable";
            txtComment.Tag = "resizable";
            txtConstitution.Tag = "resizable";
            txtConstitutionSection.Tag = "resizable";
            txtFine.Tag = "resizable";
            txtMonthlyDues.Tag = "resizable";
            txtNameComments.Tag = "resizable";
            txtNameFinedMember.Tag = "resizable";
            txtNoOfAttend.Tag = "resizable";
            txtSection.Tag = "resizable";
            txtSurnameComments.Tag = "resizable";
            txtSurnameFinedMember.Tag = "resizable";
            txtYear.Tag = "resizable";
            cmbYearContribution.Tag = "resizable";
            txtYearFinedMember.Tag = "resizable";
            #endregion

            #region
            cmbFineStatus.Tag = "resizable";
            cmbGenderComments.Tag = "resizable";
            cmbGenderFinedMember.Tag = "resizable";
            cmbMonth.Tag = "resizable";
            cmbMonthComments.Tag = "resizable";
            cmbMonthContribution.Tag = "resizable";
            cmbMonthFinedMember.Tag = "resizable";
            cmbYearComments.Tag = "resizable";
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
            dataGridView1.Tag = "resizable";
            dataGridViewComments.Tag = "resizable";
            dataGridViewConstitution.Tag = "resizable";
            dataGridViewFinedMembers.Tag = "resizable";
            dataGridViewSpecialContributions.Tag = "resizable";
            #endregion
        }

        // -------------------------------------------------------------------------
        // -------------------- GENERAL ATTENDANCE ----------------------
        // -------------------------------------------------------------------------

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormGeneralAttendance open = new FormGeneralAttendance();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (detail.GeneralAttendanceID == 0)
            {
                MessageBox.Show("Please choose an attendance from the table");
            }
            else
            {
                FormViewGeneralAttendance open = new FormViewGeneralAttendance();
                open.detail = detail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.GeneralAttendanceID == 0)
            {
                MessageBox.Show("Please choose an attendance from the table");
            }
            else
            {
                FormGeneralAttendance open = new FormGeneralAttendance();
                open.isUpdate = true;
                open.detail = detail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
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
            if (cmbYearMeeting.SelectedIndex == -1)
            {
                MessageBox.Show("Please select year");
            }
            else
            {
                bll = new GeneralAttendanceBLL();
                dto = bll.Select(Convert.ToInt32(cmbYearMeeting.SelectedValue));
                List<GeneralAttendanceDetailDTO> list = dto.GeneralAttendance;

                if (txtMonthlyDues.Text.Trim() != "")
                {
                    if (rbEqualMonDues.Checked)
                    {
                        list = list.Where(x => x.TotalDuesPaid == Convert.ToInt32(txtMonthlyDues.Text)).ToList();
                    }
                    else if (rbLessMonDues.Checked)
                    {
                        list = list.Where(x => x.TotalDuesPaid < Convert.ToInt32(txtMonthlyDues.Text)).ToList();
                    }
                    else if (rbMoreMonDues.Checked)
                    {
                        list = list.Where(x => x.TotalDuesPaid > Convert.ToInt32(txtMonthlyDues.Text)).ToList();
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
                        list = list.Where(x => x.TotalMembersPresent == Convert.ToInt32(txtNoOfAttend.Text)).ToList();
                    }
                    else if (rbLessAttend.Checked)
                    {
                        list = list.Where(x => x.TotalMembersPresent < Convert.ToInt32(txtNoOfAttend.Text)).ToList();
                    }
                    else if (rbMoreAttend.Checked)
                    {
                        list = list.Where(x => x.TotalMembersPresent > Convert.ToInt32(txtNoOfAttend.Text)).ToList();
                    }
                    else
                    {
                        MessageBox.Show("Please select a criterion from the total number of attendance group");
                    }
                }
                if (cmbMonth.SelectedIndex != -1)
                {
                    list = list.Where(x => x.MonthID == Convert.ToInt32((cmbMonth.SelectedValue)) && Convert.ToInt32(x.Year) == Convert.ToInt32((cmbYearMeeting.SelectedValue))).ToList();
                }
                dataGridView1.DataSource = list;

                RefreshCounts();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
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
            bll = new GeneralAttendanceBLL();
            dto = bll.Select(DateTime.Now.Year);
            dataGridView1.DataSource = dto.GeneralAttendance;

            txtNameComments.Clear();
            txtComment.Clear();
            txtSurnameComments.Clear();
            cmbGenderComments.SelectedIndex = -1;
            cmbMonthComments.SelectedIndex = -1;
            loadComments();

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

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            List<GeneralAttendanceDetailDTO> list = dto.GeneralAttendance;
            list = list.Where(x => x.Year.Contains(txtYear.Text.Trim())).ToList();
            dataGridView1.DataSource = list;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            detail = GeneralHelper.MapFromGrid<GeneralAttendanceDetailDTO>(dataGridView1, e.RowIndex);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.GeneralAttendanceID == 0)
            {
                MessageBox.Show("Please choose an attendance from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                    {
                        MessageBox.Show("Attendance was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        // -------------------------------------------------------------------------
        // -------------------- ABSENTEES ----------------------
        // -------------------------------------------------------------------------
        private void btnAbsentees_Click(object sender, EventArgs e)
        {
            FormNotifications open = new FormNotifications();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
        }

        // -------------------------------------------------------------------------
        // -------------------- COMMENTS ----------------------
        // -------------------------------------------------------------------------
        private void btnAddComments_Click(object sender, EventArgs e)
        {
            var form = new FormComments(_commentService, _memberService);
            form.ShowDialog();

            ClearFilters();
        }

        private Applications.DTO.CommentDTO GetSelectedComment()
        {
            if (dataGridViewComments.CurrentRow == null)
                return null;

            return dataGridViewComments.CurrentRow.DataBoundItem as Applications.DTO.CommentDTO;
        }

        private void btnUpdateComments_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedComment();
            if (selected == null)
            {
                MessageBox.Show("Please select a comment from the table");
                return;
            }

            var form = new FormComments(_commentService, _memberService);
            form.loadForEdit(selected, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void btnViewComments_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedComment();
            if (selected == null)
            {
                MessageBox.Show("Please select a country from the table");
                return;
            }

            var form = new FormViewComment(selected);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtSurnameComments_TextChanged(object sender, EventArgs e)
        {
            string search = txtSurnameComments.Text.Trim().ToLower();
            var filtered = _commentDTO.Where(x => x.LastName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewComments.DataSource = filtered;
        }

        private void txtNameComments_TextChanged(object sender, EventArgs e)
        {
            string search = txtNameComments.Text.Trim().ToLower();
            var filtered = _commentDTO.Where(x => x.FirstName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewComments.DataSource = filtered;
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {
            string search = txtComment.Text.Trim().ToLower();
            var filtered = _commentDTO.Where(x => x.Content.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewComments.DataSource = filtered;
        }

        private void btnSearchComments_Click(object sender, EventArgs e)
        {
            var filtered = _commentDTO.AsQueryable();

            if (cmbGenderComments.SelectedIndex != -1)
            {
                int searchedGender = Convert.ToInt32(cmbGenderComments.SelectedValue);
                filtered = filtered.Where(x => x.GenderId == searchedGender);
            }

            if (cmbYearComments.SelectedIndex == -1 && cmbMonthComments.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a year or month");
                return;
            }

            if (cmbYearComments.SelectedIndex != -1)
            {
                int searchedYear = Convert.ToInt32(cmbYearComments.SelectedValue);
                filtered = filtered.Where(x => x.Date.Year == searchedYear);
            }

            if (cmbMonthComments.SelectedIndex != -1)
            {
                int searchedMonth = Convert.ToInt32(cmbMonthComments.SelectedValue);
                filtered = filtered.Where(x => x.Date.Month == searchedMonth);
            }

            dataGridViewComments.DataSource = filtered.ToList();
        }

        private void btnClearComments_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnDeleteComments_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedComment();
            if (selected == null)
            {
                MessageBox.Show("Please select a constitution.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _commentService.Delete(selected.CommentId);
                ClearFilters();
            }
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
                string searchedStatus = cmbFineStatus.SelectedValue?.ToString();
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
