using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary.IO.Path
{
    public static class FilePathStringExtensions
    {
        #region AddDirectorySeparator

        /// <summary>
        /// 【拡張メソッド】末尾に"\"がない場合、"\"を付与した結果を返します
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string AddTailDirectorySeparator(this string target)
        {
            return StringCore.AddTailDirectorySeparator(target);
        }

        #endregion

        #region GetFileList

        /// <summary>
        /// ファイル一覧を取得する
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static List<String> GetFileList(this string path, string searchPattern, System.IO.SearchOption searchOption)
        {
            return FileOperationUtils.GetFileList(path, searchPattern, searchOption);
        }

        #endregion

        #region GetCurrentDirectory

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 現在コードが実行されているモジュールのローカルパスを返します
        /// </summary>
        /// <returns>指定したアセンブリが格納されているモジュールのローカルパス</returns>
        /// <remarks>
        /// 実行環境のパスを得る方法はいくつかありますが、DLLファイルなどの場合には、本メソッドを
        /// 使用してパスを取得することで、実行ファイルの配置パスを取得できます。
        /// </remarks>
        /// <history>
        ///     2008/03/24 Ryo Kaneko Ver.1.0.0 Created
        /// </history>
        /// ------------------------------------------------------------------------------------------
        public static string GetCurrentDirectory(this string target)
        {
            return FileOperationUtils.GetAssembryCodeBaseDirectory(System.Reflection.Assembly.GetExecutingAssembly());
        }

        #endregion

        #region GetFilePathType

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 文字列からファイルパスの種類を返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        public static FileOperationUtils.FilePathType GetFilePathType(string target)
        {
            return FileOperationUtils.GetFilePathType(target);
        }

        #endregion

        #region GetTempFileName

        /// <summary>
        /// 指定したディレクトリで使用可能な一時ファイルのフルパスを返す
        /// </summary>
        /// <param name="targetDirecrory"></param>
        /// <returns></returns>
        public static string GetTempFileName(this string targetDirecrory)
        {
            return FileOperationUtils.GetTempFileName(targetDirecrory);
        }

        #endregion

        #region CheckFileExclusive

        /// <summary>
        /// ファイルが排他で開けるかどうかをチェックして結果を返す
        /// </summary>
        /// <param name="target">チェックしたいファイルのフルパス</param>
        /// <returns></returns>
        public static bool CheckFileExclusive(this string target)
        {
            return FileOperationUtils.CheckFileExclusive(target);
        }

        #endregion

        public static string Combine(this string target , params string[] options)
        {
            string path = string.Empty;
            foreach ( string s in options)
            {
                path += System.IO.Path.Combine(path, s);
            }
            return path;
        }

        public static bool BackupCopyFile(this string target , string backupFileExtension ,bool removeExtension, bool addTimeStamp,bool deleteFileIfBackupExists)
        {
            try
            {
                string newFile = string.Empty;
                if (removeExtension == true)
                {
                    newFile = System.IO.Path.GetFileNameWithoutExtension(target);
                }
                else
                {
                    newFile = target;
                }

                if (backupFileExtension.Length == 0)
                {
                    backupFileExtension = "bak";
                }
                if (backupFileExtension.Substring(0, 1) != ".")
                {
                    backupFileExtension = "." + backupFileExtension;
                }

                newFile = newFile.Combine(backupFileExtension);

                if (deleteFileIfBackupExists && System.IO.File.Exists(newFile))
                {
                    System.IO.File.Delete(newFile);
                }
                System.IO.File.Copy(target, newFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
