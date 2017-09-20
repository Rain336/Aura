using System.IO;
using Aura.Parsers;

namespace Aura.Test
{
    public static class TestUtils
    {
        public static Parser CreateParser(string input)
        {
            using (var reader = new StringReader(input))
            {
                return new Parser(new TokenStack(new Lexer(reader)));
            }
        }
    }
}
