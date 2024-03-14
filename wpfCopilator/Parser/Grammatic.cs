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
        public static (List<Token> result, List<Token> errors) Parse(List<Token> tokens)
        {
            List<Token> errors = new List<Token>();
            List<Token> result = new List<Token>();
            

            List<object[]> productsList = new E().ProductsList;
            object[] products = productsList[0] != null ? productsList[0] : throw new Exception("Empty products list") ;

            _parseRec(tokens, products, result, errors);
            

            return (result, errors);
        }


        private static bool _parseRec(List<Token> tokens, object[] products, List<Token> result, List<Token> errors)
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
                        }
                        else if (tokens[t].Type.Name == TokenTypes.Space)
                        {
                            result.Add(tokens[t]);
                        }
                        else if (tokens[t].Type.Name == TokenTypes.Error)
                            continue;
                        else
                        {
                            errors.Add(tokens[t]);
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
                                List<Token> tmp_errors = new List<Token>();

                                if(_parseRec(tokens.GetRange(t, tokens.Count - t), item, tmp_result, tmp_errors) == true)
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
