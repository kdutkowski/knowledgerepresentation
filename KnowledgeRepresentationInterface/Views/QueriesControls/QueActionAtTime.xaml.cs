using Xceed.Wpf.Toolkit.Core;

namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    using System;
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
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

        public QueActionAtTime(List<ScenarioDescription> scenarios, List<WorldAction> actions, List<Fluent> fluents)
            : base(scenarios, actions, fluents)
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public override Query GetQuery(QuestionType questionType)
        {
            if (!TextBoxTime.Value.HasValue)
                throw new InvalidContentException("Time is empty.");
            return new PerformingActionAtTimeQuery(questionType, SelectedAction, TextBoxTime.Value.Value);//Int32.Parse(TextBoxTime.Text)
        }
    }
}