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

    public static Expression? FindExpression(Token token, List<Expression> expressions)
    {
        foreach (var expression in Expressions)
        {
            if (expression.IsValidToken(token, expressions))
            {
                var newExpression = (Expression)expression.GetType().GetConstructors()[0].Invoke(null);
                return newExpression;
            }
        }

        throw new InvalidExpressionException("Invalid expression");
    }

    protected abstract bool IsValidToken(Token token, List<Expression> expressions);

    public abstract void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions);
}