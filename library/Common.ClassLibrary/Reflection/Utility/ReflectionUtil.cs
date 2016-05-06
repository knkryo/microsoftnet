using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Common.ClassLibrary.Reflection
{

	/// <summary>
	/// Refrectionクラスを活用するためのユーティリティクラス
	/// </summary>
	/// <remarks></remarks>
    public class ReflectionUtil
	{

        /// <summary>
        /// プロパティ情報をdataTableとして返す
        /// </summary>
        /// <param name="AssembryName"></param>
        /// <param name="ClassName"></param>
        /// <returns></returns>
		public DataTable GetAssembryPropertyDescription(string AssembryName, string ClassName)
		{

			DataTable dt = null;
			System.Reflection.Assembly asm = null;



			try {
				dt = new DataTable();

				dt.Columns.Add("NameSpace");
				dt.Columns.Add("ClassName");
				dt.Columns.Add("DeclaringType");
				dt.Columns.Add("DeclaringTypeName");
				dt.Columns.Add("PropertName");
				dt.Columns.Add("Description");

				asm = System.Reflection.Assembly.Load(AssembryName);

				System.Type tp = null;

				tp = asm.GetType(ClassName);

				if ((tp != null)) {
					foreach (System.Reflection.PropertyInfo pi in tp.GetProperties()) {

						foreach (object o in pi.GetCustomAttributes(true)) {
							if (o is System.ComponentModel.DescriptionAttribute) {
								dt.Rows.Add(dt.NewRow());
								{
									dt.Rows[dt.Rows.Count - 1]["NameSpace"] = tp.Namespace;
									dt.Rows[dt.Rows.Count - 1]["ClassName"] = tp.FullName;
									dt.Rows[dt.Rows.Count - 1]["DeclaringType"] = tp.DeclaringType;
									if ((tp.DeclaringType != null)) {
										dt.Rows[dt.Rows.Count - 1]["DeclaringTypeName"] = tp.DeclaringType.Name.ToString();
									}
									dt.Rows[dt.Rows.Count - 1]["PropertName"] = pi.Name;
									dt.Rows[dt.Rows.Count - 1]["Description"] = ((System.ComponentModel.DescriptionAttribute)o).Description.ToString();

								}
							}
						}
					}
				}

				return dt;


			} catch (ApplicationException ex) {
				throw ex;

			} finally {
				dt = null;

			}

		}


	}

}
