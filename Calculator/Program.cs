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
            //break;
        }
    }
}