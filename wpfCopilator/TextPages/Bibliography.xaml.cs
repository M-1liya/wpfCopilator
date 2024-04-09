using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using wpfCopilator.Parser;

namespace wpfCopilator.TextPages
{
    /// <summary>
    /// Логика взаимодействия для Bibliography.xaml
    /// </summary>
    public partial class Bibliography : Page
    {
        public LocalizationResources.Localization Localization
        {
            set
            {
                switch (value)
                {
                    case LocalizationResources.Localization.EN:
                        title.Content = "Reference";
                        ContentLabel.Text =
@"
    Bibliography:
  1. ""Theory of Programming Languages: Design and Implementation"" by Yu. V. Shornikov. – Novosibirsk: NSTU Publishing, 2022. – 290 pages. – (NSTU Textbooks).
  2. ""Construction of Compilers for Digital Computing Machines"" by D.Gris; translated from English by E.B.Dokshitskaya, L.A.Zelenina, L.B.Morozova, V.S.Shtarkman; edited by Yu.M.Bayakovskiy, Vs.S.Shtarkman. - Moscow, 1975. - 544 pages: tables, diagrams.
  3. ""Compilers: Principles, Techniques, and Tools"" by A.Aho, R.Sethi, D.Ullman. - Moscow, 2003. - 768 pages.
  4. ""Formal Languages and Compilers: A Textbook"" by A.A.Malyavko. - Novosibirsk: NSTU Publishing, 2014. - 431 pages. (Series ""NSTU Textbooks"").
  5. ""Programming Languages and Translation Methods: A Textbook"" by S.Z.Sverdlov. — 2nd edition, revised. — St.Petersburg: ""Lan"" Publishing, 2019. — 564 pages: illustrations. — (Textbooks for Universities.Special Literature).
";
                       break;

                    case LocalizationResources.Localization.RU:
                        title.Content = "Список литературы";
                        ContentLabel.Text =
@"
    Список литературы:
  1. Теория языков программирования: проектирование и реализация : учебное пособие / Ю. В. Шорников. – Новосибирск : Изд-во НГТУ, 2022. – 290 с. – (Учебники НГТУ).
  2. Грис Д. Конструирование компиляторов для цифровых вычислительных машин / Д. Грис ; пер. с. англ. Е. Б. Докшицкой, Л. А. Зелениной, Л. Б. Морозовой, В. С. Штаркмана,  под ред. Ю. М. Баяковского, Вс. С. Штаркмана. - М., 1975. - 544 с. : табл., схемы
  3. Ахо А. В. Компиляторы : Принципы, технологии, инструменты / А. Ахо, Р. Сети, Д. Ульман. - М., 2003. - 768 с.
  4. Малявко, А. А. Формальные языки и компиляторы : учебник / Малявко А. А. - Новосибирск : Изд-во НГТУ, 2014. - 431 с. (Серия ""Учебники НГТУ"")
  5. Свердлов С. З. Языки программирования и методы трансляции: Учебное пособие. — 2е изд., испр. — СПб.: Издательство «Лань», 2019. — 564 с.: ил. — (Учебники для вузов. Специальная литература).";
                        break;

                    default:
                        this.Localization = LocalizationResources.Localization.EN;
                        break;
                }
            }
        }
        public Bibliography()
        {
            InitializeComponent();
        }
    }
}
