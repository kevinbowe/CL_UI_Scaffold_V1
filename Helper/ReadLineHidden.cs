namespace CL_UI_Scaffold_V1;

public static partial class Helper
{
	public static string? ReadLineHidden()
	{
		Console.CursorVisible = false;
		return Console.ReadLine();
	}
}