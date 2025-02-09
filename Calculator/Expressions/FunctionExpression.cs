namespace Calculator.Expressions;

public abstract class FunctionExpression : NumericalExpression
{
    protected List<List<Expression>> Arguments = [];
    protected string name = string.Empty;
    
    protected abstract bool IsValidToken(string identifier);
    
    protected override bool IsValidToken(Token token, List<Expression> expression)
    {
        if (token.Type != TokenType.Identifier)
            return false;

        return IsValidToken(token.Value);
    }
    
    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        name = tokens[startPosition].Value;
        
        var argument = new List<Expression>();
        Arguments = [argument];
        startPosition += 2;
        
        if (tokens[startPosition - 1].Type != TokenType.BracketOpen)
            throw new InvalidOperationException($"Expected '(', ${tokens[startPosition - 2].Value}");
        
        while (tokens[startPosition].Type != TokenType.BracketClose)
        {
            var token = tokens[startPosition];
            var expression = Expression.FindExpression(token, argument);
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
    
    public override double Compute()
    {
        var compiler = new CalculatorCompiler();
        var arguments = new List<double>();

        foreach (var argument in Arguments)
        {
            arguments.Add(compiler.Compute(argument));
        }

        return OnCompute(arguments);
    }
}