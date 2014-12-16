using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
namespace Common.WinForms.Controls
{
    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// ファイル・フォルダの選択を行うダイアログ機能を提供する
    /// </summary>
    /// <remarks></remarks>
    /// ------------------------------------------------------------------------------------------
    public partial class ExplorerBrowserDialog
    {
        private const string _DefaultExt = "*.*|全てのファイル";

        #region Enum
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ダイアログの種類を指定します
        /// </summary>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        public enum DialogTypes
        {
            /// ------------------------------------------------------------------------------------------
            /// <summary>
            /// ファイルを開くダイアログ
            /// </summary>
            /// <remarks></remarks>
            /// ------------------------------------------------------------------------------------------
            OpenFile,
            /// ------------------------------------------------------------------------------------------
            /// <summary>
            /// ファイルの保存ダイアログ
            /// </summary>
            /// <remarks></remarks>
            /// ------------------------------------------------------------------------------------------
            SaveFile,
            /// ------------------------------------------------------------------------------------------
            /// <summary>
            /// フォルダ選択ダイアログ
            /// </summary>
            /// <remarks></remarks>
            /// ------------------------------------------------------------------------------------------
            FolderBrowser
        }
        #endregion
        #region Public Property(Original)

        #region DialogType
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ダイアログの種類を指定します。
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        [System.ComponentModel.Description("ダイアログの種類を指定します。")]
        [System.ComponentModel.DefaultValueAttribute(DialogTypes.OpenFile)]
        public DialogTypes DialogType { get; set; }
        #endregion

        #region FilePath
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 指定されたファイルパスを取得します
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        [System.ComponentModel.DefaultValueAttribute("")]
        [Description("指定されたファイルパスを取得します")]
        public string FilePath
        {
            get { return this.txtFilePath.Text; }
            set { this.txtFilePath.Text = value; }
        }
        #endregion

        #region Description
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ダイアログに表示する文字列を設定します
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        [System.ComponentModel.DefaultValueAttribute("")]
        [Description("ダイアログに表示する文字列を設定します")]
        public string Description
        {
            get { return this.FolderBrowserDialog.Description; }
            set { this.FolderBrowserDialog.Description = value; }
        }
        #endregion

        #region DefaultExt
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 表示する既定の拡張子を設定します
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        [System.ComponentModel.DefaultValueAttribute(_DefaultExt)]
        [Description("表示する既定の拡張子を設定します")]
        public string DefaultExt
        {
            get { return this.SaveFileDialog.DefaultExt; }
            set
            {
                this.SaveFileDialog.DefaultExt = value;
                this.OpenFileDialog.DefaultExt = value;
            }
        }
        #endregion

        #region Title
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ダイアログに表示するタイトルを設定します
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        [System.ComponentModel.DefaultValueAttribute("")]
        [Description("ダイアログに表示するタイトルを設定します")]
        public string Title
        {
            get { return this.SaveFileDialog.Title; }
            set
            {
                this.SaveFileDialog.Title = value;
                this.OpenFileDialog.Title = value;
            }
        }
        #endregion

        #region Filter
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ダイアログに表示するタイトルを設定します
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        [System.ComponentModel.DefaultValueAttribute("")]
        [Description("ダイアログに表示するフィルタを設定します")]
        public string Filter
        {
            get { return this.SaveFileDialog.Filter; }
            set
            {
                this.SaveFileDialog.Filter = value;
                this.OpenFileDialog.Filter = value;
            }
        }
        #endregion

        #region ButtonText
        /// <summary>
        /// ボタンのテキストを取得設定します
        /// </summary>
        [System.ComponentModel.Description("ボタンのテキストを取得設定します")]
        [System.ComponentModel.DefaultValue(@"検 索(&F)")]
        public string ButtonText
        {
            get
            {
                return this.btnFind.Text;
            }
            set
            {
                this.btnFind.Text = value;
            }
        }
        #endregion

        #endregion

        #region Events

        #region OnLoad
        
        /// <summary>
        /// Loadイベント処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            //初期値の設定
            this.OpenFileDialog.DefaultExt = this.DefaultExt;
            this.SaveFileDialog.DefaultExt = this.DefaultExt;
        }
        
        #endregion

