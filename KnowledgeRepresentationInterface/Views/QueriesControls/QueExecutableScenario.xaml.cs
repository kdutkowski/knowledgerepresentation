using System;
using Xceed.Wpf.Toolkit.Core;

namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    using System.Collections.Generic;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
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

        public QueExecutableScenario(List<ScenarioDescription> scenarios, List<WorldAction> actions, List<Fluent> fluents)
            : base(scenarios, actions, fluents)
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public override Query GetQuery(QuestionType questionType)
        {
            if (String.IsNullOrEmpty(SelectedScenario))
                throw new InvalidContentException("Scenario name is required.");

            var selectedScenario = Scenarios.First(t => t.Name.Equals(SelectedScenario));
            return new ExecutableScenarioQuery(questionType, selectedScenario);
        }

        public override void Clear()
        {
            SelectedScenario = "";
        }
    }
}