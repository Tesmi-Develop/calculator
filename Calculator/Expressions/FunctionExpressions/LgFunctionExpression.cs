namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class LgFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "lg";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count < 1)
            throw new InvalidOperationException($"Invalid number of arguments. ${name}");
        return Math.Log10(arguments[0]);
    }
}