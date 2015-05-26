using KMR.Common;
using KMR.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KMR.Control
{
    public class CalcController : BaseController
    {
        public CalcController()
        {
            Mediator.Register(this, new [] 
            {
                Messages.PropertyListAdd,
                Messages.UpdateFrontend
            });

            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.OpenMenuView, MenuBack_Click));
        }

        #region Notifications

        public override void MessageNotification(string message, object args)
        {
            switch (message)
            {
                case Messages.PropertyListAdd:
                    exchangePropertyList(args);
                    break;
                case Messages.UpdateFrontend:
                    updateView(args);
                    break;
            }
        }

        #endregion

        #region Events

        private void MenuBack_Click(object sender, ExecutedRoutedEventArgs e)
        {
            Mediator.NotifyColleagues(Messages.NavigateTo, typeof(View.Menu));
        }

        #endregion

        #region Methods

        private void exchangePropertyList(object propList)
        {
            var type = this.GetType().ToString();
            var propDictionary =
                (Dictionary<string, Dictionary<string, string>>)propList;
            propDictionary.Add(type,
                new Dictionary<string, string>());

            foreach (var prop in _values.Keys)
                propDictionary[type].Add(prop, String.Empty);
        }
        private void updateView(object data)
        {
            var type = this.GetType().ToString();
            _values = ((Dictionary<string, Dictionary<string, string>>)data)[type];
            foreach (var prop in _values.Keys)
                OnPropertyChanged(prop);
        }

        #endregion

        #region Properties

        Dictionary<string, string> _values = new Dictionary<string, string>() 
        {
            {"ExistingSubstancePerc",       "0"},           {"ExistingSubstanceVal",        "0"},
            {"ExistingSubstanceBorrowPerc", "0"},           {"ExistingSubstanceBorrowVal",  "0"},
            {"ExistingSubstanceOwnPerc",    "0"},           {"ExistingSubstanceOwnVal",     "0"},
            {"SubstanceAnnuityPerc",        "0"},           {"SubstanceAnnuityVal",         "0"},
            {"SubstanceAnnuityInterestPerc","0"},           {"SubstanceAnnuityInterestVal", "0"},
            {"SubstanceAnnuityRepayPerc",   "0"},           {"SubstanceAnnuityRepayVal",    "0"},
            {"SubstanceOwnPerc",            "0"},           {"SubstanceOwnVal",             "0"},
            {"SubstanceOwnInterestPerc",    "0"},           {"SubstanceOwnInterestVal",     "0"},
            {"SubstanceOwnReservePerc",     "0"},           {"SubstanceOwnReserveVal",      "0"},

            {"InvestmentPerc",              "0"},           {"InvestmentVal",               "0"},
            {"InvestmentBorrowPerc",        "0"},           {"InvestmentBorrowVal",         "0"},
            {"InvestmentOwnPerc",           "0"},           {"InvestmentOwnVal",            "0"},
            {"InvestAnnuityPerc",           "0"},           {"InvestAnnuityVal",            "0"},
            {"InvestAnnuityInterestPerc",   "0"},           {"InvestAnnuityInterestVal",    "0"},
            {"InvestAnnuityRepayPerc",      "0"},           {"InvestAnnuityRepayVal",       "0"},
            {"InvestOwnPerc",               "0"},           {"InvestOwnVal",                "0"},
            {"InvestOwnInterestPerc",       "0"},           {"InvestOwnInterestVal",        "0"},
            {"InvestOwnReservePerc",        "0"},           {"InvestOwnReserveVal",         "0"},

            {"RentLossPerc",                "0"},           {"RentLossVal",                 "0"},
            {"MaintenancePerc",             "0"},           {"MaintenanceVal",              "0"},
            {"AdministrationPerFlat",       "0"},           {"AdministrationPerMeter",      "0"},
            {"AvarageFlatSize",             "0"},           
            {"DepreciationPerc",            "0"},           {"DepreciationVal",             "0"},
            {"HgbPerc",                     "0"},           {"HgbVal",                      "0"},

            {"RentalCosts",                 "0"},
            {"SubstanceBorrowRepayLength",  "0"},
            {"SubstanceOwnReserveLength",   "0"},
            {"InvestBorrowRepayLength",     "0"},
            {"InvestOwnReserveLength",      "0"}

        };


        public string ExistingSubstancePerc
        {
            get { return _values["ExistingSubstancePerc"]; }
            set { _values["ExistingSubstancePerc"] = value; OnPropertyChanged("ExistingSubstancePerc"); }
        }
        public string ExistingSubstanceBorrowPerc
        {
            get { return _values["ExistingSubstanceBorrowPerc"]; }
            set { _values["ExistingSubstanceBorrowPerc"] = value; OnPropertyChanged("ExistingSubstanceBorrowPerc"); }
        }
        public string ExistingSubstanceOwnPerc
        {
            get { return _values["ExistingSubstanceOwnPerc"]; }
            set { _values["ExistingSubstanceOwnPerc"] = value; OnPropertyChanged("ExistingSubstanceOwnPerc"); }
        }
        public string SubstanceAnnuityPerc
        {
            get { return _values["SubstanceAnnuityPerc"]; }
            set { _values["SubstanceAnnuityPerc"] = value; OnPropertyChanged("SubstanceAnnuityPerc"); }
        }
        public string SubstanceAnnuityInterestPerc
        {
            get { return _values["SubstanceAnnuityInterestPerc"]; }
            set { _values["SubstanceAnnuityInterestPerc"] = value; OnPropertyChanged("SubstanceAnnuityInterestPerc"); }
        }
        public string SubstanceAnnuityRepayPerc
        {
            get { return _values["SubstanceAnnuityRepayPerc"]; }
            set { _values["SubstanceAnnuityRepayPerc"] = value; OnPropertyChanged("SubstanceAnnuityRepayPerc"); }
        }
        public string SubstanceOwnPerc
        {
            get { return _values["SubstanceOwnPerc"]; }
            set { _values["SubstanceOwnPerc"] = value; OnPropertyChanged("SubstanceOwnPerc"); }
        }
        public string SubstanceOwnInterestPerc
        {
            get { return _values["SubstanceOwnInterestPerc"]; }
            set { _values["SubstanceOwnInterestPerc"] = value; OnPropertyChanged("SubstanceOwnInterestPerc"); }
        }
        public string SubstanceOwnReservePerc
        {
            get { return _values["SubstanceOwnReservePerc"]; }
            set { _values["SubstanceOwnReservePerc"] = value; OnPropertyChanged("SubstanceOwnReservePerc"); }
        }

        public string InvestmentPerc
        {
            get { return _values["InvestmentPerc"]; }
            set { _values["InvestmentPerc"] = value; OnPropertyChanged("InvestmentPerc"); }
        }
        public string InvestmentBorrowPerc
        {
            get { return _values["InvestmentBorrowPerc"]; }
            set { _values["InvestmentBorrowPerc"] = value; OnPropertyChanged("InvestmentBorrowPerc"); }
        }
        public string InvestmentOwnPerc
        {
            get { return _values["InvestmentOwnPerc"]; }
            set { _values["InvestmentOwnPerc"] = value; OnPropertyChanged("InvestmentOwnPerc"); }
        }
        public string InvestAnnuityPerc
        {
            get { return _values["InvestAnnuityPerc"]; }
            set { _values["InvestAnnuityPerc"] = value; OnPropertyChanged("InvestAnnuityPerc"); }
        }
        public string InvestAnnuityInterestPerc
        {
            get { return _values["InvestAnnuityInterestPerc"]; }
            set { _values["InvestAnnuityInterestPerc"] = value; OnPropertyChanged("InvestAnnuityInterestPerc"); }
        }
        public string InvestAnnuityRepayPerc
        {
            get { return _values["InvestAnnuityRepayPerc"]; }
            set { _values["InvestAnnuityRepayPerc"] = value; OnPropertyChanged("InvestAnnuityRepayPerc"); }
        }
        public string InvestOwnPerc
        {
            get { return _values["InvestOwnPerc"]; }
            set { _values["InvestOwnPerc"] = value; OnPropertyChanged("InvestOwnPerc"); }
        }
        public string InvestOwnInterestPerc
        {
            get { return _values["InvestOwnInterestPerc"]; }
            set { _values["InvestOwnInterestPerc"] = value; OnPropertyChanged("InvestOwnInterestPerc"); }
        }
        public string InvestOwnReservePerc
        {
            get { return _values["InvestOwnReservePerc"]; }
            set { _values["InvestOwnReservePerc"] = value; OnPropertyChanged("InvestOwnReservePerc"); }
        }

        public string RentLossPerc
        {
            get { return _values["RentLossPerc"]; }
            set { _values["RentLossPerc"] = value; OnPropertyChanged("RentLossPerc"); }
        }
        public string MaintenancePerc
        {
            get { return _values["MaintenancePerc"]; }
            set { _values["MaintenancePerc"] = value; OnPropertyChanged("MaintenancePerc"); }
        }
        public string AdministrationPerFlat
        {
            get { return _values["AdministrationPerFlat"]; }
            set { _values["AdministrationPerFlat"] = value; OnPropertyChanged("AdministrationPerFlat"); }
        }
        public string AvarageFlatSize
        {
            get { return _values["AvarageFlatSize"]; }
            set { _values["AvarageFlatSize"] = value; OnPropertyChanged("AvarageFlatSize"); }
        }
        public string DepreciationPerc
        {
            get { return _values["DepreciationPerc"]; }
            set { _values["DepreciationPerc"] = value; OnPropertyChanged("DepreciationPerc"); }
        }
        public string HgbPerc
        {
            get { return _values["HgbPerc"]; }
            set { _values["HgbPerc"] = value; OnPropertyChanged("HgbPerc"); }
        }

        public string ExistingSubstanceBorrowVal
        {
            get { return _values["ExistingSubstanceBorrowVal"]; }
            set { _values["ExistingSubstanceBorrowVal"] = value; OnPropertyChanged("ExistingSubstanceBorrowVal"); }
        }
        public string ExistingSubstanceOwnVal
        {
            get { return _values["ExistingSubstanceOwnVal"]; }
            set { _values["ExistingSubstanceOwnVal"] = value; OnPropertyChanged("ExistingSubstanceOwnVal"); }
        }
        public string SubstanceAnnuityVal
        {
            get { return _values["SubstanceAnnuityVal"]; }
            set { _values["SubstanceAnnuityVal"] = value; OnPropertyChanged("SubstanceAnnuityVal"); }
        }
        public string SubstanceAnnuityInterestVal
        {
            get { return _values["SubstanceAnnuityInterestVal"]; }
            set { _values["SubstanceAnnuityInterestVal"] = value; OnPropertyChanged("SubstanceAnnuityInterestVal"); }
        }
        public string SubstanceAnnuityRepayVal
        {
            get { return _values["SubstanceAnnuityRepayVal"]; }
            set { _values["SubstanceAnnuityRepayVal"] = value; OnPropertyChanged("SubstanceAnnuityRepayVal"); }
        }
        public string SubstanceOwnVal
        {
            get { return _values["SubstanceOwnVal"]; }
            set { _values["SubstanceOwnVal"] = value; OnPropertyChanged("SubstanceOwnVal"); }
        }
        public string SubstanceOwnInterestVal
        {
            get { return _values["SubstanceOwnInterestVal"]; }
            set { _values["SubstanceOwnInterestVal"] = value; OnPropertyChanged("SubstanceOwnInterestVal"); }
        }
        public string SubstanceOwnReserveVal
        {
            get { return _values["SubstanceOwnReserveVal"]; }
            set { _values["SubstanceOwnReserveVal"] = value; OnPropertyChanged("SubstanceOwnReserveVal"); }
        }
        public string ExistingSubstanceVal
        {
            get { return _values["ExistingSubstanceVal"]; }
            set { _values["ExistingSubstanceVal"] = value; OnPropertyChanged("ExistingSubstanceVal"); }
        }

        public string InvestmentVal
        {
            get { return _values["InvestmentVal"]; }
            set { _values["InvestmentVal"] = value; OnPropertyChanged("InvestmentVal"); }
        }
        public string InvestmentBorrowVal
        {
            get { return _values["InvestmentBorrowVal"]; }
            set { _values["InvestmentBorrowVal"] = value; OnPropertyChanged("InvestmentBorrowVal"); }
        }
        public string InvestmentOwnVal
        {
            get { return _values["InvestmentOwnVal"]; }
            set { _values["InvestmentOwnVal"] = value; OnPropertyChanged("InvestmentOwnVal"); }
        }
        public string InvestAnnuityVal
        {
            get { return _values["InvestAnnuityVal"]; }
            set { _values["InvestAnnuityVal"] = value; OnPropertyChanged("InvestAnnuityVal"); }
        }
        public string InvestAnnuityInterestVal
        {
            get { return _values["InvestAnnuityInterestVal"]; }
            set { _values["InvestAnnuityInterestVal"] = value; OnPropertyChanged("InvestAnnuityInterestVal"); }
        }
        public string InvestAnnuityRepayVal
        {
            get { return _values["InvestAnnuityRepayVal"]; }
            set { _values["InvestAnnuityRepayVal"] = value; OnPropertyChanged("InvestAnnuityRepayVal"); }
        }
        public string InvestOwnVal
        {
            get { return _values["InvestOwnVal"]; }
            set { _values["InvestOwnVal"] = value; OnPropertyChanged("InvestOwnVal"); }
        }
        public string InvestOwnInterestVal
        {
            get { return _values["InvestOwnInterestVal"]; }
            set { _values["InvestOwnInterestVal"] = value; OnPropertyChanged("InvestOwnInterestVal"); }
        }
        public string InvestOwnReserveVal
        {
            get { return _values["InvestOwnReserveVal"]; }
            set { _values["InvestOwnReserveVal"] = value; OnPropertyChanged("InvestOwnReserveVal"); }
        }

        public string RentLossVal
        {
            get { return _values["RentLossVal"]; }
            set { _values["RentLossVal"] = value; OnPropertyChanged("RentLossVal"); }
        }
        public string MaintenanceVal
        {
            get { return _values["MaintenanceVal"]; }
            set { _values["MaintenanceVal"] = value; OnPropertyChanged("MaintenanceVal"); }
        }
        public string AdministrationPerMeter
        {
            get { return _values["AdministrationPerMeter"]; }
            set { _values["AdministrationPerMeter"] = value; OnPropertyChanged("AdministrationPerMeter"); }
        }
        public string DepreciationVal
        {
            get { return _values["DepreciationVal"]; }
            set { _values["DepreciationVal"] = value; OnPropertyChanged("DepreciationVal"); }
        }
        public string HgbVal
        {
            get { return _values["HgbVal"]; }
            set { _values["HgbVal"] = value; OnPropertyChanged("HgbVal"); }
        }

        public string RentalCosts
        {
            get { return _values["RentalCosts"]; }
            set { _values["RentalCosts"] = value; OnPropertyChanged("RentalCosts"); }
        }
        public string SubstanceBorrowRepayLength
        {
            get { return _values["SubstanceBorrowRepayLength"]; }
            set { _values["SubstanceBorrowRepayLength"] = value; OnPropertyChanged("SubstanceBorrowRepayLength"); }
        }
        public string SubstanceOwnReserveLength
        {
            get { return _values["SubstanceOwnReserveLength"]; }
            set { _values["SubstanceOwnReserveLength"] = value; OnPropertyChanged("SubstanceOwnReserveLength"); }
        }
        public string InvestBorrowRepayLength
        {
            get { return _values["InvestBorrowRepayLength"]; }
            set { _values["InvestBorrowRepayLength"] = value; OnPropertyChanged("InvestBorrowRepayLength"); }
        }
        public string InvestOwnReserveLength
        {
            get { return _values["InvestOwnReserveLength"]; }
            set { _values["InvestOwnReserveLength"] = value; OnPropertyChanged("InvestOwnReserveLength"); }
        }
        #endregion
    }
}
