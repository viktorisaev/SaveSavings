using SaveSavings.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings.ViewModel
{
    public class OverallVM
    {
        public float TotalSaved { get { return DataConversion.ConvertCentsToCurrency(m_TotalSaved); } }
        public float SavedToday { get { return DataConversion.ConvertCentsToCurrency(m_SavedToday); } }

        public string DateSaved { get; set; }



        public OverallVM(DateTime _DateStarted, DateTime _DateToday, DateTime _DateSaved, int _AverageSavingsPerDay, DateTime _DateFixedAmount, int _FixedAmount )
        {
            DateSaved = _DateSaved.ToString("d");

            int nTotalDays = (_DateSaved - _DateFixedAmount).Days;
            m_TotalSaved = _FixedAmount + (nTotalDays * _AverageSavingsPerDay) - App.GlobalPersistanceService.GetTotalUniqueAmount();

            int nUpToTodayDays = (_DateToday - _DateFixedAmount).Days;
            m_SavedToday = _FixedAmount + (nUpToTodayDays * _AverageSavingsPerDay) - App.GlobalPersistanceService.GetTotalUniqueAmount();
        }


        private int m_TotalSaved;
        private int m_SavedToday;
    }
}
