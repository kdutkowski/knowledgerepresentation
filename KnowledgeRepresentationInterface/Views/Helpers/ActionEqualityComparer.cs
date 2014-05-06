using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationInterface.Views.Helpers
{
    using KnowledgeRepresentationReasoning.World;

    class ActionEqualityComparer : IEqualityComparer<WorldAction>
    {
        public bool Equals(WorldAction x, WorldAction y)
        {
            return ((x.Id == y.Id) && (x.Duration == y.Duration));
        }

        public int GetHashCode(WorldAction obj)
        {
            return base.GetHashCode();
        }
    }
}
