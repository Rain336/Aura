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
        
        // Keyword
        Var,
        Val,
        If,
        Else,
        
        // Literals
        Decimal,
        Hexadecimal,
        String,
        
        Identifier
    }
}