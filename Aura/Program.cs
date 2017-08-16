using System;
using System.IO;

namespace Aura
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length == 0) return;

            foreach (var arg in args)
            {
                Console.WriteLine("Input: " + arg);
                var stack = new TokenStack(new Lexer(new StringReader(arg)));
                Console.WriteLine("Stack: " + stack);
            }
        }
    }
}