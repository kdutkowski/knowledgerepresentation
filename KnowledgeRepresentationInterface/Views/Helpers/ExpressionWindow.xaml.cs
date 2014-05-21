using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationInterface.Views.Helpers
{
    /// <summary>
    /// Interaction logic for ExpressionWindow.xaml
    /// </summary>
    public partial class ExpressionWindow : Window
    {
        public Fluent SelectedFluent { get; set; }
        public ObservableCollection<Fluent> Fluents { get; set; }
        private string _expressionString = "";
        public string Expression
        {
            get { return _expressionString.Replace("#", " "); }
        }
        

        private int _leftBrCount = 0;
        private int _rightBrCount = 0;

        public ExpressionWindow(ObservableCollection<Fluent> fluentsCollection)
        {
            Fluents = fluentsCollection;
            InitializeComponent();
        }

        private void AddPartExpression(string part)
        {
            _expressionString += part + "#";
            TextBlockExpression.Text = _expressionString.Replace("#", " ");
        }

        #region Buttons Events
        private void ButtonAddFluent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFluent == null)
                return;
            AddPartExpression(SelectedFluent.ToString());
            EnDisAfterFluent();
        }
        
        private void ButtonAddNeg_Click(object sender, RoutedEventArgs e)
        {
            AddPartExpression("!");
            EnDisAfterNeg();
        }

        private void ButtonAddLeftBracket_Click(object sender, RoutedEventArgs e)
        {
            _leftBrCount++;
            AddPartExpression("(");
            EnDisAfterLeftBracket();
        }

        private void ButtonAddRightBracket_Click(object sender, RoutedEventArgs e)
        {
            _rightBrCount++;
            AddPartExpression(")");
            EnDisAfterRightBracket();
        }

        private void ButtonAddAnd_Click(object sender, RoutedEventArgs e)
        {
            AddPartExpression("&&");
            EnDisAfterAnd();
        }

        private void ButtonAddOr_Click(object sender, RoutedEventArgs e)
        {
            AddPartExpression("||");
            EnDisAfterOr();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (_expressionString == "")
                return;

            var splittedString = _expressionString.Split('#');
            int lastIndex = splittedString.Count() - 1;
            _expressionString = "";
            for (int i = 0; i < splittedString.Count() - 1; i++)
                _expressionString += splittedString + "#";
            TextBlockExpression.Text = _expressionString.Replace("#", " ");

            switch (splittedString[lastIndex])
            {
                case "!":
                    EnDisAfterNeg();
                    break;
                case "&&":
                    EnDisAfterAnd();
                    break;
                case "||":
                    EnDisAfterOr();
                    break;
                case "(":
                    _leftBrCount --;
                    EnDisAfterLeftBracket();
                    break;
                case ")":
                    _rightBrCount --;
                    EnDisAfterRightBracket();
                    break;
                default:
                    EnDisAfterFluent();
                    break;
            }
        }

         private void ButtonFinish_Click(object sender, RoutedEventArgs e)
         {
             this.DialogResult = true;
             this.Close();
         }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        #endregion

        #region Enable and Disable Buttons

        private void EnDisAfterFluent()
        {
            //enable and disable buttons -  po fluencie występuje &&, || lub )
            ButtonAddAnd.IsEnabled = true;
            ButtonAddOr.IsEnabled = true;
            ButtonAddFluent.IsEnabled = false;
            ButtonAddLeftBracket.IsEnabled = false;
            ButtonAddNeg.IsEnabled = false;
            if (_leftBrCount > _rightBrCount)
                ButtonAddRightBracket.IsEnabled = true;
            if (_leftBrCount == _rightBrCount)
                ButtonFinish.IsEnabled = true;
        }

        private void EnDisAfterNeg()
        {
            //enable and disable buttons - po ! występuje fluent lub (
            ButtonAddFluent.IsEnabled = true;
            ButtonAddLeftBracket.IsEnabled = true;
            ButtonAddNeg.IsEnabled = false;
            ButtonAddAnd.IsEnabled = false;
            ButtonAddOr.IsEnabled = false;
            ButtonAddRightBracket.IsEnabled = false;
            ButtonFinish.IsEnabled = false;
        }

        private void EnDisAfterLeftBracket()
        {
            //enable and disable buttons - po ( występuje fluent, ( lub !
            ButtonAddFluent.IsEnabled = true;
            ButtonAddLeftBracket.IsEnabled = true;
            ButtonAddNeg.IsEnabled = true;
            ButtonAddAnd.IsEnabled = false;
            ButtonAddOr.IsEnabled = false;
            ButtonAddRightBracket.IsEnabled = false;
            ButtonFinish.IsEnabled = false;

        }

        private void EnDisAfterRightBracket()
        {
            //enable and disable buttons - po ) występuje ), $$ lub ||
            
            ButtonAddAnd.IsEnabled = true;
            ButtonAddOr.IsEnabled = true;
            ButtonAddFluent.IsEnabled = false;
            ButtonAddLeftBracket.IsEnabled = false;
            ButtonAddNeg.IsEnabled = false;
            if (_leftBrCount > _rightBrCount)
                ButtonAddRightBracket.IsEnabled = true;
            if (_leftBrCount == _rightBrCount)
                ButtonFinish.IsEnabled = true;
        }

        private void EnDisAfterAnd()
        {
            //enable and disable buttons - po && występuje fluent, ( lub !
            ButtonAddFluent.IsEnabled = true;
            ButtonAddLeftBracket.IsEnabled = true;
            ButtonAddNeg.IsEnabled = true;
            ButtonAddRightBracket.IsEnabled = false;
            ButtonAddAnd.IsEnabled = false;
            ButtonAddOr.IsEnabled = false;
            ButtonFinish.IsEnabled = false;
        }

        private void EnDisAfterOr()
        {
            //enable and disable buttons - po || występuje fluent, ( lub !
            ButtonAddFluent.IsEnabled = true;
            ButtonAddLeftBracket.IsEnabled = true;
            ButtonAddNeg.IsEnabled = true;
            ButtonAddRightBracket.IsEnabled = false;
            ButtonAddAnd.IsEnabled = false;
            ButtonAddOr.IsEnabled = false;
            ButtonFinish.IsEnabled = false;
        }

        #endregion
    }
}
