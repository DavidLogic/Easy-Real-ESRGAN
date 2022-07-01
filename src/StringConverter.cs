using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Easy_Real_ESRGAN
{
    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //var a = (string)value;
            //if (!string.IsNullOrEmpty(a))
            //{


            //if ( a.Contains("System.Windows.Controls.ComboBoxItem: "))
            //{

            //    string str = (string)value;
            //    string b = "System.Windows.Controls.ComboBoxItem:";
            //    Regex r = new Regex(a);
            //    Match m = r.Match(str);
            //    if (m.Success)
            //    {
            //        str = str.Replace(a, "");
            //        string str1, str2;
            //        str1 = str.Substring(0, m.Index);
            //        str2 = str.Substring(m.Index + b.Length, str.Length - b.Length - m.Index);
            //        return str1 + str2;
            //    }
            //    else
            //    {
            //        return "";
            //    }
            //}
            //else
            //{
            //    return "";
            //}
            //}
            //else
            //{
            //    return "";
            //}
            return value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                var item = (ComboBoxItem)value;
            
                if(item.Content.ToString() == "自动")
                {
                    return "auto";
                }
                else
                {
                return item.Content.ToString();

                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
