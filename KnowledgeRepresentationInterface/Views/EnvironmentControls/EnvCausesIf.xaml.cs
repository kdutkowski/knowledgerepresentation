using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KnowledgeRepresentationReasoning.World.Records;
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
            bool worked1 = ParseAction(TextBoxAction.Text, out _action, out errorString);
            LabelValidation.Content = errorString;

            bool worked2 = parseExpressionEffect();
            bool worked3 = parseExpressionIf();

            if (worked1 && worked2 && worked3)
            {
                return new ActionCausesIfRecord(_action, _expressionEffect, _expressionIf);
            }
            throw new TypeLoadException("Validation error");
        }

        public override void CleanValues()
        {
            _expressionEffect = "";
            _expressionIf = "";
            _action.Duration = null;
            _action.Id = "";

            TextBoxAction.Text = "(Action, duration)";
            TextBoxFormEffect.Text = "Alfa";
            TextBoxFormIf.Text = "Pi";
        }

        private bool parseExpressionEffect()
        {
            
            _expressionEffect = TextBoxFormEffect.Text;
            if (_expressionEffect == "")
            {
                LabelValidation.Content = "All fields are reqiured";
                return false;
            }
            return true;
        }

        private bool parseExpressionIf()
        {
            _expressionIf = TextBoxFormIf.Text;
            if (_expressionIf == "")
            {
                LabelValidation.Content = "All fields are reqiured";
                return false;
            }
            return true;
        }
    }
}
