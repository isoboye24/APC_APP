using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class ExpenditureHelperService
    {
        public enum ExpenditureGridType
        {
            Basic
        }

        public static void ConfigureExpenditureGrid(DataGridView grid, ExpenditureGridType type)
        {
            switch (type)
            {
                case ExpenditureGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "Summary", "AmountSpentWithCurrency", "Day",
                        "Month", "Year");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "AmountSpentWithCurrency", "Spent" },
                                });
                    break;
            }
        }
    }
}
