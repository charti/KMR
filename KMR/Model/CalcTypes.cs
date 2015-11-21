using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMR.Model
{
    public class Calc
    {
        Dictionary<string, ICalculation> _calcObjects
            = new Dictionary<string, ICalculation>();       
        
        public ICalculation this[string name]
        {
            get { return _calcObjects[name]; }
        }

        public void register(ICalculation handler, IEnumerable<string> subs)
        {
            foreach(var sub in subs)
            {
                register(sub, handler);
            }
        }

        private void register(string name, ICalculation member)
        {
            _calcObjects.Add(name, member);
        }

    }

    internal class ExistingSubstance : BaseType
    {
        internal double SubstanceVal;
        internal double BorrowPerc;
        internal double OwnPerc;

        public ExistingSubstance()
        {
            Calculator.register(this, new[] {
                "ExistingSubstancePerc",        "ExistingSubstanceVal",
                "ExistingSubstanceBorrowPerc",  "ExistingSubstanceBorrowVal",
                "ExistingSubstanceOwnPerc",     "ExistingSubstanceOwnVal"
                });

        }

        public override Dictionary<string, string> getValues()
        {
            return new Dictionary<string, string>() {
                { "ExistingSubstancePerc", "100,00 %" },
                { "ExistingSubstanceVal", SubstanceVal.ToString("#,##") + " €" },

                { "ExistingSubstanceBorrowPerc", BorrowPerc.ToString("#,##") + " %" },
                { "ExistingSubstanceBorrowVal", (BorrowPerc * SubstanceVal).ToString("#,##") + " €" },

                { "ExistingSubstanceOwnPerc", OwnPerc.ToString("#,##") + " %" },
                { "ExistingSubstanceOwnVal", (OwnPerc * SubstanceVal).ToString("#,##") + " €" }
            };
        } 

        public override void set(KeyValuePair<string, object> input)
        {
            if(validate(input))
            {
                switch (input.Key)
                {
                    case "ExistingSubstanceVal":
                        SubstanceVal = (double)input.Value;
                        break;
                    case "ExistingSubstanceBorrowPerc":
                        BorrowPerc = (double)input.Value;
                        OwnPerc = 100 - BorrowPerc;
                        break;
                    case "ExistingSubstanceBorrowVal":
                        BorrowPerc = 100 * (double)input.Value * SubstanceVal;
                        OwnPerc = 100 - BorrowPerc;
                        break;
                    case "ExistingSubstanceOwnPerc":
                        OwnPerc = (double)input.Value;
                        BorrowPerc = 100 - OwnPerc;
                        break;
                    case "ExistingSubstanceOwnVal":
                        OwnPerc = 100 * (double)input.Value * SubstanceVal;
                        BorrowPerc = 100 - OwnPerc;
                        break;
                }
            }
            throw new ArgumentOutOfRangeException(input.Key.ToString() + input.Value.ToString());
        }

        public override bool validate(KeyValuePair<string, object> value)
        {
            throw new NotImplementedException();
        }

    }

    public abstract class BaseType : ICalculation
    {
        static Calc calcInstance = new Calc();

        public Calc Calculator { get; private set; }

        public BaseType()
        {
            Calculator = calcInstance;
        }

        public abstract Dictionary<string, string> getValues();
        public abstract void set(KeyValuePair<string, object> input);
        public abstract bool validate(KeyValuePair<string, object> value);
    }

    public interface ICalculation
    {
        void set(KeyValuePair<string, object> input);
        Dictionary<string, string> getValues();
        bool validate(KeyValuePair<string, object> value);
    }
}
