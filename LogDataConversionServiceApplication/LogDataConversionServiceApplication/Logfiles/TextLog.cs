using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDataConversionServiceApplication.Logfiles
{
	public class TextLog : ILogFile
	{
		private List<string[]> Data;
		public string Parser { get; set; }

		public List<string[]> GetData()
		{
			throw new NotImplementedException();
		}

		public List<string> GetHeaders()
		{
			throw new NotImplementedException();
		}
	}
}