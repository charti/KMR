namespace KMR_test
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;
    using KMR.Model;
    using KMR.Common;

    [TestFixture]
    public class CalculationTest
    {
        Calculator Calc = new KMR_test.Calculator();
        
        [Test]
        public void ExistingSubstance()
        {
            var res = Calc.Validate(CalcInputs.ExistingSubstance, CalcOption.M2, "10000000");
            Assert.That(!res.Success && res.ErrorMsg.Equals(
                "Die vorhande Substanz darf 100.000 € nicht übersteigen."));
            Calc.Validate(CalcInputs.ExistingSubstance, CalcOption.M2, "1000,00");
            Assert.That(Calc.ExistingSubstance == 1000);

            Calc.Validate(CalcInputs.ExistingSubstanceBorrow, CalcOption.Percent, "50");
            Assert.That(Calc.ExistingSubstanceBorrowPerc == 50);
            Calc.Validate(CalcInputs.ExistingSubstanceBorrow, CalcOption.M2, "600");
            Assert.That(Calc.ExistingSubstanceBorrowPerc == 60);
            Assert.That(Calc.ExistingSubstanceOwnPerc == 40);

            Calc.Validate(CalcInputs.SubstanceAnnuityInterest, CalcOption.Percent, "4,8");
            Assert.That(Calc.SubstanceAnnuityInterestPerc == 4.8);

            var frontEnd = Calc.GetFrontendStrings();
        }
    }

    public class Calculator
    {
        public double ExistingSubstance { get; private set; }
        public double ExistingSubstanceBorrowPerc { get; private set; }
        public double ExistingSubstanceBorrowVal
        {
            get { return ExistingSubstanceBorrowPerc * ExistingSubstance / 100; }
            private set { ExistingSubstanceBorrowPerc = value / ExistingSubstance * 100; }            
        }
        public double ExistingSubstanceOwnPerc { get { return 100 - ExistingSubstanceBorrowPerc; } }
        public double ExistingSubstanceOwnVal { get { return ExistingSubstanceOwnPerc * ExistingSubstance / 100; } }
        public double SubstanceAnnuityInterestPerc { get; private set; }


        public ValidationResult Validate(CalcInputs type, CalcOption option, string value)
        {
            double val;
            if (!Double.TryParse(value, out val))
                return new ValidationResult(false, "Eingabefehler",
                    "Bitte verwenden Sie für die Eingabe nur Zahlen.");

            ValidationResult ret = null;

            switch (type)
            {
                case CalcInputs.ExistingSubstance:
                    if (val > 100000)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die vorhande Substanz darf 100.000 € nicht übersteigen.");
                    else if (val <= 0)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Bitte geben Sie einen Wert größer als 0 ein.");
                    break;
                case CalcInputs.ExistingSubstanceBorrow:
                    if (option == CalcOption.M2 && val > ExistingSubstance)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Der Anteil des Fremdkapitals darf das Gesamtkapital nicht übersteigen.");
                    else if (option == CalcOption.Percent && (val > 100 || val < 0))
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eingabe muss größer als 0 und kleiner/ gleich 100 sein.");
                    break;
                case CalcInputs.SubstanceAnnuityInterest:
                    if (val < 0 || val > 100)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eingabe muss größer als 0 und kleiner/ gleich 100 sein.");
                    break;
                default:
                    throw new ArgumentException($"{type} isn't implemented yet for validation.");
            }

            if (ret == null)
                Calculate(type, option, val);

            return ret ?? new ValidationResult(true);
        }

        private void Calculate(CalcInputs type, CalcOption option, double value)
        {
            switch (type)
            {
                case CalcInputs.ExistingSubstance:
                    ExistingSubstance = value;
                    break;
                case CalcInputs.ExistingSubstanceBorrow:
                    if (option == CalcOption.Percent)
                        ExistingSubstanceBorrowPerc = value;
                    else if (option == CalcOption.M2)
                        ExistingSubstanceBorrowVal = value;
                    break;
                case CalcInputs.SubstanceAnnuityInterest:
                    SubstanceAnnuityInterestPerc = value;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void CalculateAnnuity()
        {

        }

        public Dictionary<string, string> GetFrontendStrings()
        {
            return new Dictionary<string, string>()
            {
                {"ExistingSubstancePerc", "100,0 %"},
                {"ExistingSubstanceVal", ExistingSubstance.ToString("0.00 €")},
                {"ExistingSubstanceBorrowPerc", ExistingSubstanceBorrowPerc.ToString()},
                {"ExistingSubstanceBorrowVal", ExistingSubstanceBorrowVal.ToString("0.00 €")},
                {"ExistingSubstanceOwnPerc", ""},
                {"ExistingSubstanceOwnVal",  ""},
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
