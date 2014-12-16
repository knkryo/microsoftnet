using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
namespace Common.WinForms.Constants
{

    /// <summary>
    /// ユーザーコントロール開発で使用する定数群
    /// </summary>
    /// <history>
    /// 2012/12 Rebuild
    /// </history>
	public static class ComponentsDeclareConstants
	{


		public const string IFormat_FormatType = "Format指定がある場合は取得設定します";
		public const string IFormat_Delimitation = "区切り文字として使用する文字列を設定します";

		public const string IFormat_MaxByteLength = "入力可能な文字長をバイト数で設定します";
		public const string INumberFormat_IsDecimalPoint = "小数点を使用するかどうかを指定します";
		public const string INumberFormat_IsDisplayDelimiter = "指定されたデリミタを使用して表示を行うかどうかを設定します。";
		public const string INumberFormat_IsDisplayZeroDecimalPoint = "小数点以下の入力桁数が不足している場合、指定桁数まで0を補完するかどうかを設定します。";
		public const string INumberFormat_IsNegativeValue = "マイナス値を許可するかどうかを設定します";
		public const string INumberFormat_ForeColorNegativeValue = "マイナス値を表示するときの文字の色を設定します";
		public const string INumberFormat_ForeColorNormalValue = "数値を表示するときの文字の色を設定します";
		public const string INumberFormat_IntegerLength = "整数部の入力可能な桁数を設定します";

		public const string INumberFormat_SmallPointLength = "少数部の入力可能な桁数を設定します";
		public const string IValidation_IsRequired = "必須チェックを行うかどうか取得設定します。設定後、Formのメソッドを呼び出すことでチェックが実行されます";
		public const string IControl_Value = "区切り文字等を省いた値を取得設定します\t";

		public const string IControl_IsChangeCheck = "変更破棄チェックを行うかどうかを指定します。Trueを指定した場合には、Formのメソッドにてチェック対象となります";
		public const string BaseTextBox_MoveNextControlByEnter = "Enterキーで次のControlにTab移動するかどうかを取得設定します";
		public const string BaseTextBox_DisableBackColor = "Enable = False 時の背景色を設定します";

		public const string BaseTextBox_DisableForeColor = "Enable = False 時の文字色を取得設定します";

		public const string ToolBoxItemFilter_ControlLibrary = "ControlLibrary";

