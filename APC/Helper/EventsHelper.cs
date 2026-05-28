using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class EventsHelper
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
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "EventTitle");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "EventTitle", "Title" },
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
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "Summary", "AmountSoldWithCurrency", "FormattedSalesDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "AmountSoldWithCurrency", "Sold" },
                                    { "FormattedSalesDate", "Date" }
                                });
                    break;
                case EventsGridType.Expenditure:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "Summary", "AmountSpentWithCurrency", "FormattedExpenditureDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "AmountSpentWithCurrency", "Amount" },
                                    { "FormattedExpenditureDate", "Date" }
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
