namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class SqrtFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "sqrt";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count == 0)
            throw new InvalidOperationException($"At least one argument is required. {Name}");
        return Math.Sqrt(arguments[0]);
    }
}