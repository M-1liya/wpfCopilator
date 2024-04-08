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
    /// Логика взаимодействия для ProgramSourceCode.xaml
    /// </summary>
    public partial class ProgramSourceCode : Page
    {
        public LocalizationResources.Localization Localization
        {
            set
            {
                switch (value)
                {
                    case LocalizationResources.Localization.EN:
                        title.Content = "Problem statement";
                        ContentLabel.Text = "In Java, Enumerations or Java Enum serve the purpose of representing a group of named constants in a programming language. Java Enums are used when we know all possible values at compile time, such as choices on a menu, rounding modes, command-line flags, etc. The set of constants in an enum type doesn’t need to stay fixed for all time.\r\n\r\nWhat is Enumeration or Enum in Java?\r\nA Java enumeration is a class type. Although we don’t need to instantiate an enum using new, it has the same capabilities as other classes. This fact makes Java enumeration a very powerful tool. Just like classes, you can give them constructors, add instance variables and methods, and even implement interfaces.";
                        break;

                    case LocalizationResources.Localization.RU:
                        title.Content = "Постановка задачи";
                        ContentLabel.Text = "В Java, Перечисления или Java Enum служат для представления группы именованных констант в языке программирования. Перечисления Java используются, когда мы знаем все возможные значения во время компиляции, такие как варианты выбора в меню, режимы округления, флаги командной строки и т.д. Набор констант в типе enum не обязательно должен оставаться фиксированным на все время.\r\n\r\nЧто такое перечисление в Java?\r\nПеречисление Java - это тип класса. Хотя нам не нужно создавать экземпляр enum с помощью новое, он обладает теми же возможностями, что и другие классы. Этот факт делает Java enumeration очень мощным инструментом. Точно так же, как классам, вы можете присваивать им конструкторы, добавлять переменные и методы экземпляра и даже реализовывать интерфейсы.";
                        break;

                    default:
                        this.Localization = LocalizationResources.Localization.EN;
                        break;
                }
            }
        }
        public ProgramSourceCode()
        {
            InitializeComponent();
        }
    }
}
