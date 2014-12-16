using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ClassLibrary.Drawing
{
    public class ImageResizerManager
    {
        private int compressedSize = 1024;

        public event EventHandler ExecuteStart;

        public event EventHandler ExecuteComplete;

        protected virtual void OnExecuteComplete(EventArgs e)
        {
            if (ExecuteComplete != null)
            {
                ExecuteComplete(this, e);
            }
        }

        protected virtual void OnExecuteStart(EventArgs e)
        {
            if (ExecuteStart != null)
            {
                ExecuteStart(this, e);
            }
        }


        public ImageResizerManager() { }
        public ImageResizerManager(int size)
        {
            FileList = new List<string>();
            compressedSize = size;
        }
        public IList<string> FileList { get; set; }

        public void Execute()
        {
            this.OnExecuteStart(new EventArgs());

            foreach (string s in this.FileList)
            {
                this.ExecuteResize(s, compressedSize);
            }

            this.OnExecuteComplete(new EventArgs());

        }

        private void ExecuteResize(string target, int size)
        {
            using (JpegImageResizer res = new JpegImageResizer(target, size))
            {
                res.Execute();
                res.Dispose();
            }
        }

        //ディレクトリを丸ごとせっとする
        public void SetDirectory(string directoryName)
        {
            this.SetDirectory(directoryName, System.IO.SearchOption.TopDirectoryOnly);
        }
        public void SetDirectory(string directoryName,System.IO.SearchOption option )
        {
            if (System.IO.Directory.Exists(directoryName) == false)
            {
                return;
            }
            foreach ( string s in System.IO.Directory.GetFiles(directoryName,"*.*",option))
            {
                if (isSupportedExtensions(System.IO.Path.GetExtension(s).ToString()))
                {
                    this.FileList.Add(s);
                }
            }

        }

        private bool isSupportedExtensions(string target)
        {
            foreach (string s in supportedExtensions())
            {
                if (target.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }

        private string[] supportedExtensions()
        {
            return new string[4]{".JPG",".jpg",".jpeg",".JPEG"};
        }

    }
}
