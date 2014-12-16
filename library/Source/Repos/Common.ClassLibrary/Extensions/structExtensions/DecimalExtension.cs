using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary.Extensions
{
    public static class DecimalExtension
    {
        #region ConvertToJapaneseYen (金額を日本表記に変更する)

        public static string ToJapaneseYen(this decimal value)
        {
            const decimal cho = 1000000000000;
            const decimal oku = 100000000;
            const decimal man = 10000;
            if (value == 0)
            {
                return "0円";
            }

            StringBuilder sb = new StringBuilder();

            if (value >= cho)
            {
                sb.Append(((decimal.Truncate(value / cho).ToString()) + "兆").ToString().FormatToCommaValue());
            }

            if (value >= oku && (decimal.Truncate(value / oku) * oku % cho) != 0)
            {
                sb.Append(((decimal.Truncate(value % cho / oku).ToString()) + "億").ToString().FormatToCommaValue());
            }

            if (value >= man && (decimal.Truncate(value / man) * man % oku) != 0)
            {
                sb.Append(((decimal.Truncate(value % oku / man).ToString()) + "万").ToString().FormatToCommaValue());
            }

            if (value % man != 0)
            {
                sb.Append(((decimal.Truncate(value % man).ToString()) + "").ToString().FormatToCommaValue()); ;
            }
            sb.Append("円");
            return sb.ToString();

        }

        #endregion

        public static string ToJapaneseYen(this decimal value, bool isCommaNeeded)
        {
            string val = string.Empty;
            val = ToJapaneseYen(value);
            if (isCommaNeeded == true)
            {
                return val;
            }
            else
            {
                return val.Replace(",", "");
            }
        }


        #region TryParse
        
        public static bool TryParse(this decimal source,string s)
        {
            decimal test;

            try
            {
                return decimal.TryParse(s, out test);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion


    }
}
