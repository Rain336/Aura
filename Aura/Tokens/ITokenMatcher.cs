﻿namespace Aura.Tokens
{
    public interface ITokenMatcher
    {
        bool Match(string input);
        Token CreateToken(string input);
    }
}