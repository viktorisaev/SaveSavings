using SaveSavings.Converters;
using SaveSavings.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;



namespace SaveSavings.ViewModel
{
    // VM for Regulars page:
    // 1) total daily income
    // 2) list of incomes
    // 3) income total
    // 4) list of spends
    // 5) spend total

    // single item
    public class RegularItemVM
    {
        public string Name { get; internal set; }
        public float Amount { get; internal set; }
        public string PerPeriod { get; internal set; }
        public string PerPeriodIndex { get; internal set; }

        private REGULARS_PERIOD m_Period;   // used to calculate daily

        public RegularItemVM(string _Name, int _Amount, REGULARS_PERIOD _Period)
        {
            this.Name = _Name;
            this.Amount = DataConversion.ConvertCentsToCurrency(_Amount);
            m_Period = _Period;

            switch (_Period)
            {
                case REGULARS_PERIOD.DAYLY:
                    this.PerPeriod = "per day";
                    this.PerPeriodIndex = "0";
                    break;
                case REGULARS_PERIOD.MONTHLY:
                    this.PerPeriod = "per month";
                    this.PerPeriodIndex = "1";
                    break;
                case REGULARS_PERIOD.YEARLY:
                    this.PerPeriod = "per year";
                    this.PerPeriodIndex = "2";
                    break;
            }
        }


        public float GetDaily() // returns daily amount depends on period
        {
            float daily = 0.0f;
            switch (m_Period)
            {
                case REGULARS_PERIOD.DAYLY:
                    daily = Amount;
                    break;
                case REGULARS_PERIOD.MONTHLY:
                    daily = Amount / 30.0f;
                    break;
                case REGULARS_PERIOD.YEARLY:
                    daily = Amount / 365.0f;
                    break;
            }

            return daily;
        }

    }   // class RegularItemVM







    // full page VM
    public class RegularsVM
    {
        public float TotalDailyIncome { get { return DataConversion.ConvertCentsToCurrency(totalDailyIncome); } }    // positive :) Could be negative, but then the app doesn't make any sense

        public float TotalIncome { get { return DataConversion.ConvertCentsToCurrency(totalIncomePerDay); } }       // incomes only
        public float TotalExpense { get { return DataConversion.ConvertCentsToCurrency(totalExpensePerDay); } }     // expenses only

        public ObservableCollection<RegularItemVM> Incomes { get { return this.m_Incomes; } }
        public ObservableCollection<RegularItemVM> Expenses { get { return this.m_Expenses; } }


        public Visibility ShowIncomesList { get { return GetVisibility(m_Incomes.Count > 0); } }
        public Visibility ShowExpensesList { get { return GetVisibility(m_Expenses.Count > 0); } }
        public Visibility ShowIncomesAddButton { get { return GetVisibility(m_Incomes.Count == 0); } }
        public Visibility ShowExpensesAddButton { get { return GetVisibility(m_Expenses.Count == 0); } }


        private Visibility GetVisibility(bool _IsVisible)
        {
            return _IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }



        public void SetTotalIncomeAndExpense(int _Income, int _Expense)
        {
            totalIncomePerDay = _Income;
            totalExpensePerDay = _Expense;
            totalDailyIncome = totalIncomePerDay - totalExpensePerDay;
        }

        public void AddIncome(RegularItemVM _Income)
        {
            m_Incomes.Add(_Income);
        }


        public void AddExpense(RegularItemVM _Income)
        {
            m_Expenses.Add(_Income);
        }


        public void SetIncomes(List<RegularItemVM> _Incomes)
        {
            foreach (RegularItemVM item in _Incomes)
            {
                m_Incomes.Add(item);
            }
        }

        public void SetSpends(List<RegularItemVM> _Spends)
        {
            foreach (RegularItemVM item in _Spends)
            {
                m_Expenses.Add(item);
            }
        }




        // shadow fields
        private ObservableCollection<RegularItemVM> m_Incomes = new ObservableCollection<RegularItemVM>();
        private ObservableCollection<RegularItemVM> m_Expenses = new ObservableCollection<RegularItemVM>();

        private int totalIncomePerDay;    // keep it int to do proper calculations
        private int totalExpensePerDay;
        private int totalDailyIncome;

    }

}
