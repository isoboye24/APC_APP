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
                    GeneralHelper.SetVisibleColumns(grid, "Month", "Year", "TotalMembersPresent", "TotalMembersAbsent",
                        "TotalDuesPaidWithCurrency");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "TotalMembersPresent", "Present" },
                                    { "TotalMembersAbsent", "Absent" },
                                    { "TotalDuesPaidWithCurrency", "Dues Paid" },
                                });
                    break;
            }
        }
    }
}
