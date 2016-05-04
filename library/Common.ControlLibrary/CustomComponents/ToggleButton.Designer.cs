using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
namespace Common.WinForms.CustomComponents
{
	[System.Drawing.ToolboxBitmap(typeof(System.Windows.Forms.Button))]
	partial class ToggleButton : Button
	{

		[System.Diagnostics.DebuggerNonUserCode()]
		public ToggleButton(System.ComponentModel.IContainer container) : this()
		{

			//Windows.Forms クラス作成デザイナのサポートに必要です。
			if ((container != null)) {
				container.Add(this);
			}

		}

		[System.Diagnostics.DebuggerNonUserCode()]
		public ToggleButton() : base()
		{

			//この呼び出しは、コンポーネント デザイナで必要です。
			InitializeComponent();

		}

		//Component は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try {
				if (disposing && components != null) {
					components.Dispose();
				}
			} finally {
				base.Dispose(disposing);
			}
		}

		//コンポーネント デザイナで必要です。

		private System.ComponentModel.IContainer components;
		//メモ: 以下のプロシージャはコンポーネント デザイナで必要です。
		//コンポーネント デザイナを使って変更できます。
		//コード エディタを使って変更しないでください。
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

	}
}
