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

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for _Results.xaml
    /// </summary>
    public partial class _Results : UserControl, ISwitchable
    {
        public _Results()
        {
            InitializeComponent();
        }

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new _Environment());
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
