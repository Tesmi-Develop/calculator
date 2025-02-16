namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class TgFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "tan" || identifier.ToLower() == "tg";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count == 0)
            throw new InvalidOperationException($"At least one argument is required. {Name}");
        return Math.Tan((arguments[0] * (Math.PI)) / 180);
    }
}