using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfCopilator.Analyzer
{
    public class TokenType
    {
        public readonly int code;
        public readonly TokenTypes Name;
        public readonly string Regex;

        static public readonly TokenType TokenError = new TokenType(-1, TokenTypes.Error, @"[^a-zA-Z0-9_ {}\t\n\r,;]+");

        private TokenType(int code, string Name, string Regex)
        {
            this.code = code;
            this.Name = TokenTypes.Error;
            this.Regex = Regex;
        }

        private TokenType(int code, TokenTypes Name, string Regex)
        {
            this.code = code;
            this.Name = Name;
            this.Regex = Regex;
        }

        static private List<TokenType> _tokens = new List<TokenType>()
        {
            new TokenType(11, TokenTypes.Enumeration, @"[A-Z]+\b"),
            new TokenType(12, TokenTypes.KeyWord, @"enum\b"),
            new TokenType(13, TokenTypes.ID, @"[a-zA-Z_][a-zA-Z0-9_]*\b"),
            new TokenType(24, TokenTypes.Space, @"[ \t\n\r]+"),
            new TokenType(35, TokenTypes.Сomma, @","),
            new TokenType(46, TokenTypes.LPar, @"{"),
            new TokenType(57, TokenTypes.RPar, @"}"),
            new TokenType(68, TokenTypes.Semicolon, @";")
        };

        static public ReadOnlyCollection<TokenType> Tokens => _tokens.AsReadOnly<TokenType>();


        public enum TokenTypes
        {
            Enumeration,
            KeyWord,
            ID,
            Space,
            Сomma,
            LPar,
            RPar,
            Semicolon,
            Error
        }
    }
}
