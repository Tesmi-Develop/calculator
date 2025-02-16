namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class SubBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority Priority => BinaryOperationPriority.Lowest;

    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        IsUnary = !(expressions.Count > 0 && expressions.Last() is not BinaryOperationExpression);
    }

    protected override bool _IsValidToken(Token token, List<Expression> expressions)
    {
        return token.Value == "-";
    }

    public override double Compute(CalculateExpression left, CalculateExpression right)
    {
        return left.Compute() - right.Compute();
    }
}