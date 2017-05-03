using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace _2.SemesterProjekt.Converter
{
    public class IntToString : IValueConverter
    {
        /// <summary>
        /// Convert from int to XAML string. Removes zero as defaultvalue in XAML</summary>
        /// 
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((int)value == 0)
            {
                return String.Empty;
            }
            return value;
        }

        /// <summary>
        /// Convert from XAML string to int. Return 0 instead of null or empty string or if parsing is not successful
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!String.IsNullOrEmpty((string)value))
            {
                int valueAsInt;
                bool canParse = Int32.TryParse((string)value, out valueAsInt);

                if (canParse)
                {
                    return valueAsInt;
                }
            }
            return 0;
        }
    }
}

