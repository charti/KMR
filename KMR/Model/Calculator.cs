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
    
}
