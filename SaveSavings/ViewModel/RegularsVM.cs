using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Amount { get; internal set; }
        public string PerPeriod { get; internal set; }
    }


    // full page VM
    public class RegularsVM
    {
        public int TotalDailyIncome { get; internal set; }    // positive :) Could be negative, but then tha app doesn't make any ssense

        public int TotalIncome { get; internal set; } // income only
        public int TotalSpends { get; internal set; } // spends only

        public int TotalSpends22 { get; internal set; } // spends only

        public ObservableCollection<RegularItemVM> Incomes { get { return this.m_Incomes; } }
        public ObservableCollection<RegularItemVM> Spends { get { return this.m_Spends; } }


        public void SetIncomes(List<RegularItemVM> _Incomes)
        {
            m_Incomes = new ObservableCollection<RegularItemVM>();

            foreach (RegularItemVM item in _Incomes)
            {

                m_Incomes.Add(item);
            }
        }

        public void SetSpends(List<RegularItemVM> _Spends)
        {
            m_Spends = new ObservableCollection<RegularItemVM>();

            foreach (RegularItemVM item in _Spends)
            {

                m_Spends.Add(item);
            }
        }


        // shadow fields
        private ObservableCollection<RegularItemVM> m_Incomes;
        private ObservableCollection<RegularItemVM> m_Spends;

    }

}
