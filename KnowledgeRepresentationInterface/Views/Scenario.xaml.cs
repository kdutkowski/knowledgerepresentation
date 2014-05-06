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

using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;

using Action = KnowledgeRepresentationReasoning.World.Action;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for _Scenario.xaml
    /// </summary>
    public partial class _Scenario : UserControl
    {
        public _Scenario(List<Fluent> fluents, List<Action> actions  )
        {
            InitializeComponent();
        }

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new _Results());
        }

        private void ButtonAddAction_Click(object sender, RoutedEventArgs e)
        {
            //TODO name of action
            ActionList.AddAction(Action.Time, Action.Fluents, "action");
            //TODO add observation
        }

    }
}
