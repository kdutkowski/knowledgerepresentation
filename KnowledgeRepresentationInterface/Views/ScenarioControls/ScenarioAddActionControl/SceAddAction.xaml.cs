using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace KnowledgeRepresentationInterface.Views.ScenarioControls.ScenarioAddActionControl
{
    /// <summary>
    /// Interaction logic for SceAddAction.xaml
    /// </summary>
    public partial class SceAddAction : UserControl, ISceControl, INotifyPropertyChanged
    {

        //TODO implement validation Time
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private int _time;
        public int Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }
        private WorldAction _selectedWARecordType;

        public WorldAction SelectedWARecordType
        {
            get { return _selectedWARecordType; }
            set
            {
                _selectedWARecordType = value;
                NotifyPropertyChanged("SelectedWARecordType");
            }
        }

        public List<WorldAction> WARecordType
        {
            get;
            set;
        }


        private List<Fluent> _fluents;
        public List<Fluent> Fluents
        {
            get
            {
                GetFluentsValues();
                return _fluents;
            }
            set
            {
                _fluents = value;
            }
        }

        public SceAddAction()
        {
            InitializeComponent();
        }

        private void InitFluents()
        {
            foreach (var item in _fluents)
            {
                StackPanelFluents.Children.Add(new FluentValue { NameFluent = item.Name, Value = item.Value });
            }
        }

        private void GetFluentsValues()
        {
            _fluents.Clear();
            foreach (var item in StackPanelFluents.Children)
            {
                _fluents.Add(new Fluent() { Name = ((FluentValue)item).NameFluent, Value = ((FluentValue)item).Value });
            }
        }

        public void CleanValues()
        {
            throw new NotImplementedException();
        }


        public void SetFluents(List<Fluent> fluents)
        {
            _fluents = (fluents != null) ? fluents : new List<Fluent>();
            InitFluents();
        }

        public void SetActions(List<WorldAction> actions)
        {
            WARecordType = actions;
        }
    }
}
