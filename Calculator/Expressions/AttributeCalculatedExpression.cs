using System.Data;

namespace Calculator.Expressions;

public abstract class AttributeCalculatedExpression : CalculableExpression
{
    protected override int Priority => 2;
    protected abstract string Identifier { get; }
    protected CalculableExpression Expression = null!;

    protected override bool IsValidToken(Token token, List<Expression> expressions, List<Token> tokens, int index)
    {
        return token.Type == TokenType.Identifier && token.Value == Identifier;
    }
    
    protected override void OnCompile(List<Token> tokens, ref int index, List<Expression> expressions)
    {
        if (expressions.Count == 0 || expressions.Last() is not CalculableExpression)
            throw new InvalidExpressionException("attribute of the calculable expression can be applied only after the evaluated expression");
        
        Expression = (CalculableExpression)expressions.Last();
        expressions.RemoveAt(expressions.Count - 1);
        index++;
    }
}