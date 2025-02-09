namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class MultiplyBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority Priority => BinaryOperationPriority.Middle;
    
    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions) {}

    protected override bool _IsValidToken(Token token, List<Expression> expressions)
    {
        return token.Value == "*";
    }

    public override double Compute(NumericalExpression left, NumericalExpression right)
    {
        return left.Compute() * right.Compute();
    }
}