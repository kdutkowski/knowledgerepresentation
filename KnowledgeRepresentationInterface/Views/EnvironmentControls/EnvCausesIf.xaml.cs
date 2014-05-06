using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.Generic;
using Action = KnowledgeRepresentationReasoning.World.Action;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    /// <summary>
    /// Interaction logic for EnvCausesIf.xaml
    /// </summary>
    public partial class EnvCausesIf
    {
        #region Properties

        private Action _action;
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
            if (ParseAction(TextBoxAction.Text, out _action, out errorString)
                && ParseExpression(TextBoxFormEffect.Text, out _expressionEffect, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out _expressionIf, out errorString))
            {
                return new ActionCausesIfRecord(_action, _expressionEffect, _expressionIf);
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        public override void CleanValues()
        {
            _expressionEffect = "";
            _expressionIf = "";
            _action = null;

            TextBoxAction.Text = "(Action, duration)";
            TextBoxFormEffect.Text = "Alfa";
            TextBoxFormIf.Text = "Pi";
            LabelValidation.Content = "";
        }

        public override List<Action> GetAllCreatedActions()
        {
            return new List<Action> {_action};
        }


    }
}
