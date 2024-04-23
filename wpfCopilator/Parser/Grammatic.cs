using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfCopilator.Analyzer;
using static wpfCopilator.Analyzer.TokenType;

namespace wpfCopilator.Parser
{
    public static class Grammatic
    {
        private static int _countErrrors = 0;
        public static (List<Token> result, List<string> errors) Parse(List<Token> tokens)
        {
            _countErrrors = 0;
            (List<Token> result, List<string> errors) ParsingResult = (new List<Token>(), new List<string>());
            
           for (int i = 0; i < tokens.Count; i++)
           {
                if (tokens[i].Type.Name == TokenTypes.KeyWord)
                {
                    (List<Token> result, List<string> errors) enumParsingResult = ParseEnum(tokens.GetRange(i, tokens.Count - i));

                    ParsingResult.result.AddRange(enumParsingResult.result);
                    ParsingResult.errors.AddRange(enumParsingResult.errors);

                    break;
                }
                else if (tokens[i].Type.Name != TokenTypes.Space)
                    ParsingResult.errors.Add($"error: '{tokens[i].Text}' не существует в текущем контексте.");
           }

            return ParsingResult;
        }

        public static (List<Token> result, List<string> errors) ParseEnum(List<Token> tokens)
        {
            _countErrrors = 0;

            List<string> errors = new List<string>();
            List<Token> result = new List<Token>();


            List<object[]> productsList = new E().ProductsList;
            object[] products = productsList[0] != null ? productsList[0] : throw new Exception("Empty products list");

            _parseRec(tokens, products, result, errors);

            return (result, errors);
        }

        private static bool _parseRec(List<Token> tokens, object[] products, List<Token> result, List<string> errors)
        {
            List<object[]> productsList = new E().ProductsList;

            int p = 0;
            for (int t = 0; t < tokens.Count && p < products.Length; t++)
            {
                switch (products[p])
                {
                    case TokenTypes:

                        if (tokens[t].Type.Name == (TokenTypes)products[p])
                        {
                            result.Add(tokens[t]);
                            p++;
                            _countErrrors = 0;
                        }
                        else if (tokens[t].Type.Name == TokenTypes.Space)
                        {
                            result.Add(tokens[t]);
                        }
                        else if (tokens[t].Type.Name == TokenTypes.Error)
                            continue;
                        else
                        {
                            errors.Add($"Error: line {tokens[t].PosLine} column {tokens[t].PosStart}, expected {(TokenTypes)products[p]}");
                            _countErrrors++;
                            if (_countErrrors == 1)
                            {
                                t--;
                                p++;
                            }
                            continue;
                        }
                        break;

                    case IRule:

                        productsList = ((IRule)products[p]).ProductsList;

                        if (productsList.Count > 1)
                        {

                            foreach (object[] item in productsList)
                            {
                                List<Token> tmp_result = new List<Token>();
                                List<string> tmp_errors = new List<string>();

                                if (_parseRec(tokens.GetRange(t, tokens.Count - t), item, tmp_result, tmp_errors) == true)
                                {
                                    result.AddRange(tmp_result);
                                    errors.AddRange(tmp_errors);

                                    return true;
                                }
                            }


                        }
                        products = productsList[0] != null ? productsList[0] : throw new Exception("Empty products list");

                        t--;
                        p = 0;
                        break;

                    default:
                        throw new Exception("Unknown type");

                }
            }



            //Если ошибок нет возврашается TRUE
            //Если по правилам продукции дошли до конца и последний токен, который был добавлен, завершает эту продукцию, то возвращаем TRUE
            //В противном случае - FALSE
            if (errors.Count == 0)
                return true;
            else if (p == products.Length && tokens[tokens.Count - 1].Type.Name == (TokenTypes)products[p - 1])
                return true;
            else
                return false;
        }

    }

}