		public const string ToolBoxItemFilter_Default = "CustomComponent";
		public const string ControlDescriptionAutoSize = "フォント サイズを基にして、サイズの自動調整を行います。これはテキストを折り返さないラベル コントロールでのみ有効です。";
		public const string ControlDescriptionAutoEllipsis = "ラベル コントロールの幅を越えるテキストの自動処理を有効にします。";
		public const string ControlDescriptionBackgroundImage = "このコントロールの背景イメージ。";
		public const string ControlDescriptionBorderStyle = "ラベルに表示される境界線を加えるかどうかを決定します。";
		public const string ControlDescriptionFlatStyle = "ユーザーがマウス ポインタをコントロールの上に動かしてクリックしたときの、ボタンの表示を決定します。";
		public const string ControlDescriptionImage = "コントロールに表示されるイメージです。";
		public const string ControlDescriptionImageIndex = "コントロールに表示される ImageList の中のイメージのインデックスです。";
		public const string ControlDescriptionImageKey = "コントロールに表示される ImageList の中のイメージのインデックスです。";
		public const string ControlDescriptionImageList = "コントロールに表示するイメージを取得するための ImageList です。";
		public const string ControlDescriptionImageAlign = "コントロールに表示されるイメージの配置です。";
		public const string ControlDescriptionPreferredHeight = "このコントロールに適切な高さです。";
		public const string ControlDescriptionPreferredWidth = "このコントロールの適切な幅です。";
		public const string ControlDescriptionTextAlign = "ラベル内のテキストの位置を決定します。";
		public const string ControlDescriptionUseCompatibleTextRendering = "テキストの表示が Windows Forms の以前のリリースとの互換性を維持するかどうかを指定します。";
		public const string ControlDescriptionUseMnemonic = "True の場合、アンパサンド & の後の最初の文字がラベルのニーモニック キーとして使用されます。";
		public const string ControlDescriptionAccessibilityObject = "このコントロールのユーザー補助オブジェクトです。";
		public const string ControlDescriptionAccessibleDefaultActionDescription = "コントロールの既定動作の説明です。";
		public const string ControlDescriptionAccessibleDescription = "ユーザー補助クライアントに送信される説明です。";
		public const string ControlDescriptionAccessibleName = "ユーザー補助クライアントに報告される名前です。";
		public const string ControlDescriptionAccessibleRole = "ユーザー補助クライアントに報告される役割です。";
		public const string ControlDescriptionAllowDrop = "コントロールが、ユーザーがドラッグしてきたデータを受け入れられるかどうかを示します。";
		public const string ControlDescriptionAnchor = "コントロールが固定されるコンテナの端を定義します。コ塔gロールがいずれかの端に固定されると、その端と、その端に最も近いコントロールの端の間は、一定の距離を保ちます。 ";
		public const string ControlDescriptionBackColor = "コンポーネントの背景色です。 ";
		public const string ControlDescriptionBindingContext = "コントロールのバインド コンテキストです。";
		public const string ControlDescriptionBottom = "コンテナ座標で表したコントロールの下部です。";
		public const string ControlDescriptionBounds = "コンテナ座標で表したコントロールの境界です。";
		public const string ControlDescriptionCanFocus = "このコントロールがフォーカスを受け取ることができるかどうかを調べます。";
		public const string ControlDescriptionCanSelect = "このコントロールを選択できるかどうかを調べます。";
		public const string ControlDescriptionCapture = "このコントロールで、すべてのマウス入力をキャプチャするかどうかを設定します。";
		public const string ControlDescriptionCausesValidation = "このコンポーネントが検証のイベントを発生させるかどうかを指定します。 ";
		public const string ControlDescriptionClientRectangle = "このコントロールの内部領域の長方形を取得します。";
		public const string ControlDescriptionClientSize = "このコントロールの内部領域のサイズを設定します。";
		public const string ControlDescriptionCompanyName = "ControlCompanyNameDescr";
		public const string ControlDescriptionContainsFocus = "このコントロールまたはその子が現在フォーカスを持っているかを決定します。";
		public const string ControlDescriptionContextMenu = "ユーザーがコントロールを右クリックしたときに表示されるショートカット メニューです。";
		public const string ControlDescriptionContextMenuStrip = "ユーザーがコントロールを右クリックしたときに表示されるショートカット メニューです。";
		public const string ControlDescriptionControls = "このコントロールにおける、子コントロールのコレクションです。";
		public const string ControlDescriptionCreated = "コントロールが完全に作成されたかどうかを示します。";
		public const string ControlDescriptionCursor = "ポインタがコントロールに移動したときに表示されるカーソルです。";
		public const string ControlDescriptionDataBindings = "コントロールのデータ バインドです。";
		public const string ControlDescriptionDisplayRectangle = "このコントロールの表示長方形を取得します。";
		public const string ControlDescriptionIsDisposed = "このコントロールが破棄されたかどうかを示します。";
		public const string ControlDescriptionDisposing = "このコントロールが破棄されているかどうかを示します。 ";
		public const string ControlDescriptionDock = "コンテナに固定されるコントロールの境界線を定義します。 ";
		public const string ControlDescriptionEnabled = "コントロールが有効かどうかを示します。";
		public const string ControlDescriptionFocused = "このコントロールがフォーカスを持っているかどうかを決定します。";
		public const string ControlDescriptionFont = "コントロールでテキストを表示するフォントです。";
		public const string ControlDescriptionForeColor = "テキストを表示するのに使用される、このコンポーネントの前景色です。";
		public const string ControlDescriptionHandle = "このコントロールのネイティブ ハンドルです。";
		public const string ControlDescriptionHasChildren = "コントロールが 1 つ以上の子コントロールを含んでいるかどうかを示します。";
		public const string ControlDescriptionHeight = "ユーザー インターフェイス要素の高さ (ピクセル単位) です。";
		public const string ControlDescriptionIsHandleCreated = "コントロールに、関連付けられたハンドルが含まれるかどうかを示します。";
		public const string ControlDescriptionInvokeRequired = "このコントロール クロス スレッドにアクセスするため、Invoke または BeginInvoke を使用するかどうかを設定します。";
		public const string ControlDescriptionIsAccessible = "コントロールがユーザー補助のアプリケーションで参考可能かどうかを示します。";
		public const string ControlDescriptionIsMirrored = "コントロールがミラー化されているかどうかを示します。";
		public const string ControlDescriptionLeft = "コンテナ座標で表したコントロールの左上です。";
		public const string ControlDescriptionLocation = "コンテナの上部左端に相対する、コントロールの上部左端の座標です。";
		public const string ControlDescriptionMargin = "このコントロールと別のコントロールの余白間のスペースを指定します。";
		public const string ControlDescriptionMaximumSize = "コントロールの最大サイズを指定します。";
		public const string ControlDescriptionMinimumSize = "コントロールの最小サイズを指定します。";
		public const string ControlDescriptionParent = "このコントロールの親";
		public const string ControlDescriptionProductName = "このコンポーネントに関連付けられた製品の名前を取得します。";
		public const string ControlDescriptionProductVersion = "このコンポーネントに関連付けられた製品のバージョンを取得します。";
		public const string ControlDescriptionRecreatingHandle = "このコントロールがハンドルを再作成中かどうかを示します。";
		public const string ControlDescriptionRegion = "このコントロールの領域、または形です。";
		public const string ControlDescriptionRight = "コントロールの右端と、そのコンテナのクライアント領域の左端との間の距離 (ピクセル) です。";
		public const string ControlDescriptionRightToLeft = "コンポーネントが RTL 言語に対して右から左に描画するかどうかを示します。";
		public const string ControlDescriptionSize = "コントロールのサイズ (ピクセル単位) です。";
		public const string ControlDescriptionTabIndex = "このコントロールが持つタブ オーダーのインデックスを決定します。";
		public const string ControlDescriptionTag = "オブジェクトに関連付けられた、ユーザー定義のデータです。";
		public const string ControlDescriptionTop = "コンテナ座標で表したコントロールの最上部の位置です。";
		public const string ControlDescriptionTopLevelControl = "このコントロールを含むトップ レベル コントロールを取得します。";
		public const string ControlDescriptionUseWaitCursor = "このプロパティが true のとき、コントロールとその子コントロールの Cursor プロパティは、WaitCursor に設定されます。";
		public const string ControlDescriptionVisible = "コントロールの表示、非表示を示します。";
		public const string ControlDescriptionWidth = "コンテナ座標で表したコントロールの幅です。";
		public const string ControlDescriptionWindowTarget = "ネイティブ メッセージが送信される場所です。";

		public const string ControlDescriptionPadding = "コントロールの内部スペースを指定します。";
	}

}
