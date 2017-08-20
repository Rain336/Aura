using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using Aura.Tokens;
using Aura.Utils;

namespace Aura
{
    public sealed class Lexer
    {
        private static readonly HashSet<CharTokenMatcher> Chars = new HashSet<CharTokenMatcher>();

        private static readonly HashSet<StringTokenMatcher> Matchers = new HashSet<StringTokenMatcher>();

        private static readonly HashSet<RegexTokenMatcher> Regexs = new HashSet<RegexTokenMatcher>();

        public static void AddToken(TokenType type, string matcher, bool regex = false)
        {
            if (string.IsNullOrWhiteSpace(matcher))
                throw new ArgumentNullException(nameof(matcher));

            if (regex)
                Regexs.Add(new RegexTokenMatcher(type, matcher));
            else if (matcher.Length == 1)
                Chars.Add(new CharTokenMatcher(type, matcher[0]));
            else
                Matchers.Add(new StringTokenMatcher(type, matcher));
        }

        static Lexer()
        {
            AddToken(TokenType.Plus, "+");
            AddToken(TokenType.Minus, "-");
            AddToken(TokenType.Times, "*");
            AddToken(TokenType.Divide, "/");
            AddToken(TokenType.Modulo, "%");

            AddToken(TokenType.OpenParentheses, "(");
            AddToken(TokenType.OpenBrace, "{");
            AddToken(TokenType.CloseParentheses, ")");
            AddToken(TokenType.CloseBrace, "}");
            AddToken(TokenType.Colon, ":");
            AddToken(TokenType.Equals, "=");
            AddToken(TokenType.Semicolon, ";");

            AddToken(TokenType.Var, "var");
            AddToken(TokenType.Val, "val");
            AddToken(TokenType.If, "if");
            AddToken(TokenType.Else, "else");
            AddToken(TokenType.In, "in");
            AddToken(TokenType.For, "for");
            AddToken(TokenType.While, "while");

            AddToken(TokenType.Decimal, "[0-9]+", true);
            AddToken(TokenType.Hexadecimal, "0x[0-9A-Fa-f]+", true);
            AddToken(TokenType.String, "\".*?\"", true);
        }

        public int Line;
        public int Column;
        public readonly TextReader Reader;
        private string _identifier = "";
        private bool _spaced;

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
                _spaced = true;
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

        private bool CreateCharToken(ICollection<Token> result, char c)
        {
            var matcher = Chars.FirstOrDefault(m => m.Char == c);
            if (matcher == null) return false;
            CommitIdentifier(result);
            result.Add(new Token(matcher.Type, matcher.Char.ToString())
            {
                Column = Column - 1,
                Line = Line
            });
            return true;
        }

        private bool CreateStringToken(ICollection<Token> result, string input)
        {
            foreach (var matcher in Matchers)
            {
                if (!matcher.Match(input)) continue;

                if (!matcher.CreateToken(ref input, this, out var token)) continue;

                token.Column = Column - token.Data.Length;
                token.Line = Line;
                CommitIdentifier(result);
                result.Add(token);
                return true;
            }
            return false;
        }

        private bool CreateRegexToken(ICollection<Token> result, string buffer)
        {
            var matcher = Regexs.FirstOrDefault(m => m.Match(buffer));
            if (matcher == null) return false;
            if (!matcher.CreateToken(buffer, this, out var token)) return false;
            CommitIdentifier(result);
            result.Add(token);
            return true;
        }

        private void CommitIdentifier(ICollection<Token> result)
        {
            if (string.IsNullOrEmpty(_identifier)) return;
            result.Add(new Token(TokenType.Identifier, _identifier)
            {
                Column = Column - _identifier.Length,
                Line = Line
            });
            _identifier = "";
        }

        public List<Token> Lex()
        {
            var result = new List<Token>();
            var buffer = "";

            while (true)
            {
                var c = Read();
                if (c == -1)
                    break;
                buffer += (char) c;

                if (_spaced && buffer.Length != 1)
                {
                    result.Add(new Token(TokenType.Identifier, buffer.Remove(buffer.Length - 1))
                    {
                        Column = Column - buffer.Length + 1,
                        Line = Line
                    });
                    buffer = buffer.Substring(buffer.Length - 1);
                }
                _spaced = false;

                if (CreateRegexToken(result, buffer))
                {
                    buffer = "";
                    continue;
                }

                if (buffer.Length == 1 && CreateCharToken(result, buffer[0]))
                {
                    buffer = "";
                    continue;
                }

                if (CreateStringToken(result, buffer))
                {
                    buffer = "";
                    continue;
                }

                _identifier += buffer;
                buffer = "";
            }

            if (_identifier.Length != 0)
                CommitIdentifier(result);

            if (buffer.Length != 0)
                throw new LexerException("Unknowen Token: '" + buffer + '\'');

            return result;
        }
    }
}