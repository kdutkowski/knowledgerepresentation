using KnowledgeRepresentationReasoning.World;
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
    /// Interaction logic for SceAddObservations.xaml
    /// </summary>
    public partial class SceAddObservations : UserControl, ISceControl, INotifyPropertyChanged
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
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public SceAddObservations()
        {
            InitializeComponent();
        }
        private int _time;
        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        private List<Fluent> _fluents;
        public List<Fluent> Fluents
        {
            get
            {
                GetFluentsValues();
                return _fluents;
            }
            set
            {
                _fluents = value;
            }
        }
        private void GetFluentsValues()
        {
            _fluents.Clear();
            foreach (var item in StackPanelFluents.Children)
            {
                _fluents.Add(new Fluent() { Name = ((FluentValue)item).NameFluent, Value = ((FluentValue)item).Value });
            }
        }

        private void InitFluents()
        {
            foreach (var item in _fluents)
            {
                StackPanelFluents.Children.Add(new FluentValue { NameFluent = item.Name, Value = item.Value });
            }
        }

        public void SetFluents(List<Fluent> fluents)
        {
            _fluents = (fluents != null) ? fluents : new List<Fluent>();
            InitFluents();
        }

        public void CleanValues()
        {
            throw new NotImplementedException();
        }
    }
}
