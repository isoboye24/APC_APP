using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APC.Helper
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
