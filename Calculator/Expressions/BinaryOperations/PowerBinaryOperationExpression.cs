namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class PowerBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority Priority => BinaryOperationPriority.Highest;
    
    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions) {}

    protected override bool _IsValidToken(Token token, List<Expression> expressions)
    {
        return token.Value == "**";
    }

    public override double Compute(NumericalExpression left, NumericalExpression right)
    {
        return Math.Pow(left.Compute(), right.Compute());
    }
}

