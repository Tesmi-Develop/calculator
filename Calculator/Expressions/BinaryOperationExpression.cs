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
    
    protected abstract bool _IsValidToken(Token token);
    
    protected override bool IsValidToken(Token token)
    {
        if (token.Type != TokenType.BinaryOperation)
            return false;

        return _IsValidToken(token);
    }
    
    public abstract double Compute(NumericalExpression left, NumericalExpression right);
}