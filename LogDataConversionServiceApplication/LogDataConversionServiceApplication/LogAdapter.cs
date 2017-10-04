using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDataConversionServiceApplication
{
	public abstract class LogAdapter
	{
		public abstract List<string> ParseHeader(List<string> Headers);
		public abstract List<string[]> ParseData(List<string> Data);
	}
}