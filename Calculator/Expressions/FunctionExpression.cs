namespace Calculator.Expressions;

public abstract class FunctionExpression : CalculableExpression
{
    private List<List<Expression>> _arguments = [];
    protected string Name = string.Empty;
    
    protected abstract bool IsValidToken(string identifier);
    
    protected override bool IsValidToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        return token.Type == TokenType.Identifier && IsValidToken(token.Value);
    }
    
    protected override void OnCompile(List<Token> tokens, ref int index, List<Expression> expressions)
    {
        Name = tokens[index].Value;
        
        var argument = new List<Expression>();
        _arguments = [argument];
        index += 2;
        
        if (tokens[index - 1].Type != TokenType.BracketOpen)
            throw new InvalidOperationException($"Expected '(', ${tokens[index - 2].Value}");
        
        while (tokens[index].Type != TokenType.BracketClose)
        {
            CalculatorCompiler.ProcessToken(tokens[index], tokens, argument, ref index);

            if (tokens[index].Type != TokenType.Comma) 
                continue;
            
            argument = [];
            _arguments.Add(argument);
            index++;
        }
        
        index++;
    }

    protected abstract double OnCompute(List<double> arguments);
    
    protected override double OnCompute(Dictionary<string, double> variables)
    {
        var arguments = new List<double>();

        foreach (var argument in _arguments)
        {
            arguments.Add(CalculatorCompiler.Compute(argument, variables));
        }

        return OnCompute(arguments);
    }
}