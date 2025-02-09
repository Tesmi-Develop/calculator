using System.Text.RegularExpressions;

namespace Calculator;

public class Lexer
{
    private Regex _regex;

    public Lexer()
    {
        var finalPattern = string.Empty;

        foreach (var (tokenType, pattern) in Tokens.TokenRegexs)
        {
            finalPattern += $"{pattern}|";
        }
        
        _regex = new Regex(finalPattern[..^1]);
    }
    
    public List<Token> Parse(string input)
    {
        var tokens = new List<Token>();

        foreach (Match match in _regex.Matches(input))
        {
            TokenType? foundTokenType = null;
            
            foreach (var (tokenType, tokenPattern) in Tokens.TokenRegexs)
            {
                if (!Regex.IsMatch(match.Value, $"^{tokenPattern}$")) 
                    continue;
                
                foundTokenType = tokenType;
                break;
            }
            
            if (foundTokenType is null)
                throw new Exception("Invalid token type");
            
            tokens.Add(new Token((TokenType)foundTokenType, match.Value));
        }

        return tokens;
    }
}