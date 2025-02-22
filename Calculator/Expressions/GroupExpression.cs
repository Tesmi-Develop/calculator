namespace Calculator.Expressions;

public abstract class GroupExpression : CalculableExpression
{
    private List<Expression> _expressions = null!;
    protected abstract TokenType StartTokenType { get; }
    protected abstract TokenType EndTokenType { get; }
    
    protected override bool IsValidToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        return token.Type == StartTokenType;
    }

    protected virtual bool IsContinuedToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        return false;
    }

    protected override void OnCompile(List<Token> tokens, ref int index, List<Expression> expressions)
    {
        _expressions = [];
        index++;
        
        while (tokens[index].Type != EndTokenType || IsContinuedToken(tokens[index], expressions, tokens, index))
        {
            CalculatorCompiler.Instance.ProcessToken(tokens[index], tokens, _expressions, ref index);
        }
        
        index++;
    }

    protected override double OnCompute(Dictionary<string, double> variables)
    {
        var compiler = new CalculatorCompiler();
        return compiler.Compute(_expressions, variables);
    }
}