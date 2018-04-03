using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SaveSavings.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        ObservableCollection<Spends> DB_ContactList = new ObservableCollection<Spends>();



        public HomePage()
        {
            this.InitializeComponent();
            this.Loaded += ReadContactList_Loaded;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }



        private void ReadContactList_Loaded(object sender, RoutedEventArgs e)
        {
            ReadAllContactsList dbcontacts = new ReadAllContactsList();
            DB_ContactList = dbcontacts.GetAllContacts();//Get all DB contacts  
            //if (DB_ContactList.Count > 0)
            //{
            //    btnDelete.IsEnabled = true;
            //}

            // set list data - recent dates first
            listBoxobj.ItemsSource = DB_ContactList.OrderByDescending(i => i.Date).ToList();
        }



        private void DateListItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                Spends listitem = listBoxobj.SelectedItem as Spends;//Get slected listbox item contact ID
                Frame.Navigate(typeof(DailyPage), listitem.Date);
            }
        }



//        private void DeleteAll_Click(object sender, RoutedEventArgs e)
//        {
//            DatabaseHelperClass delete = new DatabaseHelperClass();
//            delete.DeleteAllContact();//delete all DB contacts
//            DB_ContactList.Clear();//Clear collections
////            btnDelete.IsEnabled = false;
//            listBoxobj.ItemsSource = DB_ContactList;
//        }



        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddContact));
        }



    }   // class HomePage
}
