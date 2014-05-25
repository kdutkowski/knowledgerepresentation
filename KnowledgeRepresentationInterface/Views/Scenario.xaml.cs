using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for Scenario.xaml
    /// </summary>
    public partial class Scenario : UserControl, INotifyPropertyChanged
    {
        private string SCENARIONAMETEXTBOXCONTENT = "Scenario name";

        #region | PROPERTIES |

        private List<Fluent> _fluents;
        private ScenarioDescription _scenarioDescription;
        private List<ScenarioDescription> _savedScenarios;

        private string _scenarioName;

        public string ScenarioName
        {
            get
            {
                return _scenarioName;
            }
            set
            {
                _scenarioName = value;
                if(value == String.Empty)
                {
                    LabelValidationScenario.Content = "It is necessary to fill scenario name.";
                }
                else
                {
                    LabelValidationScenario.Content = String.Empty;
                }
                OnPropertyChanged("ScenarioName");
            }
        }

        #endregion | PROPERTIES |

        #region | PropertyChanged |

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion | PropertyChanged |

        #region | INITIALIZATION |

        public Scenario()
        {
            InitializeComponent();
        }

        public void Initialize(List<Fluent> fluents, List<WorldAction> actions, List<WorldDescriptionRecord> statements)
        {
            ActionAdd.SetActions(actions);
            _scenarioDescription = new ScenarioDescription();
            _savedScenarios = new List<ScenarioDescription>();
            _fluents = fluents;
            ScenarioName = SCENARIONAMETEXTBOXCONTENT;

            InitializeFluents();
            InitializeStatements(statements);

            StackPanelScenarios.Children.Add(new Label()
            {
                Content = "Scenarios:",
                FontSize = 10
            });
        }

        private void InitializeStatements(List<WorldDescriptionRecord> statements)
        {
            StackPanelStatements.Children.Add(new Label()
            {
                Content = "Statements:",
                FontSize = 10
            });

            foreach(WorldDescriptionRecord item in statements)
            {
                StackPanelStatements.Children.Add(new Label()
                {
                    Content = item.ToString(),
                    FontSize = 10
                });
            }
        }

        private void InitializeFluents()
        {
            StackPanelFluents.Children.Add(new Label()
            {
                Content = "Fluents:",
                FontSize = 10
            });
            foreach(Fluent item in _fluents)
            {
                StackPanelFluents.Children.Add(new Label()
                {
                    Content = item.ToString()
                });
            }
        }

        #endregion | INITIALIZATION |

        #region | METHODS |

        private void CleanValues()
        {
            ActionAdd.CleanValues();
            ObservationAdd.CleanValues();
            ActionList.CleanValues();
            LabelValidationScenario.Content = String.Empty;
        }

        #endregion | METHODS |

        #region | EVENTS |

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            if(_savedScenarios.Count==0)
            {
                LabelValidationScenario.Content = "It is necessary to fill scenario name.";
            }
            else
            {
                Switcher.NextPage(_savedScenarios);
            }
        }

        private void ButtonAddAction_Click(object sender, RoutedEventArgs e)
        {
            if(ActionAdd.SelectedWARecordType == null)
            {
                ActionAdd.LabelValidation.Content = "It is necessary to choose an action.";
            }
            else if(ActionList.AddAction(ActionAdd.Time, ActionAdd.SelectedWARecordType.Id, ActionAdd.SelectedWARecordType.Duration))
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
            if(ObservationAdd.Expression == String.Empty)
            {
                ObservationAdd.LabelValidation.Content = "It is necessary to fill expression.";
            }
            else
            {
                ILogicExpression expression = new SimpleLogicExpression(ObservationAdd.Expression);
                if(ValidationExpression(expression) && ActionList.AddObservation(ObservationAdd.Time, ObservationAdd.Expression))
                {
                    _scenarioDescription.observations.Add(new ScenarioObservationRecord(expression, ObservationAdd.Time));
                    ObservationAdd.CleanValues();
                }
                else
                {
                    ObservationAdd.LabelValidation.Content = "Expression is incorrect.";
                }
            }
        }

        private bool ValidationExpression(ILogicExpression expression)
        {
            return ValidationGoodFluents(expression);
        }

        private bool ValidationGoodFluents(ILogicExpression expression)
        {
            string[] fluentNames = expression.GetFluentNames();
            foreach(string item in fluentNames)
            {
                if(!_fluents.Exists(x => x.Name == item))
                    return false;
            }
            return true;
        }

       

        private void AddScenario_Click(object sender, RoutedEventArgs e)
        {
            if(ScenarioName != String.Empty)
            {
                foreach(ScenarioDescription item in _savedScenarios)
                {
                    if(item.Name == ScenarioName)
                    {
                        LabelValidationScenario.Content = "Scenario with this name already exists.";
                        return;
                    }
                }
                _scenarioDescription.Name = ScenarioName;
                StackPanelScenarios.Children.Add(new Label()
                {
                    Content = ScenarioName,
                    FontSize = 10
                });
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
        #endregion | EVENTS |
    }
}