using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SaveSavings.Model;
using SaveSavings.ViewModel;


namespace SaveSavings
{
    public class DataFromStorage
    {
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();


        public ObservableCollection<Spends> GetAllContacts()
        {
            return Db_Helper.ReadAllContacts();
        }


        internal ObservableCollection<Spends> GetDateContacts(DateTime date)
        {
            return Db_Helper.GetAmountsForDate(date);
        }


        string[] StubNames =
        {
            "Зарплата",
            "BahnCard",
            "ARD",
            "DJH Deutschland",
            "Sicherung",
            "Хлебушек"
        };



        internal RegularsVM GetRegulars()
        {
            RegularsVM regularsVM = Db_Helper.GetRegulars();

            // TODO: this is stub data
            regularsVM.TotalIncome = 6564;
            regularsVM.TotalSpends = 2545;
            regularsVM.TotalDailyIncome = regularsVM.TotalIncome - regularsVM.TotalSpends;

            Random rnd = new Random();

            List<RegularItemVM> lst = new List<RegularItemVM>();
            for (int i = 0, ei = rnd.Next(3,10); i < ei; ++i)
            {
                lst.Add(new RegularItemVM() {
                    Amount=rnd.Next(20, 200000),
                    Name = StubNames[rnd.Next(0, StubNames.Length)],
                    PerPeriod =rnd.Next(0,2) == 0 ? "per year" : "per month"
                });
            }
            regularsVM.SetIncomes(lst);

            lst = new List<RegularItemVM>();
            for (int i = 0, ei = rnd.Next(3, 10); i < ei; ++i)
            {
                lst.Add(new RegularItemVM()
                {
                    Amount = rnd.Next(20, 4000),
                    Name = StubNames[rnd.Next(0, StubNames.Length)],
                    PerPeriod = rnd.Next(0, 2) == 0 ? "per year" : "per month"
                });
            }
            regularsVM.SetSpends(lst);


            return regularsVM;
        }


    }   // class DatabaseStorage

}   // namespace SaveSavings
