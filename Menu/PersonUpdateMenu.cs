namespace CL_UI_Scaffold_V1;

public static partial class Menus
{

	/////////////////////////////////////////////////////////////////////////////
	/// <summary>
	/// Display Person fields and collect input.
	/// </summary>
	/// <param name="person"></param>
	/// <returns>Person</returns>
	public static Person? UpdatePersonFieldUI(Person person)
	{
		if (person is null) throw new Exception("Null person Argument");

		MenuOption menuOption;
		Console.WriteLine();
		Console.WriteLine($"Use ⬆️  and ⬇️  to navigate and press {GREEN}<return>{WHITE} to select:\n");

		//			This marks the Top of the OUTER loop
		(int outerLeft, int outerTop) = Console.GetCursorPosition();
		while(true) 
		{
			Console.SetCursorPosition(0,outerTop);
			ClearInput(outerTop,8);

			//			These are the menu options that will be displayed
			List<MenuOption> menuOptions = new List<MenuOption>();
			menuOptions.Add( new MenuOption(){ Option = 1, OptionLabel = "1.", Label = $"ID:            {WHITE}{person.Id} {RED}        <-- Read Only {WHITE}"} );
			menuOptions.Add( new MenuOption(){ Option = 2, OptionLabel = "2.", Label = $"First Name:    {WHITE}{person.FirstName}"} );
			menuOptions.Add( new MenuOption(){ Option = 3, OptionLabel = "3.", Label = $"Last Name:     {WHITE}{person.LastName} {RED}       <-- Read Only {WHITE}"} );
			menuOptions.Add( new MenuOption(){ Option = 4, OptionLabel = "4.", Label = $"SSN:           {WHITE}{person.SSN} {RED}  <-- Read Only {WHITE}"} );
			menuOptions.Add( new MenuOption(){ Option = 5, OptionLabel = "5.", Label = $"Date of Birth: {WHITE}{person.doB_sys.ToString("o")} {RED} <-- Read Only {WHITE}"} );
			menuOptions.Add( new MenuOption(){ Option = 6, OptionLabel = "", LabelPad = String.Empty, Label = "--------------------------------"} );
			menuOptions.Add( new MenuOption(){ Option = 7, OptionLabel = "S.", Label = "Save all Changes"} );
			menuOptions.Add( new MenuOption(){ Option = 8, OptionLabel = "C.", Label = $"{RED}Cancel {WHITE}"} );
			
			List<int> DisabledList = new List<int>() {1,3,4,5,6};
			List<int> DisabledNumList = new List<int>() {1,3,4,5,6,7,8,9,0};

		/* ----------------------------------
				UPDATE MENU UI
		----------------------------------- */

			menuOption = MainMenuExp(menuOptions, DisabledList, DisabledNumList);

		//------------------------------------------------------------------------
		//			Continue in OUTER_WHILE

		string? input = String.Empty;
		Console.CursorVisible = true;

		//			Display Submenu when appropriate
		switch (menuOption.Option) 
		{
			case 2:		//	First Name
				Console.WriteLine("\nSelect \u001b[32m<return>\u001b[0m to change field.");
				Console.Write($"{GREEN}First Name:  {WHITE}");
				input = Console.ReadLine();
				break;
			case 7:		//	Save all changes
				return person;
			case 8:	//	Cancel
				Console.WriteLine();
				return new Person();
		}
		Console.CursorVisible = false;

		//		Clear the input 			
		(int ll, int tt) = Console.GetCursorPosition();
		for(int i = 1; i <= 2; i++ )
		{
			Console.SetCursorPosition(0, tt-i);
			ClearCurrentConsoleLine();
		}
		
		if (person == null || input == null) return new Person();

		//			Assign the input to the proper variable.
		switch(menuOption.Option)
		{
			case 2: 		//		First Name
				person.FirstName = input;
				break;
		}

		//------------------------------------------------------------------------
		//			If we get here, the person object has been updated.

		//			Clear the input
		(int l, int t) = Console.GetCursorPosition();
		for(int i = 1; i <= 2; i++ )
		{
			Console.SetCursorPosition(0, t-i);
			ClearCurrentConsoleLine();
		}
		} // END_OUTER_WHILE
	}

	
} // END_CLASS
	
