using KMR.Common;
using KMR.Control;
using System;
using System.Collections.Generic;

namespace KMR.Model
{
    public class CalculationService : BaseController
    {
        Dictionary<string, Dictionary<string, string>> _frontEndStrings =
            new Dictionary<string, Dictionary<string, string>>();

        private Calculator Calculator { get; set; }

        public CalculationService(Calculator calculator)
        {
            if (calculator == null)
                throw new ArgumentNullException();

            Calculator = calculator;

            Mediator.Register(this, new[]
            {
                Messages.Validate
            });

            Mediator.NotifyColleagues(Messages.PropertyListAdd, _frontEndStrings);
            Mediator.NotifyColleagues(Messages.UpdateFrontend, _frontEndStrings);
        }
        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.Validate:
                    var tuple = args as Tuple<CalcInputs, CalcOption, string>;

                    var result = Calculator.Validate(tuple.Item1, tuple.Item2, tuple.Item3);
                    if (result.Success)
                    {
                        
                        Mediator.NotifyColleagues(Messages.UpdateFrontend,
                            new Dictionary<string, Dictionary<string, string>>()
                            {
                                [typeof(CalcController).ToString()] = Calculator.GetFrontEndStrings()
                            });
                    }

                    Mediator.NotifyColleagues(Messages.ValidationResult, result);
                    break;
                default:
                    new NotImplementedException();
                    break;
            }
        }

        #region Properties

        #endregion
    }
}
