namespace CL_UI_Scaffold_V1;

using System.ComponentModel.Design;
using System.Reflection;
using System.Text;
using static PersonExt;
using static Helper;

public static class PersonExtLandscape
{

	public static StringBuilder OutputPersonFriendlyName( 
		this Person person, PropertyInfo personPropInfo, StringBuilder strBuilder, string propMatch = "")
	{
		//			This will return the friendly Name "Dob"
		propMatch = SetFriendlyName(personPropInfo);

		//			Fetch the value associated with the actual property name
		object? propertyValue = personPropInfo?.GetValue(person);
 		//		
 		if (personPropInfo?.PropertyType.Name == "DateOnly")
 		{
			propertyValue = ObjectToDateOnlyFmt(propertyValue);
 		}
		var str =    $"{Menus.GREEN} {propMatch}: {Menus.WHITE} {propertyValue}";

		var padder = 20;
		//
		switch(personPropInfo?.Name)
		{
			case "Id" :
				strBuilder.Append(str.PadRight(padder,' '));
				break;
			case "SSN" :
				strBuilder.Append(str.PadRight(7+padder,' '));
				break;
			case "FirstName" :
				strBuilder.Append(str.PadRight(13+padder,' '));
				break;
			case "LastName" :
				strBuilder.Append(str.PadRight(16+padder,' '));
				break;
			case "CurrentTime_sys" : // Added-On
				strBuilder.Append(str.PadRight(28+padder,' '));
				break;
			case "InsertDte_sys" : // Added
				strBuilder.Append(str.PadRight(20+padder,' '));
				break;
			case "WorkStartsAt_sys" : // Start Time
				strBuilder.Append(str.PadRight(11+padder,' '));
				break;
			case "doB_sys" : // DoB
				strBuilder.Append(str.PadRight(7+padder,' '));
				break;
		}
		return strBuilder;
	}
	
	
	public static StringBuilder OutputPropertiesList(
		this Person person, string[] OutputProperties, string orientation) 
	{
		var strBuilder = new StringBuilder();
		
		//		Get all of the property names in the Person Class.
		IList<PropertyInfo> personPropInfoList= new List<PropertyInfo>(person.GetType().GetProperties());
		
		//		Search the OutputProperties looking for a matching property
		foreach(PropertyInfo personPropInfo in personPropInfoList)
		{
			string? propMatch = OutputProperties.Where(e => personPropInfo.Name == e).FirstOrDefault();
			if (propMatch == null) continue;

			strBuilder = person.OutputPersonFriendlyName(personPropInfo, strBuilder, propMatch);
		}
		return strBuilder;
	}
}
