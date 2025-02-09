namespace Calculator.Expressions.ConstantExpressions;

public class EConstantExpression : ConstantExpression
{
    protected override string Name => "e";
    protected override double Value => Math.E;
}