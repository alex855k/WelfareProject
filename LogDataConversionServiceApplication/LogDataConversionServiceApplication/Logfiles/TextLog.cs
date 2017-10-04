using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDataConversionServiceApplication.Logfiles
{
	public class TextLog : ILogFile
	{
		public TextLog(List<string[]> log)
		{
			this.Data = log;
		}

		private List<string[]> Data;
		public string Parser { get; set; }

		public List<string[]> GetData()
		{
			List<string[]> ToReturn = Data.ToList<string[]>();
			ToReturn.RemoveAt(0); // Remove the headers.

			return ToReturn;
		}

		public List<string> GetHeaders()
		{
			return Data[0].ToList<string>();
		}
	}
}