﻿using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

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




    public class CurrencyAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            var ss = string.Format("{0:C}", ((int)value) / 100.0f);

            return ss;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
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
