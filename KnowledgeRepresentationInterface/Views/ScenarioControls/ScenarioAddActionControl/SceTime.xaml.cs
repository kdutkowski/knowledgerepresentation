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

namespace KnowledgeRepresentationInterface.Views.ScenarioControls
{
    /// <summary>
    /// Interaction logic for SceTime.xaml
    /// </summary>
    public partial class SceTime : UserControl, ISceControl
    {
        public SceTime()
        {
            InitializeComponent();
        }

        public void CleanValues()
        {
            throw new NotImplementedException();
        }
    }
}
