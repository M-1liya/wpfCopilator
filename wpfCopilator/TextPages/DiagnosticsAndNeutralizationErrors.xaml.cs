using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfCopilator.TextPages
{
    /// <summary>
    /// Логика взаимодействия для DiagnosticsAndNeutralizationErrors.xaml
    /// </summary>
    public partial class DiagnosticsAndNeutralizationErrors : Page
    {
        public LocalizationResources.Localization Localization
        {
            set
            {
                switch (value)
                {
                    case LocalizationResources.Localization.EN:
                        title.Content = "Diagnosis and Neutralization of Errors";
                        ContentLabel1.Text =
@"
According to the assignment for the coursework, it is necessary to implement the neutralization of syntactic errors using Irons' method.

Irons' Method

The essence of Irons' method is as follows:
Upon detecting an error (a symbol is encountered in the input stream during parsing that does not correspond to any of the expected symbols), the input string of symbols looks as follows: Tt, where T is the next symbol in the input stream (erroneous symbol), t is the remaining string of symbols in the input stream after T. The neutralization algorithm consists of the following steps:
Define the incomplete branches of the parse tree;
Form a set L - a set of residual symbols of incomplete branches of the parse tree;
Remove the next symbol from the input string until the string takes the form Tt, such that U => T, where U ∈ L, i.e., until the next symbol T in the string cannot be derived from any of the residual symbols of incomplete branches.
Determine which of the incomplete branches caused the appearance of symbol U in the set L (in other words, which part of the incomplete branches includes symbol U).
Thus, it is determined to which branch in the parse tree the remaining input string can be ""attached"" after removing the erroneous fragment from the text.

Irons' Method for Automaton Grammar

The developing parser is based on an automaton grammar. The implementation of Irons' algorithm for automaton grammar has the following feature.
The parse tree using an automaton grammar is presented in picture 1.
";
                        ContentLabel2.Text =
@"Picture 1 – Parse tree structure for automaton grammar

Thus, in the event of a syntax error during parsing using an automaton grammar, there will always be only one incomplete branch in the parse tree (see Picture 2).";
                        ContentLabel3.Text =
@"Picture 2 - Incomplete branch when a syntax error occurs (highlighted with dashed lines)

Since the only incomplete branch is the one where the syntax error occurred during construction, it is the only branch to which the remaining input string of symbols can be attached.
It is proposed to reduce the neutralization algorithm to sequentially removing the next symbol from the input string until the next symbol turns out to be one of those allowed at the current parsing moment."; 
                        break;

                    case LocalizationResources.Localization.RU:
                        title.Content = "Диагностика и нейтрализация ошибок";
                        ContentLabel1.Text =
@"
Согласно заданию на курсовую работу, необходимо реализовать нейтрализацию синтаксических ошибок, используя метод Айронса.

Метод Айронса

Суть метода Айронса заключается в следующем:
При обнаружении ошибки (во входной цепочке в процессе разбора встречается символ, который не соответствует ни одному из ожидаемых символов), входная цепочка символов выглядит следующим образом: Tt, где T – следующий символ во входном потоке (ошибочный символ), t – оставшаяся во входном потоке цепочка символов после T. Алгоритм нейтрализации состоит из следующих шагов:

Определяются недостроенные кусты дерева разбора;
Формируется множество L – множество остаточных символов недостроенных кустов дерева разбора;
Из входной цепочки удаляется следующий символ до тех пор, пока цепочка не примет вид Tt, такой, что U => T, где U ∈ L, то есть до тех пор, пока следующий в цепочке символ T не сможет быть выведен из какого-нибудь из остаточных символов недостроенных кустов.
Определяется, какой из недостроенных кустов стал причиной появления символа U в множестве L (иначе говоря, частью какого из недостроенных кустов является символ U).
Таким образом, определяется, к какому кусту в дереве разбора можно «привязать» оставшуюся входную цепочку символов после удаления из текста ошибочного фрагмента.

Метод Айронса для автоматной грамматики

Разрабатываемый синтаксический анализатор построен на базе автоматной грамматики. Реализация алгоритма Айронса для автоматной грамматики имеет следующую особенность.

Дерево разбора с использованием автоматной грамматики представлено на рисунке 1.
";
                        ContentLabel2.Text =
@"Рисунок 1 – Структура дерева разбора для автоматной грамматики

Таким образом, при возникновении синтаксической ошибки в процессе разбора с использованием автоматной грамматики, в дереве разбора всегда будет только один недостроенный куст (см. рисунок 2).
";
                        ContentLabel3.Text =
@"
Рисунок 2 - Недостроенный куст при возникновении синтаксической ошибки (выделен пунктиром)

Поскольку единственный недостроенный куст – это тот, во время построения которого возникла синтаксическая ошибка, то это единственный куст, к которому можно привязать оставшуюся входную цепочку символов.
Предлагается свести алгоритм нейтрализации к последовательному удалению следующего символа во входной цепочке до тех пор, пока следующий символ не окажется одним из допустимых в данный момент разбора.
";
                        break;

                    default:
                        this.Localization = LocalizationResources.Localization.EN;
                        break;
                }
            }
        }
        public DiagnosticsAndNeutralizationErrors()
        {
            InitializeComponent();
        }
    }
}
