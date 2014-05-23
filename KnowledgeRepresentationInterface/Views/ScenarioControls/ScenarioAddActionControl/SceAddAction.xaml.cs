namespace KnowledgeRepresentationInterface.Views.ScenarioControls.ScenarioAddActionControl
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    using KnowledgeRepresentationReasoning.World;

    /// <summary>
    /// Interaction logic for SceAddAction.xaml
    /// </summary>
    public partial class SceAddAction : UserControl, INotifyPropertyChanged
    {
        //TODO implement validation Time

        #region PropertyChanged

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

        #endregion PropertyChanged

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

        private WorldAction _selectedWARecordType;

        public WorldAction SelectedWARecordType
        {
            get
            {
                return _selectedWARecordType;
            }
            set
            {
                _selectedWARecordType = value;
                OnPropertyChanged("SelectedWARecordType");
            }
        }

        private List<WorldAction> _actions = new List<WorldAction>();

        public IEnumerable<WorldAction> WARecordType
        {
            get
            {
                return this._actions;
            }
            set
            {
                _actions = value.ToList();
                OnPropertyChanged("WARecordType");
            }
        }

        public SceAddAction()
        {
            InitializeComponent();
        }

        public void SetActions(List<WorldAction> actions)
        {
            WARecordType = actions;
        }

        internal void CleanValues()
        {
            LabelValidation.Content = "";
            SelectedWARecordType = null;
            Time = 0;
        }
    }
}