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
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
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
