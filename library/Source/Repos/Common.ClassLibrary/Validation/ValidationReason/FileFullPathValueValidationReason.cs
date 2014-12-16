
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

    /// <summary>
    /// ÉtÉ@ÉCÉãÇÃFullPathåüèÿåãâ Çï‘Ç∑ValueClass
    /// </summary>
	public class FileFullPathValueValidationReason : ValidationReason
	{

		public enum FilePathValidationReason
		{

			Success = 0,

			InvalidFilePathChar,

			InvalidFileNameChar,

			RelativePath,

			UnKnownPath

		}

		public FileFullPathValueValidationReason(FilePathValidationReason filePathValidationReason)
		{
			switch (filePathValidationReason) {

				case FileFullPathValueValidationReason.FilePathValidationReason.Success:

					base.SetStatus(true, "", "");

					break;
				case FileFullPathValueValidationReason.FilePathValidationReason.InvalidFileNameChar:

					base.SetStatus(false, FileFullPathValueValidationReason.FilePathValidationReason.InvalidFileNameChar.ToString(), "0");

					break;
				default:

					break;
			}
		}

	}

}
