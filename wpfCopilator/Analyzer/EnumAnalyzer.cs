﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        public static async Task<string> Analyze(string text)
        {
            List<string> lines = new List<string>();
            StringBuilder sb = new StringBuilder();
            StringBuilder resultBuilder = new StringBuilder();

            text += '\n';
            //Разделение текста на строчки
            foreach (char c in text)
            {
                if(c == '\n')
                {
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

                    if (c == ',' || c == ' ' || c == '{' || c == '}' || c == ';' || c == '\r')
                    {
                        endWord++;
                        if (sb.Length != 0)
                        {
                            //Вычитаем endWord - 1 так как сейчас 'c' уже не слово
                            resultBuilder.AppendLine(_getInfoAboutWord(sb.ToString(), i + 1, startWord, endWord - 1));
                            startWord = endWord + 1;
                            sb.Clear();

                            resultBuilder.AppendLine(_getInfoAboutCharacter(c, i + 1, endWord));
                            continue;
                        }
                        else if(c != '\r')
                        {
                            resultBuilder.AppendLine(_getInfoAboutCharacter(c, i + 1, startWord));
                        }

                        startWord = endWord + 1;

                        continue;
                    }
                    else if (c == '\n') 
                        continue;
                    else
                    {
                        endWord++;
                        resultBuilder.AppendLine(_getInfoAboutCharacter(c, i + 1, endWord));
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
                result = "code: " + 13 + "\tидентификатор" + result;

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

    }
}
