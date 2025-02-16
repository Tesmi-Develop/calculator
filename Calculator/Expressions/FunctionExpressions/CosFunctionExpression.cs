namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class CosFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "cos";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count == 0)
            throw new InvalidOperationException($"At least one argument is required. {Name}");
        return Math.Cos((arguments[0] * (Math.PI)) / 180);
    }
}