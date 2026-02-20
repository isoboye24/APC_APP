using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
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
