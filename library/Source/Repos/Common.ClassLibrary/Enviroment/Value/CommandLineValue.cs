using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ClassLibrary.Enviroment
{
    /// <summary>
    /// コマンドラインからの値を格納するためのValueクラス
    /// </summary>
    public class CommandLineValue
    {
        public void Clear()
        {
            this.Key = string.Empty;
            this.Value = string.Empty;
        }

        public CommandLineValue()
        {
            this.Clear();
        }
        public CommandLineValue(string key)
        {
            this.Clear();
            this.Key = key;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
