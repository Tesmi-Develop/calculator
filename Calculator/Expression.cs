using System.Data;

namespace Calculator;

public abstract class Expression
{
    private static readonly List<Expression> Expressions = [];
    
    static Expression()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (Attribute.IsDefined(type, typeof(ExpressionAttribute)))
                {
                    if (type.IsAbstract)
                        continue;
                    Expressions.Add((Expression)type.GetConstructors()[0].Invoke(null)!);
                }
            }
        }
    }

    public static Expression FindExpression(Token token)
    {
        foreach (var expression in Expressions)
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