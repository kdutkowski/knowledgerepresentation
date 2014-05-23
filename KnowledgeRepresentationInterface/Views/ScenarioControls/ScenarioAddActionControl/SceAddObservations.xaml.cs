using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace KnowledgeRepresentationInterface.Views.ScenarioControls.ScenarioAddActionControl
{
    /// <summary>
    /// Interaction logic for SceAddObservations.xaml
    /// </summary>
    public partial class SceAddObservations : UserControl, INotifyPropertyChanged
    {
        #region | PropertyChanged |

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion | PropertyChanged |

        public SceAddObservations()
        {
            InitializeComponent();
            InitExpression();
        }

        private int _time;

        public int Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        private string _expression;

        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
                if(value == String.Empty)
                {
                    LabelValidation.Content = "It is necessary to fill expression.";
                }
                else
                {
                    LabelValidation.Content = "";
                }
                OnPropertyChanged("Expression");
            }
        }

        private void InitExpression()
        {
            TextBoxExpression.Text = "Expression";
        }

        internal void CleanValues()
        {
            LabelValidation.Content = "";
            TextBoxExpression.Text = "Expression";
            Time = 0;
        }
    }
}