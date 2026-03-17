using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class FinancialReportHelperService
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
                    GeneralHelper.SetVisibleColumns(grid, "Year", "TotalAmountRaisedWithCurrency",
                        "TotalAmountSpentWithCurrency", "Balance");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "TotalAmountRaisedWithCurrency", "Sold" },
                                    { "TotalAmountSpentWithCurrency", "Spent" }
                                });
                    break;
            }
        }
    }
}
