namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for QueActionAtTime.xaml
    /// </summary>
    public partial class QueActionAtTime : QueControl
    {
        public WorldAction SelectedAction { get; set; }

        public QueActionAtTime()
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public QueActionAtTime(List<string> scenarioNames, List<WorldAction> actions, List<Fluent> fluents)
            : base(scenarioNames, actions, fluents)
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public override Query GetQuery()
        {
            throw new System.NotImplementedException();
        }
    }
}
