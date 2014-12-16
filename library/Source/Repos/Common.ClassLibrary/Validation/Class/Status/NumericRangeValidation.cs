
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 数値の範囲をチェックするValidation
    /// 範囲内であればTrue,範囲外の場合はFalse
	/// </summary>
	/// <remarks></remarks>
	public class NumericRangeValidation : AbstructValidation
	{

		private int _minValue;

		private int _maxValue;

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <param name="minValue">許可する最小値</param>
		/// <param name="maxValue">許可する最大値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

        public NumericRangeValidation(string validationValue, int minValue, int maxValue):base(validationValue)
		{
			this._minValue = minValue;
			this._maxValue = maxValue;

		}

        #region IsValid

        /// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 検証を実行する
		/// </summary>
		/// <returns>True = 検証は正常 , False = 検証にて異常</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool IsValid()
		{
            int outWk;

            if (int.TryParse(_ValidationValue, out outWk) == false)
            {
				return false;
			}

			return this._minValue <= int.Parse(this._ValidationValue) && int.Parse(this._ValidationValue) <= this._maxValue;

		}

		#endregion

	}

}
