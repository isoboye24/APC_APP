using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
{
    public class GeneralAttendanceHelperService
    {
        public enum GeneralAttendanceGridType
        {
            Basic
        }

        public static void ConfigureGeneralAttendanceGrid(DataGridView grid, GeneralAttendanceGridType type)
        {
            switch (type)
            {
                case GeneralAttendanceGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "Month", "Year", "TotalMembersPresent", "TotalMembersAbsent",
                        "TotalDuesPaid");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "TotalMembersPresent", "Present" },
                                    { "TotalMembersAbsent", "Absent" },
                                    { "TotalDuesPaid", "Dues Paid" },
                                });
                    break;
            }
        }
    }
}
