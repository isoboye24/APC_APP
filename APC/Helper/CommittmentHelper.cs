using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class CommittmentHelper
    {        
        public enum MemberCommittmentGridType
        {
            Basic,
            Fines,
            Dues
        }

        public static void ConfigureMemberCommittmentGrid(DataGridView grid, MemberCommittmentGridType type)
        {
            switch (type)
            {
                case MemberCommittmentGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "ShowRank", "FirstName", "LastName", "FormattedExpectedDues", "FormattedContributedDues",
                        "FormattedBalanceDues", "FormattedTotalFines", "FormattedPaidFines", "Status", "NoOfPresent", "NoOfAbsent");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShowRank", "Rank" },
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "FormattedExpectedDues", "Exp." },
                                    { "FormattedContributedDues", "Con." },
                                    { "FormattedBalanceDues", "Remaining" },
                                    { "FormattedTotalFines", "Fines" },
                                    { "FormattedPaidFines", "P. Fines" },
                                    { "NoOfPresent", "Pres." },
                                    { "NoOfAbsent", "Abs." }
                                });

                    grid.Columns["ShowRank"].DisplayIndex = 0;
                    grid.Columns["ShowRank"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FirstName"].DisplayIndex = 1;
                    grid.Columns["LastName"].DisplayIndex = 2;
                    grid.Columns["FormattedExpectedDues"].DisplayIndex = 3;
                    grid.Columns["FormattedExpectedDues"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedContributedDues"].DisplayIndex = 4;
                    grid.Columns["FormattedContributedDues"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedBalanceDues"].DisplayIndex = 5;
                    grid.Columns["FormattedBalanceDues"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedTotalFines"].DisplayIndex = 6;
                    grid.Columns["FormattedTotalFines"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["FormattedPaidFines"].DisplayIndex = 7;
                    grid.Columns["FormattedPaidFines"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["Status"].DisplayIndex = 8;
                    grid.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["NoOfPresent"].DisplayIndex = 9;
                    grid.Columns["NoOfPresent"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    grid.Columns["NoOfAbsent"].DisplayIndex = 10;
                    grid.Columns["NoOfAbsent"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    break;

                case MemberCommittmentGridType.Fines:
                    GeneralHelper.SetVisibleColumns(grid, "ShowRank", "FirstName", "LastName", "TotalFines", "PaidFines");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShowRank", "Rank" },
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "TotalFines", "Fines (€)" },
                                    { "PaidFines", "P. Fines (€)" }
                                });

                    grid.Columns["ShowRank"].DisplayIndex = 0;
                    grid.Columns["FirstName"].DisplayIndex = 1;
                    grid.Columns["LastName"].DisplayIndex = 2;
                    grid.Columns["TotalFines"].DisplayIndex = 3;
                    grid.Columns["PaidFines"].DisplayIndex = 4;
                    break;

                case MemberCommittmentGridType.Dues:
                    GeneralHelper.SetVisibleColumns(grid, "ShowRank", "FirstName", "LastName", "ContributedDues", "BalanceDues");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShowRank", "Rank" },
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                     { "ContributedDues", "Con. (€)" },
                                    { "BalanceDues", "Cont. Bal." },
                                });

                    grid.Columns["ShowRank"].DisplayIndex = 0;
                    grid.Columns["FirstName"].DisplayIndex = 1;
                    grid.Columns["LastName"].DisplayIndex = 2;
                    grid.Columns["ContributedDues"].DisplayIndex = 3;
                    grid.Columns["BalanceDues"].DisplayIndex = 4;
                    break;

            }
        }
    }
}
