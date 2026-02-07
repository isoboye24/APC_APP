using APC.BLL;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
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
                btnDeleteChildren.Hide();
            }
            #region
            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label4.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label7.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label8.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfDivisorRegisteredMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelNoOfFemaleRegisteredMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelNoOfMenRegisteredMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtNameRegisteredMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtSurnameRegisteredMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbGenderRegisteredMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbNationalityRegisteredMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbPositionRegisteredMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbProfessionRegisteredMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnAddRegisteredMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateRegisteredMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewRegisteredMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteRegisteredMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearchRegisteredMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearRegisteredMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label12.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label13.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label14.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label15.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label16.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label17.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label11.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelFemaleChildren.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelMaleChildren.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label10.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtNameChildren.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtFathersNameChildren.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtMothersNameChildren.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtSurnameChildren.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbGenderChildren.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbNationalityChildren.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnAddChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearchChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearChildren.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label18.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalContacts.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtSurnameContacts.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnUpdateContacts.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label19.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label20.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label21.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label22.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label23.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label24.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            label25.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            label26.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            label27.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfDivisorFormerMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelNoOfFemaleFormerMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelNoOfMenFormerMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtNameFormerMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtSurnameFormerMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbGenderFormerMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbNationalityFormerMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbPositionFormerMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbProfessionFormerMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnUpdateFormerMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewFormerMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearchFormerMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearFormerMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label28.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label29.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label30.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label31.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            label32.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label33.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label34.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label35.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label36.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelNoOfDivisorDeadMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelNoOfFemaleDeadMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            labelNoOfMenDeadMembers.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtNameDeadMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            txtSurnameDeadMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbGenderDeadMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbNationalityDeadMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbPositionDeadMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            cmbProfessionDeadMembers.Font = new Font("Segoe UI", 16, FontStyle.Regular);
            btnUpdateDeadMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnViewDeadMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearchDeadMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearDeadMembers.Font = new Font("Segoe UI", 14, FontStyle.Bold);


            txtNameCommittment.Tag = "resizeable";
            txtSurnameCommittment.Tag = "resizeable";
            cmbDuesStatusCommittment.Tag = "resizeable";
            cmbFineStatusCommittment.Tag = "resizeable";
            cmbYearCommittment.Tag = "resizeable";
            #endregion

            #region
            registeredMembersDTO = registeredMembersBLL.Select();
            cmbGenderRegisteredMembers.DataSource = registeredMembersDTO.Genders;
            General.ComboBoxProps(cmbGenderRegisteredMembers, "GenderName", "genderID");
            cmbProfessionRegisteredMembers.DataSource = registeredMembersDTO.Professions;
            General.ComboBoxProps(cmbProfessionRegisteredMembers, "Profession", "professionID");
            cmbPositionRegisteredMembers.DataSource = registeredMembersDTO.Positions;
            General.ComboBoxProps(cmbPositionRegisteredMembers, "PositionName", "positionID");
            cmbNationalityRegisteredMembers.DataSource = registeredMembersDTO.Nationalities;
            General.ComboBoxProps(cmbNationalityRegisteredMembers, "Nationality", "NationalityID");

            childrenDTO = childrenBLL.Select();
            cmbGenderChildren.DataSource = childrenDTO.Genders;
            cmbNationalityChildren.DataSource = childrenDTO.Nationalities;
            General.ComboBoxProps(cmbGenderChildren, "GenderName", "GenderID");
            General.ComboBoxProps(cmbNationalityChildren, "Nationality", "NationalityID");

            formerMembersDTO = formerMembersBLL.SelectFormerMembers();
            cmbGenderFormerMembers.DataSource = formerMembersDTO.Genders;
            General.ComboBoxProps(cmbGenderFormerMembers, "GenderName", "genderID");
            cmbProfessionFormerMembers.DataSource = formerMembersDTO.Professions;
            General.ComboBoxProps(cmbProfessionFormerMembers, "Profession", "professionID");
            cmbPositionFormerMembers.DataSource = formerMembersDTO.Positions;
            General.ComboBoxProps(cmbPositionFormerMembers, "PositionName", "positionID");
            cmbNationalityFormerMembers.DataSource = formerMembersDTO.Nationalities;
            General.ComboBoxProps(cmbNationalityFormerMembers, "Nationality", "NationalityID");

            deadMembersDTO = deadMembersBLL.SelectDeadMembers();
            cmbGenderDeadMembers.DataSource = deadMembersDTO.Genders;
            General.ComboBoxProps(cmbGenderDeadMembers, "GenderName", "genderID");
            cmbProfessionDeadMembers.DataSource = deadMembersDTO.Professions;
            General.ComboBoxProps(cmbProfessionDeadMembers, "Profession", "professionID");
            cmbPositionDeadMembers.DataSource = deadMembersDTO.Positions;
            General.ComboBoxProps(cmbPositionDeadMembers, "PositionName", "positionID");
            cmbNationalityDeadMembers.DataSource = deadMembersDTO.Nationalities;
            General.ComboBoxProps(cmbNationalityDeadMembers, "Nationality", "NationalityID");

            inactiveMembersDTO = inactiveMembersBLL.SelectInactiveMembers();
            cmbGenderInactiveMembers.DataSource = inactiveMembersDTO.Genders;
            General.ComboBoxProps(cmbGenderInactiveMembers, "GenderName", "genderID");
            cmbProfessionInactiveMembers.DataSource = inactiveMembersDTO.Professions;
            General.ComboBoxProps(cmbProfessionInactiveMembers, "Profession", "professionID");
            cmbPositionInactiveMembers.DataSource = inactiveMembersDTO.Positions;
            General.ComboBoxProps(cmbPositionInactiveMembers, "PositionName", "positionID");
            cmbNationalityInactiveMembers.DataSource = inactiveMembersDTO.Nationalities;
            General.ComboBoxProps(cmbNationalityInactiveMembers, "Nationality", "NationalityID");

            #endregion

            #region
            #region
            dataGridViewRegisteredMembers.DataSource = registeredMembersDTO.Members;
                dataGridViewRegisteredMembers.Columns[0].Visible = false;
                dataGridViewRegisteredMembers.Columns[1].Visible = false;
                dataGridViewRegisteredMembers.Columns[2].Visible = false;
                dataGridViewRegisteredMembers.Columns[3].HeaderText = "Surname";
                dataGridViewRegisteredMembers.Columns[4].HeaderText = "Name";
                dataGridViewRegisteredMembers.Columns[5].Visible = false;
                dataGridViewRegisteredMembers.Columns[6].Visible = false;
                dataGridViewRegisteredMembers.Columns[7].Visible = false;
                dataGridViewRegisteredMembers.Columns[8].Visible = false;
                dataGridViewRegisteredMembers.Columns[9].Visible = false;
                dataGridViewRegisteredMembers.Columns[10].Visible = false;
                dataGridViewRegisteredMembers.Columns[11].Visible = false;
                dataGridViewRegisteredMembers.Columns[12].Visible = false;
                dataGridViewRegisteredMembers.Columns[13].HeaderText = "Nationality";
                dataGridViewRegisteredMembers.Columns[14].Visible = false;
                dataGridViewRegisteredMembers.Columns[15].HeaderText = "Profession";
                dataGridViewRegisteredMembers.Columns[16].Visible = false;
                dataGridViewRegisteredMembers.Columns[17].HeaderText = "Position";
                dataGridViewRegisteredMembers.Columns[18].Visible = false;
                dataGridViewRegisteredMembers.Columns[19].HeaderText = "Gender";
                dataGridViewRegisteredMembers.Columns[20].Visible = false;
                dataGridViewRegisteredMembers.Columns[21].Visible = false;
                dataGridViewRegisteredMembers.Columns[22].Visible = false;
                dataGridViewRegisteredMembers.Columns[23].Visible = false;
                dataGridViewRegisteredMembers.Columns[24].Visible = false;
                dataGridViewRegisteredMembers.Columns[25].Visible = false;
                dataGridViewRegisteredMembers.Columns[26].Visible = false;
                dataGridViewRegisteredMembers.Columns[27].Visible = false;
                dataGridViewRegisteredMembers.Columns[28].Visible = false;
                dataGridViewRegisteredMembers.Columns[29].Visible = false;
                dataGridViewRegisteredMembers.Columns[30].Visible = false;
                dataGridViewRegisteredMembers.Columns[31].Visible = false;
                dataGridViewRegisteredMembers.Columns[32].Visible = false;
                dataGridViewRegisteredMembers.Columns[33].Visible = false;
                dataGridViewRegisteredMembers.Columns[34].Visible = false;
                dataGridViewRegisteredMembers.Columns[35].Visible = false;
                dataGridViewRegisteredMembers.Columns[36].Visible = false;
                dataGridViewRegisteredMembers.Columns[37].Visible = false;
                dataGridViewRegisteredMembers.Columns[38].Visible = false;
                dataGridViewRegisteredMembers.Columns[39].Visible = false;
                dataGridViewRegisteredMembers.Columns[40].Visible = false;
                dataGridViewRegisteredMembers.Columns[41].Visible = false;
                dataGridViewRegisteredMembers.Columns[42].Visible = false;
                foreach (DataGridViewColumn column in dataGridViewRegisteredMembers.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
                #endregion

                #region
                dataGridViewChildren.DataSource = childrenDTO.Children;
                dataGridViewChildren.Columns[0].Visible = false;
                dataGridViewChildren.Columns[1].HeaderText = "Surname";
                dataGridViewChildren.Columns[2].HeaderText = "Name";
                dataGridViewChildren.Columns[3].Visible = false;
                dataGridViewChildren.Columns[4].Visible = false;
                dataGridViewChildren.Columns[5].Visible = false;
                dataGridViewChildren.Columns[6].HeaderText = "Gender";
                dataGridViewChildren.Columns[7].Visible = false;
                dataGridViewChildren.Columns[8].HeaderText = "Nationality";
                dataGridViewChildren.Columns[9].Visible = false;
                dataGridViewChildren.Columns[10].HeaderText = "Mother's name";
                dataGridViewChildren.Columns[11].Visible = false;
                dataGridViewChildren.Columns[12].Visible = false;
                dataGridViewChildren.Columns[13].Visible = false;
                dataGridViewChildren.Columns[14].Visible = false;
                dataGridViewChildren.Columns[15].Visible = false;
                dataGridViewChildren.Columns[16].HeaderText = "Father's name";
                dataGridViewChildren.Columns[17].Visible = false;
                dataGridViewChildren.Columns[18].Visible = false;
                dataGridViewChildren.Columns[19].Visible = false;
                dataGridViewChildren.Columns[20].Visible = false;
                dataGridViewChildren.Columns[21].Visible = false;
                foreach (DataGridViewColumn column in dataGridViewChildren.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
                #endregion

                #region
                contactsDTO = contactsBLL.Select();
                dataGridViewContacts.DataSource = contactsDTO.Members;
                dataGridViewContacts.Columns[0].Visible = false;
                dataGridViewContacts.Columns[1].Visible = false;
                dataGridViewContacts.Columns[2].Visible = false;
                dataGridViewContacts.Columns[3].HeaderText = "Surname";
                dataGridViewContacts.Columns[4].HeaderText = "Name";
                dataGridViewContacts.Columns[5].Visible = false;
                dataGridViewContacts.Columns[6].Visible = false;
                dataGridViewContacts.Columns[7].HeaderText = "Email";
                dataGridViewContacts.Columns[8].Visible = false;
                dataGridViewContacts.Columns[9].Visible = false;
                dataGridViewContacts.Columns[10].Visible = false;
                dataGridViewContacts.Columns[11].Visible = false;
                dataGridViewContacts.Columns[12].Visible = false;
                dataGridViewContacts.Columns[13].Visible = false;
                dataGridViewContacts.Columns[14].Visible = false;
                dataGridViewContacts.Columns[15].Visible = false;
                dataGridViewContacts.Columns[16].Visible = false;
                dataGridViewContacts.Columns[17].Visible = false;
                dataGridViewContacts.Columns[18].Visible = false;
                dataGridViewContacts.Columns[19].Visible = false;
                dataGridViewContacts.Columns[20].Visible = false;
                dataGridViewContacts.Columns[21].Visible = false;
                dataGridViewContacts.Columns[22].Visible = false;
                dataGridViewContacts.Columns[23].Visible = false;
                dataGridViewContacts.Columns[24].Visible = false;
                dataGridViewContacts.Columns[25].Visible = false;
                dataGridViewContacts.Columns[26].HeaderText = "Phone Number";
                dataGridViewContacts.Columns[27].HeaderText = "Phone Number 2";
                dataGridViewContacts.Columns[28].HeaderText = "Phone Number 3";
                dataGridViewContacts.Columns[29].Visible = false;
                dataGridViewContacts.Columns[30].Visible = false;
                dataGridViewContacts.Columns[31].Visible = false;
                dataGridViewContacts.Columns[32].Visible = false;
                dataGridViewContacts.Columns[33].Visible = false;
                dataGridViewContacts.Columns[34].Visible = false;
                dataGridViewContacts.Columns[35].Visible = false;
                dataGridViewContacts.Columns[36].Visible = false;
                dataGridViewContacts.Columns[37].Visible = false;
                dataGridViewContacts.Columns[38].Visible = false;
                dataGridViewContacts.Columns[39].Visible = false;
                dataGridViewContacts.Columns[40].Visible = false;
                dataGridViewContacts.Columns[41].Visible = false;
                dataGridViewContacts.Columns[42].Visible = false;
                foreach (DataGridViewColumn column in dataGridViewContacts.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
                #endregion

                #region
                dataGridViewFormerMembers.DataSource = formerMembersDTO.Members;
                dataGridViewFormerMembers.Columns[0].Visible = false;
                dataGridViewFormerMembers.Columns[1].Visible = false;
                dataGridViewFormerMembers.Columns[2].Visible = false;
                dataGridViewFormerMembers.Columns[3].HeaderText = "Surname";
                dataGridViewFormerMembers.Columns[4].HeaderText = "Name";
                dataGridViewFormerMembers.Columns[5].Visible = false;
                dataGridViewFormerMembers.Columns[6].Visible = false;
                dataGridViewFormerMembers.Columns[7].Visible = false;
                dataGridViewFormerMembers.Columns[8].Visible = false;
                dataGridViewFormerMembers.Columns[9].Visible = false;
                dataGridViewFormerMembers.Columns[10].Visible = false;
                dataGridViewFormerMembers.Columns[11].Visible = false;
                dataGridViewFormerMembers.Columns[12].Visible = false;
                dataGridViewFormerMembers.Columns[13].HeaderText = "Nationality";
                dataGridViewFormerMembers.Columns[14].Visible = false;
                dataGridViewFormerMembers.Columns[15].HeaderText = "Profession";
                dataGridViewFormerMembers.Columns[16].Visible = false;
                dataGridViewFormerMembers.Columns[17].HeaderText = "Position";
                dataGridViewFormerMembers.Columns[18].Visible = false;
                dataGridViewFormerMembers.Columns[19].HeaderText = "Gender";
                dataGridViewFormerMembers.Columns[19].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewFormerMembers.Columns[20].Visible = false;
                dataGridViewFormerMembers.Columns[21].Visible = false;
                dataGridViewFormerMembers.Columns[22].Visible = false;
                dataGridViewFormerMembers.Columns[23].Visible = false;
                dataGridViewFormerMembers.Columns[24].Visible = false;
                dataGridViewFormerMembers.Columns[25].Visible = false;
                dataGridViewFormerMembers.Columns[26].Visible = false;
                dataGridViewFormerMembers.Columns[27].Visible = false;
                dataGridViewFormerMembers.Columns[28].Visible = false;
                dataGridViewFormerMembers.Columns[29].Visible = false;
                dataGridViewFormerMembers.Columns[30].Visible = false;
                dataGridViewFormerMembers.Columns[31].Visible = false;
                dataGridViewFormerMembers.Columns[32].Visible = false;
                dataGridViewFormerMembers.Columns[33].Visible = false;
                dataGridViewFormerMembers.Columns[34].Visible = false;
                dataGridViewFormerMembers.Columns[35].Visible = false;
                dataGridViewFormerMembers.Columns[36].Visible = false;
                dataGridViewFormerMembers.Columns[37].Visible = false;
                dataGridViewFormerMembers.Columns[38].Visible = false;
                dataGridViewFormerMembers.Columns[39].Visible = false;
                dataGridViewFormerMembers.Columns[40].Visible = false;
                dataGridViewFormerMembers.Columns[41].Visible = false;
                dataGridViewFormerMembers.Columns[42].Visible = false;
                foreach (DataGridViewColumn column in dataGridViewFormerMembers.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
                #endregion

                #region
                dataGridViewDeadMembers.DataSource = deadMembersDTO.Members;
                dataGridViewDeadMembers.Columns[0].Visible = false;
                dataGridViewDeadMembers.Columns[1].Visible = false;
                dataGridViewDeadMembers.Columns[2].Visible = false;
                dataGridViewDeadMembers.Columns[3].HeaderText = "Surname";
                dataGridViewDeadMembers.Columns[4].HeaderText = "Name";
                dataGridViewDeadMembers.Columns[5].Visible = false;
                dataGridViewDeadMembers.Columns[6].Visible = false;
                dataGridViewDeadMembers.Columns[7].Visible = false;
                dataGridViewDeadMembers.Columns[8].Visible = false;
                dataGridViewDeadMembers.Columns[9].Visible = false;
                dataGridViewDeadMembers.Columns[10].Visible = false;
                dataGridViewDeadMembers.Columns[11].Visible = false;
                dataGridViewDeadMembers.Columns[12].Visible = false;
                dataGridViewDeadMembers.Columns[13].HeaderText = "Nationality";
                dataGridViewDeadMembers.Columns[14].Visible = false;
                dataGridViewDeadMembers.Columns[15].HeaderText = "Profession";
                dataGridViewDeadMembers.Columns[16].Visible = false;
                dataGridViewDeadMembers.Columns[17].HeaderText = "Position";
                dataGridViewDeadMembers.Columns[18].Visible = false;
                dataGridViewDeadMembers.Columns[19].HeaderText = "Gender";
                dataGridViewDeadMembers.Columns[19].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewDeadMembers.Columns[20].Visible = false;
                dataGridViewDeadMembers.Columns[21].Visible = false;
                dataGridViewDeadMembers.Columns[22].Visible = false;
                dataGridViewDeadMembers.Columns[23].Visible = false;
                dataGridViewDeadMembers.Columns[24].Visible = false;
                dataGridViewDeadMembers.Columns[25].Visible = false;
                dataGridViewDeadMembers.Columns[26].Visible = false;
                dataGridViewDeadMembers.Columns[27].Visible = false;
                dataGridViewDeadMembers.Columns[28].Visible = false;
                dataGridViewDeadMembers.Columns[29].Visible = false;
                dataGridViewDeadMembers.Columns[30].Visible = false;
                dataGridViewDeadMembers.Columns[31].Visible = false;
                dataGridViewDeadMembers.Columns[32].Visible = false;
                dataGridViewDeadMembers.Columns[33].Visible = false;
                dataGridViewDeadMembers.Columns[34].Visible = false;
                dataGridViewDeadMembers.Columns[35].Visible = false;
                dataGridViewDeadMembers.Columns[36].Visible = false;
                dataGridViewDeadMembers.Columns[37].HeaderText = "Died on";
                dataGridViewDeadMembers.Columns[37].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewDeadMembers.Columns[38].HeaderText = "Aged";
                dataGridViewDeadMembers.Columns[38].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewDeadMembers.Columns[39].Visible = false;
                dataGridViewDeadMembers.Columns[40].Visible = false;
                dataGridViewDeadMembers.Columns[41].Visible = false;
                dataGridViewDeadMembers.Columns[42].Visible = false;
                foreach (DataGridViewColumn column in dataGridViewDeadMembers.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
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

                // Inactive Members
                #region
                dataGridViewInactiveMembers.DataSource = inactiveMembersDTO.Members;
                dataGridViewInactiveMembers.Columns[0].Visible = false;
                dataGridViewInactiveMembers.Columns[1].Visible = false;
                dataGridViewInactiveMembers.Columns[2].Visible = false;
                dataGridViewInactiveMembers.Columns[3].HeaderText = "Surname";
                dataGridViewInactiveMembers.Columns[4].HeaderText = "Name";
                dataGridViewInactiveMembers.Columns[5].Visible = false;
                dataGridViewInactiveMembers.Columns[6].Visible = false;
                dataGridViewInactiveMembers.Columns[7].Visible = false;
                dataGridViewInactiveMembers.Columns[8].Visible = false;
                dataGridViewInactiveMembers.Columns[9].Visible = false;
                dataGridViewInactiveMembers.Columns[10].Visible = false;
                dataGridViewInactiveMembers.Columns[11].Visible = false;
                dataGridViewInactiveMembers.Columns[12].Visible = false;
                dataGridViewInactiveMembers.Columns[13].HeaderText = "Nationality";
                dataGridViewInactiveMembers.Columns[14].Visible = false;
                dataGridViewInactiveMembers.Columns[15].HeaderText = "Profession";
                dataGridViewInactiveMembers.Columns[16].Visible = false;
                dataGridViewInactiveMembers.Columns[17].HeaderText = "Position";
                dataGridViewInactiveMembers.Columns[18].Visible = false;
                dataGridViewInactiveMembers.Columns[19].HeaderText = "Gender";
                dataGridViewInactiveMembers.Columns[20].Visible = false;
                dataGridViewInactiveMembers.Columns[21].Visible = false;
                dataGridViewInactiveMembers.Columns[22].Visible = false;
                dataGridViewInactiveMembers.Columns[23].Visible = false;
                dataGridViewInactiveMembers.Columns[24].Visible = false;
                dataGridViewInactiveMembers.Columns[25].Visible = false;
                dataGridViewInactiveMembers.Columns[26].Visible = false;
                dataGridViewInactiveMembers.Columns[27].Visible = false;
                dataGridViewInactiveMembers.Columns[28].Visible = false;
                dataGridViewInactiveMembers.Columns[29].Visible = false;
                dataGridViewInactiveMembers.Columns[30].Visible = false;
                dataGridViewInactiveMembers.Columns[31].Visible = false;
                dataGridViewInactiveMembers.Columns[32].Visible = false;
                dataGridViewInactiveMembers.Columns[33].Visible = false;
                dataGridViewInactiveMembers.Columns[34].Visible = false;
                dataGridViewInactiveMembers.Columns[35].Visible = false;
                dataGridViewInactiveMembers.Columns[36].Visible = false;
                dataGridViewInactiveMembers.Columns[37].Visible = false;
                dataGridViewInactiveMembers.Columns[38].Visible = false;
                dataGridViewInactiveMembers.Columns[39].Visible = false;
                dataGridViewInactiveMembers.Columns[40].Visible = false;
                dataGridViewInactiveMembers.Columns[41].Visible = false;
                dataGridViewInactiveMembers.Columns[42].Visible = false;
                foreach (DataGridViewColumn column in dataGridViewInactiveMembers.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
                #endregion


            #endregion

            GetCounts();
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

            txtNameChildren.Clear();
            txtSurnameChildren.Clear();
            txtFathersNameChildren.Clear();
            txtMothersNameChildren.Clear();
            cmbGenderChildren.SelectedIndex = -1;
            cmbNationalityChildren.SelectedIndex = -1;
            childrenBLL = new ChildBLL();
            childrenDTO = childrenBLL.Select();
            dataGridViewChildren.DataSource = childrenDTO.Children;

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

            labelMaleChildren.Text = childrenBLL.SelectAllMaleChildren().ToString();
            labelFemaleChildren.Text = childrenBLL.SelectAllFemaleChildren().ToString();

            labelTotalContacts.Text = "Total: " + dataGridViewContacts.RowCount.ToString();

            labelNoOfMenFormerMembers.Text = formerMembersBLL.SelectCountFormerMale().ToString();
            labelNoOfFemaleFormerMembers.Text = formerMembersBLL.SelectCountFormerFemale().ToString();
            labelNoOfDivisorFormerMembers.Text = formerMembersBLL.SelectCountFormerDivisor().ToString();

            labelNoOfMenDeadMembers.Text = deadMembersBLL.SelectCountDeadMale().ToString();
            labelNoOfFemaleDeadMembers.Text = deadMembersBLL.SelectCountDeadFemale().ToString();
            labelNoOfDivisorDeadMembers.Text = deadMembersBLL.SelectCountDeadDivisor().ToString();

            labelTotalRowsCommittment.Text = "Row : " + dataGridViewCommitments.RowCount.ToString();
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
            if (registeredMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormViewMember open = new FormViewMember();
                open.detail = registeredMembersDetail;
                open.isView = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
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
            registeredMembersDetail = new MemberDetailDTO();
            registeredMembersDetail.MemberID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[0].Value);
            registeredMembersDetail.Username = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[1].Value.ToString();
            registeredMembersDetail.Password = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[2].Value.ToString();
            registeredMembersDetail.Surname = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[3].Value.ToString();
            registeredMembersDetail.Name = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[4].Value.ToString();
            registeredMembersDetail.Birthday = Convert.ToDateTime(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[5].Value);
            registeredMembersDetail.ImagePath = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[6].Value.ToString();
            registeredMembersDetail.EmailAddress = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[7].Value.ToString();
            registeredMembersDetail.HouseAddress = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[8].Value.ToString();
            registeredMembersDetail.MembershipDate = Convert.ToDateTime(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[9].Value);
            registeredMembersDetail.CountryID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[10].Value);
            registeredMembersDetail.CountryName = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[11].Value.ToString();
            registeredMembersDetail.NationalityID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[12].Value);
            registeredMembersDetail.NationalityName = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[13].Value.ToString();
            registeredMembersDetail.ProfessionID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[14].Value);
            registeredMembersDetail.ProfessionName = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[15].Value.ToString();
            registeredMembersDetail.PositionID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[16].Value);
            registeredMembersDetail.PositionName = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[17].Value.ToString();
            registeredMembersDetail.GenderID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[18].Value);
            registeredMembersDetail.GenderName = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[19].Value.ToString();
            registeredMembersDetail.EmploymentStatusID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[20].Value);
            registeredMembersDetail.EmploymentStatusName = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[21].Value.ToString();
            registeredMembersDetail.MaritalStatusID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[22].Value);
            registeredMembersDetail.MaritalStatusName = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[23].Value.ToString();
            registeredMembersDetail.PermissionID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[24].Value);
            registeredMembersDetail.PermissionName = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[25].Value.ToString();
            registeredMembersDetail.PhoneNumber = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[26].Value.ToString();
            registeredMembersDetail.PhoneNumber2 = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[27].Value.ToString();
            registeredMembersDetail.PhoneNumber3 = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[28].Value.ToString();
            registeredMembersDetail.isCountryDeleted = Convert.ToBoolean(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[29].Value);
            registeredMembersDetail.isNationalityDeleted = Convert.ToBoolean(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[30].Value);
            registeredMembersDetail.isProfessionDeleted = Convert.ToBoolean(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[31].Value);
            registeredMembersDetail.isPositionDeleted = Convert.ToBoolean(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[32].Value);
            registeredMembersDetail.isEmpStatusDeleted = Convert.ToBoolean(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[33].Value);
            registeredMembersDetail.isMarStatusDeleted = Convert.ToBoolean(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[34].Value);
            registeredMembersDetail.MembershipStatusID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[35].Value);
            registeredMembersDetail.MembershipStatus = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[36].Value.ToString();
            registeredMembersDetail.DeadDate = Convert.ToDateTime(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[37].Value);
            registeredMembersDetail.DeadAge = Convert.ToDouble(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[38].Value);
            registeredMembersDetail.LGA = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[39].Value.ToString();
            registeredMembersDetail.NameOfNextOfKin = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[40].Value.ToString();
            registeredMembersDetail.RelationshipToKinID = Convert.ToInt32(dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[41].Value);
            registeredMembersDetail.RelationshipToKin = dataGridViewRegisteredMembers.Rows[e.RowIndex].Cells[42].Value.ToString();

            string imagePath = Application.StartupPath + "\\images\\" + registeredMembersDetail.ImagePath;
            picRegisteredMember.ImageLocation = imagePath;
        }

        ChildBLL childrenBLL = new ChildBLL();
        ChildDTO childrenDTO = new ChildDTO();
        ChildDetailDTO childrenDetail = new ChildDetailDTO();
        private void btnAddChildren_Click(object sender, EventArgs e)
        {
            FormChildren open = new FormChildren();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnViewChildren_Click(object sender, EventArgs e)
        {
            if (childrenDetail.ChildID == 0)
            {
                MessageBox.Show("Please choose a child from the table");
            }
            else
            {
                FormViewChild open = new FormViewChild();
                open.detail = childrenDetail;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtNameChildren_TextChanged(object sender, EventArgs e)
        {
            List<ChildDetailDTO> list = childrenDTO.Children;
            list = list.Where(x => x.Name.Contains(txtNameChildren.Text)).ToList();
            dataGridViewChildren.DataSource = list;
        }

        private void txtSurnameChildren_TextChanged(object sender, EventArgs e)
        {
            List<ChildDetailDTO> list = childrenDTO.Children;
            list = list.Where(x => x.Surname.Contains(txtSurnameChildren.Text)).ToList();
            dataGridViewChildren.DataSource = list;
        }

        private void txtMothersNameChildren_TextChanged(object sender, EventArgs e)
        {
            List<ChildDetailDTO> list = childrenDTO.Children;
            list = list.Where(x => x.MothersName.Contains(txtMothersNameChildren.Text)).ToList();
            dataGridViewChildren.DataSource = list;
        }

        private void txtFathersNameChildren_TextChanged(object sender, EventArgs e)
        {
            List<ChildDetailDTO> list = childrenDTO.Children;
            list = list.Where(x => x.FathersName.Contains(txtFathersNameChildren.Text)).ToList();
            dataGridViewChildren.DataSource = list;
        }

        private void btnSearchChildren_Click(object sender, EventArgs e)
        {
            List<ChildDetailDTO> list = childrenDTO.Children;
            if (cmbGenderChildren.SelectedIndex != -1)
            {
                list = list.Where(x => x.GenderID == Convert.ToInt32(cmbGenderChildren.SelectedValue)).ToList();
            }
            if (cmbNationalityChildren.SelectedIndex != -1)
            {
                list = list.Where(x => x.NationalityID == Convert.ToInt32(cmbNationalityChildren.SelectedValue)).ToList();
            }
            dataGridViewChildren.DataSource = list;
        }

        private void btnDeleteChildren_Click(object sender, EventArgs e)
        {
            if (childrenDetail.ChildID == 0)
            {
                MessageBox.Show("Please select a child from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (childrenBLL.Delete(childrenDetail))
                    {
                        MessageBox.Show("Child was deleted");                        
                        ClearFilters();
                    }
                }
            }
        }

        private void dataGridViewChildren_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            childrenDetail = new ChildDetailDTO();
            childrenDetail.ChildID = Convert.ToInt32(dataGridViewChildren.Rows[e.RowIndex].Cells[0].Value);
            childrenDetail.Surname = dataGridViewChildren.Rows[e.RowIndex].Cells[1].Value.ToString();
            childrenDetail.Name = dataGridViewChildren.Rows[e.RowIndex].Cells[2].Value.ToString();
            childrenDetail.Birthday = Convert.ToDateTime(dataGridViewChildren.Rows[e.RowIndex].Cells[3].Value);
            childrenDetail.ImagePath = dataGridViewChildren.Rows[e.RowIndex].Cells[4].Value.ToString();
            childrenDetail.GenderID = Convert.ToInt32(dataGridViewChildren.Rows[e.RowIndex].Cells[5].Value);
            childrenDetail.GenderName = dataGridViewChildren.Rows[e.RowIndex].Cells[6].Value.ToString();
            childrenDetail.NationalityID = Convert.ToInt32(dataGridViewChildren.Rows[e.RowIndex].Cells[7].Value);
            childrenDetail.NationalityName = dataGridViewChildren.Rows[e.RowIndex].Cells[8].Value.ToString();
            childrenDetail.MotherID = Convert.ToInt32(dataGridViewChildren.Rows[e.RowIndex].Cells[9].Value);
            childrenDetail.MothersName = dataGridViewChildren.Rows[e.RowIndex].Cells[10].Value.ToString();
            childrenDetail.MothersSurname = dataGridViewChildren.Rows[e.RowIndex].Cells[11].Value.ToString();
            childrenDetail.MotherNationalityID = Convert.ToInt32(dataGridViewChildren.Rows[e.RowIndex].Cells[12].Value);
            childrenDetail.MotherNationalityName = dataGridViewChildren.Rows[e.RowIndex].Cells[13].Value.ToString();
            childrenDetail.MotherImagePath = dataGridViewChildren.Rows[e.RowIndex].Cells[14].Value.ToString();
            childrenDetail.FatherID = Convert.ToInt32(dataGridViewChildren.Rows[e.RowIndex].Cells[15].Value);
            childrenDetail.FathersName = dataGridViewChildren.Rows[e.RowIndex].Cells[16].Value.ToString();
            childrenDetail.FathersSurname = dataGridViewChildren.Rows[e.RowIndex].Cells[17].Value.ToString();
            childrenDetail.FatherNationalityID = Convert.ToInt32(dataGridViewChildren.Rows[e.RowIndex].Cells[18].Value);
            childrenDetail.FatherNationalityName = dataGridViewChildren.Rows[e.RowIndex].Cells[19].Value.ToString();
            childrenDetail.FatherImagePath = dataGridViewChildren.Rows[e.RowIndex].Cells[20].Value.ToString();
            childrenDetail.isNationalityDeleted = Convert.ToBoolean(dataGridViewChildren.Rows[e.RowIndex].Cells[21].Value);
        }

        private void btnUpdateChildren_Click(object sender, EventArgs e)
        {
            if (childrenDetail.ChildID == 0)
            {
                MessageBox.Show("Please choose a child from the table");
            }
            else
            {
                FormChildren open = new FormChildren();
                open.detail = childrenDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
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
            contactsDetail = new MemberDetailDTO();
            contactsDetail.MemberID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[0].Value);
            contactsDetail.Username = dataGridViewContacts.Rows[e.RowIndex].Cells[1].Value.ToString();
            contactsDetail.Password = dataGridViewContacts.Rows[e.RowIndex].Cells[2].Value.ToString();
            contactsDetail.Surname = dataGridViewContacts.Rows[e.RowIndex].Cells[3].Value.ToString();
            contactsDetail.Name = dataGridViewContacts.Rows[e.RowIndex].Cells[4].Value.ToString();
            contactsDetail.Birthday = Convert.ToDateTime(dataGridViewContacts.Rows[e.RowIndex].Cells[5].Value);
            contactsDetail.ImagePath = dataGridViewContacts.Rows[e.RowIndex].Cells[6].Value.ToString();
            contactsDetail.EmailAddress = dataGridViewContacts.Rows[e.RowIndex].Cells[7].Value.ToString();
            contactsDetail.HouseAddress = dataGridViewContacts.Rows[e.RowIndex].Cells[8].Value.ToString();
            contactsDetail.MembershipDate = Convert.ToDateTime(dataGridViewContacts.Rows[e.RowIndex].Cells[9].Value);
            contactsDetail.CountryID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[10].Value);
            contactsDetail.CountryName = dataGridViewContacts.Rows[e.RowIndex].Cells[11].Value.ToString();
            contactsDetail.NationalityID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[12].Value);
            contactsDetail.NationalityName = dataGridViewContacts.Rows[e.RowIndex].Cells[13].Value.ToString();
            contactsDetail.ProfessionID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[14].Value);
            contactsDetail.ProfessionName = dataGridViewContacts.Rows[e.RowIndex].Cells[15].Value.ToString();
            contactsDetail.PositionID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[16].Value);
            contactsDetail.PositionName = dataGridViewContacts.Rows[e.RowIndex].Cells[17].Value.ToString();
            contactsDetail.GenderID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[18].Value);
            contactsDetail.GenderName = dataGridViewContacts.Rows[e.RowIndex].Cells[19].Value.ToString();
            contactsDetail.EmploymentStatusID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[20].Value);
            contactsDetail.EmploymentStatusName = dataGridViewContacts.Rows[e.RowIndex].Cells[21].Value.ToString();
            contactsDetail.MaritalStatusID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[22].Value);
            contactsDetail.MaritalStatusName = dataGridViewContacts.Rows[e.RowIndex].Cells[23].Value.ToString();
            contactsDetail.PermissionID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[24].Value);
            contactsDetail.PermissionName = dataGridViewContacts.Rows[e.RowIndex].Cells[25].Value.ToString();
            contactsDetail.PhoneNumber = dataGridViewContacts.Rows[e.RowIndex].Cells[26].Value.ToString();
            contactsDetail.PhoneNumber2 = dataGridViewContacts.Rows[e.RowIndex].Cells[27].Value.ToString();
            contactsDetail.PhoneNumber3 = dataGridViewContacts.Rows[e.RowIndex].Cells[28].Value.ToString();
            contactsDetail.isCountryDeleted = Convert.ToBoolean(dataGridViewContacts.Rows[e.RowIndex].Cells[29].Value);
            contactsDetail.isNationalityDeleted = Convert.ToBoolean(dataGridViewContacts.Rows[e.RowIndex].Cells[30].Value);
            contactsDetail.isProfessionDeleted = Convert.ToBoolean(dataGridViewContacts.Rows[e.RowIndex].Cells[31].Value);
            contactsDetail.isPositionDeleted = Convert.ToBoolean(dataGridViewContacts.Rows[e.RowIndex].Cells[32].Value);
            contactsDetail.isEmpStatusDeleted = Convert.ToBoolean(dataGridViewContacts.Rows[e.RowIndex].Cells[33].Value);
            contactsDetail.isMarStatusDeleted = Convert.ToBoolean(dataGridViewContacts.Rows[e.RowIndex].Cells[34].Value);
            contactsDetail.MembershipStatusID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[35].Value);
            contactsDetail.MembershipStatus = dataGridViewContacts.Rows[e.RowIndex].Cells[36].Value.ToString();
            contactsDetail.DeadDate = Convert.ToDateTime(dataGridViewContacts.Rows[e.RowIndex].Cells[37].Value);
            contactsDetail.DeadAge = Convert.ToDouble(dataGridViewContacts.Rows[e.RowIndex].Cells[38].Value);
            contactsDetail.LGA = dataGridViewContacts.Rows[e.RowIndex].Cells[39].Value.ToString();
            contactsDetail.NameOfNextOfKin = dataGridViewContacts.Rows[e.RowIndex].Cells[40].Value.ToString();
            contactsDetail.RelationshipToKinID = Convert.ToInt32(dataGridViewContacts.Rows[e.RowIndex].Cells[41].Value);
            contactsDetail.RelationshipToKin = dataGridViewContacts.Rows[e.RowIndex].Cells[42].Value.ToString();
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
            if (formerMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormViewMember open = new FormViewMember();
                open.detail = formerMembersDetail;
                open.isView = true;
                open.isFormer = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;                
                ClearFilters();
            }
        }

        private void dataGridViewFormerMembers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            formerMembersDetail = new MemberDetailDTO();
            formerMembersDetail.MemberID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[0].Value);
            formerMembersDetail.Username = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[1].Value.ToString();
            formerMembersDetail.Password = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[2].Value.ToString();
            formerMembersDetail.Surname = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[3].Value.ToString();
            formerMembersDetail.Name = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[4].Value.ToString();
            formerMembersDetail.Birthday = Convert.ToDateTime(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[5].Value);
            formerMembersDetail.ImagePath = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[6].Value.ToString();
            formerMembersDetail.EmailAddress = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[7].Value.ToString();
            formerMembersDetail.HouseAddress = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[8].Value.ToString();
            formerMembersDetail.MembershipDate = Convert.ToDateTime(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[9].Value);
            formerMembersDetail.CountryID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[10].Value);
            formerMembersDetail.CountryName = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[11].Value.ToString();
            formerMembersDetail.NationalityID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[12].Value);
            formerMembersDetail.NationalityName = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[13].Value.ToString();
            formerMembersDetail.ProfessionID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[14].Value);
            formerMembersDetail.ProfessionName = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[15].Value.ToString();
            formerMembersDetail.PositionID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[16].Value);
            formerMembersDetail.PositionName = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[17].Value.ToString();
            formerMembersDetail.GenderID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[18].Value);
            formerMembersDetail.GenderName = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[19].Value.ToString();
            formerMembersDetail.EmploymentStatusID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[20].Value);
            formerMembersDetail.EmploymentStatusName = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[21].Value.ToString();
            formerMembersDetail.MaritalStatusID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[22].Value);
            formerMembersDetail.MaritalStatusName = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[23].Value.ToString();
            formerMembersDetail.PermissionID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[24].Value);
            formerMembersDetail.PermissionName = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[25].Value.ToString();
            formerMembersDetail.PhoneNumber = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[26].Value.ToString();
            formerMembersDetail.PhoneNumber2 = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[27].Value.ToString();
            formerMembersDetail.PhoneNumber3 = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[28].Value.ToString();
            formerMembersDetail.isCountryDeleted = Convert.ToBoolean(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[29].Value);
            formerMembersDetail.isNationalityDeleted = Convert.ToBoolean(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[30].Value);
            formerMembersDetail.isProfessionDeleted = Convert.ToBoolean(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[31].Value);
            formerMembersDetail.isPositionDeleted = Convert.ToBoolean(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[32].Value);
            formerMembersDetail.isEmpStatusDeleted = Convert.ToBoolean(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[33].Value);
            formerMembersDetail.isMarStatusDeleted = Convert.ToBoolean(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[34].Value);
            formerMembersDetail.MembershipStatusID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[35].Value);
            formerMembersDetail.MembershipStatus = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[36].Value.ToString();
            formerMembersDetail.DeadDate = Convert.ToDateTime(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[37].Value);
            formerMembersDetail.DeadAge = Convert.ToDouble(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[38].Value);
            formerMembersDetail.LGA = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[39].Value.ToString();
            formerMembersDetail.NameOfNextOfKin = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[40].Value.ToString();
            formerMembersDetail.RelationshipToKinID = Convert.ToInt32(dataGridViewFormerMembers.Rows[e.RowIndex].Cells[41].Value);
            formerMembersDetail.RelationshipToKin = dataGridViewFormerMembers.Rows[e.RowIndex].Cells[42].Value.ToString();
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
            deadMembersDetail = new MemberDetailDTO();
            deadMembersDetail.MemberID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[0].Value);
            deadMembersDetail.Username = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[1].Value.ToString();
            deadMembersDetail.Password = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[2].Value.ToString();
            deadMembersDetail.Surname = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[3].Value.ToString();
            deadMembersDetail.Name = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[4].Value.ToString();
            deadMembersDetail.Birthday = Convert.ToDateTime(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[5].Value);
            deadMembersDetail.ImagePath = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[6].Value.ToString();
            deadMembersDetail.EmailAddress = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[7].Value.ToString();
            deadMembersDetail.HouseAddress = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[8].Value.ToString();
            deadMembersDetail.MembershipDate = Convert.ToDateTime(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[9].Value);
            deadMembersDetail.CountryID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[10].Value);
            deadMembersDetail.CountryName = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[11].Value.ToString();
            deadMembersDetail.NationalityID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[12].Value);
            deadMembersDetail.NationalityName = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[13].Value.ToString();
            deadMembersDetail.ProfessionID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[14].Value);
            deadMembersDetail.ProfessionName = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[15].Value.ToString();
            deadMembersDetail.PositionID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[16].Value);
            deadMembersDetail.PositionName = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[17].Value.ToString();
            deadMembersDetail.GenderID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[18].Value);
            deadMembersDetail.GenderName = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[19].Value.ToString();
            deadMembersDetail.EmploymentStatusID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[20].Value);
            deadMembersDetail.EmploymentStatusName = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[21].Value.ToString();
            deadMembersDetail.MaritalStatusID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[22].Value);
            deadMembersDetail.MaritalStatusName = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[23].Value.ToString();
            deadMembersDetail.PermissionID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[24].Value);
            deadMembersDetail.PermissionName = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[25].Value.ToString();
            deadMembersDetail.PhoneNumber = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[26].Value.ToString();
            deadMembersDetail.PhoneNumber2 = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[27].Value.ToString();
            deadMembersDetail.PhoneNumber3 = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[28].Value.ToString();
            deadMembersDetail.isCountryDeleted = Convert.ToBoolean(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[29].Value);
            deadMembersDetail.isNationalityDeleted = Convert.ToBoolean(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[30].Value);
            deadMembersDetail.isProfessionDeleted = Convert.ToBoolean(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[31].Value);
            deadMembersDetail.isPositionDeleted = Convert.ToBoolean(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[32].Value);
            deadMembersDetail.isEmpStatusDeleted = Convert.ToBoolean(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[33].Value);
            deadMembersDetail.isMarStatusDeleted = Convert.ToBoolean(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[34].Value);
            deadMembersDetail.MembershipStatusID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[35].Value);
            deadMembersDetail.MembershipStatus = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[36].Value.ToString();
            deadMembersDetail.DeadDate = Convert.ToDateTime(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[37].Value);
            deadMembersDetail.DeadAge = Convert.ToDouble(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[38].Value);
            deadMembersDetail.LGA = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[39].Value.ToString();
            deadMembersDetail.NameOfNextOfKin = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[40].Value.ToString();
            deadMembersDetail.RelationshipToKinID = Convert.ToInt32(dataGridViewDeadMembers.Rows[e.RowIndex].Cells[41].Value);
            deadMembersDetail.RelationshipToKin = dataGridViewDeadMembers.Rows[e.RowIndex].Cells[42].Value.ToString();
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
            inactiveMembersDetail = new MemberDetailDTO();
            inactiveMembersDetail.MemberID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[0].Value);
            inactiveMembersDetail.Username = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[1].Value.ToString();
            inactiveMembersDetail.Password = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[2].Value.ToString();
            inactiveMembersDetail.Surname = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[3].Value.ToString();
            inactiveMembersDetail.Name = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[4].Value.ToString();
            inactiveMembersDetail.Birthday = Convert.ToDateTime(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[5].Value);
            inactiveMembersDetail.ImagePath = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[6].Value.ToString();
            inactiveMembersDetail.EmailAddress = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[7].Value.ToString();
            inactiveMembersDetail.HouseAddress = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[8].Value.ToString();
            inactiveMembersDetail.MembershipDate = Convert.ToDateTime(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[9].Value);
            inactiveMembersDetail.CountryID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[10].Value);
            inactiveMembersDetail.CountryName = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[11].Value.ToString();
            inactiveMembersDetail.NationalityID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[12].Value);
            inactiveMembersDetail.NationalityName = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[13].Value.ToString();
            inactiveMembersDetail.ProfessionID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[14].Value);
            inactiveMembersDetail.ProfessionName = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[15].Value.ToString();
            inactiveMembersDetail.PositionID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[16].Value);
            inactiveMembersDetail.PositionName = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[17].Value.ToString();
            inactiveMembersDetail.GenderID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[18].Value);
            inactiveMembersDetail.GenderName = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[19].Value.ToString();
            inactiveMembersDetail.EmploymentStatusID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[20].Value);
            inactiveMembersDetail.EmploymentStatusName = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[21].Value.ToString();
            inactiveMembersDetail.MaritalStatusID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[22].Value);
            inactiveMembersDetail.MaritalStatusName = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[23].Value.ToString();
            inactiveMembersDetail.PermissionID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[24].Value);
            inactiveMembersDetail.PermissionName = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[25].Value.ToString();
            inactiveMembersDetail.PhoneNumber = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[26].Value.ToString();
            inactiveMembersDetail.PhoneNumber2 = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[27].Value.ToString();
            inactiveMembersDetail.PhoneNumber3 = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[28].Value.ToString();
            inactiveMembersDetail.isCountryDeleted = Convert.ToBoolean(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[29].Value);
            inactiveMembersDetail.isNationalityDeleted = Convert.ToBoolean(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[30].Value);
            inactiveMembersDetail.isProfessionDeleted = Convert.ToBoolean(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[31].Value);
            inactiveMembersDetail.isPositionDeleted = Convert.ToBoolean(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[32].Value);
            inactiveMembersDetail.isEmpStatusDeleted = Convert.ToBoolean(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[33].Value);
            inactiveMembersDetail.isMarStatusDeleted = Convert.ToBoolean(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[34].Value);
            inactiveMembersDetail.MembershipStatusID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[35].Value);
            inactiveMembersDetail.MembershipStatus = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[36].Value.ToString();
            inactiveMembersDetail.DeadDate = Convert.ToDateTime(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[37].Value);
            inactiveMembersDetail.DeadAge = Convert.ToDouble(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[38].Value);
            inactiveMembersDetail.LGA = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[39].Value.ToString();
            inactiveMembersDetail.NameOfNextOfKin = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[40].Value.ToString();
            inactiveMembersDetail.RelationshipToKinID = Convert.ToInt32(dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[41].Value);
            inactiveMembersDetail.RelationshipToKin = dataGridViewInactiveMembers.Rows[e.RowIndex].Cells[42].Value.ToString();
        }

        private void btnViewInactiveMembers_Click(object sender, EventArgs e)
        {
            if (inactiveMembersDetail.MemberID == 0)
            {
                MessageBox.Show("Please choose a member from the table");
            }
            else
            {
                FormViewMember open = new FormViewMember();
                open.detail = inactiveMembersDetail;
                open.isView = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
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
    }
}