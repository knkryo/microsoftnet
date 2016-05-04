
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 大小チェックを行う
	/// </summary>
	/// <remarks></remarks>
	public class NumericFromToValidation : AbstructValidation
	{


		private string _ToValue = string.Empty;

        #region IsValid

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="FromValue">開始値</param>
		/// <param name="ToValue">終了値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

        public NumericFromToValidation(string FromValue, string ToValue)
		{
			this._ValidationValue = FromValue;
			this._ToValue = ToValue;

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 検証を実行する
		/// </summary>
		/// <returns>True = 検証は正常 , False = 検証にて異常</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool IsValid()
		{
            int outValue = 0;

			//同じか小さければ正
            if (int.TryParse(this._ValidationValue, out outValue) == true && int.TryParse(this._ToValue, out outValue) == true)
            {
				return int.Parse(this._ValidationValue) <= int.Parse(this._ToValue);
			} else {
                //TODO 後で直す
                return false;// (this._ValidationValue.ToString() <= this._ToValue.ToString());
			}

		}

		#endregion

	}

}
