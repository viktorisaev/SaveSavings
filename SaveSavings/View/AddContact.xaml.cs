using SaveSavings.ViewModel;
using System;
using System.Globalization;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{

    public sealed partial class AddContact : Page
    {
        public AddContact()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // set date if passed
            if (e.Parameter != null)
            {
                DateTime date = (DateTime)e.Parameter;

                w_DateOfExpense.Date = new DateTimeOffset(date);

            }

            // TODO: back button
            //            BackButton.IsEnabled = Frame.CanGoBack;

            w_AmountOfExpense.Focus(FocusState.Keyboard);
        }


        private async void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            if (w_AmountOfExpense.Text != "")
            {
                try
                {
                    int valCents = DataConversion.ConvertCurrencyStringToIntegerCents(w_AmountOfExpense.Text);

                    Db_Helper.Insert(new Spends(w_DateOfExpense.Date.DateTime, valCents));
                    Frame.Navigate(typeof(DailyPage), w_DateOfExpense.Date.DateTime);
                }
                catch
                {

                }


            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Please fill two fields");
                await messageDialog.ShowAsync();
            }
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

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }



        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Back_Click(sender, null);
            e.Handled = true;
        }

    }
}
