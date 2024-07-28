using CL_UI_V6_SCFD_V1;

public static partial class Helper
{
	public static string? ReadLineHidden()
	{
		Console.CursorVisible = false;
		return Console.ReadLine();
	}
}