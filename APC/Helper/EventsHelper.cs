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
                    GeneralHelper.SetVisibleColumns(grid, "Title", "FormattedEventsDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FormattedEventsDate", "Date" },
                                });

                    grid.Columns["Title"].DisplayIndex = 0;
                    grid.Columns["FormattedEventsDate"].DisplayIndex = 1;
                    grid.Columns["FormattedEventsDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;

                case EventsGridType.Images:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "ImageCaption");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "ImageCaption", "Caption" }
                                });

                    grid.Columns["Counter"].DisplayIndex = 0;
                    grid.Columns["Counter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["ImageCaption"].DisplayIndex = 1;
                    break;

                case EventsGridType.Sales:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "Summary", "FormattedAmountSold", "FormattedSalesDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "FormattedAmountSold", "Sold" },
                                    { "FormattedSalesDate", "Date" }
                                });

                    grid.Columns["Counter"].DisplayIndex = 0;
                    grid.Columns["Counter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["Summary"].DisplayIndex = 1;
                    grid.Columns["FormattedAmountSold"].DisplayIndex = 2;
                    grid.Columns["FormattedAmountSold"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedSalesDate"].DisplayIndex = 3;
                    grid.Columns["FormattedSalesDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;

                case EventsGridType.Expenditure:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "Summary", "FormattedSpentAmount", "FormattedExpenditureDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "FormattedSpentAmount", "Amount" },
                                    { "FormattedExpenditureDate", "Date" }
                                });

                    grid.Columns["Counter"].DisplayIndex = 0;
                    grid.Columns["Counter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["Summary"].DisplayIndex = 1;
                    grid.Columns["FormattedSpentAmount"].DisplayIndex = 2;
                    grid.Columns["FormattedSpentAmount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedExpenditureDate"].DisplayIndex = 3;
                    grid.Columns["FormattedExpenditureDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;

                case EventsGridType.Receipt:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "Caption", "FormattedAmountSpent", "FormattedReceiptDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "FormattedAmountSpent", "Amount" },
                                    { "FormattedReceiptDate", "Date" }
                                });

                    grid.Columns["Counter"].DisplayIndex = 0;
                    grid.Columns["Counter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["Caption"].DisplayIndex = 1;
                    grid.Columns["FormattedAmountSpent"].DisplayIndex = 2;
                    grid.Columns["FormattedAmountSpent"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedReceiptDate"].DisplayIndex = 3;
                    grid.Columns["FormattedReceiptDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;

                default:
                    GeneralHelper.SetVisibleColumns(grid, "", "", "");
                    break;
            }
        }
    }
}
