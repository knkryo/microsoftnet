using System;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// アクセス可能なファイルパスであるかどうかを判定するValidation
	/// </summary>
	/// <remarks>ライブラリを使用してアクセスしてみる</remarks>
	public class AccessableFilePathValidation : AbstructValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------


		public AccessableFilePathValidation(string validationValue)
		{
			this._ValidationValue = validationValue;
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

			string tmpString = string.Empty;

			tmpString = _ValidationValue;

			//フルパス

			if (System.IO.Path.IsPathRooted(tmpString) == true) {
				//指定ディレクトリに一時ファイルを作ってみる

				try {
                    tmpString = IO.Path.FileOperationUtils.GetTempFileName(_ValidationValue);

					if ((tmpString.Length > 0)) {
						System.IO.File.Delete(tmpString);
						return true;
					} else {
						return false;
					}


				}
                catch (AccessViolationException)
                {
					//アクセス違反が起きるのであれば、書き込みできない
					return false;


				} catch (System.Exception)
                {
					//その他エラーでも書き込みできない
					return false;
                }
                finally
                {
				}



			} else {
				return false;
			}



		}


	}


}
