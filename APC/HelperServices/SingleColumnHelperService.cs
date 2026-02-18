using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
{
    public class SingleColumnHelperService
    {
        public enum SingleColumnGridType
        {
            Basic,            
        }

        public static void ConfigureSingleColumnGrid(DataGridView grid, SingleColumnGridType type, string columnName, string newColumnName)
        {
            switch (type)
            {
                case SingleColumnGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, columnName);
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { columnName, newColumnName }
                                });
                    break;

                default:
                    GeneralHelper.SetVisibleColumns(grid, "");
                    break;
            }
        }
    }
}
