using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using KnowledgeRepresentationInterface.Views.QueriesControls;
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationInterface.Views
{
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World.Records;

    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        #region | PROPERTIES |

        private int _timeInf;

        private readonly List<ScenarioDescription> _savedScenarios;
        private readonly List<WorldAction> _actions;
        private readonly List<Fluent> _fluents;
        private QueryType _selectedQueryType;
        private Dictionary<QueryType, QueControl> _queriesControls;
        
        public QuestionType SelectedQuestionType { get; set; }

        public QueryType SelectedQueryType
        {
            get
            {
                return _selectedQueryType;
            }
            set
            {
                _selectedQueryType = value;
                this.GroupBoxQuery.Content = this._queriesControls[_selectedQueryType];
                NotifyPropertyChanged("SelectedQueryType");
            }
        }

        public IEnumerable<QueryType> QueryRecordType
        {
            get
            {
                return Enum.GetValues(typeof(QueryType)).Cast<QueryType>();
            }
        }

        public IEnumerable<QuestionType> QuestionRecordType
        {
            get
            {
                return Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion | PROPERTIES |

        #region | CONSTRUCTOR & INITIALIZATION |

        public Results()
        {
            _savedScenarios = new List<ScenarioDescription>();
            _actions = new List<WorldAction>();
            _fluents = new List<Fluent>();
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            this._queriesControls = new Dictionary<QueryType, QueControl>
                              {
                                  { QueryType.SatisfyConditionAtTime, new QueConditionAtTime(_savedScenarios, this._actions, this._fluents) },
                                  { QueryType.AccesibleCondition, new QueAccesibleCondition(_savedScenarios, this._actions, this._fluents) },
                                  { QueryType.ExecutableScenario, new QueExecutableScenario(_savedScenarios, this._actions, this._fluents) },
                                  { QueryType.PerformingActionAtTime, new QueActionAtTime(_savedScenarios, this._actions, this._fluents) }
                              };
            SelectedQueryType = QueryType.ExecutableScenario;
        }

        public void Initialize(int tInf, List<Fluent> fluents, List<WorldAction> actions, List<ScenarioDescription> savedScenarios, List<WorldDescriptionRecord> worldDescriptions)
        {
            // TIME
            _timeInf = tInf;

            // ACTIONS
            this._actions.AddRange(actions);
            if(this._actions.Any())
                ( (QueActionAtTime)this._queriesControls[QueryType.PerformingActionAtTime] ).SelectedAction = this._actions.First();
           
            // FLUENTS
            this._fluents.AddRange(fluents);
            
            // SCENARIOS
            _savedScenarios.AddRange(savedScenarios);
            InitControls();
            InitializeInformations(tInf, fluents, actions, savedScenarios, worldDescriptions);
            //LabelFluents.Content = this._fluents.Aggregate(string.Empty, (current, fluent) => current + ( fluent.Name + " " ));
            //LabelActions.Content = this._actions.Aggregate(string.Empty, (current, action) => current + ( action.ToString() + " " ));
            //LabelScenarioDescriptions.Content = savedScenarios.Aggregate(string.Empty, (current, scenario) => current + (scenario.ToString() + "\n"));
            //LabelWorldDescriptions.Content = worldDescriptions.Aggregate(string.Empty, (current, description) => current + (description.ToString() + "\n"));
        }

        private void InitializeInformations(int tInf, List<Fluent> fluents, List<WorldAction> actions, List<ScenarioDescription> savedScenarios, List<WorldDescriptionRecord> worldDescriptions)
        {
            LabelTInf.Content = tInf;
            foreach ( var f in fluents )
            {
                StackPanelFluents.Children.Add(new Label()
                {
                    Content = f.ToString(),
                    FontSize = 10
                });
            }
            foreach (var a in actions)
            {
                StackPanelActions.Children.Add(new Label()
                {
                    Content = a.ToString(),
                    FontSize = 10
                });
            }
            foreach (var f in fluents)
            {
                StackPanelFluents.Children.Add(new Label()
                {
                    Content = f.ToString(),
                    FontSize = 10
                });
            }
            foreach (var sc in savedScenarios)
            {
                StackPanelScenarios.Children.Add(new Label()
                {
                    Content = sc.Name+"\n"+sc.ToString(),
                    FontSize = 10
                });
            }

            foreach (var wd in worldDescriptions)
            {
                StackPanelWorld.Children.Add(new Label()
                {
                    Content = wd.ToString(),
                    FontSize = 10
                });
            }
        }

        #endregion | CONSTRUCTOR & INITIALIZATION |

        #region | EVENTS |

        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            // Wybieramy scenariusz wybrany w aktywnym oknie, jeśli takiego nie ma to wybieramy pierwszy
            ScenarioDescription scenarioDescription = this._savedScenarios
                .FirstOrDefault(t => t.Name.Equals(this._queriesControls[SelectedQueryType].SelectedScenario)) ?? this._savedScenarios.First();

            Query q = this._queriesControls[this.SelectedQueryType].GetQuery(SelectedQuestionType);
            QueryResult qr = Switcher.ExecuteQuery(q, scenarioDescription);
            LabelResult.Content = qr;
        }

        #endregion | EVENTS |

        #region | OTHER |

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion | OTHER |
    }
}