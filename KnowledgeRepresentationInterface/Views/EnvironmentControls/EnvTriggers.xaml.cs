using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.ObjectModel;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    /// <summary>
    /// Interaction logic for EnvTriggers.xaml
    /// </summary>
    public partial class EnvTriggers
    {
        public WorldAction SelectedAction { get; set; }

        public EnvTriggers(ObservableCollection<WorldAction> actionsCollection)
        {
            Actions = actionsCollection;
            InitializeComponent();
            RegisterName("envControl_triggers", this);
        }

        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            string expression;
            if (ParseAction(ComboBoxAction.SelectedIndex, out errorString)
                && ParseExpression(TextBoxForm.Text, out expression, out errorString))
            {

                var wdr = new ExpressionTriggersActionRecord(SelectedAction, expression);
                CleanValues();
                return wdr;
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        protected override void CleanValues()
        {
            LabelValidation.Content = "";
            ComboBoxAction.SelectedIndex = -1;
            TextBoxForm.Clear();
        }
    }
}
