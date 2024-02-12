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
using System.Windows.Shapes;
using wpfCopilator.ManualPages;

namespace wpfCopilator
{
    /// <summary>
    /// Логика взаимодействия для Manual.xaml
    /// </summary>
    public partial class Manual : Window
    {
        private Button? _previousButton = null;
        public Manual()
        {
            InitializeComponent();
            
        }

        public void Click_PagePanelTool(object sender, RoutedEventArgs e)
        {
            if (_previousButton != null)
                _previousButton.IsEnabled = true;

            Button tmpButton = sender as Button;
            tmpButton.IsEnabled = false;
            _previousButton = tmpButton;

            ContentFrame.Content = new PagePanelTool();
        }

        public void Click_PageMenuFile(object sender, RoutedEventArgs e)
        {
            if (_previousButton != null)
                _previousButton.IsEnabled = true;

            Button tmpButton = sender as Button;
            tmpButton.IsEnabled = false;
            _previousButton = tmpButton;
        }

        public void Click_PageMenuEdit(object sender, RoutedEventArgs e)
        {
            if (_previousButton != null)
                _previousButton.IsEnabled = true;

            Button tmpButton = sender as Button;
            tmpButton.IsEnabled = false;
            _previousButton = tmpButton;
        }
    }
}
