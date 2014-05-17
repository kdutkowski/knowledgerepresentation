using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using KnowledgeRepresentationInterface.Views.EnvironmentControls;
using KnowledgeRepresentationInterface.Views.Helpers;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for Environment.xaml
    /// </summary>
    public partial class Environment : UserControl, INotifyPropertyChanged
    {
        #region Properties

        private int _timeInf;
        private ObservableCollection<Fluent> _fluents;
        private ObservableCollection<WorldAction> _actions; 
        private ObservableCollection<WorldDescriptionRecord> _statements;
        private WorldDescriptionRecordType _selectedWDRecordType;
        private Dictionary<WorldDescriptionRecordType, EnvControl> StatementsControls;

        public event PropertyChangedEventHandler PropertyChanged;

        public WorldDescriptionRecordType SelectedWDRecordType
        {
            get { return _selectedWDRecordType; }
            set
            {
                _selectedWDRecordType = value;
                this.GruopBoxStatements.Content = StatementsControls[_selectedWDRecordType];
                NotifyPropertyChanged("SelectedWDRecordType");
            }
        }

        public ObservableCollection<Fluent> Fluents
        {
            get { return _fluents; }
            set { _fluents = value; }
        }

        public ObservableCollection<WorldAction> Actions
        {
            get { return _actions; }
            set { _actions = value; }
        }

        public ObservableCollection<WorldDescriptionRecord> Statements
        {
            get { return _statements; }
            set { _statements = value; }
        }
       
        public IEnumerable<WorldDescriptionRecordType> WDRecordType
        {
            get
            {
                return Enum.GetValues(typeof(WorldDescriptionRecordType)).Cast<WorldDescriptionRecordType>().Where(t => t != WorldDescriptionRecordType.Initially);
            }
        }
        
        #endregion

        #region Constructor
        public Environment()
        {
            _fluents = new ObservableCollection<Fluent>();
            _actions = new ObservableCollection<WorldAction>();
            _statements = new ObservableCollection<WorldDescriptionRecord>();
            InitControls();
            InitializeComponent();
        }

        #endregion

        #region Init methods
        private void InitControls()
        {
            StatementsControls = new Dictionary<WorldDescriptionRecordType, EnvControl>();
            StatementsControls.Add(WorldDescriptionRecordType.ActionCausesIf, new EnvCausesIf());
            StatementsControls.Add(WorldDescriptionRecordType.ActionInvokesAfterIf, new EnvInvokesAfterIf());
            StatementsControls.Add(WorldDescriptionRecordType.ActionReleasesIf, new EnvReleasesIf());
            StatementsControls.Add(WorldDescriptionRecordType.ExpressionTriggersAction, new EnvTriggers());
            StatementsControls.Add(WorldDescriptionRecordType.ImpossibleActionAt, new EnvImpossibleAt());
            StatementsControls.Add(WorldDescriptionRecordType.ImpossibleActionIf, new EnvImpossibleIf());
        }
        #endregion

        #region Property Changed
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        
        #region Buttons events

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateTimeInf())
                return;
            ParseFluentsToInitialRecords();

            /*
            string summary = "Time infinity: " + _timeInf + "\r\n" + "Fluents:\r\n" + FluentString + "\r\nStatements:\r\n" +
                             StatementsString;
            */

            Switcher.NextPage(_timeInf, _fluents.ToList(), _actions.ToList(), _statements.ToList(), "");//,_strStatement);
        }

        private void ButtonAddFluent_Click(object sender, RoutedEventArgs e)
        {
            if (!Fluents.Contains(Fluents.FirstOrDefault(f => (f.Name == TextBoxFluents.Text))))
            {
                var f = new Fluent { Name = this.TextBoxFluents.Text };
                Fluents.Add(f);
                LabelFluentsActionsValidation.Content = "";
            }
            else
            {
                LabelFluentsActionsValidation.Content = "Fluent with this name already exists.";
            }
        }

        private void ButtonRemoveFluent_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxFluents.SelectedIndex == -1)
                return;
            var fluent = (Fluent)ListBoxFluents.SelectedValue;
            Fluents.Remove(fluent);
        }

        private void ButtonAddAction_Click(object sender, RoutedEventArgs e)
        {
            int duration;
            if (!int.TryParse(TextBoxActionDuration.Text, out duration))
            {
                LabelFluentsActionsValidation.Content = "Duration should be an integer.";
                return;
            }
            if (!Actions.Contains(Actions.FirstOrDefault(f => (f.Id == TextBoxActionName.Text && f.Duration == duration))))
            {
                var action = new WorldAction {Id = TextBoxActionName.Text, Duration = duration};
                Actions.Add(action);
                LabelFluentsActionsValidation.Content = "";

            }
            else
            {
                LabelFluentsActionsValidation.Content = "Such action already exists.";
            }
        }

        private void ButtonRemoveAction_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxActions.SelectedIndex == -1)
                return;
            var action = (WorldAction)ListBoxActions.SelectedValue;
            Actions.Remove(action);
        }

        private void ButtonAddStatement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!StatementsControls.ContainsKey(SelectedWDRecordType)) //TODO hotfix
                {
                    return;
                }
                WorldDescriptionRecord wdr = StatementsControls[SelectedWDRecordType].GetWorldDescriptionRecord();
                Statements.Add(wdr);
                StatementsControls[SelectedWDRecordType].CleanValues();
            }
            catch (TypeLoadException exception)
            {
            }
        }

        private void ButtonRemoveStatement_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxStatements.SelectedIndex == -1)
                return;

            var wdr = (WorldDescriptionRecord)ListBoxStatements.SelectedValue;
            Statements.Remove(wdr);
        }

        #endregion

        #region Adding data into reasoning module

        private void ParseFluentsToInitialRecords()
        {
            foreach (var f in _fluents)
            {
                var ir = new InitialRecord(f.Name);
                _statements.Add(ir);
            }
        }

        private bool ValidateTimeInf()
        {
            if (!Int32.TryParse(TextBoxTimeInf.Text, out _timeInf))
            {
                LabelTimeInfValidation.Content = "It is necessary to fill default end time value.";
                return false;
            }
            LabelTimeInfValidation.Content = "";
            return true;
        }

        #endregion

       

       

        
    }
}
