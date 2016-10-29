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
        public double ExistingSubstance { get; private set; } = 100;
        public double ExistingSubstanceBorrowPerc { get; private set; } = 50;
        public double ExistingSubstanceBorrowVal
        {
            get { return ExistingSubstanceBorrowPerc * ExistingSubstance / 100; }
            private set { ExistingSubstanceBorrowPerc = value / ExistingSubstance * 100; }
        }
        public double ExistingSubstanceOwnPerc { get { return 100 - ExistingSubstanceBorrowPerc; } }
        public double ExistingSubstanceOwnVal 
        {
            get { return ExistingSubstanceOwnPerc * ExistingSubstance / 100; }
        }
        public double ExistingAnnuityInterest { get; private set; } = 1;
        public double ExistingAnnuityRepay { get; private set; } = 4.8;
        public double ExistingOwnInterest { get; private set; } = 4;
        public double ExistingOwnReserve { get; private set; } = 2;
        public double InvestmentVal { get; private set; } = 1000;
        public double InvestmentBorrowPerc { get; private set; } = 50;
        public double InvestmentBorrowVal
        {
            get { return InvestmentBorrowPerc * InvestmentVal / 100; }
            set { InvestmentBorrowPerc = value / InvestmentVal * 100; }
        }
        public double InvestmentOwnPerc
        {
            get { return 100 - InvestmentBorrowPerc; }
        }
        public double InvestmentOwnVal
        {
            get { return InvestmentOwnPerc * InvestmentVal / 100; }
        }
        public double InvestmentAnnuityInterest { get; private set; } = 1;
        public double InvestmentAnnuityRepay { get; private set; } = 4.8;
        public double InvestmentOwnInterest { get; private set; } = 4;
        public double InvestmentOwnReserve { get; private set; } = 2;
        public double RentLossPerc { get; private set; } = 2;
        public double AnnuitySum => ExistingAnnuity.AnnuityMonthly + ExistingOwnAnnuity.InterestMonthly +
                    InvestAnnuity.AnnuityMonthly + InvestOwnAnnuity.InterestMonthly +
                    AdministrationPerFlatMonth + MaintanceMonthly + InvestOwnAnnuity.RepayMonthly +
                    ExistingOwnAnnuity.RepayMonthly;

        public double RentLossVal => RentLossPerc == 0 ? 0 : AnnuitySum / (100 - RentLossPerc) *
            RentLossPerc;

        public double AdministrationPerFlatAnual { get; private set; } = 708;
        public double AdministrationPerFlatMonth
        {
            get { return AdministrationPerFlatAnual / AvarageFlatSize / 12; }
        }
        public double MaintancePerc { get; private set; } = 1;
        public double MaintanceMonthly => (ExistingSubstance + InvestmentVal) * MaintancePerc / 100 / 12;
        public double AvarageFlatSize { get; private set; } = 56;

        public Annuity ExistingAnnuity { get; private set; }
        public Annuity InvestAnnuity { get; private set; }
        public Annuity ExistingOwnAnnuity { get; private set; }
        public Annuity InvestOwnAnnuity { get; private set; }
        public double RentCost => AnnuitySum + RentLossVal;

        public Calculator()
        {
            CalculateAnnuities();
        }

        public Calculator(string[] data)
        {
            ExistingSubstance = Double.Parse(data[0]);
            ExistingSubstanceBorrowPerc = Double.Parse(data[1]);
            ExistingAnnuityInterest = Double.Parse(data[2]);
            ExistingAnnuityRepay = Double.Parse(data[3]);
            ExistingOwnInterest = Double.Parse(data[4]);
            ExistingOwnReserve = Double.Parse(data[5]);
            InvestmentVal = Double.Parse(data[6]);
            InvestmentBorrowPerc = Double.Parse(data[7]);
            InvestmentAnnuityInterest = Double.Parse(data[8]);
            InvestmentAnnuityRepay = Double.Parse(data[9]);
            InvestmentOwnInterest = Double.Parse(data[10]);
            InvestmentOwnReserve = Double.Parse(data[11]);
            RentLossPerc = Double.Parse(data[12]);
            AdministrationPerFlatAnual = Double.Parse(data[13]);
            MaintancePerc = Double.Parse(data[14]);
            AvarageFlatSize = Double.Parse(data[15]);

            CalculateAnnuities();
        }

        public string Save()
        {
            return $@"{ExistingSubstance}
{ExistingSubstanceBorrowPerc}
{ExistingAnnuityInterest}
{ExistingAnnuityRepay}
{ExistingOwnInterest}
{ExistingOwnReserve}
{InvestmentVal}
{InvestmentBorrowPerc}
{InvestmentAnnuityInterest}
{InvestmentAnnuityRepay}
{InvestmentOwnInterest}
{InvestmentOwnReserve}
{RentLossPerc}
{AdministrationPerFlatAnual}
{MaintancePerc}
{AvarageFlatSize}";
        }

        public Dictionary<string, string> GetFrontEndStrings()
        {
            return new Dictionary<string, string>()
            {
                {"ExistingSubstancePerc", 100.ToString("0.00 '%'")},
                {"ExistingSubstanceVal", ExistingSubstance.ToString("#,##0.00 €/m²")},
                {"ExistingSubstanceBorrowPerc", ExistingSubstanceBorrowPerc.ToString("0.00 '%'")},
                {"ExistingSubstanceBorrowVal", ExistingSubstanceBorrowVal.ToString("#,##0.00 €/m²")},
                {"ExistingSubstanceOwnPerc", ExistingSubstanceOwnPerc.ToString("0.00 '%'")},
                {"ExistingSubstanceOwnVal",  ExistingSubstanceOwnVal.ToString("#,##0.00 €/m²")},
                {"SubstanceAnnuityPerc", ExistingAnnuity.SumPerc.ToString("0.00 '%'")},
                {"SubstanceAnnuityVal", ExistingAnnuity.SumVal.ToString("#,##0.00 €/m²")},
                {"SubstanceAnnuityInterestPerc", ExistingAnnuityInterest.ToString("0.00 '%'") },
                {"SubstanceAnnuityInterestVal", ExistingAnnuity.InterestMonthly.ToString("#,##0.00 €/m²")},
                {"SubstanceAnnuityRepayPerc", ExistingAnnuityRepay.ToString("0.00 '%'") },
                {"SubstanceAnnuityRepayVal", ExistingAnnuity.RepayMonthly.ToString("#,##0.00 €/m²")},
                {"SubstanceOwnPerc", ExistingOwnAnnuity.SumPerc.ToString("0.00 '%'")},
                {"SubstanceOwnVal", ExistingOwnAnnuity.SumVal.ToString("#,##0.00 €/m²")},
                {"SubstanceOwnInterestPerc", ExistingOwnInterest.ToString("0.00 '%'") },
                {"SubstanceOwnInterestVal", ExistingOwnAnnuity.InterestMonthly.ToString("#,##0.00 €/m²")},
                {"SubstanceOwnReservePerc", ExistingOwnReserve.ToString("0.00 '%'") },
                {"SubstanceOwnReserveVal", ExistingOwnAnnuity.RepayMonthly.ToString("#,##0.00 €/m²")},

                {"InvestmentPerc", 100.ToString("0.00 '%'")},
                {"InvestmentVal", InvestmentVal.ToString("#,##0.00 €/m²")},
                {"InvestmentBorrowPerc", InvestmentBorrowPerc.ToString("0.00 '%'")},
                {"InvestmentBorrowVal", InvestmentBorrowVal.ToString("#,##0.00 €/m²")},
                {"InvestmentOwnPerc", InvestmentOwnPerc.ToString("0.00 '%'")},
                {"InvestmentOwnVal", InvestmentOwnVal.ToString("#,##0.00 €/m²")},
                {"InvestAnnuityPerc", InvestAnnuity.SumPerc.ToString("0.00 '%'")},
                {"InvestAnnuityVal", InvestAnnuity.SumVal.ToString("#,##0.00 €/m²")},
                {"InvestAnnuityInterestPerc", InvestmentAnnuityInterest.ToString("0.00 '%'")},
                {"InvestAnnuityInterestVal", InvestAnnuity.InterestMonthly.ToString("#,##0.00 €/m²")},
                {"InvestAnnuityRepayPerc", InvestmentAnnuityRepay.ToString("0.00 '%'")},
                {"InvestAnnuityRepayVal", InvestAnnuity.RepayMonthly.ToString("#,##0.00 €/m²")},
                {"InvestOwnPerc", InvestOwnAnnuity.SumPerc.ToString("0.00 '%'")},
                {"InvestOwnVal", InvestOwnAnnuity.SumVal.ToString("#,##0.00 €/m²")},
                {"InvestOwnInterestPerc", InvestmentOwnInterest.ToString("0.00 '%'")},
                {"InvestOwnInterestVal", InvestOwnAnnuity.InterestMonthly.ToString("#,##0.00 €/m²")},
                {"InvestOwnReservePerc", InvestmentOwnReserve.ToString("0.00 '%'")},
                {"InvestOwnReserveVal", InvestOwnAnnuity.RepayMonthly.ToString("#,##0.00 €/m²")},

                {"RentLossPerc", RentLossPerc.ToString("0.00 '%'") },
                {"RentLossVal", RentLossVal.ToString("#,##0.00 €/m²")},
                {"MaintenancePerc", MaintancePerc.ToString("0.00 '%'")},
                {"MaintenanceVal", MaintanceMonthly.ToString("#,##0.00 €/m²")},
                {"AdministrationPerFlat", AdministrationPerFlatAnual.ToString("#,##0.00 €/m²")},
                {"AdministrationPerMeter", AdministrationPerFlatMonth.ToString("#,##0.00 €/m²")},
                {"AvarageFlatSize", AvarageFlatSize.ToString("0 m²")},

                {"RentalCosts", RentCost.ToString("#,##0.00 €/m²")},
                {"SubstanceBorrowRepayLength", ExistingAnnuity.GetFormattedPeriode()},
                {"SubstanceOwnReserveLength", ExistingOwnAnnuity.GetFormattedPeriode()},
                {"InvestBorrowRepayLength", InvestAnnuity.GetFormattedPeriode()},
                {"InvestOwnReserveLength", InvestOwnAnnuity.GetFormattedPeriode()}

            };
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
                case CalcInputs.Investment:
                case CalcInputs.ExistingSubstance:
                    if (val > 100000)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die vorhande Substanz darf 100.000 € nicht übersteigen.");
                    else if (val <= 0)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Bitte geben Sie einen Wert größer als 0 ein.");
                    break;
                case CalcInputs.InvestmentBorrow:
                case CalcInputs.ExistingSubstanceBorrow:
                    if (option == CalcOption.M2 && val > ExistingSubstance)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Der Anteil des Fremdkapitals darf das Gesamtkapital nicht übersteigen.");
                    else if (option == CalcOption.Percent && (val > 100 || val < 0))
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eingabe muss größer als 0 und kleiner/ gleich 100 sein.");
                    break;
                case CalcInputs.InvestOwnInterest:
                case CalcInputs.InvestAnnuityInterest:
                case CalcInputs.SubstanceOwnInterest:
                case CalcInputs.SubstanceAnnuityInterest:
                    if (val < 0 || val > 100)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eingabe muss größer als 0 und kleiner/ gleich 100 sein.");
                    break;
                case CalcInputs.InvestOwnReserve:
                case CalcInputs.InvestAnnuityRepay:
                case CalcInputs.SubstanceOwnReserve:
                case CalcInputs.SubstanceAnnuityRepay:
                    if (val < 0.01 || val > 100)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eingabe muss größer / gleich 0.01 und kleiner / gleich 100 sein.");
                    break;

                case CalcInputs.RentLoss:
                    if (option == CalcOption.Percent && (val < 0 || val > 100))
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eingabe muss größer als 0 ud kleiner/ gleich 100 sein.");
                    
                    break;
                case CalcInputs.Maintance:
                    if (option == CalcOption.Percent && (val < 0 || val > 100))
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eingabe muss größer als 0 ud kleiner/ gleich 100 sein.");
                    break;
                case CalcInputs.Administration:
                    if (val < 0)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eingabe muss größer als 0 sein.");
                    break;
                case CalcInputs.AvarageFlatSize:
                    if (val <= 0)
                        ret = new ValidationResult(false, "Eingabefehler",
                            "Die Eigabe muss größer als 0 sein.");
                    break;
                default:
                    throw new NotImplementedException();
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
                    ExistingAnnuityInterest = value;
                    break;
                case CalcInputs.SubstanceAnnuityRepay:
                    ExistingAnnuityRepay = value;
                    break;
                case CalcInputs.SubstanceOwnInterest:
                    ExistingOwnInterest = value;
                    break;
                case CalcInputs.SubstanceOwnReserve:
                    ExistingOwnReserve = value;
                    break;
                case CalcInputs.Investment:
                    InvestmentVal = value;
                    break;
                case CalcInputs.InvestmentBorrow:
                    if (option == CalcOption.Percent)
                        InvestmentBorrowPerc = value;
                    else if (option == CalcOption.M2)
                        InvestmentBorrowVal = value;
                    break;
                case CalcInputs.InvestAnnuityInterest:
                    InvestmentAnnuityInterest = value;
                    break;
                case CalcInputs.InvestAnnuityRepay:
                    InvestmentAnnuityRepay = value;
                    break;
                case CalcInputs.InvestOwnInterest:
                    InvestmentOwnInterest = value;
                    break;
                case CalcInputs.InvestOwnReserve:
                    InvestmentOwnReserve = value;
                    break;
                case CalcInputs.RentLoss:
                    if (option == CalcOption.Percent)
                        RentLossPerc = value;
                    break;
                case CalcInputs.Administration:
                    if (option == CalcOption.Unit)
                        AdministrationPerFlatAnual = value;
                    else if (option == CalcOption.Month)
                        AdministrationPerFlatAnual = value * AvarageFlatSize * 12;
                    else if (option == CalcOption.Year)
                        AdministrationPerFlatAnual = value * AvarageFlatSize;
                    break;
                case CalcInputs.AvarageFlatSize:
                    AvarageFlatSize = value;
                    break;
                case CalcInputs.Maintance:
                    if (option == CalcOption.Percent)
                        MaintancePerc = value;
                    break;
                default:
                    throw new NotImplementedException();
            }

            CalculateAnnuities();
        }

        private void CalculateAnnuities()
        {
            ExistingAnnuity = new Annuity(ExistingSubstanceBorrowVal,
                ExistingAnnuityInterest, ExistingAnnuityRepay);
            InvestAnnuity = new Annuity(InvestmentBorrowVal,
                InvestmentAnnuityInterest, InvestmentAnnuityRepay);
            ExistingOwnAnnuity = new Annuity(ExistingSubstanceOwnVal,
                ExistingOwnInterest, ExistingOwnReserve);
            InvestOwnAnnuity = new Annuity(InvestmentOwnVal,
                InvestmentOwnInterest, InvestmentOwnReserve);
        }
    }
}
