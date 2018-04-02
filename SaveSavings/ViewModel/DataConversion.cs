using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings.ViewModel
{
    class DataConversion
    {
        public static int ConvertCurrencyStringToIntegerCents(string strValue)
        {
            float amount = float.Parse(strValue, NumberStyles.Currency);
            int valCents = (int)Math.Round(amount * 100.0f);

            return valCents;
        }
    }
}
