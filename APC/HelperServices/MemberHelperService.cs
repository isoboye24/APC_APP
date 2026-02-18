using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
{
    public class MemberHelperService
    {
        public enum MemberGridType
        {
            Basic,
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
                    GeneralHelper.SetVisibleColumns(grid, "Name", "Surname", "NationalityName", "PositionName", "GenderName");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "NationalityName", "Nationality" },
                                    { "PositionName", "Position" },
                                    { "GenderName", "Gender" },                                    
                                });
                    break;

                case MemberGridType.Contact:
                    GeneralHelper.SetVisibleColumns(grid, "Name", "Surname", "EmailAddress", "PhoneNumber", "PhoneNumber2", "PhoneNumber3");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "EmailAddress", "Email" },
                                    { "PhoneNumber", "Mobile" },
                                    { "PhoneNumber2", "Mobile 2" },
                                    { "PhoneNumber3", "Mobile 3" },
                                });
                    break;

                case MemberGridType.Birthday:
                    GeneralHelper.SetVisibleColumns(grid, "Name", "Surname", "PositionName", "GenderName", "BirthdayDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "PositionName", "Position" },
                                    { "GenderName", "Gender" },
                                    { "BirthdayDate", "Birthday" },
                                });
                    break;

                case MemberGridType.Dead:
                    GeneralHelper.SetVisibleColumns(grid, "Name", "Surname", "PositionName", "GenderName", "Birthday", "DeadDate", "DeadAge");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "PositionName", "Position" },
                                    { "GenderName", "Gender" },
                                    { "Birthday", "Born on" },
                                    { "DeadDate", "Died on" },
                                    { "DeadAge", "Age" }
                                });
                    break;
                case MemberGridType.Permission:
                    GeneralHelper.SetVisibleColumns(grid, "Name", "Surname", "PositionName", "PermissionName");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
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
