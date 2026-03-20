using APC.Applications.DTO;
using APC.BLL;
using APC.DAL.DTO;
using APC.Domain.Interfaces;
using APC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using static APC.Helper.SingleColumnHelper;


namespace APC.AllForms
{
    public partial class FormSettings : Form
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICountryService _countryService;
        private readonly IEmploymentStatusService _employmentStatusService;
        private readonly IMaritalStatusService _maritalStatusService;
        private readonly INationalityService _nationalityService;
        private readonly IPermissionService _permissionService;
        private readonly IPositionService _positionService;
        private readonly IProfessionService _professionService;
        private readonly IPaymentStatusService _paymentStatusService;

        private readonly IMemberService _memberService;
        private readonly ICommentService _commentService;
        private readonly IDocumentService _documentService;
        private readonly IEventImagesRepository _eventImagesService;

        private List<CountryDTO> _countryDTO;
        private List<Applications.DTO.EmploymentStatusDTO> _employmentStatusDTO;
        private List<Applications.DTO.MaritalStatusDTO> _maritalStatusDTO;
        private List<Applications.DTO.NationalityDTO> _nationalityDTO;
        private List<Applications.DTO.PermissionDTO> _permissionDTO;
        private List<Applications.DTO.PositionDTO> _positionDTO;
        private List<Applications.DTO.ProfessionDTO> _professionDTO;
        private List<Applications.DTO.PaymentStatusDTO> _paymentStatusDTO;
        private List<Applications.DTO.MembersBasicDetailDTO> _memberBasicDTO;
        private List<Applications.DTO.EventImageDTO> _eventImagesDTO;

        public FormSettings(ICountryService countryService, ICurrentUserService currentUserService, IEmploymentStatusService employmentStatusService,
            IMaritalStatusService maritalStatusService, INationalityService nationalityService, IPermissionService permissionService, 
            IPositionService positionService, IProfessionService professionService, IPaymentStatusService paymentStatusService, 
            IMemberService memberService, ICommentService commentService, IDocumentService documentService, IEventImagesRepository eventImagesService)
        {
            InitializeComponent();
            _countryService = countryService;
            _currentUserService = currentUserService;
            _employmentStatusService = employmentStatusService;
            _maritalStatusService = maritalStatusService;
            _nationalityService = nationalityService;
            _permissionService = permissionService;
            _positionService = positionService;
            _professionService = professionService;
            _paymentStatusService = paymentStatusService;
            _memberService = memberService;
            _commentService = commentService;
            _documentService = documentService;
            _eventImagesService = eventImagesService;
        }   
        
        EventsBLL eventBLL = new EventsBLL();

        MemberDetailDTO permissionDetail = new MemberDetailDTO();
        MemberBLL permissionMemberBLL = new MemberBLL();

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

        private void loadPaymentStatus()
        {
            dataGridViewPaymentStatus.DataSource = _paymentStatusService.GetAll();
            ConfigureSingleColumnGrid(dataGridViewPaymentStatus, SingleColumnGridType.Basic, "PaymentStatusName", "Payment Statuses");
        }
        private void loadDeletedPaymentStatuses()
        {
            dataGridView1.DataSource = _paymentStatusService.GetAllDeletedPaymentStatuses();
            ConfigureSingleColumnGrid(dataGridView1, SingleColumnGridType.Basic, "PaymentStatusName", "Payment Statuses");
        }

        private void loadCountries()
        {
            dataGridViewCountry.DataSource = _countryService.GetAll();
            ConfigureSingleColumnGrid(dataGridViewCountry, SingleColumnGridType.Basic, "CountryName", "Countries");
        }
        private void loadDeletedCountries()
        {
            dataGridView1.DataSource = _countryService.GetAllDeletedCountries();
            ConfigureSingleColumnGrid(dataGridView1, SingleColumnGridType.Basic, "CountryName", "Countries");
        }

