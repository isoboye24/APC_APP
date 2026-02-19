using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
{
    public class CommentHelperService
    {
        public enum CommentGridType
        {
            Basic
        }

        public static void ConfigureCommentGrid(DataGridView grid, CommentGridType type)
        {
            switch (type)
            {
                case CommentGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "CommentName", "Name", "Surname", "GenderName",
                        "MonthName", "Year");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "CommentName", "Comment" },
                                    { "GenderName", "Gender" },
                                    { "MonthName", "Month" },
                                });
                    break;
            }
        }
    }
}
