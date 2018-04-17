using SaveSavings.Converters;
using SaveSavings.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SaveSavings.ViewModel
{
    public class UniqueExpensesVM   // list of unique exppenses
    {
        public float TotalAmount { get { return DataConversion.ConvertCentsToCurrency(m_TotalAmount); } }
        public Brush TotalAmountColor { get; set; }

        public ObservableCollection<UniqueExpenseVM> ExpensesList { get { return this.m_UniqueExpensesList; } }


        public void SetTotal(int _totalExpense)
        {
            m_TotalAmount = Math.Abs(_totalExpense);
            this.TotalAmountColor = ViewHelpers.GetColorByAmount(_totalExpense);
        }

        public void AddExpense(UniqueExpenseVM _UniqueExpense)
        {
            m_UniqueExpensesList.Add(_UniqueExpense);
        }


        // shadow data fields
        private ObservableCollection<UniqueExpenseVM> m_UniqueExpensesList = new ObservableCollection<UniqueExpenseVM>();

        private int m_TotalAmount;    // keep it int to do proper calculations
    }
}
