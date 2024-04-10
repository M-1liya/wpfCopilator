using System;
using System.Collections;
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

        public static (List<Token> result, List<Token> errors) ParsePOLIZ(List<Token> stack1)
        {
            List <Token> tokens = POLIZ(stack1);
            int  iterator = 0;

            while (tokens.Count > 1)
            {
                if (tokens[iterator].Type.Name == TokenTypes.Operation)
                {
                    float.TryParse(tokens[iterator - 2].Text, out float a);
                    float.TryParse(tokens[iterator - 1].Text, out float b);

                    switch (tokens[iterator].Text)
                    {
                        case "+": b = a + b; break;
                        case "-": b = a - b; break;
                        case "*": b = a * b; break;
                        case "/": b = a / b; break;
                        default:
                            throw new Exception("Неизвестная операция:" + tokens[iterator].Text);
                    }

                    tokens[iterator - 2].Text = b.ToString();
                    tokens.RemoveRange(iterator - 1, 2);

                    iterator = 0;
                }
                else
                    iterator++;

            }
            return (tokens, new List<Token>());
        }
        public static List<Token>  POLIZ(List<Token> stack1)
        {
            List<Token> stack2 = new List<Token>();
            List<TokenPoliz> stack3 = new List<TokenPoliz>();

            foreach(Token token in stack1)
            {
                switch(token.Type.Name)
                {
                    case TokenTypes.Operand:
                        stack2.Add(token);
                        break;

                    case TokenTypes.LPar: 
                        
                        stack3.Insert(0, new TokenPoliz(token, 0));
                        break;

                    case TokenTypes.RPar:

                        TokenPoliz rpar = new TokenPoliz(token, 1);
                        process(stack2, stack3, rpar);
                        break;

                    case TokenTypes.Operation:

                        TokenPoliz oper;

                        if (token.Text == "+" || token.Text == "-") 
                            oper = new TokenPoliz(token, 2);
                        else if (token.Text == "*" || token.Text == "/")
                            oper = new TokenPoliz(token, 3);
                        else
                            throw new Exception("Не определен прироритет для данной операции:" +  token.Text);

                        process(stack2, stack3, oper);
                        break;

                }
            }

            foreach (TokenPoliz token in stack3)
                if(token.Token.Type.Name == TokenTypes.Operation)
                    stack2.Add(token.Token);

            return stack2;
        }

        private static void process(List<Token> stack2, List<TokenPoliz> stack3, TokenPoliz token)
        {
            while (stack3.Count != 0 && stack3[0].Priority >= token.Priority)
            {
                stack2.Add(stack3[0].Token);
                stack3.RemoveAt(0);
            }

            stack3.Insert(0, token);
        }


    }

}
