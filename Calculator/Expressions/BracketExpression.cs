namespace Calculator.Expressions;

[Expression]
public class BracketExpression : GroupExpression
{
    protected override TokenType StartTokenType { get; } = TokenType.BracketOpen;
    protected override TokenType EndTokenType { get; } = TokenType.BracketClose;

    public override void PreCompile(List<Token> tokens)
    {
        var depth = 0;

        foreach (var token in tokens)
        {
            if (token.Type == TokenType.BracketOpen)
                depth++;
            
            if (token.Type == TokenType.BracketClose)
                depth--;
            
            if (depth < 0)
                throw new InvalidOperationException("Invalid brackets");
        }

        if (depth != 0)
            throw new InvalidOperationException("Invalid brackets");
    }
}