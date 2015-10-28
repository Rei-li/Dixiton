using System.Globalization;
using System.Threading;

namespace Dixiton.Common.Helpers
{
    public class DecimalHelper
    {
        public static string Separator
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator;
            }
        }


        public static decimal Parse(string invariantDecimal)
        {
            decimal value;
            decimal.TryParse(invariantDecimal, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value);
            return value;
        }
    }
}
