using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
{
    public class EventsHelperService
    {
        public enum EventsGridType
        {
            Basic,
            Images,
            Sales,
            Expenditure,
            Receipt,
        }

        public static void ConfigureEventsGrid(DataGridView grid, EventsGridType type)
        {
            switch (type)
            {
                case EventsGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "Year", "EventTitle", "AmountSoldWithCurrency", 
                        "AmountSpentWithCurrency");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "EventTitle", "Title" },
                                    { "AmountSoldWithCurrency", "Sold" },
                                    { "AmountSpentWithCurrency", "Spent" },
                                });
                    break;
                case EventsGridType.Images:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "ImageCaption");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "ImageCaption", "Caption" }
                                });
                    break;
                case EventsGridType.Sales:
                    GeneralHelper.SetVisibleColumns(grid, "Summary", "AmountSoldWithCurrency", "Day",
                        "MonthName", "Year");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "AmountSoldWithCurrency", "Sold" },
                                    { "MonthName", "Month" }
                                });
                    break;
                case EventsGridType.Expenditure:
                    GeneralHelper.SetVisibleColumns(grid, "Summary", "AmountSpentWithCurrency", "Day",
                        "MonthName", "Year");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "AmountSpentWithCurrency", "Spent" },
                                    { "MonthName", "Month" }
                                });
                    break;
                case EventsGridType.Receipt:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "ImageCaption", "AmountSpentWithCurrency", "Day",
                        "MonthName", "Year");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "ImageCaption", "Caption." },
                                    { "AmountSpentWithCurrency", "Spent" },
                                    { "MonthName", "Month" }
                                });
                    break;
                default:
                    GeneralHelper.SetVisibleColumns(grid, "", "", "");
                    break;
            }
        }
    }
}
