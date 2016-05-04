using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Common.ClassLibrary.FileIO
{

	/// <summary>
	/// クラスシリアライザ
	/// </summary>
	/// <remarks></remarks>
	[Serializable()]
	public class ClassSerializer
	{

		#region 設定ファイル出力(Serialize)

		/// <summary>
		/// 設定ファイル出力
		/// </summary>
		/// <param name="settingFullPath">設定ファイルを出力するためのフルパス</param>
        /// <returns>True = 正常出力 , False = 出力失敗</returns>
        /// <remarks></remarks>
		public bool Serialize(string settingFullPath)
		{

			return this.Serialize(settingFullPath, this.GetType());

		}

		/// <summary>
		/// 設定ファイル出力
		/// </summary>
		/// <param name="settingFullPath">設定ファイルを出力するためのフルパス</param>
		/// <param name="type">取得するクラスのタイプ</param>
		/// <returns>True = 正常出力 , False = 出力失敗</returns>
		/// <remarks></remarks>
		public bool Serialize(string settingFullPath, System.Type type)
		{
            return this.Serialize(settingFullPath, type, this);
		}

        /// <summary>
        /// 設定ファイル出力
        /// </summary>
        /// <param name="settingFullPath">設定ファイルを出力するためのフルパス</param>
        /// <param name="type">取得するクラスのタイプ</param>
        /// <returns>True = 正常出力 , False = 出力失敗</returns>
        /// <remarks></remarks>
        public bool Serialize(string settingFullPath, System.Type type,object target)
        {

            XmlSerializer serializer = null;
            FileStream st = null;


            try
            {
                serializer = new XmlSerializer(this.GetType());
                st = new FileStream(settingFullPath, FileMode.Create);

                serializer.Serialize(st, target);

                return true;


            }
            catch (System.Exception)
            {
                return false;


            }
            finally
            {
                st.Close();
                st = null;
                serializer = null;

            }

        }

		#endregion

		#region "設定ファイル読込(DeSerialize)"

		/// <summary>
		/// 設定ファイル読込
		/// </summary>
		/// <param name="settingFullPath">設定ファイルのフルパス</param>
		/// <returns>指定したクラスの実体</returns>
		/// <remarks></remarks>
		public object DeSerialize(string settingFullPath)
		{

			return this.DeSerialize(settingFullPath, this.GetType());

		}

		/// <summary>
		/// 設定ファイル読込
		/// </summary>
		/// <param name="settingFullPath">設定ファイルのフルパス</param>
		/// <param name="type">取得するクラスのタイプ</param>
		/// <returns>指定したクラスの実体</returns>
		/// <remarks></remarks>
		public object DeSerialize(string settingFullPath, System.Type type)
		{

			XmlSerializer serializer = null;
			FileStream st = null;
			object targetClass = null;
			System.IO.FileInfo fi = null;


			try {
				serializer = new XmlSerializer(type);

				fi = new System.IO.FileInfo(settingFullPath);

				//読み取り専用を強制的に解除
				if (fi.IsReadOnly == true) {
					System.IO.File.SetAttributes(settingFullPath, (System.IO.FileAttributes)(System.IO.File.GetAttributes(settingFullPath) - System.IO.FileAttributes.ReadOnly));
				}

				st = new FileStream(settingFullPath, FileMode.Open);

				targetClass = serializer.Deserialize(st);

				return targetClass;


			} catch (System.Exception ex) {
				throw ex;


			} finally {
				if ((st != null)) {
					st.Close();
					st = null;
				}
				serializer = null;

			}

		}

		#endregion

	}

}
