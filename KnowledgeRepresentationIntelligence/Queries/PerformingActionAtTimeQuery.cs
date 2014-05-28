namespace KnowledgeRepresentationReasoning.Queries
{
    using System.Text;

    using KnowledgeRepresentationReasoning.World;

    public class PerformingActionAtTimeQuery : Query
    {
        private readonly WorldAction _worldAction;

        private readonly int _time;

        public PerformingActionAtTimeQuery(QuestionType questionType, WorldAction worldAction = null, int time = -1)
            : base(QueryType.PerformingActionAtTime, questionType)
        {
            this._worldAction = worldAction;
            this._time = time;
            _logger.Info("Creates:\n " + this);
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            _logger.Info("Checking Action: " + this._worldAction + "at time: " + this._time + "\nwith parameters:\nstate: " + state + "\naction: " + worldAction);

            var result = QueryResult.Undefined;

            if (time == _time || _time == -1)
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
                else if (this._worldAction.Equals(worldAction))
                {
                    result = QueryResult.True;
                }
                else
                {
                    result = QueryResult.False;
                }
            }
            else if (_time < time)
            {
                result = QueryResult.False;
            }
            else if (_time > time)
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
            stringBuilder.Append(this._worldAction.ToString());
            stringBuilder.Append("\ntime:");
            stringBuilder.Append(_time);

            return stringBuilder.ToString();
        }
    }
}