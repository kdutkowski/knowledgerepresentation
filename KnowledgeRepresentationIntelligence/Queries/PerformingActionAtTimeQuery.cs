using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationReasoning.Queries
{
    public class PerformingActionAtTimeQuery : Query
    {
        public PerformingActionAtTimeQuery()
        {
            queryType = QueryType.PerformingActionAtTime;
        }

        public override QueryResult CheckCondition(World.State state, World.Action action, int time)
        {
            throw new NotImplementedException();
        }
    }
}
