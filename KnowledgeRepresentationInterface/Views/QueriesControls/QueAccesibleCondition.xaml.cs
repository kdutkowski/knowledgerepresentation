namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for QueAccesibleCondition.xaml
    /// </summary>
    public partial class QueAccesibleCondition : QueControl
    {
        public string Condition { get; set; }

        public QueAccesibleCondition()
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public QueAccesibleCondition(List<ScenarioDescription> scenarios, List<WorldAction> actions, List<Fluent> fluents)
            : base(scenarios, actions, fluents)
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public override Query GetQuery(QuestionType questionType)
        {
            throw new System.NotImplementedException();
        }
    }
}