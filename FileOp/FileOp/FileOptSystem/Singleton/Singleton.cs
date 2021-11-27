using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Singleton<T> where T : new()
{
	protected static T _instance;
	protected static object _lockObj = new object();

	public static T Ins()
	{
		if (null == _instance)
		{
			lock(_lockObj)
			{
				_instance = new T();
			}		
		}

		return _instance;
	}

	public bool CheckStringValid(string strValue)
	{
		if (null == strValue)
			return false;

		if (strValue.Length <= 0)
			return false;

		return true;
	}

	public bool CheckListValid<T1>(List<T1> listValue)
	{
		if (null == listValue)
			return false;

		if (listValue.Count <= 0)
			return false;

		return true;
	}

	public bool CheckArrayValid<T1>(T1[] arrValue)
	{
		if (null == arrValue)
			return false;

		if (arrValue.Length <= 0)
			return false;

		return true;
	}
};
