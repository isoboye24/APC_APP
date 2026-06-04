using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.Helper
{
    public class AttendanceStatusHelper
    {
        public enum AttendanceStatusGridType
        {
            Basic
        }

        public static void ConfigureAttendanceStatusGrid(DataGridView grid, AttendanceStatusGridType type)
        {
            switch (type)
            {
                case AttendanceStatusGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "AttendanceStatusName");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "AttendanceStatusName", "Status" },                                    
                                });
                    break;
                default:
                    GeneralHelper.SetVisibleColumns(grid, "", "");
                    break;
            }
        }
    }
}
