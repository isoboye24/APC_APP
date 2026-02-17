using APC.DAL.DTO;
using System;
using System.Collections.Generic;
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
                MemberID = Convert.ToInt32(row.Cells[0].Value),
                Username = row.Cells[1].Value?.ToString(),
                Password = row.Cells[2].Value?.ToString(),
                Surname = row.Cells[3].Value?.ToString(),
                Name = row.Cells[4].Value?.ToString(),
                Birthday = Convert.ToDateTime(row.Cells[5].Value),

            };
        }
    }
}
