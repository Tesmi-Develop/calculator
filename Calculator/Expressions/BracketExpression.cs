namespace Calculator.Expressions;

[Expression]
public class BracketExpression : GroupExpression
{
    protected override TokenType StartTokenType { get; } = TokenType.BracketOpen;
    protected override TokenType EndTokenType { get; } = TokenType.BracketClose;
}