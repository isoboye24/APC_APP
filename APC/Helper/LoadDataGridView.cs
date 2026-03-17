using APC.DAL.DTO;
using System;
using System.Windows.Forms;
using static APC.Helper.CommentHelperService;
using static APC.Helper.ConstitutionHelperService;
using static APC.Helper.DocumentHelperService;
using static APC.Helper.EventsHelperService;
using static APC.Helper.ExpenditureHelperService;
using static APC.Helper.FinancialReportHelperService;
using static APC.Helper.FinedMemberHelperService;
using static APC.Helper.GeneralAttendanceHelperService;
using static APC.Helper.MemberHelperService;
using static APC.Helper.SingleColumnHelperService;
using static APC.Helper.SpecialContributionHelperService;

namespace APC.Helper
{
    public class LoadDataGridView
    {
        public static void loadPaymentStatuses(DataGridView grid, PaymentStatusDTO dto)
        {
            grid.DataSource = dto.PaymentStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "PaymentStatusName", "Payment Status");
        }

        public static void loadPositions(DataGridView grid, PositionDTO dto)
        {
            grid.DataSource = dto.Positions;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "PositionName", "Positions");
        }
        public static void loadEmploymentStatuses(DataGridView grid, EmploymentStatusDTO dto)
        {
            grid.DataSource = dto.EmploymentStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "EmploymentStatus", "Employment Statuses");
        }
        public static void loadMaritalStatuses(DataGridView grid, MaritalStatusDTO dto)
        {
            grid.DataSource = dto.MaritalStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "MaritalStatus", "Marital Statuses");
        }
        public static void loadCountries(DataGridView grid, CountryDTO dto)
        {
            grid.DataSource = dto.Countries;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "CountryName", "Countries");
        }
        public static void loadNationalities(DataGridView grid, NationalityDTO dto)
        {
            grid.DataSource = dto.Nationalities;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "Nationality", "Nationalities");
        }

        public static void loadProfessions(DataGridView grid, ProfessionDTO dto)
        {
            grid.DataSource = dto.Professions;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "Profession", "Professions");
        }        

        public static void loadPermissions(DataGridView grid, MemberDTO dto)
        {
            grid.DataSource = dto.Members;
            ConfigureMemberGrid(grid, MemberGridType.Permission);
        }
        public static void loadMembers(DataGridView grid, MemberDTO dto)
        {
            grid.DataSource = dto.Members;
            ConfigureMemberGrid(grid, MemberGridType.Basic);
        }

        public static void loadBirthdayMembers(DataGridView grid, MemberDTO dto)
        {
            grid.DataSource = dto.Members;
            ConfigureMemberGrid(grid, MemberGridType.Birthday);
        }

        public static void loadDeadMembers(DataGridView grid, MemberDTO dto)
        {
            grid.DataSource = dto.Members;
            ConfigureMemberGrid(grid, MemberGridType.Dead);
        }

        public static void loadMembersContacts(DataGridView grid, MemberDTO dto)
        {
            grid.DataSource = dto.Members;
            ConfigureMemberGrid(grid, MemberGridType.Contact);
        }

        public static void loadMembersShrinked(DataGridView grid, MemberDTO dto)
        {
            grid.DataSource = dto.Members;
            ConfigureMemberGrid(grid, MemberGridType.Shrinked);
        }

        public static void loadComments(DataGridView grid, CommentDTO dto)
        {
            grid.DataSource = dto.Comments;
            ConfigureCommentGrid(grid, CommentGridType.Basic);
        }

        public static void loadDocuments(DataGridView grid, DocumentDTO dto)
        {
            grid.DataSource = dto.Documents;
            ConfigureDocumentGrid(grid, DocumentGridType.Basic);
        }
        public static void loadEventImages(DataGridView grid, EventImageDTO dto)
        {
            grid.DataSource = dto.EventImages;
            ConfigureEventsGrid(grid, EventsGridType.Images);
        }

        public static void loadEvents(DataGridView grid, EventsDTO dto)
        {
            grid.DataSource = dto.Events;
            ConfigureEventsGrid(grid, EventsGridType.Basic);
        }

        public static void loadEventSales(DataGridView grid, EventSalesDTO dto)
        {
            grid.DataSource = dto.EventSales;
            ConfigureEventsGrid(grid, EventsGridType.Sales);
        }

        public static void loadEventExpenditure(DataGridView grid, EventExpenditureDTO dto)
        {
            grid.DataSource = dto.EventExpenditures;
            ConfigureEventsGrid(grid, EventsGridType.Expenditure);
        }

        public static void loadEventReceipt(DataGridView grid, EventReceiptsDTO dto)
        {
            grid.DataSource = dto.EventReceipts;
            ConfigureEventsGrid(grid, EventsGridType.Receipt);
        }

        public static void loadGeneralAttendances(DataGridView grid, GeneralAttendanceDTO dto)
        {
            grid.DataSource = dto.GeneralAttendance;
            ConfigureGeneralAttendanceGrid(grid, GeneralAttendanceGridType.Basic);
        }
        public static void loadExpenditure(DataGridView grid, ExpenditureDTO dto)
        {
            grid.DataSource = dto.Expenditures;
            ConfigureExpenditureGrid(grid, ExpenditureGridType.Basic);
        }

        public static void loadFinancialReport(DataGridView grid, FinancialReportDTO dto)
        {
            grid.DataSource = dto.FinancialReports;
            ConfigureFinancialReportGrid(grid, FinancialReportGridType.Basic);
        }
       
        public static void loadConstitution(DataGridView grid, ConstitutionDTO dto)
        {
            grid.DataSource = dto.Constitutions;
            ConfigureConstitutionGrid(grid, ConstitutionGridType.Basic);
        }

        public static void loadFinedMembers(DataGridView grid, FinedMemberDTO dto)
        {
            grid.DataSource = dto.FineMembers;
            ConfigureFinedMemberGrid(grid, FinedMemberGridType.Basic);
        }
        
        public static void loadSpecialContributions(DataGridView grid, SpecialContributionDTO dto)
        {
            grid.DataSource = dto.SpecialContributions;
            ConfigureSpecialContributionGrid(grid, SpecialContributionGridType.Basic);
        }

    }
}
