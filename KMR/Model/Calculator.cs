using System;
using System.Collections.Generic;

using KMR.Common;

namespace KMR.Model
{
    public class ValidationResult
    {
        public bool Success { get; private set; }
        public string ErrorCaption { get; private set; }
        public string ErrorMsg { get; private set; }

        public ValidationResult(bool success, string errorCaption = "",
            string errorMsg = "")
        {
            Success = success;
            ErrorCaption = errorCaption;
            ErrorMsg = errorMsg;
        }
    }

    public class Calculator
    {
        public double ExcistingSubstance { get; private set; }
        public double ExistingSubstanceBorrowPerc { get; private set; }

        private CalculationService CalcService { get; set; }

        public Calculator()
        {
            CalcService = new CalculationService(this);
        }
        
        public ValidationResult Validate(CalcInputs type, CalcOption option, string value)
        {
            double val;
            if (!Double.TryParse(value, out val))
                return new ValidationResult(false, "Eingabefehler",
                    "Bitte verwenden Sie für die Eingabe nur Zahlen.");

            ValidationResult ret = null;

            switch (type)
            {
                case CalcInputs.ExcistingSubstance:
                    if (val > 100000)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die vorhande Substanz darf 100.000 € nicht übersteigen.");
                    else if (val <= 0)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Bitte geben Sie einen Wert größer als 0 ein.");

                    Calculate(type, option, val);

                    break;
                case CalcInputs.ExcistingSubstanceBorrow:

                    break;
                default:
                    throw new ArgumentException("{type} " + "isnt implemented yet for validation.");
            }

            return ret ?? new ValidationResult(true);
        }

        private void Calculate(CalcInputs type, CalcOption option, double value)
        {
            switch (type)
            {
                case CalcInputs.ExcistingSubstance:
                    ExcistingSubstance = value;
                    break;
                case CalcInputs.ExcistingSubstanceBorrow:
                    if (option == CalcOption.Percent)
                        ExistingSubstanceBorrowPerc = value;
                    else if (option == CalcOption.m2)
                    {
                    }
                    break;
            }
        }

        public Dictionary<string, string> GetFrontEndStrings()
        {
            return new Dictionary<string, string>()
            {
                {"ExistingSubstancePerc",       String.Empty},   {"ExistingSubstanceVal",        String.Empty},
                {"ExistingSubstanceBorrowPerc", String.Empty},   {"ExistingSubstanceBorrowVal",  String.Empty},
                {"ExistingSubstanceOwnPerc",    String.Empty},   {"ExistingSubstanceOwnVal",     String.Empty},
                {"SubstanceAnnuityPerc",        String.Empty},   {"SubstanceAnnuityVal",         String.Empty},
                {"SubstanceAnnuityInterestPerc",String.Empty},   {"SubstanceAnnuityInterestVal", String.Empty},
                {"SubstanceAnnuityRepayPerc",   String.Empty},   {"SubstanceAnnuityRepayVal",    String.Empty},
                {"SubstanceOwnPerc",            String.Empty},   {"SubstanceOwnVal",             String.Empty},
                {"SubstanceOwnInterestPerc",    String.Empty},   {"SubstanceOwnInterestVal",     String.Empty},
                {"SubstanceOwnReservePerc",     String.Empty},   {"SubstanceOwnReserveVal",      String.Empty},

                {"InvestmentPerc",              String.Empty},   {"InvestmentVal",               String.Empty},
                {"InvestmentBorrowPerc",        String.Empty},   {"InvestmentBorrowVal",         String.Empty},
                {"InvestmentOwnPerc",           String.Empty},   {"InvestmentOwnVal",            String.Empty},
                {"InvestAnnuityPerc",           String.Empty},   {"InvestAnnuityVal",            String.Empty},
                {"InvestAnnuityInterestPerc",   String.Empty},   {"InvestAnnuityInterestVal",    String.Empty},
                {"InvestAnnuityRepayPerc",      String.Empty},   {"InvestAnnuityRepayVal",       String.Empty},
                {"InvestOwnPerc",               String.Empty},   {"InvestOwnVal",                String.Empty},
                {"InvestOwnInterestPerc",       String.Empty},   {"InvestOwnInterestVal",        String.Empty},
                {"InvestOwnReservePerc",        String.Empty},   {"InvestOwnReserveVal",         String.Empty},

                {"RentLossPerc",                String.Empty},   {"RentLossVal",                 String.Empty},
                {"MaintenancePerc",             String.Empty},   {"MaintenanceVal",              String.Empty},
                {"AdministrationPerFlat",       String.Empty},   {"AdministrationPerMeter",      String.Empty},
                {"AvarageFlatSize",             String.Empty},
                {"DepreciationPerc",            String.Empty},   {"DepreciationVal",             String.Empty},
                {"HgbPerc",                     String.Empty},   {"HgbVal",                      String.Empty},

                {"RentalCosts",                 String.Empty},
                {"SubstanceBorrowRepayLength",  String.Empty},
                {"SubstanceOwnReserveLength",   String.Empty},
                {"InvestBorrowRepayLength",     String.Empty},
                {"InvestOwnReserveLength",      String.Empty}

            };
        }
    }
}
