using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static wpfCopilator.Analyzer.TokenType;

namespace wpfCopilator.Parser
{
    public interface IRule
    {
        /// <summary>
        /// Список продукций (правил) нетерминального символа
        /// </summary>
        List<object[]> ProductsList { get; }
    }

    /// <summary>
    /// Начальный нетерминальный символ Е
    /// </summary>
    public class E : IRule
    {
        /// <summary>
        /// Начальный нетерминальный символ Е
        /// </summary>
        public E() { }

        private static List<object[]> _products = new List<object[]>()
        {
            new object[] {TokenTypes.KeyWord,  new ID()}
        };

        public List<object[]> ProductsList => _products;
    }
    /// <summary>
    /// Нетерминальный символ ID
    /// </summary>
    public class ID : IRule
    {
        /// <summary>
        /// Нетерминальный символ ID
        /// </summary>

        public ID()
        {
            
        }

        private static List<object[]> _products = new List<object[]>()
        {
            new object[] { TokenTypes.ID, TokenTypes.LPar ,new En() }
        };

        public List<object[]> ProductsList => _products;
    }
    /// <summary>
    /// Нетерминальный символ En
    /// </summary>
    public class En : IRule
    {
        /// <summary>
        /// Нетерминальный символ En
        /// </summary>

        public En()
        {
            
        }

        private static List<object[]> _products = new List<object[]>()
        {
            new object[] { TokenTypes.Enumeration, TokenTypes.Сomma ,new En() },
            new object[] { TokenTypes.Enumeration, TokenTypes.Semicolon , TokenTypes.RPar }
        };

        public List<object[]> ProductsList => _products;
    }
}
