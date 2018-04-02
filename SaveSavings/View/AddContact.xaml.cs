using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings
{

    public sealed partial class AddContact : Page
    {
        public AddContact()
        {
            this.InitializeComponent();
        }


        private async void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            if (w_AmountOfExpense.Text != "")
            {
                try
                {
                    float amount = float.Parse(w_AmountOfExpense.Text);
                    int valCents = (int)Math.Truncate(amount * 100.0f);

                    Db_Helper.Insert(new Spends(w_DateOfExpense.Date.DateTime, valCents));
                    Frame.Navigate(typeof(HomePage));
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
