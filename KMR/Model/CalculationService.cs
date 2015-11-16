using KMR.Common;
using KMR.Control;
using System.Collections.Generic;

namespace KMR.Model
{
    public class CalculationService : BaseController
    {
        Dictionary<string, Dictionary<string, string>> _frontEndStrings =
            new Dictionary<string, Dictionary<string, string>>();

        public CalculationService()
        {
            Mediator.NotifyColleagues(Messages.PropertyListAdd, _frontEndStrings);
            Mediator.NotifyColleagues(Messages.UpdateFrontend, _frontEndStrings);
        }
        public override void MessageNotification(string message, object args)
        {
        }

        #region Properties

        #endregion
    }
}
