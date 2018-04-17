using SaveSavings.Model;
using SaveSavings.ViewModel;
using System;
using System.Linq;

namespace SaveSavings.Persistance
{
    class UniqueExpensesStorage
    {
//        public int RegularIncomePerDay { get; set; }



        public UniqueExpensesStorage()
        {

            //// calculate income per day
            //using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            //{
            //    // 1) request SQL table data
            //    // RegularItem = model
            //    var regulars = conn.Table<RegularItem>().OrderBy(o => o.Amount);   // of type TableQuery<T>, BaseTableQuery, IEnumerable<T>, IEnumerable

            //    // 2) process each record

            //    float totalIncome = 0;
            //    float totalExpense = 0;

            //    RegularsVM regularsVM = new RegularsVM();
            //    foreach (RegularItem r in regulars)
            //    {
            //        int amount = r.Amount;
            //        if (amount > 0)
            //        {
            //            // positive = income
            //            RegularItemVM regularItemVM = new RegularItemVM(r.Id, r.Name, amount, r.Period);
            //            totalIncome += regularItemVM.GetDaily();

            //        }
            //        else
            //        {
            //            // negative = expense
            //            amount = -amount;   // use absolute value
            //            RegularItemVM regularItemVM = new RegularItemVM(r.Id, r.Name, amount, r.Period);
            //            totalExpense += regularItemVM.GetDaily();
            //        }
            //    }

            //    RegularIncomePerDay = (int)Math.Truncate(totalIncome * 100.0f) - (int)Math.Truncate(totalExpense * 100.0f);

            //}
        }



        // TODO: use stored cached data to create VM (possibly ?)
        internal UniqueExpensesVM GetAllUniqueExpenses()
        {
            UniqueExpensesVM outputUniqueExpenses = new UniqueExpensesVM();

            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    var dbTable = conn.Table<UniqueExpenseItem>();

                    var listOfUniqueExpenses = (from r in dbTable
                              orderby r.Date descending
                              select new UniqueExpenseItem(r.Id, r.Date, r.Amount, r.Name)
                              );

                    // build VM
                    int totalAmount = 0;

                    // 1) list
                    foreach (UniqueExpenseItem uei in listOfUniqueExpenses)
                    {
                        // eventually, store the current DB value
                        outputUniqueExpenses.AddExpense(new UniqueExpenseVM(uei.Id, uei.Date.ToLocalTime(), uei.Amount, uei.Name));

                        totalAmount += uei.Amount;
                    }

                    // 2) total
                    outputUniqueExpenses.SetTotal(totalAmount);


                }
            }
            catch
            {
            }

            // return built result
            return outputUniqueExpenses;
        }



        // Insert the new contact in the Contacts table.
        public void InsertUniqueExpense(UniqueExpenseItem _Item)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Insert(_Item);
                });
            }
        }



        // Insert the new contact in the Contacts table.
        public UniqueExpenseItem GetUniqueExpense(int _ItemID)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                UniqueExpenseItem regularItem = conn.Table<UniqueExpenseItem>().Where((v) => (v.Id == _ItemID)).FirstOrDefault();

                return regularItem;
            }
        }



        //Update existing conatct   
        public void UpdateUniqueExpense(UniqueExpenseItem _Item)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {
                conn.RunInTransaction(() =>
                {
                    conn.Update(_Item);
                });
            }
        }


        //Delete specific contact     
        public void DeleteUniqueExpense(int _ItemID)
        {
            using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
            {

                conn.RunInTransaction(() =>
                {
                    UniqueExpenseItem ri = new UniqueExpenseItem(_ItemID, DateTime.Now, 0, "");

                    conn.Delete(ri);
                });
            }
        }


        DatabaseHelperClass m_DBHelper = new DatabaseHelperClass();


    }
}
