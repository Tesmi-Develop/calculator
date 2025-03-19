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
        var tokens = Lexer.Parse(input);
        var expressions = CalculatorCompiler.Compile(tokens);
        return CalculatorCompiler.Compute(expressions, variables);
    }
}