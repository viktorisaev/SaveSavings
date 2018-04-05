using SaveSavings.Converters;
using SaveSavings.Model;
using SaveSavings.ViewModel;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsPage : Page
    {
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();

        ExpenseVM m_CurrentExpense = null;



        public DetailsPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // parameter as data
            m_CurrentExpense = e.Parameter as ExpenseVM;

            // fill data into widgets
            w_DateOfExpense.Date = new DateTimeOffset(m_CurrentExpense.Date);
            w_AmountOfExpense.Text = m_CurrentExpense.Amount.ToString();

            w_AmountOfExpense.Focus(FocusState.Keyboard);
        }



        private void UpdateContact_Click(object sender, RoutedEventArgs e)
        {
            // parse widgets values to data
            m_CurrentExpense.Date = w_DateOfExpense.Date.DateTime;
            int valCents = DataConversion.ConvertCurrencyStringToIntegerCents(w_AmountOfExpense.Text);

            // store data
            Spends spend = new Spends(m_CurrentExpense.Id, m_CurrentExpense.Date, valCents);
            Db_Helper.UpdateDetails(spend);//Update selected DB contact Id

            // interface transition
            Frame.Navigate(typeof(HomePage));
        }



        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Db_Helper.DeleteContact(m_CurrentExpense.Id);//Delete selected DB contact Id.
            Frame.Navigate(typeof(HomePage));
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
                this.Frame.GoBack();
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



    }   // class DetailsPage 
}
