using System;
using System.Text;
using System.Globalization;


namespace Common.ClassLibrary.Extensions
{

    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// String 拡張メソッド
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// 2012/12 Rebuild
    /// </history>
    /// ------------------------------------------------------------------------------------------
    public static class StringExtensions
    {

        // Add系

        #region AddDirectorySeparator

        /// <summary>
        /// 【拡張メソッド】末尾に"\"がない場合、"\"を付与した結果を返します
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string AddTailDirectorySeparator(this string target)
        {
            return AddTailMark(target, System.IO.Path.DirectorySeparatorChar.ToString());
        }

        #endregion

        #region AddTailSlush

        /// <summary>
        /// 【拡張メソッド】末尾にスラッシュ"/"がない場合、"/"を付与した結果を返します
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string AddTailSlush(this string target)
        {

            return AddTailMark(target, "/");

        }

        #endregion

        #region AddTailMark

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 【拡張メソッド】末尾に任意の文字がない場合、任意文字を付与した結果を返します
        /// </summary>
        /// <param name="target"></param>
        /// <param name="addMark">付与したい文字列</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        public static string AddTailMark(this string target, string addMark)
        {

            if (target.Trim().Equals(string.Empty) == true)
            {
                return target;
            }

            if (target.Substring(target.Length - 1, 1).Equals(addMark) == true)
            {
                return target;
            }
            else
            {
                return target + addMark;
            }

        }

        #endregion

        //汎用関数系

        #region GetByteCount

        /// <summary>
        /// 【拡張メソッド】文字列のバイト数を返す
        /// </summary>
        /// <param name="target">対象文字列</param>
        /// <remarks>SHIFT-JIS換算です</remarks>
        public static int GetByteCount(this string target)
        {
            return GetByteCount(target, 932);
        }

