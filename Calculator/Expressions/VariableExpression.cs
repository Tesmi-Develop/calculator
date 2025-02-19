using System.Globalization;

namespace Calculator.Expressions;

[Expression]
public class VariableExpression : CalculableExpression
{
    private Token _token;
    
    protected override bool IsValidToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        if (token.Type != TokenType.Identifier) return false;
        return tokens.Count == index + 1 || (tokens[index + 1].Type != TokenType.BracketOpen && tokens[index + 1].Type != TokenType.BracketClose);
    }

    protected override void OnCompile(List<Token> tokens, ref int startPosition, List<Expression> expressions)
    {
        _token = tokens[startPosition];
    }

    protected override double OnCompute(Dictionary<string, double> variables)
    {
        if (!variables.TryGetValue(_token.Value, out double result))
            throw new Exception($"Variable {_token.Value} is not defined");
        return result;
    }
}