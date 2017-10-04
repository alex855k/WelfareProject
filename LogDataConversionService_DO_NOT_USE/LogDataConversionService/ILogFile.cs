using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDataConversionService
{
	interface ILogFile
	{
		string Parser { get; }

		List<string> GetHeaders();
		List<string[]> GetData();
	}
}
