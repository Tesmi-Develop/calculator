namespace Calculator.Expressions;

public abstract class PreProcessorExpression : Expression
{
    public abstract void Run(Dictionary<string, double> variables);
}