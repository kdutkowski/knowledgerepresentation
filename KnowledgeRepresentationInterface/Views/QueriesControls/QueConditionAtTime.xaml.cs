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
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationInterface.Views.QueriesControls
{
    /// <summary>
    /// Interaction logic for QueConditionAtTime.xaml
    /// </summary>
    public partial class QueConditionAtTime
    {
        public string SelectedScenario { get; set; }

        public QueConditionAtTime()
        {
            InitializeComponent();
            RegisterName("queContr_cond", this);
        }
        public QueConditionAtTime(int timeInf, List<string> scenarioNames, List<WorldAction> actions, List<Fluent> fluents)
            :base(timeInf, scenarioNames, actions, fluents)
        {
            scenarioNames = new List<string>() { "aaa", "vvv" };
            InitializeComponent();
            RegisterName("queContr_cond", this);
            
        }

        public override Query GetQuery()
        {
            QuestionType questionType = QuestionType.Always;
           return new ConditionAtTimeQuery(questionType, TextBoxCondition.Text, Int32.Parse(TextBoxTime.Text));
        }
    }
}
