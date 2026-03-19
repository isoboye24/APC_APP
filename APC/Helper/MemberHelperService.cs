using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class MemberHelperService
    {
        public enum MemberGridType
        {
            Basic,
            Shrinked,
            Contact,
            Birthday,
            Dead,
            Permission
        }

        public static void ConfigureMemberGrid(DataGridView grid, MemberGridType type)
        {
            switch (type)
            {
                case MemberGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "NationalityName", "PositionName", "GenderName");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "NationalityName", "Nationality" },
                                    { "PositionName", "Position" },
                                    { "GenderName", "Gender" },                                    
                                });
                    break;
                case MemberGridType.Shrinked:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "GenderName");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "GenderName", "Gender" },
                                });
                    break;

                case MemberGridType.Contact:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "EmailAddress", "PhoneNumber", "PhoneNumber2", "PhoneNumber3");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "EmailAddress", "Email" },
                                    { "PhoneNumber", "Mobile" },
                                    { "PhoneNumber2", "Mobile 2" },
                                    { "PhoneNumber3", "Mobile 3" },
                                });
                    break;

                case MemberGridType.Birthday:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "PositionName", "GenderName", "BirthdayDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "PositionName", "Position" },
                                    { "GenderName", "Gender" },
                                    { "BirthdayDate", "Birthday" },
                                });
                    break;

                case MemberGridType.Dead:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "PositionName", "GenderName", "Birthday", "DeadDate", "DeadAge");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "PositionName", "Position" },
                                    { "GenderName", "Gender" },
                                    { "Birthday", "Born on" },
                                    { "DeadDate", "Died on" },
                                    { "DeadAge", "Age" }
                                });
                    break;
                case MemberGridType.Permission:
                    GeneralHelper.SetVisibleColumns(grid, "FirstName", "LastName", "PositionName", "PermissionName");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "PositionName", "Position" },
                                    { "PermissionName", "Access Level" }                                    
                                });
                    break;
                default:
                    GeneralHelper.SetVisibleColumns(grid, "", "", "");
                    break;
            }
        }
    }
}
