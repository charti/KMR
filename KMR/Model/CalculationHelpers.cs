using System.Collections.Generic;

namespace KMR.Model
{
    public class Annuity
    {
        public int RepayPeriodeMonths { get; private set; } = 0;
        public double Debt { get; private set; } = 0;
        public double Repay { get; private set; } = 0;
        public double Interest { get; private set; } = 0;
        public double RepayPerc { get; private set; } = 0;
        public double InterestPerc { get; private set; } = 0;
        public double AnnuityMonthly { get; private set; } = 0;
        public double RepayMonthly { get; private set; } = 0;
        public double InterestMonthly { get; private set; } = 0;
        public double SumPerc => RepayPerc + InterestPerc;
        public double SumVal => InterestMonthly + RepayMonthly;

        public Annuity(double debt, double interestPerc, double repayPerc)
        {
            Debt = debt;
            InterestPerc = interestPerc;
            RepayPerc = repayPerc;

            Calc();
        }

        private void Calc()
        {
            var monthlyInterestPerc = InterestPerc / 12;
            var monthlyRepayPerc = RepayPerc / 12;
            AnnuityMonthly = Debt * (monthlyInterestPerc +
                monthlyRepayPerc) / 100;

            if (AnnuityMonthly > 0)
            {
                do
                {
                    RepayPeriodeMonths++;
                    var interestVal = Debt * monthlyInterestPerc / 100;
                    var repayVal = AnnuityMonthly - interestVal;
                    Debt -= repayVal;
                    Repay += repayVal;
                    Interest += interestVal;
                } while (Debt >= 0);

                RepayMonthly = Repay / RepayPeriodeMonths;
                InterestMonthly = Interest / RepayPeriodeMonths;
            }
        }
    }

    public class OwnAnnuity
    {

    }
}
