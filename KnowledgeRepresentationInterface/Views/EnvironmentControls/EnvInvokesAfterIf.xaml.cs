using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.ObjectModel;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for EnvInvokesAfterIf.xaml
    /// </summary>
    public partial class EnvInvokesAfterIf
    {
        public WorldAction SelectedActionStart { get; set; }
        public WorldAction SelectedActionResult { get; set; }

        public EnvInvokesAfterIf(ObservableCollection<WorldAction> actionsCollection, ObservableCollection<Fluent> fluentsCollection)
        {
            Fluents = fluentsCollection;
            Actions = actionsCollection;
            InitializeComponent();
            RegisterName("envControl_invokesAfterIf", this);
        }

        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            string expression;
            if (ParseAction(ComboBoxActionStart.SelectedIndex, out errorString)
                && ParseAction(ComboBoxActionResult.SelectedIndex, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out expression, out errorString)
                && ParseTimeLenght(UpDownInvAftIfTime.Value, out errorString))
            {
                
                var wdr =  new ActionInvokesAfterIfRecord(SelectedActionStart, SelectedActionResult, UpDownInvAftIfTime.Value.Value, expression);
                CleanValues();
                return wdr;
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        protected override void CleanValues()
        {

            UpDownInvAftIfTime.Value = null;
            ComboBoxActionResult.SelectedIndex = -1;
            ComboBoxActionStart.SelectedIndex = -1;
            TextBoxFormIf.Clear();
            LabelValidation.Content = "";
        }
    }
}
