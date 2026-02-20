using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
{
    public class ConstitutionHelperService
    {
        public enum ConstitutionGridType
        {
            Basic
        }

        public static void ConfigureConstitutionGrid(DataGridView grid, ConstitutionGridType type)
        {
            switch (type)
            {
                case ConstitutionGridType.Basic:
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
