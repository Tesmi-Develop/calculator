namespace Calculator.Expressions.ConstantExpressions;


[Expression]
public class PiConstantExpression : ConstantExpression
{
    protected override string Name => "pi";
    protected override double Value => Math.PI;
}