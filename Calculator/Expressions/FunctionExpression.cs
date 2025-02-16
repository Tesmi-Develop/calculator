namespace Calculator.Expressions;

public abstract class FunctionExpression : CalculableExpression
{
    protected List<List<Expression>> Arguments = [];
    protected string Name = string.Empty;
    
    protected abstract bool IsValidToken(string identifier);
    
    protected override bool IsValidToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        if (token.Type != TokenType.Identifier)
            return false;

        return IsValidToken(token.Value);
    }
    
    protected override void OnCompile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        Name = tokens[startPosition].Value;
        
        var argument = new List<Expression>();
        Arguments = [argument];
        startPosition += 2;
        
        if (tokens[startPosition - 1].Type != TokenType.BracketOpen)
            throw new InvalidOperationException($"Expected '(', ${tokens[startPosition - 2].Value}");
        
        while (tokens[startPosition].Type != TokenType.BracketClose)
        {
            var token = tokens[startPosition];
            var expression = Expression.FindExpression(token, argument, tokens, startPosition);
            var oldIndex = startPosition;
            
            expression?.Compile(tokens, ref startPosition, expressions);
            if (expression != null) 
                argument.Add(expression);

            if (startPosition == oldIndex)
                startPosition++;

            if (tokens[startPosition].Type != TokenType.Comma) 
                continue;
            
            argument = [];
            Arguments.Add(argument);
            startPosition++;
        }
        
        startPosition++;
    }

    protected abstract double OnCompute(List<double> arguments);
    
    protected override double OnCompute(Dictionary<string, double> variables)
    {
        var compiler = new CalculatorCompiler();
        var arguments = new List<double>();

        foreach (var argument in Arguments)
        {
            arguments.Add(compiler.Compute(argument, variables));
        }

        return OnCompute(arguments);
    }
}