using System.Collections.Generic;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationReasoning.Helpers.Comparers
{
    public class WorldActionTimeComparer : IComparer<WorldAction>
    {
        public int Compare(WorldAction a, WorldAction b)
        {
            if (a.StartAt == b.StartAt) return 0;
            if (a.StartAt > b.StartAt) return 1;
            else return -1;
        }
    }
}