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
            currentExpense = e.Parameter as Spends;
            //currentcontact = Db_Helper.ReadContact(Selected_ContactId);//Read selected DB contact
            NametxtBx.Text = currentExpense.Date.ToString();//get contact Name
            PhonetxtBx.Text = currentExpense.Amount.ToString();//get contact PhoneNumber
        }

        private void UpdateContact_Click(object sender, RoutedEventArgs e)
        {
//            currentStudent.Name = NametxtBx.Text;
            currentExpense.Amount = Int32.Parse(PhonetxtBx.Text);
            Db_Helper.UpdateDetails(currentExpense);//Update selected DB contact Id
            Frame.Navigate(typeof(HomePage));
        }
        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Db_Helper.DeleteContact(currentExpense.Id);//Delete selected DB contact Id.
            Frame.Navigate(typeof(HomePage));
        }

    }   // class DetailsPage 
}
