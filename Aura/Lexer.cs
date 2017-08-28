using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using Aura.Tokens;
using Aura.Utils;

namespace Aura
{
    public sealed class Lexer
    {
        private static readonly HashSet<TokenMatcher<char>> Chars = new HashSet<TokenMatcher<char>>();

        private static readonly HashSet<TokenMatcher<string>> Strings = new HashSet<TokenMatcher<string>>();

        private static readonly HashSet<TokenMatcher<Regex>> Regexs = new HashSet<TokenMatcher<Regex>>();

        public static void AddToken(TokenType type, string matcher, bool regex = false)
        {
            if (string.IsNullOrWhiteSpace(matcher))
                throw new ArgumentNullException(nameof(matcher));

            if (regex)
                Regexs.Add(new TokenMatcher<Regex>(type, new Regex(matcher)));
            else if (matcher.Length == 1)
                Chars.Add(new TokenMatcher<char>(type, matcher[0]));
            else
                Strings.Add(new TokenMatcher<string>(type, matcher));
        }

        static Lexer()
        {
            AddToken(TokenType.Plus, "+");
            AddToken(TokenType.Minus, "-");
            AddToken(TokenType.Times, "*");
            AddToken(TokenType.Divide, "/");
            AddToken(TokenType.Modulo, "%");
            AddToken(TokenType.GreaterThan, "<");
            AddToken(TokenType.LessThan, ">");

            AddToken(TokenType.OpenParentheses, "(");
            AddToken(TokenType.OpenBrace, "{");
            AddToken(TokenType.CloseParentheses, ")");
            AddToken(TokenType.CloseBrace, "}");
            AddToken(TokenType.Colon, ":");
            AddToken(TokenType.Equals, "=");
            AddToken(TokenType.Semicolon, ";");
            AddToken(TokenType.Comma, ",");

            AddToken(TokenType.Var, "var");
            AddToken(TokenType.Val, "val");
            AddToken(TokenType.If, "if");
            AddToken(TokenType.Else, "else");
            AddToken(TokenType.In, "in");
            AddToken(TokenType.For, "for");
            AddToken(TokenType.While, "while");
            AddToken(TokenType.Import, "import");
            AddToken(TokenType.Function, "function");
            AddToken(TokenType.Class, "class");
            AddToken(TokenType.Actor, "actor");
            AddToken(TokenType.Interface, "interface");
            
            AddToken(TokenType.Public, "public");
            AddToken(TokenType.Protected, "protected");
            AddToken(TokenType.Private, "private");
            AddToken(TokenType.Internal, "internal");
            AddToken(TokenType.Extern, "extern");
            AddToken(TokenType.Unsafe, "unsafe");
            AddToken(TokenType.Static, "static");

            AddToken(TokenType.Decimal, "[0-9]+", true);
            AddToken(TokenType.Hexadecimal, "0x[0-9A-Fa-f]+", true);
            AddToken(TokenType.String, "\".*?\"", true);
        }

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
            if (s != -1 && s == '\n')
            {
                Line++;
                Column = 0;
            }
            else Column++;
            return s;
        }

        private bool CreateCharToken(ICollection<Token> result, char c)
        {
            var matcher = Chars.FirstOrDefault(m => m.Value == c);
            if (matcher == null) return false;
            result.Add(new Token(matcher.Type, matcher.Value.ToString())
            {
                Column = Column - 1,
                Line = Line
            });
            return true;
        }

        private string ReadWord(char c)
        {
            var buffer = c.ToString();
            var i = Reader.Peek();
            while (i != -1 && char.IsLetterOrDigit((char) i))
            {
                Read();
                buffer += (char) i;
                i = Reader.Peek();
            }
            return buffer.Trim();
        }

        public List<Token> Lex()
        {
            var result = new List<Token>();

            while (true)
            {
                var c = Read();
                if (c == -1)
                    break;

                if (CreateCharToken(result, (char) c)) continue;

                var buffer = ReadWord((char) c);
                if (buffer.Length == 0) continue;
                var found = false;

                foreach (var matcher in Regexs)
                {
                    if (matcher.Value.Match(buffer).Length != buffer.Length) continue;

                    found = true;
                    result.Add(new Token(matcher.Type, buffer)
                    {
                        Line = Line,
                        Column = Column - buffer.Length
                    });
                    break;
                }

                if (found) continue;

                foreach (var matcher in Strings)
                {
                    if (matcher.Value != buffer) continue;

                    found = true;
                    result.Add(new Token(matcher.Type, matcher.Value)
                    {
                        Line = Line,
                        Column = Column - matcher.Value.Length
                    });
                    break;
                }

                if (found) continue;

                result.Add(new Token(TokenType.Identifier, buffer)
                {
                    Line = Line,
                    Column = Column - buffer.Length
                });
            }

            return result;
        }
    }
}