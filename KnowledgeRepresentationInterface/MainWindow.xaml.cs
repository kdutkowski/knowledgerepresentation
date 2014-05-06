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


        private Reasoning reasoning;


        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new _Environment());
            Reasoning.Initialize();
            this.reasoning = new Reasoning();
        }

        //
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, int tInf, List<Fluent> fluents, List<WorldAction> actions,
                             List<WorldDescriptionRecord> statements)
        {
            _timeInf = tInf;
            _fluents = fluents;
            _actions = actions;
            _statements = statements;
            LoadWorldDescriptionRecords(statements);
            this.Content = nextPage;
        }

        public QueryResult ExecuteQuery(Query query)
        {
            return reasoning.ExecuteQuery(query);
        }

        private void LoadWorldDescriptionRecords(List<WorldDescriptionRecord> statements)
        {
            foreach (var worldDescriptionRecord in statements)
            {
                this.reasoning.AddWorldDescriptionRecord(worldDescriptionRecord);
            }
        }
    }
}
