namespace Aura.Tokens
{
    public enum TokenType
    {
        Unknowen,
        
        // Operator
        Plus,
        Minus,
        Times,
        Divide,
        Modulo,
        
        // Punctuation
        OpenParentheses,
        OpenBrace,
        CloseParentheses,
        CloseBrace,
        Colon,
        Equals,
        Semicolon,
        
        // Keyword
        Var,
        Val,
        If,
        Else,
        While,
        For,
        Foreach,
        In,
        
        // Literals
        Decimal,
        Hexadecimal,
        String,
        
        Identifier
    }
}