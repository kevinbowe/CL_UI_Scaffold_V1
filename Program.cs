// namespace CL_UI_Scaffold_V1;
using CL_UI_Scaffold_V1;
using System.Text;

using static CL_UI_Scaffold_V1.Menus;
// using static Menus;

////////////////////////////////////////////////////////////////////////////////
///	App Header
LoadHeadUI();

int BOTTOM_OF_RESULTS = 0;

//			Save the current cursor position.
int TOP_OF_CONSOLE = Console.GetCursorPosition().Top;

while(true)
{
	//		Restore the current cursor position to the Top
	Console.SetCursorPosition(0, TOP_OF_CONSOLE);

	////////////////////////////////////////////////////////////////////////////////
	//			Load the Main Menu
	MenuOption menuOption = MainMenuExp();

	int UNDER_MENU = Console.GetCursorPosition().Top;

	/////////////////////////////////////////////////////////////////////////////
	///		Clear the current results.
	///		

	//			Calculate the number of lines that should be cleared.
	int linesToClear = BOTTOM_OF_RESULTS - UNDER_MENU;
	if (linesToClear > 0) ClearInput(UNDER_MENU,linesToClear);

	//			Handle the Q  Quit main menu option.
	if (menuOption.Label.Contains('Q')) 
	{ 
		System.Console.WriteLine();
		return;
	}

	////////////////////////////////////////////////////////////////////////////////
	//		Always write one blank line here.
	Console.WriteLine();

	////////////////////////////////////////////////////////////////////////////////
	///		Process Input

	Console.CursorVisible = true;

	int personId = 1;
	Person inputPerson = new Person();
	//
	//		Display Submenu when appropriate
	switch (menuOption.Option) 
	{
		// case 1:	break; //	Display ALL persons
		case 2:	//	Get Person by Id
			personId = ReadPersonIdUI("Enter the Person Id to display:  ");
			break;
		case 3:	//	Delete Person by Id
			personId = ReadPersonIdUI("Enter the Person Id to delete:  ");
			break;
		// case 4:	break; //	Delete person with Person Object -- Create person object
		case 5:	//	Insert Person -- Create person object
			inputPerson = InsertPersonUI();
			break;
		case 6:	//	Update person -- create person object
			personId = ReadPersonIdUI("Enter the Person Id to update:  ");
			break;
	}
	Console.ResetColor();
	Console.CursorVisible = false;

	////////////////////////////////////////////////////////////////////////////////
	///		VALIDATE INPUT
	if (inputPerson.Id < 0) return;

	///  CONTINUE  /////////////////////////////////////////////////////////////////

	await new ProcessWorkers().ProcessWork(menuOption.Option, personId, inputPerson);

	var continueQuitMenuOption = ContinueQuitMenu();
	if(continueQuitMenuOption.Label.Contains('Q')) 
	{ 
		System.Console.WriteLine(); 
		return; 
	}

	BOTTOM_OF_RESULTS = Console.GetCursorPosition().Top;

} // END_WHILE(true)

////////////////////////////////////////////////////////////////////////////////
void LoadHeadUI()
{
	Console.Clear();
	Console.OutputEncoding = Encoding.UTF8;
	Console.CursorVisible = false;
	Console.ForegroundColor = ConsoleColor.Cyan;

	Console.WriteLine("API Lifetime Experiment.");
	Console.WriteLine("This App requires dotnet_API/ API_Scaffold_V1 to be running.\n");

	Console.ResetColor();

	Console.WriteLine($"\nTo Navigate, use ⬆️  ⬇️  or enter menu Number to select menu choice. ");
	Console.WriteLine("Press \u001b[32m<return>\u001b[0m to EXECUTE selected choice.\n");
}

public class MenuOption
{
	public int Option {get; set;} = 0;
	public string OptionLabel {get; set;} = String.Empty;
	public string LabelPad {get; set;} = "   "; //false;
	public string Label {get; set;} = String.Empty;
}
