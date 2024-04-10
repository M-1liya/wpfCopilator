using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfCopilator.Analyzer
{
    public class TokenPoliz 
    {
        private readonly Token token;
        private readonly uint _priority;

        public uint Priority { get => _priority; }
        public Token Token { get => token; }

        public TokenPoliz(Token token, uint priority)
        {
            this.token = token;
            _priority = priority;
        }

    }
}
