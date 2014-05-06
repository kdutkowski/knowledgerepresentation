using System;
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

using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.Expressions;
using System.ComponentModel;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for _Scenario.xaml
    /// </summary>
    public partial class _Scenario : UserControl, INotifyPropertyChanged
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

        private ScenarioDescription _scenarioDescription;

        public _Scenario(List<Fluent> fluents, List<WorldAction> actions  )
        {
            InitializeComponent();
           ObservationAdd.SetFluents(fluents);
            ActionAdd.SetActions(actions);
            _scenarioDescription = new ScenarioDescription();
        }

        private string _scenarioName;

        public string ScenarioName
        {
            get { return _scenarioName; }
            set { _scenarioName = value;
            OnPropertyChanged("ScenarioName");
            }
        }
        

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new _Results());
        }

        private void ButtonAddAction_Click(object sender, RoutedEventArgs e)
        {
            if (ActionAdd.SelectedWARecordType != null && ActionList.AddAction(ActionAdd.Time, ActionAdd.SelectedWARecordType.Id))
            {
                _scenarioDescription.addACS(ActionAdd.SelectedWARecordType, ActionAdd.Time);
            }

           
        }

        private void ButtonAddObservation_Click(object sender, RoutedEventArgs e)
        {
            if (ObservationAdd.Fluents.Count>0 && ActionList.AddObservation(ObservationAdd.Time, ObservationAdd.Fluents))
            {
                //TODO add observations
                //_scenarioObservationRecordList.Add(new ScenarioObservationRecord(expr, ObservationAdd.Time));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           //TODO add scenario
        }

        public ScenarioDescription GetScenarioDescription()
        {
            return _scenarioDescription;
        }

    }
}
