using System.Collections.Frozen;

namespace Calculator;

public enum TokenType
{
    Number,
    BracketOpen,
    BracketClose,
    BinaryOperation,
}

public static class Tokens
{
    public static readonly Dictionary<TokenType, string> TokenRegexs = new()
    {
        { TokenType.Number, @"-?\d+(\.\d+)?"},
        { TokenType.BinaryOperation, @"\*\*|[+\-*/%]"},
        { TokenType.BracketOpen, @"\("},
        { TokenType.BracketClose, @"\)"},
    };
}