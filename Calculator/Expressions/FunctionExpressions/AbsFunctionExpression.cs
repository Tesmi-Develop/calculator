namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class AbsFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "abs";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count == 0)
            throw new InvalidOperationException($"At least one argument is required. {name}");
        return Math.Abs(arguments[0]);
    }
}