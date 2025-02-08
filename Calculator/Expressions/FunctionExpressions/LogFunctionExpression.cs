namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class LogFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "log";
    }

    protected override double OnCompute(List<double> arguments)
    {
        if (arguments.Count < 2)
            throw new InvalidOperationException($"Invalid number of arguments. ${name}");
        return Math.Log(arguments[0], arguments[1]);
    }
}