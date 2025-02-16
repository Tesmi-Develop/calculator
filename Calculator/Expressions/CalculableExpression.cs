namespace Calculator.Expressions;

public abstract class CalculableExpression : Expression
{
    private int _sign = 1;
    
    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        var tokenPosition = startPosition - 1;
        
        if (tokens.Count > 1 && 
            startPosition > 0 && 
            tokens[tokenPosition].Type == TokenType.BinaryOperation && 
            (tokens[tokenPosition].Value == "+" || tokens[tokenPosition].Value == "-")
           )
            if (expressions.Count > 0 &&
                expressions.Last() is BinaryOperationExpression &&
                ((BinaryOperationExpression)expressions.Last()).IsUnary
               )
            {
                var prevToken = tokens[tokenPosition];
                expressions.RemoveAt(expressions.Count - 1);
                _sign = prevToken.Value == "-" ? -1 : 1;
            }
        
        OnCompile(tokens, ref startPosition, expressions);
    }

    protected abstract void OnCompile(List<Token> tokens, ref int startPosition, List<Expression> expressions);
    
    public double Compute(Dictionary<string, double> variables)
    {
        return _sign * OnCompute(variables);
    }

    protected abstract double OnCompute(Dictionary<string, double> variables);
}