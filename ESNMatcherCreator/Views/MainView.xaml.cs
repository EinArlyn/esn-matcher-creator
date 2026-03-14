using ESNMatcherCreator.ViewModels;
using Microsoft.Win32;
using System.Windows;

namespace ESNMatcherCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MainWindow() => InitializeComponent();

        /// <summary>
        /// Событие клика для открытия диалогового окна OpenFileDialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileDelimited_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
                ((MainViewModel)DataContext).ReadCSVFile(openFileDialog.FileName);
        }

        /// <summary>
        /// Событие клика для открытия диалогового окна SaveFileDialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "XML file (*.xml)|*.xml",
                FileName = "ESNMatcher"
            };

            if (saveFileDialog.ShowDialog() == true)
                ((MainViewModel)DataContext).SaveXMLFile(saveFileDialog.FileName);
        }
    }
}
