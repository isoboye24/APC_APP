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
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "AmountToContributeWithCurrency",
                        "AmountExpectedWithCurrency", "AmountContributedWithCurrency", "Status",
                        "SupervisorName", "StartDate", "EndDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "AmountExpectedWithCurrency", "Amt. Exp." },
                                    { "AmountContributedWithCurrency", "Amt. Cont." },
                                    { "AmountToContributeWithCurrency", "Amt. Each" },
                                    { "SupervisorName", "S. Name" },
                                    { "SupervisorSurname", "S. Surname" },
                                    { "StartDate", "Started" },
                                    { "EndDate", "Ended" }
                                });
                    break;
            }
        }
    }
}
