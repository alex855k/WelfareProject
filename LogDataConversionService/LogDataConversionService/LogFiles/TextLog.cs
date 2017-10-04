using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDataConversionService.LogFiles
{
	public class TextLog : ILogFile
	{
		public string Parser { get; set; }
		private List<string[]> Data;

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
