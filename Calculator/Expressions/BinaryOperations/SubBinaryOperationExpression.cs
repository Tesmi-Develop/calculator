namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class SubBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority Priority => BinaryOperationPriority.Lowest;
    
    public override void Compile(List<Token> tokens, ref int startPosition) {}

    protected override bool _IsValidToken(Token token)
    {
        return token.Value == "-";
    }

    public override double Compute(NumericalExpression left, NumericalExpression right)
    {
        return left.Compute() - right.Compute();
    }
}