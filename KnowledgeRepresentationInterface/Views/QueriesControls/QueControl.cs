namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    using System.Collections.Generic;
    using System.Windows.Controls;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.World;

    public abstract class QueControl : UserControl
    {
        public string SelectedScenario { get; set; }
        public List<string> ScenarioNames { get; set; }
        public List<WorldAction> Actions { get; set; }
        public List<Fluent> Fluents { get; set; }

        protected QueControl()
        {
        }

        protected QueControl(List<string> scenarioNames, List<WorldAction> actions, List<Fluent> fluents)
        {
            ScenarioNames = scenarioNames;
            Actions = actions;
            Fluents = fluents;
        }

        public abstract Query GetQuery();
    }
}
