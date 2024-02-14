using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfCopilator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void CommandNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CreateNewWindow window = new CreateNewWindow();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
            mainTabControl.Items.Add(new TabItem
            {
                Header = window.FileName,
                Content = new TextBox
                {
                    Name = "txt",
                    AcceptsReturn = true,
                    AcceptsTab = true,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                }
            }) ;
        }
        private void CommandNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
