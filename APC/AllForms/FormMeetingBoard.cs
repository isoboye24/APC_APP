using APC.AllForms;
using APC.BLL;
using APC.DAL.DTO;
using APC.HelperServices;
using APC.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace APC.AllForms
{
    public partial class FormMeetingBoard : Form
    {
        public FormMeetingBoard()
        {
            InitializeComponent();
        }
        GeneralAttendanceBLL bll = new GeneralAttendanceBLL();
        GeneralAttendanceDTO dto = new GeneralAttendanceDTO();
        GeneralAttendanceDetailDTO detail = new GeneralAttendanceDetailDTO();

        CommentDTO commentDTO = new CommentDTO();
        CommentBLL commentBLL = new CommentBLL();
        CommentDetailDTO commentDetail = new CommentDetailDTO();

        SpecialContributionDetailDTO specialContributionDetail = new SpecialContributionDetailDTO();
        SpecialContributionsBLL specialContributionsBLL = new SpecialContributionsBLL();
        SpecialContributionDTO specialContributionDTO = new SpecialContributionDTO();

        FinedMemberBLL finedMemberBLL = new FinedMemberBLL();
        FinedMemberDTO finedMemberDTO = new FinedMemberDTO();
        FinedMemberDetailDTO finedMemberDetail = new FinedMemberDetailDTO();

        ConstitutionBLL constitutionBLL = new ConstitutionBLL();
        ConstitutionDTO constitutionDTO = new ConstitutionDTO();
        ConstitutionDetailDTO constitutionDetail = new ConstitutionDetailDTO();

        private void FormMeetingBoard_Load(object sender, EventArgs e)
        {
            #region
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalMeetings.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelTotalPaidFines.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            rbEqualAttend.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            rbEqualMonDues.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            rbLessAttend.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            rbLessMonDues.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            rbMoreAttend.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            rbMoreMonDues.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtMonthlyDues.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtNoOfAttend.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtYear.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbMonth.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnUpdate.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnView.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnAdd.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnAbsentees.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDelete.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearch.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClear.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalComments.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtYearComments.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtNameComments.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtComment.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtSurnameComments.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbGenderComments.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbMonthComments.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnAddComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearchComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearComments.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label18.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label19.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label21.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalConstitutions.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            btnAddConstitution.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteConstitution.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateConstitution.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewConstitution.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearConstitution.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label12.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label20.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label22.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label23.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label24.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label25.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label26.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalFineMembers.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnSearchFinedMember.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearFinedMember.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            txtNameFinedMember.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtSurnameFinedMember.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtConstitutionSection.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbGenderFinedMember.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbMonthFinedMember.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbFineStatus.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbYearMeeting.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            #endregion

            dto = bll.Select(DateTime.Now.Year);
            cmbMonth.DataSource = dto.Months;
            GeneralHelper.ComboBoxProps(cmbMonth, "MonthName", "MonthID");
            cmbYearMeeting.DataSource = dto.Years;
            cmbYearMeeting.SelectedIndex = -1;

            commentDTO = commentBLL.Select();
            cmbGenderComments.DataSource = commentDTO.Genders;
            GeneralHelper.ComboBoxProps(cmbGenderComments, "GenderName", "GenderID");
            cmbMonthComments.DataSource = commentDTO.Months;
            GeneralHelper.ComboBoxProps(cmbMonthComments, "MonthName", "MonthID");

            finedMemberDTO = finedMemberBLL.Select();
            cmbMonthFinedMember.DataSource = finedMemberDTO.Months;
            GeneralHelper.ComboBoxProps(cmbMonthFinedMember, "MonthName", "MonthID");
            cmbGenderFinedMember.DataSource = finedMemberDTO.Genders;
            GeneralHelper.ComboBoxProps(cmbGenderFinedMember, "GenderName", "GenderID");
            cmbFineStatus.Items.Add("Completed");
            cmbFineStatus.Items.Add("NOT Completed");
            cmbFineStatus.Items.Add("NOT Paid");

            specialContributionDTO = specialContributionsBLL.Select();
            cmbMonthContribution.DataSource = specialContributionDTO.Months;
            GeneralHelper.ComboBoxProps(cmbMonthContribution, "MonthName", "MonthID");

            constitutionDTO = constitutionBLL.Select();

            #region
            LoadDataGridView.loadGeneralAttendances(dataGridView1, dto);
            LoadDataGridView.loadComments(dataGridViewComments, commentDTO);
            LoadDataGridView.loadConstitution(dataGridViewConstitution, constitutionDTO);
            LoadDataGridView.loadFinedMembers(dataGridViewFinedMembers, finedMemberDTO);
            LoadDataGridView.loadSpecialContributions(dataGridViewSpecialContributions, specialContributionDTO);
            #endregion

            if (LoginInfo.AccessLevel != 4)
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
            labelOverallTotalContributions.Text = "Total : " + specialContributionsBLL.OverallTotalContributions();
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
            label14.Tag = "resizable";
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
            txtNoOfContributors.Tag = "resizable";
            txtSection.Tag = "resizable";
            txtSurnameComments.Tag = "resizable";
            txtSurnameFinedMember.Tag = "resizable";
            txtYear.Tag = "resizable";
            txtYearComments.Tag = "resizable";
            txtYearContribution.Tag = "resizable";
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
            #endregion

            #region
            rbEqualAttend.Tag = "resizable";
            rbEqualContAmount.Tag = "resizable";
            rbEqualContributors.Tag = "resizable";
            rbEqualMonDues.Tag = "resizable";
            rbLessAttend.Tag = "resizable";
            rbLessContAmount.Tag = "resizable";
            rbLessContributors.Tag = "resizable";
            rbLessMonDues.Tag = "resizable";
            rbMoreAttend.Tag = "resizable";
            rbMoreContAmount.Tag = "resizable";
            rbMoreContributors.Tag = "resizable";
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
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
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
            txtYearComments.Clear();
            cmbGenderComments.SelectedIndex = -1;
            cmbMonthComments.SelectedIndex = -1;
            commentBLL = new CommentBLL();
            commentDTO = commentBLL.Select();
            dataGridViewComments.DataSource = commentDTO.Comments;

            txtConstitution.Clear();
            txtSection.Clear();
            txtFine.Clear();
            constitutionBLL = new ConstitutionBLL();
            constitutionDTO = constitutionBLL.Select();
            dataGridViewConstitution.DataSource = constitutionDTO.Constitutions;

            txtNameFinedMember.Clear();
            txtSurnameFinedMember.Clear();
            txtYearFinedMember.Clear();
            txtConstitutionSection.Clear();
            cmbFineStatus.SelectedIndex = -1;
            cmbMonthFinedMember.SelectedIndex = -1;
            cmbGenderFinedMember.SelectedIndex = -1;
            finedMemberBLL = new FinedMemberBLL();
            finedMemberDTO = finedMemberBLL.Select();
            dataGridViewFinedMembers.DataSource = finedMemberDTO.FineMembers;

            txtAmountSContributions.Clear();
            txtNoOfContributors.Clear();
            cmbMonthContribution.SelectedIndex = -1;
            specialContributionsBLL = new SpecialContributionsBLL();
            specialContributionDTO = specialContributionsBLL.Select();
            dataGridViewSpecialContributions.DataSource = specialContributionDTO.SpecialContributions;

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
            FormComments open = new FormComments();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateComments_Click(object sender, EventArgs e)
        {
            if (commentDetail.CommentID == 0)
            {
                MessageBox.Show("Please choose a comment from the table.");
            }
            else
            {
                FormComments open = new FormComments();
                open.detail = commentDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void dataGridViewComments_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            commentDetail = GeneralHelper.MapFromGrid<CommentDetailDTO>(dataGridViewComments, e.RowIndex);
        }

        private void btnViewComments_Click(object sender, EventArgs e)
        {
            if (commentDetail.CommentID == 0)
            {
                MessageBox.Show("Please choose a comment from the table.");
            }
            else
            {
                FormViewComment open = new FormViewComment();
                open.detail = commentDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtSurnameComments_TextChanged(object sender, EventArgs e)
        {
            List<CommentDetailDTO> list = commentDTO.Comments;
            list = list.Where(x => x.Surname.Contains(txtSurnameComments.Text.Trim())).ToList();
            dataGridViewComments.DataSource = list;
        }

        private void txtNameComments_TextChanged(object sender, EventArgs e)
        {
            List<CommentDetailDTO> list = commentDTO.Comments;
            list = list.Where(x => x.Name.Contains(txtNameComments.Text.Trim())).ToList();
            dataGridViewComments.DataSource = list;
        }

        private void txtYearComments_TextChanged(object sender, EventArgs e)
        {
            List<CommentDetailDTO> list = commentDTO.Comments;
            list = list.Where(x => x.Year.Contains(txtYearComments.Text.Trim())).ToList();
            dataGridViewComments.DataSource = list;            
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {
            List<CommentDetailDTO> list = commentDTO.Comments;
            list = list.Where(x => x.CommentName.Contains(txtComment.Text.Trim())).ToList();
            dataGridViewComments.DataSource = list;
        }

        private void btnSearchComments_Click(object sender, EventArgs e)
        {
            List<CommentDetailDTO> list = commentDTO.Comments;
            if (cmbGenderComments.SelectedIndex != -1)
            {
                list = list.Where(x => x.GenderID == Convert.ToInt32(cmbGenderComments.SelectedValue)).ToList();
            }
            if (cmbMonthComments.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonthComments.SelectedValue)).ToList();
            }
            dataGridViewComments.DataSource = list;
        }

        private void btnClearComments_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        // -------------------------------------------------------------------------
        // -------------------- FINED MEMBERS ----------------------
        // -------------------------------------------------------------------------

        private void dataGridViewFinedMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            finedMemberDetail = GeneralHelper.MapFromGrid<FinedMemberDetailDTO>(dataGridViewFinedMembers, e.RowIndex);
        }
        private void btnAddFinedMember_Click(object sender, EventArgs e)
        {
            FormFinedMember open = new FormFinedMember();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateFinedMember_Click(object sender, EventArgs e)
        {
            if (finedMemberDetail.FinedMemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table.");
            }
            else
            {
                FormFinedMember open = new FormFinedMember();
                open.detail = finedMemberDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnViewFinedMember_Click(object sender, EventArgs e)
        {
            if (finedMemberDetail.FinedMemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table.");
            }
            else
            {
                FormViewFinedMember open = new FormViewFinedMember();
                open.detail = finedMemberDetail;
                open.constID = finedMemberDetail.ConstitutionID;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }
        private void btnDeleteFinedMember_Click(object sender, EventArgs e)
        {
            if (finedMemberDetail.FinedMemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table.");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (finedMemberBLL.Delete(finedMemberDetail))
                    {
                        MessageBox.Show("Fined member was deleted!");
                        ClearFilters();
                    }
                }
            }
        }

        private void txtYearFinedMember_TextChanged(object sender, EventArgs e)
        {
            List<FinedMemberDetailDTO> list = finedMemberDTO.FineMembers;
            list = list.Where(x => x.Year.ToString().Contains(txtYearFinedMember.Text.Trim())).ToList();
            dataGridViewFinedMembers.DataSource = list;
        }

        private void txtYearFinedMember_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = GeneralHelper.isNumber(e, (TextBox)sender);
        }

        private void txtNameFinedMember_TextChanged(object sender, EventArgs e)
        {
            List<FinedMemberDetailDTO> list = finedMemberDTO.FineMembers;
            list = list.Where(x => x.Name.Contains(txtNameFinedMember.Text.Trim())).ToList();
            dataGridViewFinedMembers.DataSource = list;
        }

        private void txtSurnameFinedMember_TextChanged(object sender, EventArgs e)
        {
            List<FinedMemberDetailDTO> list = finedMemberDTO.FineMembers;
            list = list.Where(x => x.Surname.Contains(txtSurnameFinedMember.Text.Trim())).ToList();
            dataGridViewFinedMembers.DataSource = list;
        }

        private void btnSearchFinedMember_Click(object sender, EventArgs e)
        {
            List<FinedMemberDetailDTO> list = finedMemberDTO.FineMembers;
            if (cmbGenderFinedMember.SelectedIndex != -1)
            {
                list = list.Where(x => x.GenderID == Convert.ToInt32(cmbGenderFinedMember.SelectedValue)).ToList();
            }
            if (cmbMonthFinedMember.SelectedIndex != -1)
            {
                list = list.Where(x => x.MonthID == Convert.ToInt32(cmbMonthFinedMember.SelectedValue)).ToList();
            }
            if (cmbFineStatus.SelectedIndex == 0)
            {
                list = list.Where(x => x.FineStatus == "Completed").ToList();
            }
            if (cmbFineStatus.SelectedIndex == 1)
            {
                list = list.Where(x => x.FineStatus == "NOT completed").ToList();
            }
            if (cmbFineStatus.SelectedIndex == 2)
            {
                list = list.Where(x => x.FineStatus == "NOT Paid").ToList();
            }
            dataGridViewFinedMembers.DataSource = list;
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
            FormConstitution open = new FormConstitution();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void dataGridViewConstitution_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            constitutionDetail = GeneralHelper.MapFromGrid<ConstitutionDetailDTO>(dataGridViewConstitution, e.RowIndex);
        }

        private void btnViewConstitution_Click(object sender, EventArgs e)
        {
            if (constitutionDetail.ConstitutionID == 0)
            {
                MessageBox.Show("Please choose a constitution from the table.");
            }
            else
            {
                FormViewConstitution open = new FormViewConstitution();
                open.detail = constitutionDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnDeleteConstitution_Click(object sender, EventArgs e)
        {
            if (constitutionDetail.ConstitutionID == 0)
            {
                MessageBox.Show("Please choose a member from the table.");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (constitutionBLL.Delete(constitutionDetail))
                    {
                        MessageBox.Show("Constitution was deleted!");
                        ClearFilters();
                    }
                }
            }
        }

        private void btnUpdateConstitution_Click(object sender, EventArgs e)
        {
            if (constitutionDetail.ConstitutionID == 0)
            {
                MessageBox.Show("Please choose a member from the table.");
            }
            else
            {
                FormConstitution open = new FormConstitution();
                open.detail = constitutionDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtConstitution_TextChanged(object sender, EventArgs e)
        {
            List<ConstitutionDetailDTO> list = constitutionDTO.Constitutions;
            list = list.Where(x => x.ConstitutionText.Contains(txtConstitution.Text.Trim())).ToList();
            dataGridViewConstitution.DataSource = list;
        }

        private void txtSection_TextChanged(object sender, EventArgs e)
        {
            List<ConstitutionDetailDTO> list = constitutionDTO.Constitutions;
            list = list.Where(x => x.Section.Contains(txtSection.Text.Trim())).ToList();
            dataGridViewConstitution.DataSource = list;
        }

        private void btnClearConstitution_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void txtConstitutionSection_TextChanged(object sender, EventArgs e)
        {
            List<FinedMemberDetailDTO> list = finedMemberDTO.FineMembers;
            list = list.Where(x => x.ConstitutionSection.Contains(txtConstitutionSection.Text.Trim())).ToList();
            dataGridViewFinedMembers.DataSource = list;
        }

        private void txtFine_TextChanged(object sender, EventArgs e)
        {
            List<ConstitutionDetailDTO> list = constitutionDTO.Constitutions;
            list = list.Where(x => x.Fine.ToString().Contains(txtFine.Text.Trim())).ToList();
            dataGridViewConstitution.DataSource = list;
        }


        // -------------------------------------------------------------------------
        // -------------------- SPECIAL CONTRIBUTIONS ----------------------
        // -------------------------------------------------------------------------

        private void btnAddContribution_Click(object sender, EventArgs e)
        {
            FormSpecialContribution open = new FormSpecialContribution();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateContribution_Click(object sender, EventArgs e)
        {
            if (specialContributionDetail.SpecialContributionID == 0)
            {
                MessageBox.Show("Please choose a special contribution from the table.");
            }
            else
            {
                FormSpecialContribution open = new FormSpecialContribution();
                open.detail = specialContributionDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void dataGridViewSpecialContributions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            specialContributionDetail = GeneralHelper.MapFromGrid<SpecialContributionDetailDTO>(dataGridViewSpecialContributions, e.RowIndex);
        }

        private void btnClearContribution_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void txtAmountSContributions_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtNoOfContributors_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtAmountSContributions_TextChanged(object sender, EventArgs e)
        {
            List<SpecialContributionDetailDTO> list = specialContributionDTO.SpecialContributions;
            list = list.Where(x => x.AmountToContribute == Convert.ToDecimal(txtAmountSContributions.Text.Trim())).ToList();
            dataGridViewSpecialContributions.DataSource = list;
        }

        private void txtNoOfContributors_TextChanged(object sender, EventArgs e)
        {
            List<SpecialContributionDetailDTO> list = specialContributionDTO.SpecialContributions;
            list = list.Where(x => x.TotalMembers == Convert.ToInt32(txtNoOfContributors.Text.Trim())).ToList();
            dataGridViewSpecialContributions.DataSource = list;
        }

        private void btnViewContribution_Click(object sender, EventArgs e)
        {
            if (specialContributionDetail.SpecialContributionID == 0)
            {
                MessageBox.Show("Please choose a special contribution from the table.");
            }
            else
            {
                FormViewSpecialContribution open = new FormViewSpecialContribution();
                open.detail = specialContributionDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }            
        }

        private void btnDeleteContribution_Click(object sender, EventArgs e)
        {
            if (specialContributionDetail.SpecialContributionID == 0)
            {
                MessageBox.Show("Please choose a special contribution from the table.");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (specialContributionsBLL.Delete(specialContributionDetail))
                    {
                        MessageBox.Show("Special Contribution was deleted successfully!");
                        ClearFilters();
                    }
                }
            }
        }
    }
}
