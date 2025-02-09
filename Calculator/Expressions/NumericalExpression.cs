using System.Globalization;

namespace Calculator.Expressions;

[Expression]
public class NumericalExpression : Expression
{
    private Token _token;
    private double? _value;
    
    protected override bool IsValidToken(Token token, List<Expression> expression)
    {
        return token.Type == TokenType.Number;
    }

    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        var prevToken = tokens[startPosition - 1];
        _token = tokens[startPosition];

        if (prevToken.Type == TokenType.BinaryOperation && 
            (prevToken.Value == "+" || prevToken.Value == "-")
            )
            if (expressions.Count > 0 &&
                expressions.Last() is BinaryOperationExpression &&
                ((BinaryOperationExpression)expressions.Last()).IsUnary
               )
            {
                expressions.RemoveAt(expressions.Count - 1);
                _token = new Token(TokenType.Identifier, $"{prevToken.Value}{_token.Value}");
            }
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