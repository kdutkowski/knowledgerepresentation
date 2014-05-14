using System;

namespace KnowledgeRepresentationReasoning.World
{
    public class WorldAction : ICloneable
    {
        public string Id { get; set; }
        public int? Duration { get; set; }
        public int? TriggeredAfter { get; set; }
        public int? StartAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is WorldAction)
            {
                var action = obj as WorldAction;
                if (action.Id.Equals(this.Id) && action.Duration.Equals(this.Duration))
                    return true;
            }
            return false;
        }

        public object Clone()
        {
            WorldAction worldAction = new WorldAction();
            worldAction.Id = Id;
            worldAction.Duration = Duration;
            worldAction.TriggeredAfter = TriggeredAfter;
            worldAction.StartAt = StartAt;

            return worldAction;
        }

        public int GetEndTime()
        {
            int time = -1;
            int? endTime = StartAt + Duration;

            if (endTime.HasValue)
            {
                time = endTime.Value;
            }

            return time;
        }

        public override string ToString()
        {
            string description = "Action (" + Id + ", " + Duration + ") with start time: " + StartAt;
            return description;
        }
    }
}
