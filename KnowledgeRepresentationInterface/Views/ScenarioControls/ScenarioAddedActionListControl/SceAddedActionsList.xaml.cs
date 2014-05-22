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
using KnowledgeRepresentationReasoning.World;

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
            get
            {
                return _addedActionsList;
            }
            set
            {
                _addedActionsList = value;
            }
        }

        public SceAddedActionsList()
        {
            InitializeComponent();
            InitialAddedActions();
        }

        public bool AddAction(int time, string action, int? duration)
        {
            AddedAction addedAction = new AddedAction()
            {
                ActionName = action,
                Time = time,
                Duration = duration
            };

            if(ValidateAddedAction(addedAction))
            {
                AddAddedActionToList(addedAction);
                AddActionToStackPanel(addedAction);
                return true;
            }
            return false;
        }

        private bool ValidateAddedAction(AddedAction addedAction)
        {
            foreach(AddedAction item in _addedActionsList)
            {
                if(addedAction.Name == item.Name && addedAction.Time == item.Time && addedAction.Duration == item.Duration)
                    return false;
            }
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
            foreach(var item in _addedActionsList)
            {
                StackPanelActionList.Children.Add(item);
            }
        }

        private void InitialAddedActions()
        {
            _addedActionsList = new List<AddedAction>();
        }

        internal bool AddObservation(int time, string expression)
        {
            AddedAction addedAction = new AddedAction()
            {
                ActionName = String.Empty,
                Time = time,
                Expression = expression
            };

            if(ValidateAddedObservation(addedAction))
            {
                AddAddedActionToList(addedAction);
                AddActionToStackPanel(addedAction);
                return true;
            }
            return false;
        }

        private bool ValidateAddedObservation(AddedAction addedAction)
        {
            //TODO validate observation
            return true;
        }

        internal void CleanValues()
        {
            InitialAddedActions();
            StackPanelActionList.Children.Clear();
        }
    }
}