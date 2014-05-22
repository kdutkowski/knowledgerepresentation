namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Controls;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World;

    public abstract class QueControl : UserControl
    {
        public string SelectedScenario { get; set; }

        public List<string> ScenarioNames
        {
            get { return Scenarios.Select(t => t.Name).ToList(); }
        }

        public List<ScenarioDescription> Scenarios { get; set; }

        public List<WorldAction> Actions { get; set; }
        public List<Fluent> Fluents { get; set; }

        protected QueControl()
        {
        }

        protected QueControl(List<ScenarioDescription> scenarios, List<WorldAction> actions, List<Fluent> fluents)
        {
            Scenarios = scenarios;
            Actions = actions;
            Fluents = fluents;
        }

        public abstract Query GetQuery( QuestionType questionType);
    }
}
