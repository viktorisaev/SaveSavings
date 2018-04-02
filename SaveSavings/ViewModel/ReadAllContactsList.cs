using System;
using System.Collections.ObjectModel;

namespace SaveSavings
{
    public class ReadAllContactsList
    {
        DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
        public ObservableCollection<Spends> GetAllContacts()
        {
            return Db_Helper.ReadAllContacts();
        }

        internal ObservableCollection<Spends> GetDateContacts(DateTime date)
        {
            return Db_Helper.GetDateContacts(date);
        }
    }
}
