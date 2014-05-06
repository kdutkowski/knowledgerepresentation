using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using KnowledgeRepresentationReasoning.World.Records;
using Action = KnowledgeRepresentationReasoning.World.Action;

namespace KnowledgeRepresentationInterface.Views.EnvironmentControls
{
    public abstract class EnvControl : UserControl
    {
        public abstract WorldDescriptionRecord GetWorldDescriptionRecord();
        public abstract void CleanValues();

        protected virtual bool ParseAction(string actionText, out Action action, out string errorInfo)
        {
            string actionString = actionText;// TextBoxAction.Text;
            action = new Action();
            errorInfo = "";
            try
            {
                var act = actionString.Replace("(", "").Replace(")", "").Split(',');
                
                action.Id = act[0];
                action.Duration = Int32.Parse(act[1]);
                return true;
            }
            catch (Exception e)
            {
                errorInfo = "Action in a wrong format!";
                return false;
            }
        }
    }
}
