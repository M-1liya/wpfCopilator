using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using wpfCopilator.Analyzer;

namespace wpfCopilator.Parser
{
    public static partial class Grammatic
    {
        private static List<Token> input = new();
        private static List<string> output = new();
        private static List<string> error = new();
        private static int position;

        public static ReadOnlyCollection<string> Output => output.AsReadOnly<string>();
        public static List<string> ErrorMessage => error;
        private static Token? CurrentToken => position < input.Count ? input[position] : null;

        private static void Consume() => position++;

        private static bool Match(TokenType.TokenTypes expected)
        {
            if (CurrentToken != null && CurrentToken.Type.Name == expected)
            {
                Consume();
                return true;
            }
            return false;
        }


        public static bool RecursiveParse(IEnumerable<Token> tokens)
        {
            input = new List<Token>(tokens.Where(t => t.Type.Name != TokenType.TokenTypes.Space && t.Type.Name != TokenType.TokenTypes.Error));
            output.Clear();
            error.Clear();
            position = 0;

            

            try
            {
                Parse();
            }
            catch (ParseException ex)
            {
                error.Add(ex.Message);
                return true;
            }
            finally
            {
                input.Clear();
                position = 0;
            }
            return true;
        }

        private static void Parse()
        {
            try
            {
                E();
            }
            catch (ParseException ex)
            {
                error.Add(ex.Message);

                if(position < input.Count) 
                    E();
            }
 
            if (position < input.Count)
                throw new ParseException($"Unexpected token at position {CurrentToken.PosLine}:{CurrentToken.PosStart}: {CurrentToken.Type.Name}");
        }

        private static void E()
        {
            output.Add(" E ");

            try
            {
                T();
            }
            catch (ParseException ex)
            {
                error.Add(ex.Message);
                if (position < input.Count)
                    T();
            }

            while (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.PlusMinus)
            {
                Consume();
                try
                {
                    T();
                }
                catch (ParseException ex)
                {
                    error.Add(ex.Message);
                    if (position < input.Count)
                        T();
                }
            }
        }

        private static void T()
        {
            output.Add(" T ");
            try
            {
                F();
            }
            catch (ParseException ex)
            {
                error.Add(ex.Message);
                if (position < input.Count)
                    F();
            }

            while (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.MultDevide)
            {
                Consume();
                try
                {
                    F();
                }
                catch (ParseException ex)
                {
                    error.Add(ex.Message);
                    if (position < input.Count)
                        F();
                }
            }
        }

        private static void F()
        {
            output.Add(" F ");

            try
            {
                V();
            }
            catch (ParseException ex)
            {
                error.Add(ex.Message);
                if (position < input.Count)
                    V();
            }

            if (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.Exponentiation)
            {
                Consume();
                try
                {
                    F();
                }
                catch (ParseException ex)
                {
                    error.Add(ex.Message);
                    if (position < input.Count)
                        F();
                }
            }
        }

        private static void V()
        {
            output.Add(" V ");

            if (Match(TokenType.TokenTypes.LPar))
            {
                output.Add(" ( ");
                try
                {
                    E();
                }
                catch (ParseException ex)
                {
                    error.Add(ex.Message);
                    if (position < input.Count)
                        E();
                }

                if (!Match(TokenType.TokenTypes.RPar))
                {
                    throw new ParseException("Expected closing parenthesis");
                }
                output.Add(" ) ");
            }
            else if (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.ID) // id
            {
                output.Add(" id ");
                Consume();
                return;
            }
            else if (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.Operand) // number
            {
                output.Add(" number ");
                Consume();
                return;
            }
            else if (CurrentToken == null) // ε (пустой символ)
            {
                output.Add(" ε ");
                return;
            }
            else
            {
                output.Add(" ε ");
                return;
                //throw new ParseException($"Unexpected character at position {CurrentToken.PosLine}:{CurrentToken.PosStart}: {CurrentToken.Type.Name}");
            }
        }


    }

    internal class ParseException : Exception 
    {
        internal ParseException(string message) : base(message) { }
    }
}
