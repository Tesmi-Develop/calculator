namespace Calculator.Expressions.ConstantExpressions;

[Expression]
public class EConstantExpression : ConstantExpression
{
    protected override string Name => "e";
    protected override double Value => Math.E;
}