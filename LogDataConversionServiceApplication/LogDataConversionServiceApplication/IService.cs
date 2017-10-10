using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LogDataConversionServiceApplication
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
 [ServiceContract]
    public interface IService
    {  
        [OperationContract]
       string[][] ParseFromFile(string[] value);

        [OperationContract]
		string[][] ParseFromFileCustom(string[] value, string parser);

		[OperationContract]
		List<string[]> ParseFromURI(string uri);

		[OperationContract]
		List<string[]> ParseFromURICustom(string uri, string parser);

		[OperationContract]
		List<string> GetAlarms();

		[OperationContract]
		List<string> GetAlarmTypes();

		//[OperationContract]
  //      CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

	// Use a data contract as illustrated in the sample below to add composite types to service operations.
	//[DataContract]
	//public class CompositeType
	//{
	//	bool boolValue = true;
	//	string stringValue = "Hello ";

	//	[DataMember]
	//	public bool BoolValue {
	//		get { return boolValue; }
	//		set { boolValue = value; }
	//	}

	//	[DataMember]
	//	public string StringValue {
	//		get { return stringValue; }
	//		set { stringValue = value; }
	//	}
	//}
<<<<<<< HEAD

	// This class has the method named GetKnownTypes that returns a generic IEnumerable.
	static class Helper
	{
		public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
		{
			List<Type> knownTypes =
				new List<Type>();
			// Add any types to include here.
			knownTypes.Add(typeof(AlarmType));
			return knownTypes;
		}
	}
=======
>>>>>>> cc98d3f54ea687212ce47bd75533c1202392a618
}
