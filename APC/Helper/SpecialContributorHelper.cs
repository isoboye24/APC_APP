using APC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.Helper
{
    public class SpecialContributorHelper
    {
        public enum SpecialContributorGridType
        {
            Basic
        }

        public static void ConfigureSpecialContributorGrid(DataGridView grid, SpecialContributorGridType type)
        {
            switch (type)
            {
                case SpecialContributorGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "Counter", "FirstName","LastName", "FormattedAmountExpected", 
                        "FormattedAmountContributed", "Balance" + "PaymentStatus", "FormattedContributedDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "Counter", "No." },
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "FormattedAmountExpected", "Expected" },
                                    { "FormattedAmountContributed", "Contributed" },
                                    { "PaymentStatus", "Status" },
                                    { "FormattedContributedDate", "Date" }
                                });
                    break;
            }
        }
    }
}
