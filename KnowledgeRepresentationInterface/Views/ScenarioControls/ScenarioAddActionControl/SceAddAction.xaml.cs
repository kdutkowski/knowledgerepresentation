using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class SceAddAction : UserControl, ISceControl, INotifyPropertyChanged
    {
        //TODO implement validation Time
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int _time;

        public int Time
        {
            get { return _time; }
            set { _time = value;
             OnPropertyChanged("Time");}
        }
        
        
        
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
            LabelValidation.Content = _time.ToString();
        }
    }
}
