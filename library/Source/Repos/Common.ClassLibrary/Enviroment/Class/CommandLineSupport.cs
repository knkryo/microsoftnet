using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ClassLibrary.Enviroment
{
    /// <summary>
    /// コマンドラインを分割して利用しやすくするためのユーティリティ
    /// </summary>
    /// <remarks>
    /// ロジックの目標値
    /// ・外部からの設定による、設定可能なパラメータ値の保持
    /// ・コマンドラインをパラメータに従い分割し、Valueクラスとして格納(ValueはInterfaceで拡張可)
    /// ・自動検証およびヘルプメッセージ出力の補完
    /// </remarks>
    public class CommandLineSupport
    {
        /// <summary>
        /// コマンドラインを分割して格納しておくリスト
        /// </summary>
        public List<CommandLineValue> CommandLines { get; protected set; }

        public List<CommandLineArgumentDefineValue> DefineParameters { get; set; }

        public string CommandLine { get; set; }

        public CommandLineSupport()
        {
            CommandLines = new List<CommandLineValue>();
            SplitCommandLine();
        }

        private void SplitCommandLine()
        {
            //先頭は除外
            string[] cmd;
            cmd = System.Environment.GetCommandLineArgs();
            CommandLine = System.Environment.CommandLine;

            Console.WriteLine(cmd.Length);
            for (int i= 1;i<cmd.Length;i++)
            {
                if (cmd[i].ToString().Substring(0, 1) == "/")
                {
                    //Key
                    CommandLines.Add(new CommandLineValue(cmd[i].ToString()));
                }
                else
                {
                    //Value
                    if (CommandLines.Count == 0 || CommandLines[CommandLines.Count - 1].Value.ToString().Length > 0)
                    {
                        CommandLines.Add(new CommandLineValue() { Value = cmd[i].ToString()});
                    }
                    else
                    {
                        CommandLines[CommandLines.Count - 1].Value = cmd[i];
                    }
                }
            }
        }

        private bool isSupportedParameterKey(string value)
        {
            if (DefineParameters == null || DefineParameters.Exists(m => m.Key == value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
