﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSavings.ViewModel
{
    public class AllExpensesVM
    {
        public OverallVM Overall { get; set; }
        public ExpensesVM Expenses { get; set; }
        public UniqueExpensesVM UniqueExpenses { get; set; }
    }
}