        #region txtFilePath_DragDrop
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// パスがドラッグされた時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        private void txtFilePath_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //親に投げる
            base.OnDragDrop(e);
            try
            {
                this.txtFilePath.Text = ((Array)(e.Data.GetData(DataFormats.FileDrop, true))).GetValue(0).ToString();
            }
            catch
            {
                this.txtFilePath.Text = string.Empty;
            }
        }
        #endregion

        #region txtFilePath_DragEnter
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// パスがドラッグされた時のマウスカーソル変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        private void txtFilePath_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //親に投げる
            base.OnDragEnter(e);
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion

        #region btnFind_Click
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 検索ボタンのクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        private void btnFind_Click(System.Object sender, System.EventArgs e)
        {
            OnSearchButtonClick(new EventArgs());
            ShowDialog();
        }
        #endregion

        #endregion

        #region Public Method

        #region ShowDialog
        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        public DialogResult ShowDialog()
        {
            DialogResult result = default(DialogResult);
            this.OnDialogShow(new EventArgs());
            switch (this.DialogType)
            {
                case DialogTypes.FolderBrowser:
                    this.FolderBrowserDialog.SelectedPath = this.txtFilePath.Text;
                    result = this.FolderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this.txtFilePath.Text = this.FolderBrowserDialog.SelectedPath;
                    }
                    break;
                case DialogTypes.OpenFile:
                    this.OpenFileDialog.FileName = this.txtFilePath.Text;
                    result = this.OpenFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this.txtFilePath.Text = this.OpenFileDialog.FileName;
                    }
                    break;
                case DialogTypes.SaveFile:
                    this.SaveFileDialog.FileName = this.txtFilePath.Text;
                    result = this.SaveFileDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this.txtFilePath.Text = SaveFileDialog.FileName;
                    }
                    break;
            }
            this.OnDialogShowed(new EventArgs());
            return result;
        }
        #endregion

        #endregion

        #region Public Event

        #region OnDialogShow

        /// <summary>
        /// ShowDialogの直前に発生します
        /// </summary>
        /// <remarks></remarks>
        public event DialogShowEventHandler DialogShow;

        /// <summary>
        /// ShowDialogイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DialogShowEventHandler(object sender, EventArgs e);

        /// <summary>
        /// ShowDialogの直前に発生するイベントを処理する
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected virtual void OnDialogShow(EventArgs e)
        {
            if (DialogShow != null)
            {
                DialogShow(this, e);
            }
        }

        #endregion

        #region DialogShowed

        /// <summary>
        /// ShowDialogの実行完了前に発生します
        /// </summary>
        /// <remarks></remarks>
        public event DialogShowedEventHandler DialogShowed;

        /// <summary>
        /// ShowDialog実行完了前のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DialogShowedEventHandler(object sender, EventArgs e);

        /// <summary>
        /// ShowDialogの実行完了前にに発生するイベントを処理する
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected virtual void OnDialogShowed(EventArgs e)
        {
            if (DialogShowed != null)
            {
                DialogShowed(this, e);
            }
        }

        #endregion

        #region SearchButtonClick
        /// <summary>
        /// 検索ボタンがクリックされたときに発生します
        /// </summary>
        /// <remarks></remarks>
        public event SearchButtonClickEventHandler SearchButtonClick;

        /// <summary>
        /// 検索ボタンクリック時のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SearchButtonClickEventHandler(object sender, EventArgs e);

        /// <summary>
        /// ShowDialogの直前に発生するイベントを処理する
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected virtual void OnSearchButtonClick(EventArgs e)
        {
            if (SearchButtonClick != null)
            {
                SearchButtonClick(this, e);
            }
        }

        #endregion

        #endregion

        
        /// <summary>
        /// Constructor
        /// </summary>
        public ExplorerBrowserDialog()
        {
            InitializeComponent();
        }

        #region FilePathChanged

        /// <summary>
        /// 検索ボタンがクリックされたときに発生します
        /// </summary>
        /// <remarks></remarks>
        public event EventHandler FilePathChanged;
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (FilePathChanged != null)
            {
                FilePathChanged(this, e);
            }
        }

        #endregion

        #region txtFilePath_TextChanged

        /// <summary>
        /// テキストが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }

        #endregion

    }
}
