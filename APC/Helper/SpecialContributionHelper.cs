using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class SpecialContributionHelper
    {
        public enum SpecialContributionGridType
        {
            Basic
        }

        public static void ConfigureSpecialContributionGrid(DataGridView grid, SpecialContributionGridType type)
        {
            switch (type)
            {
                case SpecialContributionGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "FormattedAmountExpected", "FormattedAmountToContribute", "FormattedTotalContributedAmount", 
                        "Status", "FirstName", "FormattedContributionStartDate", "FormattedContributionEndDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FormattedAmountExpected", "Amt. Exp." },
                                    { "FormattedTotalContributedAmount", "Amt. Cont." },
                                    { "FormattedAmountToContribute", "Amt. Each" },
                                    { "FirstName", "S. Name" },
                                    { "FormattedContributionStartDate", "Start Date" },
                                    { "FormattedContributionEndDate", "End Date" }
                                });

                    grid.Columns["FormattedAmountExpected"].DisplayIndex = 0;
                    grid.Columns["FormattedAmountToContribute"].DisplayIndex = 1;
                    grid.Columns["FormattedTotalContributedAmount"].DisplayIndex = 2;
                    grid.Columns["Status"].DisplayIndex = 3;
                    grid.Columns["FirstName"].DisplayIndex = 4;
                    grid.Columns["FormattedContributionStartDate"].DisplayIndex = 5;
                    grid.Columns["FormattedContributionEndDate"].DisplayIndex = 6;
                    break;
            }
        }
    }
}
