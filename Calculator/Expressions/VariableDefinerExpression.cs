using System.Data;

namespace Calculator.Expressions;

[Expression]
public class VariableDefinerExpression : PreProcessorExpression
{
    public VariableExpression Variable { get; private set; } = null!;
    private CalculableExpression _calculableExpression = null!;
 
    protected override bool IsValidToken(Token token, List<Expression> expressions, List<Token> tokens, int index)
    {
        return token.Type == TokenType.Equal;
    }

    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        if (expressions.Count == 0 || expressions[^1] is not VariableExpression)
            throw new InvalidExpressionException("Expected variable expression");
        
        Variable = (VariableExpression)expressions[^1];
        expressions.RemoveAt(expressions.Count - 1);

        startPosition++;
        
        var nextExpressionIndex = expressions.Count;
        CalculatorCompiler.ProcessToken(tokens[startPosition], tokens, expressions, ref startPosition);
        
        if (expressions.Count <= nextExpressionIndex || expressions[nextExpressionIndex] is not CalculableExpression)
            throw new InvalidExpressionException("Expected variable expression");
        
        _calculableExpression = (CalculableExpression)expressions[nextExpressionIndex]; 
        expressions.RemoveAt(nextExpressionIndex);
    }

    public override void Run(Dictionary<string, double> variables)
    {
        variables[Variable.Token.Value] = _calculableExpression.Compute(variables);
    }
}