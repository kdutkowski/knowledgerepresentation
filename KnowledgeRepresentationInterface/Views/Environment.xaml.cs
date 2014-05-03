using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using KnowledgeRepresentationInterface.Views.EnvironmentControls;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for _Environment.xaml
    /// </summary>
    public partial class _Environment : UserControl, INotifyPropertyChanged//, ISwitchable
    {
        private List<Fluent> _fluents;
        private String _strFluent;
        public String FluentString
        {
            get
            {
                return _strFluent;
            }
            set
            {
                if (value != "")
                    _strFluent = value + "\r\n";
                NotifyPropertyChanged("FluentString");
            }
        }

        private String _strStatement;
        public String StatementsString
        {
            get
            {
                return _strStatement;
            }
            set
            {
                if (value != "")
                    _strStatement = value + "\r\n";
                NotifyPropertyChanged("StatementsString");
            }
        }

        private WorldDescriptionRecordType _selectedWDRecordType;
        public WorldDescriptionRecordType SelectedWDRecordType
        {
            get { return _selectedWDRecordType; }
            set
            {
                _selectedWDRecordType = value;
                //this.GruopBoxStatements.Content
                this.GruopBoxStatements.Content = StatementsControls[_selectedWDRecordType];
                NotifyPropertyChanged("SelectedWDRecordType");
            }
        }
        public IEnumerable<WorldDescriptionRecordType> WDRecordType
        {
            get
            {
                IEnumerable<WorldDescriptionRecordType> tmp = Enum.GetValues(typeof(WorldDescriptionRecordType)).Cast<WorldDescriptionRecordType>();
                //tmp.
                return tmp;
            }
        }

        Dictionary<WorldDescriptionRecordType, UserControl> StatementsControls;

        
        public _Environment()
        {
            _fluents = new List<Fluent>();
            InitControls();
            InitializeComponent();
            this.GruopBoxStatements.Content = StatementsControls[WorldDescriptionRecordType.Initially];
        }

        private void InitControls()
        {
            StatementsControls = new Dictionary<WorldDescriptionRecordType, UserControl>();
            StatementsControls.Add(WorldDescriptionRecordType.ActionCausesIf, new EnvCausesIf());
            StatementsControls.Add(WorldDescriptionRecordType.ActionInvokesAfterIf, new EnvInvokesAfterIf());
            StatementsControls.Add(WorldDescriptionRecordType.ActionReleasesIf, new EnvReleasesIf());
            StatementsControls.Add(WorldDescriptionRecordType.ExpressionTriggersAction, new EnvTriggers());
            StatementsControls.Add(WorldDescriptionRecordType.ImpossibleActionAt, new EnvImpossibleAt());
            StatementsControls.Add(WorldDescriptionRecordType.ImpossibleActionIf, new EnvImpossibleIf());
            StatementsControls.Add(WorldDescriptionRecordType.Initially, new UserControl());
            StatementsControls.Add(WorldDescriptionRecordType.AlwaysExpression, new UserControl());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new _Scenario());
        }

        private void ButtonAddFluent_Click(object sender, RoutedEventArgs e)
        {
            if (!_fluents.Exists(f => (f.Name == TextBoxFluents.Text)))
            {
                Fluent f = new Fluent();
                f.Name = TextBoxFluents.Text;
                _fluents.Add(f);
                FluentString += TextBoxFluents.Text;
            }
            else
            {
                LabelFluentsValidation.Content = "Fluent with this name already exists.";
            }
        }

        private void ButtonRemoveFluent_Click(object sender, RoutedEventArgs e)
        {
            if (_fluents.Exists(f => (f.Name == TextBoxFluents.Text)))
            {
                _fluents.Remove(_fluents.FirstOrDefault(f => (f.Name == TextBoxFluents.Text)));
                string tmp = "";
                for (int i = 0; i < _fluents.Count; i++)
                    tmp += (i != 0 ? "\r\n" : "") + _fluents[i].Name;
                _strFluent = "";
                
                FluentString = tmp;
            }
            else
            {
                LabelFluentsValidation.Content = "Fluent with this name does not exist.";
            }
        }

        //#region ISwitchable Members
        //public void UtilizeState(object state)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        
    }
}
