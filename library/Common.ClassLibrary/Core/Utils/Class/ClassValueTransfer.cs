using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Reflection;

namespace Common.ClassLibrary
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// クラス間のキャストを行うUtilityクラス
	/// </summary>
	/// <remarks></remarks>
	/// <history>
	///     2008/03/26 Ryo Kaneko Ver.1.0.0 Created
	/// </history>
	/// ------------------------------------------------------------------------------------------
    public class ClassValueTransfer
	{
		#region "Private Section"

        private object _sourceClass = null;

		private object _destClass = null;
		#endregion

		#region "Public Property"

		#region "複写元のクラスインスタンスを返します (SourceClassInstance)"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 複写元のクラスインスタンスを返します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public object SourceClassInstance {
			get { return this._sourceClass; }
		}

		#endregion

		#region "複写先のクラスインスタンスを返します (DestClassInstance)"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 複写先のクラスインスタンスを返します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public object DestClassInstance {
			get { return this._destClass; }
		}

		#endregion

		#endregion

		#region "Public Method"

		#region "複写の実行 (Invoke)"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 複写の実行
		/// </summary>
		/// <param name="sourceClassInstance"></param>
		/// <param name="DestClassInstance"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public void Invoke(object sourceClassInstance, object DestClassInstance)
		{
			Type nextValue = null;

			_sourceClass = sourceClassInstance;
			_destClass = DestClassInstance;


			try {

				if ((DestClassInstance == null)) {
					throw new System.Exception("");

				}

				nextValue = _destClass.GetType();

				//複写処理

				foreach (PropertyInfo propertyInfo in _sourceClass.GetType().GetProperties()) {

					if ((((nextValue.GetProperty(propertyInfo.Name)) != null))) {

						if ((nextValue.GetProperty(propertyInfo.Name).CanWrite == true)) {
							nextValue.InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, _destClass, new object[] { propertyInfo.GetValue(_sourceClass, null) });

						}

					}

				}


			} catch (System.Exception ex) {
				throw ex;


			} finally {
				nextValue = null;
				GC.Collect();

			}

		}

		#endregion

		#endregion

	}

}
