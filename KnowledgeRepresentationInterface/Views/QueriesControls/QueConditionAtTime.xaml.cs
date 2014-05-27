using Xceed.Wpf.Toolkit.Core;

namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    using System;
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for QueConditionAtTime.xaml
    /// </summary>
    public partial class QueConditionAtTime : QueControl
    {
        public QueConditionAtTime()
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public QueConditionAtTime(List<ScenarioDescription> scenarios, List<WorldAction> actions, List<Fluent> fluents)
            : base(scenarios, actions, fluents)
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }

        public override Query GetQuery(QuestionType questionType)
        {
            if (!UpDownTime.Value.HasValue)
                throw new InvalidContentException("Time value is required.");
            if (String.IsNullOrEmpty(TextBoxCondition.Text))
                throw new InvalidContentException("Condition is required.");

            return new ConditionAtTimeQuery(questionType, TextBoxCondition.Text, UpDownTime.Value.Value);
        }

        public override void Clear()
        {
            UpDownTime.Value = null;
            TextBoxCondition.Text = null;
        }
    }
}