        private void loadEmploymentStatus()
        {
            dataGridViewEmpStatus.DataSource = _employmentStatusService.GetAll();
            ConfigureSingleColumnGrid(dataGridViewEmpStatus, SingleColumnGridType.Basic, "EmploymentStatusName", "Employment Statuses");
        }
        private void loadDeletedEmploymentStatuses()
        {
            dataGridView1.DataSource = _employmentStatusService.GetAllEmploymentStatuses();
            ConfigureSingleColumnGrid(dataGridView1, SingleColumnGridType.Basic, "EmploymentStatusName", "Employment Statuses");
        }

        private void loadMaritalStatus()
        {
            dataGridViewMarStatus.DataSource = _maritalStatusService.GetAll();
            ConfigureSingleColumnGrid(dataGridViewMarStatus, SingleColumnGridType.Basic, "MaritalStatusName", "Marital Statuses");
        }
        private void loadDeletedMaritalStatuses()
        {
            dataGridView1.DataSource = _maritalStatusService.GetAllMaritalStatuses();
            ConfigureSingleColumnGrid(dataGridView1, SingleColumnGridType.Basic, "MaritalStatusName", "Marital Statuses");
        }

        private void loadNationality()
        {
            dataGridViewNationality.DataSource = _nationalityService.GetAll();
            ConfigureSingleColumnGrid(dataGridViewNationality, SingleColumnGridType.Basic, "NationalityName", "Nationalities");
        }
        private void loadDeletedNationalities()
        {
            dataGridView1.DataSource = _nationalityService.GetAllDeletedNationalities();
            ConfigureSingleColumnGrid(dataGridView1, SingleColumnGridType.Basic, "NationalityName", "Nationalities");
        }
        
        private void loadPermission()
        {
            dataGridViewPermissions.DataSource = _permissionService.GetAll();
            ConfigureSingleColumnGrid(dataGridViewPermissions, SingleColumnGridType.Basic, "PermissionName", "Permissions");
        }
        private void loadDeletedPermissions()
        {
            dataGridView1.DataSource = _permissionService.GetAllDeletedPermissions();
            ConfigureSingleColumnGrid(dataGridView1, SingleColumnGridType.Basic, "PermissionName", "Permissions");
        }

        private void loadPosition()
        {
            dataGridViewPositions.DataSource = _positionService.GetAll();
            ConfigureSingleColumnGrid(dataGridViewPositions, SingleColumnGridType.Basic, "PositionName", "Positions");
        }
        private void loadDeletedPositions()
        {
            dataGridView1.DataSource = _positionService.GetAllDeletedPositions();
            ConfigureSingleColumnGrid(dataGridView1, SingleColumnGridType.Basic, "PositionName", "Positions");
        }

        private void loadProfession()
        {
            dataGridViewProfessions.DataSource = _professionService.GetAll();
            ConfigureSingleColumnGrid(dataGridViewProfessions, SingleColumnGridType.Basic, "ProfessionName", "Professions");
        }
        private void loadDeletedProfessions()
        {
            dataGridView1.DataSource = _professionService.GetAllDeletedProfessions();
            ConfigureSingleColumnGrid(dataGridView1, SingleColumnGridType.Basic, "ProfessionName", "Professions");
        }

        private void loadDeletedMembers()
        {
            dataGridView1.DataSource = _memberService.GetAllDeletedMembers();
            MemberHelper.ConfigureMemberGrid(dataGridView1, MemberHelper.MemberGridType.Basic);
        }
        private void loadDeletedComments()
        {
            dataGridView1.DataSource = _commentService.GetAllDeletedComments();
            CommentHelper.ConfigureCommentGrid(dataGridView1, CommentHelper.CommentGridType.Basic);
        }
        private void loadDeletedDocuments()
        {
            dataGridView1.DataSource = _documentService.GetAllDeletedDocuments();
            DocumentHelper.ConfigureDocumentGrid(dataGridView1, DocumentHelper.DocumentGridType.Basic);
        }
        private void loadDeletedEventImages()
        {
            dataGridView1.DataSource = _eventImagesService.GetAllDeletedEventImages();
            EventsHelper.ConfigureEventsGrid(dataGridView1, EventsHelper.EventsGridType.Images);
        }



