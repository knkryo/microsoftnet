
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// ValidationBaseクラス
	/// </summary>
	public abstract class AbstructValidation : IValidation
	{

		/// <summary>
		/// 検証値を保持する
		/// </summary>
		/// <remarks></remarks>


		protected string _ValidationValue;
		protected IValidationReason _ValidReason;

        public AbstructValidation()
        {
        }

        public AbstructValidation(string validationValue)
        {
            this._ValidationValue = validationValue;
        }
        #region "InvalidReason"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// Validメソッド実行後の実行結果を返します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public virtual IValidationReason InvalidReason {
			get { return (IValidationReason)this._ValidReason; }
		}

		#endregion

		#region IsValid

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 検証を実行する
		/// </summary>
		/// <returns>True = 検証は正常 , False = 検証にて異常</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool IsValid();
		private bool IValidation_Valid()
		{
			return IsValid();
		}
		bool IValidation.IsValid()
		{
			return IValidation_Valid();
		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 検証を実行する
		/// </summary>
		/// <param name="validationValue"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public virtual bool IsValid(string validationValue)
		{
			this._ValidationValue = validationValue;
			return this.IsValid();
		}
		private bool IValidation_Valid(string validationValue)
		{
			return IsValid(validationValue);
		}
		bool IValidation.IsValid(string validationValue)
		{
			return IValidation_Valid(validationValue);
		}

		#endregion

		#region "SetInvalidReason"

		/// <summary>
		/// 検証結果を設定する
		/// </summary>
		/// <remarks></remarks>
		protected virtual void SetInvalidReason(IValidationReason invalidReason)
		{
			this._ValidReason = (IValidationReason)invalidReason;
		}

		#endregion

	}

}
