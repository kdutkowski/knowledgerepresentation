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

        private WorldAction worldAction;
        private String _expressionEffect;
        private String _expressionIf;

        #endregion

        #region Constructor

        public EnvCausesIf()
        {
            InitializeComponent();
        }

        #endregion


        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            if (ParseAction(TextBoxAction.Text, out this.worldAction, out errorString)
                && ParseExpression(TextBoxFormEffect.Text, out _expressionEffect, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out _expressionIf, out errorString))
            {
                return new ActionCausesIfRecord(this.worldAction, _expressionEffect, _expressionIf);
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        public override void CleanValues()
        {
            _expressionEffect = "";
            _expressionIf = "";
            this.worldAction = null;

            TextBoxAction.Text = "(Action, duration)";
            TextBoxFormEffect.Text = "Alfa";
            TextBoxFormIf.Text = "Pi";
            LabelValidation.Content = "";
        }

        public override List<WorldAction> GetAllCreatedActions()
        {
            return new List<WorldAction> {this.worldAction};
        }


    }
}
