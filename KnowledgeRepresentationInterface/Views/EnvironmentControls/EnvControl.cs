using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using KnowledgeRepresentationInterface.Views.Helpers;
using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    using KnowledgeRepresentationReasoning.World;

    public abstract class EnvControl : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<WorldAction> Actions { get; set; }
        public ObservableCollection<Fluent> Fluents { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract WorldDescriptionRecord GetWorldDescriptionRecord();

        protected abstract void CleanValues();

        #region Property Changed
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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

        protected void WatermarkTextBoxExpression_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {//method implemented for WatermarkTextBox
            //if (Fluents.Count == 0)
            //{
            //    Keyboard.ClearFocus();
            //    return;
            //}
            var textBox = ((WatermarkTextBox)sender);

            ExpressionWindow window = textBox.Text == "" ? new ExpressionWindow(Fluents) : new ExpressionWindow(Fluents, textBox.Text);

            var dialogResult = window.ShowDialog();

            if (dialogResult == false)
                return;
            textBox.Text = window.Expression;
            Keyboard.ClearFocus();
        }
    }
}
