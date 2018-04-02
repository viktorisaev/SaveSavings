using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsPage : Page
    {
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();

        Spends currentExpense = new Spends();



        public DetailsPage()
        {
            this.InitializeComponent();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // parameter as data
            currentExpense = e.Parameter as Spends;

            // fill data into widgets
            w_DateOfExpense.Date = new DateTimeOffset(currentExpense.Date);
            w_AmountOfExpense.Text = (currentExpense.Amount / 100.0f).ToString();
        }



        private void UpdateContact_Click(object sender, RoutedEventArgs e)
        {
            // parse widgets values to data
            currentExpense.Date = w_DateOfExpense.Date.DateTime;
            float amount = float.Parse(w_AmountOfExpense.Text);
            int valCents = (int)Math.Truncate(amount * 100.0f);
            currentExpense.Amount = valCents;

            // store data
            Db_Helper.UpdateDetails(currentExpense);//Update selected DB contact Id

            // interface transition
            Frame.Navigate(typeof(HomePage));
        }



        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Db_Helper.DeleteContact(currentExpense.Id);//Delete selected DB contact Id.
            Frame.Navigate(typeof(HomePage));
        }



    }   // class DetailsPage 
}
