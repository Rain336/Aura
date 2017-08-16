using System;
using System.Collections.Generic;
using Aura.Tokens;

namespace Aura
{
    public sealed class TokenStack
    {
        private readonly Queue<int> _cursorQueue = new Queue<int>();
        private readonly List<Token> _tokens;
        public int Cursor { get; set; }

        public TokenStack(IEnumerable<Token> input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            _tokens = new List<Token>(input);
        }

        public TokenStack(List<Token> input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            _tokens = input;
        }

        public TokenStack(LexerBase lexer) : this(lexer.Lex())
        {
        }

        public void PushCursor()
        {
            _cursorQueue.Enqueue(Cursor);
        }

        public void ForgetCursor()
        {
            _cursorQueue.Dequeue();
        }

        public void PopCursor()
        {
            Cursor = _cursorQueue.Dequeue();
        }

        public void ApplyCursor()
        {
            Cursor = _cursorQueue.Peek();
        }

        public Token Next()
        {
            var result = _tokens[Cursor];
            Cursor++;
            return result;
        }

        public Token Peek()
        {
            return _tokens[Cursor];
        }
    }
}