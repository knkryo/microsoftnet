using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary
{
    public static class DecimalCore
    {
        #region ConvertToJapaneseYen (金額を日本表記に変更する)

        public static string ToJapaneseYen(decimal value)
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
                sb.Append(StringCore.FormatToCommaValue((decimal.Truncate(value / cho).ToString()) + "兆").ToString());
            }

            if (value >= oku && (decimal.Truncate(value / oku) * oku % cho) != 0)
            {
                sb.Append(StringCore.FormatToCommaValue((decimal.Truncate(value % cho / oku).ToString()) + "億").ToString());
            }

            if (value >= man && (decimal.Truncate(value / man) * man % oku) != 0)
            {
                sb.Append(StringCore.FormatToCommaValue((decimal.Truncate(value % oku / man).ToString()) + "万").ToString());
            }

            if (value % man != 0)
            {
                sb.Append(StringCore.FormatToCommaValue((decimal.Truncate(value % man).ToString()) + "").ToString()); ;
            }
            sb.Append("円");
            return sb.ToString();

        }

        #endregion

        public static string ToJapaneseYen(decimal value, bool isCommaNeeded)
        {
            string val = string.Empty;
            val = DecimalCore.ToJapaneseYen(value);
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
