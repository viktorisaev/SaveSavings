using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DailyPage : Page
    {
        ObservableCollection<Spends> DB_ContactList = new ObservableCollection<Spends>();
        DateTime m_CurrentDate;


        public DailyPage()
        {
            this.InitializeComponent();
//            this.Loaded += ReadContactList_Loaded;
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // save current date passed at navigation
            m_CurrentDate = (DateTime)e.Parameter;

            ReadAllContactsList dbcontacts = new ReadAllContactsList();
            DB_ContactList = dbcontacts.GetDateContacts(m_CurrentDate);//Get all DB contacts  
            //if (DB_ContactList.Count > 0)
            //{
            //    btnDelete.IsEnabled = true;
            //}

            // set list data - recent dates first
            listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Date).ToList();

            // back button
            BackButton.IsEnabled = Frame.CanGoBack;

//            CultureInfo myCI = CultureInfo.CurrentCulture;
//            w_DailyCurrentDate.Text = myCI.NumberFormat.CurrencyDecimalSeparator + myCI.NumberFormat.NumberDecimalSeparator + myCI.NumberFormat.CurrencySymbol;

            // THIS IS PROPER CODE!!!
            w_DailyCurrentDate.Text = string.Format("{0:D}", m_CurrentDate);
        }


        //private void ReadContactList_Loaded(object sender, RoutedEventArgs e)
        //{
        //    ReadAllContactsList dbcontacts = new ReadAllContactsList();
        //    DB_ContactList = dbcontacts.GetAllContacts();//Get all DB contacts  
        //    //if (DB_ContactList.Count > 0)
        //    //{
        //    //    btnDelete.IsEnabled = true;
        //    //}

        //    // set list data - recent dates first
        //    listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Date).ToList();
        //}



        private void DateListItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                Spends listitem = listBoxobj.SelectedItem as Spends;//Get slected listbox item contact ID
                Frame.Navigate(typeof(DetailsPage), listitem);
            }
        }



        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass delete = new DatabaseHelperClass();
            delete.DeleteAllContact();//delete all DB contacts
            DB_ContactList.Clear();//Clear collections
//            btnDelete.IsEnabled = false;
            listBoxobj.ItemsSource = DB_ContactList;
        }



        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddContact), m_CurrentDate);
        }



        private void Back_Click(object sender, RoutedEventArgs e)
        {
            On_BackRequested();
        }



        // Handles system-level BackRequested events and page-level back button Click events
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.Navigate(typeof(HomePage));
                return true;
            }
            return false;
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }


    }   // class SingleDatePage





    public class CurrencyAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            var ss = string.Format("{0:C}", ((int)value) / 100.0f);

            return ss;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }







}   // namespace SaveSavings.View
