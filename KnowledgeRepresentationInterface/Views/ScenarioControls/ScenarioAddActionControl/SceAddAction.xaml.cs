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
            set { _time = value;
             OnPropertyChanged("Time");}
        }

        private WorldDescriptionRecordType _selectedWDRecordType;
        public WorldDescriptionRecordType SelectedWDRecordType
        {
            get { return _selectedWDRecordType; }
            set
            {
                _selectedWDRecordType = value;
                NotifyPropertyChanged("SelectedWDRecordType");
            }
        }
        public IEnumerable<WorldDescriptionRecordType> WDRecordType
        {
            get
            {
                IEnumerable<WorldDescriptionRecordType> tmp = Enum.GetValues(typeof(WorldDescriptionRecordType)).Cast<WorldDescriptionRecordType>();
                return tmp;
            }
        }

        private List<Fluent> _fluents;
        public List<Fluent> Fluents
        {
            get {
                GetFluentsValues();
                return _fluents; }
            set
            {
                _fluents = value;
            }
        }

        public SceAddAction()
        {
            InitializeComponent();
            InitFluents();
        }

        private void InitFluents()
        {
            _fluents = GetFluents();
            _fluents.Add(new Fluent() { Name = "a", Value = true });
            _fluents.Add(new Fluent() { Name = "ab", Value = true });
            _fluents.Add(new Fluent() { Name = "abc", Value = false });
            _fluents.Add(new Fluent() { Name = "ac", Value = true });
            foreach (var item in _fluents)
            {
                StackPanelFluents.Children.Add(new FluentValue { NameFluent=item.Name, Value=item.Value});
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

        private List<Fluent> GetFluents()
        {
            //TODO get fluents from previous page
            return new List<Fluent>();
        }

        public void CleanValues()
        {
            throw new NotImplementedException();
        }

    }
}
