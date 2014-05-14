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

            Initialize();
        }

        public Vertex(Vertex leaf)
        {
            Initialize();

            State = (State)leaf.State.Clone();
            WorldAction = (WorldAction)leaf.WorldAction.Clone();
            Time = leaf.Time;
            Root = leaf.Root;
            IsPossible = leaf.IsPossible;
            IsEnded = leaf.IsEnded;
            NextActions = leaf.NextActions.ToList();
        }

        public Vertex()
        {
            Initialize();
        }

        private void Initialize()
        {
            IsPossible = true;
            IsEnded = false;
            NextActions = new List<WorldAction>();
        }

        public int GetNextActionTime()
        {
            int nextTimeAction = -1;

            if (NextActions.Count > 0)
            {
                int? nextTime = NextActions.Min(x => x.StartAt);
                if (nextTime.HasValue)
                {
                    nextTimeAction = nextTime.Value;
                }
            }

            return nextTimeAction;
        }

        public List<Vertex> CreateChildsBasedOnImplications(List<Implication> implications, World.WorldAction worldAction, int nextTime)
        {
            List<Vertex> childs = new List<Vertex>();

            foreach (var implication in implications)
            {
                Vertex child = new Vertex(this);
                child.State = implication.FutureState;
                if (WorldAction.GetEndTime() <= nextTime)
                {
                    WorldAction = null;
                }
                if (nextTime == worldAction.StartAt)
                    this.WorldAction = (WorldAction)worldAction.Clone();

                child.NextActions = new List<WorldAction>();
                child.NextActions.AddRange(implication.TriggeredActions);

                child.Time = nextTime;
                child.IsPossible = child.ValidateActions();

                childs.Add(child);
            }
            return childs;
        }

        private class WorldActionTimeComparer : IComparer<WorldAction>
        {
            public int Compare(WorldAction a, WorldAction b)
            {
                if (a.StartAt == b.StartAt) return 0;
                if (a.StartAt > b.StartAt) return 1;
                else return -1;
            }
        }

        public bool ValidateActions()
        {
            SortActionsByStartTime(NextActions);
            
            for (int i = 0; i < NextActions.Count; i++)
            {
                if (this.WorldAction.StartAt <= this.NextActions[i].StartAt &&
                    this.NextActions[i].StartAt < this.WorldAction.GetEndTime()) return false;

                if (this.WorldAction.StartAt < this.NextActions[i].GetEndTime() &&
                    this.NextActions[i].GetEndTime() <= this.WorldAction.GetEndTime()) return false;

                if (i < NextActions.Count - 1)
                {
                    if (this.NextActions[i].StartAt <= this.NextActions[i + 1].StartAt &&
                    this.NextActions[i].GetEndTime() > this.NextActions[i + 1].StartAt) return false;

                    if (this.NextActions[i].StartAt < this.NextActions[i + 1].GetEndTime() &&
                        this.NextActions[i].GetEndTime() > this.NextActions[i + 1].GetEndTime()) return false;

                }
            }
            return true;
        }

        private void SortActionsByStartTime(List<World.WorldAction> NextActions)
        {
            NextActions.Sort(new WorldActionTimeComparer());
        }
    }
}
