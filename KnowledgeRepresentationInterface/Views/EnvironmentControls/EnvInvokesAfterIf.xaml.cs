using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.Generic;
using Action = KnowledgeRepresentationReasoning.World.Action;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    /// <summary>
    /// Interaction logic for EnvInvokesAfterIf.xaml
    /// </summary>
    public partial class EnvInvokesAfterIf
    {
        #region Properties

        private Action _action;
        private Action _result;
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
            if (ParseAction(TextBoxActionStart.Text, out _action, out errorString)
                && ParseAction(TextBoxActionInvoked.Text, out _result, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out _expressionIf, out errorString)
                && ParseTimeLenght(TextBoxTime.Text, out _timeToResult, out errorString))
            {
                return new ActionInvokesAfterIfRecord(_action, _result, _timeToResult, _expressionIf);
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        public override void CleanValues()
        {
            _action = null;
            _result = null;
            _expressionIf = "";
            _timeToResult = 0;

            TextBoxActionStart.Text = "(Action, duration)";
            TextBoxActionInvoked.Text = "(Action2, duration2)";
            TextBoxFormIf.Text = "Pi";
            TextBoxTime.Text = "time lenght";
            LabelValidation.Content = "";

        }

        public override List<Action> GetAllCreatedActions()
        {
            return new List<Action>() {_action, _result};
        }
    }
}
