using SaveSavings.Converters;
using SaveSavings.Helpers;
using SaveSavings.Model;
using SaveSavings.Persistance;
using System;

namespace SaveSavings.ViewModel
{


    public class EditRegularItemVM
    {
        public string Header { get; set; }
        public string HeaderColor { get; set; }

        public string EditButtonText { get; set; }

        public int RegularItemId { get; set; }


        public string Name { get { return m_Name; }
            set {
                m_Name = value;
                EditCommand.RaiseExecuteChanged();
            }
        }
        public string Amount { get { return m_Amount; } set { m_Amount = value; EditCommand.RaiseExecuteChanged(); } }
        public bool IsPerDay { get { return m_IsPerDay;  } set { m_IsPerDay = value; EditCommand.RaiseExecuteChanged(); } }
        public bool IsPerMonth { get { return m_IsPerMonth; } set { m_IsPerMonth = value; EditCommand.RaiseExecuteChanged(); } }
        public bool IsPerYear { get { return m_IsPerYear; } set { m_IsPerYear = value; EditCommand.RaiseExecuteChanged(); } }

        private string m_Name;
        private string m_Amount;
        private bool m_IsPerDay;
        private bool m_IsPerMonth;
        private bool m_IsPerYear;

        private bool m_IsIncome;




        public EditRegularItemVM()
        {

        }


        // TODO: use database ID for edit
        public EditRegularItemVM(bool _IsIncome, bool _IsAdd, int _RegularItemId)   // _RegularItemId could be NEW_REGULAR_ITEM_ID
        {
            m_IsIncome = _IsIncome;

            string action = _IsAdd ? "Add" : "Update";
            string direction = _IsIncome ? "Income" : "Expense";

            Header = string.Format("{0} {1}", action, direction);
            HeaderColor = _IsIncome ? "ForestGreen" : "DarkRed";

            EditButtonText = string.Format("{0} {1}", action, direction);

            if (_RegularItemId != RegularItemVM.NEW_REGULAR_ITEM_ID)
            {
                RegularItemId = _RegularItemId;

                RegularStorage db = new RegularStorage();
                RegularItem ri = db.GetRegular(_RegularItemId);

                this.Name = ri.Name;
                this.Amount = DataConversion.ConvertCentsToCurrency(Math.Abs(ri.Amount)).ToString();
                SetPeriod(ri.Period);
            }
            else
            {
                RegularItemId = RegularItemVM.NEW_REGULAR_ITEM_ID;

                Name = "";
                Amount = "";
                SetPeriod(REGULARS_PERIOD.YEARLY);
            }

        }




        public DelegateCommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new DelegateCommand( 
                        p => {

                            RegularStorage db = new RegularStorage();

                            int amount = DataConversion.ConvertCurrencyStringToIntegerCents(Amount);
                            if (m_IsIncome == false)
                            {
                                amount = -amount;   // expense is negative
                            }

                            REGULARS_PERIOD period = GetPeriod();

                            RegularItem ri = new RegularItem(RegularItemId, Name, period, amount);

                            if (RegularItemId != RegularItemVM.NEW_REGULAR_ITEM_ID)
                            {
                                // update
                                db.UpdateRegular(ri);
                            }
                            else
                            {
                                // add
                                db.InsertRegular(ri);
                            }

                            BackSpaceInvoked();

                        } , 
                        p => ((Name.Length > 0) && ( (Amount.Length > 0) && DataConversion.CanConvertCurrencyStringToIntegerCents(Amount) ))
                    );
                }
                return editCommand;
            }
        }





        public DelegateCommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new DelegateCommand(
                        p => {

                            RegularStorage db = new RegularStorage();

                            if (RegularItemId != RegularItemVM.NEW_REGULAR_ITEM_ID)
                            {
                                // update
                                db.DeleteRegular(RegularItemId);
                            }

                            BackSpaceInvoked();

                        },
                        p => (true)
                    );
                }
                return deleteCommand;
            }
        }





        private REGULARS_PERIOD GetPeriod()
        {
            if (IsPerDay)
            {
                return REGULARS_PERIOD.DAYLY;
            }
            else if (IsPerMonth)
            {
                return REGULARS_PERIOD.MONTHLY;
            }
            else
            {
                return REGULARS_PERIOD.YEARLY;
            }
        }




        private void SetPeriod(REGULARS_PERIOD _Period)
        {
            this.IsPerDay = false;
            this.IsPerMonth = false;
            this.IsPerYear = false;

            switch (_Period)
            {
                case REGULARS_PERIOD.DAYLY:
                    this.IsPerDay = true;
                    break;
                case REGULARS_PERIOD.MONTHLY:
                    this.IsPerMonth = true;
                    break;
                case REGULARS_PERIOD.YEARLY:
                    this.IsPerYear = true;
                    break;
            }
        }


        private void BackSpaceInvoked()
        {
            App.NavigationService.GoBack();
        }



        private DelegateCommand editCommand;
        private DelegateCommand deleteCommand;


    }
}
