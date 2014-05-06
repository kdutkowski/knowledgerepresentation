using System;

namespace KnowledgeRepresentationReasoning.World
{
    public class Action: ICloneable
    {
        public string Id { get; set; }
        public int? Duration { get; set; }
        public int? TriggeredAfter { get; set; }
        public int? StartAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Action)
            {
                var action = obj as Action;
                if (action.Id.Equals(this.Id) && action.Duration.Equals(this.Duration))
                    return true;
            }
            return false;
        }

        public object Clone()
        {
            Action action = new Action();
            action.Id = Id;
            action.Duration = Duration;
            action.TriggeredAfter = TriggeredAfter;
            action.StartAt = StartAt;

            return action;
        }

        internal int? GetEndTime()
        {
            return StartAt + Duration;
        }

        public override string ToString()
        {
            return "(" + Id + ", " + Duration + ")";
            /*
            string description = "Action (" + Id + ", " + Duration + ") with start time: " + StartAt;

            return description;
             * */
        }
    }
}
