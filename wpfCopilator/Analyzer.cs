using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using static System.Net.Mime.MediaTypeNames;

namespace wpfCopilator
{
    static internal class Analyzer
    {
        static List<string> outputInfoLexeme = new List<string>();
        static List<(int, int)> placeTokens = new List<(int, int)>();
        static List<(int, int)> placeErrors = new List<(int, int)>(); //1й - индекс начала ошибки, 2й - длина ошибки
        static public List<string> OutputData(string lexeme)
        {
            outputInfoLexeme.Clear();
            Scan(lexeme);
            FindErrors(lexeme);
            return outputInfoLexeme;
        }
        static private void Scan(string lexeme)
        {
            string errors = string.Empty;
            Regex regex = new Regex(Tokens.bigPattern);
            MatchCollection matches = regex.Matches(lexeme);
            foreach (Match match in matches)
            {
                EnumLexeme((match.Index, match.Index + match.Length - 1), match.Value);
                placeTokens.Add((match.Index, match.Index + match.Length - 1));
            }
            // StringBuilder stringBuilder = new StringBuilder(lexeme);
            /*for (int i = 0; i < Tokens.pattern.Count; i++)
            {
                Regex regex = new Regex(Tokens.pattern[i]);
                MatchCollection matches = regex.Matches(lexeme);
                foreach (Match match in matches)
                {
                  EnumLexeme((match.Index, match.Index + match.Length), Tokens.pattern[i]);
                    //stringBuilder.Remove(match.Index, (match.Index + match.Length));
                    placeTokens.Add((match.Index, match.Index + match.Length - 1));
                }
            }*/
            //placeTokens.Sort();
            //FindErrors(lexeme);
        } //012  56789
        static private void FindErrors(string lexeme)
        {
            int count = 0;
            for(int i = 0; i < placeTokens.Count; i++)
            {
                if (i + 1 == placeTokens.Count)
                    break;
                if (placeTokens[i].Item1 != 0 && i == 0)
                {
                    placeErrors.Add((0, placeTokens[i].Item1 - 1));
                }
                placeErrors.Add((placeTokens[i].Item2 + 1, placeTokens[i + 1].Item1 - 1));
            }
            foreach ((int, int) error in placeErrors)
            {
                string valueError = string.Empty;
                try 
                {
                    valueError = lexeme.Substring(error.Item1, error.Item2 - error.Item1 + 1);//error.Item2 - error.Item1 + 1
                    if(!string.IsNullOrEmpty(valueError))
                        outputInfoLexeme.Add(String.Format("404 - ошибка - {0} - c {1} по {2} символ", valueError, error.Item1, error.Item2));
                }
                catch { }
                
            }
        }
        static private void EnumLexeme((int, int) startEnd, string token)
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
                case ";":
                    outputInfoLexeme.Add(String.Format("7 - конец оператора - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case " ":
                    outputInfoLexeme.Add(String.Format("4 - разделитель - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                default:
                    if (token.Contains("\""))
                        outputInfoLexeme.Add(String.Format("6 - строка - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    else if(string.IsNullOrWhiteSpace(token))
                        outputInfoLexeme.Add(String.Format("4 - разделитель - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    else
                        outputInfoLexeme.Add(String.Format("3 - идентификатор - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;

            }
        }
        /*static private void EnumLexeme((int,int) startEnd, string token)
        {
                switch (token)
                {
                case @"\bfinally\b":
                    outputInfoLexeme.Add(String.Format("1 - ключевое слово - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case @"\bString\b":
                    outputInfoLexeme.Add(String.Format("2 - ключевое слово - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case @"=":
                    outputInfoLexeme.Add(String.Format("5 - оператор присваивания - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case @";":
                    outputInfoLexeme.Add(String.Format("7 - конец оператора - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case @"""(\s*\w*)*""":
                    outputInfoLexeme.Add(String.Format("6 - строка - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case @"\b(?!String|finally)[a-zA-Z_][a-zA-Z0-9_]*\b":
                    outputInfoLexeme.Add(String.Format("3 - идентификатор - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;
                case @"\s+":
                    outputInfoLexeme.Add(String.Format("4 - разделитель - {0} - c {1} по {2} символ", token, startEnd.Item1, startEnd.Item2));
                    break;

            }            
        }*/

        #region Закомментированно
        /*
        static private void Scan(string lexeme)
        {
            string token = string.Empty;
            int start = 0, count = 0;
            for (int i = 0; i < lexeme.Length; i++)
            {
                if (lexeme[i] == '"' || lexeme[i] == ';' || lexeme[i] == '=' || lexeme[i] == ' ')
                {
                    if (token != string.Empty && !token.StartsWith("\""))
                    {
                        EnumLexeme((start, i), token);
                        start = 0;
                        token = string.Empty;
                    }
                    if (lexeme[i] == '"')
                    {
                        count++;
                        if (start == 0)
                            start = i + 1;
                        token += lexeme[i];
                        if (count == 2)
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
        }*/
        #endregion

    }
}
