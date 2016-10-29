﻿using KMR.Common;
using System;
using System.Collections.Generic;
using System.Windows.Input;

using KMR.Model;
using System.Windows.Forms;
using System.IO;

namespace KMR.Control
{
    public class CalcController : BaseController
    {
        private Calculator Calc { get; set; }

        public CalcController()
        {
            Calc = new Calculator();

            Mediator.Register(this, new [] 
            {
                Messages.PropertyListAdd,
                Messages.UpdateFrontend,
                Messages.Validate
            });

            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.OpenMenuView, MenuBack_Click));
            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.Save, Save_Click));
            CommandManager.RegisterClassCommandBinding(typeof(System.Windows.Controls.Control),
                new CommandBinding(Commands.Load, Load_Click));

            updateView(Calc.GetFrontEndStrings());
        }

        private void Load_Click(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog()
                {
                    Filter = "(*.kmr)|*.kmr"
                };

                Calculator loaded = null;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    loaded = new Calculator(File.ReadAllLines(openFileDialog.FileName));

                Calc = loaded;
                updateView(Calc.GetFrontEndStrings());
            }
            catch
            {
                MessageBox.Show("Beim Laden ist ein unbekannter Fehler aufgetreten.", "Error");
            }
        }

        private void Save_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "(*.kmr)|*.kmr"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFileDialog.FileName, Calc.Save());
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
                    updateView(Calc.GetFrontEndStrings());
                    break;
                case Messages.Validate:
                    var input = args as Tuple<CalcInputs, CalcOption, string>;
                    if (input == null)
                        throw new InvalidCastException();

                    var result = Calc.Validate(type: input.Item1, option: input.Item2, value: input.Item3);
                    Mediator.NotifyColleagues(Messages.ValidationResult, result);

                    if (result.Success)
                        updateView(Calc.GetFrontEndStrings());

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
                propDictionary[type].Add(prop, _values[prop]);
        }

        private void updateView(Dictionary<string, string> data)
        {
            _values = data;
            foreach (var prop in _values.Keys)
                OnPropertyChanged(prop);
        }

        #endregion

        #region Properties

        Dictionary<string, string> _values = new Dictionary<string, string>()
        {
            {"ExistingSubstancePerc",       String.Empty},   {"ExistingSubstanceVal",        String.Empty},
            {"ExistingSubstanceBorrowPerc", String.Empty},   {"ExistingSubstanceBorrowVal",  String.Empty},
            {"ExistingSubstanceOwnPerc",    String.Empty},   {"ExistingSubstanceOwnVal",     String.Empty},
            {"SubstanceAnnuityPerc",        String.Empty},   {"SubstanceAnnuityVal",         String.Empty},
            {"SubstanceAnnuityInterestPerc",String.Empty},   {"SubstanceAnnuityInterestVal", String.Empty},
            {"SubstanceAnnuityRepayPerc",   String.Empty},   {"SubstanceAnnuityRepayVal",    String.Empty},
            {"SubstanceOwnPerc",            String.Empty},   {"SubstanceOwnVal",             String.Empty},
            {"SubstanceOwnInterestPerc",    String.Empty},   {"SubstanceOwnInterestVal",     String.Empty},
            {"SubstanceOwnReservePerc",     String.Empty},   {"SubstanceOwnReserveVal",      String.Empty},

            {"InvestmentPerc",              String.Empty},   {"InvestmentVal",               String.Empty},
            {"InvestmentBorrowPerc",        String.Empty},   {"InvestmentBorrowVal",         String.Empty},
            {"InvestmentOwnPerc",           String.Empty},   {"InvestmentOwnVal",            String.Empty},
            {"InvestAnnuityPerc",           String.Empty},   {"InvestAnnuityVal",            String.Empty},
            {"InvestAnnuityInterestPerc",   String.Empty},   {"InvestAnnuityInterestVal",    String.Empty},
            {"InvestAnnuityRepayPerc",      String.Empty},   {"InvestAnnuityRepayVal",       String.Empty},
            {"InvestOwnPerc",               String.Empty},   {"InvestOwnVal",                String.Empty},
            {"InvestOwnInterestPerc",       String.Empty},   {"InvestOwnInterestVal",        String.Empty},
            {"InvestOwnReservePerc",        String.Empty},   {"InvestOwnReserveVal",         String.Empty},

            {"RentLossPerc",                String.Empty},   {"RentLossVal",                 String.Empty},
            {"MaintenancePerc",             String.Empty},   {"MaintenanceVal",              String.Empty},
            {"AdministrationPerFlat",       String.Empty},   {"AdministrationPerMeter",      String.Empty},
            {"AvarageFlatSize",             String.Empty},   
            {"DepreciationPerc",            String.Empty},   {"DepreciationVal",             String.Empty},
            {"HgbPerc",                     String.Empty},   {"HgbVal",                      String.Empty},

            {"RentalCosts",                 String.Empty},
            {"SubstanceBorrowRepayLength",  String.Empty},
            {"SubstanceOwnReserveLength",   String.Empty},
            {"InvestBorrowRepayLength",     String.Empty},
            {"InvestOwnReserveLength",      String.Empty}

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
