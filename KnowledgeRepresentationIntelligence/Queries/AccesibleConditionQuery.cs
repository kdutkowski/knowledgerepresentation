﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationReasoning.Queries
{
    public class AccesibleConditionQuery : Query
    {
        public AccesibleConditionQuery()
            : base()
        {
            queryType = QueryType.AccesibleCondition;
        }

        public override QueryResult CheckCondition(World.State state, World.Action action, int time)
        {
            throw new NotImplementedException();
        }
    }
}