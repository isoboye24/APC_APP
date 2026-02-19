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

        PermissionBLL permissionBLL = new PermissionBLL();
        PermissionDTO permissionDTO = new PermissionDTO();
        MemberDetailDTO permissionDetail = new MemberDetailDTO();
        MemberBLL permissionMemberBLL = new MemberBLL();

        CommentDTO commentDTO = new CommentDTO();
        DocumentDTO documentDTO = new DocumentDTO();
        EventImageDTO eventImageDTO = new EventImageDTO();
        EventsDTO eventsDTO = new EventsDTO();
        EventSalesDTO eventSalesDTO = new EventSalesDTO();
        EventExpenditureDTO eventExpenditureDTO = new EventExpenditureDTO();
        EventReceiptsDTO eventReceiptsDTO = new EventReceiptsDTO();
        GeneralAttendanceDTO generalAttendanceDTO = new GeneralAttendanceDTO();


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
            memberDeletedDataDTO = memberDeletedDataBLL.Select(true);

            loadMembers(dataGridView1);

            loadPaymentStatuses(dataGridViewPaymentStatus);

            loadCountries(dataGridViewCountry);
            
            loadEmploymentStatuses(dataGridViewEmpStatus);
            
            loadMaritalStatuses(dataGridViewMarStatus);

            loadNationalities(dataGridViewNationality);

            loadPositions(dataGridViewPositions);

            loadProfessions(dataGridViewProfessions);

            loadPermissions(dataGridViewPermissions);
            FillPermissionComboBoxes();

            #region

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

        private void loadPaymentStatuses(DataGridView grid)
        {
            grid.DataSource = paymentStatusDTO.PaymentStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "PaymentStatusName", "Payment Status");
        }

        private void loadPositions(DataGridView grid)
        {
            grid.DataSource = positionDTO.Positions;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "PositionName", "Positions");
        }
        private void loadEmploymentStatuses(DataGridView grid)
        {
            grid.DataSource = empStatusDTO.EmploymentStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "EmploymentStatus", "Employment Statuses");
        }
        private void loadMaritalStatuses(DataGridView grid)
        {
            grid.DataSource = marStatusDTO.MaritalStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "MaritalStatus", "Marital Statuses");
        }
        private void loadCountries(DataGridView grid)
        {
            grid.DataSource = countryDTO.Countries;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "CountryName", "Countries");
        }
        private void loadNationalities(DataGridView grid)
        {
            grid.DataSource = nationalityDTO.Nationalities;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "Nationality", "Nationalities");
        }
        
        private void loadProfessions(DataGridView grid)
        {
            grid.DataSource = professionDTO.Professions;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "Profession", "Professions");
        }
        private void FillPermissionComboBoxes()
        {            
            cmbPermission.DataSource = permissionDTO.Permissions;
            GeneralHelper.ComboBoxProps(cmbPermission, "Permission", "PermissionID");
        }

        private void loadPermissions(DataGridView grid)
        {
            grid.DataSource = permissionDTO.Members;
            ConfigureMemberGrid(grid, MemberGridType.Permission);
        }
        private void loadMembers(DataGridView grid)
        {
            grid.DataSource = memberDeletedDataDTO.Members;
            ConfigureMemberGrid(grid, MemberGridType.Basic);
        }

        private void loadComments(DataGridView grid)
        {
            grid.DataSource = commentDTO.Comments;
            ConfigureCommentGrid(grid, CommentGridType.Basic);
        }

        private void loadDocument(DataGridView grid)
        {
            grid.DataSource = documentDTO.Documents;
            ConfigureDocumentGrid(grid, DocumentGridType.Basic);
        }
        private void loadEventImages(DataGridView grid)
        {
            grid.DataSource = eventImageDTO.EventImages;
            ConfigureEventsGrid(grid, EventsGridType.Images);
        }

        private void loadEvents(DataGridView grid)
        {
            grid.DataSource = eventsDTO.Events;
            ConfigureEventsGrid(grid, EventsGridType.Basic);
        }

        private void loadEventSales(DataGridView grid)
        {
            grid.DataSource = eventSalesDTO.EventSales;
            ConfigureEventsGrid(grid, EventsGridType.Sales);
        }

        private void loadEventExpenditure(DataGridView grid)
        {
            grid.DataSource = eventExpenditureDTO.EventExpenditures;
            ConfigureEventsGrid(grid, EventsGridType.Expenditure);
        }

        private void loadEventReceipt(DataGridView grid)
        {
            grid.DataSource = eventReceiptsDTO.EventReceipts;
            ConfigureEventsGrid(grid, EventsGridType.Receipt);
        }

        private void loadGeneralAttendance(DataGridView grid)
        {
            grid.DataSource = generalAttendanceDTO.GeneralAttendance;
            ConfigureGeneralAttendanceGrid(grid, GeneralAttendanceGridType.Basic);
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
                loadMembers(dataGridView1);                
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                loadCountries(dataGridView1);
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                loadNationalities(dataGridView1);
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                loadProfessions(dataGridView1);
            }
            else if (cmbDeletedData.SelectedIndex == 4)
            {
                loadPositions(dataGridView1);
            }
            else if (cmbDeletedData.SelectedIndex == 5)
            {
                loadEmploymentStatuses(dataGridView1);
            }
            else if (cmbDeletedData.SelectedIndex == 6)
            {
                loadMaritalStatuses(dataGridView1);                
            }
            else if (cmbDeletedData.SelectedIndex == 8)
            {
                loadComments(dataGridView1);                
            }
            else if (cmbDeletedData.SelectedIndex == 9)
            {
                loadDocument(dataGridView1);                
            }
            else if (cmbDeletedData.SelectedIndex == 10)
            {
                loadEventImages(dataGridView1);                
            }
            else if (cmbDeletedData.SelectedIndex == 11)
            {
                loadEvents(dataGridView1);                
            }
            else if (cmbDeletedData.SelectedIndex == 12)
            {
                loadGeneralAttendance(dataGridView1);                
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
                if (e.RowIndex < 0) return;
                eventDeletedDataDetail = GeneralHelper.MapFromGrid<EventsDetailDTO>(dataGridView1, e.RowIndex);
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
