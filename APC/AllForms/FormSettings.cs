using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Drawing.Drawing2D;
using FontAwesome.Sharp;

namespace APC.AllForms
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }
        MemberBLL memberBLL = new MemberBLL();        
        EventsBLL eventBLL = new EventsBLL();
        private void FormSettings_Load(object sender, EventArgs e)
        {
            //Button label
            #region
            label4.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label10.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label12.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            label16.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalCountry.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtCountry.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnAddCountry.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateCountry.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteCountry.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label2.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalEmpStatus.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtEmpStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnAddEmpStatus.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateEmpStatus.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteEmpStatus.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label3.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalMarStatus.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtMaritalStatus.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnAddMarStatus.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateMarStatus.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteMarStatus.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label5.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalNationality.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtNationality.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnAddNationality.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateNationality.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteNationality.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label7.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalPermissions.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            cmbPermission.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnDeletePermissions.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnSearchPermissions.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnClearPermissions.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label8.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalPositions.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtPosition.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnAddPositions.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdatePositions.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeletePositions.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            label9.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            labelTotalProfessions.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            txtProfession.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnAddProfessions.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnUpdateProfessions.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnDeleteProfessions.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            labelName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelSurname.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelAccessLevel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelPosition.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label13.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label15.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label14.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label17.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            #endregion

            #region
            countryDTO = countryBLL.Select();
            dataGridViewCountry.DataSource = countryDTO.Countries;
            dataGridViewCountry.Columns[0].Visible = false;
            dataGridViewCountry.Columns[1].HeaderText = "Country Name";
            foreach (DataGridViewColumn column in dataGridViewCountry.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            empStatusDTO = empStatusBLL.Select();
            dataGridViewEmpStatus.DataSource = empStatusDTO.EmploymentStatuses;
            dataGridViewEmpStatus.Columns[0].Visible = false;
            dataGridViewEmpStatus.Columns[1].HeaderText = "Employment Status";
            foreach (DataGridViewColumn column in dataGridViewEmpStatus.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            marStatusDTO = marStatusBLL.Select();
            dataGridViewMarStatus.DataSource = marStatusDTO.MaritalStatuses;
            dataGridViewMarStatus.Columns[0].Visible = false;
            dataGridViewMarStatus.Columns[1].HeaderText = "Marital Status Name";
            foreach (DataGridViewColumn column in dataGridViewMarStatus.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            nationalityDTO = nationalityBLL.Select();
            dataGridViewNationality.DataSource = nationalityDTO.Nationalities;
            dataGridViewNationality.Columns[0].Visible = false;
            dataGridViewNationality.Columns[1].HeaderText = "Nationality Name";
            foreach (DataGridViewColumn column in dataGridViewNationality.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            permissionDTO = permissionBLL.Select();
            cmbPermission.DataSource = permissionDTO.Permissions;
            GeneralHelper.ComboBoxProps(cmbPermission, "Permission", "PermissionID");

            dataGridViewPermissions.DataSource = permissionDTO.Members;
            dataGridViewPermissions.Columns[0].Visible = false;
            dataGridViewPermissions.Columns[1].Visible = false;
            dataGridViewPermissions.Columns[2].Visible = false;
            dataGridViewPermissions.Columns[3].HeaderText = "Surname";
            dataGridViewPermissions.Columns[4].HeaderText = "Name";
            dataGridViewPermissions.Columns[5].Visible = false;
            dataGridViewPermissions.Columns[6].Visible = false;
            dataGridViewPermissions.Columns[7].Visible = false;
            dataGridViewPermissions.Columns[8].Visible = false;
            dataGridViewPermissions.Columns[9].Visible = false;
            dataGridViewPermissions.Columns[10].Visible = false;
            dataGridViewPermissions.Columns[11].Visible = false;
            dataGridViewPermissions.Columns[12].Visible = false;
            dataGridViewPermissions.Columns[13].HeaderText = "Nationality";
            dataGridViewPermissions.Columns[14].Visible = false;
            dataGridViewPermissions.Columns[15].Visible = false;
            dataGridViewPermissions.Columns[16].Visible = false;
            dataGridViewPermissions.Columns[17].HeaderText = "Position";
            dataGridViewPermissions.Columns[18].Visible = false;
            dataGridViewPermissions.Columns[19].Visible = false;
            dataGridViewPermissions.Columns[20].Visible = false;
            dataGridViewPermissions.Columns[21].Visible = false;
            dataGridViewPermissions.Columns[22].Visible = false;
            dataGridViewPermissions.Columns[23].Visible = false;
            dataGridViewPermissions.Columns[24].Visible = false;
            dataGridViewPermissions.Columns[25].HeaderText = "Access Level";
            dataGridViewPermissions.Columns[26].Visible = false;
            dataGridViewPermissions.Columns[27].Visible = false;
            dataGridViewPermissions.Columns[28].Visible = false;
            dataGridViewPermissions.Columns[29].Visible = false;
            dataGridViewPermissions.Columns[30].Visible = false;
            dataGridViewPermissions.Columns[31].Visible = false;
            dataGridViewPermissions.Columns[32].Visible = false;
            dataGridViewPermissions.Columns[33].Visible = false;
            dataGridViewPermissions.Columns[34].Visible = false;
            dataGridViewPermissions.Columns[35].Visible = false;
            dataGridViewPermissions.Columns[36].Visible = false;
            dataGridViewPermissions.Columns[37].Visible = false;
            dataGridViewPermissions.Columns[38].Visible = false;
            dataGridViewPermissions.Columns[39].Visible = false;
            dataGridViewPermissions.Columns[40].Visible = false;
            dataGridViewPermissions.Columns[41].Visible = false;
            dataGridViewPermissions.Columns[42].Visible = false;
            foreach (DataGridViewColumn column in dataGridViewPermissions.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            positionDTO = positionBLL.Select();
            dataGridViewPositions.DataSource = positionDTO.Positions;
            dataGridViewPositions.Columns[0].Visible = false;
            dataGridViewPositions.Columns[1].HeaderText = "Position Name";
            foreach (DataGridViewColumn column in dataGridViewPositions.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            professionDTO = professionBLL.Select();
            dataGridViewProfessions.DataSource = professionDTO.Professions;
            dataGridViewProfessions.Columns[0].Visible = false;
            dataGridViewProfessions.Columns[1].HeaderText = "Profession Name";
            foreach (DataGridViewColumn column in dataGridViewProfessions.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }

            picProfilePic.SizeMode = PictureBoxSizeMode.StretchImage;
            picProfilePic.BorderStyle = BorderStyle.None;
            picProfilePic.Width = picProfilePic.Height = 40;
            picProfilePic.Paint += new PaintEventHandler(picProfilePic_Paint);

            picProfilePic.SizeMode = PictureBoxSizeMode.StretchImage;
            picProfilePic.BorderStyle = BorderStyle.None;
            picProfilePic.Width = picProfilePic.Height = 40;
            picProfilePic.Paint += new PaintEventHandler(picProfilePic_Paint);

            MemberDTO memberDTO = memberBLL.Select();
            MemberDetailDTO detail = memberDTO.Members.First(x => x.MemberID == LoginInfo.MemberID);
            string imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
            picProfilePic.ImageLocation = imagePath;
            labelName.Text = detail.Name;
            labelSurname.Text = detail.Surname;
            labelAccessLevel.Text = detail.PermissionName;
            labelPosition.Text = detail.PositionName;
            #endregion

            // Deleted Data
            #region
            label11.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            cmbDeletedData.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnRetrieve.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            cmbDeletedData.Items.Add("Registered Members");
            cmbDeletedData.Items.Add("Countries");
            cmbDeletedData.Items.Add("Nationalities");
            cmbDeletedData.Items.Add("Professions");
            cmbDeletedData.Items.Add("Positions");
            cmbDeletedData.Items.Add("Employment Statuses");
            cmbDeletedData.Items.Add("Marital Statuses");
            cmbDeletedData.Items.Add("Children");
            cmbDeletedData.Items.Add("Comments");
            cmbDeletedData.Items.Add("Documents");
            cmbDeletedData.Items.Add("Event Images");
            cmbDeletedData.Items.Add("Events");
            cmbDeletedData.Items.Add("General Attendance");
            cmbDeletedData.Items.Add("Expenditure");
            cmbDeletedData.Items.Add("Financial Report");
            cmbDeletedData.Items.Add("Constitution");
            cmbDeletedData.Items.Add("Fined Member");

            memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
            dataGridView1.DataSource = memberDeletedDataDTO.Members;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].HeaderText = "Name";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].HeaderText = "Nationality";
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].HeaderText = "Profession";
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].HeaderText = "Position";
            dataGridView1.Columns[18].Visible = false;
            dataGridView1.Columns[19].HeaderText = "Gender";
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[23].Visible = false;
            dataGridView1.Columns[24].Visible = false;
            dataGridView1.Columns[25].Visible = false;
            dataGridView1.Columns[26].Visible = false;
            dataGridView1.Columns[27].Visible = false;
            dataGridView1.Columns[28].Visible = false;
            dataGridView1.Columns[29].Visible = false;
            dataGridView1.Columns[30].Visible = false;
            dataGridView1.Columns[31].Visible = false;
            dataGridView1.Columns[32].Visible = false;
            dataGridView1.Columns[33].Visible = false;
            dataGridView1.Columns[34].Visible = false;
            dataGridView1.Columns[35].Visible = false;
            dataGridView1.Columns[36].Visible = false;
            dataGridView1.Columns[37].Visible = false;
            dataGridView1.Columns[38].Visible = false;
            dataGridView1.Columns[39].Visible = false;
            dataGridView1.Columns[40].Visible = false;
            dataGridView1.Columns[41].Visible = false;
            dataGridView1.Columns[42].Visible = false;
            dataGridView1.Columns[43].Visible = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            }
            #endregion

            if (LoginInfo.AccessLevel != 4)
            {
                btnDeleteCountry.Hide();
                btnDeleteEmpStatus.Hide();
                btnDeleteMarStatus.Hide();
                btnDeleteNationality.Hide();
                btnDeletePermissions.Hide();
                btnDeletePositions.Hide();
                btnDeleteProfessions.Hide();
            }

            Counts();
        }

        private void picProfilePic_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, picProfilePic.Width - 1, picProfilePic.Height - 1);
            Region rg = new Region(gp);
            picProfilePic.Region = rg;
        }

        CountryBLL countryBLL = new CountryBLL();
        CountryDTO countryDTO = new CountryDTO();
        CountryDetailDTO countryDetail = new CountryDetailDTO();

        private void btnAddCountry_Click(object sender, EventArgs e)
        {
            FormCountry open = new FormCountry();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void ClearFilters()
        {
            txtCountry.Clear();
            countryBLL = new CountryBLL();
            countryDTO = countryBLL.Select();
            dataGridViewCountry.DataSource = countryDTO.Countries;

            txtEmpStatus.Clear();
            empStatusBLL = new EmploymentStatusBLL();
            empStatusDTO = empStatusBLL.Select();
            dataGridViewEmpStatus.DataSource = empStatusDTO.EmploymentStatuses;

            txtMaritalStatus.Clear();
            marStatusBLL = new MaritalStatusBLL();
            marStatusDTO = marStatusBLL.Select();
            dataGridViewMarStatus.DataSource = marStatusDTO.MaritalStatuses;

            txtNationality.Clear();
            nationalityBLL = new NationalityBLL();
            nationalityDTO = nationalityBLL.Select();
            dataGridViewNationality.DataSource = nationalityDTO.Nationalities;

            cmbPermission.SelectedIndex = -1;
            permissionBLL = new PermissionBLL();
            permissionDTO = permissionBLL.Select();
            dataGridViewPermissions.DataSource = permissionDTO.Members;

            txtPosition.Clear();
            positionBLL = new PositionBLL();
            positionDTO = positionBLL.Select();
            dataGridViewPositions.DataSource = positionDTO.Positions;

            txtProfession.Clear();
            professionBLL = new ProfessionBLL();
            professionDTO = professionBLL.Select();
            dataGridViewProfessions.DataSource = professionDTO.Professions;

            Counts();
        }

        private void Counts()
        {
            labelTotalCountry.Text = "Total: " + dataGridViewCountry.RowCount.ToString();
            labelTotalEmpStatus.Text = "Total: " + dataGridViewEmpStatus.RowCount.ToString();
            labelTotalMarStatus.Text = "Total: " + dataGridViewMarStatus.RowCount.ToString();
            labelTotalNationality.Text = "Total: " + dataGridViewNationality.RowCount.ToString();
            labelTotalPermissions.Text = "Total: " + dataGridViewPermissions.RowCount.ToString();
            labelTotalPositions.Text = "Total: " + dataGridViewPositions.RowCount.ToString();
            labelTotalProfessions.Text = "Total: " + dataGridViewProfessions.RowCount.ToString();

            labelTotalProfessionSettingsDashboard.Text = professionBLL.SelectUniqueProfessionCount().ToString();
            labelTotalPositionSettingsDashboard.Text = positionBLL.SelectUniquePositionCount().ToString();
            labelTotalNationalitySettingsDashboard.Text = memberBLL.SelectCountUniqueNationality().ToString();
            labelTotalPermissionSettingsDashboard.Text = permissionBLL.SelectPermittedMembersCount().ToString();
            labelTotalEventSettingsDashboard.Text = eventBLL.SelectEventCount().ToString();
        }

        private void btnUpdateCountry_Click(object sender, EventArgs e)
        {
            if (countryDetail.CountryID == 0)
            {
                MessageBox.Show("Please select a country from the table");
            }
            else
            {
                FormCountry open = new FormCountry();
                open.detail = countryDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtCountry_TextChanged(object sender, EventArgs e)
        {
            List<CountryDetailDTO> list = countryDTO.Countries;
            list = list.Where(x => x.CountryName.Contains(txtCountry.Text)).ToList();
            dataGridViewCountry.DataSource = list;
        }

        private void dataGridViewCountry_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            countryDetail = new CountryDetailDTO();
            countryDetail.CountryID = Convert.ToInt32(dataGridViewCountry.Rows[e.RowIndex].Cells[0].Value);
            countryDetail.CountryName = dataGridViewCountry.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnDeleteCountry_Click(object sender, EventArgs e)
        {
            if (countryDetail.CountryID == 0)
            {
                MessageBox.Show("Please select a country from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (countryBLL.Delete(countryDetail))
                    {
                        MessageBox.Show("Country was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        EmploymentStatusBLL empStatusBLL = new EmploymentStatusBLL();
        EmploymentStatusDTO empStatusDTO = new EmploymentStatusDTO();
        EmploymentStatusDetailDTO empStatusDetail = new EmploymentStatusDetailDTO();

        private void btnAddEmpStatus_Click(object sender, EventArgs e)
        {
            FormEmploymentStatus open = new FormEmploymentStatus();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateEmpStatus_Click(object sender, EventArgs e)
        {
            FormEmploymentStatus open = new FormEmploymentStatus();
            open.detail = empStatusDetail;
            open.isUpdate = true;
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void txtEmpStatus_TextChanged(object sender, EventArgs e)
        {
            List<EmploymentStatusDetailDTO> list = empStatusDTO.EmploymentStatuses;
            list = list.Where(x => x.EmploymentStatus.Contains(txtEmpStatus.Text)).ToList();
            dataGridViewEmpStatus.DataSource = list;
        }

        private void dataGridViewEmpStatus_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            empStatusDetail = new EmploymentStatusDetailDTO();
            empStatusDetail.EmploymentStatusID = Convert.ToInt32(dataGridViewEmpStatus.Rows[e.RowIndex].Cells[0].Value);
            empStatusDetail.EmploymentStatus = dataGridViewEmpStatus.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnDeleteEmpStatus_Click(object sender, EventArgs e)
        {
            if (empStatusDetail.EmploymentStatusID == 0)
            {
                MessageBox.Show("Please select an employment status from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (empStatusBLL.Delete(empStatusDetail))
                    {
                        MessageBox.Show("Employment status was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        MaritalStatusBLL marStatusBLL = new MaritalStatusBLL();
        MaritalStatusDTO marStatusDTO = new MaritalStatusDTO();
        MaritalStatusDetailDTO marStatusDetail = new MaritalStatusDetailDTO();

        private void btnAddMarStatus_Click(object sender, EventArgs e)
        {
            FormMaritalStatus open = new FormMaritalStatus();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateMarStatus_Click(object sender, EventArgs e)
        {
            if (marStatusDetail.MaritalStatusID == 0)
            {
                MessageBox.Show("Please select a marital status from the table");
            }
            else
            {
                FormMaritalStatus open = new FormMaritalStatus();
                open.detail = marStatusDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtMaritalStatus_TextChanged(object sender, EventArgs e)
        {
            List<MaritalStatusDetailDTO> list = marStatusDTO.MaritalStatuses;
            list = list.Where(x => x.MaritalStatus.Contains(txtMaritalStatus.Text)).ToList();
            dataGridViewMarStatus.DataSource = list;
        }

        private void dataGridViewMarStatus_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            marStatusDetail = new MaritalStatusDetailDTO();
            marStatusDetail.MaritalStatusID = Convert.ToInt32(dataGridViewMarStatus.Rows[e.RowIndex].Cells[0].Value);
            marStatusDetail.MaritalStatus = dataGridViewMarStatus.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnDeleteMarStatus_Click(object sender, EventArgs e)
        {
            if (marStatusDetail.MaritalStatusID == 0)
            {
                MessageBox.Show("Please select a marital status from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (marStatusBLL.Delete(marStatusDetail))
                    {
                        MessageBox.Show("Marital status was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        NationalityBLL nationalityBLL = new NationalityBLL();
        NationalityDTO nationalityDTO = new NationalityDTO();
        NationalityDetailDTO nationalityDetail = new NationalityDetailDTO();

        private void btnAddNationality_Click(object sender, EventArgs e)
        {
            FormNationality open = new FormNationality();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateNationality_Click(object sender, EventArgs e)
        {
            if (nationalityDetail.NationalityID == 0)
            {
                MessageBox.Show("Please choose a nationality from the table");
            }
            else
            {
                FormNationality open = new FormNationality();
                open.detail = nationalityDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void txtNationality_TextChanged(object sender, EventArgs e)
        {
            List<NationalityDetailDTO> list = nationalityDTO.Nationalities;
            list = list.Where(x => x.Nationality.Contains(txtNationality.Text)).ToList();
            dataGridViewNationality.DataSource = list;
        }

        private void dataGridViewNationality_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            nationalityDetail = new NationalityDetailDTO();
            nationalityDetail.NationalityID = Convert.ToInt32(dataGridViewNationality.Rows[e.RowIndex].Cells[0].Value);
            nationalityDetail.Nationality = dataGridViewNationality.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnDeleteNationality_Click(object sender, EventArgs e)
        {
            if (nationalityDetail.NationalityID == 0)
            {
                MessageBox.Show("Please choose a nationality from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (nationalityBLL.Delete(nationalityDetail))
                    {
                        MessageBox.Show("Nationality was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        PermissionBLL permissionBLL = new PermissionBLL();
        PermissionDTO permissionDTO = new PermissionDTO();
        MemberDetailDTO permissionDetail = new MemberDetailDTO();
        MemberBLL permissionMemberBLL = new MemberBLL();

        private void btnSearchPermissions_Click(object sender, EventArgs e)
        {
            List<MemberDetailDTO> list = permissionDTO.Members;
            if (cmbPermission.SelectedIndex != -1)
            {
                list = list.Where(x => x.PermissionID == Convert.ToInt32(cmbPermission.SelectedValue)).ToList();
            }
            dataGridViewPermissions.DataSource = list;
        }

        private void btnDeletePermissions_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (permissionMemberBLL.DeletePermission(permissionDetail))
                {
                    MessageBox.Show("Member was deleted");
                    ClearFilters();
                }                
            }
        }

        private void dataGridViewPermissions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            permissionDetail = new MemberDetailDTO();
            permissionDetail.MemberID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[0].Value);
            permissionDetail.Username = dataGridViewPermissions.Rows[e.RowIndex].Cells[1].Value.ToString();
            permissionDetail.Password = dataGridViewPermissions.Rows[e.RowIndex].Cells[2].Value.ToString();
            permissionDetail.Surname = dataGridViewPermissions.Rows[e.RowIndex].Cells[3].Value.ToString();
            permissionDetail.Name = dataGridViewPermissions.Rows[e.RowIndex].Cells[4].Value.ToString();
            permissionDetail.Birthday = Convert.ToDateTime(dataGridViewPermissions.Rows[e.RowIndex].Cells[5].Value);
            permissionDetail.ImagePath = dataGridViewPermissions.Rows[e.RowIndex].Cells[6].Value.ToString();
            permissionDetail.EmailAddress = dataGridViewPermissions.Rows[e.RowIndex].Cells[7].Value.ToString();
            permissionDetail.HouseAddress = dataGridViewPermissions.Rows[e.RowIndex].Cells[8].Value.ToString();
            permissionDetail.MembershipDate = Convert.ToDateTime(dataGridViewPermissions.Rows[e.RowIndex].Cells[9].Value);
            permissionDetail.CountryID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[10].Value);
            permissionDetail.CountryName = dataGridViewPermissions.Rows[e.RowIndex].Cells[11].Value.ToString();
            permissionDetail.NationalityID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[12].Value);
            permissionDetail.NationalityName = dataGridViewPermissions.Rows[e.RowIndex].Cells[13].Value.ToString();
            permissionDetail.ProfessionID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[14].Value);
            permissionDetail.ProfessionName = dataGridViewPermissions.Rows[e.RowIndex].Cells[15].Value.ToString();
            permissionDetail.PositionID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[16].Value);
            permissionDetail.PositionName = dataGridViewPermissions.Rows[e.RowIndex].Cells[17].Value.ToString();
            permissionDetail.GenderID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[18].Value);
            permissionDetail.GenderName = dataGridViewPermissions.Rows[e.RowIndex].Cells[19].Value.ToString();
            permissionDetail.EmploymentStatusID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[20].Value);
            permissionDetail.EmploymentStatusName = dataGridViewPermissions.Rows[e.RowIndex].Cells[21].Value.ToString();
            permissionDetail.MaritalStatusID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[22].Value);
            permissionDetail.MaritalStatusName = dataGridViewPermissions.Rows[e.RowIndex].Cells[23].Value.ToString();
            permissionDetail.PermissionID = Convert.ToInt32(dataGridViewPermissions.Rows[e.RowIndex].Cells[24].Value);
            permissionDetail.PermissionName = dataGridViewPermissions.Rows[e.RowIndex].Cells[25].Value.ToString();
            permissionDetail.PhoneNumber = dataGridViewPermissions.Rows[e.RowIndex].Cells[26].Value.ToString();
            permissionDetail.PhoneNumber2 = dataGridViewPermissions.Rows[e.RowIndex].Cells[27].Value.ToString();
            permissionDetail.PhoneNumber3 = dataGridViewPermissions.Rows[e.RowIndex].Cells[28].Value.ToString();
        }

        private void btnClearPermissions_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

        PositionDTO positionDTO = new PositionDTO();
        PositionBLL positionBLL = new PositionBLL();
        PositionDetailDTO positionDetail = new PositionDetailDTO();

        private void btnAddPositions_Click(object sender, EventArgs e)
        {
            FormPosition open = new FormPosition();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {
            List<PositionDetailDTO> list = positionDTO.Positions;
            list = list.Where(x => x.PositionName.Contains(txtPosition.Text)).ToList();
            dataGridViewPositions.DataSource = list;
        }

        private void dataGridViewPositions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            positionDetail = new PositionDetailDTO();
            positionDetail.PositionID = Convert.ToInt32(dataGridViewPositions.Rows[e.RowIndex].Cells[0].Value);
            positionDetail.PositionName = dataGridViewPositions.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnUpdatePositions_Click(object sender, EventArgs e)
        {
            if (positionDetail.PositionID == 0)
            {
                MessageBox.Show("Please select a position from the table");
            }
            else
            {
                FormPosition open = new FormPosition();
                open.detail = positionDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnDeletePositions_Click(object sender, EventArgs e)
        {
            if (positionDetail.PositionID == 0)
            {
                MessageBox.Show("Please select a position from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (positionBLL.Delete(positionDetail))
                    {
                        MessageBox.Show("Position was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        ProfessionBLL professionBLL = new ProfessionBLL();
        ProfessionDTO professionDTO = new ProfessionDTO();
        ProfessionDetailDTO professionDetail = new ProfessionDetailDTO();

        private void btnAddProfessions_Click(object sender, EventArgs e)
        {
            FormProfession open = new FormProfession();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdateProfessions_Click(object sender, EventArgs e)
        {
            if (professionDetail.ProfessionID == 0)
            {
                MessageBox.Show("Please select a profession from the table");
            }
            else
            {
                FormProfession open = new FormProfession();
                open.detail = professionDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;                
                ClearFilters();
            }            
        }

        private void txtProfession_TextChanged(object sender, EventArgs e)
        {
            List<ProfessionDetailDTO> list = professionDTO.Professions;
            list = list.Where(x => x.Profession.Contains(txtProfession.Text)).ToList();
            dataGridViewProfessions.DataSource = list;
        }

        private void dataGridViewProfessions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            professionDetail = new ProfessionDetailDTO();
            professionDetail.ProfessionID = Convert.ToInt32(dataGridViewProfessions.Rows[e.RowIndex].Cells[0].Value);
            professionDetail.Profession = dataGridViewProfessions.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnDeleteProfessions_Click(object sender, EventArgs e)
        {
            if (professionDetail.ProfessionID == 0)
            {
                MessageBox.Show("Please select a profession from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (professionBLL.Delete(professionDetail))
                    {
                        MessageBox.Show("Profession was deleted");
                        ClearFilters();
                    }
                }
            }
        }

        MemberBLL memberDeletedDataBLL = new MemberBLL();
        DeletedDataDTO memberDeletedDataDTO = new DeletedDataDTO();
        MemberDetailDTO memberDeletedDataDetail = new MemberDetailDTO();
        CountryBLL countryDeletedDataBLL = new CountryBLL();
        CountryDetailDTO countryDeletedDataDetail = new CountryDetailDTO();
        NationalityBLL nationalityDeletedDataBLL = new NationalityBLL();
        NationalityDetailDTO nationalityDeletedDataDetail = new NationalityDetailDTO();
        ProfessionBLL professionDeletedDataBLL = new ProfessionBLL();
        ProfessionDetailDTO professionDeletedDataDetail = new ProfessionDetailDTO();
        PositionBLL positionDeletedDataBLL = new PositionBLL();
        PositionDetailDTO positionDeletedDataDetail = new PositionDetailDTO();
        EmploymentStatusBLL empStatusDeletedDataBLL = new EmploymentStatusBLL();
        EmploymentStatusDetailDTO empStatusDeletedDataDetail = new EmploymentStatusDetailDTO();
        MaritalStatusBLL marStatusDeletedDataBLL = new MaritalStatusBLL();
        MaritalStatusDetailDTO marStatusDeletedDataDetail = new MaritalStatusDetailDTO();
        CommentBLL commentDeletedDataBLL = new CommentBLL();
        CommentDetailDTO commentDeletedDataDetail = new CommentDetailDTO();
        DocumentBLL documentDeletedDataBLL = new DocumentBLL();
        DocumentDetailDTO documentDeletedDataDetail = new DocumentDetailDTO();
        EventImageBLL eventImageDeletedDataBLL = new EventImageBLL();
        EventImageDetailDTO eventImageDeletedDataDetail = new EventImageDetailDTO();
        EventsBLL eventDeletedDataBLL = new EventsBLL();
        EventsDetailDTO eventDeletedDataDetail = new EventsDetailDTO();
        GeneralAttendanceBLL genAttendDeletedDataBLL = new GeneralAttendanceBLL();
        GeneralAttendanceDetailDTO genAttendDeletedDataDetail = new GeneralAttendanceDetailDTO();
        ExpenditureBLL expendutureDeletedDataBLL = new ExpenditureBLL();
        ExpenditureDetailDTO expenditureDeletedDataDetail = new ExpenditureDetailDTO();
        FinancialReportBLL financialRepDeletedDataBLL = new FinancialReportBLL();
        FinancialReportDetailDTO financialRepDeletedDataDetail = new FinancialReportDetailDTO();
        ConstitutionBLL constitutionDeletedDataBLL = new ConstitutionBLL();
        ConstitutionDetailDTO constitutionDeletedDataDetail = new ConstitutionDetailDTO();
        FinedMemberBLL finedMemberDeletedDataBLL = new FinedMemberBLL();
        FinedMemberDetailDTO finedMemberDeletedDataDetail = new FinedMemberDetailDTO();

        private void cmbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Members;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].HeaderText = "Surname";
                dataGridView1.Columns[4].HeaderText = "Name";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].HeaderText = "Nationality";
                dataGridView1.Columns[14].Visible = false;
                dataGridView1.Columns[15].HeaderText = "Profession";
                dataGridView1.Columns[16].Visible = false;
                dataGridView1.Columns[17].HeaderText = "Position";
                dataGridView1.Columns[18].Visible = false;
                dataGridView1.Columns[19].HeaderText = "Gender";
                dataGridView1.Columns[20].Visible = false;
                dataGridView1.Columns[21].Visible = false;
                dataGridView1.Columns[22].Visible = false;
                dataGridView1.Columns[23].Visible = false;
                dataGridView1.Columns[24].Visible = false;
                dataGridView1.Columns[25].Visible = false;
                dataGridView1.Columns[26].Visible = false;
                dataGridView1.Columns[27].Visible = false;
                dataGridView1.Columns[28].Visible = false;
                dataGridView1.Columns[29].Visible = false;
                dataGridView1.Columns[30].Visible = false;
                dataGridView1.Columns[31].Visible = false;
                dataGridView1.Columns[32].Visible = false;
                dataGridView1.Columns[33].Visible = false;
                dataGridView1.Columns[34].Visible = false;
                dataGridView1.Columns[35].Visible = false;
                dataGridView1.Columns[36].Visible = false;
                dataGridView1.Columns[37].Visible = false;
                dataGridView1.Columns[38].Visible = false;
                dataGridView1.Columns[39].Visible = false;
                dataGridView1.Columns[40].Visible = false;
                dataGridView1.Columns[41].Visible = false;
                dataGridView1.Columns[42].Visible = false;
                dataGridView1.Columns[43].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Countries;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Country Name";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Nationalities;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Nationality Name";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Professions;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Profession Name";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 4)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Positions;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Prosition Name";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 5)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.EmploymentStatuses;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Employment Status";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 6)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.MaritalStatuses;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Marital Status";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 8)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Comments;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Comment";
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].HeaderText = "Surname";
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[4].HeaderText = "Name";
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].HeaderText = "Gender";
                dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].HeaderText = "Month";
                dataGridView1.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[11].HeaderText = "Year";
                dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[12].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 9)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Documents;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Document Name";
                dataGridView1.Columns[2].HeaderText = "Document Type";
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].HeaderText = "Date";
                dataGridView1.Columns[8].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 10)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.EventImages;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "No.";
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[5].HeaderText = "Picture Caption";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 11)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Events;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Year";
                dataGridView1.Columns[5].HeaderText = "Event Title";
                dataGridView1.Columns[6].HeaderText = "Summary";
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].HeaderText = "Sold (€)";
                dataGridView1.Columns[10].HeaderText = "Spent (€)";
                dataGridView1.Columns[11].HeaderText = "Balance";
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 12)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.GeneralAttendance;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].HeaderText = "Month";
                dataGridView1.Columns[4].HeaderText = "Year";
                dataGridView1.Columns[5].HeaderText = "Members Present";
                dataGridView1.Columns[6].HeaderText = "Members Absent";
                dataGridView1.Columns[7].HeaderText = "Dues Paid";
                dataGridView1.Columns[8].HeaderText = "Dues Expected";
                dataGridView1.Columns[9].HeaderText = "Balance";
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 13)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Expenditures;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Summary";
                dataGridView1.Columns[2].HeaderText = "Amount Spent (€)";
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Month";
                dataGridView1.Columns[6].HeaderText = "Year";
                dataGridView1.Columns[7].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 14)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.FinancialReports;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Year";
                dataGridView1.Columns[2].HeaderText = "Total Amount Raised";
                dataGridView1.Columns[3].HeaderText = "Total Amount Spent";
                dataGridView1.Columns[4].HeaderText = "Total Balance";
                dataGridView1.Columns[5].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 15)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.Constitutions;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Constitution";
                dataGridView1.Columns[2].HeaderText = "Short Description";
                dataGridView1.Columns[3].HeaderText = "Section";
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Fine";
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else if (cmbDeletedData.SelectedIndex == 16)
            {
                dataGridView1.DataSource = memberDeletedDataDTO.FinedMembers;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Name";
                dataGridView1.Columns[2].HeaderText = "Surname";
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].HeaderText = "Violated";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].HeaderText = "Fine";
                dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].HeaderText = "Paid";
                dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].HeaderText = "Status";
                dataGridView1.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                dataGridView1.Columns[14].Visible = false;
                dataGridView1.Columns[15].Visible = false;
                dataGridView1.Columns[16].Visible = false;
                dataGridView1.Columns[17].Visible = false;
                dataGridView1.Columns[18].Visible = false;
                dataGridView1.Columns[19].Visible = false;
                dataGridView1.Columns[20].HeaderText = "Day";
                dataGridView1.Columns[20].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[21].Visible = false;
                dataGridView1.Columns[22].HeaderText = "Month";
                dataGridView1.Columns[22].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[23].HeaderText = "Year";
                dataGridView1.Columns[23].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[24].Visible = false;
                dataGridView1.Columns[25].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.HeaderCell.Style.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                }
            }
            else
            {
                MessageBox.Show("Unknown data");
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0 || cmbDeletedData.SelectedIndex == -1)
            {
                memberDeletedDataDetail = new MemberDetailDTO();
                memberDeletedDataDetail.MemberID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                memberDeletedDataDetail.Username = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                memberDeletedDataDetail.Password = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                memberDeletedDataDetail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                memberDeletedDataDetail.Name = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                memberDeletedDataDetail.Birthday = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                memberDeletedDataDetail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                memberDeletedDataDetail.EmailAddress = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                memberDeletedDataDetail.HouseAddress = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                memberDeletedDataDetail.MembershipDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                memberDeletedDataDetail.CountryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                memberDeletedDataDetail.CountryName = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                memberDeletedDataDetail.NationalityID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
                memberDeletedDataDetail.NationalityName = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                memberDeletedDataDetail.ProfessionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
                memberDeletedDataDetail.ProfessionName = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                memberDeletedDataDetail.PositionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[16].Value);
                memberDeletedDataDetail.PositionName = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
                memberDeletedDataDetail.GenderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[18].Value);
                memberDeletedDataDetail.GenderName = dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
                memberDeletedDataDetail.EmploymentStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[20].Value);
                memberDeletedDataDetail.EmploymentStatusName = dataGridView1.Rows[e.RowIndex].Cells[21].Value.ToString();
                memberDeletedDataDetail.MaritalStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[22].Value);
                memberDeletedDataDetail.MaritalStatusName = dataGridView1.Rows[e.RowIndex].Cells[23].Value.ToString();
                memberDeletedDataDetail.PermissionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[24].Value);
                memberDeletedDataDetail.PermissionName = dataGridView1.Rows[e.RowIndex].Cells[25].Value.ToString();
                memberDeletedDataDetail.PhoneNumber = dataGridView1.Rows[e.RowIndex].Cells[26].Value.ToString();
                memberDeletedDataDetail.PhoneNumber2 = dataGridView1.Rows[e.RowIndex].Cells[27].Value.ToString();
                memberDeletedDataDetail.PhoneNumber3 = dataGridView1.Rows[e.RowIndex].Cells[28].Value.ToString();
                memberDeletedDataDetail.isCountryDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[29].Value);
                memberDeletedDataDetail.isNationalityDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[30].Value);
                memberDeletedDataDetail.isProfessionDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[31].Value);
                memberDeletedDataDetail.isPositionDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[32].Value);
                memberDeletedDataDetail.isEmpStatusDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[33].Value);
                memberDeletedDataDetail.isMarStatusDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[34].Value);
                memberDeletedDataDetail.MembershipStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[35].Value);
                memberDeletedDataDetail.MembershipStatus = dataGridView1.Rows[e.RowIndex].Cells[36].Value.ToString();
                memberDeletedDataDetail.DeadDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[37].Value);
                memberDeletedDataDetail.DeadAge = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[38].Value);
                memberDeletedDataDetail.LGA = dataGridView1.Rows[e.RowIndex].Cells[39].Value.ToString();
                memberDeletedDataDetail.NameOfNextOfKin = dataGridView1.Rows[e.RowIndex].Cells[40].Value.ToString();
                memberDeletedDataDetail.RelationshipToKinID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[41].Value);
                memberDeletedDataDetail.RelationshipToKin = dataGridView1.Rows[e.RowIndex].Cells[42].Value.ToString();
                memberDeletedDataDetail.BirthdayDate = dataGridView1.Rows[e.RowIndex].Cells[43].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                countryDeletedDataDetail = new CountryDetailDTO();
                countryDeletedDataDetail.CountryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                countryDeletedDataDetail.CountryName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                nationalityDeletedDataDetail = new NationalityDetailDTO();
                nationalityDeletedDataDetail.NationalityID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                nationalityDeletedDataDetail.Nationality = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                professionDeletedDataDetail = new ProfessionDetailDTO();
                professionDeletedDataDetail.ProfessionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                professionDeletedDataDetail.Profession = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 4)
            {
                positionDeletedDataDetail = new PositionDetailDTO();
                positionDeletedDataDetail.PositionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                positionDeletedDataDetail.PositionName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 5)
            {
                empStatusDeletedDataDetail = new EmploymentStatusDetailDTO();
                empStatusDeletedDataDetail.EmploymentStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                empStatusDeletedDataDetail.EmploymentStatus = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 6)
            {
                marStatusDeletedDataDetail = new MaritalStatusDetailDTO();
                marStatusDeletedDataDetail.MaritalStatusID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                marStatusDeletedDataDetail.MaritalStatus = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }            
            else if (cmbDeletedData.SelectedIndex == 8)
            {
                commentDeletedDataDetail = new CommentDetailDTO();
                commentDeletedDataDetail.CommentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                commentDeletedDataDetail.CommentName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                commentDeletedDataDetail.MemberID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                commentDeletedDataDetail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                commentDeletedDataDetail.Name = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                commentDeletedDataDetail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                commentDeletedDataDetail.GenderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                commentDeletedDataDetail.GenderName = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                commentDeletedDataDetail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                commentDeletedDataDetail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                commentDeletedDataDetail.MonthName = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                commentDeletedDataDetail.Year = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                commentDeletedDataDetail.isMemberDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            }
            else if (cmbDeletedData.SelectedIndex == 9)
            {
                documentDeletedDataDetail = new DocumentDetailDTO();
                documentDeletedDataDetail.DocumentID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                documentDeletedDataDetail.DocumentName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                documentDeletedDataDetail.DocumentType = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                documentDeletedDataDetail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                documentDeletedDataDetail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                documentDeletedDataDetail.MonthName = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                documentDeletedDataDetail.Year = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                documentDeletedDataDetail.Date = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                documentDeletedDataDetail.DocumentPath = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 10)
            {
                eventImageDeletedDataDetail = new EventImageDetailDTO();
                eventImageDeletedDataDetail.EventImageID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                eventImageDeletedDataDetail.EventID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                eventImageDeletedDataDetail.Summary = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                eventImageDeletedDataDetail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                eventImageDeletedDataDetail.Counter = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                eventImageDeletedDataDetail.ImageCaption = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 11)
            {
                eventDeletedDataDetail = new EventsDetailDTO();
                eventDeletedDataDetail.EventID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                eventDeletedDataDetail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                eventDeletedDataDetail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                eventDeletedDataDetail.MonthName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                eventDeletedDataDetail.Year = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                eventDeletedDataDetail.EventTitle = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                eventDeletedDataDetail.Summary = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                eventDeletedDataDetail.CoverImagePath = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                eventDeletedDataDetail.EventDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                eventDeletedDataDetail.AmountSold = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                eventDeletedDataDetail.AmountSpent = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                eventDeletedDataDetail.Balance = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
            }
            else if (cmbDeletedData.SelectedIndex == 12)
            {
                genAttendDeletedDataDetail = new GeneralAttendanceDetailDTO();
                genAttendDeletedDataDetail.GeneralAttendanceID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                genAttendDeletedDataDetail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                genAttendDeletedDataDetail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                genAttendDeletedDataDetail.Month = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                genAttendDeletedDataDetail.Year = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                genAttendDeletedDataDetail.TotalMembersPresent = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                genAttendDeletedDataDetail.TotalMembersAbsent = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                genAttendDeletedDataDetail.TotalDuesPaid = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                genAttendDeletedDataDetail.TotalDuesExpected = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                genAttendDeletedDataDetail.TotalDuesBalance = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                genAttendDeletedDataDetail.Summary = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                genAttendDeletedDataDetail.AttendanceDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
            }
            else if (cmbDeletedData.SelectedIndex == 13)
            {
                expenditureDeletedDataDetail = new ExpenditureDetailDTO();
                expenditureDeletedDataDetail.ExpenditureID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                expenditureDeletedDataDetail.Summary = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                expenditureDeletedDataDetail.AmountSpent = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                expenditureDeletedDataDetail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                expenditureDeletedDataDetail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                expenditureDeletedDataDetail.Month = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                expenditureDeletedDataDetail.Year = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                expenditureDeletedDataDetail.ExpenditureDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            }
            else if (cmbDeletedData.SelectedIndex == 14)
            {
                financialRepDeletedDataDetail = new FinancialReportDetailDTO();
                financialRepDeletedDataDetail.FinancialReportID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                financialRepDeletedDataDetail.Year = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                financialRepDeletedDataDetail.TotalAmountRaised = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                financialRepDeletedDataDetail.TotalAmountSpent = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                financialRepDeletedDataDetail.Balance = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                financialRepDeletedDataDetail.Summary = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 15)
            {
                constitutionDeletedDataDetail = new ConstitutionDetailDTO();
                constitutionDeletedDataDetail.ConstitutionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                constitutionDeletedDataDetail.ConstitutionText = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                constitutionDeletedDataDetail.ShortDescription = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                constitutionDeletedDataDetail.Section = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                constitutionDeletedDataDetail.Fine = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                constitutionDeletedDataDetail.FineWithCurrency = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            else if (cmbDeletedData.SelectedIndex == 16)
            {
                finedMemberDeletedDataDetail = new FinedMemberDetailDTO();
                finedMemberDeletedDataDetail.FinedMemberID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                finedMemberDeletedDataDetail.Name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                finedMemberDeletedDataDetail.Surname = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                finedMemberDeletedDataDetail.ConstitutionSection = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                finedMemberDeletedDataDetail.ConstitutionShortDescription = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                finedMemberDeletedDataDetail.ExpectedAmount = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                finedMemberDeletedDataDetail.ExpectedAmountWithCurrency = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                finedMemberDeletedDataDetail.AmountPaid = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                finedMemberDeletedDataDetail.AmountPaidWithCurrency = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                finedMemberDeletedDataDetail.Balance = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                finedMemberDeletedDataDetail.BalanceWithCurrency = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                finedMemberDeletedDataDetail.FineStatus = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                finedMemberDeletedDataDetail.Gender = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                finedMemberDeletedDataDetail.Summary = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                finedMemberDeletedDataDetail.ConstitutionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
                finedMemberDeletedDataDetail.Constitution = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                finedMemberDeletedDataDetail.MemberID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[16].Value);
                finedMemberDeletedDataDetail.PositionID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[17].Value);
                finedMemberDeletedDataDetail.Position = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                finedMemberDeletedDataDetail.GenderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[19].Value);
                finedMemberDeletedDataDetail.Day = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[20].Value);
                finedMemberDeletedDataDetail.MonthID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[21].Value);
                finedMemberDeletedDataDetail.MonthName = dataGridView1.Rows[e.RowIndex].Cells[22].Value.ToString();
                finedMemberDeletedDataDetail.Year = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[23].Value);
                finedMemberDeletedDataDetail.ImagePath = dataGridView1.Rows[e.RowIndex].Cells[24].Value.ToString();
                finedMemberDeletedDataDetail.FineDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[25].Value);
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0 || cmbDeletedData.SelectedIndex == -1)
            {
                if (memberDeletedDataDetail.MemberID == 0)
                {
                    MessageBox.Show("Please choose member from the table");
                }
                else
                {
                    if (memberDeletedDataDetail.isCountryDeleted)
                    {
                        MessageBox.Show("Country was deleted. Get back the country first.");
                    }
                    else if (memberDeletedDataDetail.isNationalityDeleted)
                    {
                        MessageBox.Show("Nationality was deleted. Get back the nationality first.");
                    }
                    else if (memberDeletedDataDetail.isProfessionDeleted)
                    {
                        MessageBox.Show("Profession was deleted. Get back the profession first.");
                    }
                    else if (memberDeletedDataDetail.isPositionDeleted)
                    {
                        MessageBox.Show("Position was deleted. Get back the position first.");
                    }
                    else if (memberDeletedDataDetail.isEmpStatusDeleted)
                    {
                        MessageBox.Show("Employment status was deleted. Get back the employment status first.");
                    }
                    else if (memberDeletedDataDetail.isMarStatusDeleted)
                    {
                        MessageBox.Show("marital status was deleted. Get back the marital status first.");
                    }
                    else
                    {
                        if (memberDeletedDataBLL.GetBack(memberDeletedDataDetail))
                        {
                            MessageBox.Show("Member was retrieved");
                            memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                            dataGridView1.DataSource = memberDeletedDataDTO.Members;
                        }
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                if (countryDeletedDataDetail.CountryID == 0)
                {
                    MessageBox.Show("Please choose a country from the table");
                }
                else
                {
                    if (countryDeletedDataBLL.GetBack(countryDeletedDataDetail))
                    {
                        MessageBox.Show("Country was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Countries;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                if (nationalityDeletedDataDetail.NationalityID == 0)
                {
                    MessageBox.Show("Please choose a nationality from the table");
                }
                else
                {
                    if (nationalityDeletedDataBLL.GetBack(nationalityDeletedDataDetail))
                    {
                        MessageBox.Show("Nationality was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Nationalities;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                if (professionDeletedDataDetail.ProfessionID == 0)
                {
                    MessageBox.Show("Please choose a profession from the table");
                }
                else
                {
                    if (professionDeletedDataBLL.GetBack(professionDeletedDataDetail))
                    {
                        MessageBox.Show("Profession was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Professions;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 4)
            {
                if (positionDeletedDataDetail.PositionID == 0)
                {
                    MessageBox.Show("Please choose a position from the table");
                }
                else
                {
                    if (positionDeletedDataBLL.GetBack(positionDeletedDataDetail))
                    {
                        MessageBox.Show("Position was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Positions;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 5)
            {
                if (empStatusDeletedDataDetail.EmploymentStatusID == 0)
                {
                    MessageBox.Show("Please choose an employment status from the table");
                }
                else
                {
                    if (empStatusDeletedDataBLL.GetBack(empStatusDeletedDataDetail))
                    {
                        MessageBox.Show("Employment status was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.EmploymentStatuses;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 6)
            {
                if (marStatusDeletedDataDetail.MaritalStatusID == 0)
                {
                    MessageBox.Show("Please choose a marital status from the table");
                }
                else
                {
                    if (marStatusDeletedDataBLL.GetBack(marStatusDeletedDataDetail))
                    {
                        MessageBox.Show("Marital status was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.MaritalStatuses;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 8)
            {
                if (commentDeletedDataDetail.CommentID == 0)
                {
                    MessageBox.Show("Please choose a comment from the table");
                }
                else
                {
                    if (commentDeletedDataDetail.isMemberDeleted)
                    {
                        MessageBox.Show("Member was deleted. Get back the member first.");
                    }
                    else if (commentDeletedDataBLL.GetBack(commentDeletedDataDetail))
                    {
                        MessageBox.Show("Comment was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Comments;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 9)
            {
                if (documentDeletedDataDetail.DocumentID == 0)
                {
                    MessageBox.Show("Please choose a document from the table");
                }
                else
                {
                    if (documentDeletedDataBLL.GetBack(documentDeletedDataDetail))
                    {
                        MessageBox.Show("Document was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Documents;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 10)
            {
                if (eventImageDeletedDataDetail.EventImageID == 0)
                {
                    MessageBox.Show("Please choose an event image from the table");
                }
                else
                {
                    if (eventImageDeletedDataBLL.GetBack(eventImageDeletedDataDetail))
                    {
                        MessageBox.Show("Picture was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.EventImages;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 11)
            {
                if (eventDeletedDataDetail.EventID == 0)
                {
                    MessageBox.Show("Please choose an event from the table");
                }
                else
                {
                    if (eventDeletedDataBLL.GetBack(eventDeletedDataDetail))
                    {
                        MessageBox.Show("Event was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Events;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 12)
            {
                if (genAttendDeletedDataDetail.GeneralAttendanceID == 0)
                {
                    MessageBox.Show("Please choose an attendance from the table");
                }
                else
                {
                    if (genAttendDeletedDataBLL.GetBack(genAttendDeletedDataDetail))
                    {
                        MessageBox.Show("Attendance was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.GeneralAttendance;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 13)
            {
                if (expenditureDeletedDataDetail.ExpenditureID == 0)
                {
                    MessageBox.Show("Please choose an expenditure from the table");
                }
                else
                {
                    if (expendutureDeletedDataBLL.GetBack(expenditureDeletedDataDetail))
                    {
                        MessageBox.Show("Expenditure was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Expenditures;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 14)
            {
                if (financialRepDeletedDataDetail.FinancialReportID == 0)
                {
                    MessageBox.Show("Please choose a financial report from the table");
                }
                else
                {
                    if (financialRepDeletedDataBLL.GetBack(financialRepDeletedDataDetail))
                    {
                        MessageBox.Show("Financial report was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.FinancialReports;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 15)
            {
                if (constitutionDeletedDataDetail.ConstitutionID == 0)
                {
                    MessageBox.Show("Please choose a constitution from the table");
                }
                else
                {
                    if (constitutionDeletedDataBLL.GetBack(constitutionDeletedDataDetail))
                    {
                        MessageBox.Show("Constitution was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.Constitutions;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 16)
            {
                if (finedMemberDeletedDataDetail.FinedMemberID == 0)
                {
                    MessageBox.Show("Please choose a fined member from the table");
                }
                else
                {
                    if (finedMemberDeletedDataBLL.GetBack(finedMemberDeletedDataDetail))
                    {
                        MessageBox.Show("Fined member was retrieved");
                        memberDeletedDataDTO = memberDeletedDataBLL.Select(true);
                        dataGridView1.DataSource = memberDeletedDataDTO.FinedMembers;
                    }
                }
            }

            ClearFilters();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string url = "https://www.youtube.com/playlist?list=PLaJh6UDW5CBE701TVE3D7ZZxHj9xBfgf4";
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the website: " + ex.Message);
            }
        }

        
    }
}
