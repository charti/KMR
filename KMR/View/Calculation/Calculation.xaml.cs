﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KMR.View
{
    /// <summary>
    /// Interaktionslogik für Page2.xaml
    /// </summary>
    public partial class Calculation : UserControl
    {
        public Control.CalcController Controller { get; private set; }
        public Calculation()
        {
            Controller = new Control.CalcController();
            DataContext = Controller;
            InitializeComponent();
        }
    }
}
