namespace KMR.Common
{
    public enum CalcInputs
    {
        ExistingSubstance = 1,
        ExistingSubstanceBorrow = 2,
        SubstanceAnnuityInterest = 6,
        SubstanceAnnuityRepay = 7,
        SubstanceOwnInterest = 10,
        SubstanceOwnReserve = 11,
        Investment = 15,
        InvestmentBorrow = 16,
        InvestAnnuityInterest = 20,
        InvestAnnuityRepay = 21,
        InvestOwnInterest = 24,
        InvestOwnReserve = 25,
        RentLoss = 29,
        Maintance = 31,
        Administration = 33,
        AvarageFlatSize = 35,
        HGB = 40
    }

    public enum CalcOption
    {
        Percent = 1,
        M2 = 2,
        Year = 3,
        Unit = 4,
        Month = 5
    }
}
