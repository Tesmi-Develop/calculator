using JetBrains.Annotations;

namespace Calculator;

[PublicAPI]
public static class Calculator
{
    public static double ComputeExpression(string input)
    {
        return ComputeExpression(input, []);
    }
    
    public static double ComputeExpression(string input, Dictionary<string, double> variables)
    {
        var lexer = new Lexer();
        
        var tokens = lexer.Parse(input);
        var expressions = CalculatorCompiler.Compile(tokens);
        return CalculatorCompiler.Compute(expressions, variables);
    }
}