using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for Scenario.xaml
    /// </summary>
    public partial class Scenario : UserControl, INotifyPropertyChanged
    {
        private string SCENARIONAMETEXTBOXCONTENT = "Scenario name";

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
                if (value == String.Empty)
                {
                    LabelValidationScenario.Content = "It is necessary to fill scenario name.";
                    throw new ArgumentException("");
                }
                else
                    LabelValidationScenario.Content = "Validation";
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
            ScenarioName = SCENARIONAMETEXTBOXCONTENT;

            StackPanelFluents.Children.Add(new Label() { Content="Fluents:", FontSize=10});
            foreach (Fluent item in fluents)
            {
                StackPanelFluents.Children.Add(new Label() { Content = item.Name });
            }

            StackPanelStatements.Children.Add(new Label() { Content = "Actions:", FontSize=10});
            foreach (WorldAction item in actions)
            {
                StackPanelStatements.Children.Add(new Label() { Content = "(" + item.Id + ", " + item.Duration + ")", FontSize = 10 });
            }


            StackPanelScenarios.Children.Add(new Label() { Content = "Scenarios:", FontSize = 10 });

        }

        #endregion

        #region | METHODS |

        private void CleanValues()
        {
            ActionAdd.CleanValues();
            ActionList.CleanValues();
            LabelValidationScenario.Content = "Validation";
        }

        #endregion

        #region | EVENTS |

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.NextPage(_savedScenarios);
        }

        private void ButtonAddAction_Click(object sender, RoutedEventArgs e)
        {
            if (ActionAdd.SelectedWARecordType == null)
            {
                ActionAdd.LabelValidation.Content = "It is necessary to choose an action.";
            }
            else if (ActionList.AddAction(ActionAdd.Time, ActionAdd.SelectedWARecordType.Id,ActionAdd.SelectedWARecordType.Duration))
            {
                _scenarioDescription.addACS(ActionAdd.SelectedWARecordType, ActionAdd.Time);
                ActionAdd.CleanValues();
            }
            else
            {
                ActionAdd.LabelValidation.Content = "Action with this name and time already exists.";
            }
        }

        private void ButtonAddObservation_Click(object sender, RoutedEventArgs e)
        {
            if (ObservationAdd.Expression == String.Empty)
            {
                ObservationAdd.LabelValidation.Content = "It is necessary to fill expression.";
            }
            else if (ActionList.AddObservation(ObservationAdd.Time, ObservationAdd.Expression))
            {
                _scenarioDescription.observations.Add(new ScenarioObservationRecord(new SimpleLogicExpression(ObservationAdd.Expression), ObservationAdd.Time));
                ObservationAdd.LabelValidation.Content = "Validation";
            }
        }


        #endregion

        private void AddScenario_Click(object sender, RoutedEventArgs e)
        {
            if (ScenarioName != String.Empty)
            {
                foreach (ScenarioDescription item in _savedScenarios)
                {
                    if (item.Name == ScenarioName)
                    {
                        LabelValidationScenario.Content = "Scenario with this name already exists.";
                        return;
                    }
                }
                _scenarioDescription.Name = ScenarioName;
                StackPanelScenarios.Children.Add(new Label() { Content = ScenarioName, FontSize = 10 });
                _savedScenarios.Add(_scenarioDescription);
                _scenarioDescription = new ScenarioDescription();
                ScenarioName = SCENARIONAMETEXTBOXCONTENT;
                CleanValues();
            }
            else
            {
                LabelValidationScenario.Content = "It is necessary to fill scenario name.";
            }
        }
    }
}
