using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
{
    public class ConstitutionHelper
    {
        public enum ConstitutionGridType
        {
            Basic,
            Normal
        }

        public static void ConfigureConstitutionGrid(DataGridView grid, ConstitutionGridType type)
        {
            switch (type)
            {
                case ConstitutionGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "ShortDescription", "FineWithCurrency");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShortDescription", "Summary" },
                                    { "FineWithCurrency", "Fine" }
                                });
                    break;

                case ConstitutionGridType.Normal:
                    GeneralHelper.SetVisibleColumns(grid, "ShortDescription", "Section", "FineWithCurrency");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "ShortDescription", "Summary" },
                                    { "FineWithCurrency", "Fine" }
                                });
                    break;
            }
        }
    }
}
