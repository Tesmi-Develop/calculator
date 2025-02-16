namespace Calculator;

public enum TokenType
{
    Number,
    BracketOpen,
    BracketClose,
    BinaryOperation,
    VerticalBar,
    Identifier,
    Comma
}

public static class Tokens
{
    public static readonly Dictionary<TokenType, string> TokenRegexs = new()
    {
        { TokenType.Number, @"\d+(\.\d+)?"},
        { TokenType.BinaryOperation, @"\*\*|[+\-*/%]"},
        { TokenType.BracketOpen, @"\("},
        { TokenType.BracketClose, @"\)"},
        { TokenType.VerticalBar, @"\|"},
        { TokenType.Identifier, @"\b[a-zA-Z_][a-zA-Z0-9_]*"},
        { TokenType.Comma, @"," }
    };
}