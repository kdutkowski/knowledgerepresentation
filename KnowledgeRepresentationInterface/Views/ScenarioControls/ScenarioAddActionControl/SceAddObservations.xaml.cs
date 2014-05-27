using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using KnowledgeRepresentationInterface.Views.Helpers;
using KnowledgeRepresentationReasoning.World;
using Xceed.Wpf.Toolkit;

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
            Fluents = new ObservableCollection<Fluent>();
            InitializeComponent();
        }

        public ObservableCollection<Fluent> Fluents; 

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
                
                OnPropertyChanged("Expression");
            }
        }

        internal void CleanValues()
        {
            LabelValidation.Content = "";
            this.Expression = String.Empty;
            //TextBoxExpression.Text = "";
            Time = 0;
        }

        protected void WatermarkTextBoxExpression_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {//method implemented for WatermarkTextBox
            var textBox = ((WatermarkTextBox)sender);

            ExpressionWindow window = textBox.Text == "" ? new ExpressionWindow(Fluents) : new ExpressionWindow(Fluents, textBox.Text);

            var dialogResult = window.ShowDialog();

            if (dialogResult == false)
                return;
            textBox.Text = window.Expression;
            Keyboard.ClearFocus();
        }
    }
}