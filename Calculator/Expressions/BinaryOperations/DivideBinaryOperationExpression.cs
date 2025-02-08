namespace Calculator.Expressions.BinaryOperations;

[Expression]
public class DivideBinaryOperationExpression : BinaryOperationExpression
{
    public override BinaryOperationPriority Priority => BinaryOperationPriority.Middle;
    
    public override void Compile(List<Token> tokens, ref int startPosition) {}

    protected override bool _IsValidToken(Token token)
    {
        return token.Value == "/";
    }
}