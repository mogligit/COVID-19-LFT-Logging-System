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
            PopulatePatientList();
            PopulateComboBoxes();
        }

        private void PopulatePatientList()
        {
            List<Patient> patientList = Database.GetPatientList();
            foreach (Patient patient in patientList)
            {
                dataPatients.Items.Add(patient);
            }
        }
        private void PopulateComboBoxes()
        {
            cmbGender.ItemsSource = Database.GetGenderList();
            cmbEthnicity.ItemsSource = Database.GetEthnicGroupList();
            cmbCountry.ItemsSource = Database.GetCountryList();
            cmbAreaOfWork.ItemsSource = Database.GetAreaOfWorkList();
            cmbOccupation.ItemsSource = Database.GetOccupationList();
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

        private void dataPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient selected = (Patient) e.AddedItems[0];

            // Populate fields
            txtFirstName.Text = selected.FirstName;
            txtSurname.Text = selected.Surname;
            dateDoB.SelectedDate = selected.DoB;
            cmbGender.SelectedIndex = selected.GenderId;
            cmbEthnicity.SelectedIndex = selected.EthnicGroupId;
            txtNhsNumber.Text = selected.NHSNumber;
            txtAddress.Text = selected.Address;
            txtPostcode.Text = selected.Postcode;
            cmbCountry.SelectedIndex = selected.CountryId;

            //

            chkCurrentlyInWork.IsChecked = selected.CurrentlyInWork;
            
            //

            txtEmailAddress.Text = selected.EmailAddress;
            txtMobileNumber.Text = selected.MobileNumber;


        }

        // Experimental as-you-go validation (not currently working)
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (txtNhsNumber.Text.Length > 12)
            //{
            //    txtNhsNumber.Undo();
            //}
            //else
            //{
            //    int caretIndex = txtNhsNumber.CaretIndex;
            //    string s = txtNhsNumber.Text.PadRight(10, ' ');

            //    string formatted = String.Format("{0}{1}{2} {3}{4}{5} {6}{7}{8}{9}", s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8], s[9]);

            //    txtNhsNumber.Text = formatted;
            //    txtNhsNumber.CaretIndex = caretIndex;
            //}
            
        }

        private void chkSymptoms_Changed(object sender, RoutedEventArgs e)
        {
            if ((bool) chkSymptoms.IsChecked)
            {
                gridSymptoms.Visibility = Visibility.Visible;
            } 
            else
            {
                gridSymptoms.Visibility = Visibility.Hidden;
            }
        }

        private void chkCurrentlyInWork_Changed(object sender, RoutedEventArgs e)
        {
            if ((bool)chkCurrentlyInWork.IsChecked)
            {
                gridWorkDetails.Visibility = Visibility.Visible;
            }
            else
            {
                gridWorkDetails.Visibility = Visibility.Hidden;
            }
            
        }
    }
}
