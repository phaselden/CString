namespace StringUtils;

public static class CStringTester
{
	public static void Test()
	{
		CString x = "hello there";
		Output(x);
		
		x = "   wassup  there";
		Output(x);

		x = @"
                SELECT *
                FROM Document
                WHERE DocumentId = 1234;";
		Output(x);
            
		x = @"
        SELECT *
                FROM Document

WHERE DocumentId = 4444;";
		Output(x);
		
	}

	private static void Output(CString cs)
	{
		Console.WriteLine(">> ---------------");
		Console.WriteLine(cs);
		Console.WriteLine("<< ---------------");
	}
}
