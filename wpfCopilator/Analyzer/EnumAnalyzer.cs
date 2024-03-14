using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wpfCopilator.Analyzer
{
/*
enum Months
{
    JAN, FEB, MAR,
    APR,
    MAY,
    JUN;
}
*/

    public static class EnumAnalyzer
    {
        public static string Analyze(string text)
        {
            List<string> lines = new List<string>();
            StringBuilder sb = new StringBuilder();
            StringBuilder resultBuilder = new StringBuilder();

            text.Replace('\r', '\n');
            text += '\n';
            //Разделение текста на строчки
            foreach (char c in text)
            {
                if(c == '\n')
                {
                    sb.Append(c);
                    lines.Add(sb.ToString());
                    sb.Clear();
                }
                sb.Append(c);
            }
            sb.Clear();


            
            for(int i = 0; i < lines.Count; i++)
            {
                int startWord = 1;//Позиция начала слова
                int endWord = 0;

                foreach(char c in lines[i])
                {

                    if (char.IsLetter(c))
                    {
                        sb.Append(c);
                        endWord++;
                        continue;
                    }

                    if (c == ',' || c == ' ' || c == '{' || c == '}' || c == ';' || c == '\r' || c == lines[i][lines[i].Length - 1])
                    {
                        if (c != '\r' && c != '\n')
                            endWord++;

                        if (sb.Length != 0)
                        {
                            //Вычитаем endWord - 1 так как сейчас 'c' уже не слово
                            resultBuilder.AppendLine(_getInfoAboutWord(sb.ToString(), i + 1, startWord, endWord - 1));
                            sb.Clear();

                        }

                        if(c != '\r' && c != '\n')
                        {
                            resultBuilder.AppendLine(_getInfoAboutCharacter(c, i + 1, endWord));
                        }

                        startWord = endWord + 1;

                        continue;
                    }
                    else if (c == '\n') 
                        continue;
                    else
                    {
                        endWord++;

                        if(sb.Length != 0)
                        {
                            sb.Append(c);
                            resultBuilder.AppendLine(_getInfoAboutWord(sb.ToString(), i + 1, startWord, endWord));
                        }
                        else
                            resultBuilder.AppendLine(_getInfoAboutCharacter(c, i + 1, endWord));

                        startWord = endWord + 1;
                        sb.Clear() ;
                    }

                }
            }

            return resultBuilder.ToString();
        }

        private static string _getInfoAboutWord(string str, int numLine, int startWord, int endWord)
        {
            string result = $" - {str}\t\t- Строка {numLine}, с {startWord} по {endWord} символ";

            if(str == str.ToUpper())
                result = "code: " + 11 + "\tперечисление" + result;
            else if(str == "enum")
                result = "code: " + 12 + "\tключевое слово" + result;
            else
            {
                foreach(char c in str)
                {
                    if (!char.IsLetterOrDigit(c))
                        return "ERROR" + result;
                }
                result = "code: " + 13 + "\tидентификатор" + result;
            }

            return result;
        }
        private static string _getInfoAboutCharacter(char character, int numLine, int startWord)
        {
            string result = $"\t\t- Строка {numLine}, с {startWord} по {startWord} символ";

            switch(character)
            {
                case ' ':
                    result = "code: " + 24 + "\tразделитель - (пробел)" + result;
                    break;

                case ',':
                    result = "code: " + 35 + $"\tсимвол - {character}" + result;
                    break;
                case '{':
                    result = "code: " + 46 + $"\tоткрывающая скобка - {character}" + result;
                    break;
                case '}':
                    result = "code: " + 57 + $"\tзакрывающая скобка - {character}" + result;
                    break;
                case ';':
                    result = "code: " + 68 + $"\tконец перечисления - {character}" + result;
                    break;
                default:
                    result = "ERROR" + result;
                    break;
            }

            return result;

        }


        public static Task<List<Token>> AnalyzeAsync(string text)
        {
            List<Token> tokens = new List<Token>();
            List<string> lines = new List<string>();
            StringBuilder sb = new StringBuilder();

            //Разделение текста на строчки
            foreach (char c in text)
            {

                if (c == '\n' || c == '\r')
                {
                    if(sb.Length != 0) lines.Add(sb.ToString());
                    sb.Clear();
                    continue;
                }

                sb.Append(c);
            }
            if (sb.Length != 0) lines.Add(sb.ToString());//Добавление последней строки
            sb.Clear();

            for (int numLine = 0; numLine < lines.Count; numLine++) 
            {
                int position = 0;

                while(position < lines[numLine].Length)
                {
                    bool flag = false;//Флаг для проверки нахождения одного из токенов

                    //Цикл для определения принадлежности символа(ов) одному из токенов
                    foreach(TokenType token in TokenType.Tokens)
                    {
                        Regex regex = new Regex(token.Regex);
                       
                        Match match = Regex.Match(lines[numLine].Substring(position), "^" + token.Regex);

                        if (match.Success)
                        {
                            tokens.Add(new Token(token, match.Value, numLine + 1, position + 1, match.Value.Length + position));
                            position += match.Value.Length;
                            flag = true;
                        }

                    }

                    //Если токен не определен, значит есть ошибка
                    if (!flag)
                    {
                        Match Er_match = Regex.Match(lines[numLine].Substring(position), "^" + TokenType.TokenError.Regex);

                        if (Er_match.Success)
                        {
                            tokens.Add(new Token(TokenType.TokenError, Er_match.Value, numLine + 1, position + 1, Er_match.Value.Length + position));
                            position += Er_match.Value.Length;
                        }
                        else if(position < lines[numLine].Length)//Непредусмотренный символ ошибки
                        {
                            tokens.Add(new Token(TokenType.TokenError, lines[numLine][position].ToString(), numLine + 1, position + 1, position + 1));
                            position++;
                        }

                    }

                }
            }

            return Task.FromResult(tokens);
        }

        

    }
}
