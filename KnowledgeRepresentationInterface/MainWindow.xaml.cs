using System.Collections.Generic;
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

        private System.String _strStatement;

        public MainWindow()
        {
            InitializeComponent();
            _pages = new List<UserControl>()
                         {
                             new Environment(),
                             new Scenario(),
                             new Results()
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
            ( (Results)_pages[actualPage] ).Initialize(_timeInf, _fluents, _actions, _savedScenarios);
            this.Navigate(_pages[actualPage]);
        }

        public void NextPage(int tInf, List<Fluent> fluents, List<WorldAction> actions,
                            List<WorldDescriptionRecord> statements, System.String strStatement)
        {
            _timeInf = tInf;
            _fluents = fluents;
            _actions = actions;
            _statements = statements;
            _strStatement = strStatement;
            LoadWorldDescriptionRecords(statements);
            actualPage++;
            ( (Scenario)_pages[actualPage] ).Initialize(_fluents, _actions, statements);
            this.Navigate(_pages[actualPage]);
        }

        public void PrevPage()
        {
            if(actualPage <= 0)
                return;
            actualPage--;
            this.Navigate(_pages[actualPage]);
        }

        public QueryResult ExecuteQuery(Query query)
        {
            return _reasoning.ExecuteQuery(query);
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