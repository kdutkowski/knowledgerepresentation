using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for Scenario.xaml
    /// </summary>
    public partial class Scenario : UserControl, INotifyPropertyChanged
    {

        #region | PROPERTIES |

        //private int _maxTime;
        private List<Fluent> _fluents;
        private ScenarioDescription _scenarioDescription;
        private ObservableCollection<ScenarioDescription> _savedScenarios;

        public ObservableCollection<ScenarioDescription> SavedScenarios
        {
            get
            {
                return _savedScenarios;
            }
            set
            {
                _savedScenarios = value;
            }
        }
        

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


        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion | PropertyChanged |

        #region | INITIALIZATION |

        public Scenario()
        {
            _savedScenarios = new ObservableCollection<ScenarioDescription>();
            InitializeComponent();
        }

        public void Initialize(List<Fluent> fluents, List<WorldAction> actions, List<WorldDescriptionRecord> statements, int maxTime)
        {
            ActionAdd.SetActions(actions);
            _scenarioDescription = new ScenarioDescription();
            _fluents = fluents;

            InitializeStatements(statements);
            this.ObservationAdd.Fluents = new ObservableCollection<Fluent>(fluents);
            this.ActionAdd.MaxTime = maxTime;
            this.ObservationAdd.MaxTime = maxTime;
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
                if (item.GetType() == typeof(InitialRecord))
                    continue;
                StackPanelStatements.Children.Add(new Label()
                {
                    Content = item.ToString(),
                    FontSize = 10
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
                Switcher.NextPage(_savedScenarios.ToList());
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
                ActionAdd.LabelValidation.Content = "Two actions at the some time are forbidden.";
            }
        }

        private void ButtonAddObservation_Click(object sender, RoutedEventArgs e)
        {
            if ( String.IsNullOrEmpty(ObservationAdd.Expression) )
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
            if (String.IsNullOrEmpty(ScenarioName))
            {
                LabelValidationScenario.Content = "It is necessary to fill scenario name.";
            }
            else
            {
                foreach(ScenarioDescription item in SavedScenarios)
                {
                    if(item.Name == ScenarioName)
                    {
                        LabelValidationScenario.Content = "Scenario with this name already exists.";
                        return;
                    }
                }
                _scenarioDescription.Name = ScenarioName;
                SavedScenarios.Add(_scenarioDescription);
                _scenarioDescription = new ScenarioDescription();
                ScenarioName = "";
                CleanValues();
            }
        }
        #endregion | EVENTS |

        private void RemoveScenario_Click(object sender, RoutedEventArgs e)
        {
            if(ListBoxScenarios.SelectedIndex == -1)
                return;
            var scenarioDescription = (ScenarioDescription)ListBoxScenarios.SelectedValue;
            SavedScenarios.Remove(scenarioDescription);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PrevPage(removeScenarios: false, removeEnvironment: true);
        }
    }
}