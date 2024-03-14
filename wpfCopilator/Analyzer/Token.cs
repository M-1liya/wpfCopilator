using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfCopilator.Analyzer
{
    public class Token
    {
        private TokenType _type;
        private string _text;
        private int _posStart;
        private int _posEnd;
        private int _posLine;

        public Token(TokenType Type, string Text, int PosLine, int PosStart, int PosEnd)
        {
            this.Type = Type;
            this.Text = Text;
            this.PosStart = PosStart;
            this.PosEnd = PosEnd;
            this.PosLine = PosLine;
        }

        public override string ToString()
        {
            string _tmpText = Text;
            _tmpText = Text.Replace("\n", "\\n");
            _tmpText = Text.Replace("\r", "\\r");
            _tmpText = Text.Replace("\t", "\\t");
            _tmpText = Text.Replace(" ", "'_'");
            return $"code: {_type.code}\t- {_type.Name}\t - {_tmpText}\t- Строка {PosLine}, с {PosStart} по {PosEnd}";
        }

        public TokenType Type { get => _type; set => _type = value; }
        public string Text { get => _text; set => _text = value; }
        public int PosStart { get => _posStart; set => _posStart = value; }
        public int PosEnd { get => _posEnd; set => _posEnd = value; }
        public int PosLine { get => _posLine; set => _posLine = value; }
    }
}
