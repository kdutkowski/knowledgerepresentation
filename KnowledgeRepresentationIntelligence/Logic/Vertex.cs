using KnowledgeRepresentationReasoning.World;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeRepresentationReasoning.Logic
{
    public class Vertex
    {
        public State State { get; private set; }
        public WorldAction WorldAction { get; set; }
        public int Time { get; private set; }
        private Vertex Root { get; set; }
        public bool IsPossible { get; set; }
        public bool IsEnded { get; set; }

        public List<WorldAction> NextActions { get; set; }

        public Vertex(State state, WorldAction worldAction, int time, Vertex root)
        {
            State = state;
            this.WorldAction = worldAction;
            Time = time;
            Root = root;
        }

        public Vertex()
        {
            // TODO: Complete member initialization
        }

        public Vertex(Vertex leaf)
        {
            State = leaf.State;
            WorldAction = leaf.WorldAction;
            Time = leaf.Time;
            Root = leaf.Root;
            IsPossible = leaf.IsPossible;
            IsEnded = leaf.IsEnded;
        }

        internal int? GetNextActionTime()
        {
            return NextActions.Min(action => action.StartAt);
        }

        internal void Update(int nextTime)
        {
            UpdateStateOnFluentChange(nextTime);
            //int nextTime = UpdateAction();
            UpdateTime(nextTime);
        }

        private void UpdateStateOnFluentChange(int nextTime)
        {
            
        }

        private int UpdateAction()
        {
            int endTime = this.WorldAction.StartAt + this.WorldAction.Duration??-1;

            return 0;
        }

        private void UpdateTime(int newTime)
        {
            Time = newTime;
        }

        internal List<Vertex> CreateChildsBasedOnImplications(List<Implication> implications)
        {
            throw new System.NotImplementedException();
        }

        internal bool ValidateActions()
        {
            throw new System.NotImplementedException();
        }
    }
}
