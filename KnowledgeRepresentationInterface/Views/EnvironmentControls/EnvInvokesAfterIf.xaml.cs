using System.Collections.ObjectModel;
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
        public WorldAction SelectedActionStart { get; set; }
        public WorldAction SelectedActionResult { get; set; }
        public ObservableCollection<WorldAction> Actions { get; set; }
        private string _expressionIf;
        private int _timeToResult;

        #endregion

        public EnvInvokesAfterIf(ObservableCollection<WorldAction> actionsCollection)
        {
            Actions = actionsCollection;
            InitializeComponent();
            RegisterName("envControl_invokesAfterIf", this);
        }

        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            string errorString;
            if (ParseAction(ComboBoxActionsStart.SelectedIndex, out errorString)
                && ParseAction(ComboBoxActionResult.SelectedIndex, out errorString)
                && ParseExpression(TextBoxFormIf.Text, out _expressionIf, out errorString)
                && ParseTimeLenght(UpDownTime.Value, out errorString))
            {
                LabelValidation.Content = "";
                return new ActionInvokesAfterIfRecord(SelectedActionStart, SelectedActionResult, UpDownTime.Value.Value, _expressionIf);
            }

            LabelValidation.Content = errorString;
            throw new TypeLoadException("Validation error");
        }

        public override void CleanValues()
        {
            _expressionIf = "";
            _timeToResult = 0;

            UpDownTime.Value = null;
            ComboBoxActionResult.SelectedIndex = -1;
            ComboBoxActionsStart.SelectedIndex = -1;
            TextBoxFormIf.Text = "Pi";
            LabelValidation.Content = "";

        }
    }
}
