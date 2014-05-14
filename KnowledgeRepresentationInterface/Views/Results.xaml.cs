﻿using System;
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
using KnowledgeRepresentationInterface.Views.QueriesControls;
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationInterface.Views
{
    using KnowledgeRepresentationReasoning.Scenario;

    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        #region Properties

        private int _timeInf;
        private readonly List<string> _scenarioNames; 
        private List<WorldAction> _actions;
        private List<Fluent> _fluents; 

        #endregion

        #region Visualization Properties
        private Dictionary<QueryType, QueControl> QueriesControls;

        private QueryType _selectedQueryType;
        public QueryType SelectedQuestionType { get; set; }
        public QueryType SelectedQueryType
        {
            get { return _selectedQueryType; }
            set
            {
                _selectedQueryType = value;
                this.GroupBoxQuery.Content = QueriesControls[_selectedQueryType];
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
        #endregion

        #region Constructor
        public Results()
        {
            _scenarioNames = new List<string>();
            _actions = new List<WorldAction>();
            _fluents = new List<Fluent>();
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            QueriesControls = new Dictionary<QueryType, QueControl>
                              {
                                  { QueryType.SatisfyConditionAtTime, new QueConditionAtTime(this._scenarioNames, this._actions, this._fluents) },
                                  { QueryType.AccesibleCondition, new QueAccesibleCondition(this._scenarioNames, this._actions, this._fluents) },
                                  { QueryType.ExecutableScenario, new QueExecutableScenario(this._scenarioNames, this._actions, this._fluents) }, 
                                  { QueryType.PerformingActionAtTime, new QueActionAtTime(this._scenarioNames, this._actions, this._fluents) }
                              };
        }

        public void Initialize(int tInf, List<Fluent> fluents, List<WorldAction> actions, List<ScenarioDescription> savedScenarios)
        {
            _timeInf = tInf;
            this._actions.AddRange(actions);
            if(this._actions.Any())
                ((QueActionAtTime)QueriesControls[QueryType.PerformingActionAtTime]).SelectedAction = this._actions.First();
            this._fluents.AddRange(fluents);
            foreach (var scenario in savedScenarios)
                _scenarioNames.Add(scenario.Name);
            if (_scenarioNames.Count > 0)
            {
                foreach (var queriesControl in QueriesControls.Values)
                    queriesControl.SelectedScenario = _scenarioNames.First();
            }
        }

        #endregion

        #region Property Changed
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            //todo pozostałe typy
            if (SelectedQueryType == QueryType.SatisfyConditionAtTime)
            {
                Query q = ((QueControl) QueriesControls[SelectedQueryType]).GetQuery();
                QueryResult qr = Switcher.ExecuteQuery(q);
                LabelResult.Content = qr;
            }
            
        }

        #region Buttons Events
        

        #endregion
    }
}
