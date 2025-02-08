namespace Calculator;

public class CalculatorCompiler
{
    private void ValidateTokens(List<Token> tokens)
    {
        var depth = 0;

        foreach (var token in tokens)
        {
            if (token.Type == TokenType.BracketOpen)
                depth++;
            
            if (token.Type == TokenType.BracketClose)
                depth--;
            
            if (depth < 0)
                throw new InvalidOperationException("Invalid brackets");
        }

        if (depth != 0)
            throw new InvalidOperationException("Invalid brackets");
    }

    public List<Expression> Compile(List<Token> tokens)
    {
        return Compile(tokens, 0, tokens.Count);
    }
    
    public List<Expression> Compile(List<Token> tokens, int start, int end)
    {
        ValidateTokens(tokens);
        var expressions = new List<Expression>();
        var index = start;
        
        while (index < end)
        {
            var token = tokens[index];
            var expression = Expression.FindExpression(token);
            var oldIndex = index;
            expression.Compile(tokens, ref index);
            expressions.Add(expression);
            
            if (index == oldIndex)
                index++;
        }
        
        return expressions;
    }
    
     
    
    public void Run() {}
}