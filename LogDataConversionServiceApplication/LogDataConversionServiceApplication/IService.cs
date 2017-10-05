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
		[ServiceKnownType("GetKnownTypes", typeof(Helper))]
		[OperationContract]
        List<string[]> ParseLog(List<string[]> value);


		[OperationContract]
		List<string[]> ParseLog2(List<string[]> value, string Parser);

		[OperationContract]
		AlarmType TestMethod();

		//[OperationContract]
  //      CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

	[DataContract(Name = "AlarmType")]
	public enum AlarmType
	{
		[EnumMemberAttribute]
		DoorOpen,
		[EnumMemberAttribute]
		BedSensor
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
}
