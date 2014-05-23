using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.ObjectModel;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for EnvCausesIf.xaml
    /// </summary>
    public partial class EnvCausesIf
    {

        public WorldAction SelectedAction { get; set; }

        public EnvCausesIf(ObservableCollection<WorldAction> actionsCollection,
                           ObservableCollection<Fluent> fluentsCollection)
        {
            Fluents = fluentsCollection;
            Actions = actionsCollection;
            InitializeComponent();
            RegisterName("envControl_causesIf", this);
        }


        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            string expressionEffect = "";
            
            if (ParseAction(ComboBoxAction.SelectedIndex, out errorString)
                && ParseExpression(TextBoxFormEffect.Text, out expressionEffect, out errorString))
            {
                //allow empty ifExpresion
                string expressionIf = "";
                ParseExpression(TextBoxFormIf.Text, out expressionIf, out errorString);

                var wdr = new ActionCausesIfRecord(this.SelectedAction, expressionEffect, expressionIf);
                CleanValues();
                return wdr;
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        protected override void CleanValues()
        {
            ComboBoxAction.SelectedIndex = -1;
            TextBoxFormEffect.Clear();
            TextBoxFormIf.Clear();
            LabelValidation.Content = "";
        }
    }
}
