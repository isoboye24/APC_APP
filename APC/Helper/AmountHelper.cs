using System.Globalization;

namespace APC.Helper
{
    public class AmountHelper
    {
        public static string FormatAmount(decimal value)
        {
            var culture = new CultureInfo("de-DE");
            return "€ " + value.ToString("N2", culture);
        }
    }
}
