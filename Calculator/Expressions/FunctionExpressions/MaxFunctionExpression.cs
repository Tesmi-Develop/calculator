namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class MaxFunctionExpression : FunctionExpression
{
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "max";
    }

    protected override double OnCompute(List<double> arguments)
    {
        var max = arguments[0];

        foreach (var value in arguments)
        {
            max = Math.Max(max, value);
        }
        
        return max;
    }
}