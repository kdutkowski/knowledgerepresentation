using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationReasoning.Queries
{
    public class PerformingActionAtTimeQuery : Query
    {
        public PerformingActionAtTimeQuery(QuestionType questionType)
            : base(QueryType.PerformingActionAtTime, questionType){}

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            throw new NotImplementedException();
        }
    }
}
