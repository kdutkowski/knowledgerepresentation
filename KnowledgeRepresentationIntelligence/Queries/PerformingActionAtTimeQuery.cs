using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationReasoning.Queries
{
    public class PerformingActionAtTimeQuery : Query
    {
        private WorldAction _WorldAction;
        private int _time;

        
        public PerformingActionAtTimeQuery(QuestionType questionType, WorldAction _WorldAction=null, int _time=-1)
            : base(QueryType.PerformingActionAtTime, questionType){
                this._WorldAction = _WorldAction;
                this._time = _time;
                _logger.Info("Creates:\n " + this.ToString());
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            _logger.Info("Checking Action: " + _WorldAction.ToString()+"at time: "+_time + "\nwith parameters:\nstate: " + state.ToString() + "\naction: " + worldAction??worldAction.ToString() + "\ntime: " + time);
            QueryResult result = QueryResult.None;
            if (time==_time || _time==-1)
            {
                if (this._WorldAction==null)
                {
                    if (worldAction != null) result = QueryResult.True;
                    else result = QueryResult.False;
                }
                else if (_WorldAction.Equals(worldAction))
                result = QueryResult.True;

            }
             else if (_time < time)
                result = QueryResult.False;
            else if (_time > time)
                result = QueryResult.Undefined;

              string logResult = "Method result: " + result;
            if (QueryResult.None == result)
                _logger.Warn(logResult);
            else
                _logger.Info(logResult);
 
            return result;
            
        }




        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("Action at Time Query:\nAction: ", 77);
            stringBuilder.Append(_WorldAction.ToString());
            stringBuilder.Append("\ntime:");
            stringBuilder.Append(_time);

            return stringBuilder.ToString();
        }

 
    }
}
