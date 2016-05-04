using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ClassLibrary.Enviroment
{
    public class CommandLineArgumentDefineValue
    {
        //パラメータ引き渡し時に指定するキー値を指定します
        public string Key { get; set; }

        //ヘルプに表示するメッセージを管理します
        public string HelpMessage { get; set; }

        //エラー時のエラーメッセージを管理します
        public string ErrorMessage { get; set; }

        //必須かどうかを管理します
        public bool IsRequired { get; set; }
    }
}
