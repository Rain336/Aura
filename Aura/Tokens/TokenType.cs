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
        
        // Literals
        Decimal,
        Hexadecimal,
        String,
    }
}