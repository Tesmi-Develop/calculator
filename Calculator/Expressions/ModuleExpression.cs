namespace Calculator.Expressions;

[Expression]
public class ModuleExpression : GroupExpression
{
    private static int _maxDepth = 0;
    private static int _depth = 0;
    protected override TokenType StartTokenType { get; } = TokenType.VerticalBar;
    protected override TokenType EndTokenType { get; } = TokenType.VerticalBar;

    protected override bool IsContinuedToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        return _depth != _maxDepth;
    }

    protected override void OnCompile(List<Token> tokens, ref int index, List<Expression> expressions)
    {
        _depth++;
        base.OnCompile(tokens, ref index, expressions);
    }

    public override void PreCompile(List<Token> tokens)
    {
        _depth = 0;
        var count = 0;

        foreach (var token in tokens)
        {
            if (token.Type == TokenType.VerticalBar)
                count++;
        }

        if (count % 2 != 0)
            throw new InvalidOperationException("Invalid modules");
        
        _maxDepth = count / 2;
    }
    
    protected override double OnCompute(Dictionary<string, double> variables)
    {
        return Math.Abs(base.OnCompute(variables));
    }
}