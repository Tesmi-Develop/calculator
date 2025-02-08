namespace Calculator.Expressions;

[Expression]
public class NumericalExpression : Expression
{
    private Token _token;
    protected override bool IsValidToken(Token token)
    {
        return token.Type == TokenType.Number;
    }

    public override void Compile(List<Token> tokens, ref int startPosition)
    {
        _token = tokens[startPosition];
    }

    public virtual double Compute()
    {
        return double.Parse(_token.Value);
    }
}