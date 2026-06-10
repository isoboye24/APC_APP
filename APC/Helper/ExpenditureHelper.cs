using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class ExpenditureHelper
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
                    GeneralHelper.SetVisibleColumns(grid, "Summary", "FormattedAmountSpent", "FormattedExpenditureDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FormattedAmountSpent", "Spent Amount" },
                                    { "FormattedExpenditureDate", "Date" },
                                });

                    grid.Columns["Summary"].DisplayIndex = 0;
                    grid.Columns["Summary"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    grid.Columns["FormattedAmountSpent"].DisplayIndex = 1;
                    grid.Columns["FormattedAmountSpent"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedExpenditureDate"].DisplayIndex = 2;
                    grid.Columns["FormattedExpenditureDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;
            }
        }
    }
}
