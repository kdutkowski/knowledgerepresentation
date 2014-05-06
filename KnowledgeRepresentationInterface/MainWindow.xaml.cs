using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using KnowledgeRepresentationInterface.Views;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;

using Action = KnowledgeRepresentationReasoning.World.Action;

namespace KnowledgeRepresentationInterface
{
    using KnowledgeRepresentationReasoning;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int T_inf;
        List<Fluent> fluents;
        List<WorldDescriptionRecord> statements;

       

        public MainWindow()
        {
            InitializeComponent();
            Switcher.pageSwitcher = this;
            Switcher.Switch(new _Environment());
            ReasoningFacade.Initialize();
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        //public void Navigate(UserControl nextPage, object state)
        //{
        //    this.Content = nextPage;
        //    ISwitchable s = nextPage as ISwitchable;

        //    if (s != null)
        //        s.UtilizeState(state);
        //    else
        //        throw new ArgumentException("NextPage is not ISwitchable! "
        //          + nextPage.Name.ToString());
        //}
    }
}
