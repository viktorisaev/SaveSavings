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
        }


        public int GetAverageIncomePerDay()
        {
            return m_RegularStorage.RegularIncomePerDay;
        }

        RegularStorage m_RegularStorage;
        ExpensesStorage m_ExpensesStorage;


    }
}
