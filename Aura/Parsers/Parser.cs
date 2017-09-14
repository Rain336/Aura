using System;
using System.Collections.Generic;
using Aura.Ast;
using Aura.Ast.Expressions;
using Aura.Ast.Declarations;
using Aura.Ast.Statments;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public readonly TokenStack Stack;

        public Parser(TokenStack stack)
        {
            Stack = stack ?? throw new ArgumentNullException(nameof(stack));
        }

        private Token ReadType(TokenType type)
        {
            var token = Stack.Next();
            if (token.Type != type)
                throw new ParserException(Enum.GetName(typeof(TokenType), type), token);
            return token;
        }

        public CompilationUnit ParseCompilationUnit()
        {
            var unit = new CompilationUnit();
            var token = Stack.Peek();

            while (token.Type == TokenType.Import)
            {
                unit.Imports.Add(ParseImportDirective());
                token = Stack.Peek();
            }

            var modifiers = new List<Modifier>();
            while (token.Type != TokenType.Unknowen)
            {
                if (token.IsModifier())
                {
                    modifiers.Add(token.ToModifier());
                    Stack.Cursor++;
                    token = Stack.Peek();
                    continue;
                }

                switch (token.Type)
                {
                    case TokenType.Namespace:
                        unit.Namespaces.Add(ParseNamespaceDeclaration());
                        break;

                    case TokenType.Function:
                        unit.Functions.Add(ParseFunctionDeclaration(modifiers));
                        modifiers.Clear();
                        break;

                    case TokenType.Class:
                    case TokenType.Actor:
                        unit.Classes.Add(ParseClassDeclaration(modifiers));
                        modifiers.Clear();
                        break;

                    case TokenType.Interface:
                        unit.Interfaces.Add(ParseInterfaceDeclaration(modifiers));
                        modifiers.Clear();
                        break;

                    default:
                        throw new ParserException("import, class, namespace, interface or Enum",
                            token, "Unexpected Token.");
                }
                token = Stack.Peek();
            }

            return unit;
        }

        public NamespaceDeclaration ParseNamespaceDeclaration()
        {
            var ns = new NamespaceDeclaration();
            var token = Stack.Peek();

            var modifiers = new List<Modifier>();
            while (token.Type != TokenType.Unknowen)
            {
                if (token.IsModifier())
                {
                    modifiers.Add(token.ToModifier());
                    Stack.Cursor++;
                    token = Stack.Peek();
                    continue;
                }

                switch (token.Type)
                {
                    case TokenType.Function:
                        ns.Functions.Add(ParseFunctionDeclaration(modifiers));
                        modifiers.Clear();
                        break;

                    case TokenType.Class:
                    case TokenType.Actor:
                        ns.Classes.Add(ParseClassDeclaration(modifiers));
                        modifiers.Clear();
                        break;

                    case TokenType.Interface:
                        ns.Interfaces.Add(ParseInterfaceDeclaration(modifiers));
                        modifiers.Clear();
                        break;

                    default:
                        throw new ParserException("import, class, namespace, interface or Enum",
                            token, "Unexpected Token.");
                }
                token = Stack.Peek();
            }

            return ns;
        }

        public ImportDirective ParseImportDirective()
        {
            ReadType(TokenType.Import);
            return new ImportDirective
            {
                Namespace = ReadType(TokenType.Identifier).Data
            };
        }

        public IStatment ParseStatement()
        {
            switch (Stack.Peek().Type)
            {
                case TokenType.Val:
                case TokenType.Var:
                    return ParseVariableStatment();

                case TokenType.For:
                    return ParseFor();

                case TokenType.While:
                    return ParseWhile();

                default:
                    return new ExpressionStatment
                    {
                        Expression = ParseExpression()
                    };
            }
        }

        public IExpression ParseExpression()
        {
            IExpression result;

            switch (Stack.Peek().Type)
            {
                case TokenType.Decimal:
                case TokenType.Hexadecimal:
                    result = ParseNumericLiteral();
                    break;

                case TokenType.String:
                    result = ParseStringLiteral();
                    break;

                case TokenType.Plus:
                case TokenType.Minus:
                    result = ParseUnaryOperator();
                    break;

                case TokenType.OpenParentheses:
                    Stack.Cursor++;
                    result = ParseExpression();
                    ReadType(TokenType.CloseParentheses);
                    break;

                case TokenType.If:
                    result = ParseIf();
                    break;

                case TokenType.OpenBrace:
                    result = ParseBlock();
                    break;

                case TokenType.Identifier:
                    result = ParseVariable();
                    break;

                default:
                    throw new ParserException("An expression", Stack.Peek());
            }
            return Stack.Peek().IsBinaryOperator() ? ParseBinaryOperator(result) : result;
        }
    }
}