using KnowledgeRepresentationInterface.Views;
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KnowledgeRepresentationInterface
{
    using KnowledgeRepresentationReasoning;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _timeInf;
        private List<Fluent> _fluents;
        private List<WorldAction> _actions;
        private List<WorldDescriptionRecord> _statements;


        private Reasoning _reasoning;

        private List<UserControl> _pages;
        private int actualPage = 0;


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

        public void NextPage()
        {
            if (actualPage >= _pages.Count)
                return;
            actualPage++;
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
            ((Scenario)_pages[actualPage]).Initialize(_fluents, _actions);
            this.Navigate(_pages[actualPage]);
        }


        public void PrevPage()
        {
            if (actualPage <= 0)
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
            foreach (var worldDescriptionRecord in statements)
            {
                this._reasoning.AddWorldDescriptionRecord(worldDescriptionRecord);
            }
        }
    }
}
