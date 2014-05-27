using System.Collections.ObjectModel;
using System.Windows.Input;
using KnowledgeRepresentationInterface.Views.Helpers;
using Xceed.Wpf.Toolkit;

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
        public ObservableCollection<Fluent> Fluents { get; set; }

        protected QueControl()
        {
        }

        protected QueControl(List<ScenarioDescription> scenarios, List<WorldAction> actions, List<Fluent> fluents)
        {
            Scenarios = scenarios;
            Actions = actions;
            Fluents = new ObservableCollection<Fluent>(fluents);
        }

        protected void WatermarkTextBoxExpression_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {//method implemented for WatermarkTextBox
            var textBox = ((WatermarkTextBox)sender);

            ExpressionWindow window = textBox.Text == "" ? new ExpressionWindow(Fluents) : new ExpressionWindow(Fluents, textBox.Text);

            var dialogResult = window.ShowDialog();

            if (dialogResult == false)
                return;
            textBox.Text = window.Expression;
            Keyboard.ClearFocus();
        }

        public abstract Query GetQuery(QuestionType questionType);
    }
}
