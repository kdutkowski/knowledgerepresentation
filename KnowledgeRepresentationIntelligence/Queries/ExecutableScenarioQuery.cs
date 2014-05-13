using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationReasoning.Queries
{
    class ExecutableScenarioQuery : Query
    {
        public ExecutableScenarioQuery(QuestionType questionType)
            : base(QueryType.ExecutableScenario, questionType) { }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            throw new NotImplementedException();
        }
    }
}
