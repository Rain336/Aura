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
        GreaterThan,
        LessThan,

        // Punctuation
        OpenParentheses,
        OpenBrace,
        CloseParentheses,
        CloseBrace,
        Colon,
        Equals,
        Semicolon,
        Comma,

        // Keyword
        Var,
        Val,
        If,
        Else,
        While,
        For,
        Foreach,
        In,
        Import,
        Function,
        Class,
        Actor,
        Interface,
        Namespace,

        // Modifier
        Public,
        Protected,
        Private,
        Internal,
        Extern,
        Unsafe,
        Static,

        // Literals
        Decimal,
        Hexadecimal,
        String,

        Identifier
    }
}