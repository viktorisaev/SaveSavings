using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings.Persistance
{

    // store global per-app data like
    // 1) income per day
    // 2) expense per day
    public class GlobalPersistenceService
    {

        public GlobalPersistenceService()
        {
            m_RegularStorage = new RegularStorage();    // should be initialized first because of average income data is used in expenses
            m_ExpensesStorage = new ExpensesStorage();
            m_UniqueExpensesStorage = new UniqueExpensesStorage(new DateTime(2018, 03, 29));
        }


        public UniqueExpensesStorage GetUniqueExpensesStorage()
        {
            return m_UniqueExpensesStorage;
        }

        public ExpensesStorage GetExpensesStorage()
        {
            return m_ExpensesStorage;
        }


        public int GetAverageIncomePerDay()
        {
            return m_RegularStorage.RegularIncomePerDay;
        }

        public int GetAverageSavingsPerDay()
        {
            return GetAverageIncomePerDay() - m_ExpensesStorage.AverageExpense;
        }


        public int GetTotalUniqueAmount()
        {
            return m_UniqueExpensesStorage.TotalUniqueAmountSinceFixedDate;
        }




        private RegularStorage m_RegularStorage;
        private ExpensesStorage m_ExpensesStorage;
        private UniqueExpensesStorage m_UniqueExpensesStorage;

    }
}
