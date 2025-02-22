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

    public static Expression? FindExpression(Token token, List<Expression> expressions, List<Token> tokens, int index)
    {
        foreach (var expression in Expressions)
        {
            if (expression.IsValidToken(token, expressions, tokens, index))
            {
                var newExpression = (Expression)expression.GetType().GetConstructors()[0].Invoke(null);
                return newExpression;
            }
        }

        throw new InvalidExpressionException("Invalid expression");
    }

    public static void InvokePreCompile(List<Token> tokens)
    {
        foreach (var expression in Expressions)
        {
            expression.PreCompile(tokens);
        }
    }
    
    public virtual void PreCompile(List<Token> tokens) {} 

    protected abstract bool IsValidToken(Token token, List<Expression> expressions, List<Token> tokens, int index);

    public abstract void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions);
}