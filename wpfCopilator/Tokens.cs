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
            @"\bfinal\b",
            @"\bString\b",
            @"\b(?!String|final)[a-zA-Z_][a-zA-Z0-9_]*\b",
            @"=",
            @"""(\s*\w*)*""",
            @";"

        };
        static public List<string> patternDissmis = new List<string>
        {

            @".*?(?<final>\bfinal\b).*",
            @".*?(?<final>\bfinal\b).*?(?<String>\bString\b).*",
            @".*?(?<final>\bfinal\b).*?(?<String>\bString\b).*?(?<ID>\b(?!String|final)[a-zA-Z_][a-zA-Z0-9_]*\b).*",
            @".*?(?<final>\bfinal\b).*?(?<String>\bString\b).*?(?<ID>\b(?!String|final)[a-zA-Z_][a-zA-Z0-9_]*\b).*?(?<AssignmentOperator>=).*",
            @".*?(?<final>\bfinal\b).*?(?<String>\bString\b).*?(?<ID>\b(?!String|final)[a-zA-Z_][a-zA-Z0-9_]*\b).*?(?<AssignmentOperator>=).*?(?<Row>""(\s*\w*\s*)*"").*",
            @".*?(?<final>\bfinal\b).*?(?<String>\bString\b).*?(?<ID>\b(?!String|final)[a-zA-Z_][a-zA-Z0-9_]*\b).*?(?<AssignmentOperator>=).*?(?<Row>""(\s*\w*\s*)*"").*?(?<EndofOperator>;)",


        };
        static public string LexicalAnalyzerPattern = @"(?:\bfinal\b|\bString\b|(\b(?!String|final)[a-zA-Z_][a-zA-Z0-9_]*\b)|=|""(\s*\w*)*""|;|\s+)";
        static public string parserPattern = @".*?(?<final>\bfinal\b).*?(?<String>\bString\b).*?(?<ID>\b(?!String|final)[a-zA-Z_][a-zA-Z0-9_]*\b).*?(?<AssignmentOperator>=).*?(?<Row>""(\s*\w*\s*)*"").*?(?<EndofOperator>;)";
    }
}