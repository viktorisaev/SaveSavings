using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using SaveSavings.ViewModel;
using Windows.UI.Core;
using SaveSavings.Persistance;
using SaveSavings.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegularsPage : Page
    {
        public RegularsPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RegularStorage dbcontacts = new RegularStorage();
            RegularsVM regularsVM = dbcontacts.GetRegulars();   //Get regulars

            // set data (ad execute bindings)
            this.DataContext = regularsVM;
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

        private void AddIncome_Clicked(object sender, RoutedEventArgs e)
        {
            GotoEditRegularPage(true, true, RegularItemVM.NEW_REGULAR_ITEM_ID);
        }

        private void AddExpense_Clicked(object sender, RoutedEventArgs e)
        {
            GotoEditRegularPage(false, true, RegularItemVM.NEW_REGULAR_ITEM_ID);
        }


        private void EditIncome_Clicked(object sender, RoutedEventArgs e)
        {
            if (w_IncomesList.SelectedIndex != -1)
            {
                RegularItemVM riVM = w_IncomesList.SelectedItem as RegularItemVM;   //Get selected item from the list
                GotoEditRegularPage(true, false, riVM.Id);
            }
        }


        private void EditExpense_Clicked(object sender, RoutedEventArgs e)
        {
            if (w_ExpensesList.SelectedIndex != -1)
            {
                RegularItemVM riVM = w_ExpensesList.SelectedItem as RegularItemVM;   //Get selected item from the list
                GotoEditRegularPage(false, false, riVM.Id);
            }
        }


        private void GotoEditRegularPage(bool _IsIncome, bool _IsAdd, int _RegularItemId)
        {
            // create VM
            EditRegularItemVM editRegularItem = new EditRegularItemVM(_IsIncome, _IsAdd, _RegularItemId);

            // jump to the EditRegular page
            this.Frame.Navigate(typeof(EditRegular), editRegularItem);
        }

    }
}
