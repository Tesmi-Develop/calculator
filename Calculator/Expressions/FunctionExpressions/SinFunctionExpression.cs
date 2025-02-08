namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class SinFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "sin";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count == 0)
            throw new InvalidOperationException($"At least one argument is required. {name}");
        return Math.Sin((arguments[0] * (Math.PI)) / 180);
    }
}