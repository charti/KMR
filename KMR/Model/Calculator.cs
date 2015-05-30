using KMR.Common;
using KMR.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KMR.Model
{
    public interface IBaseCalcType
    {
        bool calc();
    }

    class Calculator
    {
        public Calculator()
        {
            CalcService = new CalculationService();
        }

        #region Properties

        public CalculationService CalcService { get; private set; }

        #endregion
    }

    public class ExcistingSubstance
    {
        public bool calc()
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + " calc()", ex);
            }
        }

        #region Properties

        public double ExistingSubstancePerc { get; set; }
        public double ExistingSubstanceVal { get; set; }

        #endregion
    }


    
}
