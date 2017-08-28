using System;
using System.Collections.Generic;
using Aura.Ast;
using Aura.Ast.Definitions;
using Aura.Ast.Expressions;
using Aura.Tokens;
using Aura.Utils;

namespace Aura.Parsers
{
    public sealed partial class Parser
    {
        public InterfaceDefinition ParseInterfaceDefinition(List<Modifier> modifiers)
        {
            var token = Stack.Next();
            while (token.IsModifier())
            {
                modifiers.Add(token.ToModifier());
                token = Stack.Next();
            }

            if (token.Type != TokenType.Interface)
                throw new ParserException(Enum.GetName(typeof(TokenType), TokenType.Interface), token);
            
            var name = ReadType(TokenType.Identifier).Data;
            var @base = TypeElement.Any();
            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                @base = ParseType();
            }
            ReadType(TokenType.OpenBrace);
            
            var access = Modifier.Unknowen;
            foreach (var modifier in modifiers)
            {
                if (!modifier.IsAccessModifier()) continue;

                access = modifier;
                modifiers.Remove(modifier);
            }

            if (access == Modifier.Unknowen)
                access = Modifier.Private;
            
            var result = new InterfaceDefinition(access, name, @base)
                .WithModifiers(modifiers.ToArray());

            while (Stack.Peek().Type != TokenType.CloseBrace)
            {
                result.WithSignatures(ParseFunctionSignature());
            }
        }

        public FunctionSignature ParseFunctionSignature()
        {
            
        }

        public FunctionDefinition ParseFunctionDefinition(List<Modifier> modifiers)
        {
            var token = Stack.Next();
            while (token.IsModifier())
            {
                modifiers.Add(token.ToModifier());
                token = Stack.Next();
            }

            if (token.Type != TokenType.Function)
                throw new ParserException(Enum.GetName(typeof(TokenType), TokenType.Function), token);

            var name = ReadType(TokenType.Identifier).Data;
            ReadType(TokenType.OpenParentheses);

            var param = new List<ParameterDefinition>();
            while (Stack.Peek().Type != TokenType.CloseParentheses)
            {
                param.Add(ParseParameterDefinition());
                if (Stack.Peek().Type == TokenType.Comma)
                    Stack.Cursor++;
            }

            var access = Modifier.Unknowen;
            foreach (var modifier in modifiers)
            {
                if (!modifier.IsAccessModifier()) continue;

                access = modifier;
                modifiers.Remove(modifier);
            }

            if (access == Modifier.Unknowen)
                access = Modifier.Private;

            return (FunctionDefinition) new FunctionDefinition(access, name, ParseBlock())
                .WithModifiers(modifiers.ToArray())
                .WithParameters(param.ToArray());
        }

        public ParameterDefinition ParseParameterDefinition()
        {
            return new ParameterDefinition(ReadType(TokenType.Identifier).Data, ParseType());
        }

        public ClassDefinition ParseClassDefinition(List<Modifier> modifiers)
        {
            var token = Stack.Next();
            while (token.IsModifier())
            {
                modifiers.Add(token.ToModifier());
                token = Stack.Next();
            }

            bool actor;
            if (token.Type == TokenType.Class)
                actor = false;
            else if (token.Type == TokenType.Actor)
                actor = true;
            else
                throw new ParserException("class or actor", token);

            var name = ReadType(TokenType.Identifier).Data;

            var @base = TypeElement.Any();
            if (Stack.Peek().Type == TokenType.Colon)
            {
                Stack.Cursor++;
                @base = ParseType();
            }
            ReadType(TokenType.OpenBrace);

            var access = Modifier.Unknowen;
            foreach (var modifier in modifiers)
            {
                if (!modifier.IsAccessModifier()) continue;

                access = modifier;
                modifiers.Remove(modifier);
            }

            if (access == Modifier.Unknowen)
                access = Modifier.Private;

            token = Stack.Peek();
            var result = new ClassDefinition(access, name, @base, actor)
                .WithModifiers(modifiers.ToArray());
            while (token.Type != TokenType.CloseBrace)
            {
                if (token.IsModifier() || token.Type == TokenType.Function)
                {
                    result.WithFunctions(ParseFunctionDefinition(new List<Modifier>()));
                }
                else if (token.Type == TokenType.Var || token.Type == TokenType.Val)
                {
                    result.WithVariables(ParseVariableDefinition());
                }
                else
                    throw new ParserException("function or variable", token, "unexpected token.");
                token = Stack.Peek();
            }

            return result;
        }
    }
}