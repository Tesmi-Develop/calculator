namespace Calculator.Expressions;

public enum BinaryOperationPriority
{
    Lowest,
    Middle,
    Highest,
}

public abstract class BinaryOperationExpression : Expression
{
    public abstract BinaryOperationPriority PriorityOperation { get; }
    public bool IsUnary { get; protected set; } = false;
    
    protected abstract bool _IsValidToken(Token token, List<Expression> expressions);
    
    protected override bool IsValidToken(Token token, List<Expression> expressions, List<Token> tokens, int index)
    {
        if (token.Type != TokenType.BinaryOperation)
            return false;

        return _IsValidToken(token, expressions);
    }
    
    public abstract double Compute(CalculableExpression left, CalculableExpression right, Dictionary<string, double> variables);
}