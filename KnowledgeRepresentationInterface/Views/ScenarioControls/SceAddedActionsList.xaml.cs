﻿using KnowledgeRepresentationReasoning.World;
using System;
using System.Collections.Generic;
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

namespace KnowledgeRepresentationInterface.Views.ScenarioControls
{
    /// <summary>
    /// Interaction logic for SceAddedActionsList.xaml
    /// </summary>
    public partial class SceAddedActionsList : UserControl
    {
        private List<AddedAction> _addedActionsList;

        public List<AddedAction> AddedActionsList
        {
            get { return _addedActionsList; }
            set { _addedActionsList = value; }
        }
        
        
        public SceAddedActionsList()
        {
            InitializeComponent();
            InitialAddedActions();
        }

        public bool AddAction(int time, List<Fluent> fluents, string action)
        {
            AddedAction addedAction = new AddedAction() { ActionName = action, Time = time, Fluents = fluents };

            if (ValidateAddedAction(addedAction))
            {
                AddAddedActionToList(addedAction);
                AddActionToStackPanel(addedAction);
            }
            return false;
        }

        private bool ValidateAddedAction(AddedAction addedAction)
        {
            //TODO validate action
            return true;
        }

        private void AddAddedActionToList(AddedAction addedAction)
        {
            _addedActionsList.Add(addedAction);
            _addedActionsList = _addedActionsList.OrderBy(x => x.Time).ToList(); 
        }

        private void AddActionToStackPanel(AddedAction addedAction)
        {
            StackPanelActionList.Children.Clear();
            foreach (var item in _addedActionsList)
            {
                StackPanelActionList.Children.Add(item);
            }
        }

        private void InitialAddedActions()
        {
            _addedActionsList = new List<AddedAction>();
        }
    }
}