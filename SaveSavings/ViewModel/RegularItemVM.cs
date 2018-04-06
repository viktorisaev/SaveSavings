using SaveSavings.Converters;
using SaveSavings.Model;
using SaveSavings.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings.ViewModel
{
    // single item
    public class RegularItemVM
    {
        public int Id { get; set; }
        public string Name { get; internal set; }
        public float Amount { get; internal set; }
        public string PerPeriod { get; internal set; }
        public string PerPeriodIndex { get; internal set; }

        private REGULARS_PERIOD m_Period;   // used to calculate daily

        public const int NEW_REGULAR_ITEM_ID = -1;  // use it to add new value

        // add new regular
        public RegularItemVM()
        {
            this.Id = NEW_REGULAR_ITEM_ID;
            this.Name = "";
            this.Amount = 0;
            InitializePeriod(REGULARS_PERIOD.YEARLY);
        }


        // DB access-constructor
        public RegularItemVM(int _Id)
        {
            RegularStorage db = new RegularStorage();
            RegularItem ri = db.GetRegular(_Id);

            InitializeRegularItem(ri.Id, ri.Name, ri.Amount, ri.Period);
        }



        public RegularItemVM(int _Id, string _Name, int _Amount, REGULARS_PERIOD _Period)
        {
            InitializeRegularItem(_Id, _Name, _Amount, _Period);
        }



        private void InitializeRegularItem(int _Id, string _Name, int _Amount, REGULARS_PERIOD _Period)
        {
            this.Id = _Id;
            this.Name = _Name;
            this.Amount = DataConversion.ConvertCentsToCurrency(_Amount);
            InitializePeriod(_Period);
        }

        private void InitializePeriod(REGULARS_PERIOD _Period)
        {
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
                    daily = Amount * 12.0f / 365.0f;
                    break;
                case REGULARS_PERIOD.YEARLY:
                    daily = Amount / 365.0f;
                    break;
            }

            return daily;
        }

    }   // class RegularItemVM
}
