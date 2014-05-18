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

        private fluentEnum _selectedMyEnumType;
        public fluentEnum SelectedMyEnumType
        {
            get { return _selectedMyEnumType; }
            set
            {
                _selectedMyEnumType = value;
                OnPropertyChanged("SelectedMyEnumType");
            }
        }

        private IEnumerable<fluentEnum> _myEnumTypeValues;

        public IEnumerable<fluentEnum> MyEnumTypeValues
        {
            set
            {
                _myEnumTypeValues = Enum.GetValues(typeof(fluentEnum))
                    .Cast<fluentEnum>();
                OnPropertyChanged("MyEnumTypeValues");
            }
            get
            {
                return Enum.GetValues(typeof(fluentEnum))
                    .Cast<fluentEnum>();
            }
        }


        
        public FluentValue()
        {
            InitializeComponent();
            ComboBosValue.ItemsSource = MyEnumTypeValues;
        }
    }
}
