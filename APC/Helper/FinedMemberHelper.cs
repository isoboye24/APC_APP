using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class FinedMemberHelper
    {
        public enum FinedMemberGridType
        {
            Basic
        }

        public static void ConfigureFinedMemberGrid(DataGridView grid, FinedMemberGridType type)
        {
            switch (type)
            {
                case FinedMemberGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "ShortDescription",
                        "AmountExpected", "AmountPaid", "Status", "FormattedFineDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "ShortDescription", "Violated" },
                                    { "AmountExpected", "Fine" },
                                    { "AmountPaid", "Paid" },
                                    { "FormattedFineDate", "Date" }
                                });
                    break;
            }
        }
    }
}
