namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class LerpFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "lerp";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count < 3)
            throw new InvalidOperationException($"Invalid number of arguments. {name}");
        
        return (arguments[1] - arguments[0]) * arguments[2] + arguments[0];
    }
}