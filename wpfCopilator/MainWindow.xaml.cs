using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Path = System.IO.Path;

namespace wpfCopilator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog _openDialog = new OpenFileDialog();
        SaveFileDialog _saveDialog = new SaveFileDialog();
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
                Tag = Environment.CurrentDirectory + "//" + "tempFilesDirectory//"+ window.FileName + ".txt",
                Header = window.FileName,
                Content = new TextBox
                {
                    Name = "txt",
                    AcceptsReturn = true,
                    AcceptsTab = true,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                }
            }) ;
            TabItem item = mainTabControl.Items[mainTabControl.Items.Count - 1] as TabItem;
            item.Focus();
        }
        private void CommandNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _openDialog.Filter = "Text files (*.TXT)|*.txt|All Files (*.*)|*.*";
            if (_openDialog.ShowDialog() == true)
            {
                StreamReader reader = new StreamReader(_openDialog.FileName);
                mainTabControl.Items.Add(new TabItem
                {
                    Tag = _openDialog.FileName,
                    Header = Path.GetFileNameWithoutExtension(_openDialog.FileName),
                    Content = new TextBox
                    {
                        Name = "txt",
                        Text = reader.ReadToEnd(),
                        AcceptsReturn = true,
                        AcceptsTab = true,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                    }
                });
                reader.Close();
            }
            TabItem item = mainTabControl.Items[mainTabControl.Items.Count - 1] as TabItem;
            item.Focus();
        }
        private void CommandSaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TabItem item = mainTabControl.SelectedItem as TabItem;
            TextBox textBox = item.Content as TextBox;
            _saveDialog.Filter = "Text files (*.TXT)|*.txt|All Files (*.*)|*.*";
            if (_saveDialog.ShowDialog() == true)
            {
                StreamWriter writer = new StreamWriter(_saveDialog.FileName);
                writer.WriteLine(textBox.Text);
                writer.Close();
            }
            item.Tag = _saveDialog.FileName;
        }
        private void CommandSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TabItem item = mainTabControl.SelectedItem as TabItem;
            TextBox textBox = item.Content as TextBox;
            StreamWriter writer = new StreamWriter(Convert.ToString(item.Tag));
            writer.WriteLine(textBox.Text);
            writer.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory + "//" + "tempFilesDirectory//");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory + "//" + "tempFilesDirectory//");
            int i = 0;
            foreach (FileInfo file in di.GetFiles())
            {
                i++;
            }
            if(i!= 0)
            {
                MessageBoxResult messageResult = MessageBox.Show("У вас " + Convert.ToString(i) + " несохраненных файлов, вы хотите их сохранить?", "Предупреждение", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (messageResult == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else if (messageResult == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public void Click_HelpButton(object sender, RoutedEventArgs e)
        {
            Manual manual = new Manual();
            manual.Show();
        }
        public void Click_AboutProgram(object sender, RoutedEventArgs e)
        {
            AboutProgram aboutProgram = new AboutProgram();
            aboutProgram.Show();
            
        }
    }
}
