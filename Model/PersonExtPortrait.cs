namespace CL_UI_Scaffold_V1;
using System.Reflection;
using System.Text;
using static PersonExt;
using static Helper;

public static class PersonExtPortrait
{

	public static StringBuilder OutputPersonFriendlyNamePortrait( 
		this Person person, PropertyInfo personPropInfo, StringBuilder strBuilder, string orientation)
	// public static StringBuilder OutputPersonFriendlyNamePortrait( 
	// 	this Person person, PropertyInfo personPropInfo, StringBuilder strBuilder, string propMatch = "")
	{
		var propMatch = SetFriendlyName(personPropInfo);

		//			Fetch the value associated with the actual property name
		object? propertyValue = personPropInfo?.GetValue(person);
 		//		
 		if (personPropInfo?.PropertyType.Name == "DateOnly")
 		{
			propertyValue = ObjectToDateOnlyFmt(propertyValue);
 		}
		var str = $"{Menus.GREEN}{propMatch}: {Menus.WHITE} {propertyValue}";

		if (orientation == "portrait")
		{
		//		ORIGINAL -- WORKS
		// return strBuilder.Append( str.PadLeftLineFeed(5) + "\n");

		//			Pad the input string with Nx spaces to the left.
		//			Append an '\n' to the end of the line.
		return strBuilder.Append( PadLeft(str,5) + '\n' );
		}

		//////////////////////////////////////////////////////////////////////////
		//			If we get here, orientation == "landscape".
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
		
	public static string PadLeft(string str, int spaces)
	{
		for(int i = 1; i <= spaces; i++) str = ' ' + str; 
		return str;
	}

	public static StringBuilder OutputPropertiesListPortrait(
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
			strBuilder = OutputPersonFriendlyNamePortrait(person, personPropInfo, strBuilder, orientation);
			// strBuilder = OutputPersonFriendlyNamePortrait(person, personPropInfo, strBuilder, propMatch);
		}
		return strBuilder;
	}

}
