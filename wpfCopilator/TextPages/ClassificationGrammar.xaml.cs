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
    /// Логика взаимодействия для ClassificationGrammar.xaml
    /// </summary>
    public partial class ClassificationGrammar : Page
    {
        public LocalizationResources.Localization Localization
        {
            set
            {
                switch (value)
                {
                    case LocalizationResources.Localization.EN:
                        title.Content = "Classification grammar";
                        ContentLabel.Text =
@"
According to Chomsky's classification, the grammar G[‹DEF›] is considered to be automaton-based.
Rules (1)-(7) belong to the class of right-recursive productions (A → aB | a | ε).

    1) <Def> 	-> 'enum'<SPACE>
    2) <SPACE>	-> '_'<ID>
    3) <ID> 	-> (letter | _ )<IDRem>
    4) <IDRem>	-> (letter | _ | digit)<IDRem>
    5) <IDRem>	-> '{' <ENUMER>
    6) <ENUMER>	-> UppercaseLetter <ENUMER> | UppercaseLetter <ENUMERend>
    7) <ENUMERend>	-> ','<ENUMER> | ';''}'

";
                            break;

                    case LocalizationResources.Localization.RU:
                        title.Content = "Классификация грамматики";
                        ContentLabel.Text =
@"
Согласно классификации Хомского, грамматика G[‹DEF›] является автоматной.
Правила (1)-(7) относятся к классу праворекурсивных продукций (A → aB | a | ε):

    1) <Def> 	-> 'enum'<SPACE>
    2) <SPACE>	-> '_'<ID>
    3) <ID> 	-> (letter | _ )<IDRem>
    4) <IDRem>	-> (letter | _ | digit)<IDRem>
    5) <IDRem>	-> '{' <ENUMER>
    6) <ENUMER>	-> UppercaseLetter <ENUMER> | UppercaseLetter <ENUMERend>
    7) <ENUMERend>	-> ','<ENUMER> | ';''}'

";
                        break;

                    default:
                        this.Localization = LocalizationResources.Localization.EN;
                        break;
                }
            }
        }
        public ClassificationGrammar()
        {
            InitializeComponent();
        }
    }
}
