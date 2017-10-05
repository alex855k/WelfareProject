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
			return this.ParseLog2(log, "generic"); // In the future, this should be "smarter" and be able to determine adapter itself.
			// Or just make the genereic adapter smart and have it work for "everything".
		}

		public List<string[]> ParseLog2(List<string[]> log, string parser)
		{
			TextLog Log = new TextLog(log);
			LogParser Parser = new LogParser(Log);

			return Parser.Parse();
		}

		public List<string> GetAlarmTypes()
		{
			var AlarmTypes = new List<string>();
			AlarmTypes.Add("Door Open");
			AlarmTypes.Add("Bed Sensor");

			return AlarmTypes;
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
