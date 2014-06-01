using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using KnowledgeRepresentationInterface.Views;
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;

namespace KnowledgeRepresentationInterface
{
    using KnowledgeRepresentationReasoning;
    using KnowledgeRepresentationReasoning.Scenario;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _timeInf;
        private List<Fluent> _fluents;
        private List<WorldAction> _actions;
        private List<WorldDescriptionRecord> _statements;
        private List<ScenarioDescription> _savedScenarios;

        private Reasoning _reasoning;

        private List<UserControl> _pages;
        private int actualPage = 0;

        private UserControl _environment;
        private UserControl _scenario;
        private UserControl _results;

        public MainWindow()
        {
            InitializeComponent();
            _environment = new KnowledgeRepresentationInterface.Views.Environment();
            _scenario = new Scenario();
            _results = new Results();
            _pages = new List<UserControl>()
                         {
                             _environment,
                             _scenario,
                             _results
                         };
            Switcher.pageSwitcher = this;
            this.Navigate(_pages[actualPage]);
            Reasoning.Initialize();
            this._reasoning = new Reasoning();
        }

        private void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void NextPage(List<ScenarioDescription> savedScenarios)
        {
            _savedScenarios = savedScenarios;
            if(actualPage >= _pages.Count)
                return;
            actualPage++;
            _reasoning.AddScenarioDescriptionList(_savedScenarios);
            ( (Results)_pages[actualPage] ).Initialize(_timeInf, _fluents, _actions, _savedScenarios, _statements);
            this.Navigate(_pages[actualPage]);
        }

        public void NextPage(int tInf, List<Fluent> fluents, List<WorldAction> actions,
                            List<WorldDescriptionRecord> statements)
        {
            _timeInf = tInf;
            _fluents = fluents;
            _actions = actions;
            _statements = statements;
            LoadWorldDescriptionRecords(statements);
            actualPage++;
            ( (Scenario)_pages[actualPage] ).Initialize(_fluents, _actions, statements, tInf);
            this.Navigate(_pages[actualPage]);
        }

        public void PrevPage()
        {
            if(actualPage <= 0)
                return;
            actualPage--;
            this.Navigate(_pages[actualPage]);
        }
        public void PrevPage(bool removeScenarios, bool removeEnvironment)
        {
            if (removeScenarios)
            {
                _reasoning.RemoveScenarioDescriptionList(_savedScenarios);
                _savedScenarios = null;
                _results = new Results();
                _pages[2] = _results;
            }

            if (removeEnvironment)
            {
                foreach (var worldDescriptionRecord in _statements)
                {
                    _reasoning.RemoveWorldDescriptionRecord(worldDescriptionRecord);
                }
                foreach (var worldDescriptionRecord in _statements)
                {
                    if (worldDescriptionRecord.GetType() == typeof(InitialRecord))
                        ((Environment) _environment).Statements.Remove(worldDescriptionRecord);
                }
                
                _statements = null;
                _fluents = null;
                _actions = null;
                _timeInf = 0;
                _scenario = new Scenario();
                _pages[1] = _scenario;
            }


            if (actualPage <= 0)
                return;
            actualPage--;
            this.Navigate(_pages[actualPage]);
        }

        public QueryResult ExecuteQuery(Query query, ScenarioDescription scenarioDescription)
        {
            return _reasoning.ExecuteQuery(query, scenarioDescription);
        }

        private void LoadWorldDescriptionRecords(List<WorldDescriptionRecord> statements)
        {
            foreach(var worldDescriptionRecord in statements)
            {
                this._reasoning.AddWorldDescriptionRecord(worldDescriptionRecord);
            }
        }
    }
}