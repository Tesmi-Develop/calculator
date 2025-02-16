using System.Globalization;

namespace Calculator.Expressions;

[Expression]
public class NumericalExpression : CalculableExpression
{
    private Token _token;
    private double? _value;
    
    protected override bool IsValidToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        return token.Type == TokenType.Number;
    }

    protected override void OnCompile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        _token = tokens[startPosition];
    }

    public void Compile(double number)
    {
        _value = number;
    }

    protected override double OnCompute(Dictionary<string, double> variables)
    {
        return _value ?? double.Parse(_token.Value, CultureInfo.InvariantCulture);
    }
}