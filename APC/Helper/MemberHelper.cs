using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class MemberHelper
    {
        public enum MemberGridType
        {
            Basic,
            SemiBasic,
            Contact,
            Birthday,
            Dead,
            Permission
        }

        public static void ConfigureMemberGrid(DataGridView grid, MemberGridType type)
        {
            switch (type)
            {
                case MemberGridType.SemiBasic:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "Nationality", "Position", "Gender");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },                                                                      
                                });

                    grid.Columns["FirstName"].DisplayIndex = 0;
                    grid.Columns["LastName"].DisplayIndex = 1;
                    grid.Columns["Nationality"].DisplayIndex = 2;
                    grid.Columns["Position"].DisplayIndex = 3;
                    grid.Columns["Gender"].DisplayIndex = 4;
                    break;

                case MemberGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "Gender");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                });

                    grid.Columns["FirstName"].DisplayIndex = 0;
                    grid.Columns["LastName"].DisplayIndex = 1;
                    grid.Columns["Gender"].DisplayIndex = 2;
                    break;

                case MemberGridType.Contact:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "Email", "PhoneNumber", "PhoneNumber2", "PhoneNumber3");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "PhoneNumber", "Mobile 1" },
                                    { "PhoneNumber2", "Mobile 2" },
                                    { "PhoneNumber3", "Mobile 3" },
                                });

                    grid.Columns["FirstName"].DisplayIndex = 0;
                    grid.Columns["LastName"].DisplayIndex = 1;
                    grid.Columns["Email"].DisplayIndex = 2;
                    grid.Columns["PhoneNumber"].DisplayIndex = 3;
                    grid.Columns["PhoneNumber2"].DisplayIndex = 4;
                    grid.Columns["PhoneNumber3"].DisplayIndex = 5;
                    break;

                case MemberGridType.Birthday:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "Position", "Gender", "FormattedBirthday");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "FormattedBirthday", "Birthday" },
                                });

                    grid.Columns["FirstName"].DisplayIndex = 0;
                    grid.Columns["LastName"].DisplayIndex = 1;
                    grid.Columns["Position"].DisplayIndex = 2;
                    grid.Columns["Gender"].DisplayIndex = 3;
                    grid.Columns["FormattedBirthday"].DisplayIndex = 4;
                    break;

                case MemberGridType.Dead:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "Position", "Gender", "Birthdate", "DeadDate", "Age");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "Birthdate", "Born on" },
                                    { "DeadDate", "Died on" },
                                });

                    grid.Columns["FirstName"].DisplayIndex = 0;
                    grid.Columns["LastName"].DisplayIndex = 1;
                    grid.Columns["Position"].DisplayIndex = 2;
                    grid.Columns["Gender"].DisplayIndex = 3;
                    grid.Columns["Birthdate"].DisplayIndex = 4;
                    grid.Columns["DeadDate"].DisplayIndex = 5;
                    grid.Columns["Age"].DisplayIndex = 6;
                    break;

                case MemberGridType.Permission:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "Position", "Permission");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "Permission", "Access Level" }                                    
                                });

                    grid.Columns["FirstName"].DisplayIndex = 0;
                    grid.Columns["LastName"].DisplayIndex = 1;
                    grid.Columns["Position"].DisplayIndex = 2;
                    grid.Columns["Permission"].DisplayIndex = 3;
                    break;

                default:
                    GeneralHelper.SetVisibleColumns(grid, "", "", "");
                    break;
            }
        }
    }
}
