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
                    GeneralHelper.SetVisibleColumns(grid, "Name", "Surname", "ConstitutionShortDescription",
                        "ExpectedAmountWithCurrency", "AmountPaidWithCurrency", "FineStatus", "Day",
                        "MonthName", "Year");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ConstitutionShortDescription", "Violated" },
                                    { "ExpectedAmountWithCurrency", "Fine" },
                                    { "AmountPaidWithCurrency", "Paid" },
                                    { "FineStatus", "Status" },
                                    { "MonthName", "Month" }
                                });
                    break;
            }
        }
    }
}
