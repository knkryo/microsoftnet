using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary
{
    public class MathUtils
    {

        /// <summary>
        /// 時分の差異を計算する
        /// </summary>
        /// <param name="start">開始時間</param>
        /// <param name="end">終了時間</param>
        /// <returns></returns>
        public static string CalculateTimeSpanHHMM(string start, string end)
        {
            System.DateTime startDate;
            System.DateTime endDate;

            try
            {
                if (start.ToString().Length == 0 || end.ToString().Length == 0)
                {
                    return string.Empty;
                }
                if (start.Split(Convert.ToChar(":")).Length == 2)
                {
                    start += ":00";
                }

                if (end.Split(Convert.ToChar(":")).Length == 2)
                {
                    end += ":00";
                }

                startDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd") + " " + start);
                if (int.Parse(start.Replace(":", "")) > int.Parse(end.Replace(":", "")))
                {
                    endDate = Convert.ToDateTime(System.DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " " + end);
                }
                else
                {
                    endDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy/MM/dd") + " " + end);
                }
                return (endDate - startDate).ToString();

            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        #region TruncateValue

        /// <summary>
        /// Truncateした値に変換する
        /// </summary>
        /// <param name="target">Nullの場合、Null、数値評価できる場合、Truncate、そうでない場合、入力値のまま</param>
        /// <returns></returns>
        public static string TruncateValue(object target, int length)
        {
            if (Convert.ToString(target).Length == 0)
            {
                return string.Empty;
            }
            else
            {
                System.Decimal result = 0m;

                if (System.Decimal.TryParse(System.Convert.ToString(target), out result))
                {
                    if (length == 0)
                    {
                        result = decimal.Truncate(0);
                    }
                    else if (length > 0)
                    {
                        result = result * decimal.Parse(Math.Pow(0.1, length).ToString());
                        result = decimal.Truncate(result);
                        result = result * decimal.Parse(Math.Pow(10, length).ToString());
                    }
                    else if (length < 0)
                    {
                        result = result * decimal.Parse(Math.Pow(10, length).ToString());
                        result = decimal.Truncate(result);
                        result = result * decimal.Parse(Math.Pow(0.1, length).ToString());
                    }

                    return result.ToString();
                }
                else
                {
                    return Convert.ToString(target);
                }
            }

        }

        #endregion



    }
}
