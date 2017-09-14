using System;
using System.Collections.Generic;
using Aura.Ast;
using Aura.Ast.Declarations;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public InterfaceDeclaration ParseInterfaceDeclaration(List<Modifier> modifiers)
        {
            var iface = new InterfaceDeclaration
            {
                Name = ReadType(TokenType.Identifier).Data
            };

            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                iface.BaseType = ParseType();
            }
            else iface.BaseType = SimpleType.Any;
            ReadType(TokenType.OpenBrace);

            var access = Modifier.Private;
            foreach (var modifier in modifiers)
            {
                if (!modifier.IsAccessModifier()) continue;

                access = modifier;
                modifiers.Remove(modifier);
            }

            iface.AccessModifier = access;
            iface.Modifiers.AddRange(modifiers);

            while (Stack.Peek().Type != TokenType.CloseBrace)
            {
                iface.Entries.Add(ParseInterfaceEntry());
            }

            return iface;
        }

        public InterfaceDeclaration.Entry ParseInterfaceEntry()
        {
            var entry = new InterfaceDeclaration.Entry
            {
                Name = ReadType(TokenType.Identifier).Data
            };
            ReadType(TokenType.OpenParentheses);

            while (Stack.Peek().Type != TokenType.CloseParentheses)
            {
                entry.Parameters.Add(ParseParameterDefinition());
                if (Stack.Peek().Type == TokenType.Comma)
                    Stack.Cursor++;
            }
            Stack.Cursor++;

            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                entry.ReturnType = ParseType();
            }
            else entry.ReturnType = SimpleType.Void;
            return entry;
        }

        public FunctionDeclaration ParseFunctionDeclaration(List<Modifier> modifiers)
        {
            var func = new FunctionDeclaration();

            ReadType(TokenType.Function);
            func.Name = ReadType(TokenType.Identifier).Data;
            ReadType(TokenType.OpenParentheses);

            while (Stack.Peek().Type != TokenType.CloseParentheses)
            {
                func.Parameters.Add(ParseParameterDefinition());
                if (Stack.Peek().Type == TokenType.Comma)
                    Stack.Cursor++;
            }
            Stack.Cursor++;

            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                func.ReturnType = ParseType();
            }
            else func.ReturnType = SimpleType.Void;

            var access = Modifier.Private;
            foreach (var modifier in modifiers)
            {
                if (!modifier.IsAccessModifier()) continue;

                access = modifier;
                modifiers.Remove(modifier);
            }

            func.AccessModifier = access;
            func.Modifiers.AddRange(modifiers);
            func.Block = ParseBlock();

            return func;
        }

        public ParameterDeclaration ParseParameterDefinition()
        {
            return new ParameterDeclaration
            {
                Name = ReadType(TokenType.Identifier).Data,
                Type = ParseType()
            };
        }

        public ClassDeclaration ParseClassDeclaration(List<Modifier> modifiers)
        {
            var cls = new ClassDeclaration();
            var token = Stack.Next();
            if (token.Type == TokenType.Class)
                cls.IsActor = false;
            else if (token.Type == TokenType.Actor)
                cls.IsActor = true;
            else
                throw new ParserException("class or actor", token);

            cls.Name = ReadType(TokenType.Identifier).Data;

            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                cls.BaseType = ParseType();
            }
            else cls.BaseType = SimpleType.Any;
            ReadType(TokenType.OpenBrace);

            var access = Modifier.Private;
            foreach (var modifier in modifiers)
            {
                if (!modifier.IsAccessModifier()) continue;

                access = modifier;
                modifiers.Remove(modifier);
            }

            cls.AccessModifier = access;
            cls.Modifiers.AddRange(modifiers);

            token = Stack.Peek();
            var fmod = new List<Modifier>();
            while (token.Type != TokenType.CloseBrace)
            {
                if (token.IsModifier())
                {
                    fmod.Add(token.ToModifier());
                    Stack.Cursor++;
                }
                else if (token.Type == TokenType.Function)
                {
                    cls.Functions.Add(ParseFunctionDeclaration(fmod));
                    fmod.Clear();
                }
                else if (token.Type == TokenType.Var || token.Type == TokenType.Val)
                {
                    cls.Variables.Add(ParseVariableDeclaration());
                }
                else
                    throw new ParserException("function or variable", token, "unexpected token.");
                token = Stack.Peek();
            }
            Stack.Cursor++;

            return cls;
        }

        public VariableDeclaration ParseVariableDeclaration()
        {
            var variable = new VariableDeclaration();

            var token = Stack.Next();
            if (token.IsModifier())
            {
                variable.AccessModifier = token.ToModifier();
                Stack.Cursor++;
                token = Stack.Next();
            }
            else variable.AccessModifier = Modifier.Private;

            if (token.Type != TokenType.Var && token.Type != TokenType.Val)
                throw new ParserException("Var or Val", token);

            variable.IsImutable = token.Type == TokenType.Val;
            variable.Name = ReadType(TokenType.Identifier).Data;

            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                variable.Type = ParseType();
            }

            if (Stack.Peek().Type == TokenType.Equals)
            {
                Stack.Cursor++;
                variable.Assignment = ParseExpression();
            }

            if (variable.Type == null && variable.Assignment != null)
                variable.Type = variable.Assignment.ResultType;
            if (variable.Assignment == null && variable.Type == null)
                throw new ParserException("Cannot deduce Type!", token);
            if (variable.IsImutable && variable.Assignment == null)
                throw new ParserException("Immutable variable must be asigned.", token);

            return variable;
        }
    }
}