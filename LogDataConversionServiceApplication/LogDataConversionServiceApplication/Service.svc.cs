﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LogDataConversionServiceApplication.Logfiles;
using LogDataConversionServiceApplication.Adapters;

namespace LogDataConversionServiceApplication
{
	public class Service : IService
	{
		public string[][] ParseFromFile(string[] log)
		{
			return this.ParseFromFileCustom(log, "generic"); // In the future, this should be "smarter" and be able to determine adapter itself.
			// Or just make the genereic adapter smart and have it work for "everything".
		}

		public string[][] ParseFromFileCustom(string[] log, string parser)
		{
			TextLog LogFile = new TextLog(log);
			LogFile.Parser = parser;
			LogParser LogParser = new LogParser(LogFile);

			return LogParser.TryParse().ToArray();
		}

		public List<string[]> ParseFromURI(string uri)
		{
			return this.ParseFromURICustom(uri, "generic");
		}

		public List<string[]> ParseFromURICustom(string uri, string parser)
		{
			throw new NotImplementedException();
		}

		public List<string> GetAlarms()
		{
			return new List<string> { "Door Open", "Bed Sensor" };
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
