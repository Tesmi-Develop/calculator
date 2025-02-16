namespace Calculator;

public static class Calculator
{
    public static double ComputeExpression(string input)
    {
        var lexer = new Lexer();
        var compiler = new CalculatorCompiler();
        
        var tokens = lexer.Parse(input);
        var expressions = compiler.Compile(tokens);
        return compiler.Compute(expressions);
    }
}