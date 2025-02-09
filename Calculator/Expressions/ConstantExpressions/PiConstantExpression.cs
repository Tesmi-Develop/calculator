namespace Calculator.Expressions.ConstantExpressions;

public class PiConstantExpression : ConstantExpression
{
    protected override string Name => "pi";
    protected override double Value => Math.PI;
}