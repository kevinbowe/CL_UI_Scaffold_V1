namespace CL_UI_Scaffold_V1;
using System.Reflection;
using System.Text;
// using static PersonExtPortrait;

public static class PersonExt
{
	public static void PadLeft(this StringBuilder strBuilder, int spaces)
	{
		strBuilder.Insert(0," ", spaces);	
	}
	public static string PadLeft(this string str, int spaces)
	{
		for(int space = 1; space<=spaces; ++space)
			str = " " + str;
		return str;
	}

	public static string SetFriendlyName(PropertyInfo personPropInfo)
	{
		string propMatch;
		// Create friendly names
		switch(personPropInfo.Name) 
		{
			case "InsertDte_sys" :
				propMatch = "Added";
				break;
			case "doB_sys" :
				propMatch = "DoB";
				break;
			case "WorkStartsAt_sys" :
				propMatch = "Start Time";
				break;
			case "CurrentTime_sys" :
				propMatch = "Added On";
				break;
			default:
				propMatch = personPropInfo.Name;
				break;
		}
		return propMatch;
	}

	public static StringBuilder OutputProperties(
		this Person person, string[]? personPropertyFilterList = null, string orientation = "landscape") 
	{
		var strBuilder = new StringBuilder();

		//			Is there a Property FilterList ?
		if (personPropertyFilterList == null) 
		{
			//		If we get here; There is no Property FilterList
			if (orientation == "landscape")
			{
				// Grab all the properties in the Persons class.
				IList<PropertyInfo> personPropInfoList= new List<PropertyInfo>(person.GetType().GetProperties());
				foreach(PropertyInfo personPropInfo in personPropInfoList) 
				{
					strBuilder = person.OutputPersonFriendlyName(personPropInfo, strBuilder, orientation);
				}
				//		Indent the completed string.
				strBuilder.PadLeft(5);		
			}
			else  // Portrait
			{
				// Grab all the properties in the Persons class.
				IList<PropertyInfo> personPropInfoList= new List<PropertyInfo>(person.GetType().GetProperties());
				foreach(PropertyInfo personPropInfo in personPropInfoList) 
				{
					strBuilder = person.OutputPersonFriendlyNamePortrait(personPropInfo, strBuilder, orientation);			
				}
			}
			return strBuilder;
		}

		//		If we get here; There is a Property FilterList
		if (orientation == "landscape")
		{
			strBuilder = person.OutputPropertiesList(personPropertyFilterList, orientation);
			//		Indent the completed string.
			strBuilder.PadLeft(5);	
			return strBuilder;
		}
		else  // Portrait
		{
			strBuilder = person.OutputPropertiesListPortrait(personPropertyFilterList, orientation);
			return strBuilder;
		}
	}
}
