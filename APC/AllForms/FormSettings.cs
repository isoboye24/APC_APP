using APC.BLL;
using APC.DAL.DAO;
using APC.DAL.DTO;
using APC.HelperServices;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static APC.HelperServices.CommentHelperService;
using static APC.HelperServices.DocumentHelperService;
using static APC.HelperServices.EventsHelperService;
using static APC.HelperServices.ExpenditureHelperService;
using static APC.HelperServices.GeneralAttendanceHelperService;
using static APC.HelperServices.MemberHelperService;
using static APC.HelperServices.SingleColumnHelperService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace APC.AllForms
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }
        MemberDTO memberDTO = new MemberDTO();
        MemberBLL memberBLL = new MemberBLL();     
        
        EventsBLL eventBLL = new EventsBLL();

        PaymentStatusBLL paymentStatusBLL = new PaymentStatusBLL();
        PaymentStatusDTO paymentStatusDTO = new PaymentStatusDTO();
        PaymentStatusDetailDTO paymentStatusDetail = new PaymentStatusDetailDTO();

        CountryBLL countryBLL = new CountryBLL();
        CountryDTO countryDTO = new CountryDTO();
        CountryDetailDTO countryDetail = new CountryDetailDTO();

        EmploymentStatusBLL empStatusBLL = new EmploymentStatusBLL();
        EmploymentStatusDTO empStatusDTO = new EmploymentStatusDTO();
        EmploymentStatusDetailDTO empStatusDetail = new EmploymentStatusDetailDTO();

        MaritalStatusBLL marStatusBLL = new MaritalStatusBLL();
        MaritalStatusDTO marStatusDTO = new MaritalStatusDTO();
        MaritalStatusDetailDTO marStatusDetail = new MaritalStatusDetailDTO();

        NationalityBLL nationalityBLL = new NationalityBLL();
        NationalityDTO nationalityDTO = new NationalityDTO();
        NationalityDetailDTO nationalityDetail = new NationalityDetailDTO();

        PositionDTO positionDTO = new PositionDTO();
        PositionBLL positionBLL = new PositionBLL();
        PositionDetailDTO positionDetail = new PositionDetailDTO();

        PermissionBLL permissionBLL = new PermissionBLL();
        PermissionDTO permissionDTO = new PermissionDTO();
        MemberDetailDTO permissionDetail = new MemberDetailDTO();
        MemberBLL permissionMemberBLL = new MemberBLL();

        ProfessionBLL professionBLL = new ProfessionBLL();
        ProfessionDTO professionDTO = new ProfessionDTO();
        ProfessionDetailDTO professionDetail = new ProfessionDetailDTO();

        CommentDTO commentDTO = new CommentDTO();
        CommentBLL commentBLL = new CommentBLL();

        DocumentDTO documentDTO = new DocumentDTO();
        DocumentBLL documentBLL = new DocumentBLL();

        EventImageDTO eventImageDTO = new EventImageDTO();
        EventImageBLL eventImageBLL = new EventImageBLL();

        EventsDTO eventsDTO = new EventsDTO();
        EventsBLL eventsBLL = new EventsBLL();

        EventSalesDTO eventSalesDTO = new EventSalesDTO();
        EventSalesBLL eventSalesBLL = new EventSalesBLL();

        EventExpenditureDTO eventExpenditureDTO = new EventExpenditureDTO();
        EventExpenditureBLL eventExpenditureBLL = new EventExpenditureBLL();

        EventReceiptsDTO eventReceiptsDTO = new EventReceiptsDTO();
        EventReceiptsBLL eventReceiptsBLL = new EventReceiptsBLL();

        GeneralAttendanceDTO generalAttendanceDTO = new GeneralAttendanceDTO();
        GeneralAttendanceBLL generalAttendanceBLL = new GeneralAttendanceBLL();

        ExpenditureDTO expenditureDTO = new ExpenditureDTO();
        ExpenditureBLL expenditureBLL = new ExpenditureBLL();

        FinancialReportDTO financialReportDTO = new FinancialReportDTO();
        FinancialReportBLL financialReportBLL = new FinancialReportBLL();

        ConstitutionDTO constitutionDTO = new ConstitutionDTO();
        ConstitutionBLL constitutionBLL = new ConstitutionBLL();

        FinedMemberDTO finedMemberDTO = new FinedMemberDTO();
        FinedMemberBLL finedMemberBLL = new FinedMemberBLL();


        private void resizeControls() 
        {
            GeneralHelper.ApplyBoldFont14(
                label1, label2, label3, label4, label5, label6, label7, label8,
                label9, label10, label11, label12, label13, label14, label15, label16, label17, label19,
                btnAddCountry, btnUpdateCountry, btnDeleteCountry, btnAddEmpStatus, btnUpdateEmpStatus,
                btnDeleteEmpStatus, btnAddMarStatus, btnUpdateMarStatus, btnDeleteMarStatus, btnAddNationality,
                btnUpdateNationality, btnDeleteNationality, btnDeletePermissions, btnSearchPermissions,
                btnClearPermissions, btnAddPositions, btnUpdatePositions, btnDeletePositions, btnAddProfessions,
                btnUpdateProfessions, btnDeleteProfessions, labelName, labelSurname, labelAccessLevel,
                labelPosition, cmbDeletedData, btnRetrieve
                );

            GeneralHelper.ApplyRegularFont11(
                labelTotalCountry, labelTotalEmpStatus, labelTotalMarStatus, labelTotalNationality,
                labelTotalPermissions, labelTotalPositions, labelTotalProfessions
                );
            
            GeneralHelper.ApplyRegularFont14(
                txtCountry, txtMaritalStatus, txtEmpStatus, txtNationality, cmbPermission, txtPosition,
                txtProfession
                );
        }
        private void FormSettings_Load(object sender, EventArgs e)
        {
            // Controls sizes
            resizeControls();

            paymentStatusDTO = paymentStatusBLL.Select();
            countryDTO = countryBLL.Select();
            empStatusDTO = empStatusBLL.Select();
            marStatusDTO = marStatusBLL.Select();
            nationalityDTO = nationalityBLL.Select();
            positionDTO = positionBLL.Select();
            professionDTO = professionBLL.Select();
            permissionDTO = permissionBLL.Select();
            deletedDataDTO = deletedDataBLL.Select(true);

            LoadDataGridView.loadMembers(dataGridView1, memberDTO);

            LoadDataGridView.loadPaymentStatuses(dataGridViewPaymentStatus, paymentStatusDTO);

            LoadDataGridView.loadCountries(dataGridViewCountry, countryDTO);

            LoadDataGridView.loadEmploymentStatuses(dataGridViewEmpStatus, empStatusDTO);

            LoadDataGridView.loadMaritalStatuses(dataGridViewMarStatus, marStatusDTO);

            LoadDataGridView.loadNationalities(dataGridViewNationality, nationalityDTO);

            LoadDataGridView.loadPositions(dataGridViewPositions, positionDTO);

            LoadDataGridView.loadProfessions(dataGridViewProfessions, professionDTO);

            LoadDataGridView.loadPermissions(dataGridViewPermissions, memberDTO);
            FillPermissionComboBoxes();

            #region

            picProfilePic.SizeMode = PictureBoxSizeMode.StretchImage;
            picProfilePic.BorderStyle = BorderStyle.None;
            picProfilePic.Width = picProfilePic.Height = 40;
            picProfilePic.Paint += new PaintEventHandler(picProfilePic_Paint);

            memberDTO = memberBLL.Select();
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
                btnDeletePaymentStatus.Hide();
            }

            Counts();
        }
        private void FillPermissionComboBoxes()
        {
            cmbPermission.DataSource = permissionDTO.Permissions;
            GeneralHelper.ComboBoxProps(cmbPermission, "Permission", "PermissionID");
        }

        private void picProfilePic_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, picProfilePic.Width - 1, picProfilePic.Height - 1);
            Region rg = new Region(gp);
            picProfilePic.Region = rg;
        }

        

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
            txtPaymentStatus.Clear();
            paymentStatusBLL = new PaymentStatusBLL();
            paymentStatusDTO = paymentStatusBLL.Select();
            dataGridViewPaymentStatus.DataSource = paymentStatusDTO.PaymentStatuses;

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
            if (e.RowIndex < 0) return;
            countryDetail = GeneralHelper.MapFromGrid<CountryDetailDTO>(dataGridViewCountry, e.RowIndex);
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
            if (e.RowIndex < 0) return;
            empStatusDetail = GeneralHelper.MapFromGrid<EmploymentStatusDetailDTO>(dataGridViewEmpStatus, e.RowIndex);
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
            if (e.RowIndex < 0) return;
            marStatusDetail = GeneralHelper.MapFromGrid<MaritalStatusDetailDTO>(dataGridViewMarStatus, e.RowIndex);            
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
            if (e.RowIndex < 0) return;
            nationalityDetail = GeneralHelper.MapFromGrid<NationalityDetailDTO>(dataGridViewNationality, e.RowIndex);            
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
            if (e.RowIndex < 0) return;
            permissionDetail = GeneralHelper.MapFromGrid<MemberDetailDTO>(dataGridViewPermissions, e.RowIndex);
        }

        private void btnClearPermissions_Click(object sender, EventArgs e)
        {
            ClearFilters();
        }

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
            if (e.RowIndex < 0) return;
            positionDetail = GeneralHelper.MapFromGrid<PositionDetailDTO>(dataGridViewPositions, e.RowIndex);
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
            if (e.RowIndex < 0) return;
            professionDetail = GeneralHelper.MapFromGrid<ProfessionDetailDTO>(dataGridViewProfessions, e.RowIndex);
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

        DeletedDataDTO deletedDataDTO = new DeletedDataDTO();
        DeletedDataBLL deletedDataBLL = new DeletedDataBLL();
        MemberDetailDTO deletedDataDetail = new MemberDetailDTO();
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
        FinedMemberBLL fineddeletedDataBLL = new FinedMemberBLL();
        FinedMemberDetailDTO fineddeletedDataDetail = new FinedMemberDetailDTO();

        private void cmbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0)
            {
                memberDTO = memberBLL.Select(true);
                LoadDataGridView.loadMembers(dataGridView1, memberDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                countryDTO = countryBLL.Select(true);
                LoadDataGridView.loadCountries(dataGridView1, countryDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                nationalityDTO = nationalityBLL.Select(true);
                LoadDataGridView.loadNationalities(dataGridView1, nationalityDTO);                
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                professionDTO = professionBLL.Select(true);
                LoadDataGridView.loadProfessions(dataGridView1, professionDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 4)
            {
                positionDTO = positionBLL.Select(true);
                LoadDataGridView.loadPositions(dataGridView1, positionDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 5)
            {
                empStatusDTO = empStatusBLL.Select(true);
                LoadDataGridView.loadEmploymentStatuses(dataGridView1, empStatusDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 6)
            {
                marStatusDTO = marStatusBLL.Select(true);
                LoadDataGridView.loadMaritalStatuses(dataGridView1, marStatusDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 8)
            {
                commentDTO = commentBLL.Select(true);
                LoadDataGridView.loadComments(dataGridView1, commentDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 9)
            {
                documentDTO = documentBLL.Select(true);
                LoadDataGridView.loadDocuments(dataGridView1, documentDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 10)
            {
                eventImageDTO = eventImageBLL.Select(true);
                LoadDataGridView.loadEventImages(dataGridView1, eventImageDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 11)
            {
                eventsDTO = eventsBLL.Select(true);
                LoadDataGridView.loadEvents(dataGridView1, eventsDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 12)
            {
                generalAttendanceDTO = generalAttendanceBLL.Select(true);
                LoadDataGridView.loadGeneralAttendances(dataGridView1, generalAttendanceDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 13)
            {
                expenditureDTO = expenditureBLL.Select(true);
                LoadDataGridView.loadExpenditure(dataGridView1, expenditureDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 14)
            {
                financialReportDTO = financialReportBLL.Select(true);
                LoadDataGridView.loadFinancialReport(dataGridView1, financialReportDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 15)
            {
                constitutionDTO = constitutionBLL.Select(true);
                LoadDataGridView.loadConstitution(dataGridView1, constitutionDTO);
            }
            else if (cmbDeletedData.SelectedIndex == 16)
            {
                finedMemberDTO = finedMemberBLL.Select(true);
                LoadDataGridView.loadFinedMembers(dataGridView1, finedMemberDTO);
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
                if (e.RowIndex < 0) return;
                deletedDataDetail = GeneralHelper.MapFromGrid<MemberDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                if (e.RowIndex < 0) return;
                countryDeletedDataDetail = GeneralHelper.MapFromGrid<CountryDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                if (e.RowIndex < 0) return;
                nationalityDeletedDataDetail = GeneralHelper.MapFromGrid<NationalityDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                if (e.RowIndex < 0) return;
                professionDeletedDataDetail = GeneralHelper.MapFromGrid<ProfessionDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 4)
            {
                if (e.RowIndex < 0) return;
                positionDeletedDataDetail = GeneralHelper.MapFromGrid<PositionDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 5)
            {
                if (e.RowIndex < 0) return;
                empStatusDeletedDataDetail = GeneralHelper.MapFromGrid<EmploymentStatusDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 6)
            {
                if (e.RowIndex < 0) return;
                marStatusDeletedDataDetail = GeneralHelper.MapFromGrid<MaritalStatusDetailDTO>(dataGridView1, e.RowIndex);
            }            
            else if (cmbDeletedData.SelectedIndex == 8)
            {
                if (e.RowIndex < 0) return;
                commentDeletedDataDetail = GeneralHelper.MapFromGrid<CommentDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 9)
            {
                if (e.RowIndex < 0) return;
                documentDeletedDataDetail = GeneralHelper.MapFromGrid<DocumentDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 10)
            {
                if (e.RowIndex < 0) return;
                eventImageDeletedDataDetail = GeneralHelper.MapFromGrid<EventImageDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 11)
            {
                if (e.RowIndex < 0) return;
                eventDeletedDataDetail = GeneralHelper.MapFromGrid<EventsDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 12)
            {
                if (e.RowIndex < 0) return;
                genAttendDeletedDataDetail = GeneralHelper.MapFromGrid<GeneralAttendanceDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 13)
            {
                if (e.RowIndex < 0) return;
                expenditureDeletedDataDetail = GeneralHelper.MapFromGrid<ExpenditureDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 14)
            {
                if (e.RowIndex < 0) return;
                financialRepDeletedDataDetail = GeneralHelper.MapFromGrid<FinancialReportDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 15)
            {
                if (e.RowIndex < 0) return;
                constitutionDeletedDataDetail = GeneralHelper.MapFromGrid<ConstitutionDetailDTO>(dataGridView1, e.RowIndex);
            }
            else if (cmbDeletedData.SelectedIndex == 16)
            {
                if (e.RowIndex < 0) return;
                fineddeletedDataDetail = GeneralHelper.MapFromGrid<FinedMemberDetailDTO>(dataGridView1, e.RowIndex);
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (cmbDeletedData.SelectedIndex == 0 || cmbDeletedData.SelectedIndex == -1)
            {
                if (deletedDataDetail.MemberID == 0)
                {
                    MessageBox.Show("Please choose member from the table");
                }
                else
                {
                    if (deletedDataDetail.isCountryDeleted)
                    {
                        MessageBox.Show("Country was deleted. Get back the country first.");
                    }
                    else if (deletedDataDetail.isNationalityDeleted)
                    {
                        MessageBox.Show("Nationality was deleted. Get back the nationality first.");
                    }
                    else if (deletedDataDetail.isProfessionDeleted)
                    {
                        MessageBox.Show("Profession was deleted. Get back the profession first.");
                    }
                    else if (deletedDataDetail.isPositionDeleted)
                    {
                        MessageBox.Show("Position was deleted. Get back the position first.");
                    }
                    else if (deletedDataDetail.isEmpStatusDeleted)
                    {
                        MessageBox.Show("Employment status was deleted. Get back the employment status first.");
                    }
                    else if (deletedDataDetail.isMarStatusDeleted)
                    {
                        MessageBox.Show("marital status was deleted. Get back the marital status first.");
                    }
                    else
                    {
                        if (memberBLL.GetBack(deletedDataDetail))
                        {
                            MessageBox.Show("Member was retrieved");
                            deletedDataDTO = deletedDataBLL.Select(true);
                            dataGridView1.DataSource = deletedDataDTO.Members;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Countries;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Nationalities;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Professions;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Positions;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.EmploymentStatuses;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.MaritalStatuses;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Comments;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Documents;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.EventImages;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Events;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.GeneralAttendance;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Expenditures;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.FinancialReports;
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
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.Constitutions;
                    }
                }
            }
            else if (cmbDeletedData.SelectedIndex == 16)
            {
                if (fineddeletedDataDetail.FinedMemberID == 0)
                {
                    MessageBox.Show("Please choose a fined member from the table");
                }
                else
                {
                    if (fineddeletedDataBLL.GetBack(fineddeletedDataDetail))
                    {
                        MessageBox.Show("Fined member was retrieved");
                        deletedDataDTO = deletedDataBLL.Select(true);
                        dataGridView1.DataSource = deletedDataDTO.FinedMembers;
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

        private void btnAddPaymentStatus_Click(object sender, EventArgs e)
        {
            FormPaymentStatus open = new FormPaymentStatus();
            this.Hide();
            open.ShowDialog();
            this.Visible = true;
            ClearFilters();
        }

        private void btnUpdatePaymentStatus_Click(object sender, EventArgs e)
        {
            if (paymentStatusDetail.PaymentStatusID == 0)
            {
                MessageBox.Show("Please select a payment status from the table");
            }
            else
            {
                FormPaymentStatus open = new FormPaymentStatus();
                open.detail = paymentStatusDetail;
                open.isUpdate = true;
                this.Hide();
                open.ShowDialog();
                this.Visible = true;
                ClearFilters();
            }
        }

        private void btnDeletePaymentStatus_Click(object sender, EventArgs e)
        {
            if (paymentStatusDetail.PaymentStatusID == 0)
            {
                MessageBox.Show("Please select a payment status from the table");
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (paymentStatusBLL.Delete(paymentStatusDetail))
                    {
                        MessageBox.Show("Payment status was deleted successfully");
                        ClearFilters();
                    }
                }
            }
        }

        private void dataGridViewPaymentStatus_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            paymentStatusDetail = GeneralHelper.MapFromGrid<PaymentStatusDetailDTO>(dataGridViewPaymentStatus, e.RowIndex);
        }
    }
}
