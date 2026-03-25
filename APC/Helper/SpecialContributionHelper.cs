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
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "FormattedAmountToContribute",
                        "FormattedAmountExpected", "FormattedTotalContributedAmount", "Status",
                        "FirstName", "FormattedContributionStartDate", "FormattedContributionEndDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "FormattedAmountExpected", "Amt. Exp." },
                                    { "FormattedTotalContributedAmount", "Amt. Cont." },
                                    { "FormattedAmountToContribute", "Amt. Each" },
                                    { "FirstName", "S. Name" },
                                    { "FormattedContributionStartDate", "Start Date" },
                                    { "FormattedContributionEndDate", "End Date" }
                                });
                    break;
            }
        }
    }
}
