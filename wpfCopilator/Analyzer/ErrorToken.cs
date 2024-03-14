using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfCopilator.Analyzer
{
    public class ErrorToken
    { 
        public string Message {  get; set; }
        public string Line {  get; set; }
        public string Column {  get; set; }
        public string FilePath {  get; set; }

        public ErrorToken()
        {
            
        }
        public ErrorToken(string message, string line, string column, string filePath)
        {
            this.Message = message;
            this.Line = line;
            this.Column = column;
            this.FilePath = filePath;
        }

    }
}
