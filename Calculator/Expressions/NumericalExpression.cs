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
        _token = tokens[startPosition];

        if (tokens.Count > 1 && 
            startPosition > 0 && 
            tokens[startPosition - 1].Type == TokenType.BinaryOperation && 
            (tokens[startPosition - 1].Value == "+" || tokens[startPosition - 1].Value == "-")
            )
            if (expressions.Count > 0 &&
                expressions.Last() is BinaryOperationExpression &&
                ((BinaryOperationExpression)expressions.Last()).IsUnary
               )
            {
                var prevToken = tokens[startPosition - 1];
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