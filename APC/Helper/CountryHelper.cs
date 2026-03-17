using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.Helper
{
    public class CountryHelper
    {
        public enum CountryGridType
        {
            Basic
        }

        public static void ConfigureCountryGrid(DataGridView grid, CountryGridType type)
        {
            switch (type)
            {
                case CountryGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "CountryName");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "CountryName", "Countries" },
                                });
                    break;
            }
        }
    }


}
