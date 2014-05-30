namespace KnowledgeRepresentationReasoning.Queries
{
    using System.Text;

    using KnowledgeRepresentationReasoning.World;

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

        public override QueryResult CheckCondition(State state, WorldAction worldAction, int time)
        {
            _logger.Info("Checking Action: " + this._worldAction + "at time: " + this.Time + "\nwith parameters:\nstate: " + state + "\naction: " + worldAction);

            var result = QueryResult.Undefined;

            if (time == Time || Time == -1 || Time < time)
            {
                if (this._worldAction == null)
                {
                    if (worldAction == null)
                    {
                        result = QueryResult.True;
                    }
                    else
                    {
                        result = QueryResult.False;
                    }
                }
                else if (this._worldAction.Id == worldAction.Id) // Porównuje tylko id bo długość nie ma tu znaczenia
                {
                    result = QueryResult.True;
                }
                else
                {
                    result = QueryResult.False;
                }
            }
            else if (Time > time)
            {
                result = QueryResult.Undefined;
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