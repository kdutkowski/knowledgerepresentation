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
        #region | PROPERTIES |

        private ScenarioDescription _scenarioDescription;
        private List<ScenarioDescription> _savedScenarios;

        private string _scenarioName;

        public string ScenarioName
        {
            get { return _scenarioName; }
            set
            {
                _scenarioName = value;
                OnPropertyChanged("ScenarioName");
            }
        }

        #endregion

        #region | PropertyChanged |

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region | INITIALIZATION |

        public Scenario()
        {
            InitializeComponent();
        }

        public void Initialize(List<Fluent> fluents, List<WorldAction> actions)
        {
            ObservationAdd.SetFluents(fluents);
            ActionAdd.SetActions(actions);
            _scenarioDescription = new ScenarioDescription();
            _savedScenarios = new List<ScenarioDescription>();
        }

        #endregion

        #region | EVENTS |

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.NextPage(_savedScenarios);
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
            if (ObservationAdd.Fluents.Count > 0 && ActionList.AddObservation(ObservationAdd.Time, ObservationAdd.Fluents))
            {
                // TODO: dodać do _scenarioDescription
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _scenarioDescription.Name = ScenarioName;
            _savedScenarios.Add(_scenarioDescription);
            _scenarioDescription = new ScenarioDescription();
            ScenarioName = string.Empty;
        }

        public ScenarioDescription GetScenarioDescription()
        {
            return _scenarioDescription;
        }

        #endregion
    }
}
