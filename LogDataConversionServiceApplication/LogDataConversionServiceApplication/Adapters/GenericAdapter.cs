using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDataConversionServiceApplication.Adapters
{
	public class GenericAdapter : LogAdapter
	{
		// Colum Headers that determine a DataType
		List<string> DateHeaders = new List<string> { "Tid" };
		List<string> NameHeaders = new List<string> { "Navn" };
		List<string> AlarmHeaders = new List<string> { "Alarm" };
		List<string> AddressHeaders = new List<string> { "Afdeling" };
		List<string> PlaceHeaders = new List<string> { "Bolig" };
		List<string> CallcodeHeaders = new List<string> { "Personale" };


		public override List<string[]> ParseData(List<string[]> Data)
		{
			return Data;
		}

		public override List<string> ParseHeaders(List<string> Headers)
		{
			return Headers;
		}
	}
}