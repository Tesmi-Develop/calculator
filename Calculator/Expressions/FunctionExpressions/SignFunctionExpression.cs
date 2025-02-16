namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class SignFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "sign";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count < 1)
            throw new InvalidOperationException($"Invalid number of arguments. ${Name}");
        return Math.Sign(arguments[0]);
    }
}