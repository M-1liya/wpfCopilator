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

namespace wpfCopilator
{
    /// <summary>
    /// Логика взаимодействия для CreateNewWindow.xaml
    /// </summary>
    public partial class CreateNewWindow : Window
    {
        public string FileName { get; set; }
        public CreateNewWindow()
        {
            InitializeComponent();

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            FileName = myTextBox.Text;
            this.Close();
        }
    }
}
