namespace CL_UI_Scaffold_V1;
using System.Reflection;
using System.Text;
using static PersonExt;
using static Helper;

public static class PersonExtPortrait
{
	public static string PadLeftLineFeed(this string str, int spaces)
	{
		for(int i = 1; i <= spaces; i++) str = ' ' + str; 
		return str + "\n";
	}

	public static StringBuilder OutputPersonFriendlyNamePortrait( 
		this Person person, PropertyInfo personPropInfo, StringBuilder strBuilder, string propMatch = "")
	{
		propMatch = SetFriendlyName(personPropInfo);

		//			Fetch the value associated with the actual property name
		object? propertyValue = personPropInfo?.GetValue(person);
 		//		
 		if (personPropInfo?.PropertyType.Name == "DateOnly")
 		{
			propertyValue = ObjectToDataOnlyFmt(propertyValue);
 		}
		var str = $"{Menus.GREEN} {propMatch}: {Menus.WHITE} {propertyValue}";

		return strBuilder.Append( str.PadLeftLineFeed(5) );
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

			if(orientation == "landscape")
			{
				strBuilder = person.OutputPersonFriendlyName(personPropInfo, strBuilder, propMatch);
			}
			else 
			{
				strBuilder = OutputPersonFriendlyNamePortrait(person, personPropInfo, strBuilder, propMatch);
			}
		}
		return strBuilder;
	}

}
