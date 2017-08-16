using System;
using System.Collections.Generic;
using System.Linq;
using Aura.Tokens;

namespace Aura
{
    public sealed class StringLexer : LexerBase
    {
        public readonly string Content;

        public StringLexer(string content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            Content = content;
        }

        public override List<Token> Lex()
        {
            ITokenMatcher m = null;
            var result = new List<Token>();
            var buffer = "";

            for (var i = 0; i < Content.Length; i++)
            {
                Column++;
                if (IsIgnored(Content[i])) continue;
                buffer += Content[i];

                foreach (var matcher in Matchers)
                {
                    if (matcher.Match(buffer))
                    {
                        if (m == null)
                            m = matcher;
                        else
                        {
                            m = null;
                            break;
                        }
                    }
                }

                if (m == null) continue;

                do
                {
                    i++;
                    while (IsIgnored(Content[i])) i++;
                    buffer += Content[i];
                } while (i < Content.Length && m.Match(buffer));

                var token = m.CreateToken(buffer);
                result.Add(token);
                buffer = buffer.Substring(token.Data.Length);
            }

            return result;
        }
    }
}