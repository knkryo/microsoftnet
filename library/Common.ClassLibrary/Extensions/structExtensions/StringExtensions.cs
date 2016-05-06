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
        #region AddTailSlush

        /// <summary>
        /// 【拡張メソッド】末尾にスラッシュ"/"がない場合、"/"を付与した結果を返します
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string AddTailSlush(this string target)
        {
            return StringCore.AddTailSlush(target);
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
            return StringCore.AddTailMark(target, addMark);
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
            return StringCore.GetByteCount(target);
        }

        /// <summary>
        /// 【拡張メソッド】文字列のバイト数を返す
        /// </summary>
        /// <param name="target">対象文字列</param>
        /// <remarks></remarks>
        public static int GetByteCount(this string target, int codePage)
        {
            return StringCore.GetByteCount(target);
        }

        /// <summary>
        /// 【拡張メソッド】文字列のバイト数を返す
        /// </summary>
        /// <param name="target">対象文字列</param>
        /// <remarks></remarks>
        public static int GetByteCount(this string target, string encodingName)
        {
            return StringCore.GetByteCount(target, encodingName);
        }

        #endregion

        #region GetCharCount

        /// <summary>
        /// 【拡張メソッド】指定した文字が含まれている数をカウントして返します
        /// </summary>
        /// <param name="value"></param>
        /// <param name="match">検索したい文字列</param>
        /// <returns></returns>
        public static int GetCharCount(this string target, string match)
        {
            return StringCore.GetCharCount(target, match);
        }

        /// <summary>
        /// 【拡張メソッド】指定した文字が含まれている数をカウントして返します
        /// </summary>
        /// <param name="value"></param>
        /// <param name="match">検索したい文字列</param>
        /// <returns></returns>
        public static int GetCharCount(this string target, string[] match)
        {
            return StringCore.GetCharCount(target, match);
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
            return StringCore.IsAsciiCharacterOnly(target);
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
            return StringCore.PadRightB(target, totalWidth);
        }

        /// <summary>
        /// 【拡張メソッド】指定された文字を左寄せし、指定した文字列のバイト数になるまで、右側に指定値を埋め込みます
        /// </summary>
        /// <param name="value">処理対象の文字列</param>
        /// <param name="totalWidth">結果として生成される文字列のバイト数</param>
        /// <param name="paddingChar">埋め込み文字</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string PadRightB(this string target, int totalWidth, char paddingChar)
        {
            return StringCore.PadRightB(target, totalWidth, paddingChar);
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
            return StringCore.PadLeftB(target, totalWidth);
        }

        /// <summary>
        /// 【拡張メソッド】指定された文字を右寄せし、指定した文字列のバイト数になるまで、左側に指定値を埋め込みます
        /// </summary>
        /// <param name="value">処理対象の文字列</param>
        /// <param name="totalWidth">結果として生成される文字列のバイト数</param>
        /// <param name="paddingChar">埋め込み文字</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string PadLeftB(this string target, int totalWidth, char paddingChar)
        {
            return StringCore.PadLeftB(target, totalWidth, paddingChar);
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
            return StringCore.SubStringB(target, startIndex, Length);
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
            return StringCore.SubStringB(target, startIndex);
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
            return StringCore.ConvertToDateTime(target);
        }

        /// <summary>
        /// 【拡張メソッド】引数を元にDateTime型のオブジェクトを取得する。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static System.DateTime ToDateTime(this string target, string format)
        {
            return StringCore.ConvertToDateTime(target, format);
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
            return StringCore.ConvertToBoolean(target);
        }

        #endregion

        //型変換

        #region ToSafeInt32

        /// <summary>
        /// 【拡張メソッド】安全なInt32型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Int32 ToSafeInt32(this string target)
        {
            return TryParseCore.ToSafeParseInt32(target);
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
            return TryParseCore.ToSafeParseInt64(target);
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
            return TryParseCore.ToSafeParseInt64(target);
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
            return TryParseCore.ToSafeParseDecimal(target);
        }

        #endregion

        //変換

        #region ToTruncateValue

        /// <summary>
        /// 【拡張メソッド】Truncateした値に変換する
        /// </summary>
        /// <param name="target">Nullの場合、Null、数値評価できる場合、Truncate、そうでない場合、入力値のまま</param>
        /// <returns></returns>
        public static string ToTruncateValue(this string target, int length)
        {
            return MathUtils.TruncateValue(target, length);
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
            return StringCore.ConvertEmptyToSpaceOne(target);
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
            return StringCore.ConvertEmptyToZero(target);
        }

        /// <summary>
        /// 【拡張メソッド】指定値がString.Emptyの場合に"0"に変換して返却する
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isTrim">Trimして判定するかどうかを指定します</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertEmptyToZero(this string target, bool isTrim)
        {
            return StringCore.ConvertEmptyToZero(target, isTrim);
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
            return StringCore.ConvertZeroToEmpty(target);
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
            return StringCore.ConvertEmptyToDBNull(target);
        }

        #endregion

        #region GetNumberOnly

        /// <summary>
        /// 【拡張メソッド】対象の文字列から数字のみを取得して返す
        /// </summary>
        /// <param name="target">取得対象となる文字列</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetNumberOnly(this string target)
        {
            return StringCore.GetNumberOnly(target);
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
            return StringCore.FormatToJapaneseDate(target);
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
            return StringCore.FormatToJapaneseTime(target);
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
            return StringCore.FormatToJapaneseYen(target);
        }

        /// <summary>
        /// 【拡張メソッド】金額を日本表記に変更する
        /// </summary>
        /// <param name="target"></param>
        /// <param name="isCommaNeeded">カンマが必要かどうかを指定します</param>
        /// <returns></returns>
        public static string ToJapaneseYen(this string target, bool isCommaNeeded)
        {
            return StringCore.FormatToJapaneseYen(target,isCommaNeeded);
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
            return StringCore.FormatToZip(target);
        }

        #endregion

        #region FormatToDate (+2)

        /// <summary>
        /// 日付のフォーマットを行う
        /// </summary>
        public static string FormatToDate(this string target)
        {
            return StringCore.FormatToDate(target);
        }

        /// <summary>
        /// 日付のフォーマットを行う
        /// </summary>
        public static string FormatToDate(this string target, string format)
        {
            return StringCore.FormatToDate(target);
        }

        public static string FormatToDate(this string target, string format, string autoCompletionSupportDate)
        {
            return StringCore.FormatToDate(target, format, autoCompletionSupportDate);
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
            return StringCore.FormatToCommaValue(target);
        }

        /// <summary>
        /// 【拡張メソッド】引数の値をカンマ編集して返却する。
        /// </summary>
        /// <param name="target">対象の値</param>
        /// <returns></returns>
        public static string FormatToCommaValue(this string target, int decimalPointLength)
        {
            return StringCore.FormatToCommaValue(target, decimalPointLength);
        }

        #endregion



    }
}
