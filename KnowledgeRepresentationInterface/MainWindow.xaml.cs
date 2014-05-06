using KnowledgeRepresentationInterface.Views;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Action = KnowledgeRepresentationReasoning.World.Action;

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
        private List<Action> _actions;
        private List<WorldDescriptionRecord> _statements;



        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new _Environment());
            ReasoningFacade.Initialize();
        }

        //
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, int tInf, List<Fluent> fluents, List<Action> actions,
                             List<WorldDescriptionRecord> statements)
        {
            _timeInf = tInf;
            _fluents = fluents;
            _actions = actions;
            _statements = statements;
            this.Content = nextPage;
        }

    }
}
