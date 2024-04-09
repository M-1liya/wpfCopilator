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
    /// Логика взаимодействия для MethodAnalysis.xaml
    /// </summary>
    public partial class MethodAnalysis : Page
    {
        public LocalizationResources.Localization Localization
        {
            set
            {
                switch (value)
                {
                    case LocalizationResources.Localization.EN:
                        title.Content = "Method analysis";
                        ContentLabel.Text =
@"
Grammar G[‹DEF›] is automaton-based.
Rules for G[‹DEF›] are implemented on the graph below. Solid arrows on the graph represent syntactically correct parsing; dashed arrows symbolize transition to an error state (ERROR).
";
                        break;

                    case LocalizationResources.Localization.RU:
                        title.Content = "Метод анализа";
                        ContentLabel.Text =
@"
Грамматика G[‹DEF›] является автоматной.
Правила для G[‹DEF›] реализованы на графе ниже. Сплошные стрелки на графе характеризуют синтаксически верный разбор; пунктирные символизируют переход в состояние ошибки (ERROR);
";
                        break;

                    default:
                        this.Localization = LocalizationResources.Localization.EN;
                        break;
                }
            }
        }
        public MethodAnalysis()
        {
            InitializeComponent();
        }
    }
}
