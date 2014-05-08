using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using KnowledgeRepresentationInterface.Views.QueriesControls;
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : UserControl
    {
        #region Properties

        private int _timeInf;
        private List<string> _scenarioNames; 
        private List<WorldAction> _actions;
        private List<Fluent> _fluents; 

        #endregion

        #region Visualization Properties
        private Dictionary<QueryType, UserControl> QueriesControls;

        private QueryType _selectedQueryType;
        public QueryType SelectedQuestionType { get; set; }
        public QueryType SelectedQueryType
        {
            get { return _selectedQueryType; }
            set
            {
                _selectedQueryType = value;
                this.GroupBoxQuery.Content = QueriesControls[_selectedQueryType];
                NotifyPropertyChanged("SelectedQueryType");
            }
        }

        public IEnumerable<QueryType> QueryRecordType
        {
            get
            {
                return Enum.GetValues(typeof(QueryType)).Cast<QueryType>();
            }
        }
        public IEnumerable<QuestionType> QuestionRecordType
        {
            get
            {
                return Enum.GetValues(typeof(QuestionType)).Cast<QuestionType>();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public Results()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            //todo zmienic userControl na QueControl
            //todo zaimplementowac pozostale kontrolki
            QueriesControls = new Dictionary<QueryType, UserControl>();
            QueriesControls.Add(QueryType.SatisfyConditionAtTime, new QueConditionAtTime(_timeInf, _scenarioNames, _actions, _fluents));
            QueriesControls.Add(QueryType.AccesibleCondition, new UserControl());
            QueriesControls.Add(QueryType.ExecutableScenario, new UserControl());
            QueriesControls.Add(QueryType.PerformingActionAtTime, new UserControl());
        }
        #endregion

        #region Property Changed
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            //todo pozostałe typy
            if (SelectedQueryType == QueryType.SatisfyConditionAtTime)
            {
                Query q = ((QueControl) QueriesControls[SelectedQueryType]).GetQuery();
                QueryResult qr = Switcher.ExecuteQuery(q);
                LabelResult.Content = qr;
            }
            
        }

        #region Buttons Events
        

        #endregion
    }
}
