using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace wpfCopilator
{
    static internal class Analyzer
    {
        static List<string> outputInfoLexeme = new List<string>();
        static bool flag = false;
        static public List<string> OutputData(string lexeme)
        {
            outputInfoLexeme.Clear();
            Scan(lexeme);
            return outputInfoLexeme;
        }
        static private void SpaceLexeme(string lexeme)
        {
            List<int> spaceSymbols = new List<int>();
            int start = 0, end = 0;
            for (int i = 0; i < lexeme.Length; i++)
            {
                if(lexeme[i] == ' ')
                {
                    if(end == 0)
                    {
                        start = i + 1;
                    }
                    else
                    {
                        end = i + 1;
                        spaceSymbols.Add(start);
                        spaceSymbols.Add(end);
                        start = 0; end = 0;
                    }
                }
            }
        }
        static private void Scan(string lexeme)
        {
            string token = string.Empty;
            int start = 0, count = 0;
            for (int i = 0; i < lexeme.Length; i++)
            {
                if(lexeme[i] == '"' || lexeme[i] == ';' || lexeme[i] == '=' || lexeme[i] == ' ')
                {
                    if (token != string.Empty && !token.StartsWith("\""))
                    {
                        EnumLexeme((start, i), token);
                        start = 0;
                        token = string.Empty;
                    }
                    if(lexeme[i] == '"')
                    {
                        count++;
                        if (start == 0)
                            start = i + 1;
                        token += lexeme[i];
                        if(count == 2)
                        {
                            EnumLexeme((start, i), token);
                            start = 0;
                            token = string.Empty;
                        }
                    }
                    else
                        EnumLexeme((i + 1, i + 1), Convert.ToString(lexeme[i]));
                }
                else
                {
                    if (start == 0)
                        start = i + 1;
                    token += lexeme[i];
                }
            }
        }
        static private void EnumLexeme((int,int) startEnd, string token)
        {
            
                switch (token)
                {
                case "finally":
                    outputInfoLexeme.Add(String.Format("1 - ключевое слово - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case "String":
                    outputInfoLexeme.Add(String.Format("2 - ключевое слово - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case "=":
                    outputInfoLexeme.Add(String.Format("5 - оператор присваивания - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                /*case "\"":
                    flag = true;
                    outputInfoLexeme.Add(String.Format("двойная кавычка - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;*/
                case ";":
                    outputInfoLexeme.Add(String.Format("7 - конец оператора - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case " ":
                    outputInfoLexeme.Add(String.Format("4 - разделитель - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                default:
                    if(!token.Contains("\""))
                        outputInfoLexeme.Add(String.Format("3 - идентификатор - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    else
                        outputInfoLexeme.Add(String.Format("6 - строка - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
            }            
        }
    }
}
