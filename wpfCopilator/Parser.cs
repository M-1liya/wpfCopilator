using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.Design.AxImporter;

namespace wpfCopilator
{
    static class Parser
    {
        static public List<string> errorStrings = new List<string>();
        static public List<UnderlineColorizer> colorizers = new List<UnderlineColorizer>();
        static public ObservableCollection<MyData> data = new ObservableCollection<MyData>();
        static public string rightLexeme = string.Empty;
        static private List<(int, int)> placeErrors = new List<(int, int)>();
        static private int lastIndexPrew = 0;
        
        static public void StartParser(string input, string fileName, TextDocument document)
        {
            {
                rightLexeme = string.Empty;
                rightLexeme += "Правильная строка:";
                colorizers.Clear();
                lastIndexPrew = 0;
                errorStrings.Clear();
                placeErrors.Clear();
                data.Clear();
            }
            RegexOptions options = RegexOptions.Singleline | RegexOptions.ExplicitCapture;
            int n = 0;
            int iteration = 0;
            foreach (Match match in Regex.Matches(input, Tokens.parserPattern, options))
            {
                iteration++;
                Console.WriteLine(match.Value);
                string errorString = string.Empty;
                    foreach (Group group in match.Groups)
                    {
                        if (group == match.Groups[0])
                            continue;
                        if (group.Index != 0)
                        {
                           errorString = input.Substring(n, group.Index - n);
                           placeErrors.Add((n, group.Index - n));
                           if (n + 1 != group.Index)
                               colorizers.Add(new UnderlineColorizer { StartOffset = n, EndOffset = group.Index });
                           TextLocation location = document.GetLocation(n+1);
                           AddErrors(errorString, group.Name, fileName, location);
                        }
                        n = group.Index + group.Length;
                    rightLexeme += " " + group.Value;
                    }
            }
            if(iteration == 0)
            {
                int numberEx = 0;
                int countFor = 0;
                int endGroupMatch = 0;
                foreach(string input1 in Tokens.patternDissmis)
                {
                    numberEx++;
                    foreach (Match match in Regex.Matches(input, input1, options))
                    {
                        countFor++;
                        Console.WriteLine(match.Value);
                        string errorString = string.Empty;
                        foreach (Group group in match.Groups)
                        {
                            if (group == match.Groups[0])
                                continue;
                            if (group.Index != 0)
                            {
                                //endGroupMatch = group.Index + group.Length;
                                errorString = input.Substring(n, group.Index - n);
                                placeErrors.Add((n, group.Index - n));
                                if (n + 1 != group.Index)
                                    colorizers.Add(new UnderlineColorizer { StartOffset = n, EndOffset = group.Index });
                                TextLocation location = document.GetLocation(n + 1);
                                AddErrors(errorString, group.Name, fileName, location);
                            }
                            n = group.Index + group.Length;
                            rightLexeme += " " + group.Value;
                        }
                    }
                    if (countFor == 0)
                        break;
                    else
                    {
                        countFor = 0;
                        iteration++;
                    }
                        
                }
                string waitItem = "";
                string oldInput = input;
                if (numberEx != iteration)
                {
                    switch (numberEx)
                    {
                        case 1:
                            waitItem = " final ";
                            input = input.Insert(n, " final ");
                            document.Text = input;
                            break;
                        case 2:
                            waitItem = " String ";
                            input = input.Insert(n, " String ");
                            document.Text = input;
                            break;
                        case 3:
                            waitItem = " ID ";
                            input = input.Insert(n, " ID ");
                            document.Text = input;
                            break;
                        case 4:
                            waitItem = " AssignmentOperator ";
                            input = input.Insert(n, " = ");
                            document.Text = input;
                            break;
                        case 5:
                            waitItem = " Row ";
                            input = input.Insert(n, " \"Text\" ");
                            document.Text = input;
                            break;
                        case 6:
                            waitItem = " EndofOperator ";
                            input = input.Insert(n, " ; ");
                            document.Text = input;
                            break;
                    }
                    StartParser(input, fileName, document);
                    if(waitItem.Length != 0)
                    {
                        
                        string error = string.Format("Ошибка компиляции: Ожидалось") + waitItem;
                        errorStrings.Insert(0, error);
                        data.Insert(0, new MyData { FileName = fileName, Line = "1", Column = "1", Message = error });
                        //data.Add(new MyData { FileName = fileName, Line = "1", Column = "1", Message = error });
                        error = string.Format("Ошибочная строка на входе: ") + oldInput;
                        errorStrings.Insert(0, error);
                    }
                }
            }
        }
        static private void AddErrors(string error, string name, string fileName, TextLocation location)
        {
            string newError = Regex.Replace(error, @"\t|\n|\r", "");
            string newName = Regex.Replace(name, @"\t|\n|\r", "");
            if (!string.IsNullOrWhiteSpace(error))
            {
                errorStrings.Add(string.Format("Удалено: {0}, Ожидалось: {1} Столбец: {2} Строка: {3}", newError, newName, location.Column.ToString(), location.Line.ToString()));
                data.Add(new MyData { FileName = fileName, Line = location.Line.ToString(), Column = location.Column.ToString(), Message = string.Format("Удалено: {0}, Ожидалось: {1} Столбец: {2} Строка: {3}", newError, newName, location.Column.ToString(), location.Line.ToString()) });
            }
        }
        static public void DeleteErrors(TextDocument document)
        {
            for (int i = 0; i < placeErrors.Count; i++)
            {
                TextLocation location = document.GetLocation(lastIndexPrew);
                string empty = "";
                for(int j = 0; j < placeErrors[i].Item2; j++)
                {
                    empty += " ";
                }
                document.Replace(placeErrors[i].Item1, placeErrors[i].Item2, empty);
            }
            document.Text = Regex.Replace(document.Text, @"\s+", " ");
        }
    }
    public class MyData
    {
        public string FileName { get; set; }
        public string Line { get; set; }
        public string Column { get; set; }
        public string Message { get; set; }
    }

}
