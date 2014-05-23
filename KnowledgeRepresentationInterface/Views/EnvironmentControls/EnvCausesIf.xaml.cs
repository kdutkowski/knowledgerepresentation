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

        //    private void TextBoxExpression_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //    {
        //        if (Fluents.Count == 0)
        //        {
        //            Keyboard.ClearFocus();
        //            return;
        //        }


        //        ExpressionWindow window = new ExpressionWindow(Fluents);

        //        //todo dla już istniejącego expression
        //        var dialogResult = window.ShowDialog();

        //        if (dialogResult == false)
        //            return;
        //        if (sender.GetType() == typeof(TextBox))
        //            ((TextBox) sender).Text = window.Expression;
        //        else if (sender.GetType() == typeof(WatermarkTextBox))
        //            ((WatermarkTextBox)sender).Text = window.Expression;
        //        Keyboard.ClearFocus();
        //    }

        //}
    }
}
