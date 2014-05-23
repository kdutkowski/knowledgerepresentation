using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.ObjectModel;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    /// <summary>
    /// Interaction logic for EnvImpossibleAt.xaml
    /// </summary>
    public partial class EnvImpossibleAt
    {
        public WorldAction SelectedAction { get; set; }

        public EnvImpossibleAt(ObservableCollection<WorldAction> actionsCollection, ObservableCollection<Fluent> fluentsCollection)
        {
            Fluents = fluentsCollection;
            Actions = actionsCollection;
            InitializeComponent();
            RegisterName("envControl_impossibleAt", this);
        }

        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            if (ParseAction(ComboBoxAction.SelectedIndex, out errorString)
               && ParseTimeLenght(UpDownImpAtTime.Value, out errorString))
            {
                WorldDescriptionRecord wdr = new ImpossibleActionAtRecord(SelectedAction, UpDownImpAtTime.Value.Value);
                CleanValues();
                return wdr;
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        protected override void CleanValues()
        {
            LabelValidation.Content = "";
            UpDownImpAtTime.Value = null;
            ComboBoxAction.SelectedIndex = -1;
        }
    }
}
