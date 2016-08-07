namespace KMR.Common
{
    public enum CalcInputs
    {
        ExcistingSubstance = 1,
        ExcistingSubstanceBorrow = 2,
        SubstanceAnnuityInterest = 6,
        SubstanceAnnuityRepay = 7,
        SubstanceOwnInterest = 10,
        SubstanceOwnReserve = 11,
        InvestmentBorrow = 15,
        InvestmentOwn = 16,
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
        m2 = 2,
        year = 3
    }
}
