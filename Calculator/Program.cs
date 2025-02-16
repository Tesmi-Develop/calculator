namespace Calculator;

public static class Program
{
    public static void Main()
    {
        while (true)
        {
            var input = Console.ReadLine() ?? string.Empty;

            try
            {
                Console.WriteLine(Calculator.ComputeExpression(input));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}