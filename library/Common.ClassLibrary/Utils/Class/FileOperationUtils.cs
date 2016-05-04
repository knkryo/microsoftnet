using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common.ClassLibrary.Extensions;
namespace Common.ClassLibrary.Utils
{
    public class FileOperationUtils
    {

        #region Enum

        /// <summary>
        /// ファイルパスの種類
        /// </summary>
        /// <remarks></remarks>
        public enum FilePathType
        {

            /// <summary>
            /// 不明
            /// </summary>
            /// <remarks></remarks>
            UnKnown = 0,

            /// <summary>
            /// 絶対パス
            /// </summary>
            /// <remarks></remarks>
            AbsolutePath = 1,

            /// <summary>
            /// 相対パス
            /// </summary>
            /// <remarks></remarks>
            RelativePath = 2,

            /// <summary>
            /// パス名文字列
            /// </summary>
            /// <remarks></remarks>
            PathNameString = 4,

            /// <summary>
            /// UNC
            /// </summary>
            /// <remarks></remarks>
            UniversalNamingConvention = 8

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
        public static List<String> GetFileList(string path, string searchPattern, System.IO.SearchOption searchOption)
        {

            List<String> fileList = new List<string>();

            foreach (string s in searchPattern.Split(';'))
            {
                fileList.AddRange(System.IO.Directory.GetFiles(path, s, searchOption));
            }

            return fileList;
        }

	    #endregion

        #region GetAssembryCodeBaseDirectory

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 現在コードが実行されているモジュールのローカルパスを返します
        /// </summary>
        /// <returns>指定したアセンブリが格納されているモジュールのローカルパス</returns>
        /// <remarks>
        /// 実行環境のパスを得る方法はいくつかありますが、DLLファイルなどの場合には、本メソッドを
        /// 使用してパスを取得することで、DLLファイルの配置パスを取得できます。
        /// </remarks>
        /// <history>
        ///     2008/03/24 Ryo Kaneko Ver.1.0.0 Created
        /// </history>
        /// ------------------------------------------------------------------------------------------
        public static string GetAssembryCodeBaseDirectory(System.Reflection.Assembly ExecutingAssembly)
        {

            string stTarget = ExecutingAssembly.CodeBase;
            Uri hUri = null;


            try
            {
                hUri = new System.Uri(stTarget);

                //ファイルパスは"file://" で返るため、ローカルパスに変換して返す

                //アプリケーションを実行したファイルの、ファイル名を含まないパスを返す
                //System.Windows.Forms.Application.StartupPath

                //読み込み済みファイルのパスまたはUNC位置を返す
                //System.Reflection.Assembly.GetExecutingAssembly().Location

                //初めに指定された場所を返す(配置場所)
                //System.Reflection.Assembly.GetExecutingAssembly().CodeBase


                return System.IO.Path.GetDirectoryName(hUri.LocalPath.ToString());

            }
            finally
            {
                hUri = null;
            }

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
        public static string GetCurrentDirectory()
        {

            return GetAssembryCodeBaseDirectory(System.Reflection.Assembly.GetExecutingAssembly());

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
        public static FilePathType GetFilePathType(string target)
        {

            if (new Validation.FilePathCharValidation(target).IsValid() == false)
            {
                return FilePathType.UnKnown;
            }

            if (target.Trim().Length <= 2)
            {
                return FilePathType.UnKnown;
            }

            if (target.Substring(0, 2).Equals("\\\\"))
            {
                //UNC判定

                //3文字目が英数字でない場合には、UnKnown
                if (new Validation.BritishCharValidation(target.Substring(2, 1)).IsValid() == false)
                {
                    return FilePathType.UnKnown;
                }
                else
                {
                    return FilePathType.UniversalNamingConvention;
                }
            }
            else if (target.Length >= 3 && target.Substring(1, 2).Equals(':' + '\\'))
            {
                return FilePathType.AbsolutePath;
            }
            else if (target.Length >= 7 && target.ToUpper().Substring(0, 7) == "FILE://")
            {
                return FilePathType.PathNameString;
            }
            else
            {
                return FilePathType.RelativePath;
            }

        }

        #endregion

        #region GetTempFileName

        /// <summary>
        /// 指定したディレクトリで使用可能な一時ファイルのフルパスを返す
        /// </summary>
        /// <param name="targetDirecrory"></param>
        /// <returns></returns>
        public static string GetTempFileName(string targetDirecrory)
        {

            string tmpFileName = null;
            string tmpFileFullPath = null;

            //一時ファイル取得機能によりファイルのフルパスを取得
            tmpFileFullPath = System.IO.Path.GetTempFileName();
            System.IO.File.Delete(tmpFileFullPath);

            //一時ファイルのファイル名を拝借
            tmpFileName = System.IO.Path.GetFileName(tmpFileFullPath);

            //ファイルがなければ、そのファイル名を返す
            tmpFileFullPath = targetDirecrory.AddTailDirectorySeparator() + tmpFileName;

            if (System.IO.File.Exists(tmpFileFullPath) == false)
            {
                try
                {
                    //ファイルを書き込んでみる
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(tmpFileFullPath))
                    {
                        sw.Flush();
                    }

                    //取得したファイル名を返す
                    return tmpFileFullPath;


                }
                catch (Exception ex)
                {
                    //例外の場合にはExceptionを投げる
                    throw new System.AccessViolationException(ex.Message, ex);


                }
                finally
                {
                }
            }
            else
            {
                //ファイルが使われている場合には、再帰
                return GetTempFileName(targetDirecrory);
            }

        }

        #endregion

    }
}
