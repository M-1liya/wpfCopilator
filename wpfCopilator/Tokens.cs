using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wpfCopilator
{
    static public class Tokens
    {
        static public List<string> pattern = new List<string>
        {

            @"\s+",
            @"\bfinally\b",
            @"\bString\b",
            @"\b(?!String|finally|)[a-zA-Z_][a-zA-Z0-9_]*\b",
            @"=",
            @"""(\s*\w*)*""",
            @";"

        };
        static public string bigPattern = @"(?:\bfinally\b|\bString\b|(\b(?!String|finally)[a-zA-Z_][a-zA-Z0-9_]*\b)|=|""(\s*\w*)*""|;|\s+)";
        static List<string> tokens = new List<string>();
    }
}
// 0 234