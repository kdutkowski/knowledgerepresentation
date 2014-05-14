using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.Scenario;
using System.ComponentModel;
using KnowledgeRepresentationReasoning.Expressions;

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
            ActionAdd.SetActions(actions);
            _scenarioDescription = new ScenarioDescription();
            _savedScenarios = new List<ScenarioDescription>();
        }

        #endregion

        #region | METHODS |

        private void CleanValues()
        {
            ActionAdd.CleanValues();
        }

        #endregion

        #region | EVENTS |

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.NextPage();
        }

        private void ButtonAddAction_Click(object sender, RoutedEventArgs e)
        {
            if (ActionAdd.SelectedWARecordType == null)
            {
                ActionAdd.LabelValidation.Content = "It is necessary to choose an action.";
            }
            else if (ActionList.AddAction(ActionAdd.Time, ActionAdd.SelectedWARecordType.Id))
            {
                _scenarioDescription.addACS(ActionAdd.SelectedWARecordType, ActionAdd.Time);
                CleanValues();
            }
            else
            {
                ActionAdd.LabelValidation.Content = "Action with this name and time already exists.";
            }
        }

        private void ButtonAddObservation_Click(object sender, RoutedEventArgs e)
        {
            if (ActionList.AddObservation(ObservationAdd.Time, ObservationAdd.Expression))
            {
                _scenarioDescription.observations.Add(new ScenarioObservationRecord(new SimpleLogicExpression(ObservationAdd.Expression), ObservationAdd.Time));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _scenarioDescription.Name = ScenarioName;
            _savedScenarios.Add(_scenarioDescription);
            _scenarioDescription = new ScenarioDescription();
            ScenarioName = string.Empty;
        }


        #endregion
    }
}
