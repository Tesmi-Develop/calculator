namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class CtgFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "ctan" || identifier.ToLower() == "ctg";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count == 0)
            throw new InvalidOperationException($"At least one argument is required. {name}");
        return 1 / Math.Tan((arguments[0] * (Math.PI)) / 180);
    }
}