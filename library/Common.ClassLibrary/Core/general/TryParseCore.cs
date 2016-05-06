using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary
{
    public static class TryParseCore
    {

        #region TryParse

        public static bool TryParseInt32(string value)
        {
            return TryParse<Int32>(value, int.TryParse);
        }

        public static bool TryParseInt16(string value)
        {
            return TryParse<Int16>(value, Int16.TryParse);
        }

        public static bool TryParseInt64(string value)
        {
            return TryParse<Int64>(value, Int64.TryParse);
        }

        public static bool TryParseByte(string value)
        {
            return TryParse<byte>(value, byte.TryParse);
        }

        public static bool TryParseBoolean(string value)
        {
            return TryParse<bool>(value, bool.TryParse);
        }

        public static bool TryParseSingle(string value)
        {
            return TryParse<Single>(value, Single.TryParse);
        }

        public static bool TryParseDouble(string value)
        {
            return TryParse<double>(value, Double.TryParse);
        }

        public static bool TryParseDecimal(string value)
        {
            return TryParse<decimal>(value, Decimal.TryParse);
        }

        public static bool TryParseDateTime(string value)
        {
            return TryParse<DateTime>(value, DateTime.TryParse);
        }

        #region Private Members

        private delegate bool TryParseDelegate<T>(string s, out T result);

        private static bool TryParse<T>(string value, TryParseDelegate<T> tryParse) where T : struct
        {
            T result;
            return tryParse(value, out result);
        }

        #endregion

        #endregion

        #region Parse

        public static int ToSafeParseInt32(string value)
        {
            return parseMethod<int>(value, int.TryParse);
        }

        public static Int16 ToSafeParseInt16(string value)
        {
            return parseMethod<Int16>(value, Int16.TryParse);
        }

        public static Int64 ToSafeParseInt64(string value)
        {
            return parseMethod<Int64>(value, Int64.TryParse);
        }

        public static byte ToSafeParseByte(string value)
        {
            return parseMethod<byte>(value, byte.TryParse);
        }

        public static bool ToSafeParseBoolean(string value)
        {
            return parseMethod<bool>(value, bool.TryParse);
        }

        public static Single ToSafeParseSingle(string value)
        {
            return parseMethod<Single>(value, Single.TryParse);
        }

        public static Double ToSafeParseDoube(string value)
        {
            return parseMethod<Double>(value, Double.TryParse);
        }

        public static Decimal ToSafeParseDecimal(string value)
        {
            return parseMethod<Decimal>(value, Decimal.TryParse);
        }

        public static DateTime ToSafeParseDateTime(string value)
        {
            return parseMethod<DateTime>(value, DateTime.TryParse);
        }

        #region Private Members

        private delegate bool parseDelegate<T>(string s, out T result);

        private static T parseMethod<T>(string value, parseDelegate<T> parse) where T : struct
        {
            T result;
            parse(value, out result);
            return result;
        }

        #endregion

        #endregion

    }
}
