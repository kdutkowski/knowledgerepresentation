using KnowledgeRepresentationReasoning.World;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeRepresentationReasoning.Logic
{
    public class Vertex
    {
        public State State { get; private set; }
        public Action Action { get; set; }
        public int Time { get; private set; }
        private Vertex Root { get; set; }
        public bool IsPossible { get; set; }
        public bool IsEnded { get; set; }

        public List<Action> NextActions { get; set; }

        public Vertex(State state, Action action, int time, Vertex root)
        {
            State = state;
            Action = action;
            Time = time;
            Root = root;
        }

        internal int? GetNextActionTime()
        {
            return NextActions.Min(action => action.StartAt);
        }
    }
}
