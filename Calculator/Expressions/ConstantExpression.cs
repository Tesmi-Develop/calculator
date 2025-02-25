namespace Calculator.Expressions;

[Expression]
public abstract class ConstantExpression : CalculableExpression
{
    protected abstract string Name { get; }
    protected abstract double Value { get; }
    protected override int Priority => 2;
    
    protected override bool IsValidToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        return token.Type == TokenType.Identifier && token.Value.ToLower() == Name;
    }

    protected override void OnCompile(List<Token> tokens, ref int index, List<Expression> expressions) {}

    protected override double OnCompute(Dictionary<string, double> variables)
    {
        return Value;
    }
}