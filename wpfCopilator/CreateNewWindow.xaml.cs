using System;
using System.Collections.Generic;
using System.IO;
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
        private string? _fileName;
        public string? FileName => _fileName;
        private readonly char[] forbiddenChars = {'\\', '/', '|', '*', '?', ':', '"', '<', '>' };
        string forbiddenChars_str = @"\ / | * ? : "" < >";
        public CreateNewWindow()
        {
            InitializeComponent();
            CreateButton.IsEnabled = false;

        }

        private void CreateButton_Click2(object sender, RoutedEventArgs e) 
        {
            string fname = myTextBox.Text;

            foreach(char c in forbiddenChars) 
            {
                if(fname.Contains(c))
                {
                    
                    MessageBox.Show($"В имени файла находятся запрещенные символы: {forbiddenChars_str}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error); 
                    return;
                }
            }
            _fileName = myTextBox.Text;
            this.Close();
        } 

        private void CancelButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void myTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(myTextBox.Text))
                CreateButton.IsEnabled = false;
            else 
                CreateButton.IsEnabled = true;
        }
    }
}
