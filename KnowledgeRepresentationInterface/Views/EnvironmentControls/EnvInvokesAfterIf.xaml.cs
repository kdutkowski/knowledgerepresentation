using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.Generic;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for EnvInvokesAfterIf.xaml
    /// </summary>
    public partial class EnvInvokesAfterIf
    {
        #region Properties

        private WorldAction worldAction;
        private WorldAction _result;
        private string _expressionIf;
        private int _timeToResult;

        #endregion

        public EnvInvokesAfterIf()
        {
            InitializeComponent();
        }

        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            if (ParseAction(TextBoxActionStart.Text, out this.worldAction, out errorString)
                && ParseAction(TextBoxActionInvoked.Text, out _result, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out _expressionIf, out errorString)
                && ParseTimeLenght(TextBoxTime.Text, out _timeToResult, out errorString))
            {
                return new ActionInvokesAfterIfRecord(this.worldAction, _result, _timeToResult, _expressionIf);
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        public override void CleanValues()
        {
            this.worldAction = null;
            _result = null;
            _expressionIf = "";
            _timeToResult = 0;

            TextBoxActionStart.Text = "(Action, duration)";
            TextBoxActionInvoked.Text = "(Action2, duration2)";
            TextBoxFormIf.Text = "Pi";
            TextBoxTime.Text = "time lenght";
            LabelValidation.Content = "";

        }

        public override List<WorldAction> GetAllCreatedActions()
        {
            return new List<WorldAction>() {this.worldAction, _result};
        }
    }
}
