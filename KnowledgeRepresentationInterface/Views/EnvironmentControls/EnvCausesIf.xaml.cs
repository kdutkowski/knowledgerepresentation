using System.Collections.ObjectModel;
using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.Generic;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for EnvCausesIf.xaml
    /// </summary>
    public partial class EnvCausesIf
    {
        #region Properties

        public WorldAction SelectedAction { get; set; }
        public ObservableCollection<WorldAction> Actions { get; set; }
        private String _expressionEffect;
        private String _expressionIf;

        #endregion

        #region Constructor

        public EnvCausesIf(ObservableCollection<WorldAction> actionsCollection)
        {
            Actions = actionsCollection;
            InitializeComponent();
            RegisterName("envControl_causesIf", this);
        }

        #endregion


        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            if (ParseAction(ComboBoxActions.SelectedIndex, out errorString)
                && ParseExpression(TextBoxFormEffect.Text, out _expressionEffect, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out _expressionIf, out errorString))
            {
                LabelValidation.Content = "";
                return new ActionCausesIfRecord(this.SelectedAction, _expressionEffect, _expressionIf);
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        public override void CleanValues()
        {
            _expressionEffect = "";
            _expressionIf = "";
            this.SelectedAction = null;

            ComboBoxActions.SelectedIndex = -1;
            SelectedAction = null;
            TextBoxFormEffect.Text = "Alfa";
            TextBoxFormIf.Text = "Pi";
            LabelValidation.Content = "";
        }

    }
}
