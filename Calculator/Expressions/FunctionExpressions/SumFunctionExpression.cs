namespace Calculator.Expressions.FunctionExpressions;

[Expression]
public class SumFunctionExpression : FunctionExpression
{
    private VariableDefinerExpression _variableDefiner = null!;
    private List<Expression> endNumberExpressions = null!;
    private List<Expression> calculationExpressions = null!;
    protected override bool IsValidToken(string identifier)
    {
        return identifier.ToLower() == "sum";
    }

    protected override void OnCompile(List<Token> tokens, ref int index, List<Expression> expressions)
    {
        base.OnCompile(tokens, ref index, expressions);
        
        if (Arguments.Count != 3 || Arguments[0].Count != 1 || Arguments[0][0] is not VariableDefinerExpression variableDefinerExpression)
            throw new ArgumentException("Sum function expects 3 arguments. Example: sum(i=0, 10, i*i)");
        
        _variableDefiner = variableDefinerExpression;
        endNumberExpressions = Arguments[1];
        calculationExpressions = Arguments[2];
    }

    protected override double OnCompute(List<double> arguments)
    {
        throw new NotImplementedException();
    }

    protected override double OnCompute(Dictionary<string, double> variables)
    {
        _variableDefiner.Run(variables);

        var endPoint = CalculatorCompiler.Compute(endNumberExpressions, variables);
        var result = 0.0;
        
        for (var i = variables[_variableDefiner.Variable.Token.Value]; i <= endPoint; i++)
        {
            var cloned = new List<Expression>();
            cloned.AddRange(calculationExpressions);
            variables[_variableDefiner.Variable.Token.Value] = i;
            result += CalculatorCompiler.Compute(cloned, variables);
        }

        return result;
    }
}