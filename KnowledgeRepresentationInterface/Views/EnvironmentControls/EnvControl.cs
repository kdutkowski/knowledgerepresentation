using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    using KnowledgeRepresentationReasoning.World;

    public abstract class EnvControl : UserControl
    {
        public ObservableCollection<WorldAction> Actions { get; set; }
        public ObservableCollection<Fluent> Fluents { get; set; }

        public abstract WorldDescriptionRecord GetWorldDescriptionRecord();

        protected abstract void CleanValues();

        protected virtual bool ParseFluent(int selectedIndex, out string errorInfo)
        {
            if (selectedIndex == -1)
            {
                errorInfo = "Fluent is required.";
                return false;
            }
            errorInfo = "";
            return true;
        }

        protected virtual bool ParseAction(int selectedIndex, out string errorInfo)
        {
            if (selectedIndex == -1)
            {
                errorInfo = "Action is required.";
                return false;
            }
            errorInfo = "";
            return true;
        }

        protected virtual bool ParseExpression(string expressionText, out string expression, out string errorInfo)
        {
            expression = expressionText;
            errorInfo = "";
            if (expression == "")
            {
                errorInfo = "Expression is reqiured";
                return false;
            }
            return true;
        }

        protected virtual bool ParseTimeLenght(int? value, out string errorInfo)
        {
            if (!value.HasValue)
            {
                errorInfo = "Time value is required.";
                return false;
            }
            errorInfo = "";
            return true;
        }

    }
}
