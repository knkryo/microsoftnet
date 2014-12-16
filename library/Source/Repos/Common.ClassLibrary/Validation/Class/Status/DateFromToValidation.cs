
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 日付のFromToチェックを行うValidation
	/// </summary>
	/// <remarks></remarks>
	public class DateRangeValidation : AbstructValidation
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

		public DateRangeValidation(string FromValue, string ToValue)
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
            DateTime outDt;

			//同じか小さければ正
            if (System.DateTime.TryParse(this._ValidationValue, out outDt) == true && System.DateTime.TryParse(this._ToValue, out outDt) == true)
            {
				return System.DateTime.Parse(this._ValidationValue) <= System.DateTime.Parse(this._ToValue);
			} else {
                //TODO 後で直す
                return false;// (this._ValidationValue <= this._ToValue);
			}

		}

		#endregion

	}

}
