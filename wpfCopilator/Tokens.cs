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
            @"\b(?!String|finally)[a-zA-Z_][a-zA-Z0-9_]*\b",
            @"=",
            @"""(\s*\w*)*""",
            @";"

        };
        static public string LexicalAnalyzerPattern = @"(?:\bfinally\b|\bString\b|(\b(?!String|finally)[a-zA-Z_][a-zA-Z0-9_]*\b)|=|""(\s*\w*)*""|;|\s+)";
        static public string parserPattern = @".*?(?<finally>\bfinally\b).*?(?<String>\bString\b).*?(?<ID>\b(?!String|finally)[a-zA-Z_][a-zA-Z0-9_]*\b).*?(?<AssignmentOperator>=).*?(?<Row>""(\s*\w*\s*)*"").*?(?<EndofOperator>;)";
    }
}