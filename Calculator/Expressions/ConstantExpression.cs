namespace Calculator.Expressions;

[Expression]
public abstract class ConstantExpression : NumericalExpression
{
    private int _unaryOperator = 1;
    protected abstract string Name { get; }
    protected abstract double Value { get; }
    
    protected override bool IsValidToken(Token token, List<Expression> expression)
    {
        return token.Type == TokenType.Identifier && token.Value.ToLower() == Name;
    }

    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
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
                _unaryOperator = prevToken.Value == "+" ? 1 : -1;
            }
    }

    public override void Compile(double number) {}

    public override double Compute()
    {
        return Value * _unaryOperator;
    }
}