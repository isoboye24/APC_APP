using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC.HelperServices
{
    public class PaymentStatusHelperService
    {
        public enum PaymentStatusGridType
        {
            Basic,            
        }

        public static void ConfigurePaymentStatusGrid(DataGridView grid, PaymentStatusGridType type)
        {
            switch (type)
            {
                case PaymentStatusGridType.Basic:
                    GeneralHelper.SetVisibleColumns(grid, "PaymentStatusName");
                    GeneralHelper.RenameColumns(grid, new Dictionary<string, string>
                                {
                                    { "PaymentStatusName", "Payment Status" }
                                });
                    break;

                default:
                    GeneralHelper.SetVisibleColumns(grid, "", "", "");
                    break;
            }
        }
    }
}
