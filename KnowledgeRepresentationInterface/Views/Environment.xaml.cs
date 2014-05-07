using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for _Environment.xaml
    /// </summary>
    public partial class _Environment : UserControl, INotifyPropertyChanged
    {
        #region Properties

        private int _timeInf;
        private List<Fluent> _fluents;
        private List<WorldAction> _actions; 
        private List<WorldDescriptionRecord> _statements;
        private WorldDescriptionRecordType _selectedWDRecordType;

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
        #endregion

        #region Properties to show summary
        private String _strFluent;
        private String _strStatement;
        private Dictionary<WorldDescriptionRecordType, EnvControl> StatementsControls;

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

        public IEnumerable<WorldDescriptionRecordType> WDRecordType
        {
            get
            {
                return Enum.GetValues(typeof(WorldDescriptionRecordType)).Cast<WorldDescriptionRecordType>().Where(t => t != WorldDescriptionRecordType.Initially);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public _Environment()
        {
            _fluents = new List<Fluent>();
            _actions = new List<WorldAction>();
            _statements = new List<WorldDescriptionRecord>();
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
            string summary = "Time infinity: " + _timeInf + "\r\n" + "Fluents:\r\n" + FluentString + "\r\nStatements:\r\n" +
                             StatementsString;

            Switcher.Switch(new _Scenario(_fluents, _actions), _timeInf, _fluents, _actions, _statements);
        }

        private void ButtonAddFluent_Click(object sender, RoutedEventArgs e)
        {
            if (!_fluents.Exists(f => (f.Name == TextBoxFluents.Text)))
            {//validation
                var f = new Fluent();
                f.Name = TextBoxFluents.Text;
                _fluents.Add(f);
                FluentString += TextBoxFluents.Text;
                LabelFluentsValidation.Content = "";
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
                LabelFluentsValidation.Content = "";
            }
            else
            {
                LabelFluentsValidation.Content = "Fluent with this name does not exist.";
            }
        }

        private void ButtonAddStatement_Click(object sender, RoutedEventArgs e)
        {
            WorldDescriptionRecord wdr;
            try
            {
                if (!StatementsControls.ContainsKey(SelectedWDRecordType)) //TODO hotfix
                {
                    return;
                }
                wdr = StatementsControls[SelectedWDRecordType].GetWorldDescriptionRecord();
                _actions.AddRange(StatementsControls[SelectedWDRecordType].GetAllCreatedActions());
                _actions = _actions.Distinct(new ActionEqualityComparer()).ToList();
                _statements.Add(wdr);
                StatementsString += wdr.ToString();
                StatementsControls[SelectedWDRecordType].CleanValues();
            }
            catch (TypeLoadException exception)
            {
            }
            catch(NotImplementedException exception)
            {
                //TODO when implemented everywhere - delete this catch
            }
            
            
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
