using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static APC.HelperServices.EventsHelperService;

namespace APC.HelperServices
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
                        "MonthName", "Year");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "AmountSpentWithCurrency", "Spent" },
                                    { "MonthName", "Month" }
                                });
                    break;
            }
        }
    }
}
