using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class PersonalAttendanceHelper
    {
        public enum PersonalAttendanceGridType
        {
            Basic,
            Details
        }

        public static void ConfigurePersonalAttendanceGrid(DataGridView grid, PersonalAttendanceGridType type)
        {
            switch (type)
            {
                case PersonalAttendanceGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "FirstName", "LastName", "Gender", "AttendanceStatus", "DuesPaid");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "AttendanceStatus", "Status" },
                                    { "DuesPaid", "Dues Paid" },
                                });
                    break;
                case PersonalAttendanceGridType.Details:
                    GeneralHelper.SetVisibleColumns(grid, "Month", "Year", "AttendanceStatus", "DuesPaid");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "AttendanceStatus", "Att. Status" },
                                    { "DuesPaid", "Dues Paid" },
                                });
                    break;
            }
        }
    }
}
