using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace COVID_19_LFT_Logging_System
{
    /// <summary>
    /// Interaction logic for LogTestWindow.xaml
    /// </summary>
    public partial class LogTestWindow : Window
    {
        public LogTestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            List<Patient> patientList = Database.GetPatientList();
            foreach (Patient patient in patientList)
            {
                dataPatients.Items.Add(patient);
            }
          
        }

        private void txtLookup_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtLookup.Text.ToUpper();

            ICollectionView cv = CollectionViewSource.GetDefaultView(dataPatients.Items);


            if (!string.IsNullOrEmpty(filter))
            {
                cv.Filter = o =>
                {
                    Patient p = (Patient) o;
                    return (p.FirstName.ToUpper().Contains(filter) || p.Surname.ToUpper().Contains(filter));
                };
            }
            

        }

    }
}
