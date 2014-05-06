using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Action = KnowledgeRepresentationReasoning.World.Action;
namespace KnowledgeRepresentationInterface.Views.Helpers
{
    class ActionEqualityComparer : IEqualityComparer<Action>
    {
        public bool Equals(Action x, Action y)
        {
            return ((x.Id == y.Id) && (x.Duration == y.Duration));
        }

        public int GetHashCode(Action obj)
        {
            return base.GetHashCode();
        }
    }
}
