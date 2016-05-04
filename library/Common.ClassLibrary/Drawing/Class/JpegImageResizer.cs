using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common.ClassLibrary.Drawing
{
    public class JpegImageResizer:IDisposable
    {

        //ファイルサイズ
        private decimal compressedFileSize = 1024;

        //無限ループ防止用のカウンタ
        private int procCounter = 0;

        private const int procCounterMax = 10;

        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="originalfilePath"></param>
        public JpegImageResizer(string originalfilePath, int compressSize = 1024)
        {
            IsBackupNeeded = true;
            this.FilePath = originalfilePath;
            try
            {
                compressedFileSize = compressSize;
            }
            catch (Exception)
            {
                compressedFileSize = 1024;
            }
        }

        #endregion

        #region FilePath

        private string _FilePath;

        /// <summary>
        /// 処理対象のファイルパス
        /// </summary>
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                this._FilePath = value;
                if (CreateFilePath == string.Empty)
                {

                }
            }
        }

        #endregion

        #region CreateFilePath
        
        private string _CreateFilePath = string.Empty;
        public string CreateFilePath
        {
            get
            {
                if (_CreateFilePath.ToString().Equals(string.Empty))
                {
                    return this._FilePath;
                }
                else
                {
                    return _CreateFilePath;
                }
            }
            set
            {
                _CreateFilePath = value;
                ;
            }
        }
        #endregion

        public bool IsBackupNeeded { get; set; }
        #region Execute
        
        public void Execute()
        {
            string savedImgFileFullPath = string.Empty;
            string tmpImgFileFullPath = string.Empty;
            //ファイルを生成
            try
            {
                //ファイル存在チェック
                if (System.IO.File.Exists(this.FilePath) == false)
                {
                    throw new System.IO.FileNotFoundException();
                }

                switch (System.IO.Path.GetExtension(this.FilePath).ToUpper())
                {
                    case ".JPG":
                    case ".JPEG":
                        break;
                    default:
                        return;
                }


                procCounter = 0;

                //圧縮の実施
                tmpImgFileFullPath = this.FilePath + System.IO.Path.GetExtension(this.FilePath);
                savedImgFileFullPath = ConvertFromScale(this.FilePath, tmpImgFileFullPath);

                //作成先へのファイル生成処理
                if (this.CreateFilePath == this.FilePath)
                {
                    //-------------------------------------------------------------------
                    // 作成元と作成先が同じファイル名の場合
                    //-------------------------------------------------------------------
                    string tmpFileName = string.Empty;
                    tmpFileName = this.FilePath + ".tmp." + System.DateTime.Now.ToString("yyyyMMddhhnnss");

                    //一時ファイルとしてリネーム
                    System.IO.File.Move(this.FilePath, tmpFileName);
                    try
                    {
                        System.IO.File.Move(savedImgFileFullPath, this.FilePath);
                        if (IsBackupNeeded)
                        {
                            System.IO.File.Move(tmpFileName, this.FilePath + ".bak");
                        }
                        else
                        {
                            System.IO.File.Delete(tmpFileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        //一時ファイルを戻す
                        System.IO.File.Move(tmpFileName, this.FilePath);
                        throw ex;
                    }
                }
                else
                {

                    //-------------------------------------------------------------------
                    // 異なるファイル名の場合
                    //-------------------------------------------------------------------
                    if (System.IO.File.Exists(this.CreateFilePath) == true)
                    {
                        System.IO.File.Delete(this.CreateFilePath);
                    }
                    System.IO.File.Copy(savedImgFileFullPath, this.CreateFilePath);
                    if (IsBackupNeeded)
                    {
                        System.IO.File.Move(savedImgFileFullPath, this.FilePath + ".bak");
                    }
                    else
                    {
                        System.IO.File.Delete(savedImgFileFullPath);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region ConvertFromScale
        
        /// <summary>
        /// イメージデータの変換処理
        /// </summary>
        /// <param name="targetfileFullPath">元となるファイルのフルパス</param>
        /// <param name="createFileFullPath">作成したいファイルのフルパス</param>
        /// <returns>作成されたファイルのフルパス</returns>
        private string ConvertFromScale(string targetfileFullPath, string createFileFullPath)
        {
            long currentFileSize = new System.IO.FileInfo(targetfileFullPath).Length / 1024;    //ファイルサイズ(KB)
            float per = (float)(compressedFileSize / currentFileSize) / (float)2;

            try
            {
                procCounter+=1;
                //処理回数をオーバーしたら、強制的に元のファイルで返す
                if (procCounter > procCounterMax)
                {
                    return createFileFullPath;
                }

                //Imgの読込
                using (System.Drawing.Image img = Image.FromFile(targetfileFullPath))
                {
                    //現在のファイルサイズのほうが小さいのであれば、処理をせずに終了する
                    if (currentFileSize < compressedFileSize)
                    {
                        System.IO.File.Copy(targetfileFullPath, createFileFullPath);
                        return createFileFullPath;
                    }
                    else
                    {
                        //そうでなければ、Graphicsオブジェクトで画像を縮小
                        Bitmap canvas = new System.Drawing.Bitmap(Convert.ToInt32(img.Width * (1 - per)),
                                                                  Convert.ToInt32(img.Height * (1 - per)));

                        //イメージデータの縮小処理
                        using (Graphics g = Graphics.FromImage(canvas))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                            g.DrawImage(img, 0, 0, canvas.Width, canvas.Height);
                        }

                        //Exif情報の複写処理
                        foreach (System.Drawing.Imaging.PropertyItem pi in img.PropertyItems)
                        {
                            canvas.SetPropertyItem(pi);
                        }
                        canvas.Save(createFileFullPath, img.RawFormat);
                        canvas.Dispose();
                        img.Dispose();

                        //タイムスタンプの更新処理
                        System.IO.File.SetCreationTime(createFileFullPath, System.IO.File.GetCreationTime(targetfileFullPath));
                        System.IO.File.SetLastWriteTime(createFileFullPath, System.IO.File.GetLastWriteTime(targetfileFullPath));

                        //それでも、まだ縮小が必要な場合には、再帰処理
                        if (new System.IO.FileInfo(createFileFullPath).Length > (compressedFileSize * 1024))
                        {
                            //再度再帰処理
                            string reroutedTempFilePath = string.Empty;

                            reroutedTempFilePath = createFileFullPath + System.IO.Path.GetExtension(createFileFullPath);
                            System.IO.File.Delete(reroutedTempFilePath);
                            reroutedTempFilePath = ConvertFromScale(createFileFullPath, reroutedTempFilePath);
                            System.IO.File.Delete(createFileFullPath);
                            System.IO.File.Move(reroutedTempFilePath, createFileFullPath);
                        }

                        return createFileFullPath;
                    
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Dispose
        
        public void Dispose()
        {

        }

        #endregion

    }
}
