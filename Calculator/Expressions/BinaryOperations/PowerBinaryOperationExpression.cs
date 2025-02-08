namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class PowerBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority Priority => BinaryOperationPriority.Middle;
    
    public override void Compile(List<Token> tokens, ref int startPosition) {}

    protected override bool _IsValidToken(Token token)
    {
        return token.Value == "**";
    }

    public override double Compute(NumericalExpression left, NumericalExpression right)
    {
        return Math.Pow(left.Compute(), right.Compute());
    }
}