        private void resizeControls() 
        {
            GeneralHelper.ApplyBoldFont(14,
                label1, label2, label3, label4, label5, label6, label7, label8,
                label9, label10, label11, label12, label16, label19, btnAddCountry, btnUpdateCountry, btnDeleteCountry,
                 btnAddEmpStatus, btnUpdateEmpStatus, btnDeleteEmpStatus, btnAddMarStatus, btnUpdateMarStatus, btnDeleteMarStatus, 
                 btnAddNationality, btnAddProfessions, btnUpdateProfessions, btnDeleteProfessions, cmbDeletedData, btnRetrieve
                btnUpdateNationality, btnDeleteNationality, btnDeletePermissions, btnAddPositions, btnUpdatePositions, btnDeletePositions
                );

            GeneralHelper.ApplyRegularFont(11,
                labelTotalCountry, labelTotalEmpStatus, labelTotalMarStatus, labelTotalNationality,
                labelTotalPermissions, labelTotalPositions, labelTotalProfessions
                );
            
            GeneralHelper.ApplyRegularFont(14,
                txtCountry, txtMaritalStatus, txtEmpStatus, txtNationality, txtPosition,
                txtProfession
                );
        }
        private void FormSettings_Load(object sender, EventArgs e)
        {
            // Controls sizes
            resizeControls();

            loadPaymentStatus();
            loadCountries();
            loadEmploymentStatus();
            loadMaritalStatus();
            loadNationality();
            loadPosition();
            loadProfession();
            loadPermission();

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
            cmbDeletedData.Items.Add("Payment Status");
            cmbDeletedData.Items.Add("Permissions");

            #endregion

            if (_currentUserService.AccessLevel != 4)
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

        private void ClearFilters()
        {
            txtPaymentStatus.Clear();
            loadPaymentStatus();

            txtCountry.Clear();
            loadCountries();

            txtEmpStatus.Clear();
            loadEmploymentStatus();

            txtMaritalStatus.Clear();
            loadMaritalStatus();

            txtNationality.Clear();
            loadNationality();

            txtPermission.Clear();
            loadPermission();

            txtPosition.Clear();
            loadPosition();

            txtProfession.Clear();
            loadProfession();

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

            labelTotalProfessionSettingsDashboard.Text = _memberService.GetUniqueProfessionCount().ToString();
            labelTotalPositionSettingsDashboard.Text = _memberService.GetUniquePositionCount().ToString();
            labelTotalNationalitySettingsDashboard.Text = _memberService.GetUniqueNationalityCount().ToString();
            labelTotalPermissionSettingsDashboard.Text = _memberService.GetUniquePermissionCount().ToString();
            labelTotalEventSettingsDashboard.Text = eventBLL.SelectEventCount().ToString();
        }

        // Payment Status
        private void btnAddPaymentStatus_Click(object sender, EventArgs e)
        {
            var form = new FormPaymentStatus(_paymentStatusService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.PaymentStatusDTO GetSelectedPaymentStatus()
        {
            if (dataGridViewPaymentStatus.CurrentRow == null)
                return null;

            return dataGridViewPaymentStatus.CurrentRow.DataBoundItem as Applications.DTO.PaymentStatusDTO;
        }

        private void btnUpdatePaymentStatus_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedPaymentStatus();
            if (selected == null)
            {
                MessageBox.Show("Please select a payment status from the table");
                return;
            }

            var form = new FormPaymentStatus(_paymentStatusService);
            form.LoadForEdit(selected.PaymentStatusId, selected.PaymentStatusName, true);
            form.ShowDialog();
            ClearFilters();
        }

        private void txtPaymentStatus_TextChanged(object sender, EventArgs e)
        {
            string search = txtPaymentStatus.Text.Trim().ToLower();
            var filtered = _paymentStatusDTO.Where(x => x.PaymentStatusName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewPaymentStatus.DataSource = filtered;
        }

        private void btnDeletePaymentStatus_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedPaymentStatus();
            if (selected == null)
            {
                MessageBox.Show("Please select a payment status from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _paymentStatusService.Delete(selected.PaymentStatusId);
                ClearFilters();
            }
        }

        // Country
        private void btnAddCountry_Click(object sender, EventArgs e)
        {
            var form = new FormCountry(_countryService);
            form.ShowDialog();
            ClearFilters();
        }

        private CountryDTO GetSelectedCountry()
        {
            if (dataGridViewCountry.CurrentRow == null)
                return null;

            return dataGridViewCountry.CurrentRow.DataBoundItem as CountryDTO;
        }

        private void btnUpdateCountry_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedCountry();
            if (selected == null)
            {
                MessageBox.Show("Please select a country from the table");
                return;
            }

            var form = new FormCountry(_countryService);
            form.LoadForEdit(selected.CountryId, selected.CountryName, true);
            form.ShowDialog();

            ClearFilters();
        }

        private void txtCountry_TextChanged(object sender, EventArgs e)
        {
            string search = txtCountry.Text.Trim().ToLower();
            var filtered = _countryDTO.Where(x => x.CountryName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewCountry.DataSource = filtered;
        }

        private void btnDeleteCountry_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedCountry();
            if (selected == null)
            {
                MessageBox.Show("Please select a country from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _countryService.Delete(selected.CountryId);
                ClearFilters();
            }
        }

        // Employment status
        private void btnAddEmpStatus_Click(object sender, EventArgs e)
        {
            var form = new FormEmploymentStatus(_employmentStatusService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.EmploymentStatusDTO GetSelectedEmploymentStatus()
        {
            if (dataGridViewEmpStatus.CurrentRow == null)
                return null;

            return dataGridViewEmpStatus.CurrentRow.DataBoundItem as Applications.DTO.EmploymentStatusDTO;
        }

        private void btnUpdateEmpStatus_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEmploymentStatus();
            if (selected == null)
            {
                MessageBox.Show("Please select an employment status from the table");
                return;
            }

            var form = new FormEmploymentStatus(_employmentStatusService);
            form.LoadForEdit(selected.EmploymentStatusId, selected.EmploymentStatusName, true);
            form.ShowDialog();
            ClearFilters();
        }

        private void txtEmpStatus_TextChanged(object sender, EventArgs e)
        {
            string search = txtEmpStatus.Text.Trim().ToLower();
            var filtered = _employmentStatusDTO.Where(x => x.EmploymentStatusName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewEmpStatus.DataSource = filtered;
        }

        private void btnDeleteEmpStatus_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedEmploymentStatus();
            if (selected == null)
            {
                MessageBox.Show("Please select an employment status from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _employmentStatusService.Delete(selected.EmploymentStatusId);
                ClearFilters();
            }
        }

        // Marital status
        private void btnAddMarStatus_Click(object sender, EventArgs e)
        {
            var form = new FormMaritalStatus(_maritalStatusService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.MaritalStatusDTO GetSelectedMaritalStatus()
        {
            if (dataGridViewMarStatus.CurrentRow == null)
                return null;

            return dataGridViewMarStatus.CurrentRow.DataBoundItem as Applications.DTO.MaritalStatusDTO;
        }

        private void btnUpdateMarStatus_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMaritalStatus();
            if (selected == null)
            {
                MessageBox.Show("Please select a marital status from the table");
                return;
            }

            var form = new FormMaritalStatus(_maritalStatusService);
            form.LoadForEdit(selected.MaritalStatusId, selected.MaritalStatusName, true);
            form.ShowDialog();
            ClearFilters();
        }

        private void txtMaritalStatus_TextChanged(object sender, EventArgs e)
        {
            string search = txtMaritalStatus.Text.Trim().ToLower();
            var filtered = _maritalStatusDTO.Where(x => x.MaritalStatusName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewMarStatus.DataSource = filtered;
        }

        private void btnDeleteMarStatus_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedMaritalStatus();
            if (selected == null)
            {
                MessageBox.Show("Please select a marital status from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _maritalStatusService.Delete(selected.MaritalStatusId);
                ClearFilters();
            }
        }

        // Nationality
        private void btnAddNationality_Click(object sender, EventArgs e)
        {
            var form = new FormNationality(_nationalityService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.NationalityDTO GetSelectedNationality()
        {
            if (dataGridViewNationality.CurrentRow == null)
                return null;

            return dataGridViewNationality.CurrentRow.DataBoundItem as Applications.DTO.NationalityDTO;
        }

        private void btnUpdateNationality_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedNationality();
            if (selected == null)
            {
                MessageBox.Show("Please select a nationality from the table");
                return;
            }

            var form = new FormNationality(_nationalityService);
            form.LoadForEdit(selected.NationalityId, selected.NationalityName, true);
            form.ShowDialog();
            ClearFilters();
        }

        private void txtNationality_TextChanged(object sender, EventArgs e)
        {
            string search = txtNationality.Text.Trim().ToLower();
            var filtered = _nationalityDTO.Where(x => x.NationalityName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewNationality.DataSource = filtered;
        }

        private void btnDeleteNationality_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedNationality();
            if (selected == null)
            {
                MessageBox.Show("Please select a nationality from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _nationalityService.Delete(selected.NationalityId);
                ClearFilters();
            }
        }

        // Permission
        private Applications.DTO.PermissionDTO GetSelectedPermission()
        {
            if (dataGridViewPermissions.CurrentRow == null)
                return null;

            return dataGridViewPermissions.CurrentRow.DataBoundItem as Applications.DTO.PermissionDTO;
        }

        private void btnDeletePermissions_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedPermission();
            if (selected == null)
            {
                MessageBox.Show("Please select a permission from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _permissionService.Delete(selected.PermissionId);
                ClearFilters();
            }
        }

        // Position
        private void btnAddPositions_Click(object sender, EventArgs e)
        {
            var form = new FormPosition(_positionService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.PositionDTO GetSelectedPosition()
        {
            if (dataGridViewPositions.CurrentRow == null)
                return null;

            return dataGridViewPositions.CurrentRow.DataBoundItem as Applications.DTO.PositionDTO;
        }

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {
            string search = txtPosition.Text.Trim().ToLower();
            var filtered = _positionDTO.Where(x => x.PositionName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewPositions.DataSource = filtered;
        }

        private void btnUpdatePositions_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedPosition();
            if (selected == null)
            {
                MessageBox.Show("Please select a position from the table");
                return;
            }

            var form = new FormPosition(_positionService);
            form.LoadForEdit(selected.PositionId, selected.PositionName, true);
            form.ShowDialog();
            ClearFilters();
        }

        private void btnDeletePositions_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedPosition();
            if (selected == null)
            {
                MessageBox.Show("Please select a position from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _positionService.Delete(selected.PositionId);
                ClearFilters();
            }
        }

        // Profession
        private void btnAddProfessions_Click(object sender, EventArgs e)
        {
            var form = new FormProfession(_professionService);
            form.ShowDialog();
            ClearFilters();
        }

        private Applications.DTO.ProfessionDTO GetSelectedProfession()
        {
            if (dataGridViewProfessions.CurrentRow == null)
                return null;

            return dataGridViewProfessions.CurrentRow.DataBoundItem as Applications.DTO.ProfessionDTO;
        }

        private void btnUpdateProfessions_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProfession();
            if (selected == null)
            {
                MessageBox.Show("Please select a profession from the table");
                return;
            }

            var form = new FormProfession(_professionService);
            form.LoadForEdit(selected.ProfessionId, selected.ProfessionName, true);
            form.ShowDialog();
            ClearFilters();
        }

        private void txtProfession_TextChanged(object sender, EventArgs e)
        {
            string search = txtProfession.Text.Trim().ToLower();
            var filtered = _professionDTO.Where(x => x.ProfessionName.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            dataGridViewProfessions.DataSource = filtered;
        }

        private void btnDeleteProfessions_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedProfession();
            if (selected == null)
            {
                MessageBox.Show("Please select a profession from the table.");
                return;
            }

            var result = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _professionService.Delete(selected.ProfessionId);
                ClearFilters();
            }
        }


        MemberDetailDTO deletedDataDetail = new MemberDetailDTO();
        CountryBLL countryDeletedDataBLL = new CountryBLL();
        CountryDTO countryDeletedDataDetail = new CountryDTO();
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
                loadDeletedMembers();
            }
            else if (cmbDeletedData.SelectedIndex == 1)
            {
                loadDeletedCountries();
            }
            else if (cmbDeletedData.SelectedIndex == 2)
            {
                loadDeletedNationalities();                
            }
            else if (cmbDeletedData.SelectedIndex == 3)
            {
                loadDeletedProfessions();
            }
            else if (cmbDeletedData.SelectedIndex == 4)
            {
                loadDeletedPositions();
            }
            else if (cmbDeletedData.SelectedIndex == 5)
            {
                loadDeletedEmploymentStatuses();                
            }
            else if (cmbDeletedData.SelectedIndex == 6)
            {
                loadDeletedMaritalStatuses();
            }
            else if (cmbDeletedData.SelectedIndex == 8)
            {
                loadDeletedComments();
            }
            else if (cmbDeletedData.SelectedIndex == 9)
            {
                loadDeletedDocuments();                
            }
            else if (cmbDeletedData.SelectedIndex == 10)
            {
                loadDeletedEventImages();
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
            else if (cmbDeletedData.SelectedIndex == 17)
            {
                loadDeletedPaymentStatuses();
            }
            else if (cmbDeletedData.SelectedIndex == 18)
            {
                loadDeletedPermissions();
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
                countryDeletedDataDetail = GeneralHelper.MapFromGrid<CountryDTO>(dataGridView1, e.RowIndex);
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
                            loadDeletedMembers();
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
                        loadDeletedCountries();
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
                        loadDeletedNationalities();
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
                        loadDeletedProfessions();
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
                        loadDeletedPositions();
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
                        loadDeletedEmploymentStatuses();
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
                        loadDeletedMaritalStatuses();
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
                        loadDeletedComments();
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
                        loadDeletedDocuments();
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
                        eventImageDTO = eventImageBLL.Select(true);
                        dataGridView1.DataSource = eventImageDTO.EventImages;
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
                        eventsDTO = eventsBLL.Select(true);
                        dataGridView1.DataSource = eventsDTO.Events;
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
                        generalAttendanceDTO = generalAttendanceBLL.Select(true);
                        dataGridView1.DataSource = generalAttendanceDTO.GeneralAttendance;
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
                        expenditureDTO = expenditureBLL.Select(true);
                        dataGridView1.DataSource = expenditureDTO.Expenditures;
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
                        financialReportDTO = financialReportBLL.Select(true);
                        dataGridView1.DataSource = financialReportDTO.FinancialReports;
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
                        constitutionDTO = constitutionBLL.Select(true);
                        dataGridView1.DataSource = constitutionDTO.Constitutions;
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
                        finedMemberDTO = finedMemberBLL.Select(true);
                        dataGridView1.DataSource = finedMemberDTO.FineMembers;
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