        /// <summary>
        /// 【拡張メソッド】文字列のバイト数を返す
        /// </summary>
        /// <param name="target">対象文字列</param>
        /// <remarks></remarks>
        public static int GetByteCount(this string target, int codePage)
        {

            try
            {
                return System.Text.Encoding.GetEncoding(codePage).GetByteCount(target);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 【拡張メソッド】文字列のバイト数を返す
        /// </summary>
        /// <param name="target">対象文字列</param>
        /// <remarks></remarks>
        public static int GetByteCount(this string target, string encodingName)
        {

            try
            {
                return System.Text.Encoding.GetEncoding(encodingName).GetByteCount(target);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }


        #endregion

        #region GetCharCount

        /// <summary>
        /// 【拡張メソッド】指定した文字が含まれている数をカウントして返します
        /// </summary>
        /// <param name="value"></param>
        /// <param name="match">検索したい文字列</param>
        /// <returns></returns>
        public static int GetCharCount(this string value, string match)
        {

            int count = 0;

            for (int i = 0; i <= value.Length - 1; i++)
            {
                if (value.Substring(i, 1).Equals(match) == true)
                {
                    count += 1;
                }
            }
            return count;

        }

        /// <summary>
        /// 【拡張メソッド】指定した文字が含まれている数をカウントして返します
        /// </summary>
        /// <param name="value"></param>
        /// <param name="match">検索したい文字列</param>
        /// <returns></returns>
        public static int GetCharCount(this string value, string[] match)
        {

            int count = 0;
            string chk = null;

            chk = string.Join("", match);

            for (int i = 0; i <= value.Length - 1; i++)
            {
                if (chk.IndexOf(value.Substring(i, 1)) >= 0)
                {
                    count += 1;
                }
            }
            return count;

        }

        #endregion

        #region IsAsciiCharacters

        /// <summary>
        /// 【拡張メソッド】英数字のみかどうかを返します
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsAsciiCharacterOnly(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return false;
            }
            if (target.Length == GetCharCount(target, new Utils.AsciiCharacterCreator().getBritishStringArray().ToArray()))
            {
                return true;

            }
            else
            {
                return false;
            }


        }

        #endregion


        //文字処理

        #region PadRightB

        /// <summary>
        /// 【拡張メソッド】指定された文字を左寄せし、指定した文字列のバイト数になるまで、右側に空白を埋め込みます
        /// </summary>
        /// <param name="target">処理対象の文字列</param>
        /// <param name="totalWidth">結果として生成される文字列のバイト数</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string PadRightB(this string target, int totalWidth)
        {
            return PadRightB(target, totalWidth, ' ');
        }

        /// <summary>
        /// 【拡張メソッド】指定された文字を左寄せし、指定した文字列のバイト数になるまで、右側に指定値を埋め込みます
        /// </summary>
        /// <param name="value">処理対象の文字列</param>
        /// <param name="totalWidth">結果として生成される文字列のバイト数</param>
        /// <param name="paddingChar">埋め込み文字</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string PadRightB(this string value, int totalWidth, char paddingChar)
        {
            for (int i = 0; i <= totalWidth; i += 1)
            {
                if (GetByteCount(value) > totalWidth)
                {
                    break;
                }

                if (GetByteCount(value.ToString() + paddingChar) > totalWidth)
                {
                    break;
                }
                value += paddingChar;
            }
            return value;
        }

        #endregion

        #region PadLeftB

        /// <summary>
        /// 【拡張メソッド】指定された文字を右寄せし、指定した文字列のバイト数になるまで、左側に空白を埋め込みます
        /// </summary>
        /// <param name="target">処理対象の文字列</param>
        /// <param name="totalWidth">結果として生成される文字列のバイト数</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string PadLeftB(this string target, int totalWidth)
        {
            return PadLeftB(target, totalWidth, ' ');
        }

        /// <summary>
        /// 【拡張メソッド】指定された文字を右寄せし、指定した文字列のバイト数になるまで、左側に指定値を埋め込みます
        /// </summary>
        /// <param name="value">処理対象の文字列</param>
        /// <param name="totalWidth">結果として生成される文字列のバイト数</param>
        /// <param name="paddingChar">埋め込み文字</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string PadLeftB(this string value, int totalWidth, char paddingChar)
        {


            for (int i = 0; i <= totalWidth; i += 1)
            {
                if (GetByteCount(value) > totalWidth)
                {
                    break;
                }

                if (GetByteCount(value.ToString() + paddingChar) > totalWidth)
                {
                    break;
                }

                value = paddingChar + value;

            }

            return value;

        }

        #endregion

        #region SubStringB

        /// <summary>
        /// 【拡張メソッド】対象文字列を指定し、部分文字列を返します。この文字列は指定バイト位置から開始し、指定したバイト数以下の文字列です
        /// </summary>
        /// <param name="target">処理対象の文字列</param>
        /// <param name="startIndex">このインスタンス内の0から始まるバイト位置</param>
        /// <param name="Length">部分文字列のバイト数</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string SubStringB(this string target, int startIndex, int Length)
        {

            string buff = string.Empty;
            string startBuff = string.Empty;
            string returnsource = string.Empty;


            for (int i = 0; i <= target.Length - 1; i++)
            {
                buff = target.Substring(i, 1);
                startBuff += buff;

                if (GetByteCount(startBuff) > startIndex)
                {
                    if (GetByteCount(returnsource + buff) <= Length)
                    {
                        returnsource += buff;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return returnsource;

        }

        /// <summary>
        /// 【拡張メソッド】対象文字列を指定し、部分文字列を返します。この文字列は指定バイト位置から開始し、指定したバイト数以下の文字列です
        /// </summary>
        /// <param name="target">処理対象の文字列</param>
        /// <param name="startIndex">このインスタンス内の0から始まるバイト位置</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string SubStringB(this string target, int startIndex)
        {

            return SubStringB(target, startIndex, GetByteCount(target));

        }

        #endregion

        // 型変換

        #region ToDateTime (+1)

        /// <summary>
        /// 【拡張メソッド】引数を元にDateTime型のオブジェクトを取得する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static System.DateTime ToDateTime(this string target)
        {
            string tempDate = target.Replace("/", "");

            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentException();
            }
            else
            {
                return System.DateTime.ParseExact(tempDate, "yyyyMMdd", null);
            }
        }

        /// <summary>
        /// 【拡張メソッド】引数を元にDateTime型のオブジェクトを取得する。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static System.DateTime ToDateTime(this string target, string format)
        {
            if (string.IsNullOrEmpty(target) || string.IsNullOrEmpty(format))
            {
                throw new ArgumentException();
            }
            else
            {
                return System.DateTime.ParseExact(target, format, null);
            }
        }

        #endregion

        #region ToBoolean

        /// <summary>
        /// 【拡張メソッド】空白ならFalse,そうでない場合にはBoolean変換をした結果を返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string target)
        {

            if (target == string.Empty)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(target);
            }

        }

        #endregion

        //型変換 (他関数I/Fのみ

        #region ToSafeInt32

        /// <summary>
        /// 【拡張メソッド】安全なInt32型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Int32 ToSafeInt32(this string target)
        {
            return TryParseExtensions.ToSafeParseInt32(target);
        }

        #endregion

        #region ToSafeInt64

        /// <summary>
        /// 【拡張メソッド】安全なInt64型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Int64 ToSafeInt64(this string target)
        {
            return TryParseExtensions.ToSafeParseInt64(target);
        }

        #endregion

        #region ToSafeLong

        /// <summary>
        /// 【拡張メソッド】安全なLong型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static long ToSafeLong(this string target)
        {
            return TryParseExtensions.ToSafeParseInt64(target);
        }

        #endregion

        #region ToSafeDecimal

        /// <summary>
        /// 【拡張メソッド】安全なDecimal型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static System.Decimal ToSafeDecimal(this string target)
        {
            return TryParseExtensions.ToSafeParseDecimal(target);
        }

        #endregion

        #region ToTruncateValue

        /// <summary>
        /// 【拡張メソッド】Truncateした値に変換する
        /// </summary>
        /// <param name="target">Nullの場合、Null、数値評価できる場合、Truncate、そうでない場合、入力値のまま</param>
        /// <returns></returns>
        public static string ToTruncateValue(this string target, int length)
        {
            return Utils.MathUtils.TruncateValue(target, length);
        }

        #endregion

        //値変換

        #region ConvertEmptyToSpaceOne

        /// <summary>
        /// 【拡張メソッド】指定値がString.Emptyの場合スペース * 1 に置き換えて返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertEmptyToSpaceOne(this string target)
        {

            return (target.Trim().Equals(string.Empty) ? " " : target.Trim()).ToString();

        }

        #endregion

        #region ConvertEmptyToZero

        /// <summary>
        /// 【拡張メソッド】指定値がString.Emptyの場合に"0"に変換して返却する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ConvertEmptyToZero(this string target)
        {
            return target.EmptyToZero(false);
        }

        /// <summary>
        /// 【拡張メソッド】指定値がString.Emptyの場合に"0"に変換して返却する
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isTrim">Trimして判定するかどうかを指定します</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string EmptyToZero(this string target, bool isTrim)
        {
            if (isTrim)
            {
                return target == string.Empty ? "0" : target;
            }
            else
            {
                return (target.Trim().Equals(string.Empty) ? "0" : target.Trim()).ToString();
            }

        }

        #endregion

        #region ConvertZeroToEmpty

        /// <summary>
        /// 【拡張メソッド】引数が"0"ならEmptyに変換し返却する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ConvertZeroToEmpty(this string target)
        {
            return target == "0" ? string.Empty : target;
        }

        #endregion

        #region ConvertEmptyToDBNull

        /// <summary>
        /// 【拡張メソッド】EmptyだったらDBNull.Valueを返す
        /// </summary>
        /// <param name="target">チェックしたい文字列</param>
        /// <returns></returns>
        public static object ConvertEmptyToDBNull(this string target)
        {
            return (target == null || target == string.Empty ? DBNull.Value : (object)target);
        }

        #endregion

        #region ConvertToNumberOnly

        /// <summary>
        /// 【拡張メソッド】対象の文字列から数字のみを取得して返す
        /// </summary>
        /// <param name="target">取得対象となる文字列</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertToNumberOnly(this string target)
        {
            string ret = string.Empty;
            foreach (System.Text.RegularExpressions.Match m in (System.Text.RegularExpressions.Regex.Matches(target, "\\d+")))
            {
                ret += m.ToString();
            }
            return ret;

        }

        #endregion

        //書式変換

        #region FormatToJapaneseDate

        /// <summary>
        /// 【拡張メソッド】渡された値("/"ありの日付)を和暦に変換して返却する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string FormatToJapaneseDate(this string target)
        {

            // 和暦に変換するためのオブジェクトを生成する 

            CultureInfo culture = new CultureInfo("ja-JP", true);

            // プロパティを設定する 

            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            // 引数を和暦に変換して返却する 
            if (string.IsNullOrEmpty(target))
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToDateTime(target).ToString("ggyy年M月d日", culture);
            }
        }

        #endregion

        #region FormatToJapaneseTime

        /// <summary>
        /// 【拡張メソッド】渡された値を日本の時間表記に変換して返却する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string FormatToJapaneseTime(this string target)
        {
            if (target.Trim().ToString().Length > 0)
            {
                return target.Replace(":", "時") + "分";
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region FormatToJapaneseYen

        /// <summary>
        /// 【拡張メソッド】金額を日本表記に変更する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string FormatToJapaneseYen(this string target)
        {
            if (TryParseExtensions.TryParseDecimal(target))
            {
                return Convert.ToDecimal(target).ToJapaneseYen();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 【拡張メソッド】金額を日本表記に変更する
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isCommaNeeded">カンマが必要かどうかを指定します</param>
        /// <returns></returns>
        public static string ToJapaneseYen(this decimal target, bool isCommaNeeded)
        {

            string val = string.Empty;
            val = target.ToJapaneseYen();
            if (isCommaNeeded == true)
            {
                return val;
            }
            else
            {
                return val.Replace(",", "");
            }
        }

        #endregion

        #region FormatToZip

        /// <summary>
        /// 【拡張メソッド】引数の値を郵便番号にして返却する
        /// </summary>
        /// <param name="target">対象の値</param>
        /// <returns></returns>
        public static string FormatToZip(this string target)
        {

            if (target == string.Empty)
            {
                return string.Empty;
            }

            target = target.Replace("-", string.Empty);

            if (target.Length == 7)
            {
                return target.Substring(0, 3) + "-" + target.Substring(3, 4);
            }
            else
            {
                return target;
            }

        }

        #endregion

        #region FormatToDate (+2)

        /// <summary>
        /// 日付のフォーマットを行う
        /// </summary>
        public static string FormatToDate(this string target)
        {
            return target.FormatToDate("yyyy/MM/dd");
        }

        /// <summary>
        /// 日付のフォーマットを行う
        /// </summary>
        public static string FormatToDate(this string target, string format)
        {
            return target.FormatToDate(format, System.DateTime.Now.ToString("yyyy/MM/dd"));
        }

        public static string FormatToDate(this string target, string format, string autoCompletionSupportDate)
        {

            const string DELIMITER = "/";
            string[] items = null;
            System.DateTime result = default(System.DateTime);


            //デリミタを含んでいるのであれば、デリミタで分割して処理
            if (target.Contains(DELIMITER) == true)
            {
                items = target.Split(Convert.ToChar(DELIMITER));

                if (items.Length == 2)
                {

                    target = Convert.ToDateTime(autoCompletionSupportDate).ToString("yyyy") + DELIMITER +
                                                items[0].PadLeft(2, '0') + DELIMITER + items[1].PadLeft(2, '0');
                }
                else
                {
                    if (items[0].Length <= 4 && items[1].Length <= 2 && items[2].Length <= 2)
                    {
                        target = Convert.ToDateTime(autoCompletionSupportDate).ToString("yyyy").Substring(0, 4 - items[0].Length) + items[0] + DELIMITER +
                                                   items[1].PadLeft(2, '0') + DELIMITER + items[2].PadLeft(2, '0');
                    }
                }

            }
            else
            {
                //そうでない場合には、入力値を元に自動で補完する
                switch (target.Length)
                {
                    case 1:
                    case 2:

                        target = Convert.ToDateTime(AppDomain.CurrentDomain.GetData("NowSystemDateFormatAfter")).ToString("yyyy/MM") + DELIMITER + target.PadLeft(2, '0');
                        break;
                    case 3:
                    case 4:

                        target = Convert.ToDateTime(AppDomain.CurrentDomain.GetData("NowSystemDateFormatAfter")).ToString("yyyy") + DELIMITER + target.PadLeft(4, '0').Substring(0, 2) + DELIMITER + target.PadLeft(4, '0').Substring(2, 2);
                        break;
                    case 6:

                        target = Convert.ToDateTime(AppDomain.CurrentDomain.GetData("NowSystemDateFormatAfter")).ToString("yyyy").Substring(0, 2) + target.Substring(0, 2) + DELIMITER + target.Substring(2, 2) + DELIMITER + target.Substring(4, 2);
                        break;
                    case 8:

                        target = target.Substring(0, 4) + DELIMITER + target.Substring(4, 2) + DELIMITER + target.Substring(6, 2);
                        break;

                }
            }


            if (System.DateTime.TryParse(target, out result) != true)
            {

                return string.Empty;
            }
            else
            {
                return Convert.ToDateTime(target).ToString(format);
            }

        }

        #endregion

        #region FormatToCommaValue (+1)

        /// <summary>
        /// 【拡張メソッド】引数の値をカンマ編集して返却する。
        /// </summary>
        /// <param name="target">対象の値</param>
        /// <returns></returns>
        public static string FormatToCommaValue(this string target)
        {

            decimal result = default(decimal);

            if (target == string.Empty)
            {
                return string.Empty;
            }

            if (decimal.TryParse(target, out result) == true)
            {
                return result.ToString("#,##0.#####################");
            }
            else
            {
                return target;
            }

        }

        /// <summary>
        /// 【拡張メソッド】引数の値をカンマ編集して返却する。
        /// </summary>
        /// <param name="target">対象の値</param>
        /// <returns></returns>
        public static string FormatToCommaValue(this string target, int decimalPointLength)
        {

            decimal result = default(decimal);

            if (target == string.Empty)
            {
                return string.Empty;
            }

            if (decimal.TryParse(target, out result) == true)
            {
                if (decimalPointLength > 0)
                {
                    return result.ToString("#,##0." + ("0000000000000").ToString().Substring(0, decimalPointLength) + "################");
                }
                else
                {
                    return result.ToString("#,##0.#####################");

                }
            }
            else
            {
                return target;
            }

        }

        #endregion



    }
}
