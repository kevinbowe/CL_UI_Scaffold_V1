using CL_UI_Scaffold_V1;
using System.Text;

public class ProcessWorkers
{
	public HttpClient client = new HttpClient();
	public PersonResponse? personResponse { get; set; }

	public async Task<object> ProcessWork(int option, int personId, Person inputPerson)
	{
		var strBuilder = new StringBuilder();
		string[]? propertyList = null;

		switch (option)
		{
			case 1: // Get Persons
				PersonListResponse personListResponse = await GetPersons.GetPersonsAsync(client);
				if(personListResponse.PersonList == null) break;

				foreach (Person? person in personListResponse.PersonList)
				{
					propertyList = new[] { "Id", "FirstName", "LastName", "SSN", "doB_sys", "WorkStartsAt_sys" };
					strBuilder = person.OutputProperties(propertyList, "landscape"); // Landscape by default
					Console.WriteLine($"{strBuilder}");
				}
				Console.WriteLine();
				break;

			case 2: // Get PersonById
				personResponse = await GetPersonById.GetPersonByIdAsync(client, personId);
				if (personResponse.Person is null) break;

				propertyList = new[] { "Id", "FirstName", "LastName", "SSN", "doB_sys", "WorkStartsAt_sys" };
				strBuilder = personResponse.Person.OutputProperties(propertyList, "portrait");
				Console.WriteLine($"{strBuilder}");
				break;

			case 3: // Update Person
				personResponse = await GetPersonById.GetPersonByIdAsync(client, personId);

				if(personResponse.Person == null) break;

				//////////////////////////////////////////////////////////////////////////
				//		Update Person Menu

				Person? personObj = Menus.UpdatePersonFieldUI(personResponse.Person);
				if (personObj == null || personObj.Id == 0) return new object();

				//////////////////////////////////////////////////////////////////////////
				Console.WriteLine(await UpdatePerson.UpdatePersonAsync(client, personObj));
				Console.WriteLine();
				//
				if (personResponse.Person is not null)
				{
					propertyList = new[] { "Id", "FirstName", "LastName", "SSN", "doB_sys", "WorkStartsAt_sys" };
					strBuilder = personResponse.Person.OutputProperties(propertyList, "portrait");
					Console.WriteLine($"{strBuilder}");
				}
				break;
		}
		return new object();
	}
}
