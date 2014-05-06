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
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationInterface.Views
{
    /// <summary>
    /// Interaction logic for _Results.xaml
    /// </summary>
    public partial class _Results : UserControl
    {
        #region Properties

        private int _timeInf;
        private List<string> _scenarioNames; 
        private List<WorldAction> _actions;
        private List<Fluent> _fluents; 

        #endregion

        #region Visualization Properties
        private QueryType _selectedQueryType;
        public QueryType SelectedQuestionType { get; set; }
        public QueryType SelectedQueryType
        {
            get { return _selectedQueryType; }
            set
            {
                _selectedQueryType = value;
                //this.GruopBoxStatements.Content = StatementsControls[_selectedQueryType];
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
        public _Results()
        {
            InitializeComponent();
        }
        #endregion

        #region Property Changed
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Buttons Events
        

        #endregion
    }
}
