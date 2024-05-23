using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using wpfCopilator.Analyzer;

namespace wpfCopilator.Parser
{
    public static partial class Grammatic
    {
        private static List<Token> input = new();
        private static string error;
        private static int position;

        public static string ErrorMessage => error;
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
            input = new List<Token>(tokens.Where(t => t.Type.Name != TokenType.TokenTypes.Space));
            position = 0;

            

            try
            {
                Parse();
            }
            catch (ParseException ex)
            {
                error = ex.Message;
                return false;
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

            E();
            if (position < input.Count)
                throw new ParseException($"Unexpected token at position {CurrentToken.PosLine}:{CurrentToken.PosStart}: {CurrentToken.Type.Name}");
        }

        private static void E()
        {
            T();
            while (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.PlusMinus)
            {
                Consume();
                T();
            }
        }

        private static void T()
        {
            F();
            while (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.MultDevide)
            {
                Consume();
                F();
            }
        }

        private static void F()
        {
            V();
            if (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.Exponentiation)
            {
                Consume();
                F();
            }
        }

        private static void V()
        {
            if (Match(TokenType.TokenTypes.LPar))
            {
                E();
                if (!Match(TokenType.TokenTypes.RPar))
                    throw new ParseException("Expected closing parenthesis");
            }
            else if (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.ID) // id
            {
                Consume();
                return;
            }
            else if (CurrentToken != null && CurrentToken.Type.Name == TokenType.TokenTypes.Operand) // number
            {
                Consume();
                return;
            }
            else if (CurrentToken == null) // ε (пустой символ)
            {
                return;
            }
            else
            {
                throw new ParseException($"Unexpected character at position {position}: {CurrentToken}");
            }
        }

        /*
        private static void Id()
        {
            if (!char.IsLetter(CurrentToken))
                throw new Exception($"Expected identifier at position {position}, but got: {CurrentToken}");

            while (char.IsLetterOrDigit(CurrentToken))
            {
                Consume();
            }
        }

        private static void Number()
        {
            if (!char.IsDigit(CurrentToken))
                throw new Exception($"Expected number at position {position}, but got: {CurrentToken}");

            while (char.IsDigit(CurrentToken))
            {
                Consume();
            }
        }*/
    }

    internal class ParseException : Exception 
    {
        internal ParseException(string message) : base(message) { }
    }
}
