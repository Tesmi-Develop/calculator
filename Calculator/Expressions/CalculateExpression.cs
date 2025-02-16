namespace Calculator.Expressions;

public abstract class CalculateExpression : Expression
{
    private int _sign = 1;
    
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
                _sign = prevToken.Value == "-" ? -1 : 1;
            }
        
        OnCompile(tokens, ref startPosition, expressions);
    }

    protected abstract void OnCompile(List<Token> tokens, ref int startPosition, List<Expression> expressions);
    
    public double Compute()
    {
        return _sign * OnCompute();
    }

    protected abstract double OnCompute();
}