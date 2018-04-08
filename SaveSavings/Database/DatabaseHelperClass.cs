using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SaveSavings.Converters;
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
                    conn.CreateTable<ExpenseItem>();
                    conn.CreateTable<RegularItem>();
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
        public void Insert(ExpenseItem objContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(objContact);
                });
            }
        }




        //Update existing conatct   
        public void UpdateDetails(ExpenseItem ObjContact)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                //var existingconact = conn.Query<ExpenseItem>("select * from ExpenseItem where Id =" + ObjContact.Id).FirstOrDefault();
                //if (existingconact != null)
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

                conn.DropTable<ExpenseItem>();
                conn.CreateTable<ExpenseItem>();
                conn.Dispose();
                conn.Close();

            }
        }

        //Delete specific contact     
        public void DeleteContact(int Id)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                var existingconact = conn.Query<ExpenseItem>("select * from ExpenseItem where Id =" + Id).FirstOrDefault();
                if (existingconact != null)
                {
                    conn.RunInTransaction(() =>
                    {
                        conn.Delete(existingconact);
                    });
                }
            }
        }




        public List<ExpenseItem> GetAllExpenses()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    List<ExpenseItem> myCollection = conn.Table<ExpenseItem>().ToList<ExpenseItem>();

                    foreach (ExpenseItem s in myCollection)
                    {
                        s.Date = s.Date.ToLocalTime().Date;
                    }


                    var tt = (from r in myCollection
                              orderby r.Date
                              group r by r.Date into g
                              select new ExpenseItem { Id = 0, Date = g.Key, Amount = g.Sum((t) => (t.Amount)) }
                              );

                    return tt.ToList<ExpenseItem>();
                }
            }
            catch
            {
                return null;
            }
        }




        public int GetAverageExpense()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    var allExpenses = conn.Table<ExpenseItem>();

                    var expenses = (from r in allExpenses
                                    group r by r.Date into g
                                    select new {Amount = g.Sum((t) => (t.Amount)) }
                                    );

                    float avgCurrency = (float)expenses.Average( o => o.Amount) / 100.0f;

                    int avg = DataConversion.ConvertCurrencyToCents(avgCurrency);

                    return avg;
                }
            }
            catch
            {
                return 0;   // error!!
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
                    stats.m_TotalExpenses = conn.Table<ExpenseItem>().Sum(v => v.Amount);
                }
            }
            catch
            {
            }

            return stats;
        }



        internal List<ExpenseItem> GetAmountsForDate(DateTime date)
        {
            // extract records for 'date'
            //            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    DateTime dateOnlyFrom = date.Date;
                    DateTime dateOnlyTo = date.AddDays(1).Date;

                    //                    Expression<Func<Spends, bool>> myExpression = ;

                    List<ExpenseItem> myCollection = conn.Table<ExpenseItem>().Where((v) => ((v.Date >= dateOnlyFrom) && (v.Date < dateOnlyTo))).ToList<ExpenseItem>();

                    foreach (ExpenseItem s in myCollection)
                    {
                        s.Date = s.Date.ToLocalTime().Date;
                    }

                    return myCollection;
                }
            }
            //catch
            //{
            //    return null;
            //}
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


    }
}
