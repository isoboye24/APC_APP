using APC.DAL;
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
                    GeneralHelper.SetVisibleColumns(grid, "ShowRank", "Name", "Surname", "ExpectedAmount", 
                        "Contributed", "Balance", "Fines", "PaidFines", "PaymentStatus", "NumberOfPresence", 
                        "NumberOfAbsence");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShowRank", "Rank" },
                                    { "ExpectedAmount", "Exp. (€)" },
                                    { "Contributed", "Con. (€)" },
                                    { "Balance", "Cont. Bal." },
                                    { "Fines", "Fines (€)" },
                                    { "PaidFines", "P. Fines (€)" },
                                    { "PaymentStatus", "Status" },
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
