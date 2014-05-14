﻿using KnowledgeRepresentationReasoning.World;
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
                OnPropertyChanged("SelectedWARecordType");
            }
        }
        
        private List<WorldAction> _actions = new List<WorldAction>();
        
        public IEnumerable<WorldAction> WARecordType 
        {   
            get { return _actions.Cast<WorldAction>(); }
            set { _actions = value.ToList();
            OnPropertyChanged("WARecordType");
            }
        }
        public SceAddAction()
        {
            InitializeComponent();
        }
        public void CleanValues()
        {
            throw new NotImplementedException();
        }

        public void SetActions(List<WorldAction> actions)
        {
            //_actions = actions;
            WARecordType = actions as IEnumerable<WorldAction>;
        }
    }

}
