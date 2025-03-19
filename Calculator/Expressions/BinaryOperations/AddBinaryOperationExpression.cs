namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class AddBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority PriorityOperation => BinaryOperationPriority.Lowest;
    public override BinaryOperationAssociativity Associativity => BinaryOperationAssociativity.Left;
    
    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions) 
    {
        IsUnary = !(expressions.Count > 0 && expressions.Last() is not BinaryOperationExpression);
    }

    protected override bool _IsValidToken(Token token, List<Expression> expressions)
    {
        return token.Value == "+";
    }

    public override double Compute(CalculableExpression left, CalculableExpression right, Dictionary<string, double> variables)
    {
        return left.Compute(variables) + right.Compute(variables);
    }
}