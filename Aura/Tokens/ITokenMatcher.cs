namespace Aura.Tokens
{
    public interface ITokenMatcher
    {
        bool Match(string input);
        bool CreateToken(string buffer, Lexer lexer, out Token token);
    }
}