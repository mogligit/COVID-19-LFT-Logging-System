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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace COVID_19_LFT_Logging_System
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

        // Test connection to database at load up
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            // check if database exists, and create one
            string workingDir = Directory.GetCurrentDirectory();
            string databasePath = workingDir + "\\TestDB";
            databasePath = "DeleteDB";

            if (!Database.TryConnect(databasePath))
            {
                Database.CreateNewDatabase(databasePath);
                Database.TryConnect(databasePath);
                lblDBStatus.Content = "Database Online";
                lblDBStatus.Background = new SolidColorBrush(Color.FromRgb(51, 204, 51));
            }
            else
            {
                lblDBStatus.Content = "Database Error";
                lblDBStatus.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                // Prompt for new database or alternative one
                MessageBox.Show("Database could not be found.");
            }
                       
        }

        private void btnLogTests_Click(object sender, RoutedEventArgs e)
        {
            (new LogTestWindow()).Show();
        }

        private void btnOutput_Click(object sender, RoutedEventArgs e)
        {
            // Open Output window
        }

        private void btnBrowseData_Click(object sender, RoutedEventArgs e)
        {
            // Open Browse window
        }
    }
}
