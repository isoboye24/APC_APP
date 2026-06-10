using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class FinancialReportHelper
    {
        public enum FinancialReportGridType
        {
            Basic
        }

        public static void ConfigureFinancialReportGrid(DataGridView grid, FinancialReportGridType type)
        {
            switch (type)
            {
                case FinancialReportGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "Year", "FormattedTotalAmountRaised", "FormattedTotalAmountSpent");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FormattedTotalAmountRaised", "Sold" },
                                    { "FormattedTotalAmountSpent", "Spent" }
                                });

                    grid.Columns["Year"].DisplayIndex = 0;
                    grid.Columns["Year"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedTotalAmountRaised"].DisplayIndex = 1;
                    grid.Columns["FormattedTotalAmountSpent"].DisplayIndex = 2;
                    break;
            }
        }
    }
}
