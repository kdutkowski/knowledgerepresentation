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

namespace KnowledgeRepresentationInterface.Views.ScenarioControls.ScenarioAddActionControl
{
    /// <summary>
    /// Interaction logic for SceAddAction.xaml
    /// </summary>
    public partial class SceAddAction : UserControl, ISceControl
    {
        public SceAddAction()
        {
            InitializeComponent();
        }

        public void CleanValues()
        {
            throw new NotImplementedException();
        }

        private void ButtonAddAction_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
