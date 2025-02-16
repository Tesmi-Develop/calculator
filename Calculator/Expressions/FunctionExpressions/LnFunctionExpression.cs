namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class LnFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "ln";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count < 1)
            throw new InvalidOperationException($"Invalid number of arguments. ${Name}");
        return Math.Log(arguments[0]);
    }
}