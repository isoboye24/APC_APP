using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class FinedMemberHelper
    {
        public enum FinedMemberGridType
        {
            Basic,
            PersonalDetails
        }

        public static void ConfigureFinedMemberGrid(DataGridView grid, FinedMemberGridType type)
        {
            switch (type)
            {
                case FinedMemberGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "ShortDescription",
                        "FormattedAmountExpected", "FormattedAmountPaid", "Status", "FormattedFineDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "ShortDescription", "Violated" },
                                    { "FormattedAmountExpected", "Fine" },
                                    { "FormattedAmountPaid", "Paid" },
                                    { "FormattedFineDate", "Date" }
                                });

                    grid.Columns["FirstName"].DisplayIndex = 0;
                    grid.Columns["LastName"].DisplayIndex = 1;
                    grid.Columns["ShortDescription"].DisplayIndex = 2;
                    grid.Columns["FormattedAmountExpected"].DisplayIndex = 3;
                    grid.Columns["FormattedAmountPaid"].DisplayIndex = 4;
                    grid.Columns["Status"].DisplayIndex = 5;
                    grid.Columns["FormattedFineDate"].DisplayIndex = 6;
                    break;

                case FinedMemberGridType.PersonalDetails:
                    GeneralHelper.SetVisibleColumns(grid, "ShortDescription", "FormattedAmountExpected", "FormattedAmountPaid", "Balance", "Status", "FormattedFineDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShortDescription", "Violated" },
                                    { "FormattedAmountExpected", "Fine" },
                                    { "FormattedAmountPaid", "Paid" },
                                    { "FormattedFineDate", "Date" }
                                });

                    grid.Columns["ShortDescription"].DisplayIndex = 0;
                    grid.Columns["FormattedAmountExpected"].DisplayIndex = 1;
                    grid.Columns["FormattedAmountPaid"].DisplayIndex = 2;
                    grid.Columns["Balance"].DisplayIndex = 3;
                    grid.Columns["Status"].DisplayIndex = 4;
                    grid.Columns["FormattedFineDate"].DisplayIndex = 5;
                    break;
            }
        }
    }
}
