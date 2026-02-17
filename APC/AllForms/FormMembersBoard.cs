using APC.BLL;
using APC.DAL.DTO;
using APC.HelperServices;
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
using System.Xml.Linq;
using static APC.HelperServices.MemberHelperService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormMembersBoard : Form
    {
        public FormMembersBoard()
        {
            InitializeComponent();
        }

        MemberBLL registeredMembersBLL = new MemberBLL();
        MemberDTO registeredMembersDTO = new MemberDTO();
        MemberDetailDTO registeredMembersDetail = new MemberDetailDTO();

        MembersCommittmentBLL committmentBLL = new MembersCommittmentBLL();
        MembersCommittmentDetailDTO committmentDetail = new MembersCommittmentDetailDTO();
        MembersCommittmentDTO committmentDTO = new MembersCommittmentDTO();

        private void FormMembersBoard_Load(object sender, EventArgs e)
        {
            if (LoginInfo.AccessLevel != 4)
            {
                btnDeleteRegisteredMembers.Hide();
            }

            ResizeControls();


            #region
            registeredMembersDTO = registeredMembersBLL.Select();
            birthdayDTO = birthdayBLL.SelectBirthdayMembers(DateTime.Now.Month);
            formerMembersDTO = formerMembersBLL.SelectFormerMembers();
            deadMembersDTO = deadMembersBLL.SelectDeadMembers();
            inactiveMembersDTO = inactiveMembersBLL.SelectInactiveMembers();
            contactsDTO = contactsBLL.Select();

            LoadRegisteredMembers();
            FillRegisteredMemberComboBoxes();

            LoadBirthDayMembers();
            FillBirthdayMemberComboBoxes();

            LoadFormerMembers();
            FillFormerMemberComboBoxes();

            LoadDeadMembers();
            FillDeadMemberComboBoxes();

            LoadInactiveMembers();
            FillInactiveMemberComboBoxes();

            LoadMembersContact();

            #endregion

            // Members' Commitments
            #region
            committmentDTO = committmentBLL.Select(DateTime.Now.Year);

            cmbYearCommittment.DataSource = committmentDTO.Years;
            cmbYearCommittment.SelectedIndex = -1;

            cmbDuesStatusCommittment.Items.Add("Incomplete");
            cmbDuesStatusCommittment.Items.Add("Completed");
            cmbDuesStatusCommittment.Items.Add("Extra");

            cmbFineStatusCommittment.Items.Add("Incomplete");
            cmbFineStatusCommittment.Items.Add("Completed");
            cmbFineStatusCommittment.Items.Add("Extra");

            dataGridViewCommitments.DataSource = committmentDTO.Committments;
            dataGridViewCommitments.Columns[0].Visible = false;
            dataGridViewCommitments.Columns[1].HeaderText = "Rank Ratio";
            dataGridViewCommitments.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCommitments.Columns[2].HeaderText = "Name";
            dataGridViewCommitments.Columns[3].HeaderText = "Surname";
            dataGridViewCommitments.Columns[4].Visible = false;
            dataGridViewCommitments.Columns[5].HeaderText = "Exp (€)";
            dataGridViewCommitments.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCommitments.Columns[6].HeaderText = "Cont (€)";
            dataGridViewCommitments.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCommitments.Columns[7].HeaderText = "Dues Bal.";
            dataGridViewCommitments.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCommitments.Columns[8].HeaderText = "Fines (€)";
            dataGridViewCommitments.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCommitments.Columns[9].HeaderText = "P. Fines (€)";
            dataGridViewCommitments.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCommitments.Columns[10].HeaderText = "Pres.";
            dataGridViewCommitments.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCommitments.Columns[11].HeaderText = "Abs.";
            dataGridViewCommitments.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCommitments.Columns[12].Visible = false;

            foreach (DataGridViewColumn column in dataGridViewCommitments.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }
            dataGridViewCommitments.DefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            #endregion


            GetCounts();
        }

        private void LoadBirthDayMembers()
        {
            dataGridViewBirthday.DataSource = birthdayDTO.Members;
            ConfigureMemberGrid(dataGridViewBirthday, MemberGridType.Birthday);
        }

        private void LoadRegisteredMembers()
        {
            dataGridViewRegisteredMembers.DataSource = registeredMembersDTO.Members;
            ConfigureMemberGrid(dataGridViewRegisteredMembers, MemberGridType.Basic);
        }

        private void LoadMembersContact()
        {
            dataGridViewContacts.DataSource = contactsDTO.Members;
            ConfigureMemberGrid(dataGridViewContacts, MemberGridType.Contact);
        }

        private void LoadInactiveMembers()
        {
            dataGridViewInactiveMembers.DataSource = inactiveMembersDTO.Members;
            ConfigureMemberGrid(dataGridViewInactiveMembers, MemberGridType.Basic);
        }

        private void LoadDeadMembers()
        {
            dataGridViewDeadMembers.DataSource = deadMembersDTO.Members;
            ConfigureMemberGrid(dataGridViewDeadMembers, MemberGridType.Dead);
        }

        private void LoadFormerMembers()
        {
            dataGridViewFormerMembers.DataSource = formerMembersDTO.Members;
            ConfigureMemberGrid(dataGridViewFormerMembers, MemberGridType.Basic);
        }

        private void ResizeControls()
        {
            #region
            GeneralHelper.ApplyRegularFont11(label4, label7, label8, label19, label20, label21, label28, label29, label30,
                label31, labelTotalContacts,
                labelNoOfDivisorRegisteredMembers, labelNoOfFemaleRegisteredMembers, labelNoOfMenRegisteredMembers,
                labelNoOfDivisorFormerMembers, labelNoOfFemaleFormerMembers, labelNoOfMenFormerMembers,
                labelNoOfDivisorDeadMembers, labelNoOfFemaleDeadMembers, labelNoOfMenDeadMembers);

            GeneralHelper.ApplyBoldFont11(label24, label25, label26);

            GeneralHelper.ApplyBoldFont14(label1, label2, label3, label5, label6, label9, label10, label12,
                label13, label14, label15, label16, label17, label18, label22, label23, label27, label32,
                label33, label34, label35, label36, btnAddRegisteredMembers,
                btnUpdateRegisteredMembers, btnViewRegisteredMembers, btnDeleteRegisteredMembers,
                btnSearchRegisteredMembers, btnClearRegisteredMembers, btnViewBirthday, btnSearchBirthday,
                btnClearBirthday, btnUpdateContacts, btnUpdateFormerMembers, btnViewFormerMembers,
                btnSearchFormerMembers, btnClearFormerMembers, btnUpdateDeadMembers, btnViewDeadMembers,
                btnSearchDeadMembers, btnClearDeadMembers);

            GeneralHelper.ApplyRegularFont16(txtNameRegisteredMembers, txtSurnameRegisteredMembers,
                cmbGenderRegisteredMembers, cmbNationalityRegisteredMembers, cmbPositionRegisteredMembers,
                cmbProfessionRegisteredMembers, txtNameBirthday, txtSurnameBirthday, cmbGenderBirthday,
                cmbNationalityBirthday, cmbProfessionBirthday, cmbPositionBirthday, cmbMonthBirthday,
                txtSurnameContacts, txtNameFormerMembers, txtSurnameFormerMembers, cmbGenderFormerMembers,
                cmbNationalityFormerMembers, cmbPositionFormerMembers, cmbProfessionFormerMembers,
                txtNameDeadMembers, txtSurnameDeadMembers, cmbGenderDeadMembers, cmbNationalityDeadMembers,
                cmbPositionDeadMembers, cmbProfessionDeadMembers
                );

            txtNameCommittment.Tag = "resizeable";
            txtSurnameCommittment.Tag = "resizeable";
            cmbDuesStatusCommittment.Tag = "resizeable";
            cmbFineStatusCommittment.Tag = "resizeable";
            cmbYearCommittment.Tag = "resizeable";
            #endregion
        }

        private void FillRegisteredMemberComboBoxes()
        {
            cmbGenderRegisteredMembers.DataSource = registeredMembersDTO.Genders;
            GeneralHelper.ComboBoxProps(cmbGenderRegisteredMembers, "GenderName", "genderID");
            cmbProfessionRegisteredMembers.DataSource = registeredMembersDTO.Professions;
            GeneralHelper.ComboBoxProps(cmbProfessionRegisteredMembers, "Profession", "professionID");
            cmbPositionRegisteredMembers.DataSource = registeredMembersDTO.Positions;
            GeneralHelper.ComboBoxProps(cmbPositionRegisteredMembers, "PositionName", "positionID");
            cmbNationalityRegisteredMembers.DataSource = registeredMembersDTO.Nationalities;
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
            cmbGenderFormerMembers.DataSource = formerMembersDTO.Genders;
            GeneralHelper.ComboBoxProps(cmbGenderFormerMembers, "GenderName", "genderID");
            cmbProfessionFormerMembers.DataSource = formerMembersDTO.Professions;
            GeneralHelper.ComboBoxProps(cmbProfessionFormerMembers, "Profession", "professionID");
            cmbPositionFormerMembers.DataSource = formerMembersDTO.Positions;
            GeneralHelper.ComboBoxProps(cmbPositionFormerMembers, "PositionName", "positionID");
            cmbNationalityFormerMembers.DataSource = formerMembersDTO.Nationalities;
            GeneralHelper.ComboBoxProps(cmbNationalityFormerMembers, "Nationality", "NationalityID");
        }

        private void FillDeadMemberComboBoxes()
        {
            cmbGenderDeadMembers.DataSource = deadMembersDTO.Genders;
            GeneralHelper.ComboBoxProps(cmbGenderDeadMembers, "GenderName", "genderID");
            cmbProfessionDeadMembers.DataSource = deadMembersDTO.Professions;
            GeneralHelper.ComboBoxProps(cmbProfessionDeadMembers, "Profession", "professionID");
            cmbPositionDeadMembers.DataSource = deadMembersDTO.Positions;
            GeneralHelper.ComboBoxProps(cmbPositionDeadMembers, "PositionName", "positionID");
            cmbNationalityDeadMembers.DataSource = deadMembersDTO.Nationalities;
            GeneralHelper.ComboBoxProps(cmbNationalityDeadMembers, "Nationality", "NationalityID");
        }

        private void FillInactiveMemberComboBoxes()
        {
            cmbGenderInactiveMembers.DataSource = inactiveMembersDTO.Genders;
            GeneralHelper.ComboBoxProps(cmbGenderInactiveMembers, "GenderName", "genderID");
            cmbProfessionInactiveMembers.DataSource = inactiveMembersDTO.Professions;
            GeneralHelper.ComboBoxProps(cmbProfessionInactiveMembers, "Profession", "professionID");
            cmbPositionInactiveMembers.DataSource = inactiveMembersDTO.Positions;
            GeneralHelper.ComboBoxProps(cmbPositionInactiveMembers, "PositionName", "positionID");
            cmbNationalityInactiveMembers.DataSource = inactiveMembersDTO.Nationalities;
            GeneralHelper.ComboBoxProps(cmbNationalityInactiveMembers, "Nationality", "NationalityID");
        }

        private void btnAddRegisteredMembers_Click(object sender, EventArgs e)
        {
            FormMembers open = new FormMembers();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void ClearFilters()
        {
            txtNameRegisteredMembers.Clear();
            txtSurnameRegisteredMembers.Clear();
            cmbNationalityRegisteredMembers.SelectedIndex = -1;
            cmbGenderRegisteredMembers.SelectedIndex = -1;
            cmbPositionRegisteredMembers.SelectedIndex = -1;
            cmbProfessionRegisteredMembers.SelectedIndex = -1;
            registeredMembersBLL = new MemberBLL();
            registeredMembersDTO = registeredMembersBLL.Select();
            dataGridViewRegisteredMembers.DataSource = registeredMembersDTO.Members;

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
            contactsBLL = new MemberBLL();
            contactsDTO = contactsBLL.Select();
            dataGridViewContacts.DataSource = contactsDTO.Members;

            txtNameFormerMembers.Clear();
            txtSurnameFormerMembers.Clear();
            cmbNationalityFormerMembers.SelectedIndex = -1;
            cmbGenderFormerMembers.SelectedIndex = -1;
            cmbPositionFormerMembers.SelectedIndex = -1;
            cmbProfessionFormerMembers.SelectedIndex = -1;
            formerMembersBLL = new MemberBLL();
            formerMembersDTO = formerMembersBLL.SelectFormerMembers();
            dataGridViewFormerMembers.DataSource = formerMembersDTO.Members;

            txtNameDeadMembers.Clear();
            txtSurnameDeadMembers.Clear();
            cmbNationalityDeadMembers.SelectedIndex = -1;
            cmbGenderDeadMembers.SelectedIndex = -1;
            cmbPositionDeadMembers.SelectedIndex = -1;
            cmbProfessionDeadMembers.SelectedIndex = -1;
            deadMembersBLL = new MemberBLL();
            deadMembersDTO = deadMembersBLL.SelectDeadMembers();
            dataGridViewDeadMembers.DataSource = deadMembersDTO.Members;

            committmentBLL = new MembersCommittmentBLL();
            committmentDTO = committmentBLL.Select(DateTime.Now.Year);
            dataGridViewCommitments.DataSource = committmentDTO.Committments;
            txtNameCommittment.Clear();
            txtSurnameCommittment.Clear();            
            cmbYearCommittment.SelectedIndex = -1;
            cmbDuesStatusCommittment.SelectedIndex = -1;
            cmbFineStatusCommittment.SelectedIndex = -1;

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
            labelNoOfMenRegisteredMembers.Text = registeredMembersBLL.SelectCountMale().ToString();
            labelNoOfFemaleRegisteredMembers.Text = registeredMembersBLL.SelectCountFemale().ToString();
            labelNoOfDivisorRegisteredMembers.Text = registeredMembersBLL.SelectCountDivisor().ToString();

            labelTotalContacts.Text = "Total: " + dataGridViewContacts.RowCount.ToString();

            labelNoOfMenFormerMembers.Text = formerMembersBLL.SelectCountFormerMale().ToString();
            labelNoOfFemaleFormerMembers.Text = formerMembersBLL.SelectCountFormerFemale().ToString();
            labelNoOfDivisorFormerMembers.Text = formerMembersBLL.SelectCountFormerDivisor().ToString();

            labelNoOfMenDeadMembers.Text = deadMembersBLL.SelectCountDeadMale().ToString();
            labelNoOfFemaleDeadMembers.Text = deadMembersBLL.SelectCountDeadFemale().ToString();
            labelNoOfDivisorDeadMembers.Text = deadMembersBLL.SelectCountDeadDivisor().ToString();

            labelTotalRowsCommittment.Text = "Row : " + dataGridViewCommitments.RowCount.ToString();

            labelTotalRowsCommittment.Text = "Row : " + dataGridViewBirthday.RowCount.ToString();
        }

        private void btnUpdateRegisteredMembers_Click(object sender, EventArgs e)
        {
            if (registeredMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormMembers open = new FormMembers();
                open.detail = registeredMembersDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnViewRegisteredMembers_Click(object sender, EventArgs e)
        {            
            ViewMember(registeredMembersDetail, false);
        }

        private void txtNameRegisteredMembers_TextChanged(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = registeredMembersDTO.Members;
            list = list.Where(x => x.Name.Contains(txtNameRegisteredMembers.Text)).ToList();
            dataGridViewRegisteredMembers.DataSource = list;
        }

        private void txtSurnameRegisteredMembers_TextChanged(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = registeredMembersDTO.Members;
            list = list.Where(x => x.Surname.Contains(txtSurnameRegisteredMembers.Text)).ToList();
            dataGridViewRegisteredMembers.DataSource = list;
        }

        private void btnSearchRegisteredMembers_Click(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = registeredMembersDTO.Members;
            if (cmbNationalityRegisteredMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.NationalityID == Convert.ToInt32(cmbNationalityRegisteredMembers.SelectedValue)).ToList();
            }
            if (cmbGenderRegisteredMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.GenderID == Convert.ToInt32(cmbGenderRegisteredMembers.SelectedValue)).ToList();
            }
            if (cmbPositionRegisteredMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPositionRegisteredMembers.SelectedValue)).ToList();
            }
            if (cmbProfessionRegisteredMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.ProfessionID == Convert.ToInt32(cmbProfessionRegisteredMembers.SelectedValue)).ToList();
            }
            dataGridViewRegisteredMembers.DataSource = list;
        }

        private void btnClearRegisteredMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnDeleteRegisteredMembers_Click(object sender, EventArgs e)
        {
            if (registeredMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please select a member from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (registeredMembersBLL.Delete(registeredMembersDetail))
                    {
                        MessageBox.Show("Member was deleted");                        
                        ClearFilters();
                    }
                }
            }
        }

        private void dataGridViewRegisteredMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            registeredMembersDetail = MapMemberFromGrid(dataGridViewRegisteredMembers, e.RowIndex);

            // Load image
            string imagePath = Path.Combine(Application.StartupPath, "images", registeredMembersDetail.ImagePath);
            picRegisteredMember.ImageLocation = imagePath;
        }


        MemberBLL contactsBLL = new MemberBLL();
        MemberDTO contactsDTO = new MemberDTO();
        MemberDetailDTO contactsDetail = new MemberDetailDTO();

        private void txtSurnameContacts_TextChanged(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = contactsDTO.Members;
            list = list.Where(x => x.Surname.Contains(txtSurnameContacts.Text)).ToList();
            dataGridViewContacts.DataSource = list;
        }

        private void btnUpdateContacts_Click(object sender, EventArgs e)
        {
            if (contactsDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormMembers open = new FormMembers();
                open.detail = contactsDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;                
                ClearFilters();
            }
        }

        private void dataGridViewContacts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            contactsDetail = MapMemberFromGrid(dataGridViewContacts, e.RowIndex);
        }

        MemberDetailDTO formerMembersDetail = new MemberDetailDTO();
        MemberBLL formerMembersBLL = new MemberBLL();
        MemberDTO formerMembersDTO = new MemberDTO();

        private void btnClearFormerMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnUpdateFormerMembers_Click(object sender, EventArgs e)
        {
            if (formerMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table.");
            }
            else
            {
                FormMembers open = new FormMembers();
                open.isUpdate = true;
                open.isUpdateDeadMember = true;
                open.detail = formerMembersDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;                
                ClearFilters();
            }
        }

        private void btnViewFormerMembers_Click(object sender, EventArgs e)
        {            
            ViewMember(formerMembersDetail, true);
        }

        private void dataGridViewFormerMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            formerMembersDetail = MapMemberFromGrid(dataGridViewFormerMembers, e.RowIndex);
        }

        private void txtNameFormerMembers_TextChanged(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = formerMembersDTO.Members;
            list = list.Where(x => x.Name.Contains(txtNameFormerMembers.Text)).ToList();
            dataGridViewFormerMembers.DataSource = list;
        }

        private void txtSurnameFormerMembers_TextChanged(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = formerMembersDTO.Members;
            list = list.Where(x => x.Surname.Contains(txtSurnameFormerMembers.Text)).ToList();
            dataGridViewFormerMembers.DataSource = list;
        }

        private void btnSearchFormerMembers_Click(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = formerMembersDTO.Members;
            if (cmbNationalityFormerMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.NationalityID == Convert.ToInt32(cmbNationalityFormerMembers.SelectedValue)).ToList();
            }
            if (cmbGenderFormerMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.GenderID == Convert.ToInt32(cmbGenderFormerMembers.SelectedValue)).ToList();
            }
            if (cmbPositionFormerMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPositionFormerMembers.SelectedValue)).ToList();
            }
            if (cmbProfessionFormerMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.ProfessionID == Convert.ToInt32(cmbProfessionFormerMembers.SelectedValue)).ToList();
            }
            dataGridViewFormerMembers.DataSource = list;
        }

        MemberDetailDTO deadMembersDetail = new MemberDetailDTO();
        MemberBLL deadMembersBLL = new MemberBLL();
        MemberDTO deadMembersDTO = new MemberDTO();

        private void btnUpdateDeadMembers_Click(object sender, EventArgs e)
        {
            if (deadMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table.");
            }
            else
            {
                FormMembers open = new FormMembers();
                open.isUpdate = true;
                open.isUpdateDeadMember = true;
                open.detail = deadMembersDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void dataGridViewDeadMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            deadMembersDetail = MapMemberFromGrid(dataGridViewDeadMembers, e.RowIndex);
        }
        private void txtNameDeadMembers_TextChanged(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = deadMembersDTO.Members;
            list = list.Where(x => x.Name.Contains(txtNameDeadMembers.Text)).ToList();
            dataGridViewDeadMembers.DataSource = list;
        }

        private void txtSurnameDeadMembers_TextChanged(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = deadMembersDTO.Members;
            list = list.Where(x => x.Surname.Contains(txtSurnameDeadMembers.Text)).ToList();
            dataGridViewDeadMembers.DataSource = list;
        }

        private void btnSearchDeadMembers_Click(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = deadMembersDTO.Members;
            if (cmbNationalityDeadMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.NationalityID == Convert.ToInt32(cmbNationalityDeadMembers.SelectedValue)).ToList();
            }
            if (cmbGenderDeadMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.GenderID == Convert.ToInt32(cmbGenderDeadMembers.SelectedValue)).ToList();
            }
            if (cmbPositionDeadMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPositionDeadMembers.SelectedValue)).ToList();
            }
            if (cmbProfessionDeadMembers.SelectedIndex != -1)
            {
                list = list.Where(x => x.ProfessionID == Convert.ToInt32(cmbProfessionDeadMembers.SelectedValue)).ToList();
            }
            dataGridViewDeadMembers.DataSource = list;
        }

        private void btnClearDeadMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void btnViewDeadMembers_Click(object sender, EventArgs e)
        {
            if (deadMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormViewDeadMember open = new FormViewDeadMember();
                open.detail = deadMembersDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;               
                ClearFilters();
            }
        }
        
        private void dataGridViewCommitments_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            committmentDetail = new MembersCommittmentDetailDTO();
            committmentDetail.MemberID = Convert.ToInt32(dataGridViewCommitments.Rows[e.RowIndex].Cells[0].Value);
            committmentDetail.Rank = Convert.ToDecimal(dataGridViewCommitments.Rows[e.RowIndex].Cells[1].Value);
            committmentDetail.Name = dataGridViewCommitments.Rows[e.RowIndex].Cells[2].Value.ToString();
            committmentDetail.Surname = dataGridViewCommitments.Rows[e.RowIndex].Cells[3].Value.ToString();
            committmentDetail.ImagePath = dataGridViewCommitments.Rows[e.RowIndex].Cells[4].Value.ToString();
            committmentDetail.ExpectedAmount = Convert.ToDecimal(dataGridViewCommitments.Rows[e.RowIndex].Cells[5].Value);
            committmentDetail.Contributed = Convert.ToDecimal(dataGridViewCommitments.Rows[e.RowIndex].Cells[6].Value);
            committmentDetail.Balance = dataGridViewCommitments.Rows[e.RowIndex].Cells[7].Value.ToString();
            committmentDetail.Fines = Convert.ToDecimal(dataGridViewCommitments.Rows[e.RowIndex].Cells[8].Value);
            committmentDetail.PaidFines = Convert.ToDecimal(dataGridViewCommitments.Rows[e.RowIndex].Cells[9].Value);
            committmentDetail.NumberOfPresence = Convert.ToInt32(dataGridViewCommitments.Rows[e.RowIndex].Cells[10].Value);
            committmentDetail.NumberOfAbsence = Convert.ToInt32(dataGridViewCommitments.Rows[e.RowIndex].Cells[11].Value);

            string imagePath = Application.StartupPath + "\\images\\" + committmentDetail.ImagePath;
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

        private void btnSearchCommittment_Click(object sender, EventArgs e)
        {            
            if (cmbYearCommittment.SelectedIndex != -1)
            {
                committmentBLL = new MembersCommittmentBLL();
                committmentDTO = committmentBLL.Select(Convert.ToInt32(cmbYearCommittment.SelectedValue));
                dataGridViewCommitments.DataSource = committmentDTO.Committments;

                List<MembersCommittmentDetailDTO> list = committmentDTO.Committments;
                if (cmbDuesStatusCommittment.SelectedIndex != -1)
                {
                    if (cmbDuesStatusCommittment.SelectedIndex == 0)
                    {
                        list = list.Where(x => x.Balance.Contains("€ Remaining")).ToList();
                    }
                    if (cmbDuesStatusCommittment.SelectedIndex == 1)
                    {
                        list = list.Where(x => x.Balance.Contains("Completed")).ToList();
                    }
                    if (cmbDuesStatusCommittment.SelectedIndex == 2)
                    {
                        list = list.Where(x => x.Balance.Contains("€ Extra")).ToList();
                    }
                }
                if (cmbFineStatusCommittment.SelectedIndex != -1)
                {
                    if (cmbFineStatusCommittment.SelectedIndex == 0)
                    {
                        list = list.Where(x => x.PaidFines < x.Fines).ToList();
                    }
                    if (cmbFineStatusCommittment.SelectedIndex == 1)
                    {
                        list = list.Where(x => x.PaidFines == x.Fines).ToList();
                    }
                    if (cmbFineStatusCommittment.SelectedIndex == 2)
                    {
                        list = list.Where(x => x.PaidFines > x.Fines).ToList();
                    }
                }

                dataGridViewCommitments.DataSource = list;
                GetCounts();
            }
            else
            {
                MessageBox.Show("Please select year");
            }

        }

        private void btnClearCommittment_Click(object sender, EventArgs e)
        {
            ClearFilters();
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

        MemberDetailDTO inactiveMembersDetail = new MemberDetailDTO();
        MemberBLL inactiveMembersBLL = new MemberBLL();
        MemberDTO inactiveMembersDTO = new MemberDTO();

        private void btnClearInactiveMembers_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        private void dataGridViewInactiveMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            inactiveMembersDetail = MapMemberFromGrid(dataGridViewInactiveMembers, e.RowIndex);
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


        MemberBLL birthdayBLL = new MemberBLL();
        MemberDTO birthdayDTO = new MemberDTO();
        MemberDetailDTO birthdayDetail = new MemberDetailDTO();

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
            birthdayDetail = MapMemberFromGrid(dataGridViewBirthday, e.RowIndex);
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
    }
}