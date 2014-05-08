using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.Scenario;
using System.ComponentModel;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for Scenario.xaml
    /// </summary>
    public partial class Scenario : UserControl, INotifyPropertyChanged
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

        
        #region Initialization
        public Scenario()
        {
            
            
            InitializeComponent();
        }

        public void Initialize(List<Fluent> fluents, List<WorldAction> actions)
        {
            ObservationAdd.SetFluents(fluents);
            ActionAdd.SetActions(actions);
            _scenarioDescription = new ScenarioDescription();
        }
        #endregion
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
            Switcher.NextPage();
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
