using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.ObjectModel;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    /// <summary>
    /// Interaction logic for ImpossibleIf.xaml
    /// </summary>
    public partial class EnvImpossibleIf
    {
        public WorldAction SelectedAction { get; set; }

        public EnvImpossibleIf(ObservableCollection<WorldAction> actionsCollection)
        {
            Actions = actionsCollection;
            InitializeComponent();
            RegisterName("envControl_impossibleIf", this);
        }

        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            string expression;
            if (ParseAction(ComboBoxAction.SelectedIndex, out errorString)
               && ParseExpression(TextBoxForm.Text, out expression, out errorString))
            {
                WorldDescriptionRecord wdr = new ImpossibleActionIfRecord(SelectedAction, expression);
                CleanValues();
                return wdr;
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        protected override void CleanValues()
        {
            LabelValidation.Content = "";
            TextBoxForm.Clear();
            ComboBoxAction.SelectedIndex = -1;
        }
    }
}
