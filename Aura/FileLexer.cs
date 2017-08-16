using System;
using System.Collections.Generic;
using System.IO;
using Aura.Tokens;

namespace Aura
{
    public sealed class FileLexer : LexerBase
    {
        public readonly string Filename;

        public FileLexer(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));

            Filename = filename;
        }

        public override List<Token> Lex()
        {
            var result = new List<Token>();
            using (var reader = File.OpenText(Filename))
            {
                var buffer = "";
                ITokenMatcher m = null;

                while (!reader.EndOfStream)
                {
                    var c = (char) reader.Read();
                    if (c == '\n')
                    {
                        Line++;
                        Column = 0;
                        continue;
                    }
                    Column++;

                    if (IsIgnored(c)) continue;

                    buffer += c;

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

                    while (!reader.EndOfStream && m.Match(buffer))
                    {
                        var n = (char) reader.Read();
                        if (n == '\n' || IsIgnored(n)) continue;
                        buffer += n;
                    }

                    var token = m.CreateToken(buffer);
                    result.Add(token);
                    buffer = buffer.Substring(token.Data.Length);
                }
            }

            return result;
        }
    }
}