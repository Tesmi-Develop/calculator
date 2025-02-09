namespace Calculator.Expressions;

[Expression]
public class GroupExpression : NumericalExpression
{
    private List<Expression> _expressions = null!;
    
    protected override bool IsValidToken(Token token, List<Expression> expression)
    {
        return token.Type == TokenType.BracketOpen;
    }

    public override void Compile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        _expressions = [];
        startPosition++;
        
        while (tokens[startPosition].Type != TokenType.BracketClose)
        {
            var token = tokens[startPosition];
            var expression = Expression.FindExpression(token, _expressions);
            var oldIndex = startPosition;
            
            expression?.Compile(tokens, ref startPosition, _expressions);
            if (expression != null) 
                _expressions.Add(expression);

            if (startPosition == oldIndex)
                startPosition++;
        }
        
        startPosition++;
    }

    public override double Compute()
    {
        var compiler = new CalculatorCompiler();
        return compiler.Compute(_expressions);
    }
}