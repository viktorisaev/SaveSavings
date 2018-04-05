using SaveSavings.Helpers;
using System;

namespace SaveSavings.ViewModel
{
    public class EditRegularItemVM
    {
        public string Header { get; set; }
        public string HeaderColor { get; set; }

        public string EditButtonText { get; set; }

        public RegularItemVM RegularItem { get; set; }





        public EditRegularItemVM()
        {
        }



        // TODO: use database ID for edit
        public EditRegularItemVM(bool _IsIncome, bool _IsAdd, RegularItemVM _RegularItem)
        {
            Random rnd = new Random();

            string action = _IsAdd ? "Add" : "Update";
            string direction = _IsIncome ? "Income" : "Expense";

            Header = string.Format("{0} {1}", action, direction);
            EditButtonText = string.Format("{0} {1}", action, direction);

            HeaderColor = _IsIncome ? "ForestGreen" : "DarkRed";

            RegularItem = _RegularItem;   // and then binding
        }




        public DelegateCommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new DelegateCommand( p => this.BackSpaceInvoked() , p => true );
                }
                return editCommand;
            }
        }




        private void BackSpaceInvoked()
        {
            App.NavigationService.GoBack();
        }



        private DelegateCommand editCommand;


    }
}
