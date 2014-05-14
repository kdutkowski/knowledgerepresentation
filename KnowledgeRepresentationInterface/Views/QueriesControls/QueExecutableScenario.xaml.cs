namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    using System;
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for QueExecutableScenario.xaml
    /// </summary>
    public partial class QueExecutableScenario : QueControl
    {
        public QueExecutableScenario()
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }
        public QueExecutableScenario(List<string> scenarioNames, List<WorldAction> actions, List<Fluent> fluents)
            :base(scenarioNames, actions, fluents)
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public override Query GetQuery()
        {
           throw new NotImplementedException();
        }
    }
}
