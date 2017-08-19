using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using Aura.Tokens;
using Aura.Utils;

namespace Aura
{
    public sealed class Lexer
    {
        public static readonly HashSet<ITokenMatcher> Matchers = new HashSet<ITokenMatcher>
        {
            new CharTokenMatcher(TokenType.Plus, '+'),
            new CharTokenMatcher(TokenType.Minus, '-'),
            new CharTokenMatcher(TokenType.Times, '*'),
            new CharTokenMatcher(TokenType.Divide, '/'),
            new CharTokenMatcher(TokenType.Modulo, '%'),

            new CharTokenMatcher(TokenType.OpenParentheses, '('),
            new CharTokenMatcher(TokenType.CloseParentheses, ')'),
            new CharTokenMatcher(TokenType.Colon, ':'),
            new CharTokenMatcher(TokenType.Equals, '='),
            
            new StringTokenMatcher(TokenType.Var, "var"),
            new StringTokenMatcher(TokenType.Val, "val"),

            new RegexTokenMatcher(TokenType.Decimal, "[0-9]+"),
            new RegexTokenMatcher(TokenType.Hexadecimal, "0x[0-9A-Fa-f]+"),
            new RegexTokenMatcher(TokenType.String, "\".*?\"")
        };

        public int Line;
        public int Column;
        public readonly TextReader Reader;

        public Lexer(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            Reader = reader;
        }

        public int Read()
        {
            var s = Reader.Read();
            Column++;
            while (s != -1 && IsIgnored((char) s))
            {
                s = Reader.Read();
                Column++;
            }
            return s;
        }

        public int Peek()
        {
            var s = Reader.Peek();
            while (s != -1 && IsIgnored((char) s))
            {
                Reader.Read();
                s = Reader.Peek();
            }
            return s;
        }

        public string ReadWhile(Func<char, bool> predicate, string prefix = "")
        {
            var c = Reader.Peek();
            while (c != -1 && predicate((char) c))
            {
                Reader.Read();
                prefix += (char) c;
                c = Reader.Peek();
            }
            return prefix;
        }

        private bool IsIgnored(char c)
        {
            if (c != '\n')
                return c == ' ' || c == '\t' || c == '\r';

            Line++;
            Column = 0;
            return true;
        }

        public List<Token> Lex()
        {
            var result = new List<Token>();
            var buffer = "";

            while (true)
            {
                ITokenMatcher m = null;
                var multiple = false;
                var c = Read();
                if (c == -1)
                    break;
                buffer += (char) c;

                if (c == '/' && Peek() == '/')
                {
                    ReadWhile(p => p != '\n');
                    continue;
                }

                foreach (var matcher in Matchers)
                {
                    if (matcher.Match(buffer))
                    {
                        if (m == null)
                            m = matcher;
                        else
                            multiple = true;
                    }
                }

                if (multiple) continue;
                
                if (m == null)
                {
                    result.Add(new Token(TokenType.Identifier, ReadWhile(char.IsLetterOrDigit, buffer)));
                    buffer = "";
                    continue;
                }

                c = Peek();
                if (c == -1)
                {
                    result.Add(m.CreateToken(buffer));
                    buffer = "";
                    break;
                }
                buffer += (char) c;
                while (m.Match(buffer))
                {
                    Read();
                    c = Peek();
                    if (c == -1)
                    {
                        result.Add(m.CreateToken(buffer));
                        buffer = "";
                        break;
                    }
                    buffer += (char) c;
                }

                result.Add(m.CreateToken(buffer));
                buffer = "";
            }

            if (buffer.Length != 0)
                throw new LexerException("Unknowen Token: '" + buffer + '\'');

            return result;
        }
    }
}