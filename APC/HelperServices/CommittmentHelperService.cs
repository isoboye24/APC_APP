using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
{
    public class CommittmentHelperService
    {
        public static MembersCommittmentDetailDTO MapMemberCommittmentFromGrid(DataGridView grid, int rowIndex)
        {
            var row = grid.Rows[rowIndex];

            return new MembersCommittmentDetailDTO
            {
                MemberID = Convert.ToInt32(row.Cells["MemberID"].Value),
                ShowRank = Convert.ToDecimal(row.Cells["ShowRank"].Value),
                Name = row.Cells["Name"].Value?.ToString(),
                Surname = row.Cells["Surname"].Value?.ToString(),
                ImagePath = row.Cells["ImagePath"].Value.ToString(),
                ExpectedAmount = Convert.ToDecimal(row.Cells["ExpectedAmount"].Value),
                Contributed = Convert.ToDecimal(row.Cells["Contributed"].Value),
                Balance = row.Cells["Balance"].Value.ToString(),
                Fines = Convert.ToDecimal(row.Cells["Fines"].Value),
                PaidFines = Convert.ToDecimal(row.Cells["PaidFines"].Value),
                NumberOfPresence = Convert.ToInt32(row.Cells["NumberOfPresence"].Value),
                NumberOfAbsence = Convert.ToInt32(row.Cells["NumberOfAbsence"].Value),
                Rank = Convert.ToDecimal(row.Cells["Rank"].Value),
            };
        }

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
                    GeneralHelper.SetVisibleColumns(grid, "ShowRank", "Name", "Surname", "Contributed", "Balance", "Fines", "PaidFines", "NumberOfPresence", "NumberOfAbsence");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShowRank", "Rank" },
                                    { "Contributed", "Cont. (€)" },
                                    { "PaidFines", "P. Fines (€)" },
                                    { "NumberOfPresence", "Pres." },
                                    { "NumberOfAbsence", "Abs." }
                                });
                    break;

                case MemberCommittmentGridType.Fines:
                    GeneralHelper.SetVisibleColumns(grid, "ShowRank", "Name", "Surname", "Fines", "PaidFines");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShowRank", "Rank" },
                                    { "Fines", "Fines (€)" },
                                    { "PaidFines", "P. Fines (€)" }
                                });
                    break;

                case MemberCommittmentGridType.Dues:
                    GeneralHelper.SetVisibleColumns(grid, "ShowRank", "Name", "Surname", "Contributed", "Balance");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShowRank", "Rank" },
                                    { "Contributed", "Contributed (€)" },
                                });
                    break;

            }
        }
    }
}
