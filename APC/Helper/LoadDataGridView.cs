using APC.Applications.DTO;
using APC.Domain.Entities;
using System.Collections.Generic;
using System.Windows.Forms;
using static APC.Helper.ConstitutionHelper;
using static APC.Helper.DocumentHelper;
using static APC.Helper.EventsHelper;
using static APC.Helper.ExpenditureHelper;
using static APC.Helper.FinancialReportHelper;
using static APC.Helper.FinedMemberHelper;
using static APC.Helper.GeneralMeetingHelper;
using static APC.Helper.MemberHelper;
using static APC.Helper.SingleColumnHelper;
using static APC.Helper.SpecialContributionHelper;
using static APC.Helper.SpecialContributorHelper;

namespace APC.Helper
{
    public class LoadDataGridView
    {
        public static void loadPaymentStatuses(DataGridView grid, List<PaymentStatusDTO> paymentStatuses)
        {
            grid.DataSource = paymentStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "PaymentStatusName", "Payment Status");
        }

        public static void loadPositions(DataGridView grid, List<PositionDTO> positions)
        {
            grid.DataSource = positions;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "PositionName", "Positions");
        }
        public static void loadEmploymentStatuses(DataGridView grid, List<EmploymentStatusDTO> employmentStatuses)
        {
            grid.DataSource = employmentStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "EmploymentStatus", "Employment Statuses");
        }
        public static void loadMaritalStatuses(DataGridView grid, List<MaritalStatusDTO> maritalStatuses)
        {
            grid.DataSource = maritalStatuses;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "MaritalStatus", "Marital Statuses");
        }
        public static void loadCountries(DataGridView grid, List<CountryDTO> countries)
        {
            grid.DataSource = countries;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "CountryName", "Countries");
        }
        public static void loadNationalities(DataGridView grid, List<NationalityDTO> nationalities)
        {
            grid.DataSource = nationalities;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "Nationality", "Nationalities");
        }

        public static void loadProfessions(DataGridView grid, List<ProfessionDTO> professions)
        {
            grid.DataSource = professions;
            ConfigureSingleColumnGrid(grid, SingleColumnGridType.Basic, "Profession", "Professions");
        }        

        public static void loadPermissions(DataGridView grid, List<MemberFullDetailsDTO> memberFullDetails)
        {
            grid.DataSource = memberFullDetails;
            ConfigureMemberGrid(grid, MemberGridType.Permission);
        }
        public static void loadMembers(DataGridView grid, List<MemberFullDetailsDTO> memberFullDetails)
        {
            grid.DataSource = memberFullDetails;
            ConfigureMemberGrid(grid, MemberGridType.SemiBasic);
        }

        public static void loadBirthdayMembers(DataGridView grid, List<MemberFullDetailsDTO> memberFullDetails)
        {
            grid.DataSource = memberFullDetails;
            ConfigureMemberGrid(grid, MemberGridType.Birthday);
        }

        public static void loadDeadMembers(DataGridView grid, List<MemberFullDetailsDTO> memberFullDetails)
        {
            grid.DataSource = memberFullDetails;
            ConfigureMemberGrid(grid, MemberGridType.Dead);
        }

        public static void loadMembersContacts(DataGridView grid, List<MemberFullDetailsDTO> memberFullDetails)
        {
            grid.DataSource = memberFullDetails;
            ConfigureMemberGrid(grid, MemberGridType.Contact);
        }

        public static void loadMembersShrinked(DataGridView grid, List<MemberFullDetailsDTO> memberFullDetails)
        {
            grid.DataSource = memberFullDetails;
            ConfigureMemberGrid(grid, MemberGridType.Basic);
        }

        public static void loadDocuments(DataGridView grid, List<DocumentDTO> documents)
        {
            grid.DataSource = documents;
            ConfigureDocumentGrid(grid, DocumentGridType.Basic);
        }
        public static void loadEventImages(DataGridView grid, List<EventImages> eventImages)
        {
            grid.DataSource = eventImages;
            ConfigureEventsGrid(grid, EventsGridType.Images);
        }

        public static void loadEvents(DataGridView grid, List<EventDTO> events)
        {
            grid.DataSource = events;
            ConfigureEventsGrid(grid, EventsGridType.Basic);
        }

        public static void loadEventSales(DataGridView grid, List<EventSalesDTO> eventSales)
        {
            grid.DataSource = eventSales;
            ConfigureEventsGrid(grid, EventsGridType.Sales);
        }

        public static void loadEventExpenditure(DataGridView grid, List<EventExpenditureDTO> eventExpenditures)
        {
            grid.DataSource = eventExpenditures;
            ConfigureEventsGrid(grid, EventsGridType.Expenditure);
        }

        public static void loadEventReceipt(DataGridView grid, List<EventReceiptDTO> eventReceipts)
        {
            grid.DataSource = eventReceipts;
            ConfigureEventsGrid(grid, EventsGridType.Receipt);
        }

        public static void loadGeneralAttendances(DataGridView grid, List<GeneralMeetingDTO> generalMeetings)
        {
            grid.DataSource = generalMeetings;
            ConfigureGeneralMeetingGrid(grid, GeneralMeetingGridType.Basic);
        }
        public static void loadExpenditure(DataGridView grid, List<ExpenditureDTO> expenditures)
        {
            grid.DataSource = expenditures;
            ConfigureExpenditureGrid(grid, ExpenditureGridType.Basic);
        }

        public static void loadFinancialReport(DataGridView grid, List<FinancialReportDTO> financialReports)
        {
            grid.DataSource = financialReports;
            ConfigureFinancialReportGrid(grid, FinancialReportGridType.Basic);
        }
       
        public static void loadConstitution(DataGridView grid, List<ConstitutionDTO> constitutions)
        {
            grid.DataSource = constitutions;
            ConfigureConstitutionGrid(grid, ConstitutionGridType.Basic);
        }

        public static void loadFinedMembers(DataGridView grid, List<FinedMemberDTO> finedMembers)
        {
            grid.DataSource = finedMembers;
            ConfigureFinedMemberGrid(grid, FinedMemberGridType.Basic);
        }
        
        public static void loadSpecialContributions(DataGridView grid, List<SpecialContributionDTO> specialContributions)
        {
            grid.DataSource = specialContributions;
            ConfigureSpecialContributionGrid(grid, SpecialContributionGridType.Basic);
        }
        
        public static void loadSpecialContributors(DataGridView grid, List<SpecialContributorDTO> specialContributors)
        {
            grid.DataSource = specialContributors;
            ConfigureSpecialContributorGrid(grid, SpecialContributorGridType.Basic);
        }

    }
}
