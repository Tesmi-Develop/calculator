using System.Globalization;

namespace Calculator.Expressions;

[Expression]
public class NumericalExpression : Expression
{
    private Token _token;
    private double? _value;
    
    protected override bool IsValidToken(Token token)
    {
        return token.Type == TokenType.Number;
    }

    public override void Compile(List<Token> tokens, ref int startPosition)
    {
        _token = tokens[startPosition];
    }

    public void Compile(double number)
    {
        _value = number;
    }

    public virtual double Compute()
    {
        return _value ?? double.Parse(_token.Value, CultureInfo.InvariantCulture);
    }
}