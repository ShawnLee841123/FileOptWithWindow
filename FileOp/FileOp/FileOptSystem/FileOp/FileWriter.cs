using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class FileWriter: Singleton<FileWriter>
{
	public enum WriteFileResultType
	{
		WFRT_SUCCESS = 0,
		WFRT_ERROR_FILE_NAME,
		WFRT_ERROR_CONTENT,
	};

	public WriteFileResultType WriteFile(string strFileName, string strFileContent, Action<string> pWriteFunc)
	{
		if (!CheckStringValid(strFileName))
			return WriteFileResultType.WFRT_ERROR_FILE_NAME;

		if (!CheckStringValid(strFileContent))
			return WriteFileResultType.WFRT_ERROR_CONTENT;

		using (FileStream pFileStream = new FileStream(strFileName, FileMode.OpenOrCreate))
		{
			byte[] arrContent = System.Text.Encoding.Default.GetBytes(strFileContent);
			int nCount = arrContent.Length;
			pFileStream.Write(arrContent, 0, nCount);
			pFileStream.Flush();
		}
		return WriteFileResultType.WFRT_SUCCESS;
	}

	public WriteFileResultType WriteFile(string strFileName, string strFileContent)
	{
		if (!CheckStringValid(strFileName))
			return WriteFileResultType.WFRT_ERROR_FILE_NAME;

		if (!CheckStringValid(strFileContent))
			return WriteFileResultType.WFRT_ERROR_CONTENT;

		using (FileStream pFileStream = new FileStream(strFileName, FileMode.OpenOrCreate))
		{
			byte[] arrContent = System.Text.Encoding.Default.GetBytes(strFileContent);
			int nCount = arrContent.Length;
			pFileStream.Write(arrContent, 0, nCount);
			pFileStream.Flush();
		}
		return WriteFileResultType.WFRT_SUCCESS;
	}
}

