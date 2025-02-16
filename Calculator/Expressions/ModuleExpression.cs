namespace Calculator.Expressions;

[Expression]
public class ModuleExpression : GroupExpression
{
    protected override TokenType StartTokenType { get; } = TokenType.VerticalBar;
    protected override TokenType EndTokenType { get; } = TokenType.VerticalBar;
    
    protected override double OnCompute(Dictionary<string, double> variables)
    {
        return Math.Abs(base.OnCompute(variables));
    }
}