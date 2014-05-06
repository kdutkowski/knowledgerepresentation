using KnowledgeRepresentationReasoning.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    public abstract class QueControl:UserControl
    {
        protected int _timeInf;
        protected List<string> _scenarioNames;
        protected List<WorldAction> _actions;
        protected List<Fluent> _fluents;

        public  QueControl(int timeInf, List<string> scenarioNames, List<WorldAction> actions, List<Fluent> fluents)
        {
            _timeInf = timeInf;
            _scenarioNames = scenarioNames;
            _actions = actions;
            _fluents = fluents;
        }

        public abstract Query GetQuery();
    }
}
