
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

    /// <summary>
    /// Validationクラスで使用する定数値
    /// </summary>
    public static class ValidationConstants
    {

        #region "Validation RegexValue Constants"

        /// <summary>
        /// 英数字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string BritishNumberValueRegexString = "[0-9a-zA-Z]+";
        /// <summary>
        /// 英字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string BritishValueRegexString = "[a-zA-Z]+";
        /// <summary>
        /// 数字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string NumberValueRegexString = "[0-9]+";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオドなし)
        /// </summary>
        /// <remarks></remarks>

        public const string NumericValueRegexString = "[0-9]+";
        /// <summary>
        /// 数値を評価する正規表現文字列(マイナスあり)
        /// </summary>
        /// <remarks></remarks>

        public const string NumericValueRegexStringIncMinus = "[\\-]?[0-9]+";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオドあり)
        /// </summary>
        /// <remarks></remarks>

        public const string NumericValueRegexStringIncPeriod = "[0-9]+\\.?[0-9]+";
        /// <summary>
        /// 数値を評価する正規表現文字列(マイナス・ピリオドあり)
        /// </summary>
        /// <remarks></remarks>

        public const string NumericValueRegexStringIncPeriodMinus = "[\\-]?[0-9]+\\.?[0-9]+";
        /// <summary>
        /// 郵便番号(日本)を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string ZipJapaneseValueRegexString = "[0-9]{3}[\\-]?[0-9]{4}";
        #endregion

        #region "Validation RegexChar Constants"

        /// <summary>
        /// 英数字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string BritishNumberCharRegexString = "[0-9a-zA-Z]";
        /// <summary>
        /// 英字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string BritishCharRegexString = "[a-zA-Z]";
        /// <summary>
        /// 数字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string NumberCharRegexString = "[0-9]";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオドなし)
        /// </summary>
        /// <remarks></remarks>

        public const string NumericCharNoPeriodRegexString = "[0-9]";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオドあり)
        /// </summary>
        /// <remarks></remarks>

        public const string NumericCharRegexStringIncPeriod = "[0-9.]";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオド・マイナスあり)
        /// </summary>
        /// <remarks></remarks>

        public const string NumericCharRegexStringIncMinus = "[0-9-]";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオド・マイナスあり)
        /// </summary>
        /// <remarks></remarks>

        public const string NumericCharRegexStringIncPeriodMinus = "[0-9.-]";
        /// <summary>
        /// 郵便番号(日本)を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string ZipJapaneseCharRegexString = "[0-9-]";
        #endregion

        public enum ValidationReasons
        {

            Success = 0,

            /// <summary>
            /// ファイル名に不正な文字が含まれている
            /// </summary>
            /// <remarks></remarks>
            InvalidFileNameChar = 101





        }


    }

}
