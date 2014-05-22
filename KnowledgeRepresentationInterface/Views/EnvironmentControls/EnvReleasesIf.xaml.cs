using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.ObjectModel;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    /// <summary>
    /// Interaction logic for EnvReleasesIf.xaml
    /// </summary>
    public partial class EnvReleasesIf
    {
        public WorldAction SelectedAction { get; set; }
        public Fluent SelectedFluent { get; set; }
        

        public EnvReleasesIf(ObservableCollection<WorldAction> actionsCollection,  ObservableCollection<Fluent> fluentsCollection)
        {
            Actions = actionsCollection;
            Fluents = fluentsCollection;
            InitializeComponent();
            RegisterName("envControl_releasesIf", this);
        }

        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            string expression;
            if (ParseAction(ComboBoxAction.SelectedIndex, out errorString)
                && ParseFluent(ComboBoxFluent.SelectedIndex, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out expression, out errorString))
            {
                WorldDescriptionRecord wdr = new ActionReleasesIfRecord(SelectedAction, SelectedFluent, expression);
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
            ComboBoxFluent.SelectedIndex = -1;
            TextBoxFormIf.Clear();
        }


    }
}
