namespace KnowledgeRepresentationReasoning.Queries
{
    using System.Text;

    using KnowledgeRepresentationReasoning.World;
    using KnowledgeRepresentationReasoning.Logic;

    public class PerformingActionAtTimeQuery : Query
    {
        private readonly WorldAction _worldAction;

        public PerformingActionAtTimeQuery(QuestionType questionType, WorldAction worldAction = null, int time = -1)
            : base(QueryType.PerformingActionAtTime, questionType)
        {
            this._worldAction = worldAction;
            this.Time = time;

            _logger.Info("Creates:\n " + this);
        }

        public override QueryResult CheckCondition(Vertex vertex)
        {
            _logger.Info("Checking Action: " + this._worldAction + "at time: " + this.Time + "\nwith parameters:\nstate: " + vertex.ActualState + "\naction: " + vertex.ActualWorldAction);

            var result = QueryResult.Undefined;

            if (-1 == Time)
            {
                result = CheckAction(vertex.ActualWorldAction);
                if (result != QueryResult.True)
                {
                    result = QueryResult.False == result ? QueryResult.Undefined : QueryResult.False;
                }
            }
            else if (Time == vertex.Time)
            {
                result = CheckAction(vertex.ActualWorldAction);
            }
            else if (Time < vertex.Time)
            {
                WorldAction parentAction = vertex.GetParentAction();
                result = CheckAction(parentAction);
            }

            string logResult = "Method result: " + result;

            if (QueryResult.Undefined == result)
            {
                _logger.Warn(logResult);
            }
            else
            {
                _logger.Info(logResult);
            }

            return result;
        }

        private QueryResult CheckAction(WorldAction action)
        {
            QueryResult result = QueryResult.Undefined;
            if (action == null)
            {
                result = QueryResult.False;
            }
            else if (_worldAction.Id == action.Id)
            {
                result = QueryResult.True;
            }
            else
            {
                result = QueryResult.False;
            }

            return result;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Action at Time Query:\nAction: ", 77);
            stringBuilder.Append(this._worldAction.Id);
            stringBuilder.Append("\ntime:");
            stringBuilder.Append(Time);

            return stringBuilder.ToString();
        }
    }
}