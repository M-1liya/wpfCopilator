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
    /// Логика взаимодействия для Grammar.xaml
    /// </summary>
    public partial class Grammar : Page
    {
        public LocalizationResources.Localization Localization
        {
            set
            {
                switch (value)
                {
                    case LocalizationResources.Localization.EN:
                        title.Content = "Grammar";
                        ContentLabel.Text =
@"
Grammar G[<Def>]:

1. `<Def>` -> 'enum' `<SPACE>`
2. `<SPACE>` -> '_' `<ID>`
3. `<ID>` -> (letter | _ ) `<IDRem>`
4. `<IDRem>` -> (letter | _ | digit) `<IDRem>`
5. `<IDRem>` -> '{' `<ENUMER>`
6. `<ENUMER>` -> UppercaseLetter `<ENUMER>` | UppercaseLetter `<ENUMERend>`
7. `<ENUMERend>` -> ',' `<ENUMER>` | ';' `}'`

Where:
    - ‹digit› represents the digits 0 through 9.
    - ‹Letter› represents any letter from a to z (both lowercase and uppercase).
    - ‹Uppercaseletter› represents uppercase letters from A to Z.

Following the provided formal grammar definition, let's represent G[‹DEF›] in terms of its components:
    1. Z = ‹DEF›
    2. VT = [ a, b, c, ..., z, A, B, C, ..., Z, _, ;, 0, 1, 2, ..., 9, {, } ]
    3. VN = { ‹DEF›, ‹SPACE›, <ID>, ‹ENUMER›, ‹ENUMERend› }

    Grammar automaton G[<Def>]
";
                        break;

                    case LocalizationResources.Localization.RU:
                        title.Content = "Грамматика";
                        ContentLabel.Text =
@" Грамматика G[<Def>]:

1) <Def> 	-> 'enum'<SPACE>
2) <SPACE>	-> '_'<ID>
3) <ID> 	-> (letter | _ )<IDRem>
4) <IDRem>	-> (letter | _ | digit)<IDRem>
5) <IDRem>	-> '{' <ENUMER>
6) <ENUMER>	-> UppercaseLetter <ENUMER> | UppercaseLetter <ENUMERend>
7) <ENUMERend>	-> ','<ENUMER> | ';''}'

‹digit› → “0” | “1” | “2” | “3” | “4” | “5” | “6” | “7” | “8” | “9” ;
‹Letter› → “a” | “b” | “c” | ... | “z” | “A” | “B” | “C” | ... | “Z” ;
‹Uppercaseletter› → “A” | “B” | “C” | ... | “Z” ;

Следуя введенному формальному определению грамматики, представим G[‹DEF›] ее составляющими:
    1. Z = ‹DEF›
    2. VT = [ a, b, c, ..., z, A, B, C, ..., Z, _, ;, 0, 1, 2, ..., 9, {, } ];
    3. VN = { ‹DEF›, ‹SPACE›, <ID>, ‹ENUMER›, ‹ENUMERend› }

    Автомат грамматики G[<Def>]:
";
                        break;

                    default:
                        this.Localization = LocalizationResources.Localization.EN;
                        break;
                }
            }
        }
        public Grammar()
        {
            InitializeComponent();
        }
    }
}
