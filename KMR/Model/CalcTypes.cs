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
        public ExistingSubstance()
        {
            Calculator.register(this, new[] {
                "ExistingSubstancePerc",        "ExistingSubstanceVal",
                "ExistingSubstanceBorrowPerc",  "ExistingSubstanceBorrowVal",
                "ExistingSubstanceOwnPerc",     "ExistingSubstanceOwnVal"
                });
        }

        public override ICalculation calc()
        {
            return this;
        } 

        public override void set(KeyValuePair<string, object> input)
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

        public abstract ICalculation calc();
        public abstract void set(KeyValuePair<string, object> input);
    }

    public interface ICalculation
    {
        void set(KeyValuePair<string, object> input);
        ICalculation calc();
    }
}
