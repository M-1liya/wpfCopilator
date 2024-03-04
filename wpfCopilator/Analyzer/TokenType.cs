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
        public readonly string Name;
        public readonly string Regex;

        static public readonly TokenType TokenError = new TokenType(-1, "Error", @"[^a-zA-Z0-9_ {}\t\n\r,;]+");

        private TokenType(int code, string Name, string Regex)
        {
            this.code = code;
            this.Name = Name;
            this.Regex = Regex;
        }

        static private List<TokenType> _tokens = new List<TokenType>()
        {
            new TokenType(11, "Enumeration", @"[A-Z]+\b"),
            new TokenType(12, "KeyWord", @"enum\b"),
            new TokenType(13, "ID", @"[a-zA-Z_][a-zA-Z0-9_]*\b"),
            new TokenType(24, "Space", @"[ \t\n\r]+"),
            new TokenType(35, "Сomma", @","),
            new TokenType(46, "LPar", @"{"),
            new TokenType(57, "RPar", @"}"),
            new TokenType(68, "Semicolon", @";")
        };

        static public ReadOnlyCollection<TokenType> Tokens => _tokens.AsReadOnly<TokenType>();
    }
}
