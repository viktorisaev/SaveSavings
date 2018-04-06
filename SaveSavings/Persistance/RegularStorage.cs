using SaveSavings.Converters;
using SaveSavings.Model;
using SaveSavings.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace SaveSavings.Persistance   // this is MODEL, not ViewModel
{
    public class RegularStorage
    {

        // create (generate, load, calculate) RegularsVM for the current global regular incomes/expenses
        public RegularsVM GetRegulars()
        {
            try
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), App.DB_PATH))
                {
                    // 1) request SQL table data
                    // RegularItem = model
                    var regulars = conn.Table<RegularItem>();   // of type TableQuery<T>, BaseTableQuery, IEnumerable<T>, IEnumerable

                    // 2) process each record

                    int totalIncome = 0;
                    int totalExpense = 0;

                    RegularsVM regularsVM = new RegularsVM();
                    foreach (RegularItem r in regulars)
                    {
                        int amount = r.Amount;
                        if (amount > 0)
                        {
                            // positive = income
                            RegularItemVM regularItemVM = new RegularItemVM(r.Name, amount, r.Period);
                            totalIncome += amount;
                            regularsVM.AddIncome(regularItemVM);

                        }
                        else
                        {
                            // negative = expense
                            amount = -amount;   // use absolute value
                            RegularItemVM regularItemVM = new RegularItemVM(r.Name, amount, r.Period);
                            totalExpense += amount;
                            regularsVM.AddExpense(regularItemVM);
                        }
                    }

                    // 3) fill total values
                    regularsVM.SetTotalIncomeAndExpense(totalIncome, totalExpense);

                    // 4) return output VM
                    return regularsVM;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
