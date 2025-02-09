namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class ClampFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "clamp";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count < 3)
            throw new InvalidOperationException($"Invalid number of arguments. {name}");
        
        return Math.Clamp(arguments[0], arguments[1], arguments[2]);
    }
}