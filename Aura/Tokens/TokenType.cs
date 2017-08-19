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
        CloseParentheses,
        Colon,
        Equals,
        
        // Keyword
        Var,
        Val,
        
        // Literals
        Decimal,
        Hexadecimal,
        String,
        
        Identifier
    }
}