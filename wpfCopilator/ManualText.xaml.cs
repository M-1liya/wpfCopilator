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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using wpfCopilator.ManualPages;
using wpfCopilator.TextPages;

namespace wpfCopilator
{
    /// <summary>
    /// Логика взаимодействия для ManualText.xaml
    /// </summary>
    public partial class ManualText : Window
    {
        private Button? _previousButton = null;
        private List<Page> _pages = new List<Page>()
        {
            new FormulationProblem(),
            new Grammar(),
            new ClassificationGrammar(),
            new MethodAnalysis(),
            new DiagnosticsAndNeutralizationErrors(),
            new TestCase(),
            new Bibliography(),
            new ProgramSourceCode()
        };
        public ManualText(TextPage textPage)
            :base()
        {

        }

        public ManualText()
        {
            InitializeComponent();

            button_FormulationProblem.Tag = TextPage.FormulationProblem;
            button_Grammar.Tag = TextPage.Grammar ;
            button_ClassificationGrammar.Tag = TextPage.ClassificationGrammar;
            button_MethodAnalysis.Tag = TextPage.MethodAnalysis;
            button_DiagnosticsAndNeutralizationErrors.Tag = TextPage.DiagnosticsAndNeutralizationErrors;
            button_TestCase.Tag = TextPage.TestCase;
            button_Bibliography.Tag = TextPage.Bibliography;
            button_ProgramSourceCode.Tag = TextPage.ProgramSourceCode;
        
        }


        public void Click_button(object sender, RoutedEventArgs e)
        {
            if (_previousButton != null)
                _previousButton.IsEnabled = true;

            Button tmpButton = sender as Button;
            tmpButton.IsEnabled = false;
            _previousButton = tmpButton;

            if (tmpButton.Tag is not TextPage)
                throw new Exception($"{tmpButton.Name}'s tag is not TextPage");

            switch((TextPage)tmpButton.Tag)
            {
                case TextPage.FormulationProblem:  _navigateFrame<FormulationProblem>();        break;
                case TextPage.Grammar:              _navigateFrame<Grammar>();                  break;
                case TextPage.ClassificationGrammar: _navigateFrame<ClassificationGrammar>();   break;
                case TextPage.MethodAnalysis:       _navigateFrame<MethodAnalysis>();           break;
                case TextPage.DiagnosticsAndNeutralizationErrors: _navigateFrame<DiagnosticsAndNeutralizationErrors>(); break;
                case TextPage.TestCase:             _navigateFrame<TestCase>();                 break;
                case TextPage.Bibliography:         _navigateFrame<Bibliography>();             break;
                case TextPage.ProgramSourceCode:    _navigateFrame<ProgramSourceCode>();        break;
                default: throw new Exception($"There is no {(TextPage)tmpButton.Tag}");
            }
        }

        private void _navigateFrame<T>()
        {
            foreach (Page page in _pages)
            {
                if (page is T)
                {
                    ContentFrame.Navigate(page);
                    break;
                }
            }
        }
    }
}
