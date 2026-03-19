using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class CommentHelper
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
                    GeneralHelper.SetVisibleColumns(grid, "CommentName", "FirstName", "LastName", "GenderName",
                        "FormattedDate");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "CommentName", "Comment" },
                                    { "FirstName", "First Name" },
                                    { "LastName", "Last Name" },
                                    { "GenderName", "Gender" },
                                    { "MonthName", "Month" },
                                    { "FormattedDate", "Date" },
                                });
                    break;
            }
        }
    }
}
