using SaveSavings.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings.ViewModel
{
    public class ExpensesVM // for HomePage
    {
        public float AverageDailySave { get { return DataConversion.ConvertCentsToCurrency(m_AverageSavePerDay); } }    // positive :) Could be negative, but then the app doesn't make any sense
        public float AverageDailyIncome { get { return DataConversion.ConvertCentsToCurrency(m_AverageIncomePerDay); } }    // positive :) Could be negative, but then the app doesn't make any sense
        public float AverageDailyExpense { get { return DataConversion.ConvertCentsToCurrency(m_AverageExpensePerDay); } }    // positive :) Could be negative, but then the app doesn't make any sense

        public ObservableCollection<ExpenseVM> ExpensesList { get { return this.m_ExpensesList; } }


        public void SetAverages(int _expense, int _income, int _save)
        {
            m_AverageExpensePerDay = _expense;
            m_AverageIncomePerDay = _income;
            m_AverageSavePerDay = _save;
        }

        public void AddExpense(ExpenseVM _Expense)
        {
                m_ExpensesList.Add(_Expense);
        }

        // shadow data fields
        private ObservableCollection<ExpenseVM> m_ExpensesList = new ObservableCollection<ExpenseVM>();

        private int m_AverageExpensePerDay;    // keep it int to do proper calculations
        private int m_AverageIncomePerDay;    // keep it int to do proper calculations
        private int m_AverageSavePerDay;    // keep it int to do proper calculations
    }
}
