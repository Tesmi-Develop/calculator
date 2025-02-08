namespace Calculator;

public static class Program
{
    public static void Main()
    {
        var input = "12 + (3.5 * (8 - (2 / 4) * 3))";
        var lexer = new Lexer();
        var complite = new CalculatorCompiler();
        var tokens = lexer.Parse(input);
        var expressions = complite.Compile(tokens);
    }
}