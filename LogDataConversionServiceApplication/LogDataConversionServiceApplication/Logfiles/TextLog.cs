using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LogDataConversionServiceApplication.Logfiles
{
	public class TextLog : ILogFile
	{
		private List<Char> Seperators = new List<Char> { '\t' };
		public TextLog(string[] log)
		{
			this.Data = log;
		}

		private string[] Data;
		public string Parser { get; set; }

		public List<string[]> GetData()
		{
			List<string> DataList = Data.ToList<string>();
			DataList.RemoveAt(0); // Remove the headers.

			List<string[]> ToReturn = new List<string[]>();

			foreach(string Line in DataList)
			{
				string[] ArrLine = Line.Split(Seperators.ToArray());
				ToReturn.Add(ArrLine);
			}

			return ToReturn;
		}

		public List<string> GetHeaders()
		{
			string Headers = Data[0];

			return Headers.Split(Seperators.ToArray()).ToList<string>(); // Split the headers by the seperators, and put it to a list
		}
	}
}