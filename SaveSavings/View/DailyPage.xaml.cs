using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

using SaveSavings.Model;
using SaveSavings.ViewModel;
using SaveSavings.Persistance;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DailyPage : Page
    {
        ObservableCollection<ExpenseVM> DB_ContactList = new ObservableCollection<ExpenseVM>();
        DateTime m_CurrentDate;


        public DailyPage()
        {
            this.InitializeComponent();
            //            this.Loaded += ReadContactList_Loaded;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // save current date passed at navigation
            m_CurrentDate = (DateTime)e.Parameter;

            ExpensesStorage dbcontacts = new ExpensesStorage();
            DB_ContactList = dbcontacts.GetDateContacts(m_CurrentDate);//Get all DB contacts  
            //if (DB_ContactList.Count > 0)
            //{
            //    btnDelete.IsEnabled = true;
            //}

            // set list data - recent dates first
            listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Date).ToList();

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
                ExpenseVM listitem = listBoxobj.SelectedItem as ExpenseVM;//Get slected listbox item contact ID
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

        //private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        //{
        //    On_BackRequested();
        //    args.Handled = true;
        //}



        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Back_Click(sender, null);
            e.Handled = true;
        }

    }   // class SingleDatePage


}   // namespace SaveSavings.View
