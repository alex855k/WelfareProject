using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LogDataConversionServiceApplication.Logfiles;

namespace LogDataConversionServiceApplication
{
	public class Service : IService
	{
		public List<string[]> ParseLog(List<string[]> log)
		{
			return this.ParseLog(log, "generic");
		}

		public List<string[]> ParseLog(List<string[]> log, string parser)
		{
			TextLog Log = new TextLog(log);
			LogParser Parser = new LogParser(Log);

			return Parser.Parse();
		}

		//public CompositeType GetDataUsingDataContract(CompositeType composite)
		//{
		//    if (composite == null)
		//    {
		//        throw new ArgumentNullException("composite");
		//    }
		//    if (composite.BoolValue)
		//    {
		//        composite.StringValue += "Suffix";
		//    }
		//    return composite;
		//}
	}
}
