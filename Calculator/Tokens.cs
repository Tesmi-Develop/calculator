using System.Collections.Frozen;

namespace Calculator;

public enum TokenType
{
    Number,
    BracketOpen,
    BracketClose,
    Operation,
}

public static class Tokens
{
    public static Dictionary<TokenType, string> TokenRegexs = new()
    {
        { TokenType.Number, @"-?\d+(\.\d+)?"},
        { TokenType.Operation, @"[+\-*/%^]"},
        { TokenType.BracketOpen, @"\("},
        { TokenType.BracketClose, @"\)"},
    };
}