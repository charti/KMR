using System;
using System.Collections.Generic;

namespace KMR.Model
{
    class Calculator
    {
        public Calculator()
        {
            CalcService = new CalculationService();
            Values = new Dictionary<string, double>()
            {
                {"ExistingSubstancePerc",       100},   {"ExistingSubstanceVal",        100},
                {"ExistingSubstanceBorrowPerc", 50},    {"ExistingSubstanceBorrowVal",   50},
                {"ExistingSubstanceOwnPerc",    -1},    {"ExistingSubstanceOwnVal",     -1},
                {"SubstanceAnnuityPerc",        -1},    {"SubstanceAnnuityVal",         -1},
                {"SubstanceAnnuityInterestPerc",1},     {"SubstanceAnnuityInterestVal", -1},
                {"SubstanceAnnuityRepayPerc",   4.8},   {"SubstanceAnnuityRepayVal",    -1},
                {"SubstanceOwnPerc",            -1},    {"SubstanceOwnVal",             -1},
                {"SubstanceOwnInterestPerc",    4},     {"SubstanceOwnInterestVal",     -1},
                {"SubstanceOwnReservePerc",     2},     {"SubstanceOwnReserveVal",      -1},

                {"InvestmentPerc",              100},   {"InvestmentVal",               100},
                {"InvestmentBorrowPerc",        50},    {"InvestmentBorrowVal",         -1},
                {"InvestmentOwnPerc",           -1},    {"InvestmentOwnVal",            -1},
                {"InvestAnnuityPerc",           -1},    {"InvestAnnuityVal",            -1},
                {"InvestAnnuityInterestPerc",   1},     {"InvestAnnuityInterestVal",    -1}, 
                {"InvestAnnuityRepayPerc",      4.8},   {"InvestAnnuityRepayVal",       -1},
                {"InvestOwnPerc",               -1},    {"InvestOwnVal",                -1},
                {"InvestOwnInterestPerc",       4},     {"InvestOwnInterestVal",        -1},
                {"InvestOwnReservePerc",        2},     {"InvestOwnReserveVal",         -1},

                {"RentLossPerc",                2},     {"RentLossVal",                 -1},
                {"MaintenancePerc",             1},     {"MaintenanceVal",              -1},
                {"AdministrationPerFlat",       708},   {"AdministrationPerMeter",      -1},
                {"AvarageFlatSize",             56},

                {"DepreciationPerc",            -1},    {"DepreciationVal",             -1},
                {"HgbPerc",                     0},     {"HgbVal",                      -1},

                {"RentalCosts",                 -1},
                {"SubstanceBorrowRepayLength",  -1},
                {"SubstanceOwnReserveLength",   -1},
                {"InvestBorrowRepayLength",     -1},
                {"InvestOwnReserveLength",      -1}
            };
            Operations = new Dictionary<string, Action>()
            {
                {"ExistingSubstance",   new Action(CalcExistingSubstance)},   
                {"Investment",          new Action(CalcInvestment)},
            };

            foreach (var op in Operations)
            {
                try
                {
                    op.Value.Invoke();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void CalcExistingSubstance()
        {
            Values["ExistingSubstanceBorrowVal"] =
                Values["ExistingSubstanceBorrowPerc"] * Values["ExistingSubstanceVal"] / 100;
            Values["ExistingSubstanceOwnPerc"] =
                Values["ExistingSubstancePerc"] - Values["ExistingSubstanceBorrowPerc"];
            Values["ExistingSubstanceOwnVal"] =
                Values["ExistingSubstanceOwnPerc"] / 100 * Values["ExistingSubstanceVal"];
        }
        private void CalcInvestment()
        {
            Values["InvestmentBorrowVal"] =
                Values["InvestmentBorrowPerc"] * Values["InvestmentVal"] / 100;
            Values["InvestmentOwnPerc"] =
                Values["InvestmentPerc"] - Values["InvestmentBorrowPerc"];
            Values["InvestmentOwnVal"] =
                Values["InvestmentOwnPerc"] / 100 * Values["InvestmentVal"];
        }

        #region Properties

        public CalculationService CalcService { get; private set; }
        public Dictionary<string, Action> Operations { get; private set; }
        public Dictionary<string, double> Values { get; private set; }


        #endregion
    }

}
