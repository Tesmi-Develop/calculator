namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class PowerBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority PriorityOperation => BinaryOperationPriority.Highest;
    public override BinaryOperationAssociativity Associativity => BinaryOperationAssociativity.Right;
    
    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions) {}

    protected override bool _IsValidToken(Token token, List<Expression> expressions)
    {
        return token.Value == "**";
    }

    public override double Compute(CalculableExpression left, CalculableExpression right, Dictionary<string, double> variables)
    {
        return Math.Pow(left.Compute(variables), right.Compute(variables));
    }
}

