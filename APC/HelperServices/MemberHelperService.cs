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
        public static MemberDetailDTO MapMemberFromGrid(DataGridView grid, int rowIndex)
        {
            var row = grid.Rows[rowIndex];

            return new MemberDetailDTO
            {
                MemberID = Convert.ToInt32(row.Cells["MemberID"].Value),
                Username = row.Cells["Username"].Value?.ToString(),
                Password = row.Cells["Password"].Value?.ToString(),
                Name = row.Cells["Name"].Value?.ToString(),
                Surname = row.Cells["Surname"].Value?.ToString(),
                Birthday = Convert.ToDateTime(row.Cells["Birthday"].Value),

                ImagePath = row.Cells["ImagePath"].Value.ToString(),
                EmailAddress = row.Cells["EmailAddress"].Value.ToString(),
                HouseAddress = row.Cells["HouseAddress"].Value.ToString(),
                MembershipDate = Convert.ToDateTime(row.Cells["MembershipDate"].Value),
                CountryID = Convert.ToInt32(row.Cells["CountryID"].Value),
                CountryName = row.Cells["CountryName"].Value.ToString(),
                PhoneNumber = row.Cells["PhoneNumber"].Value.ToString(),
                PhoneNumber2 = row.Cells["PhoneNumber2"].Value.ToString(),
                PhoneNumber3 = row.Cells["PhoneNumber3"].Value.ToString(),

                NationalityID = Convert.ToInt32(row.Cells["NationalityID"].Value),
                NationalityName = row.Cells["NationalityName"].Value.ToString(),
                ProfessionID = Convert.ToInt32(row.Cells["ProfessionID"].Value),
                ProfessionName = row.Cells["ProfessionName"].Value.ToString(),
                PositionID = Convert.ToInt32(row.Cells["PositionID"].Value),
                PositionName = row.Cells["PositionName"].Value.ToString(),
                GenderID = Convert.ToInt32(row.Cells["GenderID"].Value),
                GenderName = row.Cells["GenderName"].Value.ToString(),
                EmploymentStatusID = Convert.ToInt32(row.Cells["EmploymentStatusID"].Value),
                EmploymentStatusName = row.Cells["EmploymentStatusName"].Value.ToString(),
                MaritalStatusID = Convert.ToInt32(row.Cells["MaritalStatusID"].Value),
                MaritalStatusName = row.Cells["MaritalStatusName"].Value.ToString(),
                PermissionID = Convert.ToInt32(row.Cells["PermissionID"].Value),
                PermissionName = row.Cells["PermissionName"].Value.ToString(),
                MembershipStatusID = Convert.ToInt32(row.Cells["MembershipStatusID"].Value),
                MembershipStatus = row.Cells["MembershipStatus"].Value.ToString(),

                isCountryDeleted = Convert.ToBoolean(row.Cells["isCountryDeleted"].Value),
                isNationalityDeleted = Convert.ToBoolean(row.Cells["isNationalityDeleted"].Value),
                isProfessionDeleted = Convert.ToBoolean(row.Cells["isProfessionDeleted"].Value),
                isPositionDeleted = Convert.ToBoolean(row.Cells["isPositionDeleted"].Value),
                isEmpStatusDeleted = Convert.ToBoolean(row.Cells["isEmpStatusDeleted"].Value),
                isMarStatusDeleted = Convert.ToBoolean(row.Cells["isMarStatusDeleted"].Value),

                DeadDate = Convert.ToDateTime(row.Cells["DeadDate"].Value),
                DeadAge = Convert.ToDouble(row.Cells["DeadAge"].Value),
                LGA = row.Cells["LGA"].Value.ToString(),
                NameOfNextOfKin = row.Cells["NameOfNextOfKin"].Value.ToString(),
                RelationshipToKinID = Convert.ToInt32(row.Cells["RelationshipToKinID"].Value),
                RelationshipToKin = row.Cells["RelationshipToKin"].Value.ToString(),
                BirthdayDate = row.Cells["BirthdayDate"].Value.ToString(),
            };
        }

        public enum MemberGridType
        {
            Basic,
            Contact,
            Birthday,
            Dead
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
                default:
                    GeneralHelper.SetVisibleColumns(grid, "", "", "");
                    break;
            }
        }
    }
}
