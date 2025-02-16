namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class DivideBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority Priority => BinaryOperationPriority.Middle;
    
    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions) {}

    protected override bool _IsValidToken(Token token, List<Expression> expressions)
    {
        return token.Value == "/";
    }

    public override double Compute(CalculableExpression left, CalculableExpression right, Dictionary<string, double> variables)
    {
        return left.Compute(variables) / right.Compute(variables);
    }
}