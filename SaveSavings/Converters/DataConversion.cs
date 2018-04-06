using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace SaveSavings.Converters
{
    class DataConversion
    {
        public static int ConvertCurrencyStringToIntegerCents(string strValue)
        {
            // patch decimal separator
            strValue = strValue.Replace(',', '.'); // , => .
            strValue = strValue.Replace('-', '.'); // - => .

            float amount = float.Parse(strValue, NumberStyles.Currency, CultureInfo.InvariantCulture);  // invariant culture uses "."
            int valCents = (int)Math.Round(amount * 100.0f);

            return valCents;
        }


        public static bool CanConvertCurrencyStringToIntegerCents(string strValue)
        {
            // patch decimal separator
            strValue = strValue.Replace(',', '.'); // , => .
            strValue = strValue.Replace('-', '.'); // - => .

            float amount;
            return float.TryParse(strValue, NumberStyles.Currency, CultureInfo.InvariantCulture, out amount);  // invariant culture uses "."
        }


        public static float ConvertCentsToCurrency(int _Cents)
        {
            return _Cents / 100.0f;
        }


    }




    public class CurrencyAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            var ss = string.Format("{0:C}", value);

            return ss;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }




    public class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return false;

            return (string)value == (string)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }




    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            var ss = string.Format("{0:D}", value);

            return ss;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }



}
