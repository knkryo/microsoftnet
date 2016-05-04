namespace Common.ClassLibrary.Constant
{
    public class RegExString
    {

        /// <summary>
        /// 英数字のみを許可する正規表現
        /// </summary>
        public const string RegStringEnglishAndNumber = @"[\-0-9a-zA-Z]";

        /// <summary>                                          
        /// 英字のみを許可する正規表現                         
        /// </summary>                                         
        public const string RegStringEnglishOnly = @"[a-zA-Z]";

        /// <summary>                                          
        /// 数字のみを許可する正規表現                         
        /// </summary>                                         
        public const string RegStringNumberOnly = @"^[0-9]";

        /// <summary>                                          
        /// 日付を許可する正規表現                             
        /// </summary>                                         
        public const string RegStringDate = @"^[/0-9]";

        /// <summary>                                          
        /// 日付を許可する正規表現                             
        /// </summary>                                         
        public const string RegStringDateValid = @"^[0-9]{1,4}[\/][0-9]{1,2}[\/][0-9]{1,2}$|[0-9]{1,2}[\/][0-9]{1,2}$|[0-9]{1,2}$|[0-9]{4}$|[0-9]{6}$|[0-9]{8}$";

        /// <summary>                                          
        /// 日付を許可する正規表現                             
        /// </summary>                                         
        public const string RegStringDateYm = @"^[0-9]{1,4}[\/][0-9]{1,2}|[0-9]{1,2}[\/][0-9]{1,2}$|[0-9]{1,2}$|[0-9]{4}$|[0-9]{6}$";

        /// <summary>                                          
        /// 電話番号を許可する正規表現                         
        /// </summary>                                         
        public const string RegStringTel = @"^[\-0-9]";

        /// <summary>                                          
        /// 電話番号を許可する正規表現                         
        /// </summary>                                         
        public const string RegStringTelValid = @"^[0-9]{1,8}[\-][0-9]{1,8}[\-][0-9]{1,8}$|[0-9]{1,8}[\(][0-9]{1,8}[\)][0-9]{1,8}$";

        /// <summary>                                          
        /// 郵便番号を許可する正規表現                         
        /// </summary>                                         
        public const string RegStringZip = @"^[0-9]{3}[\-]?[0-9]{4}$";

        /// <summary>                                          
        /// 時間を許可する正規表現                             
        /// </summary>                                         
        public const string RegStringTime = @"^[\:0-9]";

        /// <summary>                                          
        /// 時間を許可する正規表現                             
        /// </summary>                                         
        public const string RegStringTimeValid = @"^[0-9]{1,2}[\:][0-9]{1,2}$|[0-9]{1,4}$";

        /// <summary>                                          
        /// 数値を許可する正規表現                             
        /// </summary>                                         
        public const string RegStringNumeric = @"^[{0}0-9{1}]";

        /// <summary>                                          
        /// 数値を許可する正規表現                             
        /// </summary>                                         
        public const string RegStringNumericValid = @"^[{0}]?[0-9]{0,{1}}{2}$";

        /// <summary>                                          
        /// 全角2バイトのカタカナを許可する正規表現            
        /// </summary>                                         
        public const string RegStringKanaDoubleByte = @"^[A-Za-z０-９（）－＿ァ-ヴ・！＃＄％＆’（）￣＾｜＠［｛：；＊］｝，．＜＞／？＿＝－＋\!\#\$\%\&\'\(\)\~\^\\\|\@\\[\{\:\;\*\]\}\,\.\<\>\/\?_\=\-\+ー\`\""　]+$";

        /// <summary>
        /// 全角2バイトのカタカナを許可する正規表現
        /// </summary>
        public const string RegStringKanaDoubleByteValid = @"^[A-Za-z０-９（）－＿ァ-ヴ・！＃＄％＆’（）￣＾｜＠［｛：；＊］｝，．＜＞／？＿＝－＋\!\#\$\%\&\'\(\)\~\^\\\|\@\\[\{\:\;\*\]\}\,\.\<\>\/\?_\=\-\+\`\""ー　]+$";

        /// <summary>                                          
        /// 半角カタカナを許可する正規表現            
        /// </summary>                                         
        public const string RegStringKanaSingleByte = @"^[0-9()-_ｱ-ﾞ ]+$";

        /// <summary>
        /// 半角カタカナを許可する正規表現
        /// </summary>
        public const string RegStringKanaSingleByteValid = @"^[0-9()-_ｱ-ﾞ ]+$";

        /// <summary>                                          
        /// メールアドレスを許可する正規表現            
        /// </summary>                                         
        public const string RegStringMailSingleByte = @"[0-9a-zA-Z@\-_.]";

        /// <summary>
        /// メールアドレスを許可する正規表現
        /// </summary>
        public const string RegStringMailSingleByteValid = @"[\w\-.]+@[\w\-.]+";

        /// <summary>
        /// ExcelSeheet名を置換する正規表現
        /// </summary>
        public const string RegExcelSheetName = @"\:|\?|\\|\[|\]|\/|\*|\：|\？|\￥|\［|\］|\／|\＊";


        /// <summary>
        /// 英数字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string RegStringBritishNumberChar = "[0-9a-zA-Z]";
        /// <summary>
        /// 英字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string RegStringBritishChar = "[a-zA-Z]";
        /// <summary>
        /// 数字を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string RegStringNumberChar = "[0-9]";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオドなし)
        /// </summary>
        /// <remarks></remarks>

        public const string RegStringNumericCharNoPeriod = "[0-9]";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオドあり)
        /// </summary>
        /// <remarks></remarks>

        public const string RegStringNumericCharIncPeriod = "[0-9.]";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオド・マイナスあり)
        /// </summary>
        /// <remarks></remarks>

        public const string RegStringNumericCharIncMinus = "[0-9-]";
        /// <summary>
        /// 数値を評価する正規表現文字列(ピリオド・マイナスあり)
        /// </summary>
        /// <remarks></remarks>

        public const string RegStringNumericCharIncPeriodMinus = "[0-9.-]";
        /// <summary>
        /// 郵便番号(日本)を評価する正規表現文字列
        /// </summary>
        /// <remarks></remarks>

        public const string RegStringZipJapaneseChar = "[0-9-]";

    }
}
