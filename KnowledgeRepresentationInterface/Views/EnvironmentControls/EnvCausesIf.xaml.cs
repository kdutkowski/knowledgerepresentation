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

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    /// <summary>
    /// Interaction logic for EnvCausesIf.xaml
    /// </summary>
    public partial class EnvCausesIf : UserControl, IEnvControl
    {
        public KnowledgeRepresentationReasoning.World.Action action;
        public String formulaEffect;
        public String formulaIf;

        public EnvCausesIf()
        {
            InitializeComponent();
        }

        public WorldDescriptionRecord getWorldDescriptionRecord()
        {
            //WorldDescriptionRecord wdr = new WorldDescriptionRecord();
            //wdr.Type = WorldDescriptionRecordType.ActionCausesIf;
            //return
            bool worked1 = parseAction();
            bool worked2 = parseFormulaEffect();
            bool worked3 = parseFormulaIf();
            throw new NotImplementedException();
        }

        //public bool Validate()
        //{
        //    throw new NotImplementedException();
        //}

        public void CleanValues()
        {
            formulaEffect = "";
            formulaIf = "";
            action.Duration = null;
            action.Id = "";

            TextBoxAction.Text = "(Action, duration)";
            TextBoxFormEffect.Text = "Alfa";
            TextBoxFormIf.Text = "Pi";
        }


        private bool parseAction()
        {
            string actionString = TextBoxAction.Text;
            try
            {
                var act = actionString.Replace("(", "").Replace(")", "").Split(',');
                action = new KnowledgeRepresentationReasoning.World.Action();
                action.Id = act[0];
                action.Duration = Int32.Parse(act[1]);
                return true;
            }
            catch (Exception e)
            {
                LabelValidation.Content = "Action in a wrong format!";
                return false;
            }
        }

        private bool parseFormulaEffect()
        {
            
            formulaEffect = TextBoxFormEffect.Text;
            if (formulaEffect == "")
            {
                LabelValidation.Content = "All fields are reqiured";
                return false;
            }
            return true;
        }

        private bool parseFormulaIf()
        {
            formulaIf = TextBoxFormIf.Text;
            if (formulaIf == "")
            {
                LabelValidation.Content = "All fields are reqiured";
                return false;
            }
            return true;
        }
    }
}
