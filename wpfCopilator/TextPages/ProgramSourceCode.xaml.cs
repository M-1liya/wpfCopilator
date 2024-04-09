using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                        title.Content = "Source code";
                        ContentLabel.Text = "The source code of the program can be found at the link below:";
                        break;

                    case LocalizationResources.Localization.RU:
                        title.Content = "Исходный код";
                        ContentLabel.Text = "Исходный код программы можно найти по ссылке ниже:";
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

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://github.com/M-1liya/wpfCopilator/tree/EnumAnalyzer",
                    UseShellExecute = true
                });
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening link: " + ex.Message);
            }
        }

    }
}
