using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDataConversionServiceApplication.Adapters
{
	public class GenericAdapter : LogAdapter
	{
		public override List<string[]> ParseData(List<string> Data)
		{
			throw new NotImplementedException();
		}

		public override List<string> ParseHeaders(List<string> Headers)
		{
			throw new NotImplementedException();
		}
	}
}