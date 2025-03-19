using System.Text.RegularExpressions;

namespace Calculator;

public static class Lexer
{
    private static readonly Regex Regex;

    static Lexer()
    {
        var finalPattern = string.Empty;

        foreach (var (_, pattern) in Tokens.TokenRegexs)
        {
            finalPattern += $"{pattern}|";
        }
        
        Regex = new Regex(finalPattern[..^1]);
    }
    
    public static List<Token> Parse(string input)
    {
        var tokens = new List<Token>();

        foreach (Match match in Regex.Matches(input))
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