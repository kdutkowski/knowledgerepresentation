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
        private String _expressionEffect;
        private String _expressionIf;


        public EnvCausesIf(ObservableCollection<WorldAction> actionsCollection)
        {
            Actions = actionsCollection;
            InitializeComponent();
            RegisterName("envControl_causesIf", this);
        }


        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            if (ParseAction(ComboBoxAction.SelectedIndex, out errorString)
                && ParseExpression(TextBoxFormEffect.Text, out _expressionEffect, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out _expressionIf, out errorString))
            {
                
               var wdr = new ActionCausesIfRecord(this.SelectedAction, _expressionEffect, _expressionIf);
               CleanValues();
                return wdr;
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        protected override void CleanValues()
        {
            _expressionEffect = "";
            _expressionIf = "";

            ComboBoxAction.SelectedIndex = -1;
            TextBoxFormEffect.Clear();
            TextBoxFormIf.Clear();
            LabelValidation.Content = "";
        }

    }
}
