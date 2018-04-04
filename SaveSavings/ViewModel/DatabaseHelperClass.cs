using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using SaveSavings.Model;
using SaveSavings.ViewModel;

namespace SaveSavings
{


    class TotalStatistics
    {
        public int m_TotalExpenses;
    }



    class DatabaseHelperClass
    {
        //Create Tabble   
        public void CreateDatabase(string DB_PATH)
        {
            if (!CheckFileExists(DB_PATH).Result)
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    conn.CreateTable<Spends>();
                }
            }
        }



        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Insert the new contact in the Contacts table.   
        public void Insert(Spends objContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objContact);
                });
            }
        }

        // Retrieve the specific contact from the database.     
        //public Spends ReadContact(int contactid)
        //{
        //    using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
        //    {
        //        var existingconact = conn.Query<Spends>("select * from Contacts where Id =" + contactid).FirstOrDefault();
        //        return existingconact;
        //    }
        //}




        public ObservableCollection<Spends> ReadAllContacts()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<Spends> myCollection = conn.Table<Spends>().ToList<Spends>();

                    foreach(Spends s in myCollection)
                    {
                        s.Date = s.Date.ToLocalTime().Date;
                    }


                    var tt = (from r in myCollection
                              group r by r.Date into g
                              select new Spends { Date = g.Key, Amount = g.Sum( (t)=>(t.Amount) ) }
//                              select r
                              );

                    ObservableCollection<Spends> ContactsList = new ObservableCollection<Spends>(tt);
                    return ContactsList;
                }
            }
            catch
            {
                return null;
            }
        }




        public TotalStatistics GetTotalStatistics()
        {
            TotalStatistics stats = new TotalStatistics();

            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    // total spent
                    stats.m_TotalExpenses = conn.Table<Spends>().Sum( v => v.Amount);
                }
            }
            catch
            {
            }

            return stats;
        }



        internal ObservableCollection<Spends> GetAmountsForDate(DateTime date)
        {
            // extract records for 'date'
//            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    DateTime dateOnlyFrom = date.Date;
                    DateTime dateOnlyTo = date.AddDays(1).Date;

//                    Expression<Func<Spends, bool>> myExpression = ;

                    List<Spends> myCollection = conn.Table<Spends>().Where( (v) => ( (v.Date >= dateOnlyFrom) && (v.Date < dateOnlyTo) ) ).ToList<Spends>();

                    foreach (Spends s in myCollection)
                    {
                        s.Date = s.Date.ToLocalTime().Date;
                    }

                    ObservableCollection<Spends> ContactsList = new ObservableCollection<Spends>(myCollection);
                    return ContactsList;
                }
            }
            //catch
            //{
            //    return null;
            //}
        }



        //Update existing conatct   
        public void UpdateDetails(Spends ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Spends>("select * from Spends where Id =" + ObjContact.Id).FirstOrDefault();
                if (existingconact != null)
                {

                    conn.RunInTransaction(() =>
                    {
                        conn.Update(ObjContact);
                    });
                }

            }
        }

        //Delete all contactlist or delete Contacts table     
        public void DeleteAllContact()
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                conn.DropTable<Spends>();
                conn.CreateTable<Spends>();
                conn.Dispose();
                conn.Close();

            }
        }

        //Delete specific contact     
        public void DeleteContact(int Id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<Spends>("select * from Spends where Id =" + Id).FirstOrDefault();
                if (existingconact != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingconact);
                    });
                }
            }
        }



        public RegularsVM GetRegulars()
        {
            return new RegularsVM();
        }

    }
}
