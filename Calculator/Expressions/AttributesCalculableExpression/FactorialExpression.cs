using System.Numerics;

namespace Calculator.Expressions.AttributesCalculableExpression;

[Expression]
public class FactorialExpression : AttributeCalculatedExpression
{
    protected override string Identifier { get; } = "!";
    
    protected override double OnCompute(Dictionary<string, double> variables)
    {
        var num = Expression.Compute(variables);
        BigInteger result = 1;
        
        if (num <= 0 || num % 1 != 0)
            throw new ArgumentException("Factorial expression must be a natural number");

        var casted = (int)num;
        for (BigInteger i = 2; i <= casted; i++)
            result *= i;

        return (double)result;
    }

}