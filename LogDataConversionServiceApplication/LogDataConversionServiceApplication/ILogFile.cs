using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDataConversionServiceApplication
{
	public interface ILogFile
	{
		string Parser { get; }

		List<string> GetHeaders();
		List<string[]> GetData();
	}
}