namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class MinFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "min";
    }

    protected override double OnCompute(List<double> arguments)
    {
        var min = arguments[0];

        foreach (var value in arguments)
        {
            min = Math.Min(min, value);
        }
        
        return min;
    }
}