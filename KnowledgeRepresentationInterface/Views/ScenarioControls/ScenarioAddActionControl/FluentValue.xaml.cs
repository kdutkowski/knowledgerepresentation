using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace KnowledgeRepresentationInterface.Views.ScenarioControls.ScenarioAddActionControl
{
    /// <summary>
    /// Interaction logic for FluentValue.xaml
    /// </summary>
    public partial class FluentValue : UserControl, INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private string _nameFluent;

        public string NameFluent
        {
            get { return _nameFluent; }
            set { _nameFluent = value;
            OnPropertyChanged("NameFluent");
            }
        
        }

        private bool _value;

        public bool Value
        {
            get { return _value; }
            set { _value = value;
            OnPropertyChanged("Value");
            }
        }

        
        public FluentValue()
        {
            InitializeComponent();
        }
    }
}
