using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class GeneralMeetingHelper
    {
        public enum GeneralMeetingGridType
        {
            Basic
        }

        public static void ConfigureGeneralMeetingGrid(DataGridView grid, GeneralMeetingGridType type)
        {
            switch (type)
            {
                case GeneralMeetingGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "MonthName", "Year", "TotalMembersPresent", "TotalMembersAbsent", "FormattedTotalDuesPaid");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "MonthName", "Month" },
                                    { "TotalMembersPresent", "Present" },
                                    { "TotalMembersAbsent", "Absent" },
                                    { "FormattedTotalDuesPaid", "Dues Paid" },
                                });
                    // Force column order
                    grid.Columns["MonthName"].DisplayIndex = 0;
                    grid.Columns["Year"].DisplayIndex = 1;
                    grid.Columns["TotalMembersPresent"].DisplayIndex = 2;
                    grid.Columns["TotalMembersAbsent"].DisplayIndex = 3;
                    grid.Columns["FormattedTotalDuesPaid"].DisplayIndex = 4;
                    break;
            }
        }
    }
}
