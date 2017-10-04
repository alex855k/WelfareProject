using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogDataConversionServiceApplication.Adapters;
using LogDataConversionServiceApplication.Logfiles;

namespace LogDataConversionServiceApplication
{
	public class LogParser
	{
		ILogFile ToParse;
		LogAdapter Adapter;
		
		public LogParser(ILogFile logfile)
		{
			ToParse = logfile;

			// "Factory" for the adapter.
			switch(ToParse.Parser)
			{
				default:
				case "generic":
					Adapter = new GenericAdapter(); 
					break;
			}
		}
	}
}