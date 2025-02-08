namespace Calculator;

public static class Program
{
    public static void Main()
    {
        var input = "12 * (3.5 * (8 - (2 / 4) * 3))"; // 273

        try
        {
            var lexer = new Lexer();
            var compiler = new CalculatorCompiler();
            var tokens = lexer.Parse(input);
            var expressions = compiler.Compile(tokens);
            var result = compiler.Compute(expressions);
            
            Console.WriteLine(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}