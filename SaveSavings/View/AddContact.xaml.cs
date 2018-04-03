using SaveSavings.ViewModel;
using System;
using System.Globalization;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{

    public sealed partial class AddContact : Page
    {
        public AddContact()
        {
            this.InitializeComponent();
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



    }
}
