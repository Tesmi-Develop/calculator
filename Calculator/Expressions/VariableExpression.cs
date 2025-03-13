using System.Globalization;

namespace Calculator.Expressions;


[Expression]
public class VariableExpression : CalculableExpression
{
    public Token Token { get; private set; }
    
    protected override bool IsValidToken(Token token, List<Expression> expression, List<Token> tokens, int index)
    {
        if (token.Type != TokenType.Identifier) return false;
        return tokens.Count == index + 1 || (tokens[index + 1].Type != TokenType.BracketOpen && tokens[index + 1].Type != TokenType.BracketClose);
    }

    protected override void OnCompile(List<Token> tokens, ref int index, List<Expression> expressions)
    {
        Token = tokens[index];
    }

    protected override double OnCompute(Dictionary<string, double> variables)
    {
        if (!variables.TryGetValue(Token.Value, out var result))
            throw new Exception($"Variable {Token.Value} is not defined");
        return result;
    }
}