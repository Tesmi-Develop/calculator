namespace Calculator.Expressions;

public enum BinaryOperationPriority
{
    Lowest,
    Middle,
    Highest,
}

public abstract class BinaryOperationExpression : Expression
{
    public abstract BinaryOperationPriority Priority { get; }
    public bool IsUnary { get; protected set; } = false;
    
    protected abstract bool _IsValidToken(Token token, List<Expression> expressions);
    
    protected override bool IsValidToken(Token token, List<Expression> expressions)
    {
        if (token.Type != TokenType.BinaryOperation)
            return false;

        return _IsValidToken(token, expressions);
    }
    
    public abstract double Compute(NumericalExpression left, NumericalExpression right);
}