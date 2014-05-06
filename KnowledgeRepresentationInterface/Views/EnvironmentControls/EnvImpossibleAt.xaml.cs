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
    /// Interaction logic for EnvImpossibleAt.xaml
    /// </summary>
    public partial class EnvImpossibleAt
    {
        public EnvImpossibleAt()
        {
            InitializeComponent();
        }

        public override WorldDescriptionRecord GetWorldDescriptionRecord()
        {
            throw new NotImplementedException();
        }
        public override void CleanValues()
        {
            throw new NotImplementedException();
        }
    }
}
