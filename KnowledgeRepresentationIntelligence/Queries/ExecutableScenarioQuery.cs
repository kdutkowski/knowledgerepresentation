using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationReasoning.Queries
{
    class ExecutableScenarioQuery : Query
    {
        public ExecutableScenarioQuery()
        {
            queryType = QueryType.ExecutableScenario;
        }

        public override QueryResult CheckCondition(World.State state, World.Action action, int time)
        {
            throw new NotImplementedException();
        }
    }
}
