using System.Data;

namespace Calculator;

public abstract class Expression
{
    private static List<Expression> _expressions;
    
    static Expression()
    {
        _expressions =
        (
            from assembly in AppDomain.CurrentDomain.GetAssemblies()
            from type in assembly.GetTypes()
            where Attribute.IsDefined(type, typeof(ExpressionAttribute))
            select (Expression)type.GetConstructors()[0].Invoke(null)!
        ).ToList();

    }

    public static Expression FindExpression(Token token)
    {
        foreach (var expression in _expressions)
        {
            if (expression.IsValidToken(token))
            {
                var newExpression = (Expression)expression.GetType().GetConstructors()[0].Invoke(null);
                return newExpression;
            }
        }
        
        throw new InvalidExpressionException($"Expression {token.Value} not found");
    }

    protected abstract bool IsValidToken(Token token);

    public abstract void Compile(List<Token> tokens, ref int startPosition);
}