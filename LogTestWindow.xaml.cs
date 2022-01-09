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
        private Patient SelectedPatient;
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
            //foreach (Patient patient in patientList)
            //{
            //    dataPatients.Items.Add(patient);
            //}
            dataPatients.ItemsSource = patientList;
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
            else
            {
                cv.Filter = null;
            }
            

        }

        private void dataPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get selected Patient object
            SelectedPatient = (Patient) e.AddedItems[0];

            // Populate fields
            txtFirstName.Text = SelectedPatient.FirstName;
            txtSurname.Text = SelectedPatient.Surname;
            dateDoB.SelectedDate = SelectedPatient.DoB;
            cmbGender.SelectedIndex = SelectedPatient.GenderId;
            cmbEthnicity.SelectedIndex = SelectedPatient.EthnicGroupId;
            txtNhsNumber.Text = SelectedPatient.NHSNumber;
            txtAddress.Text = SelectedPatient.Address;
            txtPostcode.Text = SelectedPatient.Postcode;
            cmbCountry.SelectedIndex = SelectedPatient.CountryId;

            //

            chkCurrentlyInWork.IsChecked = SelectedPatient.CurrentlyInWork;
            
            //

            txtEmailAddress.Text = SelectedPatient.EmailAddress;
            txtMobileNumber.Text = SelectedPatient.MobileNumber;


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
                gridSymptoms.IsEnabled = true;
            } 
            else
            {
                gridSymptoms.IsEnabled = false;
            }
        }

        private void chkCurrentlyInWork_Changed(object sender, RoutedEventArgs e)
        {
            if ((bool)chkCurrentlyInWork.IsChecked)
            {
                gridWorkDetails.IsEnabled = true;
            }
            else
            {
                gridWorkDetails.IsEnabled = false;
            }
            
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Create Test object

            TestType currentTestType = new TestType();
            if (rdbLFDTest.IsChecked == true)
            {
                currentTestType = TestType.LFD;
            }
            else if (rdbPCRTest.IsChecked == true)
            {
                currentTestType = TestType.PCR;
            }

            string barcode = txtBarcodeNumber.Text;
            DateTime now = DateTime.Now;
            // NO CURRENT OPTION FOR THIS - DEFAULT TO FALSE
            bool contactTesting = false;
            //
            bool symptoms = (bool) chkSymptoms.IsChecked;
            int patientId = SelectedPatient.Id;
            

            Test newTest = new Test(currentTestType, barcode, now, contactTesting, symptoms, patientId);

            if (symptoms && dateSymptoms.SelectedDate != null)
            {
                newTest.SymptomsStartDate = (DateTime) dateSymptoms.SelectedDate;
            }

            if (Database.SubmitNewTest(newTest))
            {
                lblSubmissionStatus.Content = newTest.Barcode + " submitted successfully";
                lblSubmissionStatus.Background = new SolidColorBrush(Color.FromRgb(0, 200, 0));
            }
            else
            {
                lblSubmissionStatus.Content = newTest.Barcode + " not submitted";
                lblSubmissionStatus.Background = new SolidColorBrush(Color.FromRgb(200, 0, 0));
            }
        }

        private void btnNow_Click(object sender, RoutedEventArgs e)
        {
            dateTestDate.SelectedDate = DateTime.Today;
            string timeNow = DateTime.Now.ToString("h tt").ToLower();
            cmbTestTime.Text = timeNow;
        }
    }
}
