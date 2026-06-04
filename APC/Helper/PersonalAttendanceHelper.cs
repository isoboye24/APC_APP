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
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "AttendanceStatus", "DuesPaid", "Gender");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
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

                    grid.Columns["FirstName"].DisplayIndex = 0;
                    grid.Columns["LastName"].DisplayIndex = 1;
                    grid.Columns["AttendanceStatus"].DisplayIndex = 2;
                    grid.Columns["DuesPaid"].DisplayIndex = 3;
                    grid.Columns["Gender"].DisplayIndex = 4;
                    break;
            }
        }
    }
}
