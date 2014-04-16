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

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for _Environment.xaml
    /// </summary>
    public partial class _Environment : UserControl, INotifyPropertyChanged//, ISwitchable
    {
        List<string> fluents;
        private String str;
        public String fluentString
        {
            get
            {
                return str;
            }
            set
            {
                if (value != "")
                    str = value + "\r\n";
                NotifyPropertyChanged();
            }
        }

        private String strStatement;
        public String statementsString
        {
            get
            {
                return strStatement;
            }
            set
            {
                if (value != "")
                    strStatement = value + "\r\n";
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public _Environment()
        {
            fluents = new List<string>();
            //fluentString = "aaaa\r\nbbb";
            InitializeComponent();
        }

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new _Scenario());
        }

        private void ButtonAddFluent_Click(object sender, RoutedEventArgs e)
        {
            fluentString += TextBoxFluents.Text;// +"\r\n";
        }

        private void ButtonRemoveFluent_Click(object sender, RoutedEventArgs e)
        {
            //fluentString += TextBoxFluents.Text;// +"\r\n";
        }

        //#region ISwitchable Members
        //public void UtilizeState(object state)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        
    }
}
